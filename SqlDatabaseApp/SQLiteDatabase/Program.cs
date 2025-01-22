using DataAccessLibrary;
using DataAccessLibrary.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;


namespace SQLiteDatabase
{
    public class Program
    {
        static void Main(string[] args)
        {
            SqliteCrud sql = new SqliteCrud(GetConnectionString());

            BasicPersonModel updatedPerson = new BasicPersonModel
            {
                Id = 4,
                FirstName = "Jade",
                LastName = "Burke"
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

            ReadAllPeople(sql);
            //ReadPerson(sql, 1);
            //ReadPerson(sql, 2);
            //ReadPerson(sql, 3);
            //ReadPerson(sql, 4);
            //ReadPerson(sql, 5);
            //CreateNewPerson(sql);
            //ReadAllPeople(sql);
            //UpdatePerson(sql, updatedPerson);
            //UpdateAddress(sql, updatedAddress);
            //UpdateEmployer(sql, updatedEmployer);
            //RemoveEmployerFromPerson(sql, 1, 2);

            TaskFinishedMessage();
            Console.ReadLine();
        }

        private static void RemoveEmployerFromPerson(SqliteCrud sql, int personId, int employerId)
        {
            sql.RemoveEmployerFromPerson(personId, employerId);
        }

        private static void UpdateEmployer(SqliteCrud sql, EmployerModel employer)
        {
            sql.UpdateEmployer(employer);
        }

        private static void UpdateAddress(SqliteCrud sql, AddressModel address)
        {
            sql.UpdateAddress(address);
        }

        private static void UpdatePerson(SqliteCrud sql, BasicPersonModel person)
        {
            sql.UpdatePersonName(person);
        }

        private static void CreateNewPerson(SqliteCrud sql)
        {
            FullPersonModel person = new FullPersonModel
            {
                BasicInfo = new BasicPersonModel
                {
                    FirstName = "Arianne",
                    LastName = "Reynaud"
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

        private static void ReadPerson(SqliteCrud sql, int personId)
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

        private static void ReadAllPeople(SqliteCrud sql)
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
            Console.WriteLine("======================");
            Console.WriteLine("Done Processing SQLite");
        }
    }
}
