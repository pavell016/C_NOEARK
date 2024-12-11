using System.Diagnostics;
using System.Security.Cryptography.Pkcs;
using System.Text.RegularExpressions;

namespace WinFormsApp1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        private bool ValidarTextBoxes(params TextBox[] textBoxes)
        {
            // Expresi�n regular para verificar si un texto contiene solo n�meros
            Regex regex = new Regex(@"^\d+$");

            foreach (TextBox textBox in textBoxes)
            {
                // Validar que no est� vac�o
                if (string.IsNullOrWhiteSpace(textBox.Text))
                {
                    //MessageBox.Show("Uno o m�s campos est�n vac�os.", "Error de Validaci�n", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }

                // Validar que contenga solo n�meros
                if (!regex.IsMatch(textBox.Text))
                {
                    //MessageBox.Show("Uno o m�s campos contienen valores no num�ricos.", "Error de Validaci�n", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
            }

            // Si todos los TextBox pasan las validaciones
            return true;
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            button1.Enabled = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {


            
            string argumentos = textBox1.Text + " " + textBox2.Text + " " + textBox3.Text;
            Process P = new Process();
            P.StartInfo.FileName = @"C:\Users\programacion\Documents\GitHub\C_NOEARK\C#\processos\programa_processos\procesos\procesos\bin\Debug\procesos.exe";

            P.StartInfo.Arguments = argumentos;
            
            P.StartInfo.UseShellExecute = true;
            P.StartInfo.CreateNoWindow = true;

            try
            {
                P.Start();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

            

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            if (ValidarTextBoxes(textBox1, textBox2, textBox3))
            {
                button1.Enabled = true;
            }
            else { 
                button1.Enabled= false;
            }
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            if (ValidarTextBoxes(textBox1, textBox2, textBox3))
            {
                button1.Enabled = true;
            }
            else
            {
                button1.Enabled = false;
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (ValidarTextBoxes(textBox1, textBox2, textBox3))
            {
                button1.Enabled = true;
            }
            else
            {
                button1.Enabled = false;
            }
        }
    }
}
