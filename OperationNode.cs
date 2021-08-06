using System;

namespace Flow
{
    public class OperationNode
    {
        public Func<object, object> Operation { get; set;}
        public object Operate(object value)
        {
            if (Operation == null)
                return null;
            var obj = this.Operation(value);
            if (InnerOperation != null)
                return InnerOperation.Operate(obj);
            else return obj;
        }
        public OperationNode InnerOperation { get; set; }
    }
}