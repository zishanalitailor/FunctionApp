using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using WalletFunctionApp.Domain.Models.RequestModel;
using WalletFunctionApp.Service;
using System.Runtime.CompilerServices;
using WalletFunctionApp.Domain.Models.ResponseModel;

namespace WalletFunctionApp.App
{
    public class WalletFunctionApp
    {
        private readonly IWalletService _walletService;
        private readonly ITransactionDetailsService _transactionDetailsService;
        public WalletFunctionApp(IWalletService walletService, ITransactionDetailsService transactionDetailsService)
        {
            this._walletService = walletService;
            this._transactionDetailsService = transactionDetailsService;
        }

        [FunctionName("GetAccount")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)] HttpRequest req,
            ILogger log)
        {

            string responseMessage = string.Empty;
            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            var transactionObj = JsonConvert.DeserializeObject<TransactionRequestModel>(requestBody);
            
            var walletBalance = this._walletService.GetWalletBalance(transactionObj.Account);
            if (walletBalance > transactionObj.Amount)
            {
                if (this._transactionDetailsService.AddTransaction(transactionObj))
                {
                    WalletResponseModel walletResponseModel = this._walletService.GetWalletDetailsByAccountId(transactionObj.Account);
                    responseMessage = JsonConvert.SerializeObject(walletResponseModel);
                }
                else
                { responseMessage = "Something went wrong, please try again later."; }
            }
            else { responseMessage = "Not enough balance in wallet to perform this transaction."; } 

            return new OkObjectResult(responseMessage);
        }
    }
}
