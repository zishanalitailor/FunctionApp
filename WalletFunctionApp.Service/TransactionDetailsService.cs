using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WalletFunctionApp.Domain.Models.RequestModel;
using WalletFunctionApp.Service.Data;

namespace WalletFunctionApp.Service
{
    public class TransactionDetailsService : ITransactionDetailsService
    {
        private readonly IDataLayer _dataLayer;
        public TransactionDetailsService(IDataLayer dataLayer)
        {
            this._dataLayer = dataLayer;
        }
        public bool AddTransaction(TransactionRequestModel transactionRequestModel)
        {
            var isAdded = false;
            try
            {
                this._dataLayer.AddTransaction(transactionRequestModel);
                isAdded = true;
            }
            catch (Exception ex)
            {
                isAdded = false;
            }
            return isAdded;            
        }
    }
}
