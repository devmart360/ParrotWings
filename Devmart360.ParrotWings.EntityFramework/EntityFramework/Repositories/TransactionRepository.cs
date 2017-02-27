using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.EntityFramework;
using Devmart360.ParrotWings.IRepositories;
using Devmart360.ParrotWings.Models;
using Devmart360.ParrotWings.Users;

namespace Devmart360.ParrotWings.EntityFramework.Repositories
{
    public class TransactionRepository : ParrotWingsRepositoryBase<Transaction, long>, ITransactionRepository
    {
        public TransactionRepository(IDbContextProvider<ParrotWingsDbContext> dbContextProvider)
            : base(dbContextProvider)
        {

        }

        public List<Transaction> GetUserTransactions(long creatorId)
        {
            var query = this.GetAll();

            return query.Where(x => x.CreatorUser.Id == creatorId || x.RecipientUser.Id == creatorId)
                .OrderByDescending(x => x.CreationTime)
                .Include(x => x.CreatorUser)
                .Include(x => x.RecipientUser)
                .ToList();
        }

        public double GetCurrentBalance(long userId)
        {
            var query = this.GetAll();
            var debit = query.Where(x => x.CreatorUserId != x.RecipientUser.Id && x.CreatorUserId == userId).Select(x => x.Amount).DefaultIfEmpty(0).Sum();
            var credit = query.Where(x => x.RecipientUser.Id == userId).Select(x => x.Amount).DefaultIfEmpty(0).Sum();
            return credit - debit;
        }
    }
}
