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
using System.IO;
using System.Globalization;

namespace ONTI2019
{
    public partial class Form1 : Form
    {
        public static string path="";
        public static SqlConnection con = new SqlConnection("Data Source = (LocalDB)\\MSSQLLocalDB; AttachDbFilename=G:\\C# Rezolvari\\ONTI 2019\\ONTI2019\\ONTI2019\\Biblioteca.mdf;Integrated Security = True; Connect Timeout = 30");

        //SqlConnection con = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\\Biblioteca.mdf;Integrated Security=True;Connect Timeout=30");
        public Form1()
        {
            InitializeComponent();
            con.Open();
        }

        public string Criptare(string parola)
        {
            char[] arr = new char[1000];
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
                    arr[i] = (char)(9 - ((int)arr[i] - '0') +'0');

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
        private void Form1_Load(object sender, EventArgs e)
        {
            path = System.IO.Directory.GetCurrentDirectory();
            path += "\\ONTI_C#_2019_resurse\\";
            //label1.Text = path;
             StreamReader sr = new StreamReader(path+"carti.txt");
            SqlCommand cmd1 = new SqlCommand("Delete from Utilizatori",con);
            Bitmap imgBg = new Bitmap(path + "\\Imagini\\altele\\biblioteca1.jpg");
            imgBg = new Bitmap(imgBg, pictureBox1.Width, pictureBox1.Height);
            pictureBox1.Image = imgBg;
           // cmd1.ExecuteNonQuery();
            string line = "";
            while((line=sr.ReadLine())!=null)
            {
                SqlCommand cmd = new SqlCommand("Insert into Carti(Titlu,Autor,Nrpag) values (@param1,@param2,@param3)", con);
                cmd.Parameters.Add("@param1",line.Split(';')[0]);
                cmd.Parameters.Add("@param2", line.Split(';')[1]);
                cmd.Parameters.Add("@param3", line.Split(';')[2]);
               // cmd.ExecuteNonQuery();

            }

            sr = new StreamReader(path + "imprumuturi.txt");

            while ((line = sr.ReadLine()) != null)
            {
                SqlCommand cmd = new SqlCommand("Insert into Imprumuturi(IdCititor,IdCarte,DataImprumut,DataRestituire) values (@param1,@param2,@param3,@param4)", con);
                 DateTime dt1 = DateTime.ParseExact(line.Split(';')[2], "MM/dd/yyyy hh/mm/ss tt",CultureInfo.InvariantCulture);
                DateTime dt2 = new DateTime(1970, 1, 1);

                if (line.Split(';')[3] != "NULL")
                     dt2 = DateTime.ParseExact(line.Split(';')[3], "MM/dd/yyyy hh/mm/ss tt", CultureInfo.InvariantCulture);


                cmd.Parameters.Add("@param1",Convert.ToInt32(line.Split(';')[0]));
                cmd.Parameters.Add("@param2", Convert.ToInt32(line.Split(';')[1]));
                cmd.Parameters.Add("@param3",dt1);
                cmd.Parameters.Add("@param4", dt2);
 

               //cmd.ExecuteNonQuery();

            }
            sr = new StreamReader(path+"rezervari.txt");
            while ((line = sr.ReadLine()) != null)
            {
                int id1 = 0, id2 = 0;
                DateTime dt;
                int status = 0;
                id1 = Convert.ToInt32(line.Split(';')[0]);
                id2 = Convert.ToInt32(line.Split(';')[1]);
                dt = DateTime.ParseExact(line.Split(';')[2], "MM/dd/yyyy hh/mm/ss tt",CultureInfo.InvariantCulture);
                status = Convert.ToInt32(line.Split(';')[3]);
                SqlCommand cmd = new SqlCommand("Insert into Rezervari(IdCititor, IdCarte,DataRezervare,StatusRezervare) values (@p1,@p2,@p3,@p4)",con);
                cmd.Parameters.Add("@p1", id1);
                cmd.Parameters.Add("@p2", id2);
                cmd.Parameters.Add("@p3", dt);
                cmd.Parameters.Add("@p4", status);
            //    cmd.ExecuteNonQuery();
            }
            sr = new StreamReader(path + "utilizatori.txt");
            while ((line = sr.ReadLine()) != null)
            {
                int tip = 0;
                string nume = "", email = "", parola = "";
                tip = Convert.ToInt32(line.Split(';')[0]);
                nume = line.Split(';')[1];
                email = line.Split(';')[2];
                parola = line.Split(';')[3];
                SqlCommand cmd = new SqlCommand("Insert into Utilizatori(TipUtilizator,NumePrenume,Email,Parola) values (@p1,@p2,@p3,@p4)",con);
                cmd.Parameters.Add("@p1", tip);
                cmd.Parameters.Add("@p2", nume);
                cmd.Parameters.Add("@p3", email);
                cmd.Parameters.Add("@p4", Criptare(parola));
               // listBox1.Items.Add(Criptare(parola)+'\n');
              // cmd.ExecuteNonQuery();

            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            LogareBiblioteca frm2 = new LogareBiblioteca();
            frm2.Show();
        }
    }
}
