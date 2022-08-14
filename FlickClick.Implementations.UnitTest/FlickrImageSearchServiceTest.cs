using FlickClick.Implementations;
using FlickClick.Interfaces.Exceptions;
using FlickClick.Interfaces.Settings;
using Infrastructure.Interfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;

namespace FlickClick.UnitTest
{
    [TestClass]
    public class FlickrImageSearchServiceTest
    {
        [TestMethod]
        public void GivenValidSecretAndKey_Search_ShouldReturnAtleast1Image()
        {
            var applicationLoggerMock = new Mock<IApplicationLogger>();
            var flickrAppSettings = new FlickrAppSettings("dbc316af64fb77dae9140de64262da0a", "0781969a058a2745");
            FlickrImageSearchService flickrImageSearchService = new FlickrImageSearchService(applicationLoggerMock.Object, flickrAppSettings);
            var result = flickrImageSearchService.Search("kitten", 0);
            applicationLoggerMock.Verify(x => x.Log(LogType.Info, "Success"), Times.Once);
            Assert.IsTrue(result.Images.Count > 1);
        }

        [TestMethod]
        [ExpectedException(typeof(FlickrImageSearchException), "Unable to fetch Images")]
        public void GivenInValidSecretAndKey_Search_ShouldReturnAtleast1Image()
        {
            var applicationLoggerMock = new Mock<IApplicationLogger>();
            var flickrAppSettings = new FlickrAppSettings("dbc316af64fb77d9140de64262da0a", "0781969a058a2745");
            FlickrImageSearchService flickrImageSearchService = new FlickrImageSearchService(applicationLoggerMock.Object, flickrAppSettings);
            var result = flickrImageSearchService.Search("kitten", 0);
            applicationLoggerMock.Verify(x => x.Log(LogType.Error, It.IsAny<string>()), Times.Once);

        }
    }
}
