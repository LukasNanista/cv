using DataAccessLibrary.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace DataAccessLibrary
{
    public class TextFileDataAccess
    {
        // since it's just a text file, we need to keep how we read/write data in sync - same number of columns, same order
        public List<ContactModel> ReadAllRecords(string textFile)
        {
            //we cannot read from file that does not exist yet
            //writing to it is no problem, since it will create it then
            if (File.Exists(textFile) == false)
            {
                return new List<ContactModel>();
            }

            var lines = File.ReadAllLines(textFile);
            List<ContactModel> output = new List<ContactModel>();

            foreach (var line in lines)
            {
                //using short name, because it's just for our foreach
                ContactModel c = new ContactModel();
                //turn our line (a string) into a string array
                //when using split (and maybe related shit) - '' are for char - single character, "" are for string - longer separator
                var vals = line.Split(',');

                //validate our line is legit
                if (vals.Length < 4)
                {
                    throw new Exception($"Invalid row of data: {line}");
                }

                c.FirstName = vals[0];
                c.LastName = vals[1];
                c.EmailAddresses = vals[2].Split(';').ToList();
                c.PhoneNumbers = vals[3].Split(';').ToList();

                output.Add(c);
            }

            return output;
        }

        //we could turn this into json - probably easiest way of going about it, but bit messy trying to read it
        //instead we will go with csv (comma separated value)
        //we can also choose different separator instead of "," - but then this would not be csv anymore (but same logic is used)
        public void WriteAllRecords(List<ContactModel> contacts, string textFile)
        {
            //there can ofc be empty columns
            //col1,col2,col3
            //Tim,Corey,555-1212
            //Madonna,,555-1234

            //note that manipulating strings can become expensive for memory
            //in such cases - use string builder (but it has some upfront memory cost and also bit more complicated)
            //here we won't use it

            List<string> lines = new List<string>();

            foreach (var c in contacts)
            {
                lines.Add($"{c.FirstName},{c.LastName},{String.Join(';', c.EmailAddresses)},{String.Join(';', c.PhoneNumbers)}");
            }

            File.WriteAllLines(textFile, lines);
        }


    }
}
