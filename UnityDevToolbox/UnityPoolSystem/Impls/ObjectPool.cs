using System;
using System.Collections.Generic;
using UnityPoolSystem.Interfaces;


namespace UnityPoolSystem.Impls
{
    /// <summary>
    /// class ObjectPool
    /// 
    /// The class implements an object pool for game objects' lifecycle management
    /// </summary>
    /// <typeparam name="T">Any type that implements IPoolable interface</typeparam>

    public class ObjectPool<T>: IObjectPool<T>
        where T: IPoolable<T>
    {
        protected List<T>           mObjectsList;

        protected LinkedList<uint>  mFreeEntitiesRegistry;

        protected IObjectFactory<T> mFactory;

        public ObjectPool(IObjectFactory<T> factory, uint preallocatedObjectsCount = 10)
        {
            mFactory = factory ?? throw new ArgumentNullException("factory");

            mObjectsList = new List<T>(Convert.ToInt32(preallocatedObjectsCount));

            mFreeEntitiesRegistry = new LinkedList<uint>();

            T currObject = default(T);

            for (uint i = 0; i < preallocatedObjectsCount; ++i)
            {
                currObject = factory.Create();

                // initialize and release the object immediately to prepare it for usage
                currObject.OnCreate(this);
                currObject.OnFree();

                mObjectsList.Add(currObject);

                mFreeEntitiesRegistry.AddLast(i);
            }
        }

        /// <summary>
        /// The method creates a new object from the pool
        /// </summary>
        /// <returns>An initialized object of T type</returns>

        public T Create()
        {
            T poolObject = default(T);

            // extract a first free element and initialize it
            if (mFreeEntitiesRegistry.Count > 0)
            {
                int objectId = Convert.ToInt32(mFreeEntitiesRegistry.First);

                poolObject = mObjectsList[objectId];

                mFreeEntitiesRegistry.RemoveFirst();
            }

            /// create a new one instance because there is no free elements within the pool
            if (poolObject == null)
            {
                poolObject = mFactory.Create();
            }

            poolObject.OnCreate(this);

            return poolObject;
        }

        /// <summary>
        /// The method releases a given object
        /// </summary>
        /// <param name="obj">A reference to IPoolable object</param>

        public void Free(T obj)
        {
            if (obj == null)
            {
                return;
            }

            // retrieve index of an object from the pool's array
            int objectId = mObjectsList.FindIndex(entity => entity?.Equals(obj) ?? false);

            /// the object doesn't belong to this pool
            if (objectId == -1)
            {
                return;
            }

            mFreeEntitiesRegistry.AddLast(Convert.ToUInt32(objectId));

            obj.OnFree();
        }
    }
}
