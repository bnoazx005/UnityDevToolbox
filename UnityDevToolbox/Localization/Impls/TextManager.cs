using System;
using System.Collections.Generic;
using UnityDevToolbox.Interfaces;
using UnityEngine;

namespace UnityDevToolbox.Impls
{
    public class TextManager : ITextManager
    {
        protected CoroutineContext mCoroutineContext;

        protected TextDataPackage mCurrLoadedPackage;

        protected IDictionary<string, int> mIndexedDataTable;

        protected string mLocalizationPackageName;

        public TextManager(CoroutineContext coroutineContext, string packageName)
        {
            mCoroutineContext = coroutineContext ?? throw new ArgumentNullException("coroutineContext");

            mCurrLoadedPackage = null;
            mIndexedDataTable = new Dictionary<string, int>();

            mLocalizationPackageName = packageName;
        }

        public void Reload(AssetBundleReader assetBundleReader, E_LOCALE_TYPE locale)
        {
            if (assetBundleReader == null)
            {
                throw new ArgumentNullException("assetBundleReader");
            }

            mIndexedDataTable.Clear();

            assetBundleReader.OpenAsync((reader) =>
                {
                    TextDataPackagesBundle packagesBundle = reader.LoadAsset<TextDataPackagesBundle>(mLocalizationPackageName);

                    if (packagesBundle != null)
                    {
                        mCurrLoadedPackage = packagesBundle.GetPackage(locale);
                        _buildIndexedTable();
                    }
                },
                (error) =>
                {
                    Debug.LogError(error);
                });
        }

        public string GetText(string key)
        {
            if (!mIndexedDataTable.ContainsKey(key))
            {
                return string.Empty;
            }

            return mCurrLoadedPackage.mData[mIndexedDataTable[key]].mValue;
        }

        protected void _buildIndexedTable()
        {
            var data = mCurrLoadedPackage.mData;

            for (int i = 0; i < data.Count; ++i)
            {
                mIndexedDataTable.Add(data[i].mKey, i);
            }
        }
    }
}
