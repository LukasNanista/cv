using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

//task: Build a WinForms application that has a simple data-entry screen with First and Last name fields. Have a button say "Hi {FN} {LN}" when pressed.
//add some QoL code, like in the demo app - check for empty fields, move the cursor to the field that is incorrect
//e.g. filled FN, but unfilled LN sets cursor to LN

/* task overview:
 * 1. create winform app - DONE
 * 2. create fields (with labels) for first and last names - DONE
 * 3. create button that sends message when clicked - DONE
 * 4. add checks for blank fields and focus cursor to the field with error - DONE
 * 
 * all tasks complete
 */

namespace Homework39WinForms
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
            Application.Run(new HelloApp());
        }
    }
}
