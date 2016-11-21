using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Model;
using Business;

namespace Arayuz
{
    public partial class EkleDuzenle : Form
    {
        public EkleDuzenle()
        {
            InitializeComponent();
        }
        KategoriKontrol kat = new KategoriKontrol();
        NotControl nk = new NotControl();
        public Button BizimButton { get { return btn; } }
        public Not GelenNot { get; set; }
        private void EkleDuzenle_Load(object sender, EventArgs e)
        {
            comboBox1.DataSource = kat.TumKategoriler();
            comboBox1.DisplayMember = "KategoriAdi";
            comboBox1.ValueMember = "KategoriID";

            if (GelenNot != null) { //düzenleme işlemiyse
                comboBox1.SelectedValue = GelenNot.KategoriID;
                richTextBox1.Text = GelenNot.Yazi;
            }
        }

        private void btn_Click(object sender, EventArgs e)
        {
            Not n = new Not();
            n.Yazi = richTextBox1.Text;
            n.KategoriID = (int)comboBox1.SelectedValue;
            if (btn.Text == "Ekle")
                MessageBox.Show(nk.Ekle(n));
            else {
                n.NotID = GelenNot.NotID;
                MessageBox.Show(nk.Duzenle(n));
            }

            NotEkran ne = (NotEkran)Application.OpenForms["NotEkran"];
            ne.GridDoldur();
        }
    }
}
