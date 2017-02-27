using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using Devmart360.ParrotWings.Models;
using Devmart360.ParrotWings.Users;

namespace Devmart360.ParrotWings.IRepositories
{
    public interface ITransactionRepository : IRepository<Transaction, long>
    {
        List<Transaction> GetUserTransactions(long creatorId);

        double GetCurrentBalance(long userId);
    }
}
