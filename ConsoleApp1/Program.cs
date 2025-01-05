using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Common;
using System.Data.SqlTypes;
using System.Net;
using System.Reflection.Metadata.Ecma335;
using System.Security.Cryptography.X509Certificates;

// Future enhancement: Add front facing UI to allow users to add pets to the list and view the list of pets in the shelter
// Future enhancement: Replace list with database to store the list of pets in the shelter and add columns for more information about the pets (including pet age, notes column, foster human name, address, etc.)

namespace ShelterPets 
{
    class ShelterPetsProgram
    {
        public
        class ShelterPetsList
        {
            public virtual List<string> ShelterPets { get; set; }
            public List<string> petNames;
            public List<string> petTypes;
            public List<string> petBreeds;
            public List<string> petGenders;
            public List<string> petStatuses;    
            public List<string> petIntakeDates;

            public ShelterPetsList()
            {
                ShelterPets = new List<string>();
                petNames = new List<string>();
                petTypes = new List<string>();
                petBreeds = new List<string>();
                petGenders = new List<string>();
                petStatuses = new List<string>();
                petIntakeDates = new List<string>();
            }

            // Add animals to the list of shelter pets
            public virtual void AddShelterPet(string petName, string petType, string petBreed, string petGender, string petStatus, string petIntakeDate)
            {
                petNames.Add(petName);
                petTypes.Add(petType);
                petBreeds.Add(petBreed);
                petGenders.Add(petGender);
                petStatuses.Add(petStatus);
                petIntakeDates.Add(petIntakeDate);
            }

            public static implicit operator ShelterPetsList(List<string> v)
            {
                throw new NotImplementedException();
            }
        }
        public static ShelterPetsList shelterPetsList = new ShelterPetsList();

            // Gets the number of pets currently in foster care
            public static int GetFosterCount()
            {
                // Set the number of fosters to start at 0
                int fosterCount = 0;

                foreach(string petStatus in shelterPetsList.petStatuses)
                {
                    if (petStatus == "F")
                    {
                        fosterCount += 1;
                    }
                }
                return fosterCount;
            }
            // Gets the number of pets currently in the shelter
            public static int GetShelterCount()
            {
                // Set the number of shelters to start at 0
                int shelterCount = 0;
                foreach(string petStatus in shelterPetsList.petStatuses)
                {
                    if (petStatus == "S")
                    {
                        shelterCount += 1;
                    }
                }
                return shelterCount;
            }
            // Gets the length, name, type and breed of the pet that has been in foster care the longest so rescuer knows which pet to focus on finding a FURever home for :)
            public static int GetLongestFoster()
            {
                
                int tempFosterLength = 0; // Set the temporary length to start at 0
                int longestFosterLength = 0; // Set the longest foster to start at 0
                string longestFosterLengthYears = "";
                DateTime fTodaysDate = DateTime.Now;
                string longestFosterName = "";
                string longestFosterTypeBreed = "";
                string fosterHeShe = "";

                foreach(string petStatus in shelterPetsList.petStatuses)
                {
                    if (petStatus == "F")
                    {
                        string petIntakeDate = shelterPetsList.petIntakeDates[shelterPetsList.petStatuses.IndexOf(petStatus)];
                        DateTime fpetIntakeDate = Convert.ToDateTime(petIntakeDate);
                        tempFosterLength = (fTodaysDate - fpetIntakeDate).Days;

                        if(tempFosterLength > longestFosterLength)
                        {
                            longestFosterLength = tempFosterLength;
                            longestFosterName = shelterPetsList.petNames[shelterPetsList.petStatuses.IndexOf(petStatus)];
                            longestFosterTypeBreed = shelterPetsList.petTypes[shelterPetsList.petStatuses.IndexOf(petStatus)] + ", (" + shelterPetsList.petBreeds[shelterPetsList.petStatuses.IndexOf(petStatus)] + ")";
                            fosterHeShe = shelterPetsList.petGenders[shelterPetsList.petStatuses.IndexOf(petStatus)];

                            if(longestFosterLength/365 < 1)
                            {
                                longestFosterLengthYears = "less than a year";
                            }
                            else
                            {
                                longestFosterLengthYears = (longestFosterLength/365).ToString() + " years";
                            }

                            if(shelterPetsList.petGenders[shelterPetsList.petStatuses.IndexOf(petStatus)] == "Male")
                            {
                                fosterHeShe = "He";
                            }
                            else
                            {
                                fosterHeShe = "She";
                            }
                        }  
                    }
                }

                Console.WriteLine($"\nThe longest foster has been in rescue/foster for {longestFosterLength/365} years({longestFosterLength} days to be exact). \n{fosterHeShe} is a(n) {longestFosterTypeBreed} named {longestFosterName}." +
                $"\nNow let's see if we can find {longestFosterName} a FURever home!");
                
                return longestFosterLength;
            }
            // Gets the length, name, type and breed of the pet that has been in the shelter the longest so rescuer knows which pet to focus on finding a foster or FURever home for :)
            public static int GetLongestShelter()
            {
                int tempShelterLength = 0; // Set the temporary length to start at 0
                int longestShelterLength = 0; // Set the longest foster to start at 0
                string longestShelterLengthYears = "";
                DateTime sTodaysDate = DateTime.Now;
                string longestShelterName = "";
                string longestShelterTypeBreed = "";
                string shelterHeShe = "";

                foreach(string petStatus in shelterPetsList.petStatuses)
                {
                    if (petStatus == "S")
                    {
                        string petIntakeDate = shelterPetsList.petIntakeDates[shelterPetsList.petStatuses.IndexOf(petStatus)];
                        DateTime fpetIntakeDate = Convert.ToDateTime(petIntakeDate);
                        tempShelterLength = (sTodaysDate - fpetIntakeDate).Days;

                        if(tempShelterLength > longestShelterLength)
                        {
                            longestShelterLength = tempShelterLength;
                            longestShelterName = shelterPetsList.petNames[shelterPetsList.petStatuses.IndexOf(petStatus)];
                            longestShelterTypeBreed = shelterPetsList.petTypes[shelterPetsList.petStatuses.IndexOf(petStatus)] + ", (" + shelterPetsList.petBreeds[shelterPetsList.petStatuses.IndexOf(petStatus)] + ")";
                            shelterHeShe = shelterPetsList.petGenders[shelterPetsList.petStatuses.IndexOf(petStatus)];

                            if(longestShelterLength/365 < 1)
                            {
                                longestShelterLengthYears = "less than a year";
                            }
                            else
                            {
                                longestShelterLengthYears = (longestShelterLength/365).ToString() + " years";
                            }

                            if(shelterPetsList.petGenders[shelterPetsList.petStatuses.IndexOf(petStatus)] == "Male")
                            {
                                shelterHeShe = "He";
                            }
                            else
                            {
                                shelterHeShe = "She";
                            }
                        }  
                    }
                }

                Console.WriteLine($"\nThe longest shelter pet has been in rescue for {longestShelterLengthYears} ({longestShelterLength} days to be exact). \n{shelterHeShe} is a(n) {longestShelterTypeBreed} named {longestShelterName}." +
                $"\nNow let's see if we can find {longestShelterName} a FURever home!");
                
                return longestShelterLength;
            }
            // Loop to allow user to continue to interact with the application
            // User can choose to do the following: 
            // 0: get the number of fosters 
            // 1: get the number of pets currently in the shelter 
            // 2: get the length of time and name/type/breed of the pet that has been in foster care the longest 
            // 3: get the length of time and name/type/breed of the pet that has been in the shelter the longest 
            // 4: exit the application
            static void ShelterPetsLoop()   
            {
            Console.WriteLine("\n********** Welcome to your Shelter Pets Console Application! **********");
            string continueOn = "Y"; //set string to dafault Y to continue the loop 
            
            while (continueOn == "Y" || continueOn == "y")// allow for lowercase and uppercase
                {   
                Console.WriteLine("\nWhat would you like to do?" +
                 "\n\n 0. Tell me the number of pets currently in foster care \n 1. Tell me the number of pets currently in the shelter"  + 
                 "\n 2. Tell me which pet has been in foster care the longest (and for how long) \n 3. Tell me which pet has been in the shelter the longest (and for how long) \n 4. Exit");

                Console.WriteLine("\nPlease enter a number from 0 to 4: ");
                string? input = Console.ReadLine(); // Get the user's input

                if (input == null)
                {
                    Console.WriteLine("\nInvalid input. Please try again. Valid inputs are 0, 1, 2, 3 or 4.");
                }

                switch (input)
                {
                    case "0":
                        Console.WriteLine("\nThe number of fosters is " + GetFosterCount());
                        break;
                    case "1":
                        Console.WriteLine("\nThe number of pets currently in the shelter is " + GetShelterCount());
                         break;
                    case "2":
                        GetLongestFoster();
                        break;
                    case "3":
                        GetLongestShelter();
                        break;
                    case "4":
                        Console.WriteLine("\nThank you for all that you do. Goodbye!\n");
                        return;
                    default:
                        Console.WriteLine("\nInvalid input. Please try again. Valid inputs are 0, 1, 2, 3 or 4.");
                        break;
                    }

                    Console.WriteLine("\nWould you like to continue? (Y/N)");

                    continueOn = Console.ReadLine() ?? string.Empty;

                    if(continueOn == "Y" || continueOn == "y")  // If the user wants to continue, continue the loop
                    {
                        continue;
                    }
                    else
                    {
                        Console.WriteLine("\nThank you for all tha you do. Goodbye!\n");
                        return;
                    }
                }
            }
        
        static void Main(string[] args)
        {
            ShelterPetsList shelterPets = shelterPetsList;

            // Pets are currently stored in a list of lists
            // 0: name
            // 1: type (Dog, Cat, Bird, etc.)
            // 2: breed
            // 3: gender (Male, Female)
            // 4: status (F = Fostered, S = Still in Shelter, A = Adopted, O = Other (e.g. deceased or lost))   :-( 
            // 5: intake date
            shelterPets.AddShelterPet("Chewey", "Dog", "Mix", "Male", "O", "10/10/2012");
            shelterPets.AddShelterPet("Clarence", "Bird", "Cockatiel", "Male", "A", "02/14/2013");
            shelterPets.AddShelterPet("Lola", "Dog", "Great Pyranese","Female", "A", "10/04/2017");
            shelterPets.AddShelterPet("Arrow", "Guinea Pig", "Smooth Hair", "Female", "O", "06/11/2020");
            shelterPets.AddShelterPet("La Quica", "Cat", "Siamese", "Male", "A", "08/15/2021");
            shelterPets.AddShelterPet("Fluffy", "Dog", "Doodle","Male", "F", "01/01/2022");
            shelterPets.AddShelterPet("Mr. Magic", "Bunny", "Dutch", "Male", "F", "04/21/2022");
            shelterPets.AddShelterPet("Beefcake", "Bunny", "Lop", "Male", "F", "04/21/2022");
            shelterPets.AddShelterPet("Stash", "Dog", "Poodle", "Male", "F", "09/04/2022");
            shelterPets.AddShelterPet("Stinky", "Cat", "Ragdoll", "Male", "A", "12/23/2023");
            shelterPets.AddShelterPet("Harvey", "Dog", "Great Dane", "Male", "A", "12/30/2023");
            shelterPets.AddShelterPet("Little Smo", "Guinea Pig", "Smooth Hair", "Male", "F", "01/14/2024");
            shelterPets.AddShelterPet("Kombucha", "Guinea Pig", "Puruvian", "Male", "A", "01/20/2024");
            shelterPets.AddShelterPet("Bruno", "Guinea Pig", "Hairless", "Male", "A", "01/20/2024");
            shelterPets.AddShelterPet("Whiskers", "Cat", "Siamese", "Female", "S", "03/13/2024");
            shelterPets.AddShelterPet("Bobo", "Guinea Pig", "Floof", "Female", "S", "12/07/2024");
            shelterPets.AddShelterPet("Winnie", "Guinea Pig", "Smooth Hair", "Female", "S", "12/07/2024");

            ShelterPetsLoop();
            
        }
    }
}
