namespace AutoRenovatio;

public interface IParser<out T> where T : IUpdate, new()
{
    public T? Parse(string content);
}