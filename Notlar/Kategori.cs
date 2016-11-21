using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Business;
using Model;

namespace Arayuz
{
    public partial class Kategori : Form
    {
        public Kategori()
        {
            InitializeComponent();
        }
        KategoriKontrol k = new KategoriKontrol();
        private void Kategori_Load(object sender, EventArgs e)
        {
            Yenile();
            btn_sil.Enabled = false;
            btn_duzenle.Enabled = false;
        }

        void Yenile()
        {
            listBox1.DataSource = k.TumKategoriler();
            listBox1.ValueMember = "KategoriID";
            listBox1.DisplayMember = "KategoriAdi";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //Model.Kategori eklenecek = new Model.Kategori();
            //eklenecek.KategoriAdi = textBox1.Text;
            //string mesaj = k.Ekle(eklenecek);
            //Message.Show(mesaj);
            if (btn_ekle.Text == "Ekle")
            {
                MessageBox.Show(k.Ekle(new Model.Kategori() { KategoriAdi = textBox1.Text }));
            }
            else
            { //düzenlemeyi kaydet
                Model.Kategori duzenlenecek = new Model.Kategori();
                duzenlenecek.KategoriID = (int)listBox1.SelectedValue;
                duzenlenecek.KategoriAdi = textBox1.Text;
                MessageBox.Show(k.Duzenle(duzenlenecek));
                GroupYenile();
            }
            Yenile();
        }

        void GroupYenile()
        {
            groupBox1.Text = "Yeni Kategori";
            btn_ekle.Text = "Ekle";
            textBox1.Text = "";
        }

        public void ESCbasildi(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Escape) //27
                GroupYenile();
        }

        private void button4_Click(object sender, EventArgs e)
        { //geri butonu (ana ekrana geçiş)
            this.Close();
            Giris g = (Giris)Application.OpenForms["Giris"];
            g.Show();
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            //selectedindex: seçilen elemanın indisi
            //birşey seçilmezse -1 gelir
            if (listBox1.SelectedIndex > -1) {
                //bir kategori seçildiyse butonlar aktif olsun
                btn_duzenle.Enabled = true;
                btn_sil.Enabled = true;
            }
        }

        private void btn_sil_Click(object sender, EventArgs e)
        {
          DialogResult d= MessageBox.Show("Bu kategoriyi silmek istediğinize emin misiniz?", "Kategori Sil", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if(d == DialogResult.Yes)
            { //kategoriyi siliyoruz
                Model.Kategori silinecek = new Model.Kategori();
                silinecek.KategoriID = (int)listBox1.SelectedValue;
                MessageBox.Show(k.Sil(silinecek));
                Yenile();
            }
        }
        
        private void btn_duzenle_Click(object sender, EventArgs e)
        {
            groupBox1.Text = "Kategori Düzenle";
            btn_ekle.Text = "Kaydet";
            textBox1.Text = ((Model.Kategori)listBox1.SelectedItem).KategoriAdi;
            toolTip1.ToolTipTitle = "İpucu";
            toolTip1.Show("Düzenlemeyi iptal etmek için esc tuşuna basın.", this,groupBox1.Left+30,textBox1.Bottom+50,2000);
        }
    }
}
