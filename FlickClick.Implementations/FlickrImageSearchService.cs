// --------------------------------------------------------------------------------------------------------------------
// <copyright file="FlickrImageSearchService.cs" company="Intuit">
// © Copyright 2022 Intuit - All rights reserved.
// </copyright>
// <summary>
//   
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace FlickClick.Implementations
{
    using DataManagement.Interfaces.Models;
    using FlickClick.Interfaces;
    using FlickClick.Interfaces.Exceptions;
    using FlickClick.Interfaces.Settings;
    using FlickrNet;
    using Infrastructure.Interfaces;
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Defines the <see cref="FlickrImageSearchService" />.
    /// </summary>
    public class FlickrImageSearchService : IImageSearchService
    {
        /// <summary>
        /// Defines the applicationLogger.
        /// </summary>
        private IApplicationLogger applicationLogger;

        /// <summary>
        /// Defines the appSettings.
        /// </summary>
        private FlickrAppSettings appSettings;

        /// <summary>
        /// Initializes a new instance of the <see cref="FlickrImageSearchService"/> class.
        /// </summary>
        /// <param name="applicationLogger">The applicationLogger<see cref="IApplicationLogger"/>.</param>
        /// <param name="appSettings">The appSettings<see cref="FlickrAppSettings"/>.</param>
        public FlickrImageSearchService(IApplicationLogger applicationLogger, FlickrAppSettings appSettings)
        {
            this.applicationLogger = applicationLogger;
            this.appSettings = appSettings;
        }

        /// <summary>
        /// The Search.
        /// </summary>
        /// <param name="tag">The tag<see cref="string"/>.</param>
        /// <param name="pageNumber">The pageNumber<see cref="int"/>.</param>
        /// <returns>The <see cref="OperationOutcome"/>.</returns>
        public OperationOutcome Search(string tag, int pageNumber)
        {
            try
            {
                List<FlickClickItem> flickClicks = new List<FlickClickItem>();
                Flickr f = new Flickr(this.appSettings.Key, this.appSettings.Secret);
                PhotoSearchOptions o = new PhotoSearchOptions();
                o.Extras = PhotoSearchExtras.AllUrls | PhotoSearchExtras.Description | PhotoSearchExtras.OwnerName;
                o.SortOrder = PhotoSearchSortOrder.Relevance;
                o.Tags = tag;
                PhotoCollection result = f.PhotosSearch(o);
                foreach (Photo photo in result)
                {
                    flickClicks.Add(new FlickClickItem(photo.Title, photo.LargeSquareThumbnailUrl, photo.DoesLargeExist ? photo.LargeUrl : photo.MediumUrl));
                }

                applicationLogger.Log(LogType.Info, "Success");
                return new OperationOutcome() { Images = flickClicks, Page = result.Page, Pages = result.Pages, Total = result.Total, PerPage = result.PerPage };


            }
            catch (Exception ex)
            {
                applicationLogger.Log(LogType.Error, ex.Message);
                throw new FlickrImageSearchException("Unable to fetch Images");
            }
        }
    }
}
