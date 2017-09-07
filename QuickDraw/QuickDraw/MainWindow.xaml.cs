using System;
using System.Collections.Generic;
using System.Linq;
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

        ExerciseViewer CurrentExerciseViewer = new ExerciseViewer(new Exercise());
        ExerciseEditorWindow EditorWindow;

        public MainWindow() {
            InitializeComponent();
            this.DataContext = CurrentExerciseViewer;
        }

        private void CountLabel_MouseDoubleClick(object sender, MouseButtonEventArgs e) {
            CurrentExerciseViewer.ResetCount();
        }

        private void StartButton_Click(object sender, RoutedEventArgs e) {
            if (!CurrentExerciseViewer.Running && CurrentExerciseViewer.HasImages()) {
                StartButton.Content = "Stop";
                CurrentExerciseViewer.ToggleTimer();
            } else if (CurrentExerciseViewer.Running) {
                StartButton.Content = "Start";
                CurrentExerciseViewer.ToggleTimer();
            }
        }

        private void NextImageButton_Click(object sender, RoutedEventArgs e) {
            CurrentExerciseViewer.SetNextImage();
        }

        private void ConfigureButton_Click(object sender, RoutedEventArgs e) {
            EditorWindow = new ExerciseEditorWindow();
            EditorWindow.Show();
        }

        private void Window_Closed(object sender, EventArgs e) {
            EditorWindow.Close();
        }
    }
}
