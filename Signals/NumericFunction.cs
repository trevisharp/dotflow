using System;

namespace Flow.Signals
{
    public class NumericFunction : Function
    {
        internal Func<double, double> func;
        internal NumericFunction(Func<double, double> _f)
            => this.func = _f;
        public override double this[double t] => func(t);
        public override Function Derive()
            => new Func<double, double>(t => (func(t + 1e-4) - func(t)) / 1e-4);
        public static NumericFunction operator +(NumericFunction f, NumericFunction g)
            => new NumericFunction(t => f.func(t) + g.func(t));
        public static NumericFunction operator -(NumericFunction f, NumericFunction g)
            => new NumericFunction(t => f.func(t) - g.func(t));
        public static NumericFunction operator *(NumericFunction f, NumericFunction g)
            => new NumericFunction(t => f.func(t) * g.func(t));
        public static NumericFunction operator /(NumericFunction f, NumericFunction g)
            => new NumericFunction(t => f.func(t) / g.func(t));

    }
}