using System;
using UnityDevToolbox.Interfaces;
using UnityEngine;
using UnityEngine.SceneManagement;


namespace UnityDevToolbox.Impls
{
    /// <summary>
    /// abstract class BaseSceneHandler
    /// 
    /// The class is a base for all scene handlers that should load their 
    /// content and initialize logic of a separate level
    /// </summary>

    public abstract class BaseSceneHandler : MonoBehaviour, ISceneHandler
    {
        protected bool mIsInitialized = false;

        public abstract void OnBeginScene();

        public virtual void OnEndScene() { }

        protected void Awake()
        {
            var bootSceneMarker = FindObjectOfType<BootSceneComponent>();

            if (bootSceneMarker == null)
            {
                SceneManager.LoadSceneAsync("Boot", LoadSceneMode.Additive);
                SceneManager.sceneLoaded += _onBootSceneLoaded;
            }
        }

        private void _onBootSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            /// NOTE: Skip underneath logic if the method is called for already loaded scene
            if (scene.buildIndex == gameObject.scene.buildIndex)
            {
                return;
            }

            SceneManager.sceneLoaded -= _onBootSceneLoaded;

#if DEBUG
            Debug.LogWarningFormat("[Base Scene Handler] The following scene {0} was loaded and begins initialization...", scene.name);
#endif
            
            mIsInitialized = true;

            OnBeginScene();
        }

        protected void Start()
        {
            SceneManager.SetActiveScene(gameObject.scene);

            if (!mIsInitialized)
            {
                OnBeginScene();
            }
        }

        protected void OnDestroy()
        {
            if (mIsInitialized)
            {
                OnEndScene();

                mIsInitialized = false;
            }
        }
    }
}
