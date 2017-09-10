using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace QuickDraw {
    class Utils {

        public const string APPDATA_FOLDER_NAME = "QuickDraw";
        public const string FILE_EXTENSION = "qde";
        public const string LAST_OPEN_FILE_NAME = "lastopen." + FILE_EXTENSION;
        public const string FILE_DIALOG_FILTER = "QuickDraw Image Collections(*." + FILE_EXTENSION + ")|*" + FILE_EXTENSION;
        public const string IMAGE_FILE_DIALOG_FILTER = "Image Files(*.png;*.jpg;*.gif;*.bmp)|*.png;*.jpg;*.gif;*.bmp";

        public static Exercise LoadExerciseFromFile(string filePath) {
            System.Runtime.Serialization.IFormatter formatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
            Stream stream = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.Read);
            Exercise exercise = (Exercise)formatter.Deserialize(stream);
            stream.Close();

            return exercise;
        }

        public static void SaveExerciseToFile(Exercise exercise, string fileName) {
            if (!fileName.EndsWith("." + FILE_EXTENSION)) fileName += "." + FILE_EXTENSION;
            IFormatter formatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
            Stream stream = new FileStream(fileName, FileMode.Create, FileAccess.Write, FileShare.None);
            formatter.Serialize(stream, exercise);
            stream.Close();
        }

    }
}
