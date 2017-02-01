using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace OdtwarzaczMuzyczny
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public void UpdateFirst(string a,int nr, ImageSource n)
        {
            foreach (Window w in Windows)
            {
                if (w is MainWindow)
                {
                    ((MainWindow)w).ChangeSong(a,nr,n);
                }
            }
        }
        public void UpdateFirst2(string b,ImageSource a)
        {
            foreach (Window w in Windows)
            {
                if (w is MainWindow)
                {
                    ((MainWindow)w).ChangeSource(b,a);
                }
            }
        }
      
        public void UpdateList(List<string> a,List<string> b,List<ImageSource>c)
        {
            foreach (Window w in Windows)
            {
                if (w is MainWindow)
                {
                    ((MainWindow)w).ChangeSongAndSourceGroup(a, b,c);
                }
            }
        }
        public void UpdateOkladka(Image a)
        {
            foreach (Window w in Windows)
            {
                if (w is MainWindow)
                {
                   // ((MainWindow)w).zmienOkladke2(a);
                }
                if (w is CollectionMusicWindow)
                {
                    ((CollectionMusicWindow)w).changeAlbumImage(a);
                }
            }
        }
        public void UpdateZdjecia(Image a)
        {
            foreach (Window w in Windows)
            {
               
                if (w is CollectionMusicWindow)
                {
                    ((CollectionMusicWindow)w).ChangeImage(a);
                }
            }
        }
        public void UpdatePlayliste(string a,ImageSource b)
        {
            foreach (Window w in Windows)
            {

                if (w is MainWindow)
                {
                    ((MainWindow)w).SynchPlaylist(a,b);
                }
            }
        }
    }
}
