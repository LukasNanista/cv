using DataAccessLibrary;
using DataAccessLibrary.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;
using System.Linq;

//task: Build a simple database in SQL that holds People, Addresses, and Employers. Make sure it builds and that you can load and save data in C#.

//this is gonna take a while
//perhaps do it in tiers - do just people, test insert, update, delete, read - then add addresses, update methods, test, then finally add employers

/* task overview:
 * DONE - create console app for UI
 * DONE - add sql server project to solution
 * DONE - add core library to solution
 * DONE - compare with demo app and download necessary nuget packages for projects in this solution
 * DONE - create people table (id, FN, LN)
 * DONE - create addresses table (id, street address, city, state, zip code)
 * DONE - create employers table (id, employer name)
 * DONE - create peopleAddresses table (id, person id, addresses id)
 * DONE - create peopleEmployers table (id, person id, employers id)
 * DONE - publish the sql project to turn in into sqlserver db
 * DONE - create appsetting.json with connection string
 * DONE - check appsettigns properties and make sure that "copy to output directory" = copy if newer
 * DONE - create models for the tables in the library
 *  DONE - id lookup model (id)
 *  DONE - basic person model (id, FN, LN)
 *  DONE - full person model (basic person model, list<addressModel>, list<employerModel>)
 *  DONE - address model (id, street address, city, state, zip code)
 *  DONE - employer model (id, employer name)
 *  DONE - person address model (id, person id, address id)
 *  DONE - person employer model (id, person id, employer id)
 * DONE - create class for generic methods to save/load data in th library (SqlDataAccess)
 * DONE - create class for CRUD methods in the library (SqlCrud) (at first add methods analogous to those in demo app)
 *  DONE - create ctor with connections string parameter and turn in into private field for later use
 *  DONE - create method to get all people
 *  DONE - create method to get (full) person by id
 *  DONE - create method to create (full) person
 *  DONE - create method to update person name
 *      DONE - (bonus) create method to update address
 *      DONE - (bonus) create method to update employer name
 *  DONE - create method to remove employer/address from person
 *  DONE - create method to get connection string and call it in main()
 *  //methods will probably take hardcoded ids, like in demo app, unless I find out that it's rather easy to make it create ids dynamically, but will not go out of my way to find out (maybe as bonus objective)
 *  DONE - create method to get all people from db
 *  DONE - create method to get person by id
 *  DONE - create method to create full person
 *  DONE - create method to update person name
 *      DONE - (bonus) create method to update address
 *      DONE - (bonus) create method to update employer name
 *  DONE - create method to remove employer/address from person
 * 
 * all tasks complete
 * 
 */

namespace Homework53SQLDatabase
{
    internal class Program
    {
        static void Main(string[] args)
        {
            SqlCrud sql = new SqlCrud(GetConnectionString());

            BasicPersonModel updatedPerson = new BasicPersonModel
            {
                Id = 1002,
                FirstName = "Kimberly",
                LastName = "Day"
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
            //ReadPerson(sql, 4);
            //CreateNewPerson(sql);
            //UpdatePerson(sql, updatedPerson);
            //UpdateAddress(sql, updatedAddress);
            //UpdateEmployer(sql, updatedEmployer);
            //RemoveEmployerFromPerson(sql, 1, 2);

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
