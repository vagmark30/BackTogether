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
                .AddJsonFile("appsettings.json")
                .Build();

            _keyVaultName = config["KeyVaultConfig:KeyVaultURI"];
            _tenantId = config["KeyVaultConfig:TenantId"];
            _secretName = config["KeyVaultConfig:SecretName"];
        }

        public async Task OpenConnection() {

            client = new SecretClient(new Uri($"https://%7B_keyvaultname%7D.vault.azure.net/%22"), new DefaultAzureCredential());
            KeyVaultSecret secret = await client.GetSecretAsync(_secretName);
            _password = secret.Value;

            _connectionString = $"Server=tcp:back-together-server.database.windows.net,1433;Initial Catalog=BackTogetherDB;Persist Security Info=False;User ID=karseniou@athtech.gr;Password={_password};MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Authentication="Active Directory Password";";

            connection = new SqlConnection(_connectionString);
            connection.Open();
        }

        public void Dispose() {
            connection?.Dispose();
        }
    }
}