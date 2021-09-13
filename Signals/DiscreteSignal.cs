namespace Flow.Signals
{
    //TODO
    public class DiscreteSignal : Signal
    {
        internal double[] signal = null;
        internal double rate;
        internal DiscreteSignal(double[] sig, double rate = 1e-4)
        {
            this.signal = sig;
            this.rate = rate;
        }
        public override double this[double t]
        {
            get
            {
                int i = (int)(t / rate);
                if (i < 0 || i > signal.Length)
                    return 0.0;
                return signal[i];
            }
        }
    }
}