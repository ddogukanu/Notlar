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
    public partial class NotEkran : Form
    {
        ////propfull
        //private int myVar; //asıl olarak kullanılacak private değişken. Başka yerden erişilemez.

        //public int MyProperty //Dışarıya açık public değişken. Private alanı kontrol ediyor.
        //{
        //    get { return myVar; }
        //    set { myVar = value; }
        //}

        //private olan radio buttonları public olarak dışarıya açıyoruz.

        public string arananKelime { get; set; }
        public DateTime? arananTarih { get; set; }

        public RadioButton Ryeninotlar
        {
            get { return radioButton4; }
        }

        public RadioButton Rokunmamislar
        {
            get { return radioButton3; }
        }

        public RadioButton Rtumnotlar
        {
            get { return radioButton1; }
        }

        public NotEkran()
        {
            InitializeComponent();
        }

        NotControl n = new NotControl();

        public void GridDoldur()
        {
            List<Not> notliste = new List<Not>();
            if (radioButton4.Checked)  //seçiliyse
               notliste = n.YeniNotlar();
            else if (radioButton3.Checked)
                notliste= n.OkunmamisNotlar();
            else if (radioButton1.Checked)
               notliste = n.TumNotlar();

            //  => lambda operator x öyleki x içindeki yazı alanı aranan kelime içerenler
            //x değişkeni listedeki her bir notu temsil
            if (arananKelime != null)
                notliste = notliste.Where(x => x.Yazi.Contains(arananKelime)).ToList();

            //if(arananTarih!=DateTime.MinValue) DateTime? değilse
            if (arananTarih != null)
                notliste = notliste.Where(n => n.Tarih == arananTarih).ToList();

            dataGridView1.DataSource = notliste;

        

            //okunmamış satırlar sarı olsun
            foreach (DataGridViewRow item in dataGridView1.Rows)
            {
                if ((bool)item.Cells["OkunduMu"].Value == false)
                    item.DefaultCellStyle.BackColor = Color.Yellow;
                else
                    item.DefaultCellStyle.BackColor = Color.White;
            }
        }
        private void radioButton4_CheckedChanged(object sender, EventArgs e)
        { //yeni notlar
            GridDoldur();
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        { //okunmamışlar
            GridDoldur();
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            GridDoldur();
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        { //ekle formu

            EkleDuzenle ed = new EkleDuzenle();
            ed.Show();

        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {//düzenle butonu
            if (dataGridView1.SelectedRows.Count > 0)
            {
                EkleDuzenle ed = new EkleDuzenle();
                ed.BizimButton.Text = "Kaydet";
                ed.GelenNot = (Not)dataGridView1.SelectedRows[0].DataBoundItem;
                ed.Show();
            }
            else MessageBox.Show("Düzenlemek istediğiniz notu seçin");
        }

        private void NotEkran_Load(object sender, EventArgs e)
        {
            GridDoldur();
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        { //sil butonu
            if (dataGridView1.SelectedRows.Count > 0)
            {
                DialogResult dr = MessageBox.Show("Emin misiniz?", "Not Sil", MessageBoxButtons.YesNo);
                if (dr == DialogResult.Yes)
                {
                    object id = dataGridView1.SelectedRows[0].Cells["NotID"].Value;
                    MessageBox.Show(n.Sil(new Not() { NotID = (int)id }));
                    GridDoldur();
                }
            }
            else MessageBox.Show("Silinecek notu seçiniz");
        }

        private void btn_ara_Click(object sender, EventArgs e)
        {
            NotAra n = new NotAra();
            n.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            List<int> idliste = new List<int>();
            foreach (DataGridViewRow item in dataGridView1.Rows)
            {
                if ((bool)item.Cells["OkunduMu"].Value == true) {
                    int id = (int)item.Cells["NotID"].Value;
                    idliste.Add(id);
                }
            }
            n.Oku(idliste);
            GridDoldur();
        }
    }
}
