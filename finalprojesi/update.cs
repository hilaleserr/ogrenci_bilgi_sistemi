using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace finalprojesi
{
    public partial class update : Form
    {
        private OpenFileDialog openFileDialog1;

        public update()
        {
            InitializeComponent();

            openFileDialog1 = new OpenFileDialog();
            openFileDialog1.Title = "Resim Aç";
            openFileDialog1.Filter = "JPEG Dosyası (*.jpg)|*.jpg|GIF Dosyası (*.gif)|*.gif|PNG Dosyası (*.png)|*.png";
        }

        private void button3_Click(object sender, EventArgs e)
        {
            DialogResult onay = MessageBox.Show("Ana programa dönmek istediğinizden emin misiniz?", "Çıkış Başarılı", MessageBoxButtons.YesNo);
            if (onay == DialogResult.Yes)
            {
                icerik frm = new icerik();
                frm.Show();
                this.Visible = false;
            }
        }

        string resimPath;
        bool kontrol = false;

        private void button1_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                pictureBox1.Image = Image.FromFile(openFileDialog1.FileName);
                resimPath = openFileDialog1.FileName;
                kontrol = true; // resim seçildi
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            using (SqlConnection baglanti = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\\denemeVT.mdf;Integrated Security=True"))
            {
                baglanti.Open();

                if (!kontrol)
                {
                    SqlCommand duzenleme = new SqlCommand("update icerik set ad=@ad, soyad=@soyad, tc=@tc_no, dogum_yili=@dogum_yili, dogum_yeri=@dogum_yeri where ID=@kimlik", baglanti);
                    duzenleme.Parameters.AddWithValue("@ad", textBox1.Text);
                    duzenleme.Parameters.AddWithValue("@soyad", textBox2.Text);
                    duzenleme.Parameters.AddWithValue("@tc_no", textBox3.Text);
                    duzenleme.Parameters.AddWithValue("@dogum_yili", textBox4.Text);
                    duzenleme.Parameters.AddWithValue("@dogum_yeri", textBox5.Text);
                    duzenleme.Parameters.AddWithValue("@kimlik", Program.Duzenlenecek_ID);
                    duzenleme.ExecuteNonQuery();

                    MessageBox.Show("Resimsiz düzenleme başarılı...");
                }
                else
                {
                    // Resimli güncelleme
                    byte[] resim = null;
                    using (FileStream fs = new FileStream(resimPath, FileMode.Open, FileAccess.Read))
                    {
                        using (BinaryReader br = new BinaryReader(fs))
                        {
                            resim = br.ReadBytes((int)fs.Length);
                        }
                    }

                    SqlCommand duzenleme = new SqlCommand("update icerik set ad=@ad, soyad=@soyad, tc_no=@tc_no, dogum_yili=@dogum_yili, dogum_yeri=@dogum_yeri, resim=@image where ID=@kimlik", baglanti);
                    duzenleme.Parameters.AddWithValue("@ad", textBox1.Text);
                    duzenleme.Parameters.AddWithValue("@soyad", textBox2.Text);
                    duzenleme.Parameters.AddWithValue("@tc_no", textBox3.Text);
                    duzenleme.Parameters.AddWithValue("@dogum_yili", textBox4.Text);
                    duzenleme.Parameters.AddWithValue("@dogum_yeri", textBox5.Text);
                    duzenleme.Parameters.Add("@image", SqlDbType.Image, resim.Length).Value = resim;
                    duzenleme.Parameters.AddWithValue("@kimlik", Program.Duzenlenecek_ID);
                    duzenleme.ExecuteNonQuery();

                    MessageBox.Show("Resimli düzenleme başarılı");
                    icerik frm = new icerik();
                    frm.tiklama_kontrol = false;

                    frm.Show();
                    this.Close();
                }
            }
        }
    }
}
