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
            {
                new Person("Bob", 22),
                new Person("Alice", 29),
                new Person("Jim", 31),
                new Person("Eve", 25),
                new Person("Frank", 45),
                new Person("Grace", 32),
                new Person("Hank", 38),
                new Person("Ivy", 27),
                new Person("Jack", 19),
                new Person("Kara", 40),
                new Person("Leo", 35),
                new Person("Mia", 50),
                new Person("Nina", 60),
                new Person("Oscar", 70),
                new Person("Paul", 80),
                new Person("Quinn", 90),
                new Person("Rose", 100),
                new Person("Sam", 110),
                new Person("Tina", 120),
                new Person("Uma", 130)

            };
            PrintMenu();
        }

        public void PrintMenu()
        {
            bool continueRunning = true;
            while (continueRunning)
            {
                Console.Clear();

                Console.WriteLine("Welcome to the management system!\n");
                Console.WriteLine("1. Print all users");
                Console.WriteLine("2. Add user");
                Console.WriteLine("3. Edit user");
                Console.WriteLine("4. Search user");
                Console.WriteLine("5. Remove user");
                Console.WriteLine("6. Exit");

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

            if (ReturnToMenuIfNoUsers())
                return;

            PrintAllUserDetails();
            Console.WriteLine("\nSuccessfully printed all users!\n");


            GoBackToMenu();

        }

        public void PrintAllUserDetails()
        {
            for (int i = 0; i < people.Count; i++)
            {
                Console.Write($"{i + 1}) ");
                people[i].PrintUserDetails();
            }
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

            if (ReturnToMenuIfNoUsers())
                return;

            Console.WriteLine("List of users:\n");
            PrintAllUserDetails();
            Console.WriteLine("\n");

            while (true)
            {
                int indexOfUserToEdit = InputUtility.GetIntegerWithOptionalRange("Index of person to update details for? ", true, 1, people.Count);
                string nameOfUserToEdit = people[indexOfUserToEdit - 1].GetName();
                int ageOfUserToEdit = people[indexOfUserToEdit - 1].GetAge();

                if (FindAndEditUser(nameOfUserToEdit, ageOfUserToEdit))
                {
                    return; // Successfully found and edited user
                }

                Console.WriteLine("The user you are trying to edit does not exist in the system. Press <Enter> to try again or <Escape> to return to menu.");
                if (Console.ReadKey(true).Key == ConsoleKey.Enter)
                {
                    continue; // If Enter is pressed, ask for another name in the next loop iteration.
                }
                else
                {
                    return; // If any other key is pressed, return to the menu.
                }
            }
        }

        public bool FindAndEditUser(string nameOfUserToEdit, int ageOfUserToEdit)
        {
            for (int i = 0; i < people.Count; i++)
            {
                if (nameOfUserToEdit == people[i].GetName())
                {
                    string updatedUserName = InputUtility.GetValidTitleCaseName("\nNew name for user? ");
                    int updatedUserAge = InputUtility.GetIntegerWithOptionalRange("New age for user? ", true, 0, 150);

                    people[i].SetName(updatedUserName);
                    people[i].SetAge(updatedUserAge);

                    Console.WriteLine("\nSuccessfully edited the user!");
                    Console.WriteLine($"{nameOfUserToEdit} | {ageOfUserToEdit} --> {updatedUserName} | {updatedUserAge}\n");
                    GoBackToMenu();
                    return true;
                }
            }
            return false;
        }
        public void SearchUser()
        {
            OptionInitialMessage("Search for a user...");
            bool foundUser = false;

            if (ReturnToMenuIfNoUsers())
                return;

            string nameOfUserToSearchFor = InputUtility.GetValidTitleCaseName("Name of user to search for?\n");
            
            for (int i = 0; i < people.Count; i++)
            {
                if (people[i].GetName().ToLower().Contains(nameOfUserToSearchFor.ToLower()))
                {
                    people[i].PrintUserDetails();
                    Console.Write("\n");
                    foundUser = true;
                    break;
                }
            }
            if (!foundUser)
            {
                Console.WriteLine("Could not find that user in the system.\n");
            }

            GoBackToMenu();

        }
        public void RemoveUser()
        {
            OptionInitialMessage("Remove a user...");

            if (ReturnToMenuIfNoUsers())
                return;

            Console.WriteLine("List of users:\n");
            PrintAllUserDetails();
            Console.WriteLine("\n");

            int indexOfPersonToRemove = InputUtility.GetIntegerWithOptionalRange("Please enter the index of the user you would like to remove: ", true, 1, people.Count);

            Console.WriteLine($"\nHere are the details of the user you would like to remove: ");
            people[indexOfPersonToRemove - 1].PrintUserDetails();

            string confirmYesOrNo = InputUtility.GetNonEmptyString("\nAre you sure you want to remove this user from the system? (yes/no) ");

            while (confirmYesOrNo.ToLower() != "yes" && confirmYesOrNo.ToLower() != "y" && confirmYesOrNo.ToLower() != "no" && confirmYesOrNo.ToLower() != "n")
            {
                Console.WriteLine("\nInvalid input. Please enter 'yes' or 'no'.");
                confirmYesOrNo = InputUtility.GetNonEmptyString("Are you sure you want to remove this user from the system? (yes/no) ");
            }

            if (confirmYesOrNo.ToLower() == "yes" || confirmYesOrNo.ToLower() == "y")
            {
                people.RemoveAt(indexOfPersonToRemove - 1);
                Console.WriteLine("\nSuccessfully removed user from the system.\n");
            }
            else
            {
                Console.WriteLine("\nCancelled removal of that user from the system.\n");
            }

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

        public bool ReturnToMenuIfNoUsers()
        {
            if (people.Count == 0)
            {
                Console.WriteLine("There are no users in the system.\n");
                GoBackToMenu();
                return true;
            }
            return false;
        }
    }
}
