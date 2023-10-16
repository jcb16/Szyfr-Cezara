using Microsoft.VisualBasic;
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
using static System.Formats.Asn1.AsnWriter;

namespace Szyfr_Cezara
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            textBoxPlainText.PreviewMouseDown += TextBox_MouseDown;
            EncryptedText.PreviewMouseDown += TextBox_MouseDown;
        }

        private bool isFirstClickPlainText = true;
        private bool isFirstClickEncryptedText = true;

        private void TextBox_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (sender is TextBox textBox)
            {
                if (textBox == textBoxPlainText && isFirstClickPlainText)
                {
                    textBox.Clear();
                    isFirstClickPlainText = false;
                }
                else if (textBox == EncryptedText && isFirstClickEncryptedText)
                {
                    textBox.Clear();
                    isFirstClickEncryptedText = false;
                }
            }
        }


        //private string Szyfruj(string tekst, int klucz)
        //{
        //    StringBuilder zaszyfrowanyTekst = new StringBuilder();

        //    foreach (char znak in tekst)
        //    {
        //        if (char.IsLetter(znak))
        //        {
        //            char startZakresu = char.IsUpper(znak) ? 'A' : 'a';
        //            int offset = char.IsUpper(znak) ? 'Z' : 'z';

        //            // Szyfrowanie znaku
        //            int zaszyfrowanyZnak = ((znak - startZakresu + klucz) % (offset - startZakresu + 1)) + startZakresu;

        //            if (zaszyfrowanyZnak < startZakresu)
        //            {
        //                zaszyfrowanyZnak += (offset - startZakresu + 1);
        //            }

        //            zaszyfrowanyTekst.Append((char)zaszyfrowanyZnak);
        //        }
        //        else
        //        {
        //            zaszyfrowanyTekst.Append(znak);
        //        }
        //    }

        //    return zaszyfrowanyTekst.ToString();
        //}

        private string Szyfruj(string tekst, int klucz)
        {
            StringBuilder zaszyfrowanyTekst = new StringBuilder();

            foreach (char znak in tekst)
            {
                if (char.IsLetter(znak))
                {
                    char startZakresu = char.IsUpper(znak) ? 'A' : 'a';
                    char offset = char.IsUpper(znak) ? 'Z' : 'z';

                    if (znak >= 'Ą' && znak <= 'Ź')
                    {
                        startZakresu = 'Ą';
                        offset = 'Ź';
                    }

                    // Szyfrowanie znaku
                    int zaszyfrowanyZnak = ((znak - startZakresu + klucz) % (offset - startZakresu + 1)) + startZakresu;

                    if (zaszyfrowanyZnak < startZakresu)
                    {
                        zaszyfrowanyZnak += (offset - startZakresu + 1);
                    }

                    zaszyfrowanyTekst.Append((char)zaszyfrowanyZnak);
                }
                else
                {
                    zaszyfrowanyTekst.Append(znak);
                }
            }

            return zaszyfrowanyTekst.ToString();
        }





        private string Deszyfruj(string tekst, int klucz)
        {
            return Szyfruj(tekst, 26 - klucz);
        }


        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string tekstWejsciowy = textBoxPlainText.Text;
            if (comboBoxKey.SelectedItem != null)
            {
                if (int.TryParse(((ComboBoxItem)comboBoxKey.SelectedItem).Content.ToString(), out int wybranyKlucz))
                {
                    labelResult.Content = "";
                    labelResult.Content = Szyfruj(tekstWejsciowy, wybranyKlucz);
                }
                else
                {
                    MessageBox.Show("Nie można przekonwertować wybranego klucza na liczbę całkowitą.");
                }
                
            }
            else
            {
                MessageBox.Show("Wybierz klucz szyfrowania z ComboBoxa.");
            }

            
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            string szyfrogram = EncryptedText.Text;
            if (comboBoxKey.SelectedItem != null)
            {
                if (int.TryParse(((ComboBoxItem)comboBoxKey.SelectedItem).Content.ToString(), out int wybranyKlucz))
                {
                    labelResult2.Content = "";
                    labelResult2.Content = Deszyfruj(szyfrogram, wybranyKlucz);
                }
                else
                {
                    MessageBox.Show("Nie można przekonwertować wybranego klucza na liczbę całkowitą.");
                }

            }
            else
            {
                MessageBox.Show("Wybierz klucz szyfrowania z ComboBoxa.");
            }
        }
    }


}

