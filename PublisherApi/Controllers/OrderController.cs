using MassTransit;
using Messages.Abstractions.Entities;
using Messages.Abstractions.Messages;
using Messages.Abstractions.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace PublisherApi.Controllers;
[Route("api/[controller]")]
[ApiController]
public class OrderController : ControllerBase
{
    private readonly IPublishEndpoint _publishEndpoint;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IOrderRepository _orderRepository;

    public OrderController(IPublishEndpoint publishEndpoint, 
        IUnitOfWork unitOfWork, IOrderRepository orderRepository)
    {
        _publishEndpoint = publishEndpoint;
        _unitOfWork = unitOfWork;
        _orderRepository = orderRepository;
    }

    [HttpPost()]
    public async Task<ActionResult<Response>> CreateOrder(
        [FromBody] Request body,
        CancellationToken cancellationToken)
    {
        var order = new Order
        {
            Id = Guid.NewGuid(),
            Name = body.Name
        };

        await _unitOfWork.BeginTransactionAsync(cancellationToken);
        await _orderRepository.AddAsync(order, cancellationToken);
        await _publishEndpoint.Publish<OrderPublished>(new
        {
            order.Id,
            order.Name
        }, cancellationToken);
        await _unitOfWork.CommitTransactionAsync();
        
        return Ok(order);
    }
}

public class Request
{
    public string Name { get; set; } = string.Empty;
}

public class Response
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
}
