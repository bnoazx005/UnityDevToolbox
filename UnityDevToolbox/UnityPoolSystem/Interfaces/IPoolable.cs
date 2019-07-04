namespace UnityPoolSystem.Interfaces
{
    /// <summary>
    /// interface IPoolable
    /// 
    /// The interface describes a functionality of an object that
    /// can be stored with an object pool
    /// </summary>

    public interface IPoolable<T>
    {
        /// <summary>
        /// The method is called when the object is initialized
        /// </summary>
        /// <param name="poolInstance">A reference to an object pool which owns this object</param>

        void OnCreate(IObjectPool<T> poolInstance);

        /// <summary>
        /// The method is called when the object is destroyed
        /// </summary>

        void OnFree();
    }
}
