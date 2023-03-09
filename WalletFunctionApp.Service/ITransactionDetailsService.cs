using System;
using System.Collections.Generic;
using System.Diagnostics.SymbolStore;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WalletFunctionApp.Domain.Models.RequestModel;

namespace WalletFunctionApp.Service
{
    public interface ITransactionDetailsService
    {
        bool AddTransaction(TransactionRequestModel transactionRequestModel);
    }
}
