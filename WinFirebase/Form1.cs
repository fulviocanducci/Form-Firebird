using Access;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Windows.Forms;
using WinFirebase.Utils;
namespace WinFirebase
{
    public partial class Form1 : Form
    {
        public DatabaseContext DatabaseContext { get; private set; }
        private void LoadDatabaseContext()
        {
            DbContextOptionsBuilder<DatabaseContext> configuration = new DbContextOptionsBuilder<DatabaseContext>();
            configuration.UseFirebird(Connection.Configuration);
            DatabaseContext = new DatabaseContext(configuration.Options);
        }
        private void LoadDataGridView()
        {
            dataGridView1.DataSource = DatabaseContext.People.All();
            dataGridView1.Update();
        }

        private void InsertOrUpdate(People data = null)
        {
            Form2 form = data == null
                ? new Form2()
                : new Form2(data);
            form.ShowDialog();
            FormStatus formStatus = (FormStatus)form.Tag;
            if (formStatus != FormStatus.Cancel)
            {
                People people = form.People;
                if (formStatus == FormStatus.Insert)
                {
                    DatabaseContext.People.Add(people);
                }
                else if (formStatus == FormStatus.Update)
                {
                    DatabaseContext.People.Update(people);
                }
                DatabaseContext.SaveChanges();
                DatabaseContext.Detach(people);
                LoadDataGridView();
            }
            form?.Dispose();
        }

        public Form1()
        {
            InitializeComponent();
            LoadDatabaseContext();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            LoadDataGridView();
        }

        private void BtnAction_Click(object sender, EventArgs e)
        {
            InsertOrUpdate();
        }

        private void BtnEnd_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                object value = dataGridView1.Rows[e.RowIndex].Cells[0].Value;
                if (value != null && int.TryParse(value.ToString(), out int id))
                {
                    People people = DatabaseContext.People.Get(x => x.Id == id);
                    InsertOrUpdate(people);
                }
            }
        }
    }
}