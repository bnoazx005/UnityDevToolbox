using System.Collections;
using UnityEngine;


namespace UnityDevToolbox.Interfaces
{
    public interface ICoroutineContext
    {
        Coroutine ExecuteCoroutine(IEnumerator coroutine);
        void StopCoroutine(Coroutine coroutine);
    }
}
