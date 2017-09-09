using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace QuickDraw {
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window {

        ExerciseViewer Viewer = new ExerciseViewer();
        ExerciseEditorWindow EditorWindow;

        public MainWindow() {
            InitializeComponent();
            this.DataContext = Viewer;
        }


        private void CountLabel_MouseDoubleClick(object sender, MouseButtonEventArgs e) {
            Viewer.ResetCount();
        }

        private void StartButton_Click(object sender, RoutedEventArgs e) {
            if (!Viewer.Running && Viewer.HasImages()) {
                StartButton.Content = "Stop";
                Viewer.ToggleTimer();
            } else if (Viewer.Running) {
                StartButton.Content = "Start";
                Viewer.ToggleTimer();
            }
        }

        private void NextImageButton_Click(object sender, RoutedEventArgs e) {
            if (Viewer.HasImages()) {
                Viewer.SetNextImage();
            }
        }

        private void ConfigureButton_Click(object sender, RoutedEventArgs e) {
            if (EditorWindow == null || !EditorWindow.IsLoaded) {
                EditorWindow = new ExerciseEditorWindow(Viewer);
                EditorWindow.Top = this.Top;
                EditorWindow.Left = this.Left - EditorWindow.Width;
                EditorWindow.Show();
            } else if (!EditorWindow.IsFocused) {
                EditorWindow.Focus();
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs args) {
            string lastOpenPath = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), Utils.APPDATA_FOLDER_NAME);

            if (!Directory.Exists(lastOpenPath)) {
                Directory.CreateDirectory(lastOpenPath);
            }

            string lastOpenFile = System.IO.Path.Combine(lastOpenPath, Utils.LAST_OPEN_FILE_NAME);

            if (File.Exists(lastOpenFile)) {
                Viewer.CurrentExercise = Utils.LoadExerciseFromFile(lastOpenFile);
                Viewer.SetNextImage();
            }
        }

        private void Window_Closed(object sender, EventArgs e) {
            EditorWindow?.Close();
            string lastOpenPath = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), Utils.APPDATA_FOLDER_NAME);
            lastOpenPath = System.IO.Path.Combine(lastOpenPath, Utils.LAST_OPEN_FILE_NAME);
            Utils.SaveExerciseToFile(Viewer.CurrentExercise, lastOpenPath);
        }
    }
}
