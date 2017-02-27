using Abp.AutoMapper;
using Devmart360.ParrotWings.Users;

namespace Devmart360.ParrotWings.Transactions.Dto
{
    public class TransactionInputDto
    {
         public long RecipientUserId { get; set; }

         public double Amount { get; set; }
    }
}