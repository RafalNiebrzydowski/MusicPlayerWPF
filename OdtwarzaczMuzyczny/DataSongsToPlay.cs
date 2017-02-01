using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Threading;

namespace OdtwarzaczMuzyczny
{
    class DataSongsToPlay
    {

        private static ObservableCollection<Song> collectionSongsToPlay = new ObservableCollection<Song>();

        private static List<String> songList;
        private static List<String> sourceList;
        private static List<String> nameAlbumImage;
        private static List<Image> albumImage;
        private static bool addedImage = false;
        Image standardAlbumCover = new Image();

        public DataSongsToPlay(List<String> songs, List<String> source, List<Image> image, List<String> nameImage)
        {
            collectionSongsToPlay.Clear();
            BackgroundWorker allSongsToPlay = new BackgroundWorker();
            allSongsToPlay.DoWork += allSongsToPlay_DoWork;
            allSongsToPlay.RunWorkerAsync();
            songList = songs;
            sourceList = source;
            nameAlbumImage = nameImage;
            albumImage = image;
        }

        private void allSongsToPlay_DoWork(object sender, DoWorkEventArgs e)
        {
            standardAlbumCover.Dispatcher.Invoke((Action)(() =>
            {
                BitmapImage bitmapImage = new BitmapImage();
                bitmapImage.BeginInit();
                bitmapImage.UriSource = new Uri("/OdtwarzaczMuzyczny;component/Images/art_unknown_grid.png", UriKind.Relative);
                bitmapImage.EndInit();
                standardAlbumCover.Stretch = Stretch.Fill;
                standardAlbumCover.Source = bitmapImage;
            }));
            for (int i = 0; i < songList.Count(); i++)
            {
                TagLib.File tagFile = TagLib.File.Create(sourceList.ElementAt(i) + "//" + songList.ElementAt(i));
                StringBuilder sb1 = new StringBuilder();
                StringBuilder artistStringBuilder = new StringBuilder();
                StringBuilder titleStringBuilder = new StringBuilder();
                StringBuilder sb3 = new StringBuilder();
                StringBuilder albumStringBuilder = new StringBuilder();
                albumStringBuilder = albumStringBuilder.AppendLine(tagFile.Tag.Album);
                System.Windows.Application.Current.Dispatcher.Invoke(DispatcherPriority.Normal, (Action)delegate ()
                {
                    for (int k = 0; k < albumImage.Count; k++)
                    {
                        addedImage = false;
                        albumImage.ElementAt(k).Dispatcher.Invoke((Action)(() =>
                       {

                           String name;
                           name = albumStringBuilder.ToString().Remove(albumStringBuilder.ToString().Length - 2);

                           if (albumImage.ElementAt(k) != null && nameAlbumImage.ElementAt(k).Equals(name + ".png"))
                           {
                               collectionSongsToPlay.Add(new Song
                               {
                                   AlbumImage = albumImage.ElementAt(k).Source,
                                   Nr = i,
                                   Title = titleStringBuilder.AppendLine(tagFile.Tag.Title).ToString(),
                                   Artist = artistStringBuilder.AppendLine(tagFile.Tag.FirstPerformer).ToString(),
                                   Duration = tagFile.Properties.Duration.ToString(@"hh\:mm\:ss"),
                                   Path = sourceList.ElementAt(i),
                                   SecondPath = songList.ElementAt(i)

                               });
                               addedImage = true;
                               k = albumImage.Count();

                           }
                       }));
                    }
                    standardAlbumCover.Dispatcher.Invoke((Action)(() =>
                   {
                       if (addedImage == false)
                       {
                           collectionSongsToPlay.Add(new Song
                           {
                               AlbumImage = standardAlbumCover.Source,
                               Nr = i,
                               Title = titleStringBuilder.AppendLine(tagFile.Tag.Title).ToString(),
                               Artist = artistStringBuilder.AppendLine(tagFile.Tag.FirstPerformer).ToString(),
                               Duration = tagFile.Properties.Duration.ToString(@"hh\:mm\:ss"),
                               Path = sourceList.ElementAt(i),
                               SecondPath = songList.ElementAt(i)

                           });
                       }

                   }));


                });
            }
        }


        public ObservableCollection<Song> GetSongs()
        {
            return collectionSongsToPlay;
        }
    }
}
