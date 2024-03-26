namespace Ponito.AnyParser
{
    public struct Result<T>
    {
        public bool  hasPassed  { get; }
        public T     value     { get; }
        public Input remainder { get; }
    
        private Result(bool hasPassed, T value, Input remainder)
        {
            this.hasPassed  = hasPassed;
            this.value     = value;
            this.remainder = remainder;
        }

        public static Result<T> Passed(T value, Input remainder)
        {
            return new Result<T>(true, value, remainder);
        }
    
        public static Result<T> Fail(Input remainder)
        {
            return new Result<T>(false, default, remainder);
        }
    
        public static implicit operator bool(Result<T> result)
        {
            return result.hasPassed;
        }
    }
}