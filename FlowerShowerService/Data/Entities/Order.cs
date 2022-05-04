namespace FlowerShowerService.Data.Entities
{
    public class Order
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public float Total { get; set; }
        public bool Completed { get; set; }
    }
}
