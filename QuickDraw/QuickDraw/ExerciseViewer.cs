using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace QuickDraw {
    using Seconds = System.Int32;

    class ExerciseViewer {

        readonly Exercise CurrentExercise;

        Timer NextImageTimer;
        bool _Running = false;

        string _CurrentImagePath;
        Seconds _TimeRemaining;
        int _CurrentCount;

        public Uri CurrentImagePath { get => (_CurrentImagePath == null)? null : new Uri(_CurrentImagePath);}
        public string TimeRemaining { get => FormatTime(_TimeRemaining); }
        public int CurrentCount { get => _CurrentCount; }
        public bool Running { get => _Running; }

        public ExerciseViewer(Exercise currentExercise) {
            CurrentExercise = currentExercise;
            NextImageTimer = new Timer(CurrentExercise.TimerDuration);
            NextImageTimer.AutoReset = true;
            _CurrentCount = 0;
            _TimeRemaining = 0;
        }

        private string FormatTime(Seconds time) {
            int minutes = time / 60;
            int remainingSeconds = time - (minutes * 60);
            return minutes + ":" + (remainingSeconds < 10 ? "0" : "") + remainingSeconds;
        }

        private void PlaySound() {
            System.Media.SoundPlayer player = new System.Media.SoundPlayer(QuickDraw.Properties.Resources.block);
            player.Play();
        }

        public bool HasImages() {
            return CurrentExercise.HasImages();
        }

        public void ToggleTimer() {
            if (!_Running && CurrentExercise.HasImages()) {
                NextImageTimer.Start();
                _Running = true;
            } else {
                NextImageTimer.Stop();
                _Running = false;
                _TimeRemaining = CurrentExercise.TimerDuration;
            }
        }

        public void SetNextImage() {
            _CurrentImagePath = CurrentExercise.GetNextImage();
        }

        public void ResetCount() {
            _CurrentCount = 0;
        }


    }
}
