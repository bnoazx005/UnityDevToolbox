using UnityEngine;


namespace UnityPoolSystem.Interfaces
{
    /// <summary>
    /// interface IObjectFactory
    /// 
    /// The interface can implement a factory for all available types
    /// </summary>
    /// <typeparam name="T">Any type of an object an instance of which we want to create</typeparam>

    public interface IObjectFactory<T>
    {
        /// <summary>
        /// The method constructs a new object of a given type
        /// </summary>
        /// <returns>A new created object</returns>

        T Create();
    }


    /// <summary>
    /// interface IGameObjectFactory
    /// 
    /// The interface describes a functionality of a factory that specialises on
    /// instantiating of Unity3D's GameObjects
    /// </summary>
    /// <typeparam name="T">A type of a component that derives MonoBehaviour class</typeparam>

    public interface IGameObjectFactory<T>: IObjectFactory<T>
        where T: IPoolable<T>
    {
        /// <summary>
        /// The method instantiates a new GameObject with specified parameters
        /// </summary>
        /// <param name="position">A new position of an object</param>
        /// <param name="rotation">A new orientation of an object</param>
        /// <returns>A reference to a component which was created</returns>

        T Create(Vector3 position, Quaternion rotation);
    }
}
