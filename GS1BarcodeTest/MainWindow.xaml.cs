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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Drawing;
using System.IO;

namespace GS1BarcodeTest
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string DatenbezeichnerGLN = "01";
            string DatenbezeichnerProduktionsdatum = "11";
            string DatenbezeichnerChargenNummer = "10";
            string Datenbezeichner = "00";
            string GLN = "4013493000002";
            string GLNBasis = "4013493";
            string Reserveziffer = "1";
            string Produktionsdatum = "311218";
            string ChargenNummer = "151218";
            string NVEDaten = "000000001";
            string Barcode1 = DatenbezeichnerGLN + GLN + DatenbezeichnerProduktionsdatum + Produktionsdatum + DatenbezeichnerChargenNummer + ChargenNummer;

            string NVERoh = Reserveziffer + GLNBasis + NVEDaten;
            int NVERohdatenINT = Int32.Parse(NVERoh);
            int Prüfziffer = PrüfzifferBerechnen(NVERohdatenINT);

            
            BarcodeLib.Barcode b = new BarcodeLib.Barcode();
            System.Drawing.Image img = b.Encode(BarcodeLib.TYPE.CODE128, Barcode1, System.Drawing.Color.Black, System.Drawing.Color.White, 800, 240);
            BarcodeIMG.Source = ToWpfImage(img);
        }

        public BitmapImage ToWpfImage(System.Drawing.Image img)
        {
            MemoryStream ms = new MemoryStream();  // no using here! BitmapImage will dispose the stream after loading
            img.Save(ms, System.Drawing.Imaging.ImageFormat.Bmp);

            BitmapImage ix = new BitmapImage();
            ix.BeginInit();
            ix.CacheOption = BitmapCacheOption.OnLoad;
            ix.StreamSource = ms;
            ix.EndInit();
            return ix;
        }
        public int PrüfzifferBerechnen(int[] NVERohdaten)
        {

            return 0;
        }

    }
}
