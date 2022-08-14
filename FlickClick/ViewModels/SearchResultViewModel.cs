// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SearchResultViewModel.cs" company="Intuit">
// © Copyright 2022 Intuit - All rights reserved.
// </copyright>
// <summary>
//   
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace FlickClick.ViewModels
{
    using Caliburn.Micro;
    using DataManagement.Interfaces.Models;
    using FlickClick.Commands;
    using FlickClick.Implementations;
    using FlickClick.Interfaces;
    using FlickClick.Views;
    using Infrastructure.Interfaces;
    using MaterialDesignExtensions.Controls;
    using System;
    using System.Collections.ObjectModel;
    using System.Runtime.CompilerServices;
    using System.Threading.Tasks;
    using System.Windows;
    using System.Windows.Input;


    /// <summary>
    /// Defines the <see cref="SearchResultViewModel" />.
    /// </summary>
    public class SearchResultViewModel : Screen, INotifyPropertyChangedEx
    {
        /// <summary>
        /// Defines the events.
        /// </summary>
        private IEventAggregator events;

        /// <summary>
        /// Defines the imageSearchService.
        /// </summary>
        private IImageSearchService imageSearchService;

        /// <summary>
        /// Defines the dispatcher.
        /// </summary>
        private IDispatcher dispatcher;

        /// <summary>
        /// Defines the applicationLogger.
        /// </summary>
        private IApplicationLogger applicationLogger;

        /// <summary>
        /// Initializes a new instance of the <see cref="SearchResultViewModel"/> class.
        /// </summary>
        /// <param name="events">The events<see cref="IEventAggregator"/>.</param>
        /// <param name="imageSearchService">The imageSearchService<see cref="IImageSearchService"/>.</param>
        /// <param name="applicationLogger">The applicationLogger<see cref="IApplicationLogger"/>.</param>
        /// <param name="dispatcher">The dispatcher<see cref="IDispatcher"/>.</param>
        public SearchResultViewModel(IEventAggregator events, IImageSearchService imageSearchService, IApplicationLogger applicationLogger, IDispatcher dispatcher)
        {
            this.events = events;
            this.imageSearchService = imageSearchService;
            this.dispatcher = dispatcher;
            this.applicationLogger = applicationLogger;
            this.FlickClicks = new ObservableCollection<FlickClickItem>();
            SelectionCommand = new RelayCommand(execute: (obj) =>
            {
                ImageZoomView view = new ImageZoomView();
                ImageZoomViewModel viewModel = new ImageZoomViewModel();
                viewModel.ImageSource = SelectedFlickClickItem.ImageUrl;
                view.DataContext = viewModel;
                MaterialDesignThemes.Wpf.DialogHost.Show(view, "AMHOST");

            });
        }

        /// <summary>
        /// Gets or sets the SelectionCommand.
        /// </summary>
        public ICommand SelectionCommand
        {
            get { return selectionCommand; }
            set
            {
                selectionCommand = value;
                NotifyOfPropertyChange(nameof(SelectionCommand));
            }
        }

        /// <summary>
        /// Defines the tag.
        /// </summary>
        internal string tag;

        /// <summary>
        /// Gets or sets the SearchTag.
        /// </summary>
        public string SearchTag
        {
            get { return tag; }
            set
            {
                tag = value;
                NotifyOfPropertyChange(nameof(SearchTag));
                SearchWithTask();
            }
        }

        public async void SearchWithTask()
        {
            await this.Search(CurrentResultPage);
        }

        /// <summary>
        /// Defines the selectedflickClickItem.
        /// </summary>
        private FlickClickItem selectedflickClickItem;

        /// <summary>
        /// Gets or sets the SelectedFlickClickItem.
        /// </summary>
        public FlickClickItem SelectedFlickClickItem
        {
            get { return selectedflickClickItem; }
            set
            {
                selectedflickClickItem = value;
                NotifyOfPropertyChange(nameof(SelectedFlickClickItem));
            }
        }

        /// <summary>
        /// Defines the flickClick.
        /// </summary>
        private ObservableCollection<FlickClickItem> flickClick;

        /// <summary>
        /// Gets or sets the FlickClicks.
        /// </summary>
        public ObservableCollection<FlickClickItem> FlickClicks
        {
            get { return flickClick; }
            set
            {
                flickClick = value;
                NotifyOfPropertyChange(nameof(FlickClicks));
            }
        }

        /// <summary>
        /// Defines the currentResultPage.
        /// </summary>
        private int currentResultPage = 0;

        /// <summary>
        /// Gets or sets the CurrentResultPage.
        /// </summary>
        public int CurrentResultPage
        {
            get { return currentResultPage; }
            set
            {
                currentResultPage = value;
                NotifyOfPropertyChange(nameof(CurrentResultPage));
            }
        }

        /// <summary>
        /// Defines the totalPages.
        /// </summary>
        private int totalPages = 0;

        /// <summary>
        /// Gets or sets the TotalPages.
        /// </summary>
        public int TotalPages
        {
            get { return totalPages; }
            set
            {
                totalPages = value;
                NotifyOfPropertyChange(nameof(TotalPages));
            }
        }

        /// <summary>
        /// Defines the isBusy.
        /// </summary>
        private bool isBusy;

        /// <summary>
        /// Defines the selectionCommand.
        /// </summary>
        private ICommand selectionCommand;

        /// <summary>
        /// Gets or sets a value indicating whether IsBusy.
        /// </summary>
        public bool IsBusy
        {
            get { return isBusy; }
            set
            {
                isBusy = value;
                NotifyOfPropertyChange(nameof(IsBusy));
            }
        }

        /// <summary>
        /// The Search.
        /// </summary>
        /// <param name="pageNumber">The pageNumber<see cref="int"/>.</param>
        /// <returns>The <see cref="Task"/>.</returns>
        public async Task Search(int pageNumber)
        {
            try
            {
                IsBusy = true;
                await Task.Run(async () =>
              {
                  try
                  {

                      var result = this.imageSearchService.Search(SearchTag, pageNumber);

                      dispatcher.Dispatch(() =>
                      {
                          foreach (var item in result.Images)
                          {
                              FlickClicks.Add(item);
                          }
                          TotalPages = result.Pages;
                          CurrentResultPage = result.Page;
                          IsBusy = false;
                      });
                      this.applicationLogger.Log(LogType.Info, "Success");
                  }
                  catch (Exception ex)
                  {
                      this.applicationLogger.Log(LogType.Error, ex.Message);
                      dispatcher.Dispatch(() =>
                      {
                          AlertDialog.ShowDialogAsync("AMHOST", new AlertDialogArguments() { OkButtonLabel = "OK", Title = "Flick Click", Message = "Unable to fetch image" });
                      });
                  }
              });

            }
            catch (Exception ex)
            {
                this.applicationLogger.Log(LogType.Error, ex.Message);
                dispatcher.Dispatch(() =>
                {
                    AlertDialog.ShowDialogAsync("AMHOST", new AlertDialogArguments() { OkButtonLabel = "OK", Title = "Flick Click", Message = "Unable to fetch image" });
                });
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}
