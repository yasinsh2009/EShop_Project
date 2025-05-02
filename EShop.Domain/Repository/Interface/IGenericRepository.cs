using EShop.Domain.Entities.Common;

namespace EShop.Domain.Repository.Interface;

public interface IGenericRepository<TEntity> : IAsyncDisposable where TEntity : BaseEntity
{
    IQueryable<TEntity> GetQuery();
    Task AddEntity(TEntity entity);
    Task AddRangeEntity(List<TEntity> entities);
    Task<TEntity> GetEntityById(long entityId);
    void EditEntity(TEntity entity);
    void EditEntityByUser(TEntity entity, string username);
    void DeleteEntity(TEntity entity);
    Task DeleteEntityById(long entityId);
    void DeletePermanentEntity(TEntity entity);
    void DeletePermanentEntities(List<TEntity> entityIds);
    Task SaveChanges();
}