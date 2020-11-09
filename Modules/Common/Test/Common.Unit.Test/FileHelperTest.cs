/* Copyright (c) 2020 
 * Owned by Sahana. All rights reserved.
 */

using Assessment.Common;
using NUnit.Framework;
using System.IO;

namespace Common.Unit.Test
{
    [TestFixture]
    public class FileHelperTest
    {
        IFileHelper fileHelper;
        string samplePath;

        [SetUp]
        public void Setup()
        {
            fileHelper = new FileHelper();
            samplePath = @"C:\Temp\Test.txt";
        }

        [Test]
        public void CreateTest()
        {
            fileHelper.Create(samplePath);
            Assert.IsTrue(File.Exists(samplePath));
        }



        [TearDown]
        public void Teardown()
        {
            if (File.Exists(samplePath))
            {
                File.Delete(samplePath); 
            }
        }
    }
}
