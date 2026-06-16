using Google.Protobuf.Collections;

namespace GrpcApp.Models;

public class Order
{
    public int Id { get; set; }
    public DateTime OrdrerDate { get; set; }
    public List<Product> Products { get; set; }
}
