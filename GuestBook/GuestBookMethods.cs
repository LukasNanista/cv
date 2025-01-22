using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuestBook
{
    public static class GuestBookMethods
    {
        public static (List<string> partyGuestList, int partySizeTotal) GuestInput()
        {
            List<string> partyGuestList = new();
            //for older .NET versions
            //List<string> partyGuestList = new List<string>();
            int partySizeTotal = 0;
            string partyName = string.Empty;
            string partySizeText = string.Empty;
            int partySize = 0;
            bool endInput = false;
            bool isValidInt = int.TryParse(partySizeText, out partySize);

            while (endInput == false)
            {
                //input and check party name
                Console.Write("Enter party (group) name or press Enter to finish: ");
                partyName = Console.ReadLine();
                if (partyName == string.Empty)
                {
                    endInput = true;
                }
                else
                {
                    //add name to the party list
                    partyGuestList.Add(partyName);
                    //reset int check for another group
                    isValidInt = false;

                    //ask for party size
                    while (isValidInt == false)
                    {
                        Console.Write("Enter party (group) size: ");
                        partySizeText = Console.ReadLine();
                        int.TryParse(partySizeText, out partySize);
                        //check if party size is valid int (>= 1)
                        if (partySize > 0)
                        {
                            isValidInt = true;
                            partySizeTotal += partySize;
                        }
                    }
                }
                //for spacing between party groups
                Console.WriteLine();
            }

            return (partyGuestList, partySizeTotal);
        }

        public static void GuestOutput((List<string> partyGuestList, int partySizeTotal) guestData)
        {
            //for one more line of spacing from party groups
            Console.WriteLine();

            //check if there is enough people for the party
            if (guestData.partySizeTotal < 1)
            {
                //feels bad, man
                Console.WriteLine("Nobody came, there is no party :(");
            }
            else if (guestData.partySizeTotal == 1)
            {
                Console.WriteLine($"{guestData.partyGuestList.First()} is the only guest at this party.");
            }
            else
            {
                //should print out total for attending guests
                Console.WriteLine($"There are {guestData.partySizeTotal} guests at this party.");

                //should print out all the parties attending
                Console.WriteLine("List of partygoers:");
                foreach (var g in guestData.partyGuestList)
                {
                    Console.WriteLine($"{g}");
                }

            }
        }
    }
}
