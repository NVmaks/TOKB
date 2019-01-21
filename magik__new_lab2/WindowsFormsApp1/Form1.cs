using Microsoft.Win32.SafeHandles;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using System.Security.Cryptography;




namespace WindowsFormsApp1
{
    public partial class ZeroSector : Form
    {

        
        byte[] mbrData;
        int check = 3;
        public static byte[] buffer0sector;

        [DllImport("kernel32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        public static extern SafeFileHandle CreateFile(
            string lpFileName,
            [MarshalAs(UnmanagedType.U4)] FileAccess dwDesiredAccess,
            [MarshalAs(UnmanagedType.U4)] FileShare dwShareMode,
            IntPtr lpSecurityAttributes,
            [MarshalAs(UnmanagedType.U4)] FileMode dwCreationDisposition,
            [MarshalAs(UnmanagedType.U4)] FileAttributes dwFlagsAndAttributes,
            IntPtr hTemplateFile);



        public ZeroSector()
        {
            InitializeComponent();
             button3.Enabled = false;
             button2.Enabled = false;
            
            button1.Enabled = false;
        }

        public static string disk_ch;




        void readmbr()
        {
            

            SafeFileHandle handle = CreateFile(
           lpFileName: @"\\.\" + textBox3.Text + ":",
           dwDesiredAccess: FileAccess.Read,
           dwShareMode: FileShare.ReadWrite,
           lpSecurityAttributes: IntPtr.Zero,
           dwCreationDisposition: System.IO.FileMode.OpenOrCreate,
           dwFlagsAndAttributes: FileAttributes.Normal,
           hTemplateFile: IntPtr.Zero);
            using (FileStream disk = new FileStream(handle, FileAccess.Read))
            {
                buffer0sector = new byte[512];
                disk.Read(buffer0sector, 0, 512);
                MessageBox.Show("Нулевой сектор прочитан");
                // button3.Enabled = true;
                //  button2.Enabled = true;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            readmbr();
            int summa = 0;
            for (int i = 384; i < 416; i++)
                summa = summa + buffer0sector[i];
            if (summa == 0)
            {   
                MessageBox.Show("Пароль отсутствует! Задайте новый ");
                Hide();
                disk_ch = textBox3.Text;
                Form Form2 = new Form2();
                Form2.ShowDialog();
                Close();
            }
            button3.Enabled = true;
            button2.Enabled = true;
           
        }


        private void button2_Click(object sender, EventArgs e)
        {
            readmbr();
            SafeFileHandle handle = CreateFile(
            lpFileName: @"\\.\" + textBox3.Text + ":",
            dwDesiredAccess: FileAccess.Read,
            dwShareMode: FileShare.ReadWrite,
            lpSecurityAttributes: IntPtr.Zero,
            dwCreationDisposition: System.IO.FileMode.OpenOrCreate,
            dwFlagsAndAttributes: FileAttributes.Normal,
             hTemplateFile: IntPtr.Zero);

            using (FileStream disk = new FileStream(handle, FileAccess.Read))
            {
                mbrData = new byte[512];
                disk.Read(mbrData, 0, 512);
            }

            handle = CreateFile(
            lpFileName: @"\\.\" + textBox3.Text + ":",
            dwDesiredAccess: FileAccess.Write,
            dwShareMode: FileShare.ReadWrite,
            lpSecurityAttributes: IntPtr.Zero,
            dwCreationDisposition: System.IO.FileMode.OpenOrCreate,
            dwFlagsAndAttributes: FileAttributes.Normal,
            hTemplateFile: IntPtr.Zero);


            string oldpassword = magik(textBox1.Text);
            int error_num = 0;

            for (int j = 0; j < oldpassword.Length; j++)
            {
                if (buffer0sector[384 + j] != (byte)oldpassword[j])
                {

                    error_num++;
                    textBox1.SelectAll();
                    textBox1.Focus();


                }


            }



            if (error_num == 0)
            {
                Hide();
                disk_ch = textBox3.Text;
                Form Form2 = new Form2();
                Form2.ShowDialog();
                Close();
            }
            else { MessageBox.Show("Старый пароль введён неверно!"); check--; label3.Text = "осталось попыток: " + check; }
            if (check == 0) { Close(); }

        }

        static string magik(string input)
        {
            string _crresultChar = "";
            string _line = input;
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

                    if ((_quad[i, j] - 1) < _line.Length)
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
            return _cryptedString;
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            SafeFileHandle handle = CreateFile(
           lpFileName: @"\\.\" + textBox3.Text + ":",
           dwDesiredAccess: FileAccess.Read,
           dwShareMode: FileShare.ReadWrite,
           lpSecurityAttributes: IntPtr.Zero,
           dwCreationDisposition: System.IO.FileMode.OpenOrCreate,
           dwFlagsAndAttributes: FileAttributes.Normal,
            hTemplateFile: IntPtr.Zero);

            using (FileStream disk = new FileStream(handle, FileAccess.Read))
            {
                buffer0sector = new byte[512];
                disk.Read(buffer0sector, 0, 512);
            }

            handle = CreateFile(
            lpFileName: @"\\.\" + textBox3.Text + ":",
            dwDesiredAccess: FileAccess.Write,
               dwShareMode: FileShare.ReadWrite,
               lpSecurityAttributes: IntPtr.Zero,
               dwCreationDisposition: System.IO.FileMode.OpenOrCreate,
               dwFlagsAndAttributes: FileAttributes.Normal,
               hTemplateFile: IntPtr.Zero);

            using (FileStream disk = new FileStream(handle, FileAccess.Write))
            {

            

                for (int i = 0; i < 32; i++)
                    buffer0sector[384 + i] = 0;
           

                disk.Write(buffer0sector, 0, 512);
                button2.Enabled = false;
                MessageBox.Show("Пароль удалён из MBR");



            }
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            if (textBox3.Text != "") button1.Enabled = true;
        }
    }
}
