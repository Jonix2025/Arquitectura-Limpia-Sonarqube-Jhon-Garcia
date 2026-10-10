using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;
using Domain.Entities;
using Domain.Interfaces;

namespace Infrastructure.Data
{
    public class OrderRepository : IOrderRepository
    {
        private readonly string connectionString;

        public OrderRepository(IConfiguration configuration)
        {
            connectionString = configuration.GetConnectionString("DefaultConnection")?? "Server=localhost;Database=master;User Id=sa;Password=SuperSecret123!;TrustServerCertificate=True";
        }

        public async Task SaveOrderAsync(Order order)
        {
            const string sql = "INSERT INTO Orders (Id, Customer, Product, Qty, Price) VALUES (@Id, @Customer, @Product, @Qty, @Price)";

            using var conn = new SqlConnection(connectionString);
            using var cmd = new SqlCommand(sql, conn);

            cmd.Parameters.AddWithValue("@Id", order.Id);
            cmd.Parameters.AddWithValue("@Customer", order.CustomerName);
            cmd.Parameters.AddWithValue("@Product", order.ProductName);
            cmd.Parameters.AddWithValue("@Qty", order.Quantity);
            cmd.Parameters.AddWithValue("@Price", order.UnitPrice);

            await conn.OpenAsync();
            await cmd.ExecuteNonQueryAsync();
        }
    }
}