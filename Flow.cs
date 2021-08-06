using System;
using System.Collections;
using System.Collections.Generic;

namespace Flow
{
    public abstract class Flow
    {
        
    }

    public class Flow<T> : Flow
    {
        public T State { get; set; } = default(T);

        public Flow(T obj)
            => this.State = obj;
        
        public Flow<U, Flow<T>> Open<U>(U state)
            => new Flow<U, Flow<T>>(state, this);
        
        public static implicit operator T(Flow<T> flow)
            => flow.State;
    }

    public class Flow<T, R> : Flow<T>
    {
        public R Return { get; set; } = default(R);

        public Flow(T state, R flowreturn) : base(state)
            => this.Return = flowreturn;
        
        public R Zip(Func<T, R, R> func)
            => func(this.State, this.Return);
        
        public R Zip(Action<T, R> func)
        {
            func(this.State, this.Return);
            return this.Return;
        }
    }
}