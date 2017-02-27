using Abp;
using Abp.Dependency;
using Abp.Domain.Repositories;
using Abp.Events.Bus.Handlers;
using Abp.Notifications;
using Devmart360.ParrotWings.EventModels;
using Devmart360.ParrotWings.IRepositories;
using Devmart360.ParrotWings.Models;
using Devmart360.ParrotWings.Notifications;
using Devmart360.ParrotWings.Users;

namespace Devmart360.ParrotWings.EventHandlers
{
    /// <summary>
    /// Зачисление баланса при регистрации пользователя
    /// </summary>
    public class StartBalanceHandler : IEventHandler<UserCreatedEventData>, ITransientDependency
    {
        private readonly IRepository<User, long> _userRepository;
        private readonly ITransactionRepository _transactionRepository;
        private readonly INotificationPublisher _notificationPublisher;

        /// <summary>
        /// Initializes a new instance of the <see cref="T:System.Object"/> class.
        /// </summary>
        public StartBalanceHandler(IRepository<User, long> userRepository, ITransactionRepository transactionRepository, INotificationPublisher notificationPublisher)
        {
            _userRepository = userRepository;
            _transactionRepository = transactionRepository;
            _notificationPublisher = notificationPublisher;
        }

        public void HandleEvent(UserCreatedEventData eventData)
        {
            var user = _userRepository.Get(eventData.UserId);

            // зачислить на счёт 500 PW
            var transaction = new Transaction
            {
                Amount = 500,
                RecipientUser = user,
                CreatorUser = user,  // todo: создатель может быть "админ" или "система"
                Type = 1,

                // остальные поля аудита заполняются автоматически
            };

            _transactionRepository.Insert(transaction);

            _notificationPublisher.Publish("BalanceChanged",
                new BalanceChangedNotificationData(transaction.Amount),
                userIds: new[] { new UserIdentifier(user.TenantId, user.Id), });
        }
    }
}