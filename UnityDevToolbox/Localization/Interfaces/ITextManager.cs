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

        /// <summary>
        /// The method transforms input string of the following structure " aaa{key}bbbbb..."
        /// So let assume that {key} equals to "one" so the result of the method will be "aaaonebbbb..."
        /// </summary>
        /// <param name="rawText"></param>
        /// <returns></returns>

        string GetFormattedText(string rawText);
    }
}
