namespace Flow.Signals
{
    public abstract class Function : Signal
    {
        public abstract Function Derive();
        public static implicit operator Function(double value)
            => new ConstanteFunction(value);

        public static Function operator +(Function f, Function g)
        {
            if (f is ConstanteFunction fc && g is ConstanteFunction gc)
                return fc + gc;
            return null;
        }
        public static Function operator -(Function f, Function g)
        {
            if (f is ConstanteFunction fc && g is ConstanteFunction gc)
                return fc - gc;
            return null;
        }
        public static Function operator *(Function f, Function g)
        {
            if (f is ConstanteFunction fc && g is ConstanteFunction gc)
                return fc * gc;
            return null;
        }
        public static Function operator /(Function f, Function g)
        {
            if (f is ConstanteFunction fc && g is ConstanteFunction gc)
                return fc / gc;
            return null;
        }

        public static Function operator +(Function f)
            => f;
        public static Function operator -(Function f)
            => -1.0 * f;
    }
}