/* Copyright (c) 2020 
 * Owned by Sahana. All rights reserved.
 */

using Assessment.Common;
using Assessment.ImageSearchBusinessLogic.Utility;
using Assessment.Interfaces.ImageSearchAPI;
using Assessment.Interfaces.Logging;
using Microsoft.Practices.Unity;
using NUnit.Framework;
using Rhino.Mocks;
using System;
using System.IO;

namespace Assessment.ImageSearchBusinessLogic.Unit.Test
{
    [TestFixture]
    public class FetchImagesTest
    {
        IUnityContainer testContainer;
        IDirectoryHelper _directoryMock;
        IFileHelper _fileMock;
        IDevelopmentLogger _devLogMock;
        IFetchImages fetchImage;
        string fileName;

        [SetUp]
        public void Setup()
        {
            testContainer = new UnityContainer();
            _directoryMock = MockRepository.GenerateMock<IDirectoryHelper>();
            _fileMock = MockRepository.GenerateMock<IFileHelper>();
            _devLogMock = MockRepository.GenerateMock<IDevelopmentLogger>();

            testContainer.RegisterInstance<IDirectoryHelper>(_directoryMock);
            testContainer.RegisterInstance<IFileHelper>(_fileMock);
            testContainer.RegisterInstance<IDevelopmentLogger>(_devLogMock);
            fetchImage = new FetchImages(testContainer);
            fileName = Path.Combine(@"C:\Temp\Destination", Constants.HttpResponseFile);
            if (!Directory.Exists(Path.GetDirectoryName(fileName)))
            {
                Directory.CreateDirectory(Path.GetDirectoryName(fileName));
            }
            if (!File.Exists(fileName))
            {
                File.Copy(Constants.HttpResponseFile, fileName);
            }
        }

        [Test]
        public void LoadTest()
        {
            FileStream fs;
            fs = MockRepository.GenerateMock<FileStream>(fileName, FileMode.Create);
            fs.Expect(r => r.Write(null, 0, 0)).Callback((byte[] array, int offset, int count) =>
            {
                return offset== 0 && count==0;
            });
            fs.Expect(r => r.Close()).IgnoreArguments();
            _fileMock.Expect(t => t.Create(Arg<string>.Is.Anything)).IgnoreArguments().Return(fs);

            fetchImage.Load("sampleText", @"C:\Temp\Destination");

        }

        [Test]
        public void Load_ExceptionTest()
        {
            _fileMock.Expect(t => t.Create(Arg<string>.Is.Anything)).IgnoreArguments().Throw(new System.Exception());
            Assert.Throws<Exception>(() => fetchImage.Load("sampleText", @"C:\Temp\Destination"));
        }

        [TearDown]
        public void TearDown()
        {
            File.Delete(fileName);
        }
    }
}
