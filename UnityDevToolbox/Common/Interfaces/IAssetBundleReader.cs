using UnityEngine;


namespace UnityDevToolbox.Interfaces
{
    public delegate void OnAssetBundleLoadedCallback(IAssetBundleReader assetBundleReader);
    public delegate void OnErrorCallback(object errorData);


    public interface IAssetBundleReader
    {
        void OpenAsync(OnAssetBundleLoadedCallback onLoadedCallback, OnErrorCallback onErrorCallback);

        bool Close();

        bool ContainsAsset(string name);

        UnityEngine.Object LoadAsset(string assetName);
        T LoadAsset<T>(string assetName)  where T : UnityEngine.Object;

        AssetBundleRequest LoadAssetAsync(string assetName);
    }
}
