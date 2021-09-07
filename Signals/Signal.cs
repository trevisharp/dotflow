namespace Flow.Signals
{
    public abstract class Signal
    {
        public abstract double this[double t] { get; }
    }
}