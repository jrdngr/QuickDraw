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

        List<string> _ImagePaths;
        List<string> UnseenImages;

        int CurrentIndex = 0;
        [NonSerialized]
        Random rnd = new Random();

        bool _Randomize = true;
        bool _PlaySound = true;
        Seconds _TimerDuration = 30;
        Seconds _PlaySoundAt = 0;

        public bool Randomize { get => _Randomize; set => _Randomize = value; }
        public int TimerDuration { get => _TimerDuration; set => _TimerDuration = value; }
        public int PlaySoundAt { get => _PlaySoundAt; set => _PlaySoundAt = value; }
        public List<string> ImagePaths { get => _ImagePaths; set => _ImagePaths = value; }
        public bool PlaySound { get => _PlaySound; set => _PlaySound = value; }

        public Exercise() {
            _ImagePaths = new List<string>();
            UnseenImages = new List<string>(ImagePaths);
        }

        [OnDeserialized]
        private void OnDeserialization(StreamingContext context) {
            rnd = new Random();
        }

        public string GetNextImage() {
            if (_ImagePaths.Count == 0) {
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
            return _ImagePaths.Count > 0;
        }

        public void AddImagePaths(ICollection<string> imagePaths) {
            _ImagePaths.AddRange(imagePaths);
            UnseenImages.AddRange(imagePaths);
        }

        public void RemoveImagePaths(ICollection<string> imagePaths) {
            foreach (string path in imagePaths) {
                _ImagePaths.Remove(path);
                UnseenImages.Remove(path);
            }
        }

        private static void Swap(List<string> list, int index1, int index2) {
            if (index1 < 0 || index2 < 0 || index1 >= list.Count || index2 >= list.Count) {
                return;
            }
            string temp = list[index1];
            list[index1] = list[index2];
            list[index2] = temp;
        }

        private List<string> GetOrderedPaths(ICollection<string> imagePaths) {
            List<string> orderedPaths = new List<string>(imagePaths);
            orderedPaths.Sort((x, y) => _ImagePaths.IndexOf(x).CompareTo(_ImagePaths.IndexOf(y)));
            return orderedPaths;
        }

        public void ShiftPathsUp(ICollection<string> imagePaths) {
            List<string> orderedPaths = GetOrderedPaths(imagePaths);
            if (orderedPaths.Count > 0 && _ImagePaths.IndexOf(orderedPaths.First()) > 0) {
                foreach (string path in orderedPaths) {
                    int index = _ImagePaths.IndexOf(path);
                    Swap(_ImagePaths, index, index - 1);
                }
            }
        }

        public void ShiftPathsDown(ICollection<string> imagePaths) {
            List<string> orderedPaths = GetOrderedPaths(imagePaths);
            if (orderedPaths.Count > 0 && _ImagePaths.IndexOf(orderedPaths.Last()) <= _ImagePaths.Count - 2) {
                orderedPaths.Reverse();
                foreach (string path in orderedPaths) {
                    int index = _ImagePaths.IndexOf(path);
                    Swap(_ImagePaths, index, index + 1);
                }
            }

        }

        public void ClearImages() {
            _ImagePaths = new List<string>();
            UnseenImages = new List<string>(ImagePaths);
            CurrentIndex = 0;
        }

        public void Reset() {
            CurrentIndex = 0;
            UnseenImages = new List<string>(ImagePaths);
        }

    }
}
