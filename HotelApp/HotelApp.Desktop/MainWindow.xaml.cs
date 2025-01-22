using HotelAppLibrary.Data;
using HotelAppLibrary.Models;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace HotelApp.Desktop
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly IDatabaseData _db;

        public MainWindow(IDatabaseData db)
        {
            InitializeComponent();
            _db = db;
        }

        private void searchForGuest_Click(object sender, RoutedEventArgs e)
        {
            List<BookingFullModel> bookings = _db.SearchBookings(lastNameText.Text);
            //video at 11:44 - need to fix the fkin datetime bug first, because calendar beyond 12th day is unusable and currently today() would be 15.7.2022
            //(4.8.2022) continuing with en/us date format in windows work-around
            //1. to get this done
            //2. to try and find out, whether this fuckery is in any way done by sql server (hypothesis is that nope, it some kind of fuckery somewhere in the razor component, since it is fucking up on passing date from one razor page to another, chosen from datepicker and that is not stored in any db, also - even in us format, when viewing booking data in VS, it shows eu/sk time format, and is saving it correctly (also, am keeping data when trying to insert dates in eu/sk format that were flipped when saving them in - which further points fingers on that fuckery is likely occuring on the second/booking razor page)
            resultsList.ItemsSource = bookings;
        }

        private void CheckInButton_Click(object sender, RoutedEventArgs e)
        {
            var checkInForm = App.serviceProvider.GetService<CheckInForm>();
            var model = (BookingFullModel)((Button)e.Source).DataContext;

            checkInForm.PopulateCheckInInfo(model);

            checkInForm.Show();
        }
    }
}
