namespace UnityDevToolbox.Interfaces
{
    public interface ITextDataPackage
    {
        string GetTextValue(string key);
    }

    public interface ITextDataPackagesBundle
    {
        ITextDataPackage GetPackage(E_LOCALE_TYPE locale);
    }
}
