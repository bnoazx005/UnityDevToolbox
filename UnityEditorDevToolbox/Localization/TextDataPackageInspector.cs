using CsvHelper;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using UnityDevToolbox.Impls;
using UnityDevToolbox.Interfaces;
using UnityEditor;
using UnityEngine;

namespace UnityEditorDevToolbox.Impls
{
    [CustomEditor(typeof(TextDataPackage), true)]
    public class TextDataPackageInspector: Editor
    {
        private class TextRecord
        {
            public string Key { get; set; }

            public string En { get; set; }
            public string Ru { get; set; }
        }

        private E_LOCALE_TYPE mCurrSelectedType;

        private TextDataPackage mCurrEditedObject;

        public override void OnInspectorGUI()
        {
            EditorGUILayout.BeginHorizontal("box");
            {
                mCurrSelectedType = (E_LOCALE_TYPE)EditorGUILayout.EnumPopup("Parse As", mCurrSelectedType);

                if (GUILayout.Button("Load From CSV"))
                {
                    _buildPackageFromCsvFile();
                }
            }
            EditorGUILayout.EndHorizontal();

            base.OnInspectorGUI();
        }

        private void OnEnable()
        {
            mCurrEditedObject = target as TextDataPackage;
        }

        private void _buildPackageFromCsvFile()
        {
            string filePath = EditorUtility.OpenFilePanel("Select CSV file to read", "", "csv");

            if (string.IsNullOrEmpty(filePath))
            {
                Debug.LogWarning("[TextDataPackageInspector] Invalid file path was specified");
                return;
            }

            using (StreamReader csvFileStreamReader = new StreamReader(filePath))
            {
                using (CsvReader csvReader = new CsvReader(csvFileStreamReader, CultureInfo.InvariantCulture))
                {
                    var record = new TextRecord();
                    var records = csvReader.EnumerateRecords(record);

                    var packageData = _initPackageData();

                    foreach (var currRecord in records)
                    {
                        var parsedData = _getKeyValuePairByLocaleType(currRecord, mCurrSelectedType);

                        packageData.Add(new TextDataPackage.TextDataEntity { mKey = parsedData.Item1, mValue = parsedData.Item2 });
                    }

                    mCurrEditedObject.mData = packageData;
                }
            }
        }

        private List<TextDataPackage.TextDataEntity> _initPackageData()
        {
            var packageData = mCurrEditedObject.mData;

            if (packageData == null)
            {
                packageData = new List<TextDataPackage.TextDataEntity>();
            }
            else
            {
                packageData.Clear();
            }

            return packageData;
        }

        private Tuple<string, string> _getKeyValuePairByLocaleType(TextRecord textEntity, E_LOCALE_TYPE locale)
        {
            switch (locale)
            {
                case E_LOCALE_TYPE.EN:
                    return new Tuple<string, string>(textEntity.Key, textEntity.En);

                case E_LOCALE_TYPE.RU:
                    return new Tuple<string, string>(textEntity.Key, textEntity.Ru);
            }

            return new Tuple<string, string>(textEntity.Key, string.Empty);
        }
    }
}
