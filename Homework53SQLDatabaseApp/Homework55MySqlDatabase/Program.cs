using DataAccessLibrary;
using DataAccessLibrary.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;

//task: Build a simple databease in MySQL that holds People, Addresses, and Employers. Make sure it builds and that you can load and save data in C#.

//will actually do it with MariaDB because MySQL tools from Oracle don't support VS2022, but it works the same

/* task overview:
 * DONE - create separate console app ui for mysql version
 * DONE - link class library to this ui
 * DONE - recreate sql tables in mysql/mariaDB
 * DONE - add necessary nuget packages to mysql ui and library
 * DONE - create appsettings.json  in the ui and add connections string for mysql db there and configure copy if newer
 * DONE - create mysql versions of SqlCrud and SqlDataAccess in the library
 * DONE - create mysql versions of methods in main()
 * 
 * all tasks complete
 */

namespace Homework55MySqlDatabase
{
    public class Program
    {
        static void Main(string[] args)
        {
            MySqlCrud sql = new MySqlCrud(GetConnectionString());

            BasicPersonModel updatedPerson = new BasicPersonModel
            {
                Id = 5,
                FirstName = "Kimberly",
                LastName = "Day"
            };
            AddressModel updatedAddress = new AddressModel
            {
                Id = 6,
                StreetAddress = "CJ's Mansion",
                City = "Los Santos",
                State = "San Andreas",
                ZipCode = "90210"
            };
            EmployerModel updatedEmployer = new EmployerModel
            {
                Id = 5,
                EmployerName = "Vault-Tec Corporation"
            };

            //ReadAllPeople(sql);
            //CreateNewPerson(sql);
            //UpdatePerson(sql, updatedPerson);
            //UpdateAddress(sql, updatedAddress);
            //UpdateEmployer(sql, updatedEmployer);
            //RemoveEmployerFromPerson(sql, 4, 2);

            //ReadAllPeople(sql);

            //ReadPerson(sql, 1);
            //ReadPerson(sql, 2);
            //ReadPerson(sql, 3);
            //ReadPerson(sql, 4);
            //ReadPerson(sql, 5);

            TaskFinishedMessage();
            Console.ReadLine();
        }

        private static void RemoveEmployerFromPerson(MySqlCrud sql, int personId, int employerId)
        {
            sql.RemoveEmployerFromPerson(personId, employerId);
        }

        private static void UpdateEmployer(MySqlCrud sql, EmployerModel employer)
        {
            sql.UpdateEmployer(employer);
        }

        private static void UpdateAddress(MySqlCrud sql, AddressModel address)
        {
            sql.UpdateAddress(address);
        }

        private static void UpdatePerson(MySqlCrud sql, BasicPersonModel person)
        {
            sql.UpdatePersonName(person);
        }

        private static void CreateNewPerson(MySqlCrud sql)
        {
            FullPersonModel person = new FullPersonModel
            {
                BasicInfo = new BasicPersonModel
                {
                    FirstName = "Kimber",
                    LastName = "Day"
                }
            };

            person.Addresses.Add(new AddressModel
            {
                Id = 5,
                StreetAddress = "B&R HOT Sorority House",
                City = "College Town",
                State = "Colorado",
                ZipCode = "12345"
            });

            person.Employers.Add(new EmployerModel { Id = 1, EmployerName = "student" });
            person.Employers.Add(new EmployerModel { EmployerName = "VersaLife" });

            sql.CreatePerson(person);
        }

        private static void ReadPerson(MySqlCrud sql, int personId)
        {
            var person = sql.GetFullPersonById(personId);

            Console.WriteLine($"{person.BasicInfo.FirstName} {person.BasicInfo.LastName}:");

            Console.WriteLine(" - Addresses:");
            foreach (var address in person.Addresses)
            {
                Console.WriteLine(
                    $"{address.Id}: {address.StreetAddress}, {address.City}, {address.State}, {address.ZipCode}");
            }

            Console.WriteLine(" - Employers:");
            foreach (var employer in person.Employers)
            {
                Console.WriteLine($"{employer.Id}: {employer.EmployerName}");
            }

            Console.WriteLine("===============");
            Console.WriteLine();

        }

        private static void ReadAllPeople(MySqlCrud sql)
        {
            var rows = sql.GetAllPeople();

            foreach (var row in rows)
            {
                Console.WriteLine($"{row.Id}: {row.FirstName} {row.LastName}");
            }
        }

        private static string GetConnectionString(string connectionStringName = "Default")
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json");

            var config = builder.Build();

            string output = config.GetConnectionString(connectionStringName);
            return output;
        }

        private static void TaskFinishedMessage()
        {
            Console.WriteLine();
            Console.WriteLine("=============================");
            Console.WriteLine("Done Processing MySQL/MariaDB");
        }
    }
}
