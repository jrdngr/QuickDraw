using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickDraw {
    using Seconds = System.Int32;

    [Serializable]
    class Exercise {

        List<string> ImagePaths;
        List<string> UnseenImages;

        int CurrentIndex = 0;
        [NonSerialized]
        Random rnd = new Random();

        bool _Randomize;
        Seconds _TimerDuration = 30;
        Seconds _StartSoundAt = 0;
        Seconds _PlaySoundEvery = 0;

        public bool Randomize { get => _Randomize; set => _Randomize = value; }
        public int TimerDuration { get => _TimerDuration; set => _TimerDuration = value; }
        public int StartSoundAt { get => _StartSoundAt; set => _StartSoundAt = value; }
        public int PlaySoundEvery { get => _PlaySoundEvery; set => _PlaySoundEvery = value; }

        public Exercise() {
            ImagePaths = new List<string>();
            UnseenImages = new List<string>(ImagePaths);
        }

        public string GetNextImage() {
            if (UnseenImages.Count == 0) {
                UnseenImages = new List<string>(ImagePaths);
            }
            int nextIndex;
            if (_Randomize) {
                nextIndex = rnd.Next(0, UnseenImages.Count);
            } else {
                nextIndex = CurrentIndex + 1;
            }
            CurrentIndex = nextIndex;
            string nextPath = UnseenImages[nextIndex];
            UnseenImages.RemoveAt(nextIndex);
            return nextPath;
        }

        public bool HasImages() {
            return ImagePaths.Count > 0;
        }

        public void AddImagePaths(ICollection<string> imagePaths) {
            ImagePaths.AddRange(ImagePaths);
            UnseenImages.AddRange(ImagePaths);
        }

        public void RemoveImagePaths(ICollection<string> imagePaths) {
            foreach (string path in imagePaths) {
                ImagePaths.Remove(path);
                UnseenImages.Remove(path);
            }
        }

        public void ClearImages() {
            ImagePaths = new List<string>();
            UnseenImages = new List<string>(ImagePaths);
            CurrentIndex = 0;
        }

        public void Reset() {
            CurrentIndex = 0;
            UnseenImages = new List<string>(ImagePaths);
        }

    }
}
