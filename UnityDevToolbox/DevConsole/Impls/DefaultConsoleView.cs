using System;
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

        private bool                mIsInitialized = false;

        private IArgsParser<string> mArgsParser;
        
        /// <summary>
        /// The method outputs log message into the console
        /// </summary>
        /// <param name="message">A message should be displayed</param>

        public void Log(string message)
        {
            mOutput.text += $"\n{message}";
        }

        private void OnEnabled()
        {
            if (mIsInitialized)
            {
                return;
            }

            mSubmitButton?.onClick.AddListener(_onSubmitButtonClicked);

            mArgsParser = new ConsoleArgsParser();

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
            var args = mArgsParser.Parse(mInput.text);
            
            OnNewCommandSubmited?.Invoke(args.Item1, args.Item2);
        }
    }
}
