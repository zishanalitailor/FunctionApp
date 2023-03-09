using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WalletFunctionApp.Domain.Models.ResponseModel;

namespace WalletFunctionApp.Service
{
    public interface IWalletService
    {
        decimal GetWalletBalance(int accountId);

        WalletResponseModel GetWalletDetailsByAccountId(int accountId);
    }
}
