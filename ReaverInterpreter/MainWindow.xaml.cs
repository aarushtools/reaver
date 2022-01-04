using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
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

namespace ReaverInterpreter
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var openFileDialog = new OpenFileDialog();
            openFileDialog.InitialDirectory = System.Windows.Forms.Application.StartupPath;
            openFileDialog.Filter = "Reaver Config Files (*.rcf)|*.rcf";
            Nullable<bool> result = openFileDialog.ShowDialog();
            openFileDialog.Title = "Open RCF File";
            if (result == true)
            {
                FileLoaded.Content = "File loaded: " + openFileDialog.SafeFileName;
            }
            interpretFile(openFileDialog.FileName);
        }

        WebClient wc = new WebClient();

        private void interpretFile(string file)
        {
            if (!File.Exists(file))
                return;
            var RCF = new IniFile(file);
            var Downloads = RCF.Read("Downloads", "Info");
            var FileMoves = RCF.Read("Moves", "Info");
            var CommandsExecuted = RCF.Read("CommandsExecuted", "Info");
            InfoLabel.Content = $"Downloads: {Downloads}     File Moves: {FileMoves}     Commands: {CommandsExecuted}";
            string[] downloadsArray = new string[Convert.ToInt32(Downloads)];
            int[] fileMovesArray = new int[Convert.ToInt32(FileMoves)];
            int[] commandsExecutedArray = new int[Convert.ToInt32(CommandsExecuted)];

            for (int i = 0; i < Convert.ToInt32(Downloads); i++)
            {
                downloadsArray[i] = i.ToString();
            }
            for (int i = 0; i < Convert.ToInt32(FileMoves); i++)
            {
                fileMovesArray[i] = i;
            }
            for (int i = 0; i < Convert.ToInt32(CommandsExecuted); i++)
            {
                commandsExecutedArray[i] = i;
            }
            foreach (var item in downloadsArray)
            {
                bool DownloadComplete = false;
                var DownloadLink = RCF.Read($"Download{item}", "FileDownloads");
                Uri uri = new Uri(DownloadLink);
                string DownloadLinkFilename = System.IO.Path.GetFileName(uri.LocalPath);
                wc.DownloadProgressChanged += (s, e) =>
                {
                    ProgressBar.Value = e.ProgressPercentage;
                    DownloadProgress.Content = e.ProgressPercentage.ToString() + "%";
                };
                wc.DownloadFileCompleted += (s, e) =>
                {
                    DownloadComplete = true;
                };
                wc.DownloadFileAsync(new Uri(DownloadLink), DownloadLinkFilename);
                Process.Content = $"Downloading: {DownloadLinkFilename}";
                Steps.Content = $"Step {Convert.ToInt32(item) + 1} out of {downloadsArray.Count()}";
                while (!DownloadComplete)
                {
                    System.Windows.Forms.Application.DoEvents();
                }
                downloadsArray[Convert.ToInt32(item)] = DownloadLinkFilename;
            }
            wc.Dispose();
            foreach (var item in fileMovesArray)
            {
                var MoveItem = RCF.Read($"FileMove{item}", "FileMoves");
                Process.Content = $"Moving: {downloadsArray[item]} to {MoveItem}";
                Steps.Content = $"Step {item + 1} out of {fileMovesArray.Count()}";
                if (MoveItem.Contains("%user%"))
                {
                    MoveItem = Regex.Replace(MoveItem, @"%user%", Environment.UserName);
                }
                if (!Directory.Exists(MoveItem))
                {
                    Directory.CreateDirectory(MoveItem);
                }
                File.Move(downloadsArray[item], MoveItem + $"\\{downloadsArray[item]}");
            }
            foreach (var item in commandsExecutedArray)
            {
                var CommandExecuted = RCF.Read($"Command{item}", "Commands");
                Process.Content = $"Executing: {CommandExecuted}";
                Steps.Content = $"Step {item + 1} out of {commandsExecutedArray.Count()}";
                using (Process process = new Process())
                {
                    ProcessStartInfo startInfo = new ProcessStartInfo();
                    startInfo.WindowStyle = ProcessWindowStyle.Hidden;
                    startInfo.FileName = "cmd.exe";
                    startInfo.Arguments = "/C " + CommandExecuted;
                    process.StartInfo = startInfo;
                    process.Start();
                }
            }
            Process.Content = "No file currently loaded for processing";
            Steps.Content = "No steps found in file currently loaded";
            FileLoaded.Content = "File loaded: No file loaded currently";
            InfoLabel.Content = "Downloads: 0     File Moves: 0     Commands: 0";
            MessageBox.Show("Done!");
        }

        private void Grid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                DragMove();
            }
            catch (Exception)
            {
                return;
            };
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}
