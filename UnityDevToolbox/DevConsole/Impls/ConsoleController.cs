using System;
using System.Collections.Generic;
using UnityDevToolbox.Interfaces;


namespace UnityDevToolbox.Impls
{
    /// <summary>
    /// The class is an implementation of a console's controller
    /// </summary>

    public class ConsoleController: IConsoleController
    {
        private IDictionary<string, IConsoleCommand> mCommandsTable;

        private IConsoleView                         mView;

        private IArgsParser<string>                  mArgsParser;

        public ConsoleController(IConsoleView view, IArgsParser<string> argsParser)
        {
            mView = view ?? throw new ArgumentNullException("view");

            mArgsParser = argsParser ?? throw new ArgumentNullException("argsParser");

            mView.OnNewCommandSubmited += _processNewCommand;

            mCommandsTable = new Dictionary<string, IConsoleCommand>();
        }

        /// <summary>
        /// The method register a given command within the controller
        /// </summary>
        /// <param name="command">An implementation of IConsoleCommand</param>

        public void RegisterCommand(IConsoleCommand command)
        {
            if (command == null)
            {
                throw new ArgumentNullException("command");
            }

            mCommandsTable.Add(command.Name, command);
        }

        /// <summary>
        /// The method removes a command with a given identifier within the controller
        /// </summary>
        /// <param name="commandName">An identifier of a command</param>

        public void UnregisterCommand(string commandName)
        {
            mCommandsTable.Remove(commandName);
        }

        private void _processNewCommand(string input)
        {
            var result = mArgsParser.Parse(input);

            IConsoleCommand command = mCommandsTable[result.Item1];

            mView.Log(input);
            mView.Log(command.Run(result.Item2));
        }
    }
}
