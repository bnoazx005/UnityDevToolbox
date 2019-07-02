namespace UnityDevToolbox.Interfaces
{
    /// <summary>
    /// The interface describes a functionality of a console's controller
    /// </summary>

    public interface IConsoleController
    {
        /// <summary>
        /// The method register a given command within the controller
        /// </summary>
        /// <param name="command">An implementation of IConsoleCommand</param>

        void RegisterCommand(IConsoleCommand command);

        /// <summary>
        /// The method removes a command with a given identifier within the controller
        /// </summary>
        /// <param name="commandName">An identifier of a command</param>

        void UnregisterCommand(string commandName);

        /// <summary>
        /// The method clears up current output buffer of the console's view
        /// </summary>

        void ClearOutput();
    }
}
