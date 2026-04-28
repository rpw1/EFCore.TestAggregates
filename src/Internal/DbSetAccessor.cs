using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace EFCore.TestAggregates.Internal;

internal sealed record DbSetAccessor<TDbContext, TEntity>(Expression<Func<TDbContext, DbSet<TEntity>>> Accessor)
    where TEntity : class
    where TDbContext : class;