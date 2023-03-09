using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using WalletFunctionApp.Service;
using WalletFunctionApp.Service.Data;

[assembly: FunctionsStartup(typeof(WalletFunctionApp.App.Startup))]
namespace WalletFunctionApp.App
{
    public class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            builder.Services.AddTransient<IWalletService, WalletService>();
            builder.Services.AddTransient<ITransactionDetailsService, TransactionDetailsService>();
            builder.Services.AddTransient<IDataLayer, DataLayer>();
        }
    }
}
    