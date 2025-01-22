using GuestBookLibrary;

//this is presumably within the main()
//it won't let me to make the guests public static
List<GuestModel> guests = new List<GuestModel>();

CollectGuestData(guests);
PrintOutGuestList(guests);

Console.ReadLine();

//and these are presumably outside/below the main()
static void PrintOutGuestList(List<GuestModel> guests)
{
    foreach (GuestModel guest in guests)
    {
        Console.WriteLine(guest.GuestInformation);
    }
}

static void CollectGuestData(List<GuestModel> guests)
{
    string moreGuestsComing = string.Empty;

    do
    {
        GuestModel guest = new GuestModel();

        guest.FirstName = GetGuestStringInfo("What is your first name: ");
        guest.LastName = GetGuestStringInfo("What is your last name: ");
        guest.Age = GetGuestIntInfo("How old are you: ");
        guest.MessageToHost = GetGuestStringInfo("What would you like to say to your host: ");
        guests.Add(guest);

        moreGuestsComing = GetGuestStringInfo("Are more guests coming (yes/no): ");
        Console.Clear();
    } while (moreGuestsComing.ToLower() == "yes");
}

static string GetGuestStringInfo(string message)
{
    Console.Write(message);
    string output = Console.ReadLine();

    return output;
}

static int GetGuestIntInfo(string message)
{
    Console.Write(message);
    bool validInt = int.TryParse(Console.ReadLine(), out int output);

    if (validInt)
    {
        return output;
    }
    else { return 0; }
}

