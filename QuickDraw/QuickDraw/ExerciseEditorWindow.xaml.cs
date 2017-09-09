using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace QuickDraw {
    /// <summary>
    /// Interaction logic for ExerciseEditorWindow.xaml
    /// </summary>
    public partial class ExerciseEditorWindow : Window {

        ExerciseEditor Editor;
        ExerciseViewer Viewer;

        public ExerciseEditorWindow(ExerciseViewer viewer) {
            InitializeComponent();
            Viewer = viewer;
            Editor = new ExerciseEditor(viewer);
            this.DataContext = Editor;
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e) {
            System.Windows.Forms.SaveFileDialog saveDialog = new System.Windows.Forms.SaveFileDialog() {
                Filter = Utils.FILE_DIALOG_FILTER,
                RestoreDirectory = true
            };

            if (saveDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK) {
                Utils.SaveExerciseToFile(Viewer.CurrentExercise, saveDialog.FileName);
            }
        }

        private void LoadButton_Click(object sender, RoutedEventArgs e) {
            System.Windows.Forms.OpenFileDialog openFileDialog = new System.Windows.Forms.OpenFileDialog {
                Filter = Utils.FILE_DIALOG_FILTER,
                RestoreDirectory = true
            };

            if (openFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK) {
                try {
                    Viewer.CurrentExercise = Utils.LoadExerciseFromFile(openFileDialog.FileName);
                    Viewer.SetNextImage();
                } catch (Exception ex) {
                    System.Windows.MessageBox.Show("Could not open file: " + ex.Message);
                }

            }
        }

        private void AddImagesButton_Click(object sender, RoutedEventArgs e) {
            System.Windows.Forms.OpenFileDialog openFileDialog = new System.Windows.Forms.OpenFileDialog {
                Multiselect = true,
                Filter = Utils.IMAGE_FILE_DIALOG_FILTER,
                RestoreDirectory = true
            };

            if (openFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK) {
                try {
                    bool setViewerImage = !Viewer.HasImages();
                    Viewer.CurrentExercise.AddImagePaths(openFileDialog.FileNames);
                    if (setViewerImage) {
                        Viewer.SetNextImage();
                    }
                } catch (Exception ex) {
                    System.Windows.MessageBox.Show("Could not open one or more files: " + ex.Message);
                }
            }
        }

        private void ClearImagesButton_Click(object sender, RoutedEventArgs e) {
            Viewer.CurrentExercise.ClearImages();
            Viewer.SetNextImage();
        }
        
    }
}
