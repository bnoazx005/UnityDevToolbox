using System;
using UnityEngine;
using UnityPoolSystem.Interfaces;


namespace UnityPoolSystem.Impls
{
    /// <summary>
    /// class GameObjectFactory
    /// 
    /// The class implements IGameObjectFactory interface and instantiates a new GameObjects
    /// which have an attached component of type T
    /// </summary>
    /// <typeparam name="T"></typeparam>

    public class GameObjectFactory<T> : IGameObjectFactory<T>
        where T : MonoBehaviour, IPoolable<T>
    {
        Transform mObjectsListRoot;

        T         mPrefabObject;

        public GameObjectFactory(T prefab, Transform objectsRoot = null)
        {
            mObjectsListRoot = objectsRoot;

            mPrefabObject = prefab ?? throw new ArgumentNullException("prefab");
        }

        /// <summary>
        /// The method instantiates a new GameObject with default values of parameters
        /// </summary>
        /// <returns>A reference to a component which was created</returns>

        public T Create()
        {
            return Create(Vector3.zero, Quaternion.identity);
        }

        /// <summary>
        /// The method instantiates a new GameObject with specified parameters
        /// </summary>
        /// <param name="position">A new position of an object</param>
        /// <param name="rotation">A new orientation of an object</param>
        /// <returns>A reference to a component which was created</returns>

        public T Create(Vector3 position, Quaternion rotation)
        {
            return GameObject.Instantiate<T>(mPrefabObject, position, rotation, mObjectsListRoot);
        }
    }
}
