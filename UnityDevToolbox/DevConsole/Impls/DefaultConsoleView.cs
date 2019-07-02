using System;
using System.Collections.Generic;
using UnityDevToolbox.Interfaces;
using UnityEngine;
using UnityEngine.UI;


namespace UnityDevToolbox.Impls
{
    /// <summary>
    /// The class is a default implementation of a console's view.
    /// Implement your own class to extend the functionality of the console
    /// </summary>

    public sealed class DefaultConsoleView : MonoBehaviour, IConsoleView
    {
        public event CommandAction OnNewCommandSubmited;

        public event EmptyAction   OnScrollUp;

        public event EmptyAction   OnScrollDown;

        public Text                 mOutput;

        public InputField           mInput;

        public Button               mSubmitButton;

        public uint                 mPageSize = 10;

        private bool                mIsInitialized = false;

        private List<string>        mCurrentLogBuffer;

        private int                 mCurrLineIndex;
        
        /// <summary>
        /// The method outputs log message into the console
        /// </summary>
        /// <param name="message">A message should be displayed</param>

        public void Log(string message)
        {
            if (string.IsNullOrEmpty(message))
            {
                return;
            }

            mCurrentLogBuffer.Add(message);

            _updateLogBufferView(mCurrentLogBuffer.ToArray(), mCurrLineIndex, (int)mPageSize);
        }

        /// <summary>
        /// The method clears up current input of the console's view
        /// </summary>

        public void ClearInput()
        {
            mInput.text = string.Empty;
        }

        /// <summary>
        /// The method clears up current output buffer of the console's view
        /// </summary>

        public void ClearOutput()
        {
            mOutput.text = string.Empty;

            mCurrentLogBuffer.Clear();
        }

        private void Awake() => OnEnabled();

        private void OnEnabled()
        {
            if (mIsInitialized)
            {
                return;
            }

            mCurrentLogBuffer = new List<string>();

            mSubmitButton?.onClick.AddListener(_onSubmitButtonClicked);
            
#if DEBUG
            Debug.LogWarning("[Default Console View] The console's view was initialized");
#endif

            mIsInitialized = true;
        }

        private void OnDestroy()
        {
            mSubmitButton?.onClick.RemoveAllListeners();

#if DEBUG
            Debug.LogWarning("[Default Console View] The console's view was destroyed");
#endif
        }

        private void _onSubmitButtonClicked()
        {            
            OnNewCommandSubmited?.Invoke(mInput.text);
        }

        private void _updateLogBufferView(string[] linesBuffer, int currLineIndex, int pageSize)
        {
            mOutput.text = string.Join("\n", linesBuffer, currLineIndex, Math.Min(linesBuffer.Length - currLineIndex, pageSize));
        }

#if DEBUG
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Return))
            {
                _onSubmitButtonClicked();
            }

            if (Input.GetKeyDown(KeyCode.PageUp))
            {
                mCurrLineIndex = Mathf.Max(0, mCurrLineIndex - 1);

                _updateLogBufferView(mCurrentLogBuffer.ToArray(), mCurrLineIndex, (int)mPageSize);
            }

            if (Input.GetKeyDown(KeyCode.PageDown))
            {
                mCurrLineIndex = Mathf.Min(mCurrentLogBuffer.Count, mCurrLineIndex + 1);

                _updateLogBufferView(mCurrentLogBuffer.ToArray(), mCurrLineIndex, (int)mPageSize);
            }
        }
#endif
    }
}
