namespace TreeViewSandbox.DupesFromArtemis
{
    public class PureConnectionString
    {
        private readonly string _value;

        public PureConnectionString(string value)
        {
            _value = value;
        }

        public static implicit operator string(PureConnectionString instance)
        {
            return instance._value;
        }
    }
}
