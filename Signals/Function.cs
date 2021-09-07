namespace Flow.Signals
{
    public abstract class Function : Signal
    {
        public abstract Function Derive();

        public static implicit operator Function(double value)
            => new ConstanteFunction(value);
    }
}