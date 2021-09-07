namespace Flow.Signals
{
    public class ConstanteFunction : Function
    {
        internal ConstanteFunction(double value)
            => constant = value;
        private double constant = 0.0;
        public override double this[double t] 
            => constant;
        public override Function Derive() => 0.0;
    }
}