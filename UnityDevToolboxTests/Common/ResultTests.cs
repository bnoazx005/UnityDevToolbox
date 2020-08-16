using NUnit.Framework;
using System;
using UnityDevToolbox;
using UnityDevToolbox.Impls;
using UnityDevToolbox.Interfaces;


namespace UnityDevToolboxTests
{
    [TestFixture]
    public class ResultTests
    {
        internal enum ResultCode: byte
        {
            Ok,
            Fail,
        }
        
        [Test]
        public void TestResultConstructor_PassActualData_SuccessfullyCreatesResultInstance()
        {
            int expectedValue = 42;

            var result = new Result<int, ResultCode>(expectedValue);

            Assert.IsTrue(result.IsOk);
            Assert.IsFalse(result.HasError);
            Assert.AreEqual(expectedValue, result.Value);
        }

        [Test]
        public void TestResultConstructor_PassErrorData_SucessfullyCreatesResultInstance()
        {
            ResultCode expectedValue = ResultCode.Fail;

            var result = new Result<int, ResultCode>(expectedValue);

            Assert.IsTrue(result.HasError);
            Assert.IsFalse(result.IsOk);
            Assert.AreEqual(expectedValue, result.Error);
        }

        [Test]
        public void TestValue_ResultContainsErrorTryToGetActualValue_ThrowsException()
        {
            ResultCode expectedValue = ResultCode.Fail;

            Assert.Throws<ResultBadAccessException>(() =>
            {
                var value = (new Result<int, ResultCode>(expectedValue)).Value;
            });            
        }

        [Test]
        public void TestError_ResultContainsDataTryToGetErrorValue_ThrowsException()
        {
            int expectedValue = 42;

            Assert.Throws<ResultErrorBadAccessException>(() =>
            {
                var error = (new Result<int, ResultCode>(expectedValue)).Error;
            });
        }
    }
}
