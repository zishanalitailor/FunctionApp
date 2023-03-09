using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using WalletFunctionApp.Domain.Models.RequestModel;
using WalletFunctionApp.Domain.Models.ResponseModel;

namespace WalletFunctionApp.Service.Data
{
    public class DataLayer : IDataLayer
    {
        //private readonly string connetionString = "Data Source=localhost;Initial Catalog=WalletFunctionApp;Integrated Security=SSPI;";
        private readonly string connetionString = "Server=tcp:wallet-test-server.database.windows.net,1433;Initial Catalog = FunctionApp; Persist Security Info=False;User ID = sa123; Password=test@123; MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout = 30;";
        public void AddTransaction(TransactionRequestModel transactionRequestModel)
        {
            SqlCommand command;
            string sql = "AddTransactionUpdateWalletgivenAccount_sp";
            using (SqlConnection connection = new SqlConnection(connetionString))
            {
                connection.Open();
                command = new SqlCommand(sql, connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@Id", transactionRequestModel.Id);
                command.Parameters.AddWithValue("@Amount", transactionRequestModel.Amount);
                command.Parameters.AddWithValue("@Direction", transactionRequestModel.Direction);
                command.Parameters.AddWithValue("@AccountID", transactionRequestModel.Account);
                command.ExecuteNonQuery();
                connection.Close();
            }
        }

        public decimal GetWalletBalance(int accountId)
        {
            SqlCommand command;
            string record = string.Empty;
            decimal accountBalance = 0;
            string sql = "SELECT AccountBalance FROM [dbo].[Wallet] WHERE AccountID = @AccountID";

            using (SqlConnection connection = new SqlConnection(connetionString))
            {
                connection.Open();
                command = new SqlCommand(sql, connection);
                command.Parameters.AddWithValue("@AccountID", accountId);
                record = command.ExecuteScalar() == null ? string.Empty : command.ExecuteScalar().ToString();
                connection.Close();
            }
            decimal.TryParse(record, out accountBalance);
            return accountBalance;
        }

        public WalletResponseModel GetWalletDetailsByAccountId(int accountId)
        {
            SqlCommand command;
            string sql = "SELECT * FROM [dbo].[Wallet] WHERE AccountID = @AccountID";
            WalletResponseModel walletResponseModel = new WalletResponseModel();
            using (SqlConnection connection = new SqlConnection(connetionString))
            {
                connection.Open();
                command = new SqlCommand(sql, connection);
                command.Parameters.AddWithValue("@AccountID", accountId);
                SqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    reader.Read();
                    //Here is the mapping code that maps our ClientEntity class to our SqlDataReader
                    walletResponseModel.AccountID = reader["AccountID"].ToString();
                    walletResponseModel.AccountBalance = reader["AccountBalance"].ToString();
                    walletResponseModel.FirstName = reader["FirstName"].ToString();
                    walletResponseModel.LastName = reader["LastName"].ToString();
                }
                connection.Close();
            }

            return walletResponseModel;
        }
    }
}
