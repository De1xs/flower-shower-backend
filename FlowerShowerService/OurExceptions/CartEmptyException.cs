namespace FlowerShowerService.OurExceptions;

public class CartEmptyException : Exception
{
    public CartEmptyException()
    {
    }

    public CartEmptyException(string message)
        : base (message)
    {
    }

    public CartEmptyException(string message, Exception inner)
        : base(message, inner)
    {
    }
}
