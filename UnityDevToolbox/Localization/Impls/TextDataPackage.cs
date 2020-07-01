using System;
using System.Collections.Generic;
using UnityDevToolbox.Interfaces;
using UnityEngine;


namespace UnityDevToolbox.Impls
{
    [CreateAssetMenu]
    public class TextDataPackage: ScriptableObject
    {
        [Serializable]
        public struct TextDataEntity
        {
            public string mKey;
            public string mValue;
        }

        public List<TextDataEntity> mData = new List<TextDataEntity>();
    }


    [CreateAssetMenu]
    public class TextDataPackagesBundle: ScriptableObject
    {
        [Serializable]
        public struct LocalePackageEntity
        {
            public E_LOCALE_TYPE mLocale;
            public TextDataPackage mPackage;
        }

        public List<LocalePackageEntity> mPackages = new List<LocalePackageEntity>();

        public TextDataPackage GetPackage(E_LOCALE_TYPE locale)
        {
            int index = mPackages.FindIndex(entity => entity.mLocale == locale);

            return (index < 0) ? null : mPackages[index].mPackage;
        }
    }
}
