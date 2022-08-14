// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ShellViewModel.cs" company="Intuit">
// © Copyright 2022 Intuit - All rights reserved.
// </copyright>
// <summary>
//   
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace FlickClick.ViewModels
{
    using Caliburn.Micro;
    using System.Collections;

    /// <summary>
    /// Defines the <see cref="ShellViewModel" />.
    /// </summary>
    public class ShellViewModel : Conductor<object>, IScreen, INotifyPropertyChangedEx
    {
        private Stack searches;

        public Stack Searches
        {
            get { return searches; }
            set { searches = value; }
        }

        /// <summary>
        /// Defines the _events.
        /// </summary>
        private readonly IEventAggregator _events;

        /// <summary>
        /// Defines the _windowManager.
        /// </summary>
        private readonly IWindowManager _windowManager;

        /// <summary>
        /// Initializes a new instance of the <see cref="ShellViewModel"/> class.
        /// </summary>
        /// <param name="eventAggregator">The eventAggregator<see cref="IEventAggregator"/>.</param>
        /// <param name="windowManager">The windowManager<see cref="IWindowManager"/>.</param>
        public ShellViewModel(IEventAggregator eventAggregator, IWindowManager windowManager)
        {
            this._events = eventAggregator;
            this._windowManager = windowManager;
            this._events.SubscribeOnPublishedThread(this);
            NotifyOfPropertyChange(nameof(CanSearch));
            Searches = new Stack();
        }

        /// <summary>
        /// Defines the tag.
        /// </summary>
        private string tag;
        private bool _canSearch;

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
            }
        }

        public bool CanSearch
        {
            get { return _canSearch; }
        }

        public void SearchTagUpdated(object eventobj)
        {
            var text = ((System.Windows.Controls.TextBox)((System.Windows.RoutedEventArgs)eventobj).Source).Text;
            if (string.IsNullOrEmpty(text))
            {
                _canSearch = false;
            }

            _canSearch = true;

            NotifyOfPropertyChange(nameof(CanSearch));
        }

        /// <summary>
        /// The Search.
        /// </summary>
        public void Search()
        {
            if (ActiveItem != null)
                Searches.Push(ActiveItem);
            var searchResultViewModel = IoC.Get<SearchResultViewModel>();
            searchResultViewModel.SearchTag = SearchTag;
            ActivateItemAsync(searchResultViewModel);

            NotifyOfPropertyChange(nameof(CanGoBack));
        }

        public bool CanGoBack
        {
            get { return Searches.Count > 0; }
        }


        public void GotoPrevious()
        {
            if (Searches.Count > 0)
            {
                var item = Searches.Pop();
                ChangeActiveItemAsync(item, true);
            }

            NotifyOfPropertyChange(nameof(CanGoBack));

        }
    }
}
