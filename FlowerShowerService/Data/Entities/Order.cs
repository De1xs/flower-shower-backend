namespace FlowerShowerService.Data.Entities;

public class Order
{
    public int Id { get; set; }
    public bool Completed { get; set; }
    public DateTime OrderedOn { get; set; } = DateTime.UtcNow;
    public User User { get; set; } = new();
    public List<OrderItem> OrderItems { get; set; } = new();
}
