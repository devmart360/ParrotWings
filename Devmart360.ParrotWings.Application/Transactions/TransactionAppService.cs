using System;
using System.Collections.Generic;
using System.Linq;
using Abp;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Abp.Collections.Extensions;
using Abp.Domain.Repositories;
using Abp.Extensions;
using Abp.Notifications;
using Abp.UI;
using AutoMapper;
using Devmart360.ParrotWings.IRepositories;
using Devmart360.ParrotWings.Models;
using Devmart360.ParrotWings.Notifications;
using Devmart360.ParrotWings.Transactions.Dto;
using Devmart360.ParrotWings.Users;
using Devmart360.ParrotWings.Users.Dto;

namespace Devmart360.ParrotWings.Transactions
{
    public class TransactionAppService : ParrotWingsAppServiceBase, ITransactionAppService
    {
        private readonly ITransactionRepository _transactionRepository;
        private readonly IRepository<User, long> _userRepository;
        private readonly INotificationPublisher _notificationPublisher;

        public TransactionAppService(ITransactionRepository transactionRepository, IRepository<User, long> userRepository, INotificationPublisher notificationPublisher)
        {
            _transactionRepository = transactionRepository;
            _userRepository = userRepository;
            _notificationPublisher = notificationPublisher;
        }

        /// <summary>
        /// Получить все переводы
        /// </summary>
        /// <returns></returns>
        public TransactionOutputDto GetAll()
        {
            var currentUserId = AbpSession.UserId;
            if (currentUserId != null)
            {
                var transactions = _transactionRepository.GetUserTransactions(currentUserId.Value);

                var transactionsDtos = Mapper.Map<List<TransactionInfoDto>>(transactions);
                return new TransactionOutputDto()
                {
                    Transactions = transactionsDtos
                };
            }
            return null;
        }

        public double GetBalance()
        {
            var currentUserId = AbpSession.UserId;
            if (currentUserId != null)
                return _transactionRepository.GetCurrentBalance(currentUserId.Value);
            return 0;
        }

        /// <summary>
        /// Создание нового перевода
        /// </summary>
        /// <param name="transactionInputDto"></param>
        /// <returns></returns>
        public TransactionInfoDto Create(TransactionInputDto transactionInputDto)
        {
            var recipientUserId = transactionInputDto.RecipientUserId;
            var amount = transactionInputDto.Amount;

            var currentBalance = GetBalance();
            if (amount > currentBalance)
            {
                throw new UserFriendlyException("Суммы на счёте недостаточно для перевода");
            }

            var recipientUser = _userRepository.Get(recipientUserId);
            if (AbpSession.UserId == null)
            {
                throw new UserFriendlyException("Текущий пользователь не найден");
            }

            var creator = _userRepository.Get(AbpSession.UserId.Value);

            if (creator.Id == recipientUser.Id)
            {
                throw new UserFriendlyException("Невозможно выполнить перевод этому же пользователю");
            }

            var transaction = new Transaction()
            {
                Amount = amount,
                CreatorUser = creator,
                RecipientUser = recipientUser,
                Type = -1
            };

            var id = _transactionRepository.InsertAndGetId(transaction);
            transaction = _transactionRepository.Get(id);

            _notificationPublisher.Publish("BalanceChanged",
                new BalanceChangedNotificationData(_transactionRepository.GetCurrentBalance(creator.Id)),
                userIds: new[]
                {
                    new UserIdentifier(creator.TenantId, creator.Id)
                });
            _notificationPublisher.Publish("BalanceChanged",
                new BalanceChangedNotificationData(_transactionRepository.GetCurrentBalance(recipientUser.Id)),
                userIds: new[]
                {
                    new UserIdentifier(creator.TenantId, recipientUser.Id)
                });

            return Mapper.Map<TransactionInfoDto>(transaction);
        }

        public ListResultDto<UserListDto> Find(UserFilterInputDto termUserFilterInputDto)
        {
            // note: этот метод в UserAppService не работает для всех из-за атрибута [AbpAuthorize()], надо разобраться с разрешениями Users

            var currentUserId = AbpSession.UserId;

            var users = UserManager.Users.WhereIf(!termUserFilterInputDto.SearchTerm.IsNullOrWhiteSpace(),
                u => u.Name.Contains(termUserFilterInputDto.SearchTerm) ||
                     u.Surname.Contains(termUserFilterInputDto.SearchTerm) ||
                     u.UserName.Contains(termUserFilterInputDto.SearchTerm) ||
                     u.EmailAddress.Contains(termUserFilterInputDto.SearchTerm))
                     .Where(u => u.Id != currentUserId);

            return new ListResultDto<UserListDto>(
                users.MapTo<List<UserListDto>>()
                );
        }
    }
}