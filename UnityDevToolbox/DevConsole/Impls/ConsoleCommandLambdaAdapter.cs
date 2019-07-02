using System;
using UnityDevToolbox.Interfaces;


namespace UnityDevToolbox.Impls
{
    /// <summary>
    /// The class implements an adapter to pass lambda functions as commands' implementations
    /// </summary>

    public class ConsoleCommandLambdaAdapter: IConsoleCommand
    {
        public delegate string CommandFunctor(IConsoleController controller, params string[] args);

        protected IConsoleController mController;

        protected string         mName;

        protected CommandFunctor mCommandFunctor;

        public ConsoleCommandLambdaAdapter(IConsoleController controller, string name, CommandFunctor functor)
        {
            mController = controller ?? throw new ArgumentNullException("controller");

            mName = name;

            mCommandFunctor = functor ?? throw new ArgumentNullException("functor");

        }

        /// <summary>
        /// The method executes some actions if the command is invoked by the console engine
        /// </summary>
        /// <param name="args">A list of arguments</param>
        /// <returns>The method returns a string which contains some result of the invocation</returns>

        public string Run(params string[] args)
        {
            return mCommandFunctor.Invoke(mController, args);
        }

        /// <summary>
        /// The readonly property returns an identifier of a command
        /// </summary>

        public string Name => mName;
    }
}
