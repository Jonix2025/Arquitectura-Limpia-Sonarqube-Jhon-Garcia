using Domain.Entities;
using System.Threading.Tasks;

namespace Domain.Interfaces;

public interface IOrderRepository
{

    Task SaveOrderAsync(Order order);
}
