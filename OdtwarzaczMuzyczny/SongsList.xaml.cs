using System;

using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text.RegularExpressions;
using System.IO;

namespace OdtwarzaczMuzyczny
{
    /// <summary>
    /// Interaction logic for Lista.xaml
    /// </summary>
    public partial class SongsList : Window
    {
        DataSongsToPlay data;
        List<String> songsList = new List<String>();
        List<String> sourceList = new List<String>();
       
        public String selectedSong { get; set; }
        public String selectedPath { get; set; }
        public ImageSource selectedAlbumImage { get; set; }
        public int nrSong { get; set; }
        public SongsList(List<String> songsList, List<string> sourceList, int nr)
        {
            InitializeComponent();
            List<String> nameImage = new List<String>();
            List<Image> albumImage = new List<Image>();
            this.songsList = songsList;
            this.sourceList = sourceList;
            try
            {
                DirectoryInfo directionAlbumImage = new DirectoryInfo("C://Users//Rafal//Music//Okladki");
                Utility.LoadingImages("C://Users//Rafal//Music//Okladki", albumImage, nameImage);

                data = new DataSongsToPlay(this.songsList, this.sourceList, albumImage, nameImage);
                songListBox.ItemsSource = data.GetSongs();
                songListBox.SelectedIndex = nr;
            }
            catch (DirectoryNotFoundException e)
            {
                MessageBox.Show("Nie znaleziono scieżki C://Users//Rafał//Music//Okladki");
            }

        }

        private ListCollectionView View
        {
            get
            {
                return (ListCollectionView)CollectionViewSource.GetDefaultView(data.GetSongs());
            }
        }
        
        private void ImageListDoubleClick(object sender, MouseEventArgs e)
        {
            if (songListBox.SelectedIndex != -1)
            {
                Song read = songListBox.Items.GetItemAt(songListBox.SelectedIndex) as Song;
                selectedSong = read.SecondPath;
                nrSong = read.Nr;
                selectedAlbumImage = read.AlbumImage;
                DialogResult = true;


            }
        }
        public void AddSongs(List<String> songsList)
        {
            this.songsList = songsList;
        }




    }
}
