using Library;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
    /// Interaction logic for PersonInfo.xaml
    /// </summary>
    public partial class PersonInfo : Window, ISaveAddress
    {
        BindingList<AddressModel> addresses = new BindingList<AddressModel>();

        public PersonInfo()
        {
            InitializeComponent();

            firstNameText.Focus();

            addressesList.ItemsSource = addresses;
            addressesList.DisplayMemberPath = nameof(AddressModel.FullAddress);
        }

        public void SaveAddress(AddressModel address)
        {
            addresses.Add(address);
        }

        private void addNewAddress_Click(object sender, RoutedEventArgs e)
        {
            new AddressInfo(this).Show();

            
        }
    }
}
