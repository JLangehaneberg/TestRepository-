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
            string DatenbezeichnerNVE = "00";
            string DatenbezeichnerProduktionsdatum = "11";
            string DatenbezeichnerChargenNummer = "10";
            string DatenbezeichnerMHD = "15";
            string DatenbezeichnerMenge = "37";
            string Datenbezeichner = "00";
            string DatenbezeichernGTIN = "02";
            string EANGTIN = "4260046693352";
            string GLNBasis = "4013493";
            string Reserveziffer = "3";
            string GLN = "4013493000002";
            string Produktionsdatum = "311218";
            string VarNummer = "000000001";
            string ChargenNummer = "181231";
            string Menge = "0095";
            string MHD = "190131";

            string NVERohdaten = Reserveziffer + GLNBasis + VarNummer;
            string Barcode1 = DatenbezeichernGTIN + EANGTIN + DatenbezeichnerMHD + MHD + DatenbezeichnerMenge + Menge;
            string Barcode2 = DatenbezeichnerNVE + NVERohdaten + PrüfzifferBerechnen(NVERohdaten) + DatenbezeichnerChargenNummer + ChargenNummer;

            //int NVERohdatenINT = Int32.Parse(NVERoh);
            //int Prüfziffer = PrüfzifferBerechnen(NVERohdatenINT);


            BarcodeLib.Barcode b = new BarcodeLib.Barcode();
            System.Drawing.Image img = b.Encode(BarcodeLib.TYPE.CODE128, Barcode1, System.Drawing.Color.Black, System.Drawing.Color.White, 800, 240);
            System.Drawing.Image img2 = b.Encode(BarcodeLib.TYPE.CODE128, Barcode2, System.Drawing.Color.Black, System.Drawing.Color.White, 800, 240);
            BarcodeIMG2.Source = ToWpfImage(img2);
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
        public int PrüfzifferBerechnen(string NVERohdaten)
        {
            char[] arrayIteration = NVERohdaten.ToArray();
            
            int Produktsumme = 0;
            foreach (char i in arrayIteration)
            {
                int Zahl = Convert.ToInt32(char.GetNumericValue(i));
                if (istGerade(Zahl))
                {
                    Produktsumme = Produktsumme + Zahl * 3;
                    // * 3 Rechnen laut GS1 Standard
                }
                else
                {
                    Produktsumme = Produktsumme + Zahl * 1;
                }
               

            }
            double Modul = Produktsumme / 10.0;
            //Modulo 10 Berechnung Gemäß GS1 STandart; 
            int Zwischensumme = Convert.ToInt32(Math.Ceiling(Modul));
            int ReturnValue = (Zwischensumme * 10) - Produktsumme;
            return ReturnValue;
        }
        private bool istGerade(int Zahl)
        {
            return Zahl % 2 == 0;
        }

    }
}
