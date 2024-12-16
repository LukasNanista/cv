using GuestBookLibrary;

//task: Recreate the guest book project without looking back. Just take the concept and create the application. (with bit of a twist - also ask for age - don't forget to convert it to int)

//open question is where to write those methods, when i cannot see the main()
//presumably all of this is written within main(), so does this refactoring even matter? am I nesting methods into the main()?
//since there is a need to put the arguments for these methods to function properly, perhaps these methods are created outside the (implicit) main()

/* task overview:
 * 1. capture information about each guest (assumption is at least one guest and unknown maximum) - DONE
 * 2. info to capture: first name, last name, age, message to the host - DONE
 * 3. once done, loop through each guest and print their info - DONE
 * 4. make sure to use SRP (single responsibity principle), refactor code if necessary - DONE
 * 
 * all tasks complete
 */

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

