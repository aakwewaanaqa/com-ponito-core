namespace Ponito.AnyParser
{
    public readonly struct Input
    {
        public string source { get; }

        public int Length => string.IsNullOrEmpty(source) ? 0 : source.Length;

        public Result<char> Match(char c)
        {
            return Length    == 0 ? Result<char>.Fail(string.Empty) :
                   source[0] == c ? Result<char>.Passed(c, source[1..]) :
                                    Result<char>.Fail(source);
        }
        
        public Input(string source)
        {
            this.source = source;
        }

        public static implicit operator Input(string str)
        {
            return new Input(str);
        }
    }
}