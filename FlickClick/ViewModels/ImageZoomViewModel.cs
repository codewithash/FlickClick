using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlickClick.ViewModels
{
    public class ImageZoomViewModel : Screen, INotifyPropertyChangedEx
    {
        public ImageZoomViewModel()
        {

        }

        private string imageSource;

        public string ImageSource
        {
            get { return imageSource; }
            set
            {
                imageSource = value;
                NotifyOfPropertyChange(nameof(ImageSource));
            }
        }

        public void Close()
        {
            MaterialDesignThemes.Wpf.DialogHost.CloseDialogCommand.Execute(null, null);
        }

    }
}
