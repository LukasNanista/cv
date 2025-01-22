using DataAccessLibrary;
using DataAccessLibrary.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;
using System.Linq;


namespace SqlDatabase
{
    internal class Program
    {
        static void Main(string[] args)
        {
            SqlCrud sql = new SqlCrud(GetConnectionString());

            BasicPersonModel updatedPerson = new BasicPersonModel
            {
                Id = 4,
                FirstName = "Josy",
                LastName = "Taylor"
            };
            AddressModel updatedAddress = new AddressModel
            {
                Id = 10,
                StreetAddress = "CJ's Mansion",
                City = "Los Santos",
                State = "San Andreas",
                ZipCode = "90210"
            };
            EmployerModel updatedEmployer = new EmployerModel
            {
                Id = 1002,
                EmployerName = "Vault-Tec"
            };

            //ReadAllPeople(sql);
            //ReadPerson(sql, 1);
            ReadPerson(sql, 2);
            //ReadPerson(sql, 3);
            ReadPerson(sql, 4);
            ReadPerson(sql, 1002);
            //CreateNewPerson(sql);
            //UpdatePerson(sql, updatedPerson);
            //UpdateAddress(sql, updatedAddress);
            //UpdateEmployer(sql, updatedEmployer);
            //RemoveEmployerFromPerson(sql, 1, 2);

            //ReadAllPeople(sql);

            TaskFinishedMessage();
            Console.ReadLine();
        }

        private static void RemoveEmployerFromPerson(SqlCrud sql,int personId, int employerId)
        {
            sql.RemoveEmployerFromPerson(personId, employerId);
        }

        //to have complete set of db updates
        private static void UpdateEmployer(SqlCrud sql, EmployerModel employer)
        {
            sql.UpdateEmployer(employer);
        }

        //derivative from UpdatePerson()
        private static void UpdateAddress(SqlCrud sql, AddressModel address)
        {
            sql.UpdateAddress(address);
        }

        //better control than demo app version
        private static void UpdatePerson(SqlCrud sql, BasicPersonModel person)
        {
            sql.UpdatePersonName(person);
        }

        //could remake this one too, but meh, is not pertinent to sql and one example of refactored method suffices
        private static void CreateNewPerson(SqlCrud sql)
        {
            //note here that if the entry already exists and Id is not provided, it will create another entry with new id
            //but when the person is read, it will show the duplicite entry twice but with the first id only
            //was tested on employer versalife, but should work on any of these entries - basicInfo, address, employer

            FullPersonModel person = new FullPersonModel
            {
                BasicInfo = new BasicPersonModel
                {
                    FirstName = "Sage",
                    LastName = "Burke"
                }
            };

            person.Addresses.Add(new AddressModel
            {
                Id = 9,
                StreetAddress = "B&R HOT Sorority House",
                City = "College Town",
                State = "Colorado",
                ZipCode = "12345"
            });
            person.Addresses.Add(new AddressModel
            {
                StreetAddress = "Burke Residence",
                City = "College Town",
                State = "Colorado",
                ZipCode = "12345"
            });

            person.Employers.Add(new EmployerModel { Id = 1, EmployerName = "student" });
            person.Employers.Add(new EmployerModel { EmployerName = "VersaLife" });

            sql.CreatePerson(person);
        }

        private static void ReadPerson(SqlCrud sql, int personId)
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

        private static void ReadAllPeople(SqlCrud sql)
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
            Console.WriteLine("===============");
            Console.WriteLine("Done Processing");
        }
    }
}
