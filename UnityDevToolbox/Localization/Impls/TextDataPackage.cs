using System;
using System.Collections.Generic;
using UnityDevToolbox.Interfaces;
using UnityEngine;


namespace UnityDevToolbox.Impls
{
    [CreateAssetMenu]
    public class TextDataPackage: ScriptableObject, ITextDataPackage
    {
        [Serializable]
        public struct TextDataEntity
        {
            public string mKey;
            public string mValue;
        }

        public List<TextDataEntity> mData = new List<TextDataEntity>();

        public string GetTextValue(string key)
        {
            int index = mData.FindIndex(entity => entity.mKey == key);

            return (index != -1) ? mData[index].mValue : key;
        }
    }


    [CreateAssetMenu]
    public class TextDataPackagesBundle: ScriptableObject, ITextDataPackagesBundle
    {
        [Serializable]
        public struct LocalePackageEntity
        {
            public E_LOCALE_TYPE mLocale;
            public TextDataPackage mPackage;
        }

        public List<LocalePackageEntity> mPackages = new List<LocalePackageEntity>();

        public ITextDataPackage GetPackage(E_LOCALE_TYPE locale)
        {
            int index = mPackages.FindIndex(entity => entity.mLocale == locale);

            return (index < 0) ? null : mPackages[index].mPackage;
        }
    }
}
