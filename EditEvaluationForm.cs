using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Sistech
{
    public partial class EditEvaluationForm : Form
    {
        private List<Tuple<string, string>> intrebariSiCategorii;

        public EditEvaluationForm()
        {
            InitializeComponent();
        }

        public EditEvaluationForm(List<Tuple<string, string>> intrebariSiCategorii)
        {
            InitializeComponent();
            this.intrebariSiCategorii = intrebariSiCategorii;

            comboBox1.Items.Add("--Alege categoria--");
            comboBox1.Items.Add("Autodezvoltare");
            comboBox1.Items.Add("Colaborare cu alte departamente");
            comboBox1.Items.Add("Comunicare");
            comboBox1.SelectedIndex = 0;

            foreach (var intrebareCategorie in intrebariSiCategorii)
            {
                AdaugaIntrebareSiCategorie(intrebareCategorie.Item1, intrebareCategorie.Item2);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string intrebare = textBox1.Text;
            string categorie = comboBox1.SelectedItem?.ToString();

            if (categorie != "--Alege categoria--" && !string.IsNullOrEmpty(intrebare))
            {
                AdaugaIntrebareSiCategorie(intrebare, categorie);
                intrebariSiCategorii.Add(new Tuple<string, string>(intrebare, categorie));
                MessageBox.Show($"Întrebare: {intrebare}\nCategorie: {categorie}\n\nÎntrebare salvată cu succes!", "Evaluare Salvată", MessageBoxButtons.OK, MessageBoxIcon.Information);
                textBox1.Clear();
                comboBox1.SelectedIndex = 0;

                //intrebariGenerale.AdaugaIntrebareSiCategorie(intrebare, categorie);

            }
            else
            {
                MessageBox.Show("Te rog să completezi atât întrebarea cât și să alegi o categorie.", "Eroare", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void AdaugaIntrebareSiCategorie(string intrebare, string categorie)
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

        private void StergeIntrebare(Panel panelIntrebare)
        {
            int index = flowLayoutPanel1.Controls.IndexOf(panelIntrebare);
            flowLayoutPanel1.Controls.RemoveAt(index);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            // Actualizează lista intrebariSiCategorii cu întrebările din flowLayoutPanel
            intrebariSiCategorii.Clear();
            foreach (Control control in flowLayoutPanel1.Controls)
            {
                if (control is Panel panelIntrebare)
                {
                    string intrebare = "";
                    string categorie = "";

                    // Parcurge controalele din panou pentru a găsi eticheta și combobox-ul
                    foreach (Control subControl in panelIntrebare.Controls)
                    {
                        if (subControl is TextBox textBox)
                        {
                            intrebare = textBox.Text;
                        }
                        else if (subControl is ComboBox comboBox)
                        {
                            categorie = comboBox.SelectedItem?.ToString();
                        }
                    }

                    // Adaugă întrebarea și categoria în listă sub formă de tuplu
                    intrebariSiCategorii.Add(new Tuple<string, string>(intrebare, categorie));
                }
            }

            // Deschide PreviewForm și trimite datele necesare
            Preview previewForm = new Preview(intrebariSiCategorii);
            previewForm.ShowDialog();
        }

        private void SalveazaIntrebariInFisier(List<Tuple<string, string>> intrebari, string numeFisier)
        {
            try
            {
                using (System.IO.StreamWriter file = new System.IO.StreamWriter(numeFisier))
                {
                    foreach (var intrebare in intrebari)
                    {
                        file.WriteLine($"{intrebare.Item1},{intrebare.Item2}");
                    }
                }
                MessageBox.Show("Intrebarile au fost salvate cu succes!", "Salvare reusita", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"A apărut o eroare la salvarea întrebărilor: {ex.Message}", "Eroare", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            using (SaveFileDialog saveFileDialog = new SaveFileDialog())
            {
                saveFileDialog.Filter = "Text Files (*.txt)|*.txt|All Files (*.*)|*.*";
                saveFileDialog.FilterIndex = 1;
                saveFileDialog.RestoreDirectory = true;

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string numeFisier = saveFileDialog.FileName;
                    SalveazaIntrebariInFisier(intrebariSiCategorii, numeFisier); // Accesează variabila de instanță intrebariSiCategorii
                }
            }
        }


        private List<Tuple<string, string>> IncarcaIntrebariDinFisier(string numeFisier)
        {
            List<Tuple<string, string>> intrebariIncarcate = new List<Tuple<string, string>>();

            try
            {
                string[] linii = System.IO.File.ReadAllLines(numeFisier);
                foreach (string linie in linii)
                {
                    string[] campuri = linie.Split(',');
                    if (campuri.Length == 2)
                    {
                        string intrebare = campuri[0];
                        string categorie = campuri[1];
                        intrebariIncarcate.Add(new Tuple<string, string>(intrebare, categorie));
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"A apărut o eroare la încărcarea întrebărilor: {ex.Message}", "Eroare", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return intrebariIncarcate;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "Text Files (*.txt)|*.txt|All Files (*.*)|*.*";
                openFileDialog.FilterIndex = 1;
                openFileDialog.RestoreDirectory = true;

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string numeFisier = openFileDialog.FileName;
                    List<Tuple<string, string>> intrebariIncarcate = IncarcaIntrebariDinFisier(numeFisier);
                    // Utilizează lista de întrebări încărcate pentru afișare sau alte operații
                }
            }
        }
    }
}
