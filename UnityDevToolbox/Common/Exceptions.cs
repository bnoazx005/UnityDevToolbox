using System;


namespace UnityDevToolbox
{
    /// <summary>
    /// The class is a type of an exception which is used with ConsoleArgsParser class
    /// </summary>

    public class UnknownCommandException: Exception
    {
        public UnknownCommandException(string input):
            base($"unknown command {input} was found")
        {
        }
    }
}
