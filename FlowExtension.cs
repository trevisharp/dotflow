using System;

namespace Flow
{
    public static class FlowExtension
    {
        public static Flow<T, A, B, P> Zip<T, A, I1, B, I2, P>(
            this Flow<B, I1, Flow<A, I2, P>> flow, 
            Func<A, B, T> zipfunc)
        {
            Flow<T, A, B, P> zip = new Flow<T, A, B, P>(flow.Parent, flow, zipfunc);
            return zip;
        }
        public static Flow<T, A, B, P> Zip<T, A, I1, B, I2, P>(
            this Flow<B, I1, Flow<A, I2>> flow, 
            Func<A, B, T> zipfunc)
        {
            Flow<T, A, B, P> zip = new Flow<T, A, B, P>(flow.Parent, flow, zipfunc);
            return zip;
        }
    }
}