using System;

namespace Flow.Signals
{
    using static Functions;
    public class CosAggregation : IAggregation
    {
        public double Compute(Function inner, double t)
            => Math.Cos(inner[t]);

        public Function Derivative(Function inner)
            => -sin(inner) * d(inner);
    }
}