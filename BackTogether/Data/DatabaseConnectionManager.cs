using Azure.Identity;
using Azure.Security.KeyVault.Secrets;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.Data.SqlClient;
using System;
using System.Data.SqlClient;

namespace BackTogether.Data {
    public class DatabaseConnectionManager : IDisposable {

        private static string _keyVaultName;
        private static string _tenantId;
        private static string _secretName;
        private static string _password;
        private static string _connectionString;

        private static SecretClient client;
        private static SqlConnection connection;

        public DatabaseConnectionManager() {
            var config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.Development.json")
                .Build();

            _keyVaultName = config["KeyVaultConfig:KeyVaultURI"];
            _tenantId = config["KeyVaultConfig:TenantId"];
            _secretName = config["KeyVaultConfig:SecretName"];
        }

        public async Task<string> GetConnectionStringAsync() {
            client = new SecretClient(new Uri($"https://{_keyVaultName}.vault.azure.net"), new DefaultAzureCredential());
            KeyVaultSecret secret = await client.GetSecretAsync(_secretName);
            _password = secret.Value;
            _connectionString = $"Server=tcp:back-together-server.database.windows.net,1433;Initial Catalog=BackTogetherDB;Persist Security Info=False;User ID=karseniou@athtech.gr;Password={_password};MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Authentication=Active Directory Password";
            return _connectionString;
            //return "";
		}

        public void Dispose() {
            connection?.Dispose();
        }
    }
}