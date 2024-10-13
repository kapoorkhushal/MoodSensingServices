using System.Linq.Expressions;

namespace MoodSensingServices.Application.Interfaces
{
    public interface IRepository<T> : IDisposable where T : class
    {
        /// <summary>
        /// get entity by id
        /// </summary>
        /// <param name="Id">Guid</param>
        /// <returns></returns>
        T? GetById(Guid Id);

        /// <summary>
        /// Get entity data as per condition
        /// </summary>
        /// <param name="expression">condition to be matched</param>
        /// <returns>returns null or single entity which satisfy the condition</returns>
        Task<T?> Get(Expression<Func<T, bool>> expression);

        /// <summary>
        /// Get list of Entries as per condition
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        Task<IList<T>> GetAll(Expression<Func<T, bool>> expression);

        /// <summary>
        /// Get all the list of entities
        /// </summary>
        /// <returns></returns>
        Task<List<T>> SelectAll();

        /// <summary>
        /// save entity into database
        /// </summary>
        /// <returns></returns>
        Task SaveAsync();

        /// <summary>
        /// Insert an entity
        /// </summary>
        /// <param name="entity"><see cref="T"/></param>
        /// <returns></returns>
        Task Insert(T entity);

        /// <summary>
        /// add entity into db context
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task Add(T entity);

        /// <summary>
        /// update an entity
        /// </summary>
        /// <param name="entity">enetity object</param>
        /// <returns></returns>
        Task Update(T entity);

        /// <summary>
        /// deletes the specified entity
        /// </summary>
        /// <param name="entity"><see cref="T"/></param>
        /// <returns></returns>
        Task Delete(T entity);

        /// <summary>
        /// Dispose the resource
        /// </summary>
        void Dispose();
    }
}
