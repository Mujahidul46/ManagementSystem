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
            people = new List<Person>() // Create a list of people
            {
                // Create hard-coded person objects - Only use for testing purposes.

                //new Person("Bob", 22),
                //new Person("Alice", 29),
                //new Person("Jim", 31),
                //new Person("Eve", 25),
                //new Person("Frank", 45),
                //new Person("Grace", 32),
                //new Person("Hank", 38),
                //new Person("Ivy", 27),
                //new Person("Jack", 19),
                //new Person("Kara", 40),
                //new Person("Leo", 35),
                //new Person("Mia", 50),
                //new Person("Nina", 60),
                //new Person("Oscar", 70),
                //new Person("Paul", 80),
                //new Person("Quinn", 90),
                //new Person("Rose", 100),
                //new Person("Sam", 110),
                //new Person("Tina", 120),
                //new Person("Uma", 130)

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

                if (int.TryParse(Console.ReadLine(), out int menuOption)) // Checks if valid integer is entered
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
                            continueRunning = false; // Exit the loop if 'Exit' selected
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

            if (IsSystemEmpty())
                return;

            PrintAllUserDetails();
            Console.WriteLine("\nSuccessfully printed all users!\n");


            GoBackToMenu();

        }

        public void PrintAllUserDetails()
        {
            for (int i = 0; i < people.Count; i++)
            {
                Console.Write($"{i + 1}) "); // These 2 lines print a numbered list of people info. E.g. 1) Name: Alice Smith | Age: 23
                people[i].PrintUserDetails();
            }
        }

        public void AddUser() // Asks for a name and age of the new user, and then adds the user to the system
        {
            OptionInitialMessage("Add a user...");
            int newUserAge;

            string newUserName = InputUtility.GetValidTitleCaseName("Name of the new user? ");
            newUserAge = InputUtility.GetIntegerWithOptionalRange("Age of the user? ", true, 0, 150);

            people.Add(new Person(newUserName, newUserAge));
            Console.WriteLine("\nSuccessfully added a new user!\n");
            GoBackToMenu();
        }
        public void EditUser()
        {
            OptionInitialMessage("Edit a user...");

            if (IsSystemEmpty())
                return;

            Console.WriteLine("List of users:\n");
            PrintAllUserDetails(); // Displays all users to help the manager to find the user they want to edit.
            Console.WriteLine("\n");

            // Ask for index of the user to edit. This is quicker and more convenient than making the manager type the name.
            int indexOfUserToEdit = InputUtility.GetIntegerWithOptionalRange("Index of person to update details for? ", true, 1, people.Count);

            // Get the name and age of the user to edit. These variables are used to inform the manager which person they are currently
            // editing.
            // Additionally, these values are passed to FindAndEditUser method so a summary of changes can be displayed after
            // a successful edit.
            string nameOfUserToEdit = people[indexOfUserToEdit - 1].GetName();
            int ageOfUserToEdit = people[indexOfUserToEdit - 1].GetAge();

            Console.WriteLine($"\nYou are currently editing the following user:\n{nameOfUserToEdit} | {ageOfUserToEdit}");

            FindAndEditUser(nameOfUserToEdit, ageOfUserToEdit);
        }

        public void FindAndEditUser(string nameOfUserToEdit, int ageOfUserToEdit) // Updates the user details with the new name and age.
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

                    // Displays summary of changes for manager to review.
                    Console.WriteLine($"{nameOfUserToEdit} | {ageOfUserToEdit} --> {updatedUserName} | {updatedUserAge}\n");
                    GoBackToMenu();

                    return; // Return once user is edited to avoid unnecessarily iterating over entire people list.
                }
            }
        }
        public void SearchUser()
        {
            OptionInitialMessage("Search for a user...");
            bool foundUser = false;

            if (IsSystemEmpty())
                return;

            string nameOfUserToSearchFor = InputUtility.GetValidTitleCaseName("Name of user to search for?\n");
            
            for (int i = 0; i < people.Count; i++)
            {
                // Use .Contains() to ensure that the name inputted is correctly matched with the name in the system, even if the name is
                // only partially given.
                // This helps the manager successfully search for a user, even if they can't remember exact spelling of name.
                if (people[i].GetName().Contains(nameOfUserToSearchFor))
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
        public void RemoveUser() // Removes a user by asking the manager to enter the index of the user they want to remove.
        {
            OptionInitialMessage("Remove a user...");

            if (IsSystemEmpty())
                return;

            Console.WriteLine("List of users:\n");
            PrintAllUserDetails();
            Console.WriteLine("\n");

            int indexOfPersonToRemove = InputUtility.GetIntegerWithOptionalRange("Please enter the index of the user you would like to remove: ", true, 1, people.Count);

            Console.WriteLine($"\nHere are the details of the user you would like to remove: ");
            people[indexOfPersonToRemove - 1].PrintUserDetails();

            // Asks for confirmation as removing a user is an irreversible process.
            string confirmYesOrNo = InputUtility.GetNonEmptyString("\nAre you sure you want to remove this user from the system? (yes/no) ");

            // Validates input - Only "yes/y/no/n" are valid inputs.
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
            else // Manager entered "no" or "n", so cancel removal of user.
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

        public bool IsSystemEmpty()
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
