using Microsoft.VisualBasic.ApplicationServices;
using System.Diagnostics;
using System.Text.RegularExpressions;

namespace Scheduler
{
    public partial class Form1 : Form
    {
        static SemaphoreSlim sem = new SemaphoreSlim(3); // permet nomes 3 processos
        public static readonly object locker = new object();
        Queue<string> alumne = new Queue<string>(); //llista of queues
        static int processos_actius = 0; // contara cuants processos estan actius al mateix temps
        static int queuetotal = 0;
        //aquesta funció fa un update de les variables que controlen els processos actius i en espera
        public void updateValues() {
            this.Invoke((MethodInvoker)delegate
            {
                proces_actiu.Text = processos_actius.ToString(); 
                totalq.Text = queuetotal.ToString();
                
            });
        }
        public void create_process() {
            Process P = new Process();
            //programa que crea la fitxa de notes de l'alumne
            string path = Path.Combine(Environment.CurrentDirectory, "Processos_Pavell.exe");
            P.StartInfo.FileName = path;
            //allow to open shells
            P.StartInfo.UseShellExecute = true;
            P.StartInfo.CreateNoWindow = true;
            while (true) { 
                if (processos_actius < 3) { 
                        sem.Wait();
                    if (alumne.Count != 0)
                    {
                        P.StartInfo.Arguments = alumne.Dequeue();
                    }
                    else { 
                        break;
                    }
                    lock (locker)
                    {
                            processos_actius++;
                            queuetotal--;
                    }
                    updateValues();
                    try
                    {
                        
                        
                        P.Start();
                        P.WaitForExit();
                    }
                    catch (Exception ex)
                    {
                        error.Text = ex.ToString(); //en cas d'errors
                    }
                    finally
                    {
                        
                        sem.Release();
                        lock (locker)
                        {
                            processos_actius--;
                        }
                        updateValues();
                    }

                }
            }
        }

        public void comprovacio_Validesa(TextBox t1, TextBox t2, TextBox t3)
        {
            Regex onlytext = new Regex(@"^[a-zA-Z0-9\sñ]+$"); //comprova que nomes hi hagui lletres  siguin majuscules o minuscules
            Regex intonly = new Regex(@"^[1-2]$"); //comprova que només hi hagui un numero que a la vegada pot ser nomes 1 o 2

            //comprova si els camps de text es dona la informació que es demana
            if (!string.IsNullOrWhiteSpace(t1.Text) &&
                !string.IsNullOrWhiteSpace(t2.Text) &&
                !string.IsNullOrWhiteSpace(t3.Text) &&
                onlytext.IsMatch(t1.Text) &&
                onlytext.IsMatch(t2.Text) &&
                intonly.IsMatch(t3.Text))
            {
                submit.Enabled = true;
            }
            else
            {
                submit.Enabled = false;
            }

        }
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            submit.Enabled = false;
            updateValues();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //arguments
            String arguments = nom.Text + " " + cognom.Text + " " + curs.Text;
            alumne.Enqueue(arguments);
            queuetotal++;
            updateValues();
            //Creates threads
            Thread card = new Thread(create_process);
            card.Start();
            //clear data after a process is created
            nom.Clear();
            cognom.Clear();
            curs.Clear();
            

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void nom_TextChanged(object sender, EventArgs e)
        {
            comprovacio_Validesa(nom, cognom, curs);
        }

        private void cognom_TextChanged(object sender, EventArgs e)
        {
            comprovacio_Validesa(nom, cognom, curs);
        }

        private void curs_TextChanged(object sender, EventArgs e)
        {
            comprovacio_Validesa(nom, cognom, curs);
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click_1(object sender, EventArgs e)
        {

        }
    }
}
