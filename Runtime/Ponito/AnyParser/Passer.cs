namespace Ponito.AnyParser
{
    public delegate U Passer<in T, out U>(T value);
}