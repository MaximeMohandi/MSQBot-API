namespace MSQBot_API.Core.Interfaces
{
    /// <summary>
    /// Business services contract
    /// </summary>
    /// <typeparam name="TReturn">Return type of the service</typeparam>
    /// <typeparam name="TCreation">Type od the entity to create entity</typeparam>
    public interface IBusinessServices<TReturn, TCreation> 
        where TReturn : class 
        where TCreation : class
    {
        /// <summary>
        /// Get one element by it's Id
        /// </summary>
        /// <param name="id">id use to identify the element</param>
        /// <returns>a element with the given id</returns>
        public Task<TReturn> Get(int id);

        /// <summary>
        /// Get All the element
        /// </summary>
        /// <returns>list of all element</returns>
        public Task<List<TReturn>> GetAll();

        /// <summary>
        /// Add a new element
        /// </summary>
        public Task Add(TCreation entity);
    }
}
