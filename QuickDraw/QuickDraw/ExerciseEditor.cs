using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace QuickDraw {
    class ExerciseEditor : INotifyPropertyChanged {

        public const int THUMBNAIL_WIDTH = 50;

        public class ImageBrowserItem {
            readonly string _FilePath;
            readonly BitmapImage _Thumbnail;

            public ImageBrowserItem(string path, BitmapImage thumbnail) {
                _FilePath = path;
                _Thumbnail = thumbnail;
            }

            public string FileName { get => Path.GetFileName(_FilePath).ToString();}
            public string FilePath { get => _FilePath; }
            public BitmapImage Thumbnail { get => _Thumbnail;}
        }

        readonly ExerciseViewer Viewer;

        int _Minutes;
        int _Seconds;

        public event PropertyChangedEventHandler PropertyChanged;

        public int Minutes { get => _Minutes; set { _Minutes = value; UpdateTime(); } }
        public int Seconds { get => _Seconds; set { _Seconds = value; UpdateTime(); } }
        public int PlaySoundAt { get => Viewer.CurrentExercise.PlaySoundAt; set => Viewer.CurrentExercise.PlaySoundAt = value; }
        public bool PlaySound { get => Viewer.CurrentExercise.PlaySound; set => Viewer.CurrentExercise.PlaySound = value; }
        public bool Randomize { get => Viewer.CurrentExercise.Randomize; set => Viewer.CurrentExercise.Randomize = value; }
        public List<ImageBrowserItem> Images {
            get {
                List<ImageBrowserItem> items = new List<ImageBrowserItem>();
                foreach (string path in Viewer.CurrentExercise.ImagePaths) { 
                    BitmapImage image = new BitmapImage();
                    image.BeginInit();
                    image.DecodePixelWidth = THUMBNAIL_WIDTH;
                    image.UriSource = new Uri(path);
                    image.EndInit();
                    items.Add(new ImageBrowserItem(path, image));
                }
                return items;
            }
        }
        

        public ExerciseEditor(ExerciseViewer viewer) {
            Viewer = viewer;
        }

        public void GetLoadedValues() {
            int minutes = Viewer.CurrentExercise.TimerDuration / 60;
            int seconds = Viewer.CurrentExercise.TimerDuration - (60 * minutes);

            _Seconds = seconds;
            _Minutes = minutes;

            PropertyChanged(this, new PropertyChangedEventArgs("PlaySoundAt"));
            PropertyChanged(this, new PropertyChangedEventArgs("Seconds"));
            PropertyChanged(this, new PropertyChangedEventArgs("Minutes"));
            PropertyChanged(this, new PropertyChangedEventArgs("Images"));
        }

        private void UpdateTime() {
            Viewer.UpdateTimerDuration(60 * _Minutes + _Seconds);
            PropertyChanged(this, new PropertyChangedEventArgs("Seconds"));
            PropertyChanged(this, new PropertyChangedEventArgs("Minutes"));
        }

    }
}
