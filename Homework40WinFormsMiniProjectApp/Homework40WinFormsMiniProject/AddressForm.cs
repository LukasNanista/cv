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
    public partial class AddressForm : Form
    {
        //put data from fields to one string to be sent
        public string FullAddress => $"{streetNameText.Text} {streetNumberText.Text}, {cityText.Text}, {zipCodeText.Text}, {countryText.Text}";

        PersonForm _parent;

        public AddressForm(PersonForm parent)
        {
            InitializeComponent();
            _parent = parent;
        }

        private void createAddressButton_Click(object sender, EventArgs e)
        {
            //send the data from the fields to the list box on person form
            //perhaps WireUpLists() should be here? since we send the data to add to list by clicking this?
            //ignoring the blank fields check for now

            //put data from fields to one string to be sent
            //FullAddress = $"{streetNameText.Text} {streetNumberText.Text}, {cityText.Text}, {zipCodeText.Text}, {countryText.Text}";

            //now link this to person form somehow so I can use
            _parent.SaveAddress(FullAddress);

            //close the form
            this.Close();
        }
    }
}
