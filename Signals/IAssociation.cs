using System.Collections.Generic;

namespace Flow.Signals
{
    public interface IAssociation
    {
        Function Derivative(List<Function> funcs);
        double Compute(List<Function> funcs, double t);
    }
}