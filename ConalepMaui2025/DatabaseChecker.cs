using System;
using System.Threading.Tasks;
using MySqlConnector;

namespace ConalepMaui2025.Services
{
    public class DatabaseChecker
    {
        private readonly string _connectionString;

        public DatabaseChecker(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<bool> VerificarConexionAsync()
        {
            try
            {
                using var connection = new MySqlConnection(_connectionString);
                await connection.OpenAsync();
                return connection.State == System.Data.ConnectionState.Open;
            }
            catch (Exception ex)
            {
                // Aquí puedes loguear el error si quieres
                Console.WriteLine($"Error al conectar: {ex.Message}");
                return false;
            }
        }
    }
}
