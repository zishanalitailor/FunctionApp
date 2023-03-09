using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WalletFunctionApp.Domain.Models.ResponseModel
{
    public class WalletResponseModel
    {
        public string AccountID { get; set; }
        public string AccountBalance { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
