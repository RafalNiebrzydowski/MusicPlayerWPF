using System;
using System.Collections.Generic;
using System.IO;
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
using System.Windows.Shapes;

namespace OdtwarzaczMuzyczny
{
    /// <summary>
    /// Interaction logic for EdytowaniePlaylisty.xaml
    /// </summary>
    public partial class EditPlaylistWindow : Window
    {
        private static String mediaFolder;
        public List<string> sourceList { get; set; }
        public List<string> songList { get; set; }
        public List<ImageSource> albumImageSourceList { get; set; }

        public bool selected { get; set; }
        public EditPlaylistWindow(MyData a,String name,List<string> list,String sourcePlaylist)
        {
            InitializeComponent();
            this.Name.Text = name.Remove(name.Length-4);
            playlistListBox.ItemsSource = a.GetSongs();
            mediaFolder = sourcePlaylist;
            
            for (int i = 0; i < playlistListBox.Items.Count; i++)
            {Song song = playlistListBox.Items.GetItemAt(i) as Song;
            for (int s = 0; s < list.Count; s++)
            {
               
                if (song.SecondPath.Equals(list.ElementAt(s)))
                {

                    playlistListBox.SelectedItems.Add(playlistListBox.Items[i]);
                    s = list.Count;
                }
            }
            }
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Edit_Click(object sender, RoutedEventArgs e)
        {
            songList = new List<string>();
            sourceList = new List<string>();
            albumImageSourceList = new List<ImageSource>();
            selected = true;
           
            if (playlistListBox.SelectedIndex != -1)
            {
                foreach (var item in playlistListBox.SelectedItems)
                {

                    Song song = item as Song;
                    songList.Add(song.SecondPath);
                    sourceList.Add(song.Path);
                    albumImageSourceList.Add(song.AlbumImage);

                }
                try
                {
                    using (var fileStream = new FileStream(mediaFolder+"//Playlist//" + Name.Text + ".txt", FileMode.Create, FileAccess.ReadWrite))
                    {
                        StreamWriter writer = new StreamWriter(fileStream);
                       
                        for (int i = 0; i < songList.Count; i++)
                        {
                         
                           writer.WriteLine(sourceList.ElementAt(i));
                           writer.WriteLine(songList.ElementAt(i));
                           writer.WriteLine(albumImageSourceList.ElementAt(i));
                        }
                        writer.Flush(); 

                    }
             

                    }
                
                catch (System.UnauthorizedAccessException)
                {

                }


                DialogResult = true;
                Close();
            }
        
        }
        public void UpdatePlaylist()
        {
            songList = new List<string>();
            sourceList = new List<string>();
            albumImageSourceList = new List<ImageSource>();
            selected = true;

            if (playlistListBox.SelectedIndex != -1)
            {
                foreach (var item in playlistListBox.SelectedItems)
                {
                    Song song = item as Song;
                    songList.Add(song.SecondPath);
                    sourceList.Add(song.Path);
                    albumImageSourceList.Add(song.AlbumImage);

                }
                try
                {
                    using (var fileStream = new FileStream(mediaFolder + "//Playlist//" + Name.Text + ".txt", FileMode.Create, FileAccess.ReadWrite))
                    {
                        StreamWriter writer = new StreamWriter(fileStream);

                        for (int i = 0; i < songList.Count; i++)
                        {

                            writer.WriteLine(sourceList.ElementAt(i));
                            writer.WriteLine(songList.ElementAt(i));
                            writer.WriteLine(albumImageSourceList.ElementAt(i));
                        }
                        writer.Flush();

                    }


                }

                catch (System.UnauthorizedAccessException)
                {

                }


                Close();
            }
        }
    }
}
