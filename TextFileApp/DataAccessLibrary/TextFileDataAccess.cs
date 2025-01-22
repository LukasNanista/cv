using DataAccessLibrary.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace DataAccessLibrary
{
    public class TextFileDataAccess
    {
        public List<ContactModel> ReadAllRecords(string textFile)
        {
            if (File.Exists(textFile) == false)
            {
                return new List<ContactModel>();
            }

            var lines = File.ReadAllLines(textFile);
            List<ContactModel> output = new List<ContactModel>();

            foreach (var line in lines)
            {
                ContactModel c = new ContactModel();
                var vals = line.Split(',');

                if (vals.Length < 6)
                {
                    throw new Exception($"Invalid row of data: {line}");
                }

                c.Id = int.Parse(vals[0]);
                c.FirstName = vals[1];
                c.LastName = vals[2];
                c.EmailAddress = vals[3];
                c.PhoneNumber = vals[4];
                c.IsAlive=bool.Parse(vals[5]);

                output.Add(c);
            }

            return output;
        }

        public void WriteAllRecords(List<ContactModel> contacts, string textFile)
        {
            List<string> lines = new List<string>();

            foreach (var c in contacts)
            {
                lines.Add($"{c.Id},{c.FirstName},{c.LastName},{c.EmailAddress},{c.PhoneNumber},{c.IsAlive}");
            }

            File.WriteAllLines(textFile, lines);
        }
    }
}
