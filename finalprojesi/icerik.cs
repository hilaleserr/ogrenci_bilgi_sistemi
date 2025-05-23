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

namespace finalprojesi
{
    public partial class icerik : Form
    {
        public icerik()
        {
            InitializeComponent();
        }

        void goster()
        {
            SqlConnection baglanti = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\\denemeVT.mdf;Integrated Security=True");
            baglanti.Open();
            SqlDataAdapter vericek = new SqlDataAdapter("select * from icerik order by ad", baglanti);
            DataSet ds = new DataSet();
            vericek.Fill(ds);
            dataGridView1.DataSource = ds.Tables[0];
            baglanti.Close();
        }

        private void icerik_Load(object sender, EventArgs e)
        {
            goster();
            dataGridView1.MultiSelect = false;
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        }

        public string SeciliKayitNo;
       public int SeciliKayit; //genel değişken global
        public Boolean tiklama_kontrol = false;



        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            SeciliKayit = dataGridView1.SelectedCells[0].RowIndex;
            SeciliKayitNo = dataGridView1.Rows[SeciliKayit].Cells[0].Value.ToString();

            // Aşağıda her sütun farklı bir TextBox'a atanmalı. Örnek olarak:
            textBox1.Text = dataGridView1.Rows[SeciliKayit].Cells[1].Value.ToString(); // ad
            textBox2.Text = dataGridView1.Rows[SeciliKayit].Cells[2].Value.ToString(); // soyad
            textBox3.Text = dataGridView1.Rows[SeciliKayit].Cells[3].Value.ToString(); // yaş
            textBox4.Text = dataGridView1.Rows[SeciliKayit].Cells[4].Value.ToString(); // şehir
            textBox5.Text = dataGridView1.Rows[SeciliKayit].Cells[5].Value.ToString(); // açıklama

            SqlConnection baglanti = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\\denemeVT.mdf;Integrated Security=True");
            baglanti.Open();
            SqlCommand komut = new SqlCommand("select * from icerik where ID=@kimlik", baglanti);
            komut.Parameters.AddWithValue("@kimlik", dataGridView1.Rows[SeciliKayit].Cells[0].Value);
            SqlDataReader dr = komut.ExecuteReader();
            if (dr.Read())
            {
                if (dr[6].ToString() == "")
                {
                    pictureBox1.Image = null;
                }
                else
                {
                    byte[] imgData = (byte[])dataGridView1.Rows[SeciliKayit].Cells[6].Value;
                    MemoryStream ms = new MemoryStream(imgData);
                    pictureBox1.Image = Image.FromStream(ms);
                }
                komut.Dispose();
                baglanti.Close();




            }
            tiklama_kontrol = true;


        }

        private void button1_Click(object sender, EventArgs e)
        {
            ekle frm = new ekle();
            frm.Show();
            this.Visible = false;

        }

        private void button2_Click(object sender, EventArgs e)
        {
            int Secilikayit = dataGridView1.SelectedCells[0].RowIndex;
            SeciliKayitNo = dataGridView1.Rows[Secilikayit].Cells[0].Value.ToString();
            DialogResult onay = MessageBox.Show(SeciliKayitNo + "nolu kayıdı silmek istediğinize emin misiniz? ", "silme işlemi", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (onay == DialogResult.Yes)
            {
                SqlConnection baglanti = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\\denemeVT.mdf;Integrated Security=True");

                baglanti.Open();
                SqlCommand komut = new SqlCommand("delete from icerik where ID=@kimlik",baglanti);
                komut.Parameters.AddWithValue("@kimlik" , dataGridView1.Rows[Secilikayit].Cells[0].Value);
                komut.ExecuteNonQuery();
                MessageBox.Show("silme işlemi başarılı ile tamamlandı...");
                baglanti.Close() ;
                goster();
                pictureBox1.Image = null;
                textBox1.Text = "";
                textBox2.Text = "";
                textBox3.Text = "";
                textBox4.Text = "";
                textBox5.Text = "";




            }
        }

      

        private void button3_Click_1(object sender, EventArgs e)
        {

            if (tiklama_kontrol == false) { 
                MessageBox.Show("Lütfen bir kayıt seçiniz..");

            }
            else {

                update frm = new update();
                frm.Controls["textBox1"].Text = textBox1.Text;
                frm.Controls["textBox2"].Text = textBox2.Text;
                frm.Controls["textBox3"].Text = textBox3.Text;
                frm.Controls["textBox4"].Text = textBox4.Text;
                frm.Controls["textBox5"].Text = textBox5.Text;

                ((PictureBox)frm.Controls["pictureBox1"]).Image = pictureBox1.Image;
                Program.Duzenlenecek_ID = Convert.ToInt32(dataGridView1.Rows[SeciliKayit].Cells[0].Value);




                frm.Show();
                this.Visible = false;
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            SqlConnection baglanti = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\\denemeVT.mdf;Integrated Security=True");
            baglanti.Open();
            SqlDataAdapter arama =new SqlDataAdapter("select * from icerik where ad like '"+textBox6.Text+"' order by ad ",baglanti);
            DataSet ds = new DataSet();
            arama.Fill(ds);
            dataGridView1.DataSource = ds.Tables[0];
            baglanti.Close();






        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {
            SqlConnection baglanti = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\\denemeVT.mdf;Integrated Security=True");
            baglanti.Open();
            SqlDataAdapter arama = new SqlDataAdapter("select * from icerik where ad like '" + textBox6.Text + "%' order by ad ", baglanti);
            DataSet ds = new DataSet();
            arama.Fill(ds);
            dataGridView1.DataSource = ds.Tables[0];
            baglanti.Close();
        }
    }
}