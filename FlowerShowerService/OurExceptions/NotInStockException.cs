namespace FlowerShowerService.OurExceptions;

public class NotInStockException : Exception
{
    public NotInStockException()
    {
    }

    public NotInStockException(string message)
        : base (message)
    {
    }

    public NotInStockException(string message, Exception inner)
        : base(message, inner)
    {
    }
}
