namespace Flow.Signals
{
    public class AggregatedFunction : Function
    {
        public Function F { get; set; }
        public IAggregation Aggregation { get; set; }
        public override double this[double t] => Aggregation.Compute(F, t);
        public override Function Derive()
            => Aggregation.Derivative(F);
    }
}