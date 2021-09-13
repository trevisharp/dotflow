using System;
using System.Linq;
using System.Collections.Generic;

namespace Flow.Signals
{
    public class PolynomialFunction : Function
    {
        internal PolynomialFunction(List<(double coef, double pow)> value = null)
            => data = (value ?? new List<(double coef, double pow)>())
                .OrderBy(x => x.pow).ToList();
        internal List<(double coef, double pow)> data;
        public override double this[double t]
        {
            get
            {
                double sum = 0;
                foreach (var mono in data)
                    sum += mono.coef * Math.Pow(t, mono.pow);
                return sum;
            }
        }
        public override Function Derive()
        {
            List<(double coef, double pow)> newdata = new List<(double coef, double pow)>();
            foreach ((double coef, double pow) in data)
            {
                if (coef == 0)
                    continue;
                newdata.Add((coef * pow, pow - 1.0));
            }
            return new PolynomialFunction(newdata);
        }
        public static PolynomialFunction operator +(PolynomialFunction f, PolynomialFunction g)
        {
            List<(double coef, double pow)> data = new List<(double coef, double pow)>();
            var it = f.data.GetEnumerator();
            var jt = g.data.GetEnumerator();
            bool ihasnext = it.MoveNext(),
                 jhasnext = jt.MoveNext();
            while (ihasnext && jhasnext)
            {
                if (it.Current.pow == jt.Current.pow)
                {
                    data.Add((it.Current.coef + jt.Current.coef, it.Current.pow));
                    ihasnext = it.MoveNext();
                    jhasnext = jt.MoveNext();
                }
                else if (it.Current.pow > jt.Current.pow)
                {
                    data.Add((jt.Current.coef, jt.Current.pow));
                    jhasnext = jt.MoveNext();
                }
                else
                {
                    data.Add((it.Current.coef, it.Current.pow));
                    ihasnext = it.MoveNext();
                }
            }
            while (ihasnext)
            {
                data.Add((it.Current.coef, it.Current.pow));
                ihasnext = it.MoveNext();
            }
            while (jhasnext)
            {
                data.Add((jt.Current.coef, jt.Current.pow));
                jhasnext = jt.MoveNext();
            }
            return new PolynomialFunction(data);
        }
        public static PolynomialFunction operator -(PolynomialFunction f, PolynomialFunction g)
        {
            List<(double coef, double pow)> data = new List<(double coef, double pow)>();
            var it = f.data.GetEnumerator();
            var jt = g.data.GetEnumerator();
            bool ihasnext = it.MoveNext(),
                 jhasnext = jt.MoveNext();
            while (ihasnext && jhasnext)
            {
                if (it.Current.pow == jt.Current.pow)
                {
                    data.Add((it.Current.coef - jt.Current.coef, it.Current.pow));
                    ihasnext = it.MoveNext();
                    jhasnext = jt.MoveNext();
                }
                else if (it.Current.pow > jt.Current.pow)
                {
                    data.Add((-jt.Current.coef, jt.Current.pow));
                    jhasnext = jt.MoveNext();
                }
                else
                {
                    data.Add((it.Current.coef, it.Current.pow));
                    ihasnext = it.MoveNext();
                }
            }
            while (ihasnext)
            {
                data.Add((it.Current.coef, it.Current.pow));
                ihasnext = it.MoveNext();
            }
            while (jhasnext)
            {
                data.Add((-jt.Current.coef, jt.Current.pow));
                jhasnext = jt.MoveNext();
            }
            return new PolynomialFunction(data);
        }
    }
}