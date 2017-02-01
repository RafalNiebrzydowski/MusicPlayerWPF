using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace OdtwarzaczMuzyczny
{
    public class Album :  INotifyPropertyChanged
    {
        private ImageSource albumImage;
        private String name;
        private String path;
        private String artist;
        private String genre;
        private List<Song> songsList { get; set; }
        public event PropertyChangedEventHandler PropertyChanged;

        public Album() { }


        public ImageSource AlbumImage
        {
            get
            {
                return albumImage;
            }
            set
            {
                albumImage = value;
                OnPropertyChanged("AlbumOkladka");
            }
        }
        public String Name
        {
            get
            {
                return name;
            }
            set
            {
                name = value;
                OnPropertyChanged("Nazwa");
            }
        }
        public String Artist
        {
            get
            {
                return artist;
            }
            set
            {
                artist = value;
                OnPropertyChanged("Artist");
            }
        }
        public String Path
        {
            get
            {
                return path;
            }
            set
            {
                path = value;
                OnPropertyChanged("Sciezka");
            }
        }
        public String Genre
        {
            get
            {
                return genre;
            }
            set
            {
                genre = value;
                OnPropertyChanged("Gatunek");
            }
        }

        public string this[string columnName]
        {
            get
            {

                return null;
            }
        }

        public void OnPropertyChanged(string property)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(property));
        }
    }
}
