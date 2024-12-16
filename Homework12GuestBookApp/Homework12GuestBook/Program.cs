using Homework12GuestBook;

//task: Build a Console Guest Book. Ask the user for their name and how many are in their party. Keep track if how many people are at the party. At the end, print out the guest list and the total number of guests.

/* task overview:
 * 1. build a console app - guest book - DONE
 * 2. ask the user for their name and how many people are in their party (party as in their sub-group :) - DONE
 * 3. keep asking for names and numbers until there are no more invitees (let's say enter blank to continue) - DONE
 * 4. keep track of how people are at the party (party as in the event :) - DONE
 * 5. print out the guest list (names of parties) and the total number of guests - DONE
 * 
 * all tasks complete
 */


(List<string> partyGuestList, int partySizeTotal) guestData = GuestBookMethods.GuestInput();
GuestBookMethods.GuestOutput(guestData);
//this also works, instead of the 2 lines above
//GuestBookMethods.GuestOutput(GuestBookMethods.GuestInput());

Console.ReadLine();