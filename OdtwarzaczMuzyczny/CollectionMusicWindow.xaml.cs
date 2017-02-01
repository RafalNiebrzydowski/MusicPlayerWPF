using System;

using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.ComponentModel;
using System.IO;

namespace OdtwarzaczMuzyczny
{
    /// <summary>
    /// Interaction logic for MusicCollection.xaml
    /// </summary>
    /// 
    public partial class CollectionMusicWindow : Window
    {
        MyData allCollections;
        MyData playlistCollection;
        MyData albumCollection;

        List<String> songList = new List<String>();
        List<String> pathList = new List<String>();
        List<String> nameAlbumImageList = new List<String>();
        List<String> nameArtistImageList = new List<String>();
        List<Image> albumImageList = new List<Image>();
        List<Image> artistImageList = new List<Image>();

        
        
        string mediaFolder;
        public Album selectedAlbum { get; set; }
        public Song selectedPlaylist { get; set; }
        public List<string> sourcePlaylist { get; set; }
        public List<string> songPlaylist { get; set; }
        public List<ImageSource> albumIamge { get; set; }
        public bool selected { get; set; }
        public CollectionMusicWindow(String path)
        {
            InitializeComponent();
            selected = false;
            mediaFolder = path;

            Utility.CheckExistDirectories(path);
            Utility.LoadingMp3(path, pathList, songList);
            Utility.LoadingImages(path + "//Okladki", albumImageList, nameAlbumImageList);
            Utility.LoadingImages(path + "//ZdjeciaArtysty", artistImageList, nameArtistImageList);

            allCollections = new MyData(songList, pathList, albumImageList, artistImageList, nameAlbumImageList, nameArtistImageList);
            albumCollection = new MyData(songList, pathList, albumImageList, nameAlbumImageList);
            playlistCollection = new MyData(mediaFolder);
            albumListBox.ItemsSource = allCollections.GetAlbums();
            artistListListBox.ItemsSource = allCollections.GetArtists();
            playlistListBox.ItemsSource = playlistCollection.GetPlaylists();
            allSongsListBox.ItemsSource = allCollections.GetSongs();
            wyborWykonawcy.ItemsSource = allCollections.GetArtists();
            grupowanie.SelectedIndex = 0;
            grupowanie2.SelectedIndex = 0;
        }
        private ListCollectionView ViewAlbum
        {
            get
            {
                return (ListCollectionView)CollectionViewSource.GetDefaultView(allCollections.GetAlbums());
            }
        }
        private ListCollectionView ViewArtists
        {
            get
            {
                return (ListCollectionView)CollectionViewSource.GetDefaultView(allCollections.GetArtists());
            }
        }
        private ListCollectionView ViewAllSongs
        {
            get
            {
                return (ListCollectionView)CollectionViewSource.GetDefaultView(allCollections.GetSongs());
            }
        }
        private ListCollectionView ViewPlaylist
        {
            get
            {
                return (ListCollectionView)CollectionViewSource.GetDefaultView(playlistCollection.GetPlaylists());
            }
        }
        private void GroupGenreAlbum(object sender, RoutedEventArgs e)
        {
            ViewAlbum.GroupDescriptions.Clear();
            ViewAlbum.SortDescriptions.Add(new SortDescription("Gatuenk", ListSortDirection.Ascending));
            AlbumGrouperGenre grouper = new AlbumGrouperGenre();
            ViewAlbum.GroupDescriptions.Add(new PropertyGroupDescription("Gatunek", grouper));
        }
        private void GroupGenreArtist(object sender, RoutedEventArgs e)
        {

            ViewArtists.GroupDescriptions.Clear();
            ViewArtists.SortDescriptions.Add(new SortDescription("Gatuenk", ListSortDirection.Ascending));
            AlbumGrouperGenre grouper = new AlbumGrouperGenre();
            ViewArtists.GroupDescriptions.Add(new PropertyGroupDescription("Gatunek", grouper));


        }
        private void GroupAlphAlbum(object sender, RoutedEventArgs e)
        {

            ViewAlbum.GroupDescriptions.Clear();
            ViewAlbum.SortDescriptions.Add(new SortDescription("Nazwa", ListSortDirection.Ascending));
            AlbumGrouper grouper = new AlbumGrouper();
            ViewAlbum.GroupDescriptions.Add(new PropertyGroupDescription("Nazwa", grouper));

        }
        private void GroupAlfpAllSongs(object sender, RoutedEventArgs e)
        {

            ViewAllSongs.GroupDescriptions.Clear();
            ViewAllSongs.SortDescriptions.Add(new SortDescription("Tytul", ListSortDirection.Ascending));
            AlbumGrouper grouper = new AlbumGrouper();
            ViewAllSongs.GroupDescriptions.Add(new PropertyGroupDescription("Tytul", grouper));

            AlphabeticallyGroupButton.Content = "Wyłącz grupowanie";
            AlphabeticallyGroupButton.Click += new RoutedEventHandler(this.Group_NoneAllSongs);
        }
        private void GroupAlphPlaylist(object sender, RoutedEventArgs e)
        {

            ViewPlaylist.GroupDescriptions.Clear();
            ViewPlaylist.SortDescriptions.Add(new SortDescription("Tytul", ListSortDirection.Ascending));
            AlbumGrouper grouper = new AlbumGrouper();
            ViewAllSongs.GroupDescriptions.Add(new PropertyGroupDescription("Tytul", grouper));

            AlphabeticallyGroupPlaylistButton.Content = "Wyłącz grupowanie";
            AlphabeticallyGroupPlaylistButton.Click += new RoutedEventHandler(this.Group_NonePlaylist);
        }
        private void GroupAlphArtists(object sender, RoutedEventArgs e)
        {
            ViewArtists.GroupDescriptions.Clear();
            ViewArtists.SortDescriptions.Add(new SortDescription("Nazwa", ListSortDirection.Ascending));
            AlbumGrouper grouper = new AlbumGrouper();
            ViewArtists.GroupDescriptions.Add(new PropertyGroupDescription("Nazwa", grouper));


        }
        private void Group_NoneAlbum(object sender, RoutedEventArgs e)
        {

            ViewAlbum.SortDescriptions.Clear();
            ViewAlbum.GroupDescriptions.Clear();


        }
        private void Group_NoneArtist(object sender, RoutedEventArgs e)
        {

            ViewArtists.SortDescriptions.Clear();
            ViewArtists.GroupDescriptions.Clear();


        }
        private void Group_NoneAllSongs(object sender, RoutedEventArgs e)
        {

            ViewAllSongs.SortDescriptions.Clear();
            ViewAllSongs.GroupDescriptions.Clear();
            AlphabeticallyGroupButton.Content = "Grupowanie alfabetyczne";
            AlphabeticallyGroupButton.Click += new RoutedEventHandler(this.GroupAlfpAllSongs);
        }
        private void Group_NonePlaylist(object sender, RoutedEventArgs e)
        {

            ViewPlaylist.SortDescriptions.Clear();
            ViewPlaylist.GroupDescriptions.Clear();
            AlphabeticallyGroupPlaylistButton.Content = "Grupowanie alfabetyczne";
            AlphabeticallyGroupPlaylistButton.Click += new RoutedEventHandler(this.GroupAlphPlaylist);
        }
        private void Filter(object sender, RoutedEventArgs e)
        {
            Album selectedAlbum = wyborWykonawcy.Items.GetItemAt(wyborWykonawcy.SelectedIndex) as Album;

            ViewAlbum.Filter = delegate (object item)
            {

                Album product = item as Album;

                if (product != null)
                {
                    return (product.Artist.Equals(selectedAlbum.Name));
                }

                return false;
            };
        }
        private void FilterNone(object sender, RoutedEventArgs e)
        {
            ViewAlbum.Filter = null;
        }
        private void AlbumListDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (albumListBox.SelectedIndex != -1)
            {

                Album album = albumListBox.Items.GetItemAt(albumListBox.SelectedIndex) as Album;
                selectedAlbum = album;
                DialogResult = true;

                Close();
            }
        }
        private void PlaylistListDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (playlistListBox.SelectedIndex != -1)
            {

                Song selectedSong = playlistListBox.Items.GetItemAt(playlistListBox.SelectedIndex) as Song;
                selectedPlaylist = selectedSong;

                DialogResult = true;
                Close();
            }
        }




        private void RemoveCover(object sender, RoutedEventArgs e)
        {
            if (albumListBox.SelectedIndex != -1)
            {
                Album album = albumListBox.Items.GetItemAt(albumListBox.SelectedIndex) as Album;

                try
                {
                    File.Delete(mediaFolder + "//Okladki//" + album.Name.Remove(album.Name.Length - 2) + ".png");
                    allCollections = new MyData(songList, pathList, albumImageList, artistImageList, nameAlbumImageList, nameArtistImageList);
                    albumListBox.ItemsSource = allCollections.GetAlbums();
                }
                catch (IOException iox)
                {
                    Console.WriteLine(iox.Message);
                }
            }

        }
        private void RemoveImage(object sender, RoutedEventArgs e)
        {
            if (artistListListBox.SelectedIndex != -1)
            {
                Album album = artistListListBox.Items.GetItemAt(artistListListBox.SelectedIndex) as Album;


                try
                {

                    File.Delete(mediaFolder + "//ZdjeciaArtysty//" + album.Name.Remove(album.Name.Length - 2) + ".png");
                    allCollections = new MyData(songList, pathList, albumImageList, artistImageList, nameAlbumImageList, nameArtistImageList);
                    artistListListBox.ItemsSource = allCollections.GetArtists();

                }
                catch (IOException iox)
                {
                    Console.WriteLine(iox.Message);
                }

            }
        }
        public void changeAlbumImage(Image AlbumPoster)
        {
            Album album = albumListBox.Items.GetItemAt(albumListBox.SelectedIndex) as Album;
            album.AlbumImage = AlbumPoster.Source;

            BitmapSource bitmapSource = AlbumPoster.Source as BitmapSource;
            String content = mediaFolder + "//Okladki//";
            String path = album.Name;
            path = path.Remove(path.Length - 2);
            String fileType = ".png";
            String allPath = content + path + fileType;
            MessageBoxResult e = MessageBox.Show(allPath);
            try
            {
                using (var fileStream = new FileStream(allPath, FileMode.Create))
                {
                    BitmapEncoder encoder = new PngBitmapEncoder();
                    encoder.Frames.Add(BitmapFrame.Create(bitmapSource));

                    encoder.Save(fileStream);
                }
            }
            catch (System.UnauthorizedAccessException)
            {

            }
            if (playlistListBox.Items.Count > 0)
            {
                for (int i = 0; i < playlistListBox.Items.Count; i++)
                {
                    List<String> newList = new List<String>();
                    List<String> songsList = new List<String>();

                    Song selectedSong = playlistListBox.Items.GetItemAt(i) as Song;


                    int nk = 0;
                    try
                    {
                        using (var fileStream = new FileStream(mediaFolder + "//Playlist//" + selectedSong.Title + ".txt", FileMode.Open))
                        {
                            StreamReader reader = new StreamReader(fileStream);

                            String line;

                            while ((line = reader.ReadLine()) != null)
                            {
                                if (nk == 1)

                                    songsList.Add(line);
                                if (nk < 3)
                                    nk++;
                                if (nk == 3)
                                    nk = 0;
                            }
                            newList = songsList;
                            reader.Close();

                        }


                        EditPlaylistWindow dlg = new EditPlaylistWindow(allCollections, selectedSong.Title, newList, mediaFolder);


                        dlg.UpdatePlaylist();
                        playlistCollection = new MyData(mediaFolder);
                        playlistListBox.ItemsSource = playlistCollection.GetPlaylists();


                    }

                    catch (System.UnauthorizedAccessException)
                    {

                    }
                }
            }
            DirectoryInfo dir = new DirectoryInfo(mediaFolder + "//Okladki");

            foreach (FileInfo file in dir.GetFiles("*.*", SearchOption.AllDirectories))
            {

                if (file.Extension == ".png")
                {

                    Image n = new Image();
                    n.Source = new BitmapImage(new Uri(file.DirectoryName + "//" + file.Name));

                    albumImageList.Add(n);
                    nameAlbumImageList.Add(file.Name);


                }

            }
            albumCollection = new MyData(songList, pathList, albumImageList, nameAlbumImageList);
            allSongsListBox.ItemsSource = allCollections.GetSongs();
        }
        private void ChangeAlbumCoverClick(object sender, RoutedEventArgs e)
        {
            Album wybrany = albumListBox.Items.GetItemAt(albumListBox.SelectedIndex) as Album;
            FindAlbumImageWindow dlg = new FindAlbumImageWindow(wybrany.Name);
            if (dlg.ShowDialog() == true)
            {
                ((App)Application.Current).UpdateOkladka(dlg.selectedAlbumImage);
            }
        }


        private void PlayAll_Click(object sender, RoutedEventArgs e)
        {
            allSongsListBox.SelectAll();
            songPlaylist = new List<string>();
            sourcePlaylist = new List<string>();
            albumIamge = new List<ImageSource>();
            selected = true;
            if (allSongsListBox.SelectedIndex != -1)
            {
                foreach (var item in allSongsListBox.SelectedItems)
                {



                    Song selectedSong = item as Song;
                    songPlaylist.Add(selectedSong.SecondPath);
                    sourcePlaylist.Add(selectedSong.Path);
                    albumIamge.Add(selectedSong.AlbumImage);

                }
                DialogResult = true;
                Close();

            }


        }

        private void ArtistList_DoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (artistListListBox.SelectedIndex != -1)
            {
                Artist selectedImage = artistListListBox.Items.GetItemAt(artistListListBox.SelectedIndex) as Artist;


                ArtistAlbumsWindow dlg = new ArtistAlbumsWindow(selectedImage.Name, allCollections);

                if (dlg.ShowDialog() == true)
                {
                    if (dlg.selectedAlbumImagePath != null)
                    {
                        selectedAlbum = dlg.selectedAlbum;
                        ((App)Application.Current).UpdateFirst2(dlg.selectedAlbumImagePath, dlg.selectedAlbumImage);
                        DialogResult = true;
                        Close();
                    }
                }
            }
        }


        public void ChangeImage(Image zdjecie)
        {
            Album selectedImage = artistListListBox.Items.GetItemAt(artistListListBox.SelectedIndex) as Album;
            selectedImage.AlbumImage = zdjecie.Source;

            BitmapSource bitmapSource = zdjecie.Source as BitmapSource;
            String content = mediaFolder + "//ZdjeciaArtysty//";
            String path = selectedImage.Name;
            path = path.Remove(path.Length - 2);
            String fileType = ".png";
            String allPath = content + path + fileType;
            MessageBoxResult e = MessageBox.Show(allPath);
            try
            {
                using (var fileStream = new FileStream(allPath, FileMode.Create))
                {
                    BitmapEncoder encoder = new PngBitmapEncoder();
                    encoder.Frames.Add(BitmapFrame.Create(bitmapSource));

                    encoder.Save(fileStream);
                }
            }
            catch (System.UnauthorizedAccessException)
            {

            }
        }
        private void ChangeImage_Click(object sender, RoutedEventArgs e)
        {
            Album selectedImage = artistListListBox.Items.GetItemAt(artistListListBox.SelectedIndex) as Album;
            FindAlbumImageWindow dlg = new FindAlbumImageWindow(selectedImage.Name);
            if (dlg.ShowDialog() == true)
            {
                ((App)Application.Current).UpdateZdjecia(dlg.selectedAlbumImage);
            }
        }

        private void RemovePlaylist_Click(object sender, RoutedEventArgs e)
        {
            if (playlistListBox.SelectedIndex != -1)
            {
                Song song = playlistListBox.Items.GetItemAt(playlistListBox.SelectedIndex) as Song;

                try
                {
                    File.Delete(mediaFolder + "//Playlist//" + song.Title + ".txt");
                    playlistCollection = new MyData(mediaFolder);
                    playlistListBox.ItemsSource = playlistCollection.GetPlaylists();
                }
                catch (IOException iox)
                {
                    Console.WriteLine(iox.Message);
                }
            }
        }

        private void NewPlaylist_Click(object sender, RoutedEventArgs e)
        {
            AddPlaylistWindow dlg = new AddPlaylistWindow(allCollections, mediaFolder);
            if (dlg.ShowDialog() == true)
            {
                playlistCollection = new MyData(mediaFolder);
                playlistListBox.ItemsSource = playlistCollection.GetPlaylists();

            }

        }

        private void EditPlaylist_Click(object sender, RoutedEventArgs e)
        {
            List<String> newPlaylist = new List<String>();

            if (playlistListBox.SelectedIndex != -1)
            {
                Song selectedSong = playlistListBox.Items.GetItemAt(playlistListBox.SelectedIndex) as Song;


                int nk = 0;
                try
                {
                    using (var fileStream = new FileStream(mediaFolder + "//Playlist//" + selectedSong.Title + ".txt", FileMode.Open))
                    {
                        StreamReader reader = new StreamReader(fileStream);

                        String line;
                        
                        while ((line = reader.ReadLine()) != null)
                        {
                            if (nk == 1)

                                newPlaylist.Add(line);
                            if (nk < 3)
                                nk++;
                            if (nk == 3)
                                nk = 0;
                        }
                        reader.Close();

                    }





                    EditPlaylistWindow dlg = new EditPlaylistWindow(allCollections, selectedSong.Title + ".txt", newPlaylist, mediaFolder);
                    if (dlg.ShowDialog() == true)
                    {

                        playlistCollection = new MyData(mediaFolder);
                        playlistListBox.ItemsSource = playlistCollection.GetPlaylists();

                    }
                }

                catch (System.UnauthorizedAccessException)
                {

                }
            }
        }



        private void PlaySelected_Click(object sender, RoutedEventArgs e)
        {
            songPlaylist = new List<string>();
            sourcePlaylist = new List<string>();
            albumIamge = new List<ImageSource>();
            selected = true;
            if (allSongsListBox.SelectedIndex != -1)
            {
                foreach (var item in allSongsListBox.SelectedItems)
                {
                    Song selectedSong = item as Song;
                    songPlaylist.Add(selectedSong.SecondPath);
                    sourcePlaylist.Add(selectedSong.Path);
                    albumIamge.Add(selectedSong.AlbumImage);
                }

                DialogResult = true;
                Close();

            }
        }

    }

}
