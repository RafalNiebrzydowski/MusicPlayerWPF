using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Threading;

namespace OdtwarzaczMuzyczny
{
    public class MyData
    {
        private static ObservableCollection<Album> albumCollection = new ObservableCollection<Album>();
        private static ObservableCollection<Album> artistAlbumCollection = new ObservableCollection<Album>();
        private static ObservableCollection<Artist> artistCollection = new ObservableCollection<Artist>();
        private static ObservableCollection<Song> songCollection = new ObservableCollection<Song>();
        private static ObservableCollection<Song> playlistCollection = new ObservableCollection<Song>();
        private static List<String> playlist = new List<String>();
        private static List<String> songsList;
        private static List<String> sourceList;
        private static List<String> nameAlbumImageList;
        private static List<String> nameArtistImageList;
        private static bool addedAlbumImage = false;
        private static bool addedArtistImage = false;
        private static List<Image> albumImageList;
        private static List<Image> artistImageList;

        Image standardImage = new Image();


        public MyData(List<String> songs, List<String> sources, List<Image> albumImages, List<Image> artistImages, List<String> nameAlbumImages, List<String> nameArtistImage)
        {
            BackgroundWorker bw = new BackgroundWorker();
            bw.DoWork += bw_DoWork;
            bw.RunWorkerAsync();
            songsList = songs;
            sourceList = sources;
            nameAlbumImageList = nameAlbumImages;
            nameArtistImageList = nameArtistImage;
            albumImageList = albumImages;
            artistImageList = artistImages;
            albumCollection.Clear();
            artistCollection.Clear();


        }
        void bw_DoWork(object sender, DoWorkEventArgs e)
        {

            standardImage.Dispatcher.Invoke((Action)(() =>
            {
                BitmapImage bi3 = new BitmapImage();

                bi3.BeginInit();
                bi3.UriSource = new Uri("/OdtwarzaczMuzyczny;component/Images/art_unknown_grid.png", UriKind.Relative);
                bi3.EndInit();
                standardImage.Stretch = Stretch.Fill;
                standardImage.Source = bi3;
            }));


            int x = 0;

            for (int i = 0; i < songsList.Count; i++)
            {

                TagLib.File tagFile = TagLib.File.Create(sourceList.ElementAt(i) + "//" + songsList.ElementAt(i));
                x = i;
                StringBuilder sb1 = new StringBuilder();
                StringBuilder sb5 = new StringBuilder();
                StringBuilder sb3 = new StringBuilder();
                StringBuilder sb4 = new StringBuilder();
                StringBuilder sb2 = new StringBuilder();
                sb2 = sb2.AppendLine(tagFile.Tag.Album);
                sb4 = sb4.AppendLine(tagFile.Tag.FirstPerformer);
                sb5 = sb5.AppendLine(tagFile.Tag.FirstGenre);





                if (i > 0)
                {
                    TagLib.File tagFile2 = TagLib.File.Create(sourceList.ElementAt(--x) + "//" + songsList.ElementAt(x));
                    sb1 = sb1.AppendLine(tagFile2.Tag.Album);

                    sb3 = sb3.AppendLine(tagFile2.Tag.FirstPerformer);
                }


                if (!sb2.ToString().ToUpper().Equals(sb1.ToString().ToUpper()))
                {
                    for (int k = 0; k < albumImageList.Count; k++)
                    {
                        addedAlbumImage = false;
                        albumImageList.ElementAt(k).Dispatcher.Invoke((Action)(() =>
                        {
                            String Name;
                            Name = sb2.ToString().Remove(sb2.ToString().Length - 2);

                            if (albumImageList.ElementAt(k) != null && nameAlbumImageList.ElementAt(k).Equals(Name + ".png"))
                            {
                                albumCollection.Add(new Album { AlbumImage = albumImageList.ElementAt(k).Source, Name = sb2.ToString(), Genre = sb5.ToString(), Artist = sb4.ToString(), Path = sourceList.ElementAt(i) });


                                addedAlbumImage = true;
                                k = albumImageList.Count();

                            }


                        }));

                    }
                    standardImage.Dispatcher.Invoke((Action)(() =>
              {
                  if (addedAlbumImage == false)
                  { albumCollection.Add(new Album { AlbumImage = standardImage.Source, Name = sb2.ToString(), Genre = sb5.ToString(), Artist = sb4.ToString(), Path = sourceList.ElementAt(i) }); }
              }));
                }

                if (!sb4.ToString().ToUpper().Equals(sb3.ToString().ToUpper()))
                {

                    for (int j = 0; j < artistImageList.Count; j++)
                    {
                        addedArtistImage = false;
                        artistImageList.ElementAt(j).Dispatcher.Invoke((Action)(() =>
                        {
                            String nazwa1;
                            nazwa1 = sb4.ToString().Remove(sb4.ToString().Length - 2);

                            if (artistImageList.ElementAt(j) != null && nameArtistImageList.ElementAt(j).Equals(nazwa1 + ".png"))
                            {


                                artistCollection.Add(new Artist { AlbumImage = artistImageList.ElementAt(j).Source, Genre = sb5.ToString(), Name = sb4.ToString(), Path = sourceList.ElementAt(i) });
                                addedArtistImage = true;
                                j = artistImageList.Count();
                            }


                        }));
                    }
                    standardImage.Dispatcher.Invoke((Action)(() =>
                    {
                        if (addedArtistImage == false)
                        {
                            artistCollection.Add(new Artist { AlbumImage = standardImage.Source, Genre = sb5.ToString(), Name = sb4.ToString(), Path = sourceList.ElementAt(i) });
                        }
                    }));

                }

            }

        }
        public MyData(List<String> songs, List<String> sources, List<Image> images, List<String> nameImages)
        {


            songCollection.Clear();
            BackgroundWorker bw_wszystkie_utwory = new BackgroundWorker();
            bw_wszystkie_utwory.DoWork += bw_wszystkie_utwory_DoWork;
            bw_wszystkie_utwory.RunWorkerAsync();
            songsList = songs;
            sourceList = sources;
            nameAlbumImageList = nameImages;
            albumImageList = images;


        }

        private void bw_wszystkie_utwory_DoWork(object sender, DoWorkEventArgs e)
        {
            standardImage.Dispatcher.Invoke((Action)(() =>
            {
                BitmapImage bi3 = new BitmapImage();

                bi3.BeginInit();
                bi3.UriSource = new Uri("/OdtwarzaczMuzyczny;component/Images/art_unknown_grid.png", UriKind.Relative);
                bi3.EndInit();
                standardImage.Stretch = Stretch.Fill;
                standardImage.Source = bi3;
            }));
            for (int i = 0; i < songsList.Count(); i++)
            {
                TagLib.File tagFile = TagLib.File.Create(sourceList.ElementAt(i) + "//" + songsList.ElementAt(i));
                StringBuilder sb1 = new StringBuilder();
                StringBuilder sb2 = new StringBuilder();
                StringBuilder sb = new StringBuilder();
                StringBuilder sb3 = new StringBuilder();
                StringBuilder sb4 = new StringBuilder();
                sb4 = sb4.AppendLine(tagFile.Tag.Album);

                System.Windows.Application.Current.Dispatcher.Invoke(DispatcherPriority.Normal, (Action)delegate ()
                {
                    for (int k = 0; k < albumImageList.Count; k++)
                    {
                        addedAlbumImage = false;
                        albumImageList.ElementAt(k).Dispatcher.Invoke((Action)(() =>
                        {

                            String Name;
                            Name = sb4.ToString().Remove(sb4.ToString().Length - 2);

                            if (albumImageList.ElementAt(k) != null && nameAlbumImageList.ElementAt(k).Equals(Name + ".png"))
                            {
                                songCollection.Add(new Song
                                {
                                    AlbumImage = albumImageList.ElementAt(k).Source,
                                    Nr = i,
                                    Title = sb.AppendLine(tagFile.Tag.Title).ToString(),
                                    Artist = sb2.AppendLine(tagFile.Tag.FirstPerformer).ToString(),
                                    Duration = tagFile.Properties.Duration.ToString(@"hh\:mm\:ss"),
                                    Path = sourceList.ElementAt(i),
                                    SecondPath = songsList.ElementAt(i)

                                });
                                addedAlbumImage = true;
                                k = albumImageList.Count();

                            }
                        }));
                    }
                    standardImage.Dispatcher.Invoke((Action)(() =>
                    {
                        if (addedAlbumImage == false)
                        {
                            songCollection.Add(new Song
                            {
                                AlbumImage = standardImage.Source,
                                Nr = i,
                                Title = sb.AppendLine(tagFile.Tag.Title).ToString(),
                                Artist = sb2.AppendLine(tagFile.Tag.FirstPerformer).ToString(),
                                Duration = tagFile.Properties.Duration.ToString(@"hh\:mm\:ss"),
                                Path = sourceList.ElementAt(i),
                                SecondPath = songsList.ElementAt(i)

                            });
                        }

                    }));


                });
            }
        }
        public ObservableCollection<Album> GetAlbums()
        {
            return albumCollection;
        }
        public ObservableCollection<Album> GetArtistAlbum(string a)
        {
            artistAlbumCollection.Clear();

            for (int i = 0; i < albumCollection.Count(); i++)
            {
                if (albumCollection.ElementAt(i).Artist.Equals(a))
                {
                    artistAlbumCollection.Add(albumCollection.ElementAt(i));

                }
            }
            return artistAlbumCollection;
        }
        public ObservableCollection<Artist> GetArtists()
        {
            return artistCollection;
        }

        public ObservableCollection<Song> GetSongs()
        {
            return songCollection;
        }

        public MyData(String path)
        {
            playlist.Clear();
            DirectoryInfo dir = new DirectoryInfo(path + "//Playlist");

            foreach (FileInfo file in dir.GetFiles("*.*", SearchOption.AllDirectories))
            {

                if (file.Extension == ".txt")
                {

                    playlist.Add(file.Name);



                }
            }
            playlistCollection.Clear();

            for (int i = 0; i < playlist.Count; i++)

                playlistCollection.Add(new Song { Title = playlist.ElementAt(i).Remove(playlist.ElementAt(i).Length - 4) });

        }

        public ObservableCollection<Song> GetPlaylists()
        {
            return playlistCollection;
        }
    }
}
