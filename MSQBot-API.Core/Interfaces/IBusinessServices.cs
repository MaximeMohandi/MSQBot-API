namespace MSQBot_API.Core.Interfaces
{
    /// <summary>
    /// Business services contract
    /// </summary>
    public interface IBusinessServices
    {
        /// <summary>
        /// Get one element by it's Id
        /// </summary>
        /// <typeparam name="T">type of element to return</typeparam>
        /// <param name="id">id use to identify the element</param>
        /// <returns>a element with the given id</returns>
        public Task<T> Get<T>(int id) where T : class;

        /// <summary>
        /// Get All the element
        /// </summary>
        /// <typeparam name="T">type of elmenet to return</typeparam>
        /// <returns>list of all element</returns>
        public Task<List<T>> GetAll<T>() where T : class;

        /// <summary>
        /// Add a new element
        /// </summary>
        /// <typeparam name="T">type of element to add</typeparam>
        /// <param name="source">element to add</param>
        public Task Add<T>(T source) where T : class;
    }
}
