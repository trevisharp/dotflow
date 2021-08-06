using System;

namespace Flow
{
    public class IterableFlow<T>
    {
        public object State { get; set; } = null;
        public IterableFlow() { }

        private OperationNode operation = null;

        public void AddOperation(Func<object, object> op)
        {
            if (operation == null)
            {
                operation = new OperationNode()
                {
                    Operation = op
                };
            }
            else
            {
                var crr = operation;
                while (crr.InnerOperation != null)
                    crr = crr.InnerOperation;
                crr.InnerOperation = new OperationNode()
                {
                    Operation = op
                };
            }
        }
        
        public void Iterate()
        {
            if (operation == null)
                return;
            this.State = operation.Operate(this.State);
        }

        public void SetState(T state)
        {
            this.State = state;
            Iterate();
        }
    }

    public class IterableFlow<T, R>
    {
        public object State { get; set; } = null;
        public R Return { get; set; }
        public IterableFlow(R flowreturn)
        {
            this.Return = flowreturn;
        }
        
        private OperationNode operation = null;
        
        public void AddOperation(Func<object, object> op)
        {
            if (operation == null)
            {
                operation = new OperationNode()
                {
                    Operation = op
                };
            }
            else
            {
                var crr = operation;
                while (crr.InnerOperation != null)
                    crr = crr.InnerOperation;
                crr.InnerOperation = new OperationNode()
                {
                    Operation = op
                };
            }
        }
        
        public void Iterate()
        {
            if (operation == null)
                return;
            this.State = operation.Operate(this.State);
        }

        public void SetState(T state)
        {
            this.State = state;
            Iterate();
        }
    }
}