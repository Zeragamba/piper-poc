using System.Collections.Generic;
using System.Collections.Immutable;

namespace Piper
{
    public class Pipe<T> : IOperator<T>
    {
        private readonly List<IOperator<T>> _operators;

        public Pipe()
        {
            _operators = new List<IOperator<T>>();
        }

        public Buckets<T> Run(Buckets<T> buckets)
        {
            foreach (var op in _operators)
            {
                buckets = op.Run(buckets);
            }
            return buckets;
        }

        public Pipe<T> AddOperator(IOperator<T> op)
        {
            _operators.Add(op);
            return this;
        }

        public Buckets<T> Run()
        {
            return Run(new Buckets<T>());
        }
    }

    public class Buckets<T>
    {
        private readonly ImmutableDictionary<string, T> _bucketList;

        public Buckets()
        {
            _bucketList = ImmutableDictionary.Create<string, T>();
        }

        public Buckets(ImmutableDictionary<string, T> bucketList)
        {
            _bucketList = bucketList;
        }

        public T Get(string bucketName)
        {
            return _bucketList[bucketName];
        }

        public Buckets<T> Set(string bucketName, T value)
        {
            return new Buckets<T>(_bucketList.SetItem(bucketName, value));
        }
    }

    public interface IOperator<T>
    {
        Buckets<T> Run(Buckets<T> buckets);
    }
}