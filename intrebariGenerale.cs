using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sistech
{
    public partial class intrebariGenerale : Form
    {

        public intrebariGenerale()
        {
            // Initializează FlowLayoutPanel
            flowLayoutPanel1 = new FlowLayoutPanel();
            flowLayoutPanel1.Dock = DockStyle.Fill;
            flowLayoutPanel1.AutoScroll = true;
            this.Controls.Add(flowLayoutPanel1);
        }

        public void AdaugaIntrebareSiCategorie(string intrebare, string categorie)
        {
            Panel panelIntrebare = new Panel();
            panelIntrebare.Size = new Size(flowLayoutPanel1.ClientSize.Width - 25, 80);
            panelIntrebare.BorderStyle = BorderStyle.FixedSingle;

            TextBox txtIntrebare = new TextBox();
            txtIntrebare.Text = intrebare;
            txtIntrebare.Location = new Point(5, 5);
            txtIntrebare.Size = new Size(panelIntrebare.ClientSize.Width - 10, 20);

            ComboBox cmbCategorie = new ComboBox();
            cmbCategorie.Items.AddRange(new string[] { "Autodezvoltare", "Colaborare cu alte departamente", "Comunicare" });
            cmbCategorie.SelectedItem = categorie;
            cmbCategorie.Location = new Point(5, 30);
            cmbCategorie.Size = new Size(panelIntrebare.ClientSize.Width - 10, 20);

            Button btnModificare = new Button();
            btnModificare.Text = "Modificare";
            btnModificare.Location = new Point(panelIntrebare.ClientSize.Width - 80 - 5, 55);
            btnModificare.Size = new Size(75, 23);
            btnModificare.Click += (sender, e) =>
            {
                intrebare = txtIntrebare.Text;
                categorie = cmbCategorie.SelectedItem?.ToString();
                txtIntrebare.Text = intrebare;
            };

            panelIntrebare.Controls.Add(txtIntrebare);
            panelIntrebare.Controls.Add(cmbCategorie);
            panelIntrebare.Controls.Add(btnModificare);

            flowLayoutPanel1.Controls.Add(panelIntrebare);
        }

    }
}
