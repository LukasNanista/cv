using DataAccessLibrary;
using DataAccessLibrary.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;

//pro tip: Don't overcomplicate data access. Sometimes a simple text file is the best option.

namespace TextFileUI
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

            ContactModel user1 = new ContactModel();
            user1.FirstName = "Tim";
            user1.LastName = "Corey";
            user1.EmailAddresses.Add("tim@iamtimcorey.com");
            user1.EmailAddresses.Add("me@timothycorey.com");
            user1.PhoneNumbers.Add("555-1212");
            user1.PhoneNumbers.Add("555-1234");

            ContactModel user2 = new ContactModel();
            user2.FirstName = "Charity";
            user2.LastName = "Corey";
            user2.EmailAddresses.Add("nope@aol.com");
            user2.EmailAddresses.Add("me@timothycorey.com");
            user2.PhoneNumbers.Add("555-1212");
            user2.PhoneNumbers.Add("555-9876");

            CreateContact(user1);
            CreateContact(user2);

            //UpdateContactsFirstName("Timothy");

            //RemovePhoneNumberFromUser("555-1212");

            //RemoveUser();

            GetAllContacts();

            Console.WriteLine("Done processing text file");

            Console.ReadLine();
        }
        
        //regarding every method here that should have taken id:
        //now this a bit iffy, since it takes the first entry that matches the name - but that's what we get for not creating id parameter :)
        //we are making a shitty simulation of UI, where we would e.g. select entire contact from listBox
        //here we are taking the very first contact that matches our criteria (the literal first contact in the file, for delete)

        private static void RemoveUser()
        {
            var contacts = db.ReadAllRecords(textFile);
            contacts.RemoveAt(0);
            db.WriteAllRecords(contacts, textFile);
        }

        public static void RemovePhoneNumberFromUser(string phoneNumber)
        {
            var contacts = db.ReadAllRecords(textFile);
            contacts[0].PhoneNumbers.Remove(phoneNumber);
            db.WriteAllRecords(contacts, textFile);
        }

        private static void UpdateContactsFirstName(string firstName)
        {
            var contacts = db.ReadAllRecords(textFile);

            contacts[0].FirstName = firstName;
            db.WriteAllRecords(contacts, textFile);
        }

        private static void GetAllContacts()
        {
            var contacts = db.ReadAllRecords(textFile);

            foreach (var contact in contacts)
            {
                Console.WriteLine($"{contact.FirstName} {contact.LastName}");
            }
        }

        private static void CreateContact(ContactModel contact)
        {
            var contacts = db.ReadAllRecords(textFile);

            contacts.Add(contact);

            db.WriteAllRecords(contacts, textFile);
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
