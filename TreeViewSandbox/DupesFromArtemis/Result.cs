namespace TreeViewSandbox.DupesFromArtemis
{
    public class Result : IResult
    {
        public string Message { get; set; }

        public bool Success { get; set; }
    }

    public class Result<T> : IResult<T>
    {
        public string Message { get; set; }

        public bool Success { get; set; }

        public T Outcome { get; internal set; }
    }
}
