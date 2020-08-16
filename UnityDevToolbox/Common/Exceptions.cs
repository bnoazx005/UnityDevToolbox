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

    public class ResultBadAccessException: Exception
    {
        public ResultBadAccessException():
            base("Try to access to Result<T, E> which has no actual data")
        {
        }
    }

    public class ResultErrorBadAccessException : Exception
    {
        public ResultErrorBadAccessException() :
            base("Try to access to error of Result<T, E> which doesn't exist")
        {
        }
    }
}
