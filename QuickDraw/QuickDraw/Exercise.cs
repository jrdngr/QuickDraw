using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace QuickDraw {
    using Seconds = System.Int32;

    [Serializable]
    public class Exercise {

        List<string> ImagePaths;
        List<string> UnseenImages;

        int CurrentIndex = 0;
        [NonSerialized]
        Random rnd = new Random();

        bool _Randomize = true;
        Seconds _TimerDuration = 30;
        Seconds _PlaySoundAt = 0;

        public bool Randomize { get => _Randomize; set => _Randomize = value; }
        public int TimerDuration { get => _TimerDuration; set => _TimerDuration = value; }
        public int PlaySoundAt { get => _PlaySoundAt; set => _PlaySoundAt = value; }

        public Exercise() {
            ImagePaths = new List<string>();
            UnseenImages = new List<string>(ImagePaths);
        }

        [OnDeserialized]
        public void OnDeserialization(StreamingContext context) {
            rnd = new Random();
        }

        public string GetNextImage() {
            if (ImagePaths.Count == 0) {
                return null;
            }
            if (UnseenImages.Count == 0) {
                UnseenImages = new List<string>(ImagePaths);
            }
            int nextIndex;
            if (_Randomize) {
                nextIndex = rnd.Next(0, UnseenImages.Count);
            } else {
                nextIndex = CurrentIndex + 1;
            }
            if (nextIndex >= UnseenImages.Count) {
                Reset();
            } else {
                CurrentIndex = nextIndex;
            }
            string nextPath = UnseenImages[nextIndex];
            UnseenImages.RemoveAt(nextIndex);
            return nextPath;
        }

        public bool HasImages() {
            return ImagePaths.Count > 0;
        }

        public void AddImagePaths(ICollection<string> imagePaths) {
            ImagePaths.AddRange(imagePaths);
            UnseenImages.AddRange(imagePaths);
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
