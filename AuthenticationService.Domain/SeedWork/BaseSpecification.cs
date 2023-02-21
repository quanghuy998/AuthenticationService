using System.Linq.Expressions;

namespace AuthenticationService.Domain.SeedWork
{
    public class BaseSpecification<T> : IBaseSpecification<T>
    {
        public Expression<Func<T, bool>> Expression { get; set; }
        public List<Expression<Func<T, object>>> Includes { get; }
        public List<string> IncludeStrings { get; }

        public BaseSpecification(Expression<Func<T, bool>> expression)
        {
            Includes = new();
            IncludeStrings = new();
            Expression = expression;
        }

        protected virtual void AddInclude(Expression<Func<T, object>> includeExpression)
        {
            Includes.Add(includeExpression);
        }

        protected virtual void AddInclude(string includeString)
        {
            IncludeStrings.Add(includeString);
        }
    }
}
