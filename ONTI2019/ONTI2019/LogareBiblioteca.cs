using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace ONTI2019
{
    public partial class LogareBiblioteca : Form
    {
        public LogareBiblioteca()
        {
            InitializeComponent();
            
        }
        public static int idUser=1;
        public static string user = "";
        public string Criptare(string parola)
        {
            char[] arr = new char[29];
            for (int i = 0; i < parola.Length; i++)
            {
                arr[i] = parola[i];
                if (arr[i] >= 'a' & arr[i] < 'z')
                    arr[i]++;
                if (arr[i] == 'z')
                    arr[i] = 'a';
                if (arr[i] > 'A' & arr[i] <= 'Z')
                    arr[i]--;
                if (arr[i] == 'A')
                    arr[i] = 'Z';
                if (arr[i] >= '0' & arr[i] <= '9')
                    arr[i] = (char)(9 - ((int)arr[i] - '0') + '0');

            }
            string alou = new string(arr);

            alou = alou.Substring(0, parola.Length);
            return alou;
        }


        public string Decriptare(string parola)
        {
            char[] arr = new char[1000];
            for (int i = 0; i < parola.Length; i++)
            {
                arr[i] = parola[i];
                if (arr[i] > 'a' & arr[i] <= 'z')
                    arr[i]--;
                if (arr[i] == 'a')
                    arr[i] = 'z';
                if (arr[i] >= 'A' & arr[i] < 'Z')
                    arr[i]++;
                if (arr[i] == 'Z')
                    arr[i] = 'A';
                if (arr[i] >= '0' & arr[i] <= '9')
                    arr[i] = (char)(9 - ((int)arr[i] - '0') + '0');

            }
            string alou = new string(arr);


            return alou;
        }
        private void LogareBiblioteca_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            SqlCommand command = new SqlCommand("Select IdUtilizator from Utilizatori",Form1.con);
            SqlDataReader aa = command.ExecuteReader();
            aa.Read();
            int aux = 0;
            aux = Convert.ToInt32(aa[0]);
            aa.Close();
            SqlCommand cmdSel = new SqlCommand("Select IdUtilizator,NumePrenume from Utilizatori Where Email=@p1 and Parola=@p2",Form1.con);
            cmdSel.Parameters.Add("@p1", textBox1.Text);
            cmdSel.Parameters.Add("@p2", Criptare(textBox2.Text));
            SqlDataReader rdr = cmdSel.ExecuteReader();
            if (rdr.Read())
            {
                BibliotecarBiblioteca frm3 = new BibliotecarBiblioteca();
                idUser = Convert.ToInt32(rdr[0]) - aux + 1;
                user = rdr[1].ToString();
                frm3.Show();
            }
            else
                MessageBox.Show("Incorect!");
            rdr.Close();
        }
    }
}
