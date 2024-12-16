using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Homework40WinFormsMiniProject
{
    public partial class PersonForm : Form
    {
        BindingList<string> addresses = new BindingList<string>();

        public PersonForm()
        {
            InitializeComponent();
            WireUpLists();
        }

        private void WireUpLists()
        {
            addressListBox.DataSource = addresses;
            addressListBox.DisplayMember = nameof(AddressForm.FullAddress);
        }

        public void SaveAddress(string address)
        {
            addresses.Add(address);
        }

        private void addAddressButton_Click(object sender, EventArgs e)
        {
            new AddressForm(this).Show();

            //same as above, looks like
            //AddressForm form = new AddressForm();
            //form.Show();

            //Show() and ShowDialog(), at least here, apparently do the same thing
        }
    }
}
