namespace UnityPoolSystem.Interfaces
{
    /// <summary>
    /// interface IObjectPool
    /// 
    /// The interface describes a functionality of an object pool
    /// </summary>

    public interface IObjectPool<T>
    {
        /// <summary>
        /// The method creates a new object from the pool
        /// </summary>
        /// <returns>An initialized object of T type</returns>

        T Create();

        /// <summary>
        /// The method releases a given object
        /// </summary>
        /// <param name="obj">A reference to IPoolable object</param>

        void Free(T obj);
    }
}
