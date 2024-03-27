namespace Ponito.AnyParser
{
    public delegate Result<U> CastParser<T, U>(T value);
}