using System;

namespace Flow.Signals
{
    public abstract class Function : Signal
    {
        public abstract Function Derive();
        public static implicit operator Function(double value)
            => new ConstanteFunction(value);
        public static implicit operator Function(Func<double, double> f)
            => new NumericFunction(f);
        public static Function operator +(Function f, Function g)
        {
            if (f is ConstanteFunction fc && g is ConstanteFunction gc)
                return fc + gc;
            else if (f is NumericFunction fn && g is NumericFunction gn)
                return fn + gn;
            else if (f is PolynomialFunction fp && g is PolynomialFunction gp)
                return fp + gp;
            return null;
        }
        public static Function operator -(Function f, Function g)
        {
            if (f is ConstanteFunction fc && g is ConstanteFunction gc)
                return fc - gc;
            else if (f is NumericFunction fn && g is NumericFunction gn)
                return fn - gn;
            else if (f is PolynomialFunction fp && g is PolynomialFunction gp)
                return fp - gp;
            return null;
        }
        public static Function operator *(Function f, Function g)
        {
            if (f is ConstanteFunction fc && g is ConstanteFunction gc)
                return fc * gc;
            else if (f is NumericFunction fn && g is NumericFunction gn)
                return fn * gn;
            else if (f is PolynomialFunction fp && g is PolynomialFunction gp)
                return fp * gp;
            return null;
        }
        public static Function operator /(Function f, Function g)
        {
            if (f is ConstanteFunction fc && g is ConstanteFunction gc)
                return fc / gc;
            else if (f is NumericFunction fn && g is NumericFunction gn)
                return fn / gn;
            else if (f is PolynomialFunction fp && g is PolynomialFunction gp)
                return fp / gp;
            return null;
        }
        public static Function operator +(Function f)
            => f;
        public static Function operator -(Function f)
            => -1.0 * f;
    }
}