using Access;
using System;
using System.Windows.Forms;
using WinFirebase.Models;
using WinFirebase.Utils;
namespace WinFirebase
{
    public partial class Form2 : Form
    {
        public People People { get; private set; }
        public Form2()
        {
            InitializeComponent();
            People = new People
            {
                Id = 0
            };
            TxtName.Text = "";
            ChkActive.Checked = true;
            BtnAction.Text = "Inserir";
            Tag = FormStatus.Insert;

        }

        public Form2(People people)
        {
            InitializeComponent();
            People = people;
            TxtName.Text = people.Name;
            ChkActive.Checked = people.Active;
            BtnAction.Text = "Gravar";
            Tag = FormStatus.Update;
        }

        private void BtnSair_Click(object sender, EventArgs e)
        {
            Tag = FormStatus.Cancel;
            Close();
        }

        private void BtnAction_Click(object sender, EventArgs e)
        {
            using ValidationResult validationResult = Validation();
            if (validationResult.IsValid())
            {
                People.Name = TxtName.Text;
                People.Active = ChkActive.Checked;
                Close();
            }
            else
            {
                MessageBox.Show(validationResult.Content(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Form2_Load(object sender, EventArgs e)
        {        
        }

        private ValidationResult Validation()
        {
            ValidationResult validationResult = new ValidationResult();            
            if (TxtName.Text.Trim().Length == 0)
            {
                validationResult.Messages.Add("Digite o nome completo");
            }
            return validationResult;
        }        
    }
}
