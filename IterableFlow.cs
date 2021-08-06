using System;

namespace Flow
{
    public class IterableFlow<T> : Flow<T>
    {
        public IterableFlow() : base(default(T)) { }

        private OperationNode operation = null;

        public void AddOperation(Func<T, T> op)
        {
            if (operation == null)
            {
                operation = new OperationNode<T, T>()
                {
                    Operation = op
                };
            }
            else
            {
                var crr = operation;
                while (crr.InnerOperation != null)
                    crr = crr.InnerOperation;
                crr.InnerOperation = new OperationNode<T, T>()
                {
                    Operation = op
                };
            }
        }
        
        public void Iterate()
        {
            if (operation == null)
                return;
            this.State = (T)operation.Operate(this.State);
        }

        public void SetState(T state)
        {
            this.State = state;
            Iterate();
        }
    }

    public class IterableFlow<T, R> : Flow<T, R>
    {
        public IterableFlow(R flowreturn) : base(default(T), flowreturn) { }
        
        private OperationNode operation = null;
        
        public void AddOperation<P, U>(Func<P, U> op)
        {
            if (operation == null)
            {
                operation = new OperationNode<P, U>()
                {
                    Operation = op
                };
            }
            else
            {
                var crr = operation;
                while (crr.InnerOperation != null)
                    crr = crr.InnerOperation;
                crr.InnerOperation = new OperationNode<P, U>()
                {
                    Operation = op
                };
            }
        }
        
        public void Iterate()
        {
            if (operation == null)
                return;
            this.State = (T)operation.Operate(this.State);
        }

        public void SetState(T state)
        {
            this.State = state;
            Iterate();
        }

        public new R Zip(Action<T, R> func)
        {
            AddOperation<T, T>(s => 
            {
                func(s, this.Return);
                return s;
            });
            return this.Return;
        }
    }
}