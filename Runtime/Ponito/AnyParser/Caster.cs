namespace Ponito.AnyParser
{
    public delegate U Caster<in T, out U>(T value);
}