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
using System.IO;
using System.Data.SqlTypes;

namespace finalprojesi
{
    public partial class ekle : Form
    {
        public ekle()
        {
            InitializeComponent();
        }

        string resimPath;

        private void button1_Click(object sender, EventArgs e)
        {
            openFileDialog1.Title = "Resim Aç";
            openFileDialog1.Filter = "JPEG Dosyası (*.jpg)|*.jpg|GIF Dosyası (*.gif)|*.gif|PNG Dosyası (*.png)|*.png";

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                pictureBox1.Image = Image.FromFile(openFileDialog1.FileName);
                resimPath = openFileDialog1.FileName;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (pictureBox1.Image == null)
            {
                MessageBox.Show("Lütfen bir resim seçiniz...");

            }

            else
            {

                FileStream fs =new FileStream(resimPath, FileMode.Open ,FileAccess.Read);
                BinaryReader br = new BinaryReader(fs);
                byte[] resim=br.ReadBytes((int)fs.Length);
                br.Close();
                fs.Close();
                SqlConnection baglanti = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\\denemeVT.mdf;Integrated Security=True");
                baglanti.Open();
                SqlCommand komut = new SqlCommand("select * from icerik where tc_no=@tc_no", baglanti);
                komut.Parameters.AddWithValue("@tc_no",textBox3.Text);
                SqlDataReader dr=komut.ExecuteReader();
                if (dr.Read())
                {
                    MessageBox.Show("Bu tc kayıtlıdır...");

                }
                else
                {
                    dr.Close();
                    SqlCommand kmt = new SqlCommand("insert into icerik (ad,soyad ,tc_no, dogum_yili,dogum_yeri,resim) values(@ad,@soyad,@tc_no,@dogum_yili,@dogum_yeri,@image)", baglanti);
                    kmt.Parameters.AddWithValue("@ad", textBox1.Text);
                    kmt.Parameters.AddWithValue("@soyad", textBox2.Text);
                    kmt.Parameters.AddWithValue("@tc_no", textBox3.Text);
                    kmt.Parameters.AddWithValue("@dogum_yili", textBox4.Text);
                    kmt.Parameters.AddWithValue("@dogum_yeri", textBox5.Text);
                    kmt.Parameters.Add("@image", SqlDbType.Image, resim.Length).Value = resim;
                    kmt.ExecuteNonQuery();
                    MessageBox.Show("kayıt başarılı");
                    baglanti.Close();
                    icerik frm = new icerik();
                    frm.Show();
                    this.Visible = false;

                }
            }    
        }

        private void button3_Click(object sender, EventArgs e)
        {
            DialogResult onay = MessageBox.Show("Ana programa dönmek istediğinizden emin misiniz?", "çıkış başarılı");
            if (onay == DialogResult.Yes)
            {
                icerik frm = new icerik();
                frm.Show();
                this.Visible = false;




            }




        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
    }
}
