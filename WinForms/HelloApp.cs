using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinForms
{
    public partial class HelloApp : Form
    {
        public HelloApp()
        {
            InitializeComponent();
        }

        private void HelloButton_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(firstNameTextBox.Text))
            {
                MissingName("first");
                firstNameTextBox.Focus();
            }
            else if (string.IsNullOrWhiteSpace(lastNameTextBox.Text))
            {
                MissingName("last");
                lastNameTextBox.Focus();
            }
            else
            {
                MessageBox.Show(
                    $"Hello {firstNameTextBox.Text} {lastNameTextBox.Text}",
                    "Hello Message",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                firstNameTextBox.Text = string.Empty;
                lastNameTextBox.Text = string.Empty;
                firstNameTextBox.Focus();
            }
        }

        private void MissingName(string nameType)
        {
            MessageBox.Show(
                    $"Please enter your {nameType} name",
                    $"Missing {nameType} name",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
        }
    }
}
