// --------------------------------------------------------------------------------------------------------------------
// <copyright file="OperationOutcome.cs" company="Intuit">
// © Copyright 2022 Intuit - All rights reserved.
// </copyright>
// <summary>
//   
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace DataManagement.Interfaces.Models
{
    using System.Collections.Generic;

    /// <summary>
    /// Defines the <see cref="OperationOutcome" />.
    /// </summary>
    public class OperationOutcome : OperationOutcomeBase
    {
        /// <summary>
        /// Gets or sets the Images.
        /// </summary>
        public List<FlickClickItem> Images { get; set; }
    }
}
