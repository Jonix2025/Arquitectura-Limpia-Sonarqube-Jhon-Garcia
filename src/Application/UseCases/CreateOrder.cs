using System.Threading.Tasks;
using Domain.Entities;
using Domain.Interfaces;
using Domain.Services;

namespace Application.UseCases;

public class CreateOrderUseCase
{
    private readonly IOrderRepository _orderRepository;

   
    public CreateOrderUseCase(IOrderRepository orderRepository)
    {
        _orderRepository = orderRepository;
    }

    public async Task<Order> ExecuteAsync(string customer, string product, int qty, decimal price)
    {
        
        var order = OrderService.CreateTerribleOrder(customer, product, qty, price);

        await _orderRepository.SaveOrderAsync(order);

        return order;
    }
}