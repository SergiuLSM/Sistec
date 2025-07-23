using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Sistech
{
    public partial class Preview : Form
    {
        private List<Tuple<string, string>> intrebari;

        public Preview(List<Tuple<string, string>> intrebari)
        {
            InitializeComponent();
            this.intrebari = intrebari;
            AdaugaEvaluareInInterfata(intrebari); // Aici apelezi metoda pentru a afișa întrebările
        }

        public void AdaugaEvaluareInInterfata(List<Tuple<string, string>> intrebariSiCategorii)
        {
            string categorieCurenta = null;
            int offsetY = 20;

            foreach (var intrebareCategorie in intrebariSiCategorii)
            {
                if (intrebareCategorie.Item2 != categorieCurenta)
                {
                    AdaugaCategorie(intrebareCategorie.Item2, offsetY);
                    offsetY += 30; // Adăugăm spațiu între categorie și întrebări
                    categorieCurenta = intrebareCategorie.Item2;
                }

                AdaugaIntrebare(intrebareCategorie.Item1, offsetY);
                offsetY += 30; // Adăugăm spațiu între întrebări
            }
        }

        private void AdaugaCategorie(string categorie, int offsetY)
        {
            Label lblCategorie = new Label();
            lblCategorie.Text = categorie;
            lblCategorie.Font = new Font(lblCategorie.Font, FontStyle.Bold);
            lblCategorie.Location = new Point(5, offsetY);
            lblCategorie.AutoSize = true;
            flowLayoutPanel1.Controls.Add(lblCategorie);
        }

        private void AdaugaIntrebare(string intrebare, int offsetY)
        {
            Panel panelIntrebare = new Panel();
            panelIntrebare.Size = new Size(flowLayoutPanel1.ClientSize.Width - 25, 100); // Dimensiunea panelului pentru întrebare
            panelIntrebare.BorderStyle = BorderStyle.FixedSingle;

            Label lblIntrebare = new Label();
            lblIntrebare.Text = $"Întrebare: {intrebare}";
            lblIntrebare.Location = new Point(5, 5);
            lblIntrebare.AutoSize = true;
            panelIntrebare.Controls.Add(lblIntrebare);

            // Adăugăm radiobutton-urile pentru întrebare
            GroupBox groupBox = new GroupBox();
            groupBox.Text = "Note"; // Textul deasupra radiobutton-urilor
            groupBox.Location = new Point(5, 30); // Poziția group box-ului
            groupBox.Size = new Size(panelIntrebare.ClientSize.Width - 10, 50);

            for (int i = 1; i <= 5; i++)
            {
                RadioButton radioButton = new RadioButton();
                radioButton.Text = i.ToString();
                radioButton.AutoSize = true;
                radioButton.Location = new Point(10 + (i - 1) * 50, 20);
                groupBox.Controls.Add(radioButton);
            }

            panelIntrebare.Controls.Add(groupBox);
            flowLayoutPanel1.Controls.Add(panelIntrebare);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string filePath = "raspunsuri.txt";
            try
            {
                using (System.IO.StreamWriter file = new System.IO.StreamWriter(filePath))
                {
                    string categorieCurenta = null;
                    file.WriteLine("Categorie  Intrebare  Nota");

                    foreach (Control control in flowLayoutPanel1.Controls)
                    {
                        if (control is Label lblCategorie)
                        {
                            categorieCurenta = lblCategorie.Text;
                        }
                        else if (control is Panel panelIntrebare)
                        {
                            string intrebare = null;
                            string raspuns = null;

                            foreach (Control subControl in panelIntrebare.Controls)
                            {
                                if (subControl is Label lblIntrebare)
                                {
                                    intrebare = lblIntrebare.Text.Replace("Întrebare: ", "");
                                }
                                else if (subControl is GroupBox groupBox)
                                {
                                    foreach (Control rb in groupBox.Controls)
                                    {
                                        if (rb is RadioButton radioButton && radioButton.Checked)
                                        {
                                            raspuns = radioButton.Text;
                                            break;
                                        }
                                    }
                                }
                            }

                            file.WriteLine($"{categorieCurenta},{intrebare},{raspuns}");
                        }
                    }
                }
                MessageBox.Show("Întrebările și răspunsurile au fost salvate cu succes în fișierul raspunsuri.txt!", "Salvare reușită", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"A apărut o eroare la salvarea întrebărilor: {ex.Message}", "Eroare", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
    
}
