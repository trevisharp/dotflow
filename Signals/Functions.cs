namespace Flow.Signals
{
    public static class Functions
    {
        public static VariableFunction x { get; set; } = new VariableFunction();
        public static VariableFunction t { get; set; } = new VariableFunction();
        public static Function d(Function f)
            => f.Derive();
        
        public static Function sin(Function f)
        {
            AggregatedFunction af = new AggregatedFunction();
            af.F = f;
            af.Aggregation = new SinAggregation();
            return af;
        }

        public static Function cos(Function f)
        {
            AggregatedFunction af = new AggregatedFunction();
            af.F = f;
            af.Aggregation = new CosAggregation();
            return af;
        }
    }
}