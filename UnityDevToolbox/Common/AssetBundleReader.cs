using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;


namespace UnityDevToolbox.Impls
{
    public class AssetBundleReader
    {
        public delegate void OnAssetBundleLoadedCallback(AssetBundleReader assetBundleReader);
        public delegate void OnErrorCallback(object errorData);

        protected CoroutineContext mCoroutineContext;

        protected bool mIsOpened = false;

        protected AssetBundle mAssetBundle;

        protected string mName;

        public AssetBundleReader(string filename, CoroutineContext coroutineContext)
        {
            mCoroutineContext = coroutineContext ?? throw new ArgumentNullException("coroutineContext");
            mName = filename;
        }

        public void OpenAsync(OnAssetBundleLoadedCallback onLoadedCallback, OnErrorCallback onErrorCallback)
        {
            if (string.IsNullOrEmpty(mName) || mIsOpened || mCoroutineContext == null)
            {
                throw new InvalidOperationException();
            }

            mCoroutineContext.ExecuteCoroutine(_openAsync(mName, onLoadedCallback, onErrorCallback));
        }

        public bool Close()
        {
            if (!mIsOpened)
            {
                return false;
            }

            mIsOpened = false;

            if (mAssetBundle != null)
            {
                mAssetBundle.Unload(false); /// TODO: replace hardcoded value
            }

            return true;
        }

        public bool ContainsAsset(string name)
        {
            return mAssetBundle.Contains(name);
        }

        public UnityEngine.Object LoadAsset(string assetName)
        {
            return mAssetBundle.LoadAsset(assetName);
        }

        public T LoadAsset<T>(string assetName)
            where T : UnityEngine.Object
        {
            return mAssetBundle.LoadAsset<T>(assetName);
        }

        public AssetBundleRequest LoadAssetAsync(string assetName)
        {
            return mAssetBundle.LoadAssetAsync(assetName);
        }

        protected IEnumerator _openAsync(string filename, OnAssetBundleLoadedCallback successCallback = null,
                                         OnErrorCallback errorCallback = null)
        {
            UnityWebRequest www = UnityWebRequestAssetBundle.GetAssetBundle(filename);

            yield return www.SendWebRequest();

            if (www.isNetworkError)
            {
                errorCallback?.Invoke(www.error);
                yield break;
            }

            mAssetBundle = ((DownloadHandlerAssetBundle)www.downloadHandler).assetBundle;

            mIsOpened = true;

            successCallback?.Invoke(this);

            yield return null;
        }
    }
}
