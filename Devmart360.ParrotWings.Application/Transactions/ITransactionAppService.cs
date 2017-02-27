using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Devmart360.ParrotWings.Transactions.Dto;
using Devmart360.ParrotWings.Users.Dto;

namespace Devmart360.ParrotWings.Transactions
{
    public interface ITransactionAppService : IApplicationService
    {
        TransactionOutputDto GetAll();

        double GetBalance();

        TransactionInfoDto Create(TransactionInputDto transactionInputDto);

        ListResultDto<UserListDto> Find(UserFilterInputDto termUserFilterInputDto);
    }
}
