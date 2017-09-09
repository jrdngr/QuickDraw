using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace QuickDraw {
    class ExerciseEditor : INotifyPropertyChanged {

        readonly ExerciseViewer Viewer;

        int _Minutes;
        int _Seconds;

        public event PropertyChangedEventHandler PropertyChanged;

        public int Minutes { get => _Minutes; set { _Minutes = value; UpdateTime(); } }
        public int Seconds { get => _Seconds; set { _Seconds = value; UpdateTime(); } }
        public int PlaySoundAt { get => Viewer.CurrentExercise.PlaySoundAt; set => Viewer.CurrentExercise.PlaySoundAt = value; }
        public bool Randomize { get => Viewer.CurrentExercise.Randomize; set => Viewer.CurrentExercise.Randomize = value; }
        

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

        }

        private void UpdateTime() {
            Viewer.UpdateTimerDuration(60 * _Minutes + _Seconds);
            PropertyChanged(this, new PropertyChangedEventArgs("Seconds"));
            PropertyChanged(this, new PropertyChangedEventArgs("Minutes"));
        }

        private void AddImagePaths(ICollection<string> imagePaths) {
            Viewer.CurrentExercise.AddImagePaths(imagePaths);
        }

        private void RemoveImages(ICollection<string> imagePaths) {
            Viewer.CurrentExercise.RemoveImagePaths(imagePaths);
        }

        private void ClearImages() {
            Viewer.CurrentExercise.ClearImages();
        }
        

    }
}
