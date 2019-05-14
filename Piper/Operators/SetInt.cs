namespace Piper.Operators
{
    public class SetInt : IOperator<int>
    {
        private readonly string _bucket;
        private readonly int _value;

        public SetInt(string bucket, int value)
        {
            _bucket = bucket;
            _value = value;
        }

        public Buckets<int> Run(Buckets<int> buckets)
        {
            return buckets.Set(_bucket, _value);
        }
    }
}