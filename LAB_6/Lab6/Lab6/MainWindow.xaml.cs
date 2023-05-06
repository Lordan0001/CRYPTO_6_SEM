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

namespace Lab6
{
    public partial class MainWindow : Window
    {
        string original_alphabet = "abcdefghijklmnopqrstuvwxyz";
        Runner L, M, R;
        Deflector reflector;

        public MainWindow()
        {
            original_alphabet.ToCharArray();
            InitializeComponent();
            L = new Runner
            {
                Alphabet = new Char[] {'e', 'k', 'm', 'f', 'l', 'g', 'd', 'q', 'v', 'z', 'n', 't', 'o', 'w', 'y', 'h', 'x', 'u', 's', 'p', 'a', 'i', 'b', 'r', 'c', 'j',
},
                Shift = 1
            };
            M = new Runner
            {
                Alphabet = new Char[] { 'a', 'j', 'd', 'k', 's', 'i', 'r', 'u','x',
                                        'b', 'l', 'h', 'w', 't', 'm', 'c', 'q','g',
                                        'z', 'n', 'p', 'y', 'f', 'v','o','e'},
                Shift = 0,

            };
            R = new Runner
            {
                Alphabet = new Char[] {'b', 'd', 'f', 'h', 'j', 'l', 'c', 'r', 't', 'x', 'v', 'z', 'n', 'y', 'e', 'i', 'w', 'g', 'a', 'k', 'm', 'u', 's', 'q', 'o'},
                Shift = 1,
            };

            reflector = new Deflector();
            reflector.Alphabet = new Dictionary<char, char> { { 'a', 'y' }, { 'b', 'r' }, { 'c', 'u' }, { 'd', 'h' }, { 'e', 'q' }, { 'f', 's' }, { 'g', 'l' },
                                                              { 'i', 'p' }, { 'j', 'x' }, { 'k', 'n' }, { 'm', 'o' }, { 't', 'z' }, { 'v', 'w' }};
        }

        private void Encrypt_Click(object sender, RoutedEventArgs e)
        {
            string original_message = Orig_message_input.Text.ToLower().Replace(" ","");
            char encrypt_symbol;
            string encrypt_message = "";

            L.PickStartPosition(Start_position_Rot_1.Items[Start_position_Rot_1.SelectedIndex].ToString());
            M.PickStartPosition(Start_position_Rot_2.Items[Start_position_Rot_2.SelectedIndex].ToString());
            R.PickStartPosition(Start_position_Rot_3.Items[Start_position_Rot_3.SelectedIndex].ToString());

            for (int i = 0; i < original_message.Length; i++)
            {
                encrypt_symbol = R.Alphabet[original_alphabet.IndexOf(original_message[i])];
                encrypt_symbol = M.Alphabet[original_alphabet.IndexOf(encrypt_symbol)];
                encrypt_symbol = L.Alphabet[original_alphabet.IndexOf(encrypt_symbol)];
                char encrypt_symbolFromRefl;
                if (!reflector.Alphabet.TryGetValue(encrypt_symbol, out encrypt_symbolFromRefl))
                    encrypt_symbol = reflector.Alphabet.First(key => key.Value == encrypt_symbol).Key;
                else
                    encrypt_symbol = encrypt_symbolFromRefl;
                encrypt_symbol = L.Alphabet[original_alphabet.IndexOf(encrypt_symbol)];
                encrypt_symbol = M.Alphabet[original_alphabet.IndexOf(encrypt_symbol)];
                encrypt_message = encrypt_message + R.Alphabet[original_alphabet.IndexOf(encrypt_symbol)];
                L.DoShift(M.DoShift(R.DoShift(0)));
            }
            Encrypt_Output.Text = encrypt_message;
        }

    }
}
