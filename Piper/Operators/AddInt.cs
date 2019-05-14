namespace Piper.Operators
{
    public class AddInt : IOperator<int>
    {
        private readonly string _leftBucket;
        private readonly string _rightBucket;
        private readonly string _outBucket;

        public AddInt(string leftBucket, string rightBucket, string outBucket)
        {
            _leftBucket = leftBucket;
            _rightBucket = rightBucket;
            _outBucket = outBucket;
        }

        public Buckets<int> Run(Buckets<int> buckets)
        {
            var leftValue = buckets.Get(_leftBucket);
            var rightValue = buckets.Get(_rightBucket);

            return buckets.Set(_outBucket, leftValue + rightValue);
        }
    }
}