using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WalletFunctionApp.Domain.Models.RequestModel;
using WalletFunctionApp.Domain.Models.ResponseModel;

namespace WalletFunctionApp.Service.Data
{
    public interface IDataLayer
    {
        void AddTransaction(TransactionRequestModel transactionRequestModel);
        decimal GetWalletBalance(int accountId);
        WalletResponseModel GetWalletDetailsByAccountId(int accountId);
    }
}
