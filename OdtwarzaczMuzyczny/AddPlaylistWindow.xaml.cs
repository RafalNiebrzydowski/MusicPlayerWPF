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
    /// Interaction logic for DodawaniePlaylist.xaml
    /// </summary>
    public partial class AddPlaylistWindow : Window
    {
        private static String mediaFolder;
        public List<string> zrodlo { get; set; }
        public List<string> Song { get; set; }
        public List<ImageSource> AlbumPoster { get; set; }
        public bool zaznaczanie { get; set; }
        public AddPlaylistWindow(MyData a,String zrodloPlaylisty)
        {
            InitializeComponent();
            playlistListBox.ItemsSource= a.GetSongs();
            mediaFolder = zrodloPlaylisty;
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            Song = new List<string>();
            zrodlo = new List<string>();
            AlbumPoster = new List<ImageSource>();
           
            Console.WriteLine("zapiasano");
            zaznaczanie = true;
           
            if (playlistListBox.SelectedIndex != -1)
            {
                foreach (var item in playlistListBox.SelectedItems)
                {

                    Song wybrany = item as Song;
                    Song.Add(wybrany.SecondPath);
                    zrodlo.Add(wybrany.Path);
                    AlbumPoster.Add(wybrany.AlbumImage);

                }
                try
                {
                    using (var fileStream = new FileStream(mediaFolder+"//Playlist//" + nazwa.Text + ".txt", FileMode.Create, FileAccess.ReadWrite))
                    {
                        StreamWriter writer = new StreamWriter(fileStream);
                       
                        for (int i = 0; i < Song.Count; i++)
                        {
                         
                           writer.WriteLine(zrodlo.ElementAt(i));
                           writer.WriteLine(Song.ElementAt(i));
                           writer.WriteLine(AlbumPoster.ElementAt(i));
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

       
    }
}
