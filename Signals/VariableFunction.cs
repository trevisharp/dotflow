namespace Flow.Signals
{
    public class VariableFunction : Function
    {
        public override double this[double t] => t;
        public override Function Derive() => 1.0;
    }
}