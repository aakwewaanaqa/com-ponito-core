namespace Ponito.Core.Asyncs.Promises
{
    public class Promise
    {
        public float        Progress { get; set; }
        public PromiseState State    { get; set; }
        public object       Error    { get; set; }
    }
}