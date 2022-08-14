// --------------------------------------------------------------------------------------------------------------------
// <copyright file="FlickrAppSettings.cs" company="Intuit">
// © Copyright 2022 Intuit - All rights reserved.
// </copyright>
// <summary>
//   
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace FlickClick.Interfaces.Settings
{
    /// <summary>
    /// Defines the <see cref="FlickrAppSettings" />.
    /// </summary>
    public class FlickrAppSettings
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FlickrAppSettings"/> class.
        /// </summary>
        /// <param name="key">The key<see cref="string"/>.</param>
        /// <param name="secret">The secret<see cref="string"/>.</param>
        public FlickrAppSettings(string key, string secret)
        {
            Key = key;
            Secret = secret;
        }

        /// <summary>
        /// Gets the Key.
        /// </summary>
        public string Key { get; private set; }

        /// <summary>
        /// Gets the Secret.
        /// </summary>
        public string Secret { get; private set; }
    }
}
