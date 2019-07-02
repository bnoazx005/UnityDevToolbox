namespace UnityDevToolbox.Interfaces
{
    /// <summary>
    /// The interface describes a functionality of a console command that
    /// could be executed with some set of arguments during runtime
    /// </summary>

    public interface IConsoleCommand
    {
        /// <summary>
        /// The method executes some actions if the command is invoked by the console engine
        /// </summary>
        /// <param name="args">A list of arguments</param>
        /// <returns>The method returns a string which contains some result of the invocation</returns>

        string Run(params string[] args);

        /// <summary>
        /// The readonly property returns an identifier of a command
        /// </summary>
        
        string Name { get; }
    }
}
