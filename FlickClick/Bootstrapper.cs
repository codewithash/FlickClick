// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Bootstrapper.cs" company="Intuit">
// © Copyright 2022 Intuit - All rights reserved.
// </copyright>
// <summary>
//   
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace FlickClick
{
    using Caliburn.Micro;
    using FlickClick.Dispatcher;
    using FlickClick.Implementations;
    using FlickClick.Interfaces;
    using FlickClick.Interfaces.Settings;
    using FlickClick.ViewModels;
    using Infrastructure.Implementations;
    using Infrastructure.Interfaces;
    using Serilog;
    using System;
    using System.Collections.Generic;
    using System.Windows;

    /// <summary>
    /// Defines the <see cref="Bootstrapper" />.
    /// </summary>
    public class Bootstrapper : BootstrapperBase
    {
        /// <summary>
        /// Defines the _container.
        /// </summary>
        private SimpleContainer _container;

        /// <summary>
        /// Defines the seriLogLogger.
        /// </summary>
        private Infrastructure.Interfaces.IApplicationLogger seriLogLogger;

        /// <summary>
        /// Initializes a new instance of the <see cref="Bootstrapper"/> class.
        /// </summary>
        public Bootstrapper()
        {
            Initialize();
        }

        /// <summary>
        /// The Configure.
        /// </summary>
        protected override void Configure()
        {
            _container = new SimpleContainer();
            _container.Singleton<IWindowManager, WindowManager>();
            _container.Singleton<ICryptoService, CryptoService>();
            _container.Singleton<IEventAggregator, EventAggregator>();
            _container.PerRequest<IImageSearchService, FlickrImageSearchService>();
            _container.PerRequest<IDispatcher, RealDispatcher>();
            _container.PerRequest<SearchResultViewModel, SearchResultViewModel>();
            var logger = new LoggerConfiguration().ReadFrom.AppSettings()
                .CreateLogger();
            this.seriLogLogger = new SeriLogApplicationLogger(logger);
            _container.RegisterInstance(typeof(IApplicationLogger), "serilog", seriLogLogger);

            _container.PerRequest<ShellViewModel>();
        }

        /// <summary>
        /// The OnStartup.
        /// </summary>
        /// <param name="sender">The sender<see cref="object"/>.</param>
        /// <param name="e">The e<see cref="StartupEventArgs"/>.</param>
        protected override void OnStartup(object sender, StartupEventArgs e)
        {
            var cryptoService = IoC.Get<ICryptoService>();
            var encryptedKey = cryptoService.Decrypt(Properties.Settings.Default.FLICKR_KEY);
            var encryptedSecret = cryptoService.Decrypt(Properties.Settings.Default.FLICKR_SECRET);
            _container.RegisterInstance(typeof(FlickrAppSettings), "flickrAppSettings", new FlickrAppSettings(encryptedKey, encryptedSecret));
            DisplayRootViewForAsync<ShellViewModel>();
        }

        /// <summary>
        /// The GetInstance.
        /// </summary>
        /// <param name="service">.</param>
        /// <param name="key">.</param>
        /// <returns>.</returns>
        protected override object GetInstance(Type service, string key)
        {
            var instance = _container.GetInstance(service, key);
            if (instance != null)
                return instance;

            throw new System.Exception("Could not locate any instances.");
        }

        /// <summary>
        /// The GetAllInstances.
        /// </summary>
        /// <param name="service">.</param>
        /// <returns>.</returns>
        protected override IEnumerable<object> GetAllInstances(Type service)
        {
            return _container.GetAllInstances(service);
        }

        /// <summary>
        /// The BuildUp.
        /// </summary>
        /// <param name="instance">.</param>
        protected override void BuildUp(object instance)
        {
            _container.BuildUp(instance);
        }
    }
}
