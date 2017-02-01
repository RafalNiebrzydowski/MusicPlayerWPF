using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Controls.Primitives;
using System.Windows.Threading;
using System.IO;
using System.Windows.Media.Animation;

namespace OdtwarzaczMuzyczny
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Boolean isCliked = false;
        private bool mediaPlayerIsPlaying = false;
        private bool userIsDraggingSlider = false;
        List<String> mediaFileList;
        string mediaFolder = string.Empty;
        List<string> sourceMediaFolder;
        List<ImageSource> sourceAlbumImage;
        private bool loop=false;
        private bool loopall = false;
        String actualSong;
       
        int begin = 0;
        int rand = 0;
        private bool random = false;
        List<int> randomList = new List<int>();
        public MainWindow()
        {
            InitializeComponent();
            VolumeSlider.Value = 0.5;
            DispatcherTimer timer = new DispatcherTimer();
           
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += timer_Tick;
            timer.Start();

        }
        private void timer_Tick(object sender, EventArgs e)
        {
            if ((mePlayer.Source != null) && (mePlayer.NaturalDuration.HasTimeSpan) && (!userIsDraggingSlider))
            {
                TimeSlider.Minimum = 0;
                TimeSlider.Maximum = mePlayer.NaturalDuration.TimeSpan.TotalSeconds;
                TimeSlider.Value = mePlayer.Position.TotalSeconds;
            }
        }

        private void MusicCollection_Click(object sender, RoutedEventArgs e)
        {
            CollectionMusicWindow dlg = new CollectionMusicWindow(mediaFolder);
           
            if (dlg.ShowDialog() == true)
            {
                ActualSongList.IsEnabled = true;
               
                RepeatButton.IsEnabled = true;
                RandomButton.IsEnabled = true;
                if(dlg.selectedAlbum!=null)
                ((App)Application.Current).UpdateFirst2(dlg.selectedAlbum.Path,dlg.selectedAlbum.AlbumImage);
                if(dlg.selected==true)
                ((App)Application.Current).UpdateList(dlg.songPlaylist, dlg.sourcePlaylist,dlg.albumIamge);
                if (dlg.selectedPlaylist != null)
                ((App)Application.Current).UpdatePlayliste(dlg.selectedPlaylist.Title,dlg.selectedPlaylist.AlbumImage);
            }
        }
        public void SynchPlaylist(string nazwa_playlisty,ImageSource AlbumPoster)
        {
            List<string> sourceList = new List<string>();
            List<string> songsList = new List<string>();
            List<string> albumImageList = new List<string>();
            List<ImageSource> albumImageSourceList = new List<ImageSource>();
            int lineCount = 0 ;
            try{
               using (var fileStream = new FileStream(mediaFolder+"//Playlist//" + nazwa_playlisty +".txt", FileMode.Open))
                    {
                        StreamReader reader = new StreamReader(fileStream);
                       
                        String line;
 
                        while ((line = reader.ReadLine()) != null)
                        {
                            if (lineCount == 0)
                                sourceList.Add(line);
                            if (lineCount  == 2)
                                albumImageList.Add(line);
                            if (lineCount == 1)
                                songsList.Add(line);
                            if(lineCount<3)
                            lineCount++;
                            if (lineCount == 3) lineCount = 0;
                }
            
                        reader.Close();
                   for(int i=0;i<albumImageList.Count;i++){

                       if (albumImageList.ElementAt(i).Contains("file:///")) { albumImageSourceList.Add(new BitmapImage(new Uri(albumImageList.ElementAt(i).Substring(8, albumImageList.ElementAt(i).Length - 8)))); }
                       else albumImageSourceList.Add(new BitmapImage(new Uri(albumImageList.ElementAt(i), UriKind.Relative)));
                       
                   }
                        ChangeSongAndSourceGroup(songsList, sourceList,albumImageSourceList);
                    }
             

                    }
                
                catch (System.UnauthorizedAccessException)
                {

                }

        }
       
        private void RepeatButton_Click(object sender, RoutedEventArgs e)
        {
            loopall = true;
            if (loopall) { RepeatButton.Visibility = Visibility.Hidden; SingleRepeatButtonClicked.Visibility = Visibility.Visible;  }
        }
        private void SingleRepeatButton_Click(object sender, RoutedEventArgs e)
        {

            if (loop) { SingleRepeatButton.Visibility = Visibility.Hidden; SingleRepeatButtonClicked.Visibility = Visibility.Hidden; RepeatButton.Visibility = Visibility.Visible; loop = false; }
        }

        private void ActualSongList_Click(object sender, RoutedEventArgs e)
        {
            SongsList dlg = new SongsList(mediaFileList,sourceMediaFolder,begin);
           
            if (dlg.ShowDialog() == true)
            {
                
                ((App)Application.Current).UpdateFirst(dlg.selectedSong,dlg.nrSong,dlg.selectedAlbumImage);
               
               
            }
        }

       
        public void ChangeSong(String Song,int nr,ImageSource Album)
        {
            begin = nr;
            actualSong = Song;
            
            mePlayer.Source = new Uri(sourceMediaFolder.ElementAt(begin) + "//" + actualSong);
            TagLib.File tagFile = TagLib.File.Create(sourceMediaFolder.ElementAt(begin) + "//" + actualSong);
            StringBuilder sb1 = new StringBuilder();
            StringBuilder sb2 = new StringBuilder();
            StringBuilder sb = new StringBuilder();
            TimeSong.Content = tagFile.Properties.Duration.ToString(@"hh\:mm\:ss");
            NameSong.Content = sb.AppendLine(tagFile.Tag.Title);
            NameAlbum.Content = sb1.AppendLine(tagFile.Tag.Album);
            NameArtist.Content = sb2.AppendLine(tagFile.Tag.FirstPerformer);
            AlbumPoster.Source = Album;
            
        }
        public void ChangeSongAndSource(String Song, String source)
        {
            begin = 0;
            mediaFolder = source;
            actualSong = Song;
            mediaFileList = new List<String>();
            mediaFileList.Add(Song);
            
            mePlayer.Source = new Uri(source + "//" + actualSong); 
            TagLib.File tagFile = TagLib.File.Create(source + "//" + actualSong);
            StringBuilder sb1 = new StringBuilder();
            StringBuilder sb2 = new StringBuilder();
            StringBuilder sb = new StringBuilder();
            TimeSong.Content = tagFile.Properties.Duration.ToString(@"hh\:mm\:ss");
            NameSong.Content = sb.AppendLine(tagFile.Tag.Title);
            NameAlbum.Content = sb1.AppendLine(tagFile.Tag.Album);
            NameArtist.Content = sb2.AppendLine(tagFile.Tag.FirstPerformer);
        }
        public void ChangeSongAndSourceGroup(List<String> songsList, List<String> sourcesList,List<ImageSource> Album)
        {
            begin = 0;
            sourceMediaFolder = new List<string>();
            sourceAlbumImage = new List<ImageSource>();
            sourceMediaFolder = sourcesList ;
            sourceAlbumImage = Album;
            actualSong = songsList.ElementAt(0);
            mediaFileList = new List<String>();
            mediaFileList = songsList;
            mePlayer.Source = new Uri(sourceMediaFolder.ElementAt(begin) + "//" + actualSong);
            TagLib.File tagFile = TagLib.File.Create(sourceMediaFolder.ElementAt(begin) + "//" + actualSong);
            StringBuilder sb1 = new StringBuilder();
            StringBuilder sb2 = new StringBuilder();
            StringBuilder sb = new StringBuilder();
            TimeSong.Content = tagFile.Properties.Duration.ToString(@"hh\:mm\:ss");
            NameSong.Content = sb.AppendLine(tagFile.Tag.Title);
            NameAlbum.Content = sb1.AppendLine(tagFile.Tag.Album);
            NameArtist.Content = sb2.AppendLine(tagFile.Tag.FirstPerformer);
            AlbumPoster.Source=sourceAlbumImage.ElementAt(begin);
            randomList = MixingSongs(begin);
            
        }
        public void ChangeSource(String source, ImageSource Album)
        {
            begin = 0;
            mediaFileList = new List<String>();
            sourceMediaFolder = new List<string>();
            sourceAlbumImage = new List<ImageSource>();
            DirectoryInfo dir = new DirectoryInfo(source);
            foreach (FileInfo file in dir.GetFiles("*.*", SearchOption.AllDirectories))
            {
                if (file.Extension == ".mp3")
                {
                    
                    mediaFileList.Add(file.Name);
                    sourceMediaFolder.Add(source);
                    sourceAlbumImage.Add(Album);
                    actualSong = mediaFileList.ElementAt(begin);
                    mePlayer.Source = new Uri(sourceMediaFolder.ElementAt(begin) + "//" + actualSong);

                }
            }
            TagLib.File tagFile = TagLib.File.Create(sourceMediaFolder.ElementAt(begin) + "//" + actualSong);
            StringBuilder sb1 = new StringBuilder();
            StringBuilder sb2 = new StringBuilder();
            StringBuilder sb = new StringBuilder();
            TimeSong.Content = tagFile.Properties.Duration.ToString(@"hh\:mm\:ss");
            NameSong.Content = sb.AppendLine(tagFile.Tag.Title);
            NameAlbum.Content = sb1.AppendLine(tagFile.Tag.Album);
            NameArtist.Content = sb2.AppendLine(tagFile.Tag.FirstPerformer);
            AlbumPoster.Source =sourceAlbumImage.ElementAt(begin);
            randomList = MixingSongs(begin);
        }
     
        private void AlbumImage_ImageFailed(object sender, ExceptionRoutedEventArgs e)
        {
           
        }
        private void Open_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void Open_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            begin = 0;
            mediaFileList = new List<String>();
            sourceMediaFolder = new List<string>();
            System.Windows.Forms.FolderBrowserDialog fbd = new
           System.Windows.Forms.FolderBrowserDialog();
            if (fbd.ShowDialog() != System.Windows.Forms.DialogResult.Cancel)
            {
                mediaFolder = fbd.SelectedPath;
                DirectoryInfo dir = new DirectoryInfo(mediaFolder);
            }

            MusicCollection.IsEnabled = true;
        }

        private void Play_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = (mePlayer != null) && (mePlayer.Source != null);
           
        }
         private void Next_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = (mePlayer != null) && (mePlayer.Source != null);
           
        }
         private void Previous_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = (mePlayer != null) && (mePlayer.Source != null);
           
        }
        private void Play_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            mePlayer.Play();
            
            
            mediaPlayerIsPlaying = true;

            if (mediaPlayerIsPlaying) { PlayPauseButton.Visibility = Visibility.Hidden; PauseButton.Visibility = Visibility.Visible; PlayThumb.ImageSource = PauseButtonReturned.Source; PlayThumb.Command = MediaCommands.Pause; PlayThumb.CommandTarget = PauseButton; }

           
        }
         private void Next_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            
            if (begin < mediaFileList.Count() - 1)
            {
                ChangeSong(mediaFileList.ElementAt(++begin), begin, sourceAlbumImage.ElementAt(begin));

            }
            else
            {
                begin = 0;
                ChangeSong(mediaFileList.ElementAt(begin), begin, sourceAlbumImage.ElementAt(begin));
            }
            if (random && rand < mediaFileList.Count() - 1) { ChangeSong(mediaFileList.ElementAt(randomList.ElementAt(++rand)), randomList.ElementAt(rand), sourceAlbumImage.ElementAt(randomList.ElementAt(rand))); }
        }
         private void Previous_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            if (begin > 0)
            {
                ChangeSong(mediaFileList.ElementAt(--begin), begin, sourceAlbumImage.ElementAt(begin));
            }
            if (begin == 0)
                ChangeSong(mediaFileList.ElementAt(0), 0, sourceAlbumImage.ElementAt(0));
            if (random && rand < mediaFileList.Count()) { ChangeSong(mediaFileList.ElementAt(randomList.ElementAt(--rand)), randomList.ElementAt(rand), sourceAlbumImage.ElementAt(randomList.ElementAt(rand))); }
        }
        private void Pause_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = mediaPlayerIsPlaying;
        }

        private void Pause_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            mePlayer.Pause();
           
            PauseButton.Visibility = Visibility.Hidden; PlayPauseButton.Visibility = Visibility.Visible; 
            PlayThumb.ImageSource = PlayPauseButtonReturned.Source; PlayThumb.Command = MediaCommands.Play; PlayThumb.CommandTarget = PlayPauseButton;
        }


        private void sliProgress_DragStarted(object sender, DragStartedEventArgs e)
        {
            
           
            userIsDraggingSlider = true;
         
        }

        private void sliProgress_DragCompleted(object sender, DragCompletedEventArgs e)
        {
            userIsDraggingSlider = false;

          
            mePlayer.Position = TimeSpan.FromSeconds(TimeSlider.Value);
           
            
        }

        private void sliProgress_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {   
            TimeNowSong.Content = TimeSpan.FromSeconds(TimeSlider.Value).ToString(@"hh\:mm\:ss");
        
        }

        

        private void VolumeSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            mePlayer.Volume = (double)VolumeSlider.Value;
        }


        private void VolumeButton_Click(object sender, RoutedEventArgs e)
        {
            if (isCliked == false)
            {
                VolumeSlider.Visibility = Visibility.Visible;
                isCliked = true;
            }
            else
            {
                VolumeSlider.Visibility = Visibility.Hidden;
                isCliked =false;
            }
        }

        private void Auto_nextSong(object sender, RoutedEventArgs e)
        {
            if (begin < mediaFileList.Count() - 1 && loop == false && random == false )
            {
                ChangeSong(mediaFileList.ElementAt(++begin), begin, sourceAlbumImage.ElementAt(begin));
            }
            
            if (begin == mediaFileList.Count() - 1 && loop == false && random == false && loopall==false)
            {
                mePlayer.Pause();
                PauseButton.Visibility = Visibility.Hidden; PlayPauseButton.Visibility = Visibility.Visible;
                PlayThumb.ImageSource = PlayPauseButtonReturned.Source; PlayThumb.Command = MediaCommands.Play; PlayThumb.CommandTarget = PlayPauseButton;
                ChangeSong(mediaFileList.ElementAt(0), 0, sourceAlbumImage.ElementAt(0));
            }
            if (loop) ChangeSong(mediaFileList.ElementAt(begin), begin, sourceAlbumImage.ElementAt(begin));
            if (loopall)
            {
              
                if (begin < mediaFileList.Count() - 1 )
                {
                    ChangeSong(mediaFileList.ElementAt(begin), begin, sourceAlbumImage.ElementAt(begin));
                    ++begin;
                }
                else if (begin == mediaFileList.Count() - 1)
                {
                    
                    ChangeSong(mediaFileList.ElementAt(begin), begin, sourceAlbumImage.ElementAt(begin));
                    begin = -1;
                  
                }
            }
            if (random) {

                if (rand < mediaFileList.Count()-1 )
                {
                    
                    ChangeSong(mediaFileList.ElementAt(randomList.ElementAt(++rand)), randomList.ElementAt(rand), sourceAlbumImage.ElementAt(randomList.ElementAt(rand)));
                }
            }
        }

        private void SingleRepeatButtonClicked_Click(object sender, RoutedEventArgs e)
        {
            loopall = false;
            if (loopall == false) { SingleRepeatButtonClicked.Visibility = Visibility.Hidden; RepeatButton.Visibility = Visibility.Hidden; SingleRepeatButton.Visibility = Visibility.Visible; loop = true; }
        }
        
        private void RandomButtonClicked_Click(object sender, RoutedEventArgs e)
        {
            random = false;
            if (random == false) { RandomButtonClicked.Visibility = Visibility.Hidden; RandomButton.Visibility = Visibility.Visible; }
        }

        private void RandomButton_Click(object sender, RoutedEventArgs e)
        {
            random = true;
            if (random == true) { RandomButton.Visibility = Visibility.Hidden; RandomButtonClicked.Visibility = Visibility.Visible; }
        }



        public List<int> MixingSongs(int poczatek) {
            List<int> inputList = new List<int>(mediaFileList.Count()); for (int i = 0; i < mediaFileList.Count(); i++) inputList.Add(i);
        
            List<int> randomList = new List<int>();

            Random r = new Random();
            int randomIndex = 0;
            randomList.Add(poczatek);
            inputList.RemoveAt(poczatek);
            while (inputList.Count > 0)
            {
                randomIndex = r.Next(0, inputList.Count);
               
                    randomList.Add(inputList[randomIndex]);
                    inputList.RemoveAt(randomIndex);
               
            }
            return randomList;
        }
        private void LabelNameSong_SizeChanged(object sender, RoutedEventArgs e)
        {
           
                CreateAnimation();
            
        }
        private void CreateAnimation()
        {
            
            DoubleAnimation doubleAnimation = new DoubleAnimation();
            
            doubleAnimation.From = -NameSong.ActualWidth;
            doubleAnimation.To = canMain.ActualWidth;
            doubleAnimation.RepeatBehavior = RepeatBehavior.Forever;
            doubleAnimation.Duration = new Duration(TimeSpan.Parse("0:0:10"));
            NameSong.BeginAnimation(Canvas.RightProperty, doubleAnimation);
        }
        void Window1_Loaded(object sender, RoutedEventArgs e)
        {
           
                CreateAnimation();
            
        }

        private void MainWindow_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            CreateAnimation();
        }

    }
}
