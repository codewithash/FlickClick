// --------------------------------------------------------------------------------------------------------------------
// <copyright file="OperationOutcomeBase.cs" company="Intuit">
// © Copyright 2022 Intuit - All rights reserved.
// </copyright>
// <summary>
//   
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace DataManagement.Interfaces.Models
{
    /// <summary>
    /// Defines the <see cref="OperationOutcomeBase" />.
    /// </summary>
    public class OperationOutcomeBase
    {
        /// <summary>
        /// Gets or sets the Page.
        /// </summary>
        public int Page { get; set; }

        /// <summary>
        /// Gets or sets the PerPage.
        /// </summary>
        public int PerPage { get; set; }

        /// <summary>
        /// Gets or sets the Total.
        /// </summary>
        public int Total { get; set; }

        /// <summary>
        /// Gets or sets the Pages.
        /// </summary>
        public int Pages { get; set; }
    }
}
