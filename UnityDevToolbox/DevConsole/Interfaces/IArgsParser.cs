using System;


namespace UnityDevToolbox.Interfaces
{
    /// <summary>
    /// The interface represents a functionality of arguments parser
    /// </summary>

    public interface IArgsParser<T>
    {
        /// <summary>
        /// The method parser input stream into a sequence of tokens of a given type
        /// </summary>
        /// <param name="input">An input stream of characters</param>
        /// <returns>A tuple that contains a command name as first argument and an array of objects of type T as second one</returns>

        Tuple<string, T[]> Parse(string input);
    }
}
