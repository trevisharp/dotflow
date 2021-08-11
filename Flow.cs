using System;

namespace Flow
{
    public interface ChildrenFlow<P>
    {
        P Parent { get; }
    }

    public abstract class Flow
    {
        public event Action<dynamic> OnSet;
        public virtual void Set(dynamic value)
        {
            if (OnSet != null)
                OnSet(value);
        }

        public static Flow<T> New<T>(T initial)
        {
            Flow<T> flow = new Flow<T>();
            flow.Set(initial);
            return flow;
        }

        public static Flow<T, I> From<T, I>(Flow<I> flow, Func<I, T> entry)
            => new Flow<T, I>(flow, entry);

        public static Flow<T, I, P> From<T, I, O, P>(Flow<I, O, P> flow, Func<I, T> entry)
            => new Flow<T, I, P>(flow, entry, flow.Parent);
        
        public static Flow<T, T, P> WithParent<T, P>(Flow<T> flow, P parent)
            => new Flow<T, T, P>(flow, x => x, parent);
    }

    public class Flow<T> : Flow
    {
        protected T state = default(T);
        public T State => this.state;
        public override void Set(dynamic value)
        {
            this.state = value;
            base.Set(this.state);
        }
    }

    public class Flow<T, I> : Flow<T>
    {
        public Flow(Flow<I> source, Func<I, T> entryoperation)
            => source.OnSet += x => Set(entryoperation(x));
    }

    public class Flow<T, I, P> : Flow<T, I>, ChildrenFlow<P>
    {
        protected P parent;

        public P Parent => this.parent;

        public Flow(Flow<I> source, Func<I, T> entryoperation, P parent) 
            : base(source, entryoperation) => this.parent = parent;
    }

    public class Flow<T, A, B, P> : Flow<T>, ChildrenFlow<P>
    {
        protected P parent;
        public P Parent => this.parent;

        public Flow(Flow<A> sourceA, Flow<B> sourceB, Func<A, B, T> entryoperation)
        {
            bool hasA = false,
                 hasB = false;
            A a = default(A);
            B b = default(B);
            if (sourceA is ChildrenFlow<P> cf)
                this.parent = cf.Parent;
            sourceA.OnSet += x =>
            {
                hasA = true;
                a = x;
                if (hasA && hasB)
                    Set(entryoperation(a, b));
            };
            sourceB.OnSet += x =>
            {
                hasB = true;
                b = x;
                if (hasA && hasB)
                    Set(entryoperation(a, b));
            };
        }
    }
}