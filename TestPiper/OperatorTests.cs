using NUnit.Framework;
using Piper;
using Piper.Operators;

namespace TestPiper
{
    public class OperatorTests
    {
        private Pipe<int> _pipe;

        [SetUp]
        public void Setup()
        {
            _pipe = new Pipe<int>();
        }

        [Test]
        public void TestSetInt()
        {
            _pipe.AddOperator(new SetInt("out", 5));

            Assert.AreEqual(5, _pipe.Run().Get("out"));
        }

        [Test]
        public void TestAddInt()
        {
            var buckets = new Buckets<int>()
                .Set("left", 5)
                .Set("right", 3);

            _pipe.AddOperator(new AddInt("left", "right", "out"));

            Assert.AreEqual(5 + 3, _pipe.Run(buckets).Get("out"));
        }

        [Test]
        public void TestSubtractInt()
        {
            var buckets = new Buckets<int>()
                .Set("left", 5)
                .Set("right", 3);

            _pipe.AddOperator(new SubtractInt("left", "right", "out"));

            Assert.AreEqual(5 - 3, _pipe.Run(buckets).Get("out"));
        }

        [Test]
        public void TestManyOperators()
        {
            _pipe
                .AddOperator(new SetInt("out", 0))
                .AddOperator(new SetInt("five", 5))
                .AddOperator(new SetInt("three", 3))
                .AddOperator(new SetInt("one", 1))
                .AddOperator(new AddInt("out", "five", "out"))
                .AddOperator(new AddInt("out", "three", "out"))
                .AddOperator(new AddInt("out", "one", "out"));

            Assert.AreEqual(
                0 + 5 + 3 + 1,
                _pipe.Run().Get("out")
            );
        }
    }
}