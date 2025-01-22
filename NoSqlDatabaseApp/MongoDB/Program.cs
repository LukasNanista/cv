using DataAccessLibrary;
using DataAccessLibrary.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;
using System.Linq;


namespace MongoDB
{
    public class Program
    {
        private static MongoDBDataAccess db;
        private static readonly string tableName = "People";

        static void Main(string[] args)
        {
            db = new MongoDBDataAccess("MongoTest", GetConnectionString());

            //CreatePerson(new PersonModel
            //{
            //    FirstName = "Sage",
            //    LastName = "Burke",
            //    Addresses =
            //    {
            //        new AddressModel
            //        {
            //            StreetAddress="Burke Residence",
            //            City="College Town",
            //            State="Colorado",
            //            ZipCode="12345"
            //        },
            //        new AddressModel
            //        {
            //            StreetAddress="B&R HOT",
            //            City="College Town",
            //            State="Colorado",
            //            ZipCode="12345"
            //        }
            //    },
            //    Employers =
            //    {
            //        new EmployerModel{EmployerName="student"},
            //        new EmployerModel{EmployerName="VersaLife"}
            //    }
            //});

            PersonModel person = new PersonModel
            {
                FirstName = "Isabella",
                LastName = "Roberts",
                Addresses ={
                    new AddressModel
                    {
                        StreetAddress="Roberts Residence",
                        City="College Town",
                        State="Colorado",
                        ZipCode="12345"
                    }
                },
                Employers =
                {
                    new EmployerModel
                    {
                        EmployerName="B&R College"
                    }
                }
            };

            //CreatePerson(person);

            //RemovePerson("657f2cbf-e24f-443d-b3d9-29eb2ea68080");
            //RemoveEmployerFromPerson("VersaLife", "a7595149-1c6a-4053-88a7-008c0f13d9bb");

            //UpdatePersonEmployer("VersaLife", "a7595149-1c6a-4053-88a7-008c0f13d9bb");

            //RemoveAddressFromPerson("Burke Residence", "College Town", "Colorado", "12345", "a7595149-1c6a-4053-88a7-008c0f13d9bb");
            //UpdatePersonAddress("B&R HOT", "College Town", "Colorado", "12345", "a7595149-1c6a-4053-88a7-008c0f13d9bb");
            //UpdatePersonAddress("Burke Residence", "College Town", "Colorado", "12345", "a7595149-1c6a-4053-88a7-008c0f13d9bb");


            GetAllPeople();

            DoneProcessing();
        }
               
        public static void RemovePerson(string id)
        {
            Guid guid = new Guid(id);

            //to get name of deleted so it can be displayed in message
            var person = db.LoadRecordById<PersonModel>(tableName, guid);
            Console.WriteLine($"Deleted {person.FirstName} {person.LastName} from {tableName}");

            db.DeleteRecord<PersonModel>(tableName, guid);
        }

        public static void RemoveAddressFromPerson(string streetAddress, string city, string state, string zipCode, string id)
        {
            Guid guid = new Guid(id);
            var person = db.LoadRecordById<PersonModel>(tableName, guid);

            person.Addresses = person.Addresses.Where(x =>
                x.StreetAddress != streetAddress
                || x.City != city
                || x.State != state
                || x.ZipCode != zipCode)
                .ToList();

            db.UpsertRecord(tableName, person.Id, person);

            Console.WriteLine($"Removed address {streetAddress}, {city}, {state}, {zipCode} from {person.FirstName} {person.LastName}");
        }

        public static void RemoveEmployerFromPerson(string employer, string id)
        {
            Guid guid = new Guid(id);
            var person = db.LoadRecordById<PersonModel>(tableName, guid);

            Console.WriteLine($"Removed {employer} from {person.FirstName} {person.LastName}");

            person.Employers = person.Employers.Where(x => x.EmployerName != employer).ToList();

            db.UpsertRecord(tableName, person.Id, person);

        }

        private static void UpdatePersonAddress(string streetAddress, string city, string state, string zipCode, string id)
        {
            Guid guid = new Guid(id);
            var person = db.LoadRecordById<PersonModel>(tableName, guid);

            AddressModel newAddress = new AddressModel
            {
                StreetAddress = streetAddress,
                City = city,
                State = state,
                ZipCode = zipCode
            };
            int addressCheck = person.Addresses.Where(x =>
                x.StreetAddress == streetAddress
                && x.City == city
                && x.State == state
                && x.ZipCode == zipCode).Count();

            if (addressCheck == 0)
            {
                person.Addresses.Add(newAddress);
                db.UpsertRecord(tableName, person.Id, person);

                Console.WriteLine($"{newAddress.StreetAddress},{newAddress.City}, {newAddress.State}, {newAddress.ZipCode} was added to {person.FirstName} {person.LastName}");
            }
            else
            {
                Console.WriteLine($"{person.FirstName} {person.LastName} already has this address listed");
            }
        }

        private static void UpdatePersonEmployer(string employer, string id)
        {
            Guid guid = new Guid(id);
            var person = db.LoadRecordById<PersonModel>(tableName, guid);
            int employerCheck = person.Employers.Where(x => x.EmployerName == employer).Count();

            //check if emloyer is already in list
            if (employerCheck == 0)
            {
                person.Employers.Add(new EmployerModel { EmployerName = employer });
                db.UpsertRecord(tableName, person.Id, person);

                Console.WriteLine($"{employer} was added to {person.FirstName} {person.LastName}");
            }
            else
            {
                Console.WriteLine($"{person.FirstName} {person.LastName} is already employed by {employer}");
            }
        }

        private static void UpdatePersonName(string firstName, string lastName, string id)
        {
            Guid guid = new Guid(id);
            var person = db.LoadRecordById<PersonModel>(tableName, guid);

            Console.WriteLine($"Updated name from {person.FirstName} {person.LastName} to {firstName} {lastName}");

            person.FirstName = firstName;
            person.LastName = lastName;

            db.UpsertRecord(tableName, person.Id, person);
        }

        //let's try just guid as parameter first, then do string version as overload of this
        private static void GetPersonById(string id)
        {
            Guid guid = new Guid(id);
            GetPersonById(guid);
        }

        private static void GetPersonById(Guid id)
        {
            var person = db.LoadRecordById<PersonModel>(tableName, id);

            PrintPerson(person);
        }


        private static void GetAllPeople()
        {
            var people = db.LoadRecords<PersonModel>(tableName);

            Console.WriteLine("===============");
            Console.WriteLine("List of people:");

            foreach (var person in people)
            {
                Console.WriteLine("---------------");
                PrintPerson(person);

                Console.WriteLine("Address(es):");
                foreach (var address in person.Addresses)
                {
                    Console.WriteLine($" - {address.StreetAddress}, {address.City}, {address.State}, {address.ZipCode}");
                }

                Console.WriteLine("Employer(s):");
                foreach (var employer in person.Employers)
                {
                    Console.WriteLine($" - {employer.EmployerName}");
                }
            }

            Console.WriteLine("===============");
        }

        private static void PrintPerson(PersonModel person)
        {
            Console.WriteLine($"Id: {person.Id}");
            Console.WriteLine($"{person.FirstName} {person.LastName}");
        }

        private static void CreatePerson(PersonModel person)
        {
            Console.WriteLine($"Added {person.FirstName} {person.LastName} to {tableName}");

            db.UpsertRecord(tableName, person.Id, person);
        }

        private static void DoneProcessing()
        {
            Console.WriteLine();
            //Console.WriteLine("=======================");
            Console.WriteLine("Done processing MongoDB");
            Console.ReadLine();
        }

        private static string GetConnectionString(string connectionStringName = "Default")
        {
            string output = string.Empty;

            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json");

            var config = builder.Build();

            output = config.GetConnectionString(connectionStringName);

            return output;
        }
    }
}
