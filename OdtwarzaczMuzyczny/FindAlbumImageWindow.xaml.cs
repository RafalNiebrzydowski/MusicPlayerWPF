using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using Google.API.Search;
using System.ComponentModel;
using System.Collections.ObjectModel;
using Google.Apis.Customsearch.v1;
using Google.Apis.Customsearch.v1.Data;
using Newtonsoft.Json;

namespace OdtwarzaczMuzyczny
{
    /// <summary>
    /// Interaction logic for WyszukiwanieOkladki.xaml
    /// </summary>
    public partial class FindAlbumImageWindow : Window
    {
        public Image selectedAlbumImage { get; set; }

        public FindAlbumImageWindow(String a)
        {
            InitializeComponent();
            textSearch.Text = a;
            FindAlbumImages();
        }

        private void btnSubmit_Click(object sender, RoutedEventArgs e)
        {
            FindAlbumImages();
        }
        private async void FindAlbumImages()
        {
            try
            {
             AlbumImageListBox.Items.Clear();


            var client = new System.Net.Http.HttpClient();
            var queryString = HttpUtility.ParseQueryString(string.Empty);
                
            client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", "d01da6c28e184030be164f4d4f9a9236");

            // Request parameters
            queryString["q"] = string.Format("{0}", textSearch.Text.Trim());
            queryString["count"] = textResult.Text;
            queryString["offset"] = "0";
            queryString["mkt"] = "en-us";
            queryString["safeSearch"] = "Moderate";
            queryString["size"] = "medium";
            var uri = "https://api.cognitive.microsoft.com/bing/v5.0/images/search?" + queryString;
                
            var response = await client.GetAsync(uri);
            string content = await response.Content.ReadAsStringAsync();
            var responseType = new { value = new[] { new { contentUrl = "", width = 300, height = 300 } } };
            var results = JsonConvert.DeserializeAnonymousType(content, responseType);
                for (int i = 0; i < results.value.Count(); i++)
                {
                    var firstResult = results.value[i].contentUrl;
                    var request = WebRequest.Create(firstResult);

                    using (var response2 = request.GetResponse())
                    using (var stream = response2.GetResponseStream())
                    {
                        System.Drawing.Image image = System.Drawing.Image.FromStream(stream);
                        System.Windows.Controls.Image img = new System.Windows.Controls.Image();
                        
                        System.Drawing.Bitmap bmp = new System.Drawing.Bitmap(image);
                        IntPtr hBitmap = bmp.GetHbitmap();
                        System.Windows.Media.ImageSource WpfBitmap = System.Windows.Interop.Imaging.CreateBitmapSourceFromHBitmap(hBitmap, IntPtr.Zero, Int32Rect.Empty, BitmapSizeOptions.FromEmptyOptions());

                        img.Source = WpfBitmap;
                        AlbumImageListBox.Items.Add(img);

                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private async Task<BitmapImage> DownloadImage(string url)
        {
            return byteArrayToImage(await new WebClient().DownloadDataTaskAsync(url));
        }

        public BitmapImage byteArrayToImage(byte[] byteArrayIn)
        {
            BitmapImage myBitmapImage;
            using (MemoryStream stream = new MemoryStream(byteArrayIn))
            {
                myBitmapImage = new BitmapImage();
                myBitmapImage.BeginInit();
                myBitmapImage.StreamSource = stream;
                myBitmapImage.CacheOption = BitmapCacheOption.OnLoad;
                myBitmapImage.EndInit();
            }
            return myBitmapImage;
        }
        private void ImageListDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (AlbumImageListBox.SelectedIndex != -1)
            {

                Image newAlbumImage = AlbumImageListBox.Items.GetItemAt(AlbumImageListBox.SelectedIndex) as Image;
                selectedAlbumImage = newAlbumImage;
                DialogResult = true;
                Close();
            }
        }
    }
}
