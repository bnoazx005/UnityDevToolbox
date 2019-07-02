namespace UnityDevToolbox.Interfaces
{
    public delegate void CommandAction(string input);

    public delegate void EmptyAction();


    /// <summary>
    /// The interfaces represents a functionality of a console's view
    /// </summary>

    public interface IConsoleView
    {
        event CommandAction OnNewCommandSubmited;

        event EmptyAction   OnScrollUp;

        event EmptyAction   OnScrollDown;

        /// <summary>
        /// The method outputs log message into the console
        /// </summary>
        /// <param name="message">A message should be displayed</param>

        void Log(string message);
    }
}
