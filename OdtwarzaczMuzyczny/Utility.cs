using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Controls;
using System.Collections.ObjectModel;

namespace OdtwarzaczMuzyczny
{
    static class Utility
    {
        

        public static void CheckExistDirectories(String path)
        {
            if (!Directory.Exists(path + "\\Okladki"))
            {
                Directory.CreateDirectory(path + "\\Okladki");
            }
            if (!Directory.Exists(path + "\\ZdjeciaArtysty"))
            {
                Directory.CreateDirectory(path + "\\ZdjeciaArtysty");
            }
            if (!Directory.Exists(path + "\\Playlist"))
            {
                Directory.CreateDirectory(path + "\\Playlist");
            }
        }

        public static void LoadingImages(String path, List<Image> imageList, List<String> nameList)
        {
            DirectoryInfo dir = new DirectoryInfo(path);

            foreach (FileInfo file in dir.GetFiles("*.*", SearchOption.AllDirectories))
            {

                if (file.Extension == ".png")
                {

                    Image image = new Image();
                    image.Source = new BitmapImage(new Uri(file.DirectoryName + "//" + file.Name));
                    imageList.Add(image);
                    nameList.Add(file.Name);


                }

            }
        }
        public static void LoadingMp3(String path, List<String> directionList, List<String> nameList)
        {
            DirectoryInfo dir = new DirectoryInfo(path);
            foreach (FileInfo file in dir.GetFiles("*.*", SearchOption.AllDirectories))
            {
                if (file.Extension == ".mp3")
                {
                    directionList.Add(file.DirectoryName);
                    nameList.Add(file.Name);
                   

                }
            }
        }
        
    }
}
