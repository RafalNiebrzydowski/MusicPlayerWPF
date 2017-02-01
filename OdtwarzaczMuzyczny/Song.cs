using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace OdtwarzaczMuzyczny
{
    public class Song : INotifyPropertyChanged
    {
        private String title;
        private ImageSource albumImage;
        private String duration;
        private String artist;
        private String path;
        private String secondPath;
        private int nr;
        public event PropertyChangedEventHandler PropertyChanged;

       

        public Song() { }

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
        public String Title
        {
            get
            {
                return title;
            }
            set
            {
                title = value;
                OnPropertyChanged("Tytul");
            }
        }
        public String Duration
        {
            get
            {
                return duration;
            }
            set
            {
                duration = value;
                OnPropertyChanged("Czas");
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
        public String SecondPath
        {
            get
            {
                return secondPath;
            }
            set
            {
                secondPath = value;
                OnPropertyChanged("Sciezka2");
            }
        }
        public int Nr
        {
            get
            {
                return nr;
            }
            set
            {
                nr = value;
                OnPropertyChanged("Nr");
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
