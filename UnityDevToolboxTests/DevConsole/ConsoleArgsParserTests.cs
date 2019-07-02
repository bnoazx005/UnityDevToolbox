using NUnit.Framework;
using System;
using UnityDevToolbox;
using UnityDevToolbox.Impls;
using UnityDevToolbox.Interfaces;


namespace UnityDevToolboxTests
{
    [TestFixture]
    public class ConsoleArgsParserTests
    {
        private IArgsParser<string> mArgsParser;

        [SetUp]
        public void Init()
        {
            mArgsParser = new ConsoleArgsParser();
        }

        [Test]
        public void TestParse_PassEmptyString_ReturnsError()
        {
            Assert.Throws<UnknownCommandException>(() =>
            {
                mArgsParser.Parse(string.Empty);
            });
        }

        [TestCase("test", "test")]
        [TestCase("test ", "test")]
        public void TestParse_PassSingleTokenString_ReturnsCommandWithEmptyArgsList(string input, string expectedResult)
        {
            Assert.DoesNotThrow(() =>
            {
                Tuple<string, string[]> result = mArgsParser.Parse(input);

                Assert.AreEqual(expectedResult, result.Item1);
                Assert.IsNotNull(result.Item2);
                Assert.AreEqual(0, result.Item2.Length);
            });
        }

        [TestCase("command arg1 arg2 arg3", "command", 3)]
        [TestCase("command arg1 arg2\targ3\n", "command", 3)]
        [TestCase("command \"scoped argument will be read as single one\" arg2\targ3\n", "command", 3)]
        public void TestParse_PassMultipleArgs_ReturnCorrectTuple(string input, string commandName, int expectedArgsCount)
        {
            Assert.DoesNotThrow(() =>
            {
                Tuple<string, string[]> result = mArgsParser.Parse(input);

                Assert.AreEqual(commandName, result.Item1);
                Assert.IsNotNull(result.Item2);
                Assert.AreEqual(expectedArgsCount, result.Item2.Length);
            });
        }
    }
}
