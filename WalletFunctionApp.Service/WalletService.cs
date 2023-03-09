using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WalletFunctionApp.Domain.Models.ResponseModel;
using WalletFunctionApp.Service.Data;

namespace WalletFunctionApp.Service
{
    public class WalletService : IWalletService
    {
        private readonly IDataLayer _dataLayer;
        public WalletService(IDataLayer dataLayer)
        {
            this._dataLayer = dataLayer;
        }
        public decimal GetWalletBalance(int accountId)
        {
            return _dataLayer.GetWalletBalance(accountId);
        }

        public WalletResponseModel GetWalletDetailsByAccountId(int accountId)
        {
            return _dataLayer.GetWalletDetailsByAccountId(accountId);
        }
    }
}
