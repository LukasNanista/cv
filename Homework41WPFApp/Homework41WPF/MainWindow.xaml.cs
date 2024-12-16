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

//task: Build a WPF appplication that has a simple data-entry screen with First and Last name fields. Have a button say "Hi {FN} {LN}" when pressed.
//can be a pop-up or can write it on a screen somewhere (preferable)

/* task overview:
 * 1. create WPF app - DONE
 * 2. create first + last name fields (with labels) - DONE
 * 3. create button that says "Hi {FN} {LN}" when pressed - DONE
 * 
 * all tasks complete
 */

namespace Homework41WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            firstNameText.Focus();
        }

        private void sayHello_Click(object sender, RoutedEventArgs e)
        {
            helloText.Text=$"Hello {firstNameText.Text} {lastNameText.Text}";
            
            firstNameText.Text=String.Empty;
            lastNameText.Text=String.Empty;
            
            firstNameText.Focus();
        }
    }
}
