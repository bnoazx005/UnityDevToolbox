using UnityDevToolbox.Interfaces;
using System;
using System.Text;
using System.Collections.Generic;


namespace UnityDevToolbox.Impls
{
    public class ConsoleArgsParser: IArgsParser<string>
    {
        /// <summary>
        /// The method parser input stream into a sequence of tokens of a given type
        /// </summary>
        /// <param name="input">An input stream of characters</param>
        /// <returns>A tuple that contains a command name as first argument and an array of objects of type T as second one</returns>

        public Tuple<string, string[]> Parse(string input)
        {
            string[] tokens = _tokenize(input);

            int tokensCount = tokens.Length;

            if (tokensCount < 1)
            {
                throw new UnknownCommandException(input);
            }

            string[] args = (tokensCount > 1) ? new string[tokens.Length - 1] : new string[0];

            return new Tuple<string, string[]>(tokens[0], args);
        }

        private string[] _tokenize(string input)
        {
            List<string> tokens = new List<string>();

            StringBuilder currTokenBuffer = new StringBuilder();

            char currCh = '\0';

            int i = 0;

            while (i < input.Length)
            {
                currCh = input[i];

                // quoted args parsing
                if (currCh == '\"')
                {
                    ++i;

                    // read 'til closing quote
                    while ((i + 1 < input.Length) && (currCh = input[++i]) != '\"')
                    {
                        currTokenBuffer.Append(currCh);
                    }

                    if (currTokenBuffer.Length > 0)
                    {
                        tokens.Add(currTokenBuffer.ToString());
                    }

                    continue;
                }

                if (char.IsWhiteSpace(currCh))
                {
                    // discard current buffer
                    tokens.Add(currTokenBuffer.ToString());

                    currTokenBuffer.Clear();

                    // find next token (skip all delimiters
                    while ((i + 1 < input.Length) && char.IsWhiteSpace(currCh = input[++i]))
                    {
                    }

                    ++i;

                    continue;
                }

                currTokenBuffer.Append(currCh);

                ++i;
            }

            if (currTokenBuffer.Length > 0)
            {
                tokens.Add(currTokenBuffer.ToString());
            }

            return tokens.ToArray();
        }
    }
}
