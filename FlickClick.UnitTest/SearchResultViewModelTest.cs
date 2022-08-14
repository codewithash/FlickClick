// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SearchResultViewModelTest.cs" company="Intuit">
// © Copyright 2022 Intuit - All rights reserved.
// </copyright>
// <summary>
//   
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace FlickClick.UnitTest
{
    using Caliburn.Micro;
    using DataManagement.Interfaces.Models;
    using FlickClick.Interfaces;
    using FlickClick.ViewModels;
    using Infrastructure.Interfaces;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Moq;
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Defines the <see cref="SearchResultViewModelTest" />.
    /// </summary>
    [TestClass]
    public class SearchResultViewModelTest
    {
        /// <summary>
        /// Defines the eventAggreagatorMock.
        /// </summary>
        private Mock<IEventAggregator> eventAggreagatorMock;

        /// <summary>
        /// Defines the dispatcher.
        /// </summary>
        private FakeDispatcher dispatcher;

        /// <summary>
        /// Defines the applicationLoggerMock.
        /// </summary>
        private Mock<IApplicationLogger> applicationLoggerMock;

        /// <summary>
        /// Defines the imageSearchServiceMock.
        /// </summary>
        private Mock<IImageSearchService> imageSearchServiceMock;

        /// <summary>
        /// The Setup.
        /// </summary>
        [TestInitialize]
        public void Setup()
        {
            var item1 = new FlickClickItem("Title1", "ImageURL1", "SquareThumbnailUrl1");
            var item2 = new FlickClickItem("Title2", "ImageURL2", "SquareThumbnailUrl2");
            var item3 = new FlickClickItem("Title3", "ImageURL3", "SquareThumbnailUrl3");
            var item4 = new FlickClickItem("Title4", "ImageURL4", "SquareThumbnailUrl4");
            this.eventAggreagatorMock = new Mock<IEventAggregator>();
            this.dispatcher = new FakeDispatcher();
            this.applicationLoggerMock = new Mock<IApplicationLogger>();
            this.imageSearchServiceMock = new Mock<IImageSearchService>();
            this.imageSearchServiceMock.Setup(x => x.Search(It.IsAny<string>(), 0)).Returns(new OperationOutcome()
            {
                Images = new List<FlickClickItem>() { item1, item2 },
                Page = 1,
                Pages = 5,
                PerPage = 2,
                Total = 10
            });

            this.imageSearchServiceMock.Setup(x => x.Search(It.IsAny<string>(), 1)).Returns(new OperationOutcome()
            {
                Images = new List<FlickClickItem>() { item3, item4 },
                Page = 2,
                Pages = 5,
                PerPage = 2,
                Total = 10
            });

            this.imageSearchServiceMock.Setup(x => x.Search("Handle", 0)).Returns(new OperationOutcome()
            {
                Images = new List<FlickClickItem>() { },
                Page = 0,
                Pages = 0,
                PerPage = 0,
                Total = 0
            });

            this.imageSearchServiceMock.Setup(x => x.Search("Handle", 100)).Throws(new Exception());
        }

        /// <summary>
        /// The GivenValidSearchTag_Search_ShouldReturnImages.
        /// </summary>
        [TestMethod]
        public void GivenValidSearchTag_Search_ShouldReturnImages()
        {
            SearchResultViewModel viewModel = new SearchResultViewModel(eventAggreagatorMock.Object, imageSearchServiceMock.Object, this.applicationLoggerMock.Object, this.dispatcher);
            viewModel.tag = "Kitten";
            viewModel.Search(0).Wait();
            Assert.IsTrue(viewModel.FlickClicks.Count == 2);
        }

        /// <summary>
        /// The GivenValidSearchTagWithPageIndex_Search_ShouldAddToExistingImagesCollection.
        /// </summary>
        [TestMethod]
        public void GivenValidSearchTagWithPageIndex_Search_ShouldAddToExistingImagesCollection()
        {
            SearchResultViewModel viewModel = new SearchResultViewModel(eventAggreagatorMock.Object, imageSearchServiceMock.Object, this.applicationLoggerMock.Object, this.dispatcher);
            viewModel.tag = "Kitten";
            viewModel.Search(0).Wait();
            Assert.IsTrue(viewModel.FlickClicks.Count == 2);
            viewModel.Search(1).Wait();
            Assert.IsTrue(viewModel.FlickClicks.Count == 4);
        }

        /// <summary>
        /// The GivenInValidSearchTagWithPageIndex_Search_ShouldReturnEmptyImagesCollection.
        /// </summary>
        [TestMethod]
        public void GivenInValidSearchTagWithPageIndex_Search_ShouldReturnEmptyImagesCollection()
        {
            SearchResultViewModel viewModel = new SearchResultViewModel(eventAggreagatorMock.Object, imageSearchServiceMock.Object, this.applicationLoggerMock.Object, this.dispatcher);
            viewModel.tag = "Handle";
            viewModel.Search(0).Wait();
            Assert.IsTrue(viewModel.FlickClicks.Count == 0);
        }

        /// <summary>
        /// The GivenInValidSearchTagWithPageIndex_Search_ShouldCatchException.
        /// </summary>
        [TestMethod]
        public void GivenInValidSearchTagWithPageIndex_Search_ShouldCatchException()
        {
            SearchResultViewModel viewModel = new SearchResultViewModel(eventAggreagatorMock.Object, imageSearchServiceMock.Object, this.applicationLoggerMock.Object, this.dispatcher);
            viewModel.tag = "Handle";
            viewModel.Search(100).Wait();
            applicationLoggerMock.Verify(x => x.Log(LogType.Error, It.IsAny<string>()), Times.Exactly(1));
        }
    }
}
