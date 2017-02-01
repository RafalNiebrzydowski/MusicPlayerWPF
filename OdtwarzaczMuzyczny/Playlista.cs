using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OdtwarzaczMuzyczny
{
    class Playlista
    {
        private String name;
        private String title;
        private String duration;
        private String artist;
        private String path;
        private String secondPath;
        private int nr;
        private List<Song> songList { get; set; }
        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged(string property)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(property));
        }
        public Playlista() { }
        public string Error { get { return null; } }
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
    }
}
