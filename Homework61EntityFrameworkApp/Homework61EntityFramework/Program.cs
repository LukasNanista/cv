using DataAccessLibrary;
using DataAccessLibrary.Models;
using System;
using System.Linq;

//task: Build a simple database using code first. Create models for People, Addresses, and Employers. Make sure it builds and that you can load and save data.

//put data access to a library, not the UI layer

/* task overview:
 * DONE - create console app fo UI
 * DONE - create appsettings.json (and set to copy if newer)
 * DONE - create data access library
 *  DONE - install nuget packages
 *      DONE - Microsoft.EntityFrameworkCore.SqlServer
 *      DONE - Microsoft.EntityFrameworkCore.Tools
 *      DONE - Microsoft.Extensions.Configuration.Json
 *  DONE - create models for
 *      DONE - people (id, FN, LN, list of addresses, list of employers)
 *      DONE - addresses (id, person id, city, state)
 *      DONE - employers (id, person id, employer name)
 *  DONE - create db context (person context)
 * DONE - in package manager console run commands:
 *  DONE - select data access library as default project and set it as startup project (while running commands in PMC)
 *  DONE - Add-Migration CreateDatabase (c.20:00 in video)
 *  DONE - Update-Database (c.32:50 in video)
 * DONE - create crud methods (smaller version will suffice this time, more in line with demo app):
 *  DONE - read all people
 *  DONE - read person by id
 *  DONE - create person
 *  DONE - update person name
 *  DONE - add address to person
 *  DONE - remove address from person
 *  DONE - add employer to person
 *  DONE - remove employer from person
 *  DONE - remove person from db
 * DONE - create methods in UI to call crud methods:
 *  DONE - get all people
 *  DONE - get person by id
 *  DONE - create person
 *  DONE - update person name
 *  DONE - add address to person
 *  DONE - remove address from person
 *  DONE - add employer to person
 *  DONE - remove employer from person
 *  DONE - remove person from db
 * 
 * all tasks complete
 */

namespace Homework61EntityFramework
{
    public class Program
    {
        static void Main(string[] args)
        {
            EFCrud sql = new EFCrud();

            //CreateNewPerson(sql, "Bob", "Page");
            //CreateNewPerson(sql, "Maggie", "Chow", "Hong Kong", "PRC", "VersaLife");
            //CreateNewPerson(sql, "test", "to", "delete", "this", "one");
            //GetPersonById(sql, 2);
            Address address = new Address { City = "NYC", State = "USA" };
            Address address2 = new Address { City = "Los Angeles", State = "USA" };
            Address address3 = new Address { City = "Berlin", State = "Germany" };
            //AddAddress(sql, 1, address);
            //AddAddress(sql, 1, address2);
            //AddAddress(sql, 1, address3);
            //RemoveAddress(sql, 1, address);
            Employer employer = new Employer { EmployerName = "VersaLife" };
            Employer employer2 = new Employer { EmployerName = "Page Industries" };
            //UpdateEmployer(sql, 2, employer);
            //UpdateEmployer(sql, 1, employer2);
            //RemoveEmployer(sql,1,employer);
            //UpdatePersonName(sql, 1, "Robert", "Page");
            //RemovePerson(sql, 4);

            GetAllPeople(sql);

            Console.WriteLine();
            Console.WriteLine("Done processing");
            Console.ReadLine();
        }

        private static void RemovePerson(EFCrud sql, int id)
        {
            sql.RemoveUser(id);
        }

        private static void UpdatePersonName(EFCrud sql, int id, string firstName, string lastName)
        {
            sql.UpdatePersonName(id, firstName, lastName);
            //just quick rename without bells and whistles, does not need additional shit
        }

        private static void RemoveEmployer(EFCrud sql, int id, Employer employer)
        {
            var person = sql.GetPersonById(id);
            var employerExists = person.Employers.Where(e => e.EmployerName == employer.EmployerName).Count() > 0;

            if (employerExists == true)
            {
                sql.RemoveEmployer(id, employer);
            }
            else
            {
                Console.WriteLine($"{person.FirstName} {person.LastName} doesn't have this employer");
            }
        }

        private static void UpdateEmployer(EFCrud sql, int id, Employer employer)
        {
            var person = sql.GetPersonById(id);
            var employerExists = person.Employers.Where(e => e.EmployerName == employer.EmployerName).Count() > 0;

            if (employerExists == false)
            {
                sql.UpdateEmployer(id, employer);
            }
            else
            {
                Console.WriteLine($"{person.FirstName} {person.LastName} already has this employer");
            }
        }

        private static void RemoveAddress(EFCrud sql, int id, Address address)
        {
            var person = sql.GetPersonById(id);

            var addressExists = person.Addresses.Where(a => (a.City == address.City) && (a.State == address.State)).Count() > 0;

            if (addressExists == true)
            {
                sql.RemoveAddress(id, address);
            }
            else
            {
                Console.WriteLine($"{person.FirstName} {person.LastName} doesn't have this address");
            }
        }

        private static void AddAddress(EFCrud sql, int id, Address address)
        {
            var person = sql.GetPersonById(id);

            var addressExists = person.Addresses.Where(a => (a.City == address.City) && (a.State == address.State)).Count() > 0;

            if (addressExists == false)
            {
                sql.UpdateAddress(id, address);
            }
            else
            {
                Console.WriteLine($"{person.FirstName} {person.LastName} already has this address");
            }
        }

        private static void AddEmployer(EFCrud sql, string employer)
        {

        }

        private static void CreateNewPerson(EFCrud sql, string firstName, string lastName, string city, string state, string employer)
        {
            Person person = new Person
            {
                FirstName = firstName,
                LastName = lastName,
                Addresses =
                {
                    new Address{ City = city, State = state }
                },
                Employers =
                {
                    new Employer{ EmployerName = employer }
                }
            };
            sql.CreatePerson(person);
        }

        private static void PrintPerson(Person p)
        {
            Console.WriteLine("--------------------");
            Console.WriteLine($"{p.Id}: {p.FirstName} {p.LastName}");
            Console.WriteLine("--------------------");
            Console.WriteLine("Addresses:");
            foreach (var a in p.Addresses)
            {
                Console.WriteLine($" - {a.City}, {a.State}");
            }
            Console.WriteLine("Employers:");
            foreach (var e in p.Employers)
            {
                Console.WriteLine($" - {e.EmployerName}");
            }
        }

        private static void GetAllPeople(EFCrud sql)
        {
            var people = sql.GetAllPeople();

            foreach (var p in people)
            {
                PrintPerson(p);
            }
        }

        private static void GetPersonById(EFCrud sql, int id)
        {
            Person person = sql.GetPersonById(id);

            if (person != null)
            {
                PrintPerson(person);
            }
            else
            {
                Console.WriteLine("We ain't found shit");
            }
        }
    }
}
