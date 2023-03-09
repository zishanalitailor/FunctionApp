using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WalletFunctionApp.Domain.Models
{
    public class WalletModel
    {
        public int AccountID { get; set; }
        public decimal AccountBalance { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
