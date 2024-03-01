using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Threading;
using Microsoft.WindowsAPICodePack.Dialogs;

namespace AudioPlayer
{
    public partial class MainWindow : Window
    {
        private List<string> _audioFiles;
        private int _currentIndex;
        private bool _isRepeat;
        private bool _isShuffle;
        private DispatcherTimer _positionTimer;
        private DispatcherTimer _durationTimer;

        public MainWindow()
        {
            InitializeComponent();
            _audioFiles = new List<string>();
            _currentIndex = 0;
            _isRepeat = false;
            _isShuffle = false;
            _positionTimer = new DispatcherTimer();
            _positionTimer.Interval = TimeSpan.FromSeconds(1);
            _positionTimer.Tick += PositionTimer_Tick;
            _durationTimer = new DispatcherTimer();
            _durationTimer.Interval = TimeSpan.FromSeconds(1);
            _durationTimer.Tick += DurationTimer_Tick;
        }

        private void OpenFolderButton_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new CommonOpenFileDialog
            {
                IsFolderPicker = true
            };

            if (dialog.ShowDialog() == CommonFileDialogResult.Ok)
            {
                _audioFiles = Directory.GetFiles(dialog.FileName, "*.mp3", SearchOption.AllDirectories).ToList();
                _audioFiles.AddRange(Directory.GetFiles(dialog.FileName, "*.m4a", SearchOption.AllDirectories));
                _audioFiles.AddRange(Directory.GetFiles(dialog.FileName, "*.wav", SearchOption.AllDirectories));

                if (_audioFiles.Any())
                {
                    PlayAudio(_audioFiles[0]);
                }
            }
        }

        public void PlayAudio(string filePath)
        {
            mediaElement.Source = new Uri(filePath);
            mediaElement.LoadedBehavior = MediaState.Manual;
            mediaElement.UnloadedBehavior = MediaState.Manual;
            mediaElement.MediaOpened += MediaElement_MediaOpened;
            mediaElement.MediaEnded += MediaElement_MediaEnded;
            mediaElement.Play();
            _positionTimer.Start();
            _durationTimer.Start();
        }


        private void PlayButton_Click(object sender, RoutedEventArgs e)
        {
            mediaElement.Play();
        }

        private void PauseButton_Click(object sender, RoutedEventArgs e)
        {
            mediaElement.Pause();
        }

        private void PreviousButton_Click(object sender, RoutedEventArgs e)
        {
            if (_isShuffle)
            {
                Random rand = new Random();
                _currentIndex = rand.Next(_audioFiles.Count);
            }
            else
            {
                _currentIndex = (_currentIndex - 1 + _audioFiles.Count) % _audioFiles.Count;
            }
            PlayAudio(_audioFiles[_currentIndex]);
        }

        private void NextButton_Click(object sender, RoutedEventArgs e)
        {
            if (_isShuffle)
            {
                Random rand = new Random();
                _currentIndex = rand.Next(_audioFiles.Count);
            }
            else
            {
                _currentIndex = (_currentIndex + 1) % _audioFiles.Count;
            }
            PlayAudio(_audioFiles[_currentIndex]);
        }

        private void RepeatButton_Click(object sender, RoutedEventArgs e)
        {
            _isRepeat = !_isRepeat;
        }

        private void ShuffleButton_Click(object sender, RoutedEventArgs e)
        {
            _isShuffle = !_isShuffle;
            if (_isShuffle)
            {
                Random rand = new Random();
                _audioFiles = _audioFiles.OrderBy(a => rand.Next()).ToList();
            }
            else
            {
                _audioFiles = _audioFiles.OrderBy(a => a).ToList();
            }
        }

        private void VolumeSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (mediaElement != null)
            {
                mediaElement.Volume = (double)volumeSlider.Value / 100;
            }
        }

        private void PositionSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            mediaElement.Position = TimeSpan.FromSeconds((double)positionSlider.Value);
        }

        private void PositionTimer_Tick(object sender, EventArgs e)
        {
            positionSlider.Value = mediaElement.Position.TotalSeconds;
        }

        private void DurationTimer_Tick(object sender, EventArgs e)
        {
            durationTextBlock.Text = $"{mediaElement.Position.ToString(@"mm\:ss")} / {mediaElement.NaturalDuration.TimeSpan.ToString(@"mm\:ss")}";
        }

        private void HistoryButton_Click(object sender, RoutedEventArgs e)
        {
            var historyWindow = new HistoryWindow(_audioFiles);
            historyWindow.ShowDialog();
        }

        private void MediaElement_MediaOpened(object sender, RoutedEventArgs e)
        {
            positionSlider.Maximum = mediaElement.NaturalDuration.TimeSpan.TotalSeconds;
            durationTextBlock.Text = $"{mediaElement.NaturalDuration.TimeSpan.ToString(@"mm\:ss")}";
        }
        private void MediaElement_MediaEnded(object sender, RoutedEventArgs e)
        {
            if (_isRepeat)
            {
                PlayAudio(_audioFiles[_currentIndex]);
            }
            else
            {
                NextButton_Click(null, null);
            }
        }
    }

    public partial class HistoryWindow : Window
    {
        public HistoryWindow(List<string> audioFiles)
        {
            InitializeComponent();
            listBox.ItemsSource = audioFiles;
        }

        private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedItem = listBox.SelectedItem as string;
            if (selectedItem != null)
            {
                ((MainWindow)Owner).PlayAudio(selectedItem);
                DialogResult = true;
            }
        }
    }
}