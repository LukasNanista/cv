using Library;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace WpfMiniProject
{
    /// <summary>
    /// Interaction logic for AddressInfo.xaml
    /// </summary>
    public partial class AddressInfo : Window
    {
        ISaveAddress _parent;

        public AddressInfo(ISaveAddress parent)
        {
            InitializeComponent();

            _parent = parent;

            streetAddressText.Focus();
        }

        private void saveRecord_Click(object sender, RoutedEventArgs e)
        {
            AddressModel address = new AddressModel
            {
                StreetAddress = streetAddressText.Text,
                City = cityText.Text,
                ZipCode = zipCodeText.Text,
                Country = countryText.Text
            };

            _parent.SaveAddress(address);

            this.Close();
        }
    }
}
