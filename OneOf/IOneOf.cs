namespace OneOf
{
    public interface IOneOf
    {
        object Value { get; }
        bool IsT<T>();
        T AsT<T>();
    }
}