using System;

namespace TreeViewSandbox.DupesFromArtemis
{
    public interface ISystemTime
    {
        DateTime Now { get; }
    }

    public class SystemTime : ISystemTime
    {
        public DateTime Now => DateTime.Now;
    }
}
