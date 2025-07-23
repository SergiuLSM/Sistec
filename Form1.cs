using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Sistech
{
    public partial class Form1 : Form
    {
        private List<Chestionar> chestionare = new List<Chestionar>();
        private List<Evaluare> evaluari = new List<Evaluare>();
        private List<Tuple<string, string>> intrebari = new List<Tuple<string, string>>();
        private string utilizatorConectat;


        public Form1(string utilizatorConectat)
        {
            InitializeComponent();
            this.utilizatorConectat = utilizatorConectat;
        }

        private void logOutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Sunteți sigur că doriți să vă deconectați?", "Confirmare deconectare", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                this.Close(); // Ascundeți forma curentă (Form1)
                login loginForm = new login(); // Creați o nouă instanță a formei de autentificare
                loginForm.ShowDialog(); // Afișați forma de autentificare
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string titlu = textBox1.Text;
            if (!string.IsNullOrWhiteSpace(titlu))
            {
                // Verificăm dacă titlul există deja
                if (chestionare.Any(c => c.Titlu == titlu))
                {
                    MessageBox.Show("Acest titlu de evaluare deja există.", "Eroare", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                Chestionar chestionar = new Chestionar(titlu);
                Evaluare evaluare = new Evaluare(titlu, utilizatorConectat);
                evaluari.Add(evaluare);
                chestionare.Add(chestionar);
                AdaugaEvaluareInInterfata(evaluare);
                textBox1.Clear();
            }
            else
            {
                MessageBox.Show("Te rog să introduci un titlu pentru evaluare.", "Eroare", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void AdaugaIntrebareInChestionar(string intrebare, string titluChestionar)
        {
            Chestionar chestionar = chestionare.FirstOrDefault(c => c.Titlu == titluChestionar);
            if (chestionar != null)
            {
                if (!chestionar.Intrebari.Contains(intrebare))
                {
                    chestionar.Intrebari.Add(intrebare);
                    MessageBox.Show($"Întrebare adăugată cu succes în chestionarul '{titluChestionar}'.", "Succes", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show($"Această întrebare există deja în chestionarul '{titluChestionar}'.", "Eroare", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void AdaugaEvaluareInInterfata(Evaluare evaluare)
        {
            Panel panelEvaluare = new Panel();
            panelEvaluare.Size = new Size(flowLayoutPanel1.ClientSize.Width - 25, 50);
            panelEvaluare.BorderStyle = BorderStyle.FixedSingle;

            Label lblInfo = new Label();
            lblInfo.Text = evaluare.ToString();
            lblInfo.Location = new Point(5, 5);
            lblInfo.Size = new Size(panelEvaluare.ClientSize.Width - 90, 40);
            lblInfo.Font = new Font(lblInfo.Font.FontFamily, 14, FontStyle.Regular);

            Button btnModifica = new Button();
            btnModifica.Text = "Modifică";
            btnModifica.Location = new Point(panelEvaluare.ClientSize.Width - 80 - 5, 5);
            btnModifica.Size = new Size(75, 23);
            btnModifica.BackColor = Color.Orange;
            btnModifica.Click += (sender, e) => ModificaEvaluare(evaluare);

            panelEvaluare.Controls.Add(lblInfo);
            panelEvaluare.Controls.Add(btnModifica);

            flowLayoutPanel1.Controls.Add(panelEvaluare);
        }

        private void ModificaEvaluare(Evaluare evaluare)
        {
            EditEvaluationForm editForm = new EditEvaluationForm(intrebari);
            editForm.ShowDialog();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void intrebariGeneraleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            intrebariGenerale forma = new intrebariGenerale();
            forma.ShowDialog();
        }

        private void evaluariToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
    }
}
