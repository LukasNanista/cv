using GuestBook;


(List<string> partyGuestList, int partySizeTotal) guestData = GuestBookMethods.GuestInput();
GuestBookMethods.GuestOutput(guestData);
//this also works, instead of the 2 lines above
//GuestBookMethods.GuestOutput(GuestBookMethods.GuestInput());

Console.ReadLine();