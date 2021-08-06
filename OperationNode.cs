using System;

namespace Flow
{
    public abstract class OperationNode
    {
        public abstract object Operate(object value);
        public OperationNode InnerOperation { get; set; }
    }
    public class OperationNode<T, R> : OperationNode
    {
        public Func<T, R> Operation { get; set;}
        public override object Operate(object value)
        {
            if (Operation == null)
                return null;
            var obj = this.Operation((T)value);
            if (InnerOperation != null)
                return InnerOperation.Operate(obj);
            else return obj;
        }
    }
}