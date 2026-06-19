using Google.Protobuf.WellKnownTypes;
using Grpc.Core;

namespace GrpcApp.Services;

public class OrderService : Order.OrderBase
{
    private static readonly List<Models.Order> orders = new();
    public override Task<GetListReply> GetList(VoidRequest request, ServerCallContext context)
    {
        var reply = new GetListReply();
        reply.Orders.AddRange(orders.Select(ToOrderReply));

        return Task.FromResult(reply);
    }

    public override Task<GetListReply> GetFilteredList(FilterRequest request, ServerCallContext context)
    {
        var reply = new GetListReply();
        reply.Orders.AddRange(
            orders.Where(o => o.Products.Sum(p => p.Price) >= request.MinPrice && 
            o.OrdrerDate >= request.MinDate.ToDateTime()).Select(ToOrderReply));

        return Task.FromResult(reply);
    }

    public override Task<OrderReply> Get(GetRequest request, ServerCallContext context)
    {
        var id = request.Id;
        var order = orders.FirstOrDefault(o => o.Id == id);

        
        return Task.FromResult(ToOrderReply(order));
    }

    public override Task<OrderReply> Add(AddRequest request, ServerCallContext context)
    {
        var date = request.OrderDate.ToDateTime();

        var products = request.Products.Select(p => new Models.Product
        {
            Title = p.Title,
            Price = p.Price
        }
        ).ToList();

        var order = new Models.Order
        {
            Id = orders.Count() + 1,
            OrdrerDate = date,
            Products = products
        };

        orders.Add(order);

        return Task.FromResult(ToOrderReply(order));
    }

    public override Task<OrderReply> Update(UpdateRequest request, ServerCallContext context)
    {
        var id = request.Id;
        var order = orders.FirstOrDefault(o => o.Id == id);

        order.OrdrerDate = request.OrderDate.ToDateTime();
        order.Products = request.Products.Select(p => new Models.Product
        {
            Title = p.Title,
            Price = p.Price
        }).ToList();

        return Task.FromResult(ToOrderReply(order));
    }

    public override Task<DeleteReply> Delete(DeleteRequest request, ServerCallContext context)
    {
        var id = request.Id;
        var order = orders.FirstOrDefault(o => o.Id == id);

        if (order is null)
            return Task.FromResult(new DeleteReply { Result = false});

        orders.Remove(order);
        return Task.FromResult(new DeleteReply { Result = true });
    }

    private static OrderReply ToOrderReply(Models.Order order)
    {
        var reply = new OrderReply
        {
            Id = order.Id,
            OrderDate = Timestamp.FromDateTime(order.OrdrerDate),
        };
        reply.Products.AddRange(order.Products.Select(p => new Product
        {
            Price = p.Price,
            Title = p.Title
        }));

        return reply;
    }
}
