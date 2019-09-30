using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CustomFontTool
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        

        //字符串转bitmap
        private Bitmap StringToBitmap(String str)
        {
            StringFormat sf = new StringFormat();

            sf.Alignment = StringAlignment.Center;
            sf.LineAlignment = StringAlignment.Near;

            Bitmap bmp = new Bitmap(16, 16);
            Graphics g = Graphics.FromImage(bmp);
            Font f = new Font("宋体", 16 * 3 / 4);
            g.DrawString(str, f, Brushes.Black, new Rectangle(0, 0, 16, 16), sf);

            return bmp;
        }

        //bitmap转16进制点阵数
        //暂用列行式
        private byte[,] BitmapToCharArray(Bitmap bmp)
        {
            int row;
            int y = bmp.Height;

            if (y % 8 > 0)
                row = y / 8 + 1;
            else
                row = y / 8;

            byte[,] dat = new byte[row, bmp.Width];//创建数组

            for( int i = 0; i < row; i ++)//遍历每一行的“列行式”数据
            {
                //遍历每一列8像素点
                for( int x = 0; x < bmp.Width; x ++)
                {
                    for( int line = 0; line < 8; line ++)
                    {
                        if (bmp.GetPixel(x, i * 8 + line) == Color.FromArgb(0, 0, 0))
                            dat[i, x] |= (byte)(1<<(line));
                    }
                }
            }

            return dat;
        }

        //Byte二维数组转成单汉字的字符串
        private string ByteDArrayToString(byte [,]dat)
        {
            StringBuilder str = new StringBuilder();
            int num = 0;

            for (int i = 0; i < dat.GetLength(0); i++)
            {
                for (int j = 0; j < dat.GetLength(1); j++)
                {                    
                    str.Append("0x" + dat[i, j].ToString("x2"));
                    num++;
                    str.Append(",");
                    if (num % 16 == 0)//每16个换下行
                        str.Append("\r\n");                   
                }
            }

            return str.ToString();
        }

        private void createLibBtn_Click(object sender, EventArgs e)
        {
            string str;

            if (inputTextBox.Text != "")
            {
                str = inputTextBox.Text;
                char[] cr = str.ToCharArray();


                outputTextBox.Text = "";//Clear
                                        //遍历
                for (int i = 0; i < cr.Length; i++)
                {
                    Bitmap bmp = StringToBitmap(cr[i].ToString());
                    byte[,] dat = BitmapToCharArray(bmp);
                    string hex_data = ByteDArrayToString(dat);

                    outputTextBox.Text += "\"" + cr[i].ToString() + "\",\r\n" + hex_data;
                }
            }
            

            

            //pictureBox1.Image = Image.FromHbitmap(bmp.GetHbitmap());
            //pictureBox1.Show();
        }
    }
}
