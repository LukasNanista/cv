﻿using DataAccessLibrary;
using DataAccessLibrary.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;
using System.Linq;

//task: Build a CSV file that holds Person information. Load the file into models in C# and save the data from C# back into the CSV file.

//do simpler version of demo app - FN, LN, 1 email, 1 phone number, then try other types - like bool (isAlive), int (age) or dateTime - will need to cast (tryParse) them, since we get them from the file as string

//hell, will create id - nothing fancy, it will have to be put in manually (like everything else), but we can have functional db


/* task overview:
 * DONE - manually create csv file with some records (2 will do - id, FN, LN, email, phone, isAlive bool)
 * DONE - create console app
 *  DONE - manage nuget packages
 *      DONE - Microsoft.Extensions.Configuration.Binder
 *      DONE - Microsoft.Extensions.Configuration.Json
 * DONE - create appsettings.json with file path
 * DONE - create library for data acccess
 * DONE - create contact model in library
 * DONE - create data access class in library
 *  DONE - method to read all contacts
 *  DONE - method to write all contacts
 * DONE - create methods in console ui to:
 *  DONE - initialize config
 *  DONE - create contact (basic version - id input manually like other data)
 *  DONE - read all contacts
 *  DONE - read contact by id
 *  DONE - update contact name (overloads for full name, FN, LN - well probably should name partials update FN/LN but they use full rename with preconfigured arguments)
 *  DONE - update email
 *  DONE - update phone number
 *  DONE - update bool status
 *  DONE - remove email
 *  DONE - remove phone number
 *  DONE - delete contact
 * DONE - (bonus) create method to generate id on contact creation (takes largest in file and +1 or defaults to 1 on new file)
 * 
 * all tasks complete
 */

namespace Homework58TextFile
{
    public class Program
    {
        private static IConfiguration _config;
        private static string textFile;
        private static TextFileDataAccess db = new TextFileDataAccess();

        static void Main(string[] args)
        {
            InitializeConfiguration();
            textFile = _config.GetValue<string>("TextFile");

            //GetContactById(1);
            //GetMaxId();
            //CreateContact(new ContactModel { FirstName = "Another", LastName = "Entry" });
            //RemoveContact(133);
            //RemovePhoneNumberFromContact(2);
            //UpdateContactName(34, "Gilbert", "Bates");
            //UpdateContactFirstName(1, "Maya Victoria");
            //UpdateContactLastName(2, "the White");
            //UpdateContactLivingStatus(2, true);
            //UpdateContactPhoneNumber(2, "555-9876");
            //UpdateContactEmailAddress(34, "gbates@tarant.com");
            //RemoveEmailAddressFromContact(2);

            GetAllContacts();

            OutroMessage();
        }

        private static void RemoveContact(int id)
        {
            var contacts = db.ReadAllRecords(textFile);
            bool isValidId = contacts.Where(x => x.Id == id).Count() > 0;

            if (isValidId == true)
            {
                ContactModel contact = contacts.Where(x => x.Id == id).First();

                contacts.Remove(contact);

                Console.WriteLine($"{contact.FirstName} {contact.LastName} was removed from contacts");

                db.WriteAllRecords(contacts, textFile);
            }
            else
            {
                Console.WriteLine($"We ain't found shit to delete");
            }
        }

        public static void RemoveEmailAddressFromContact(int id)
        {
            var contacts = db.ReadAllRecords(textFile);
            bool isValidId = contacts.Where(x => x.Id == id).Count() > 0;

            if (isValidId == true)
            {

                ContactModel contact = contacts.Where(x => x.Id == id).First();

                if (contact.EmailAddress != String.Empty)
                {
                    contact.EmailAddress = String.Empty;
                    Console.WriteLine($"Email address was removed from {contact.FirstName} {contact.LastName}");

                    db.WriteAllRecords(contacts, textFile);
                }
                else
                {
                    Console.WriteLine($"{contact.FirstName} {contact.LastName} has no email address listed");
                }
            }
            else
            {
                Console.WriteLine($"We ain't found shit");
            }
        }

        private static void RemovePhoneNumberFromContact(int id)
        {
            var contacts = db.ReadAllRecords(textFile);
            bool isValidId = contacts.Where(x => x.Id == id).Count() > 0;

            if (isValidId == true)
            {

                ContactModel contact = contacts.Where(x => x.Id == id).First();

                if (contact.PhoneNumber != String.Empty)
                {
                    contact.PhoneNumber = String.Empty;
                    Console.WriteLine($"Phone number was removed from {contact.FirstName} {contact.LastName}");

                    db.WriteAllRecords(contacts, textFile);
                }
                else
                {
                    Console.WriteLine($"{contact.FirstName} {contact.LastName} has no phone number listed");
                }
            }
            else
            {
                Console.WriteLine($"We ain't found shit");
            }
        }

        public static void UpdateContactLivingStatus(int id, bool isAlive)
        {
            var contacts = db.ReadAllRecords(textFile);
            bool isValidId = contacts.Where(x => x.Id == id).Count() > 0;


            if (isValidId == true)
            {
                ContactModel contact = contacts.Where(x => x.Id == id).First();

                //cannot be bothered to do another layer of checks if contact was alive or dead before update :P
                if (isAlive == true)
                {
                    Console.WriteLine($"{contact.FirstName} {contact.LastName} was resurrected.");

                }
                else
                {
                    Console.WriteLine($"{contact.FirstName} {contact.LastName} died.");
                }

                db.WriteAllRecords(contacts, textFile);
            }
            else
            {
                Console.WriteLine($"We ain't found shit");
            }
        }

        private static void UpdateContactEmailAddress(int id, string email)
        {
            var contacts = db.ReadAllRecords(textFile);
            bool isValidId = contacts.Where(x => x.Id == id).Count() > 0;


            if (isValidId == true)
            {
                ContactModel contact = contacts.Where(x => x.Id == id).First();

                contact.EmailAddress = email;

                Console.WriteLine($"Email address of {contact.FirstName} {contact.LastName} was updated to {email}");

                db.WriteAllRecords(contacts, textFile);
            }
            else
            {
                Console.WriteLine($"We ain't found shit");
            }
        }

        public static void UpdateContactPhoneNumber(int id, string phoneNumber)
        {
            var contacts = db.ReadAllRecords(textFile);
            bool isValidId = contacts.Where(x => x.Id == id).Count() > 0;


            if (isValidId == true)
            {
                ContactModel contact = contacts.Where(x => x.Id == id).First();

                contact.PhoneNumber = phoneNumber;

                Console.WriteLine($"Phone number of {contact.FirstName} {contact.LastName} was updated to {phoneNumber}");

                db.WriteAllRecords(contacts, textFile);
            }
            else
            {
                Console.WriteLine($"We ain't found shit");
            }
        }

        private static void UpdateContactLastName(int id, string lastName)
        {
            UpdateContactName(id, string.Empty, lastName, false, true);
        }

        private static void UpdateContactFirstName(int id, string firstName)
        {
            UpdateContactName(id, firstName, string.Empty, true, false);
        }

        private static void UpdateContactName(int id, string firstName, string lastName)
        {
            UpdateContactName(id, firstName, lastName, true, true);
        }

        private static void UpdateContactName(int id, string firstName, string lastName, bool first, bool last)
        {
            var contacts = db.ReadAllRecords(textFile);
            bool isValidId = contacts.Where(x => x.Id == id).Count() > 0;


            if (isValidId == true)
            {
                ContactModel contact = contacts.Where(x => x.Id == id).First();

                if (first == true && last == true)
                {
                    Console.WriteLine($"{contact.FirstName} {contact.LastName} was renamed to {firstName} {lastName}");
                    contact.FirstName = firstName;
                    contact.LastName = lastName;
                }
                else if (first == true && last == false)
                {
                    Console.WriteLine($"{contact.FirstName} {contact.LastName} was renamed to {firstName} {contact.LastName}");
                    contact.FirstName = firstName;
                }
                else
                {
                    Console.WriteLine($"{contact.FirstName} {contact.LastName} was renamed to {contact.FirstName} {lastName}");
                    contact.LastName = lastName;
                }

                db.WriteAllRecords(contacts, textFile);
            }
            else
            {
                Console.WriteLine($"We ain't found shit");
            }
        }

        private static void GetContactById(int id)
        {
            var contacts = db.ReadAllRecords(textFile);
            bool isValidId = contacts.Where(x => x.Id == id).Count() > 0;

            if (isValidId == true)
            {
                ContactModel contact = contacts.Where(x => x.Id == id).First();
                PrintPerson(contact);
            }
            else
            {
                Console.WriteLine($"We ain't found shit");
            }
        }

        private static void GetAllContacts()
        {
            var contacts = db.ReadAllRecords(textFile);

            Console.WriteLine("List of contacts:");

            foreach (var contact in contacts)
            {
                Console.WriteLine("-----------------");
                PrintPerson(contact);
            }
        }

        private static void PrintPerson(ContactModel c)
        {
            if (c.IsAlive == false)
            {
                Console.WriteLine($"Id {c.Id}: {c.FirstName} {c.LastName} (deceased)");
            }
            else
            {
                Console.WriteLine($"Id {c.Id}: {c.FirstName} {c.LastName}");
            }
            Console.WriteLine($" - Email address: {c.EmailAddress}");
            Console.WriteLine($" - Phone number:  {c.PhoneNumber}");
        }

        //research for id lookup in file
        private static void GetMaxId()
        {
            var contacts = db.ReadAllRecords(textFile);

            //this gets largest id in file, does not matter on what line it is
            var maxId = contacts.Max(x => x.Id);

            //this gets id on the last line
            var lastId = contacts[contacts.LastIndexOf(contacts.Last())].Id;

            Console.WriteLine($"maxId: {maxId}");
            Console.WriteLine($"lastId: {lastId}");
        }

        private static void CreateContact(ContactModel contact)
        {
            var contacts = db.ReadAllRecords(textFile);

            //generates id for new entry, will override id (potentially) provided in argument
            if (contacts.Count > 0)
            {
                contact.Id = contacts.Max(x => x.Id) + 1;
            }
            else
            {
                contact.Id = 1;
            }

            contacts.Add(contact);

            db.WriteAllRecords(contacts, textFile);
        }

        private static void OutroMessage()
        {
            Console.WriteLine();
            Console.WriteLine("=========================");
            Console.WriteLine("Done processing Text file");
            Console.WriteLine("=========================");
            Console.ReadLine();
        }

        private static void InitializeConfiguration()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json");

            _config = builder.Build();
        }
    }
}