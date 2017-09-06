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

        string _Name;
        bool _Randomize;
        Seconds _TimerDuration;
        Seconds _StartSoundAt;
        Seconds _PlaySoundEvery;

        public string Name { get => _Name; set => _Name = value; }
        public bool Randomize { get => _Randomize; set => _Randomize = value; }
        public int TimerDuration { get => _TimerDuration; set => _TimerDuration = value; }
        public int StartSoundAt { get => _StartSoundAt; set => _StartSoundAt = value; }
        public int PlaySoundEvery { get => _PlaySoundEvery; set => _PlaySoundEvery = value; }

        public Exercise(String name) {
            _Name = name;
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

    }
}
