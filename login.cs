using System.Windows.Forms;
using System;

namespace Sistech
{
    public partial class login : Form
    {
        // Declarați o proprietate publică pentru a stoca utilizatorul autentificat
        public string UtilizatorAutentificat { get; private set; }

        public login()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string user = textBox1.Text;
            string parola = textBox2.Text;

            if (user == "admin" && parola == "1234")
            {
                // Setează utilizatorul autentificat la valoarea introdusă
                UtilizatorAutentificat = user;
                MessageBox.Show("Autentificare reusita!", "Succes", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            else
            {
                MessageBox.Show("Nume de utilizator sau parola incorecta!", "Eroare", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void login_Load(object sender, EventArgs e)
        {

        }
    }
}
