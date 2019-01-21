using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form2 : Form
    {
        string _newPass;
        string _confimNewPass;
        //string _randomLine = "нуkы6мцc5вlт2шajдdрэ1лrъефx3ogbeжохfа0hipnп97и8qкvщtючьzmсsзywuг4йб";
        string _cryptedString = "";
        string _result = "";
        string _resultChar = "";
        string _crresultChar = "";
       Random _rand = new Random();


        public Form2()
        {
            InitializeComponent();
            button1.Enabled = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {

            
            if (_newPass == _confimNewPass)
                {
                int _d = (int)Math.Ceiling(Math.Sqrt(_newPass.Length));
                if (_d % 2 != 1)
                    _d++;
                int[,] _quad = new int[_d, _d];
                for (int j = 0; j < _d; j++)
                {
                    for (int i = 0; i < _d; i++)
                    {
                        _quad[i, j] = _d * (((i + 1) + (j + 1) - 1 + (_d / 2)) % _d) + (((i + 1) + 2 * (j + 1) - 2) % _d) + 1;
                        _resultChar = (_quad[i, j].ToString() + " ");
                        _result += _resultChar;



                    }
                    _result += "\n";
                }
                for (int j = 0; j < _d; j++)
                {

                    for (int i = 0; i < _d; i++)
                    {

                        if ((_quad[i, j] - 1) < _newPass.Length)
                        {
                            _cryptedString += _newPass[_quad[i, j] - 1]; _crresultChar += _newPass[_quad[i, j] - 1];
                        }
                        else
                        {
                            char _randomChar = _randomLine[j];
                            _cryptedString += _randomChar; _crresultChar += _randomChar;
                        }

                    }

                 
                   
                }

                StreamWriter writer = new StreamWriter("password.txt"); //указываем путь к файлу, а не поток 

                    writer.Write(_crresultChar);
                    writer.Close();
                }
            Form Form4 = new Form4();
            Form4.ShowDialog();
            Close();



        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            _newPass = textBox1.Text.ToUpper().Replace(" ", "");
           
           

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
           
            _confimNewPass = textBox2.Text.ToUpper().Replace(" ", "");
            if (textBox2.Text == textBox1.Text & textBox1.Text.Length >= 4) button1.Enabled = true;
            else button1.Enabled = false;


        }

        
    }
}
