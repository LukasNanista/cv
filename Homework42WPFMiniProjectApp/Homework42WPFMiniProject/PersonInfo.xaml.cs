using HomeworkLibrary;
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

//task: Build a WPF application with two forms. Create a form that takes in a person's info and another that takes in address info (multiple per person).

//person info - first + last name + list of addresses from second form
//address info - street (+number), city, zip, country

//will create std 2.0 library for person, address models + interface for transfering addresses to person form
//essentially transfering model winform miniproject to get more xp with that shit

/* task overview:
 * 1. create WPF app - DONE
 * 2. create form for person info + button to add adress(es) - DONE
 * 3. create form for address info - DONE
 * 3. create person model - DONE
 * 4. create address model - DONE
 * 5. create interface for tranfering addresses - DONE
 * 6. wire up add address button on person info form - DONE
 * 7. wire up save address on address info form - DONE
 * 
 * all tasks complete
 * 
 */

namespace Homework42WPFMiniProject
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
