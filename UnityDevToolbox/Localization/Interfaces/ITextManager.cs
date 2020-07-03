namespace UnityDevToolbox.Interfaces
{
    public enum E_LOCALE_TYPE: uint
    {
        RU,
        EN,
    }


    public interface ITextManager
    {
        void Reload(IAssetBundleReader assetBundleReader, E_LOCALE_TYPE locale);

        string GetText(string key);
    }
}
