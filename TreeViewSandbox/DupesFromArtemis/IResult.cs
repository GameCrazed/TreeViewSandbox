namespace TreeViewSandbox.DupesFromArtemis
{
    public interface IResult
    {
        bool Success { get; set; }
        string Message { get; set; }
    }

    public interface IResult<T> : IResult
    {
        T Outcome { get; }
    }
}
