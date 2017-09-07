using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace QuickDraw {
    class ExerciseEditor {

        public const string APPDATA_FOLDER_NAME = "QuickDraw";
        public const string LAST_OPEN_FILE_NAME = "lastopen";
        public const string FILE_EXTENSION = "qdc";
        public const string FILE_DIALOG_FILTER = "QuickDraw Image Collections(*." + FILE_EXTENSION + ")|*" + FILE_EXTENSION;
        
        Exercise CurrentExercise = new Exercise();

        int _Minutes;
        int _Seconds;

        public int Minutes { get => _Minutes; set { _Minutes = value; UpdateTime(); } }
        public int Seconds { get => _Seconds; set { _Seconds = value; UpdateTime(); } }
        public int PlaySoundAt { get => CurrentExercise.StartSoundAt; set => CurrentExercise.StartSoundAt = value; }
        public int PlaySoundEvery { get => CurrentExercise.PlaySoundEvery; set => CurrentExercise.PlaySoundEvery = value; }

        public ExerciseEditor() {
        }

        private void UpdateTime() {
            CurrentExercise.TimerDuration = 60 * _Minutes + _Seconds;
        }

        private void AddImagePaths(ICollection<string> imagePaths) {
            CurrentExercise.AddImagePaths(imagePaths);
        }

        private void RemoveImages(ICollection<string> imagePaths) {
            CurrentExercise.RemoveImagePaths(imagePaths);
        }

        private void ClearImages() {
            CurrentExercise.ClearImages();
        }

        private Exercise LoadExerciseFromFile(string filePath) {
            System.Runtime.Serialization.IFormatter formatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
            Stream stream = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.Read);
            Exercise exercise = (Exercise)formatter.Deserialize(stream);
            stream.Close();

            return exercise;
        }

        private void SaveExerciseToFile() {
            System.Windows.Forms.SaveFileDialog saveDialog = new System.Windows.Forms.SaveFileDialog() {
                Filter = FILE_DIALOG_FILTER,
                RestoreDirectory = true
            };

            if (saveDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK) {
                IFormatter formatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
                Stream stream = new FileStream(saveDialog.FileName, FileMode.Create, FileAccess.Write, FileShare.None);
                formatter.Serialize(stream, CurrentExercise);
                stream.Close();
            }
        }

    }
}
