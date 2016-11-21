using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Arayuz
{
    public partial class NotAra : Form
    {
        public NotAra()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        { //filtrele
            NotEkran ne = (NotEkran)Application.OpenForms["NotEkran"];
            if (ne != null) ne.Activate();
            else
            {
                ne = new NotEkran();
                ne.Rtumnotlar.Checked = true;
                ne.Show();
            }
            //NotEkran açıksa öne geçsin
            if (checkBox1.Checked)
                ne.arananTarih = dateTimePicker1.Value;
            else
                ne.arananTarih = null;

            if (!string.IsNullOrEmpty(textBox1.Text))
                ne.arananKelime = textBox1.Text;
            else
                ne.arananKelime = null;

            ne.GridDoldur();

        }

        private void NotAra_Load(object sender, EventArgs e)
        {
            dateTimePicker1.Enabled = false;
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
                dateTimePicker1.Enabled = true;
            else
                dateTimePicker1.Enabled = false;
        }
    }
}
