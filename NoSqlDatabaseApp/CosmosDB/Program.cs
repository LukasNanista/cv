using DataAccessLibrary;
using DataAccessLibrary.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;


namespace CosmosDB
{
    public class Program
    {
        private static CosmosDBDataAccess db;

        static async Task Main(string[] args)
        {
            var c = GetCosmosInfo();
            db = new CosmosDBDataAccess(c.endpointUrl, c.primaryKey, c.databaseName, c.containerName);

            PersonModel person = new PersonModel
            {
                FirstName = "Sage",
                LastName = "Burke",
                Addresses =
                {
                    new AddressModel
                    {
                        StreetAddress = "B&R HOT Sorority",
                        City = "College Town",
                        State = "Colorado",
                        ZipCode = "12345"
                    }
                },
                Employers =
                {
                    new EmployerModel
                    {
                        EmployerName = "student"
                    }
                }
            };
            PersonModel person2 = new PersonModel
            {
                FirstName = "Jade",
                LastName = "Burke",
                Addresses =
                {
                    new AddressModel
                    {
                        StreetAddress = "Burke Residence",
                        City = "College Town",
                        State = "Colorado",
                        ZipCode = "12345"
                    }
                },
                Employers =
                {
                    new EmployerModel
                    {
                        EmployerName = "B&R College"
                    }
                }
            };
            PersonModel person3 = new PersonModel
            {
                FirstName = "test",
                LastName = "testovic"
            };

            AddressModel address = new AddressModel
            {
                StreetAddress = "Burke Residence",
                City = "College Town",
                State = "Colorado",
                ZipCode = "12345"
            };
            AddressModel address2 = new AddressModel
            {
                StreetAddress = "B&R HOT Sorority",
                City = "College Town",
                State = "Colorado",
                ZipCode = "12345"
            };
            AddressModel address3 = new AddressModel
            {
                StreetAddress = "test",
                City = "bla",
                State = "CA",
                ZipCode = "98765"
            };

            //await CreatePersonAsync(person);
            //await CreatePersonAsync(person2);
            //await CreatePersonAsync(person3);

            //await RemovePerson("e288ebc1-579f-4bda-8286-086e8ed8f866", "testovic");

            //await UpdatePersonLastNameAsync("Sunny", "3b01668c-d543-439f-8ffd-072b2197b3ad");
            //await UpdatePersonFirstNameAsync("Maya Victoria", "5d3a3513-38db-4f3a-a43d-94e5c75c4b49");
            //await UpdatePersonNameAsync("Maya", "Reese", "5d3a3513-38db-4f3a-a43d-94e5c75c4b49");

            //await UpdatePersonAddressAsync(address, "5bed2041-8dfd-4f01-be60-c8acae6a3689");
            //await RemoveAddressFromPersonAsync(address, "5bed2041-8dfd-4f01-be60-c8acae6a3689");

            //await RemoveAddressFromPersonAsync(address, "ae85a107-c32e-4c36-9098-a4bc0158551f");
            //await UpdatePersonAddressAsync(address, "ae85a107-c32e-4c36-9098-a4bc0158551f");
            //await UpdatePersonAddressAsync(address2, "ae85a107-c32e-4c36-9098-a4bc0158551f");
            //await UpdatePersonAddressAsync(address3, "ae85a107-c32e-4c36-9098-a4bc0158551f");
            //await UpdatePersonAddressAsync("Burke Residence", "College Town", "Colorado", "12345", "ae85a107-c32e-4c36-9098-a4bc0158551f");
            //await RemoveAddressFromPersonAsync("Burke Residence", "College Town", "Colorado", "12345", "ae85a107-c32e-4c36-9098-a4bc0158551f");

            //await UpdatePersonEmployerAsync("student", "ae85a107-c32e-4c36-9098-a4bc0158551f");
            //await UpdatePersonEmployerAsync("VersaLife", "ae85a107-c32e-4c36-9098-a4bc0158551f");

            //await RemoveEmployerFromPersonAsync("versalife", "ae85a107-c32e-4c36-9098-a4bc0158551f");

            await GetAllPeopleAsync();


            //try both versions of get by id - works ok
            //await GetPersonByIdAsync("ae85a107-c32e-4c36-9098-a4bc0158551f");
            //await GetPersonByIdAsync(new Guid("5bed2041-8dfd-4f01-be60-c8acae6a3689"));

            DoneProcessing();
        }

        //first try it without id check - will probably get exception, since id will be invalid
        //how would we even ask for it - certainly cannot use the pattern for checking addresses/employers
        public static async Task RemovePerson(string id, string lastName)
        {
            Guid guid = new Guid(id);
            var person = await db.LoadRecordByIdAsync<PersonModel>(guid);

            //here would be check for valid id if I knew how :D

            await db.DeleteRecordAsync<PersonModel>(id, lastName);

            //how to get name of db? {db} gets me DataAccessLibrary.CosmosDBDataAccess
            Console.WriteLine($"Deleted {person.FirstName} {person.LastName} from database");
        }

        public static async Task RemoveAddressFromPersonAsync(string streetAddress, string city, string state, string zipCode, string id)
        {
            AddressModel address = new AddressModel
            {
                StreetAddress = streetAddress,
                City = city,
                State = state,
                ZipCode = zipCode
            };

            await RemoveAddressFromPersonAsync(address, id);
        }

        public static async Task RemoveAddressFromPersonAsync(AddressModel address, string id)
        {
            Guid guid = new Guid(id);
            var person = await db.LoadRecordByIdAsync<PersonModel>(guid);

            int addressCheck = person.Addresses.Where(x =>
                x.StreetAddress.ToLower() == address.StreetAddress.ToLower()
                && x.City.ToLower() == address.City.ToLower()
                && x.State.ToLower() == address.State.ToLower()
                && x.ZipCode.ToLower() == address.ZipCode.ToLower())
                .Count();

            if (addressCheck == 0)
            {
                Console.WriteLine($"{person.FirstName} {person.LastName} does not have this address listed");
            }
            else
            {
                person.Addresses = person.Addresses.Where(x =>
                x.StreetAddress.ToLower() != address.StreetAddress.ToLower()
                || x.City.ToLower() != address.City.ToLower()
                || x.State.ToLower() != address.State.ToLower()
                || x.ZipCode.ToLower() != address.ZipCode.ToLower())
                .ToList();

                await db.UpsertRecordAsync(person);
                
                Console.WriteLine($"{address.StreetAddress}, {address.City}, {address.State}, {address.ZipCode} was removed from {person.FirstName} {person.LastName}");
            }
        }
                
        private static async Task RemoveEmployerFromPersonAsync(string employer, string id)
        {
            Guid guid = new Guid(id);
            var person = await db.LoadRecordByIdAsync<PersonModel>(guid);
            int employerCheck = person.Employers.Where(x => x.EmployerName.ToLower() == employer.ToLower()).Count();

            if (employerCheck == 0)
            {

                Console.WriteLine($"{person.FirstName} {person.LastName} does not work at {employer}");
            }
            else
            {
                Console.WriteLine($"Removed {employer} from {person.FirstName} {person.LastName}");

                person.Employers = person.Employers.Where(x => x.EmployerName.ToLower() != employer.ToLower()).ToList();

                await db.UpsertRecordAsync(person);
            }

        }

        private static async Task UpdatePersonAddressAsync(string streetAddress, string city, string state, string zipCode, string id)
        {
            AddressModel address = new AddressModel
            {
                StreetAddress = streetAddress,
                City = city,
                State = state,
                ZipCode = zipCode
            };
            await UpdatePersonAddressAsync(address, id);
        }

        private static async Task UpdatePersonAddressAsync(AddressModel address, string id)
        {
            Guid guid = new Guid(id);
            var person = await db.LoadRecordByIdAsync<PersonModel>(guid);

            int addressCheck = person.Addresses.Where(x =>
                x.StreetAddress.ToLower() == address.StreetAddress.ToLower()
                && x.City.ToLower() == address.City.ToLower()
                && x.State.ToLower() == address.State.ToLower()
                && x.ZipCode.ToLower() == address.ZipCode.ToLower())
                .Count();

            if (addressCheck == 0)
            {
                person.Addresses.Add(address);
                await db.UpsertRecordAsync(person);

                Console.WriteLine($"{address.StreetAddress}, {address.City}, {address.State}, {address.ZipCode} was added to {person.FirstName} {person.LastName}");
            }
            else
            {
                Console.WriteLine($"{person.FirstName} {person.LastName} already has this address listed");
            }
        }

        private static async Task UpdatePersonEmployerAsync(string employer, string id)
        {
            Guid guid = new Guid(id);
            var person = await db.LoadRecordByIdAsync<PersonModel>(guid);
            int employerCheck = person.Employers.Where(x => x.EmployerName.ToLower() == employer.ToLower()).Count();

            if (employerCheck == 0)
            {
                person.Employers.Add(new EmployerModel { EmployerName = employer });
                await db.UpsertRecordAsync(person);

                Console.WriteLine($"{employer} was added to {person.FirstName} {person.LastName}");
            }
            else
            {
                Console.WriteLine($"{person.FirstName} {person.LastName} is already employed by {employer}");
            }
        }

        private static async Task UpdatePersonLastNameAsync(string lastName, string id)
        {
            Guid guid = new Guid(id);
            var person = await db.LoadRecordByIdAsync<PersonModel>(guid);

            Console.WriteLine($"Updated last name from {person.LastName} to {lastName}");

            person.LastName = lastName;

            await db.UpsertRecordAsync(person);
        }

        private static async Task UpdatePersonFirstNameAsync(string firstName, string id)
        {
            Guid guid = new Guid(id);
            var person = await db.LoadRecordByIdAsync<PersonModel>(guid);

            Console.WriteLine($"Updated first name from {person.FirstName} to {firstName}");

            person.FirstName = firstName;

            await db.UpsertRecordAsync(person);
        }

        private static async Task UpdatePersonNameAsync(string firstName, string lastName, string id)
        {
            Guid guid = new Guid(id);
            var person = await db.LoadRecordByIdAsync<PersonModel>(guid);

            Console.WriteLine($"Updated name from {person.FirstName} {person.LastName} to {firstName} {lastName}");

            person.FirstName = firstName;
            person.LastName = lastName;

            await db.UpsertRecordAsync(person);
        }

        private static async Task GetPersonByIdAsync(string id)
        {
            Guid guid = new Guid(id);
            await GetPersonByIdAsync(guid);
        }

        private static async Task GetPersonByIdAsync(Guid id)
        {
            var person = await db.LoadRecordByIdAsync<PersonModel>(id);

            PrintPerson(person);
        }

        private static async Task GetAllPeopleAsync()
        {
            var people = await db.LoadRecordsAsync<PersonModel>();

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

        private static async Task CreatePersonAsync(PersonModel person)
        {
            await db.UpsertRecordAsync(person);
        }


        private static (string endpointUrl, string primaryKey, string databaseName, string containerName) GetCosmosInfo()
        {
            (string endpointUrl, string primaryKey, string databaseName, string containerName) output;

            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json");

            var config = builder.Build();

            output.endpointUrl = config.GetValue<string>("CosmosDB:EndpointUrl");
            output.primaryKey = config.GetValue<string>("CosmosDB:PrimaryKey");
            output.databaseName = config.GetValue<string>("CosmosDB:DatabaseName");
            output.containerName = config.GetValue<string>("CosmosDB:ContainerName");

            return output;
        }

        private static void DoneProcessing()
        {
            Console.WriteLine();
            Console.WriteLine("Done processing CosmosDB");
            Console.ReadLine();
        }
    }
}
