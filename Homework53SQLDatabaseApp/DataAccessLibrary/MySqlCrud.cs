using DataAccessLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataAccessLibrary
{
    public class MySqlCrud
    {
        private readonly string connectionString;
        private MySqlDataAccess db = new MySqlDataAccess();

        public MySqlCrud(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public List<BasicPersonModel> GetAllPeople()
        {
            string sql = "select Id, FirstName, LastName from People;";

            return db.LoadData<BasicPersonModel, dynamic>(sql, new { }, connectionString);
        }

        public FullPersonModel GetFullPersonById(int id)
        {
            string sql = "select Id, FirstName, LastName from People where Id = @Id;";

            FullPersonModel output = new FullPersonModel();

            output.BasicInfo = db.LoadData<BasicPersonModel, dynamic>(sql, new { Id = id }, connectionString).FirstOrDefault();

            if (output.BasicInfo == null)
            {
                return null;
            }

            sql = @"select a.* 
                    from Addresses a
                    inner join PeopleAddresses pa on pa.AddressId = a.Id
                    where pa.PersonId = @Id;";

            output.Addresses = db.LoadData<AddressModel, dynamic>(sql, new { Id = id }, connectionString);

            sql = @"select e.*
                    from Employers e 
                    inner join PeopleEmployers pe on pe.EmployerId = e.Id 
                    where pe.PersonId = @Id;";

            output.Employers = db.LoadData<EmployerModel, dynamic>(sql, new { Id = id }, connectionString);

            return output;
        }

        public void CreatePerson(FullPersonModel person)
        {
            string sql = "insert into People (FirstName, LastName) values (@FirstName, @LastName);";

            db.SaveData(
                sql,
                new { person.BasicInfo.FirstName, person.BasicInfo.LastName },
                connectionString);

            sql = "select Id from People where FirstName = @FirstName and LastName = @LastName;";

            int personId = db.LoadData<IdLookupModel, dynamic>(
                sql,
                new { person.BasicInfo.FirstName, person.BasicInfo.LastName },
                connectionString).First().Id;

            foreach (var employer in person.Employers)
            {
                if (employer.Id == 0)
                {
                    sql = "insert into Employers (EmployerName) values (@EmployerName);";
                    db.SaveData(sql, new { employer.EmployerName }, connectionString);

                    sql = "select Id from Employers where EmployerName = @EmployerName;";
                    employer.Id = db.LoadData<IdLookupModel, dynamic>(
                        sql,
                        new { employer.EmployerName },
                        connectionString).First().Id;
                }

                sql = "insert into PeopleEmployers (PersonId, EmployerId) values (@PersonId, @EmployerId);";
                db.SaveData(sql, new { PersonId = personId, EmployerId = employer.Id }, connectionString);
            }

            foreach (var address in person.Addresses)
            {
                if (address.Id == 0)
                {
                    sql = @"insert into Addresses (StreetAddress, City, State, ZipCode)
                            values (@StreetAddress, @City, @State, @ZipCode)";
                    db.SaveData(
                        sql,
                        new { address.StreetAddress, address.City, address.State, address.ZipCode },
                        connectionString);

                    sql = @"select Id from Addresses
                        where StreetAddress = @StreetAddress
                        and City = @City
                        and State = @State
                        and ZipCode = @ZipCode;";
                    address.Id = db.LoadData<IdLookupModel, dynamic>(
                        sql,
                        new { address.StreetAddress, address.City, address.State, address.ZipCode },
                        connectionString).First().Id;
                }

                sql = "insert into PeopleAddresses (PersonId, AddressId) values (@PersonId, @AddressId);";
                db.SaveData(sql, new { PersonId = personId, AddressId = address.Id }, connectionString);
            }
        }

        public void UpdatePersonName(BasicPersonModel person)
        {
            string sql = "update People set FirstName = @FirstName, lastName = @LastName where Id = @Id;";
            db.SaveData(sql, person, connectionString);
        }

        public void UpdateAddress(AddressModel address)
        {
            string sql = @"update Addresses set 
                            StreetAddress = @StreetAddress,
                            City = @City,
                            State = @State,
                            ZipCode = @ZipCode
                            where Id = @Id;";
            db.SaveData(sql, address, connectionString);
        }
        public void UpdateEmployer(EmployerModel employer)
        {
            string sql = "update Employers set EmployerName = @EmployerName where Id = @Id;";
            db.SaveData(sql, employer, connectionString);
        }

        public void RemoveEmployerFromPerson(int personId, int employerId)
        {
            string sql = "select Id, PersonId, EmployerId from PeopleEmployers where EmployerId = @EmployerId;";
            var links = db.LoadData<PersonEmployerModel, dynamic>(sql, new { EmployerId = employerId }, connectionString);

            sql = "delete from PeopleEmployers where EmployerId = @EmployerId and PersonId = @PersonId;";
            db.SaveData(sql, new { EmployerId = employerId, PersonId = personId }, connectionString);

            if (links.Count == 1)
            {
                sql = "delete from Employers where Id = @EmployerId;";
                db.SaveData(sql, new { EmployerId = employerId }, connectionString);
            }
        }
    }
}
