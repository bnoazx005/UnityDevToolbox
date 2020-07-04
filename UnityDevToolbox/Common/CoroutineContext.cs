using UnityEngine;
using System;
using System.Collections;
using UnityDevToolbox.Interfaces;

namespace UnityDevToolbox.Impls
{
    public class CoroutineContext: ICoroutineContext
    {
        private MonoBehaviour mContextOwner;

        public CoroutineContext(MonoBehaviour contextOwner)
        {
            mContextOwner = contextOwner ?? throw new ArgumentNullException("contextOwner");
        }

        public Coroutine ExecuteCoroutine(IEnumerator coroutine)
        {
            return mContextOwner.StartCoroutine(coroutine);
        }

        public void StopCoroutine(Coroutine coroutine)
        {
            mContextOwner.StopCoroutine(coroutine);
        }
    }
}
