using System;
using System.Collections.Generic;
using System.Text;
using UnityDevToolbox.Interfaces;
using UnityEngine;

namespace UnityDevToolbox.Impls
{
    public class TextManager : ITextManager
    {
        public event OnLocalePackageChanged OnLocalizationChanged;

        protected ICoroutineContext mCoroutineContext;

        protected ITextDataPackage mCurrLoadedPackage;

        protected string mLocalizationPackageName;

        public TextManager(ICoroutineContext coroutineContext, string packageName)
        {
            mCoroutineContext = coroutineContext ?? throw new ArgumentNullException("coroutineContext");

            mCurrLoadedPackage = null;

            mLocalizationPackageName = packageName;
        }

        public void Reload(IAssetBundleReader assetBundleReader, E_LOCALE_TYPE locale)
        {
            if (assetBundleReader == null)
            {
                throw new ArgumentNullException("assetBundleReader");
            }

            assetBundleReader.OpenAsync((reader) =>
                {
                    TextDataPackagesBundle packagesBundle = reader.LoadAsset<TextDataPackagesBundle>(mLocalizationPackageName);

                    if (packagesBundle != null)
                    {
                        mCurrLoadedPackage = packagesBundle.GetPackage(locale);
                        OnLocalizationChanged?.Invoke();
                    }
                },
                (error) =>
                {
                    Debug.LogError(error);
                });
        }

        public string GetText(string key)
        {
            return mCurrLoadedPackage.GetTextValue(key);
        }

        /// <summary>
        /// The method transforms input string of the following structure " aaa{key}bbbbb..."
        /// So let assume that {key} equals to "one" so the result of the method will be "aaaonebbbb..."
        /// </summary>
        /// <param name="rawText"></param>
        /// <returns></returns>

        public string GetFormattedText(string rawText)
        {
            StringBuilder formattedString = new StringBuilder();

            int pos = 0;
            int endPos = 0;

            while ((pos = rawText.IndexOf('{')) != -1)
            {
                formattedString.Append(rawText.Substring(0, pos));

                endPos = rawText.IndexOf('}', pos);

                if (endPos != -1)
                {
                    formattedString.Append(GetText(rawText.Substring(pos + 1, endPos - pos)));
                }

                rawText = rawText.Substring(((endPos != -1) ? endPos : pos) + 1);
            }

            return formattedString.ToString();
        }
    }
}
