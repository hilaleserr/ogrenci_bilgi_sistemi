using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace finalprojesi
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DialogResult onay = MessageBox.Show("Çıkmak istediğinizden emin misiniz?", "Çıkış İşlemi", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (onay == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SqlConnection baglanti = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\\denemeVT.mdf;Integrated Security=True;Connect Timeout=30");
            baglanti.Open();
            SqlCommand komut = new SqlCommand("select * from Kullanici where KullaniciAdi=@KullaniciAdi and Parola=@Parola", baglanti);
            komut.Parameters.AddWithValue("@KullaniciAdi", textBox1.Text);
            komut.Parameters.AddWithValue("@Parola", textBox2.Text);
            SqlDataReader dr = komut.ExecuteReader();
            if (dr.Read())
            {
                baglanti.Close();
                icerik frm = new icerik();
                frm.Show();
                this.Hide(); // this.Visible = false yerine daha doğru kullanım
            }
            else
            {
                MessageBox.Show("Yanlış kullanıcı adı veya parolası");
            }

            dr.Close();
            baglanti.Close();
        }
    }
}
