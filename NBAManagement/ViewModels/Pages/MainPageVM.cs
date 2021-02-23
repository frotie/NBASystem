using NBAManagement.Events;
using NBAManagement.Messages;
using NBAManagement.Services;
using NBAManagement.Views.Pages;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace NBAManagement.ViewModels.Pages
{
    class MainPageVM : ViewModelBase
    {
        public ObservableCollection<string> Gallery { get; set; }
        public ObservableCollection<string> ShownPictures { get; set; }
        private uint _picOffset = 0;
        private EventBus _eventBus;
        public MainPageVM(EventBus eventBus)
        {
            Gallery = new ObservableCollection<string>();
            ShownPictures = new ObservableCollection<string>();
            _eventBus = eventBus;

            LoadImages();
            UpdateImages();
        }

        private uint UpdateImages(uint offset = 0)
        {
            uint result = 0;
            ShownPictures.Clear();
            for (int i = 0; i < 3; i++)
            {
                if(Gallery.Count > offset + i)
                {
                    ShownPictures.Add(Gallery[(int)(i + offset)]);
                    result++;
                }
            }

            return result;
        }

        private void LoadImages()
        {
            string path = Directory.GetCurrentDirectory() + $"\\Resources\\Pictures";

            foreach(string file in Directory.GetFiles(path))
            {
                if(Path.GetExtension(file) == ".jpg")
                {
                    Gallery.Add(file);
                }
            }
        }

        public ICommand PreviousImagesCommand => new RelayCommand(o =>
        {
            _picOffset -= UpdateImages(_picOffset - 3);
        }, o => _picOffset != 0);

        public ICommand NextImagesCommand => new RelayCommand(o =>
        {
            _picOffset += UpdateImages(_picOffset + 3);
        }, o => _picOffset < Gallery.Count - 3);

        public ICommand VisitorButton => new RelayCommand(async o =>
        {
            await _eventBus.Publish(new ChangePage(new VisitorMenu()));
        });
    }
}
