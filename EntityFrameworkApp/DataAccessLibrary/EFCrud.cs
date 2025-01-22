using DataAccessLibrary.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataAccessLibrary
{
    public class EFCrud
    {
        private PersonContext db = new PersonContext();

        public void RemoveUser(int id)
        {
            var person = db.People
                .Include(a=>a.Addresses)
                .Include(e=>e.Employers)
                .Where(p => p.Id == id).FirstOrDefault();

            db.People.Remove(person);
            db.SaveChanges();

        }

        public void UpdatePersonName(int id, string firstName, string lastName)
        {
            var person = db.People.Where(p => p.Id == id).FirstOrDefault();

            person.FirstName = firstName;
            person.LastName = lastName;
            db.SaveChanges();
        }

        public void RemoveEmployer(int id, Employer employer)
        {
            var person = db.People.Where(p => p.Id == id).FirstOrDefault();

            person.Employers.RemoveAll(e => e.EmployerName == employer.EmployerName);
            db.SaveChanges();
        }

        public void UpdateEmployer(int id, Employer employer)
        {
            var person = db.People.Where(p => p.Id == id).FirstOrDefault();

            person.Employers.Add(employer);
            db.SaveChanges();
        }

        public void RemoveAddress(int id, Address address)
        {
            var person = db.People.Where(p => p.Id == id).FirstOrDefault();

            person.Addresses.RemoveAll(a => (a.City == address.City) && (a.State == address.State));
            db.SaveChanges();
        }

        public void UpdateAddress(int id, Address address)
        {
            var person = db.People.Where(p => p.Id == id).FirstOrDefault();
            
            person.Addresses.Add(address);
            db.SaveChanges();
        }

        public void CreatePerson(Person person)
        {
            db.People.Add(person);
            db.SaveChanges();
        }

        public List<Person> GetAllPeople()
        {
            //will use this inefficient query because we will have about 2 records in here
            var output = db.People
                .Include(a => a.Addresses)
                .Include(e => e.Employers)
                .ToList();

            return output;
        }

        public Person GetPersonById(int id)
        {
            return db.People.Where(p => p.Id == id)
                .Include(a => a.Addresses)
                .Include(e => e.Employers)
                .FirstOrDefault();
        }
    }
}
