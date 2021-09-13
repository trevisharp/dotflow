using System.Collections.Generic;

namespace Flow.Signals
{
    public class AssociatedFunction : Function
    {
        public List<Function> Functions { get; private set; } = new List<Function>();
        public IAssociation Association { get; set; }
        public override double this[double t] 
            => Association.Compute(Functions, t);
        public override Function Derive()
            => Association.Derivative(Functions);
    }
}