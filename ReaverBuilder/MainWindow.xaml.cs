using ICSharpCode.AvalonEdit.Highlighting;
using ICSharpCode.AvalonEdit.Highlighting.Xshd;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
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
using System.Xml;

namespace ReaverBuilder
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public int downloads = 0;
        public int fileMoves = 0;
        public int commands = 0;
        public string file;
        public MainWindow()
        {
            InitializeComponent();
            var assembly = Assembly.GetExecutingAssembly();
            var iniHighlighting = "ReaverBuilder.Ini.xshd";
            Stream xshd_stream = assembly.GetManifestResourceStream(iniHighlighting); 
            XmlTextReader xshd_reader = new XmlTextReader(xshd_stream);
            textEditor.SyntaxHighlighting = HighlightingLoader.Load(xshd_reader, HighlightingManager.Instance);
            xshd_reader.Close();
            xshd_stream.Close();
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                DragMove();
            }
            catch (Exception)
            {
                return;
            }
        }

        private void Label_MouseDown(object sender, MouseButtonEventArgs e)
        {

        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            if (textEditor.Text == "")
            {
                var openFileDialog = new OpenFileDialog();
                openFileDialog.InitialDirectory = System.Windows.Forms.Application.StartupPath;
                openFileDialog.Filter = "Reaver Config Files (*.rcf)|*.rcf";
                Nullable<bool> result = openFileDialog.ShowDialog();
                openFileDialog.Title = "Open RCF File";
                if (result == true)
                {
                    textEditor.Text = File.ReadAllText(openFileDialog.FileName);
                }
                else
                {
                    return;
                }
                file = openFileDialog.FileName;
                if (textEditor.LineCount < 4)
                {
                    textEditor.Text = "[Info]\nDownloads=0\nMoves=0\nCommandsExecuted=0";
                    File.WriteAllText(file, textEditor.Text);
                    await Task.Delay(5000);
                }
                var currentFile = new IniFile(file);
                var downloadsstr = currentFile.Read("Downloads", "Info");
                var fileMovesstr = currentFile.Read("Moves", "Info");
                var commandsStr = currentFile.Read("CommandsExecuted", "Info");
                downloads = Convert.ToInt32(downloadsstr);
                fileMoves = Convert.ToInt32(fileMovesstr);
                commands = Convert.ToInt32(commandsStr);
                return;
            }
            var saveFileDialog = new SaveFileDialog();
            saveFileDialog.InitialDirectory = System.Windows.Forms.Application.StartupPath;
            saveFileDialog.Filter = "Reaver Config Files (*.rcf)|*.rcf";
            Nullable<bool> result2 = saveFileDialog.ShowDialog();
            saveFileDialog.Title = "Save RCF File";
            if (result2 == true)
            {
                File.WriteAllText(saveFileDialog.FileName, textEditor.Text);
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (!checkForFile())
            {
                return;
            }
            var currentFile = new IniFile(file);
            currentFile.Write($"Download{downloads}", textBox.Text, "FileDownloads");
            currentFile.Write($"Downloads", $"{downloads + 1}", "Info");
            currentFile.Write($"FileMove{fileMoves}", "C:\\Example\\Path\\%user%", "FileMoves");
            currentFile.Write($"Moves", $"{fileMoves + 1}", "Info");
            downloads++;
            fileMoves++;
            textEditor.Text = File.ReadAllText(file);
        }

        private bool checkForFile()
        {
            if (file == null)
            {
                MessageBox.Show("Open a file first!");
                return false;
            }
            return true;
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            var currentFile = new IniFile(file);
            currentFile.Write($"Command{commands}", textBox.Text, "Commands");
            currentFile.Write($"CommandsExecuted", $"{commands + 1}", "Info");
            commands++;
            textEditor.Text = File.ReadAllText(file);
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void Button_Click_5(object sender, RoutedEventArgs e)
        {
            Process.Start("msedge.exe", "https://github.com/aarushtools/reaver/wiki");
        }

        private void Button_Click_6(object sender, RoutedEventArgs e)
        {
            Process.Start("msedge.exe", "https://github.com/aarushtools/reaver");
        }
    }
}
