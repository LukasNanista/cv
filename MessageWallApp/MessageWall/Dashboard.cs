using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MessageWall
{
    public partial class Dashboard : Form
    {
        private BindingList<string> messages = new BindingList<string>();

        public Dashboard()
        {
            InitializeComponent();

            WireUpLists();
        }

        private void WireUpLists()
        {
            messageListBox.DataSource = messages;

            //if we had list of objects object e.g. List<PersonModel> instead of List<string>
            //it would display just name of the object (over and over), e.g. MessageWall.Dashboard
            //in those cases we want a display name for it in the list
            //messageListBox.DisplayMember = nameof(Dashboard.Text); //or nameof(PersonModel.FirstName)
        }

        private void addMesage_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(messageText.Text))
            {
                MessageBox.Show(
                    "Please enter a message before trying to add it to the list.",
                    "Blank Message Field",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
            else
            {
                messages.Add(messageText.Text);
                messageText.Text = "";
            }

            messageText.Focus();
        }
    }
}
