using System;

namespace CSharpLearning.Examples.CommonPitfalls
{
    // Simple approach
    public class SimpleCalculator
    {
        public decimal Add(decimal a, decimal b)
        {
            return a + b;
        }

        public decimal Subtract(decimal a, decimal b)
        {
            return a - b;
        }
    }

    // Over-engineered approach
    public interface IOperationStrategy
    {
        decimal Execute(decimal a, decimal b);
    }

    public class AdditionStrategy : IOperationStrategy
    {
        public decimal Execute(decimal a, decimal b)
        {
            return a + b;
        }
    }

    public class SubtractionStrategy : IOperationStrategy
    {
        public decimal Execute(decimal a, decimal b)
        {
            return a - b;
        }
    }

    public class CalculationContext
    {
        private readonly IOperationStrategy _strategy;

        public CalculationContext(IOperationStrategy strategy)
        {
            _strategy = strategy;
        }

        public decimal ExecuteStrategy(decimal a, decimal b)
        {
            return _strategy.Execute(a, b);
        }
    }
}