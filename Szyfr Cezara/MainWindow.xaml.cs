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



        private string Szyfruj(string tekst, int klucz)
        {
            StringBuilder zaszyfrowanyTekst = new StringBuilder();

            foreach (char znak in tekst)
            {
                if (char.IsLetter(znak))
                {
                    bool jestDuzaLitera = char.IsUpper(znak);
                    string alfabet = jestDuzaLitera ? "ABCDEFGHIJKLMNOPQRSTUVWXYZĄĆĘŁŃÓŚŹŻ" : "abcdefghijklmnopqrstuvwxyząćęłńóśźż";

                    int pozycja = alfabet.IndexOf(znak);
                    if (pozycja >= 0)
                    {
                        int nowaPozycja = (pozycja + klucz) % alfabet.Length;
                        zaszyfrowanyTekst.Append(alfabet[nowaPozycja]);
                    }
                }
                else
                {
                    zaszyfrowanyTekst.Append(znak);
                }
            }

            return zaszyfrowanyTekst.ToString();
        }

        private string Deszyfruj(string zaszyfrowanyTekst, int klucz)
        {
            StringBuilder odszyfrowanyTekst = new StringBuilder();

            foreach (char znak in zaszyfrowanyTekst)
            {
                if (char.IsLetter(znak))
                {
                    bool jestDuzaLitera = char.IsUpper(znak);
                    string alfabet = jestDuzaLitera ? "ABCDEFGHIJKLMNOPQRSTUVWXYZĄĆĘŁŃÓŚŹŻ" : "abcdefghijklmnopqrstuvwxyząćęłńóśźż";

                    int pozycja = alfabet.IndexOf(znak);
                    if (pozycja >= 0)
                    {
                        int nowaPozycja = (pozycja - klucz + alfabet.Length) % alfabet.Length;
                        odszyfrowanyTekst.Append(alfabet[nowaPozycja]);
                    }
                }
                else
                {
                    odszyfrowanyTekst.Append(znak);
                }
            }

            return odszyfrowanyTekst.ToString();
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
            if (ComboBoxKey2.SelectedItem != null)
            {
                if (int.TryParse(((ComboBoxItem)ComboBoxKey2.SelectedItem).Content.ToString(), out int wybranyKlucz))
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

