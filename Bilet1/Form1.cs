using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Bilet1
{
    public partial class Form1 : Form
    {
        Job job;
        public Form1()
        {
            InitializeComponent();
            job = new Job("Programator");
        }

        private void btnAdauga_Click(object sender, EventArgs e)
        {
            Form2 form2 = new Form2();
            form2.ShowDialog();
            if(form2.DialogResult == DialogResult.OK)
            {
                string nume = form2.tbNume.Text;
                int varsta = int.Parse(form2.tbVarsta.Text);
                int raspunsuri = int.Parse(form2.nudRaspunsuri.Text);
                DateTime data = form2.datePicker.Value;

                Interviu i = new Interviu(nume, varsta, raspunsuri, data);
                job.Jobs.Add(i);

                ListViewItem itm = new ListViewItem(nume);
                itm.SubItems.Add(varsta.ToString());
                itm.SubItems.Add(raspunsuri.ToString());
                itm.SubItems.Add(i.Nota.ToString());
                itm.SubItems.Add(data.ToString());

                listView1.Items.Add(itm);
                listView1.Refresh();
            }
        }

        private void btnStergere_Click(object sender, EventArgs e)
        {
            if(listView1.SelectedItems.Count == 1)
            {
                ListViewItem itm = listView1.SelectedItems[0];
                int poz = itm.Index;
                itm.Remove();
                job.Jobs.RemoveAt(poz);
            }
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string mesaj = "Lista interviuri:";
            foreach (Interviu i in job.Jobs)
                mesaj += $"\n{i.Nume}, cu nota {i.Nota}";
            File.WriteAllText("interviews.txt", mesaj);
            FileStream fs = new FileStream("interviews.txt", FileMode.Create);
            BinaryFormatter bf = new BinaryFormatter();
            bf.Serialize(fs, mesaj);
            fs.Close();
        }

        private void printToolStripMenuItem_Click(object sender, EventArgs e)
        {
            printPreviewDialog1.Document = printDocument1;
            printPreviewDialog1.ShowDialog();
        }

        private void printPreviewDialog1_Load(object sender, EventArgs e)
        {

        }

        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            e.Graphics.DrawString("Lista interviewuri:", Font, Brushes.Black, 20, 20);
            int inaltime = 40;
            foreach(Interviu i in job.Jobs)
            {
                e.Graphics.DrawString($"{i.ToString()}", Font, Brushes.Black, 20, inaltime);
                inaltime += 20;
            }
        }

        private void btnConnect_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;Initial Catalog=Interviews;Integrated Security=True;Pooling=False");
            con.Open();
            SqlDataAdapter adapter = new SqlDataAdapter("select * from INTERV", con);
            DataSet ds = new DataSet();
            adapter.Fill(ds);
            dataGridView1.DataSource = ds;
            dataGridView1.DataMember = ds.Tables[0].TableName;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'interviewsDataSet.INTERV' table. You can move, or remove it, as needed.
            //this.iNTERVTableAdapter.Fill(this.interviewsDataSet.INTERV);

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
