namespace InventoryMgtApp.DAL.Exceptions;

public class UnauthorizedAccessException : Exception
{
    public UnauthorizedAccessException(string message) : base(message)
    {
    }
}
