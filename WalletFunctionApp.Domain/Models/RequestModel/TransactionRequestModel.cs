using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WalletFunctionApp.Domain.Models.RequestModel
{
    public class TransactionRequestModel
    {
        public int Id { get; set; }
        public decimal Amount { get; set; }
        public string Direction { get; set; }
        public int Account { get; set; }
    }
}
