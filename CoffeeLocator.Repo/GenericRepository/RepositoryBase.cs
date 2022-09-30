using CoffeeLocator.Repo.Data;
using Microsoft.EntityFrameworkCore;

namespace CoffeeLocator.Repo.GenericRepository;

internal abstract class RepositoryBase<T> : IRepositoryBase<T> where T : class
{
    protected RepositoryContext _repositoryContext;
    public RepositoryBase(RepositoryContext repositoryContext) => _repositoryContext = repositoryContext;

    public async Task CreateAsync(T entity) => await Task.Run(() => _repositoryContext.Set<T>().Add(entity));

    public async Task RemoveAsync(T entity) => await Task.Run(() => _repositoryContext.Set<T>().Remove(entity));

    public async Task UpdateAsync(T entity) => await Task.Run(() => _repositoryContext.Set<T>().Update(entity));

    public async Task<IQueryable<T>> FindAllAsync(bool trackChanges) => !trackChanges ? await Task.Run(() => _repositoryContext.Set<T>().AsNoTracking()) :
                                                                  await Task.Run(() => _repositoryContext.Set<T>());

    public async Task<IQueryable<T>> FindByConditionAsync(Expression<Func<T, bool>> expression, bool trackChanges) => !trackChanges ? await Task.Run(() => _repositoryContext.Set<T>().AsNoTracking()) :
                                                                  await Task.Run(() => _repositoryContext.Set<T>().Where(expression));

}
