﻿//Prague parking

bool systemOn = true;
string[] carPark = new string[101];

/************** Main program loop ****************/

do
{
    //Menu choices and operations
    int choice = 0;
    int Menu(int choice)
    {
        switch (choice)
        {
            case 1:
                Console.Clear();
                Console.WriteLine("Park");
                Park();
                break;
            case 2:
                Console.Clear();
                Console.WriteLine("Search");
                Search();
                break;
            case 3:
                Console.Clear();
                Console.WriteLine("Show All");
                ShowAll();
                break;
            case 4:
                Console.Clear();
                Console.WriteLine("Move");
                Move();
                break;
            case 5:
                Console.Clear();
                Console.WriteLine("Get / remove");
                Remove();
                break;
            case 6:
                Console.Clear();
                Console.WriteLine("Optimise parking");
                OptimiseParking();
                break;
            case 7:

                Console.Clear();
                Console.WriteLine("Shutdown");
                systemOn = false;
                Console.WriteLine("Good bye!");
                break;
            default:
                Console.WriteLine("nothing");
                break;
        }
        return choice;
    }

    Console.WriteLine("Please choose an option (1 - 6)");
    Console.WriteLine("1. Park");
    Console.WriteLine("2. Search");
    Console.WriteLine("3. Show all");
    Console.WriteLine("4. Move");
    Console.WriteLine("5. Remove");
    Console.WriteLine("6. Optimise parking");
    Console.WriteLine("7. Shut down");

    choice = int.Parse(Console.ReadLine() ?? "");
    Menu(choice);

    /*************** Methods for operations *************/

    //Park
    void Park(string reg = "", string type = "", string vehicle = "")
    {
        //Get vehicle registration number and type
        reg = GetRegistrationNumber();
        type = GetVechicleType();
        
        //Concat strings with separator
        vehicle = reg + "#" + type;

        //Find space and park
        for (int i = 1; i < carPark.Length; i++)
        {
            if (carPark[i] == null)
            {
                carPark[i] = vehicle;
           
                Console.WriteLine($"You parked {type} with reg {reg} at space: {i}");
                ReturnToMenu();
                break;
            }  
        }   
    }

    //Search vehicle
    void Search(int space = 0, string reg = "")
    {
        //Find vehicle by reg number
        Console.WriteLine("Enter registration number of the vehicle you wish to find: ");
        reg = Console.ReadLine() ?? "";
        space = getSpace(reg);

        if (space > 0)
        {
            Console.WriteLine($"Found vehicle with registration {reg} at space {space}");
            
        }
        else
        {
            Console.WriteLine($"Vehicle with registration {reg} can not be found..");
        }
        ReturnToMenu();
    }

    //Show all vehicles
    void ShowAll()
    {
        for(int i = 1; i < carPark.Length; i++)
        {
            Console.WriteLine($"Vechile: {carPark[i]} | Space: {i}");
        }
        ReturnToMenu();
    }

    //Move vehicle
    void Move(string reg = "", int currentSpace = 0, int newSpace = 0)
    {
        GetRegistrationNumber();

        currentSpace = getSpace(reg);
        Console.WriteLine("CURRENT SPACE IS " + currentSpace);
        Console.WriteLine("Please choose a new space (1 - 100)");
        newSpace = int.Parse(Console.ReadLine() ?? "") ;

        if (carPark[newSpace] != null)
        {
            Console.WriteLine("This space is taken, please choose another space...");
        }
        else
        {
            carPark[newSpace] = carPark[currentSpace];
            carPark[currentSpace] = null;
        }
        ReturnToMenu();
    }

    //Remove vehicle
    void Remove(int space = 0, string reg = "")
    {
        Console.WriteLine("Please enter registration number to remove vehicle from parking:");
        reg = Console.ReadLine() ?? "";
        space = getSpace(reg);
        carPark[space] = null;
        Console.WriteLine($"You removed car with registration number {reg} from space {space}");
        ReturnToMenu();
    }

    //Optimise parking, merge a single mc to a space with another mc
    void OptimiseParking()
    {
        int spaceOne = 0;
        int spaceTwo = 0;
        string mcOne = "";
        string mcTwo = "";
        
       for (int i = 0; i < carPark.Length; i++)
        {
            if (carPark[i] == null)
            {
                continue;
            }
            else if (carPark[i].Contains("mc") && !carPark[i].Contains('|'))
            {
                spaceOne = i;
                mcOne = carPark[i];
                
                for (int j = i + 1; j < carPark[i].Length; j++)
                {
                    if (carPark[j].Contains("mc"))
                    {
                        spaceTwo = j;
                        mcTwo = carPark[j];
                        break;
                    }
                }
                break;
            }
        }

        string optimisedParking = carPark[spaceOne] + "|" + carPark[spaceTwo];
        carPark[spaceOne] = optimisedParking;
        carPark[spaceTwo] = null;
        if (mcTwo != "")
        {
            Console.WriteLine($"You have moved mc with registration {mcTwo.Substring(0, 6)}");
            Console.WriteLine($"From space: {spaceTwo} to space: {spaceOne}.");
            ReturnToMenu();
        }
        else
        {
            Console.WriteLine("No mc's have been moved...");
            ReturnToMenu();
        }
    }

    //Get space helper method
    int getSpace(string reg = "", int space = 0)
    {
        for (int i = 1; i < carPark.Length; i++)
        {
            if (carPark[i] == null)
            {
                continue;
            }
            else
            {
                if (carPark[i].Contains(reg))
                {
                    space = i;
                    break;
                }
                else
                {
                    continue;
                }
            }
        }
        return space;
    }

    //Method used to return to main menu after clearing the console.
    void ReturnToMenu()
    {
        Console.WriteLine("Press enter to return to the menu...");
        Console.ReadLine();
        Console.Clear();
    }

    //Lets user enter registration number, checks if string length is less than 10
    string GetRegistrationNumber()
    {
        Console.WriteLine("Please enter registration number:");
        string reg = Console.ReadLine() ?? "";

        if (reg.Length <= 10)
        {
            return reg;
        }
        else
        {
            Console.WriteLine("The maximum amount of characters is 10, please enter registration number:");
            reg = Console.ReadLine() ?? "";
            return reg;
        }
    }

    //Lets user enter vehicle type
    string GetVechicleType()
    {
        Console.WriteLine("Is the vehicle a car or a mc? (type car or mc)");
        string type = Console.ReadLine() ?? "";
        return type;
    }

} while (systemOn) ;


