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

    public partial class Form1 : Form
    {
        string _line = null;
        int i = 3;
        string _passLine;

        public Form1()
        {
            InitializeComponent();
            button1.Enabled = false;

        }

        private void label1_Click(object sender, EventArgs e)
        {
            
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
           
           if (textBox1.Text.Length != 0) { button1.Enabled = true; _line = textBox1.Text.ToUpper().Replace(" ", ""); }
           else button1.Enabled = false;

        }



        private void button1_Click(object sender, EventArgs e)
        {
            FileStream file1 = new FileStream("password.txt", FileMode.OpenOrCreate);

            StreamReader reader = new StreamReader(file1);
            _passLine = reader.ReadToEnd();

            reader.Close();
            string _crresultChar = "";
            string _randomLine = "нуkы6мцc5вlт2шajдdрэ1лrъефx3ogbeжохfа0hipnп97и8qкvщtючьzmсsзywuг4йб";
            string _cryptedString = "";
            string _result = "";
            string _resultChar = "";
             Random _rand = new Random();
            int _d = (int)Math.Ceiling(Math.Sqrt(_line.Length));
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
             }
            for (int j = 0; j < _d; j++)
            {

                for (int i = 0; i < _d; i++)
                {

                    if ((_quad[i, j] - 1) < _line.Length )
                    {

                        _cryptedString += _line[_quad[i, j] - 1]; _crresultChar += _line[_quad[i, j] - 1];
                      


                    }
                    else
                    {
                        char _randomChar = _randomLine[j];
                        _cryptedString += _randomChar; _crresultChar += _randomChar;
                    }


                }



            }
           


                      
          
             

              if (_passLine.Length == 0)
               {
                   Hide();
                   Form Form5 = new Form5();
                   Form5.ShowDialog();
                   Close();
               }
                else
                  if (_cryptedString == _passLine)
               {
                   Hide();
                   Form Form2 = new Form2();
                   Form2.ShowDialog();
                   Close();
               }


              else { textBox1.Text = ""; i--; label2.Text = "Осталось попыток " + i + "."; } 
            if (i == 1) { Form Form3 = new Form3(); Form3.ShowDialog(); }
            if (i == 0) Close();

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
