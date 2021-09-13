namespace Flow.Signals
{
    public class ConstanteFunction : Function
    {
        internal ConstanteFunction(double value) => constant = value;
        internal double constant = 0.0;
        public override double this[double t] => constant;
        public override Function Derive() => 0.0;
        public static ConstanteFunction operator +(ConstanteFunction c, ConstanteFunction k)
            => new ConstanteFunction(c.constant + k.constant);
        public static ConstanteFunction operator -(ConstanteFunction c, ConstanteFunction k)
            => new ConstanteFunction(c.constant - k.constant);
        public static ConstanteFunction operator *(ConstanteFunction c, ConstanteFunction k)
            => new ConstanteFunction(c.constant * k.constant);
        public static ConstanteFunction operator /(ConstanteFunction c, ConstanteFunction k)
            => new ConstanteFunction(c.constant / k.constant);
    }
}