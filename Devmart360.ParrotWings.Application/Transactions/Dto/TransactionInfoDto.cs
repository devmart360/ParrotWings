using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.AutoMapper;
using Devmart360.ParrotWings.Models;
using Devmart360.ParrotWings.Users;

namespace Devmart360.ParrotWings.Transactions.Dto
{
    [AutoMapFrom(typeof(Transaction))]
    public class TransactionInfoDto
    {
        public DateTime CreationTime { get; set; }
        
        public double Amount { get; set; }
        
        public string CreatorUserName { get; set; }
        
        public string RecipientUserName { get; set; }
    }


}
