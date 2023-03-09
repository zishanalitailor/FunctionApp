using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WalletFunctionApp.Domain.Models
{
    public class TransactionModel
    {
        public int Id { get; set; }
        public decimal Amount { get; set; }
        public string Direction { get; set; }
        public int AccountID { get; set; }
        public DateTime TimeStamp { get; set; } 
    }
}
