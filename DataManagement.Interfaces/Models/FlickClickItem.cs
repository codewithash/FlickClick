// --------------------------------------------------------------------------------------------------------------------
// <copyright file="FlickClick.cs" company="Intuit">
// © Copyright 2022 Intuit - All rights reserved.
// </copyright>
// <summary>
//   
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace DataManagement.Interfaces.Models
{
    /// <summary>
    /// Defines the <see cref="FlickClickItem" />.
    /// </summary>
    public class FlickClickItem
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FlickClickItem"/> class.
        /// </summary>
        /// <param name="title">The title<see cref="string"/>.</param>
        /// <param name="squareThumbnailUrl">The squareThumbnailUrl<see cref="string"/>.</param>
        /// <param name="imageUrl">The imageUrl<see cref="string"/>.</param>
        public FlickClickItem(string title, string squareThumbnailUrl, string imageUrl)
        {
            this.Title = title;
            this.SquareThumbnailUrl = squareThumbnailUrl;
            this.ImageUrl = imageUrl;
        }

        /// <summary>
        /// Gets the Title.
        /// </summary>
        public string Title { get; private set; }

        /// <summary>
        /// Gets the SquareThumbnailUrl.
        /// </summary>
        public string SquareThumbnailUrl { get; private set; }

        /// <summary>
        /// Gets the ImageUrl.
        /// </summary>
        public string ImageUrl { get; private set; }
    }
}
