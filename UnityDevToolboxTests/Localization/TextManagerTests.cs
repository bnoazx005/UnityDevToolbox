using NSubstitute;
using NUnit.Framework;
using UnityDevToolbox.Impls;
using UnityDevToolbox.Interfaces;


namespace UnityDevToolboxTests
{
    [TestFixture]
    public class TextManagerTests
    {
        private ITextManager mTextManager;

        private const string mPackageName = "SomePackageName";

        [SetUp]
        public void Init()
        {
            Assert.DoesNotThrow(() =>
            {
                mTextManager = new TextManager(Substitute.For<ICoroutineContext>(), mPackageName);
                Assert.IsNotNull(mTextManager);
            });
        }

        [Test]
        public void TestGetFormattedText_PassEmptyString_ReturnsEmptyString()
        {
            var packagesBundle = Substitute.For<ITextDataPackagesBundle>();

            var mockedBundleReader = Substitute.For<IAssetBundleReader>();
            mockedBundleReader.OpenAsync(Arg.Do<OnAssetBundleLoadedCallback>(x => x.Invoke(mockedBundleReader)), Arg.Any<OnErrorCallback>());
            //mockedBundleReader.LoadAsset<ITextDataPackagesBundle>(mPackageName).Returns(packagesBundle);
            // TODO
            mTextManager.Reload(mockedBundleReader, E_LOCALE_TYPE.EN);
        }
    }
}
