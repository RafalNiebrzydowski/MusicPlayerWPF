using System;
using System.Collections.Generic;
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
    /// Interaction logic for AlbumyWykonawcy.xaml
    /// </summary>
    public partial class ArtistAlbumsWindow : Window
    {
        
        public String selectedAlbumImagePath { get; set; }
        public Album selectedAlbum { get; set; }
        public ImageSource selectedAlbumImage { get; set; }
        public ArtistAlbumsWindow(string Artist, MyData albumy)
        {
          
            
            InitializeComponent();
            
            albumList.ItemsSource = albumy.GetArtistAlbum(Artist);


        }
        private void ImageListDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (albumList.SelectedIndex != -1)
            {

                Album selectedAlbum = albumList.Items.GetItemAt(albumList.SelectedIndex) as Album;
                selectedAlbumImagePath = selectedAlbum.Path;
                selectedAlbumImage = selectedAlbum.AlbumImage;
                this.selectedAlbum = selectedAlbum;
                DialogResult = true;

                Close();
            }
        }
    }
}
