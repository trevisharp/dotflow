namespace Flow.Signals
{
    public interface IAggregation
    {
        Function Derivative(Function inner);
        double Compute(Function inner, double t);
    }
}