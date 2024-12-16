using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

//task: Build a WinForms application with two forms. Create a form that takes in a person's info and another that takes in address info (multiple per person).
//first form will have a list box and a button to open other form where new address is created and added to the first form

/* task overview:
 * 1. create winform app - DONE
 * 2. create form for person's info (first + last name, address - list box for multiple addresses) - this is main form that loads on app start - DONE
 * 3. create form for creating address - DONE 
 * 4. link this form to first form to add to the person's address list box - DONE
 * 
 * all tasks complete
 * 
 * notes:
 * - did not create any button to "save" and/or clear the data from the person - was not in requirements
 */

namespace Homework40WinFormsMiniProject
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new PersonForm());
        }
    }
}
