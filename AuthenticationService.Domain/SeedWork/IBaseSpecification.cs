using System.Linq.Expressions;

namespace AuthenticationService.Domain.SeedWork
{
    public interface IBaseSpecification<T>
    {
        Expression<Func<T, bool>> Expression { get; }
        List<Expression<Func<T, object>>> Includes { get; }
        List<string> IncludeStrings { get; }
    }
}
