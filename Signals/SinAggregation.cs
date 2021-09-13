using System;

namespace Flow.Signals
{
    using static Functions;
    public class SinAggregation : IAggregation
    {
        public double Compute(Function inner, double t)
            => Math.Sin(inner[t]);

        public Function Derivative(Function inner)
            => cos(inner) * d(inner);
    }
}