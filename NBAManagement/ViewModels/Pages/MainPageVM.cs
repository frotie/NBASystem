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
using System.Windows.Media.Imaging;

namespace NBAManagement.ViewModels.Pages
{
    class MainPageVM : ViewModelBase
    {
        public ObservableCollection<string> Gallery { get; set; }

        public ObservableCollection<string> ShownPictures { get; set; }

        private const int PicturesToShow = 3;
        private int picOffset = 0;

        private EventBus _eventBus;
        public MainPageVM(EventBus eventBus)
        {
            Gallery = new ObservableCollection<string>();
            ShownPictures = new ObservableCollection<string>();
            _eventBus = eventBus;

            LoadImages();
            MoveImages();
        }

        private void LoadImages()
        {
            string path = Path.Combine(Directory.GetCurrentDirectory(), $"..\\..\\Resources\\Pictures");

            foreach(string file in GetAllFiles(path))
            {
                if (IsValidImage(file))
                {
                    Gallery.Add(file);
                }
            }
        }

        private bool IsValidImage(string filename)
        {
            try
            {
                BitmapImage newImage = new BitmapImage(new Uri(filename));
                return true;
            }
            catch (NotSupportedException)
            {
                return false;
            }
        }

        private IEnumerable<string> GetAllFiles(string dir)
        {
            List<string> files = new List<string>();
            foreach (string subDir in Directory.GetDirectories(dir))
            {
                files.AddRange(GetAllFiles(subDir));
            }

            foreach (string file in Directory.GetFiles(dir))
            {
                files.Add(file);
            }

            return files;
        }

        private int NormilizePicIndex(int index)
        {
            while (index < 0)
            {
                index += Gallery.Count;
            }

            return index;
        }

        private void MoveImages()
        {
            picOffset = NormilizePicIndex(picOffset);
            ShownPictures.Clear();

            if (!Gallery.Any())
            {
                return;
            }

            for (int i = 0; i < PicturesToShow; i++)
            {
                ShownPictures.Add(Gallery[(picOffset + i) % Gallery.Count]);
            }
        }

        public ICommand PreviousImagesCommand => new RelayCommand(o =>
        {
            picOffset -= PicturesToShow;
            MoveImages();
        }, o => Gallery.Any());

        public ICommand NextImagesCommand => new RelayCommand(o =>
        {
            picOffset += PicturesToShow;
            MoveImages();
        }, o => Gallery.Any());

        public ICommand VisitorButton => new RelayCommand(async o =>
        {
            await _eventBus.Publish(new ChangePage(new VisitorMenu()));
        });
    }
}
