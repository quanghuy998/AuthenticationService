using System.Linq.Expressions;

namespace AuthenticationService.Domain.SeedWork
{
    public class BaseSpecification<T> : IBaseSpecification<T>
    {
        public Expression<Func<T, bool>> Expression { get; set; }
        public List<Expression<Func<T, object>>> Includes { get; }
        public List<string> IncludeStrings { get; }

        public BaseSpecification()
        {
            Includes = new();
            IncludeStrings = new();
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
