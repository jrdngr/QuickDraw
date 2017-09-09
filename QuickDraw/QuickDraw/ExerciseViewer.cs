using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace QuickDraw {
    using Seconds = System.Int32;

    public class ExerciseViewer : INotifyPropertyChanged {

        Exercise _CurrentExercise;

        Timer NextImageTimer;
        bool _Running = false;

        string _CurrentImagePath;
        Seconds _TimeRemaining;
        int _CurrentCount;

        public event PropertyChangedEventHandler PropertyChanged;

        public Exercise CurrentExercise { get => _CurrentExercise; set => _CurrentExercise = value; }
        public string CurrentImagePath { get => _CurrentImagePath; }
        public string TimeRemaining { get => FormatTime(_TimeRemaining); }
        public int CurrentCount { get => _CurrentCount; }
        public bool Running { get => _Running; }
        

        public ExerciseViewer() {
            _CurrentExercise = new Exercise();
            NextImageTimer = new Timer(1000);
            NextImageTimer.AutoReset = true;
            NextImageTimer.Elapsed += TimerEvent;
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
                ResetTime();
                NextImageTimer.Start();
                _Running = true;
            } else {
                ResetTime();
                NextImageTimer.Stop();
                _Running = false;
                _TimeRemaining = CurrentExercise.TimerDuration;
            }
        }

        private void TimerEvent(object sender, ElapsedEventArgs args) {
            if (_TimeRemaining > 0) {
                _TimeRemaining -= 1;
                PropertyChanged(this, new PropertyChangedEventArgs("TimeRemaining"));
                if (_TimeRemaining == CurrentExercise.PlaySoundAt) {
                    PlaySound();
                }
            } else {
                _CurrentCount += 1;
                PropertyChanged(this, new PropertyChangedEventArgs("CurrentCount"));
                SetNextImage();
            }
        }

        public void SetNextImage() {
            _CurrentImagePath = CurrentExercise.GetNextImage();
            ResetTime();
            PropertyChanged(this, new PropertyChangedEventArgs("CurrentImagePath"));
        }

        public void UpdateTimerDuration(Seconds time) {
            CurrentExercise.TimerDuration = time;
            _TimeRemaining = time;
            PropertyChanged(this, new PropertyChangedEventArgs("TimeRemaining"));
        }

        public void ResetCount() {
            _CurrentCount = 0;
            PropertyChanged(this, new PropertyChangedEventArgs("CurrentCount"));
        }

        public void ResetTime() {
            _TimeRemaining = CurrentExercise.TimerDuration;
            PropertyChanged(this, new PropertyChangedEventArgs("TimeRemaining"));
        }

        public void SetCurrentExercise(Exercise exercise) {
            CurrentExercise = exercise;
        }

    }
}
