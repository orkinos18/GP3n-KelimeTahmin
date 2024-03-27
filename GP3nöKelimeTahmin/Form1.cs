using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics.Eventing.Reader;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GP3nöKelimeTahmin
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
            string kelime="";
            string gizli = "";
            string yeni = "";
            int hak = 1;

        private void button1_Click(object sender, EventArgs e)
        {
            labelKelime.Text = "";
            kelime = "";
            gizli = "";
            SqlConnection bag = new SqlConnection(@"server=orkinos\orkinos;initial catalog=gorsel3kelimetahmin;integrated security=true");
            string sql = "Select * from kelimeler";
            SqlDataAdapter da = new SqlDataAdapter(sql,bag);
            DataTable dt = new DataTable();
            da.Fill(dt);
            int satirsayisi = dt.Rows.Count;

            Random rnd=new Random();
            int indexnumber=rnd.Next(0,satirsayisi);
            if (checkBox1.Checked)
                kelime = dt.Rows[indexnumber][2].ToString();
            else
                kelime = dt.Rows[indexnumber][1].ToString();
            kelime=kelime.Trim();
            foreach (var item in kelime)
                gizli += "*";

            labelKelime.Text = gizli;
            button1.Enabled = false;

        }

        private void button2_Click(object sender, EventArgs e)
        {


            label1.Text = hak.ToString() + ". hakkınız";
                yeni = "";
                char karakter = Convert.ToChar(textBoxKarakter.Text);
                for (int i = 0; i < kelime.Length; i++)
                {
                    if (karakter == kelime[i])
                    {
                        yeni += kelime[i];
                    }
                    else
                    {
                        yeni += gizli[i];
                    }
                }
                gizli = yeni;
                labelKelime.Text = gizli;
                textBoxKarakter.Clear();
                if (kelime == labelKelime.Text)
                {
                    MessageBox.Show("Tebrikler");
                }
                hak++;
                if (hak==6)
                {
                    MessageBox.Show("hakkınız bitti");
                    labelKelime.Text = kelime;
                }
                button1.Enabled = true;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string tahmin = textBox2.Text;
            if (tahmin==kelime)
            {
                MessageBox.Show("Tebrikler");
                labelKelime.Text = kelime;

            }
            else
            {
                MessageBox.Show("yanlış");
                labelKelime.Text =kelime;
            }
            button1.Enabled = true;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (DialogResult.Yes == MessageBox.Show("Kapanıyor", "", MessageBoxButtons.YesNo))
            {
                Application.Exit();
            }
        }
    }
}
