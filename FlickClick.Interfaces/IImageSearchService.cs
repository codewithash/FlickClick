// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IImageSearchService.cs" company="Intuit">
// © Copyright 2022 Intuit - All rights reserved.
// </copyright>
// <summary>
//   
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace FlickClick.Interfaces
{
    using DataManagement.Interfaces.Models;

    /// <summary>
    /// Defines the <see cref="IImageSearchService" />.
    /// </summary>
    public interface IImageSearchService
    {
        /// <summary>
        /// The Search.
        /// </summary>
        /// <param name="tag">The tag<see cref="string"/>.</param>
        /// <param name="pageIndex">The pageIndex<see cref="int"/>.</param>
        /// <returns>The <see cref="OperationOutcome"/>.</returns>
        OperationOutcome Search(string tag, int pageIndex);
    }
}
