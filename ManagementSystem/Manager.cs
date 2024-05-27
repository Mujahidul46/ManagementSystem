using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagementSystem
{
    class Manager
    {
        public List<Person> people;

        public Manager()
        {
            people = new List<Person>()
            /*{
                new Person("Bob", 22),
                new Person("Alice", 29),
                new Person("Jim", 31),

            }*/;
            PrintMenu();
        }
        // TODO: push to github so u can debug easier...

        public void PrintMenu()
        {
            bool continueRunning = true;
            while (continueRunning)
            {
                Console.Clear();

                Console.WriteLine("Welcome to the management system!\n");
                Console.WriteLine("1. Print all users (IMPLEMENTED)");
                Console.WriteLine("2. Add user (IMPLEMENTED)");
                Console.WriteLine("3. Edit user (IMPLEMENTED)");
                Console.WriteLine("4. Search user (NOT IMPLEMENTED)");
                Console.WriteLine("5. Remove user (NOT IMPLEMENTED)");
                Console.WriteLine("6. Exit (NOT IMPLEMENTED)");

                Console.Write("\nEnter your menu option: ");

                if (int.TryParse(Console.ReadLine(), out int menuOption))
                {
                    switch (menuOption)
                    {
                        case 1:
                            PrintAllUsers();
                            break;
                        case 2:
                            AddUser();
                            break;
                        case 3:
                            EditUser();
                            break;
                        case 4:
                            SearchUser();
                            break;
                        case 5:
                            RemoveUser();
                            break;
                        case 6:
                            continueRunning = false; // Exit the loop
                            break;
                        default:
                            Console.WriteLine("\nInvalid option. Press <Enter> to continue.");
                            Console.ReadLine();
                            break;
                    }
                }
                else
                {
                    Console.WriteLine("\nInvalid input. Press <Enter> to try again.");
                    Console.ReadLine();
                }
            }
        }

        public void PrintAllUsers()
        {
            OptionInitialMessage("Printing all users...");

            if (people.Count == 0)
            {
                Console.WriteLine("No users are in the system.\n");
            }
            else
            {
                for (int i = 0; i < people.Count; i++)
                {
                    Console.Write($"{i + 1}) ");
                    people[i].PrintUserDetails();
                }
                Console.WriteLine("\nSuccessfully printed all users!\n");
            }

            GoBackToMenu();

        }
        public void AddUser()
        {
            OptionInitialMessage("Add a user...");
            int newUserAge = -1;

            string newUserName = InputUtility.GetValidTitleCaseName("Name of the new user? ");
            newUserAge = InputUtility.GetIntegerWithOptionalRange("Age of the user? ", true, 0, 150);

            people.Add(new Person(newUserName, newUserAge));
            Console.WriteLine("\nSuccessfully added a new user!\n");
            GoBackToMenu();
        }
        public void EditUser()
        {
            OptionInitialMessage("Edit a user...");

            if (people.Count == 0)
            {
                Console.WriteLine("There are no users in the system to edit.\n");
                GoBackToMenu();
                return;
            }

            while (true)
            {
                string? nameOfUserToEdit = InputUtility.GetValidTitleCaseName("Name of person to update details for? ");
                bool foundMatch = false;
                for (int i = 0; i < people.Count; i++)
                {
                    if (nameOfUserToEdit == people[i].GetName())
                    {
                        Console.WriteLine($"\nThe current details of this user are:");
                        people[i].PrintUserDetails();

                        string updatedUserName = InputUtility.GetValidTitleCaseName("\nNew name for user? ");
                        int updatedUserAge = InputUtility.GetIntegerWithOptionalRange("New age for user? ", true, 0, 150);

                        people[i].SetName(updatedUserName);
                        people[i].SetAge(updatedUserAge);

                        Console.WriteLine("\nSuccessfully edited the user!\n");
                        GoBackToMenu();
                        return;
                    }
                }
                if (!foundMatch)
                {
                    Console.WriteLine("The user you are trying to edit does not exist in the system. Press <Enter> to try again or <Escape> to return to menu.");
                    if (Console.ReadKey().Key == ConsoleKey.Enter)
                    {
                        continue; // If Enter is pressed, ask for another name in the next loop iteration.
                    }
                    else
                    {
                        return; // If any other key is pressed, return to the menu.
                    }
                }
            }
        }
        public void SearchUser()
        {
            OptionInitialMessage("Search for a user...");

            GoBackToMenu();

        }
        public void RemoveUser()
        {
            OptionInitialMessage("Remove a user...");

            GoBackToMenu();

        }

        public void GoBackToMenu()
        {
            Console.Write("Press <Enter> to return to menu. ");
            Console.ReadLine();
        }

        public void OptionInitialMessage(string message)
        {
            Console.Clear();
            Console.WriteLine($"{message}\n");
        }
    }
}
