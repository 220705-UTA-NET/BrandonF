// Option 2 of the menu
// public void modifyExercise()
// {

//     bool runLoop = true;

//     while (runLoop)
//     {

//         // show list of exercises to modify
//         Console.WriteLine("Enter the [Name] of the exercise you want to modify:");
//         printExercises();
//         // ask which exercise user whats to modify
//         // take user input
//         string? name = Console.ReadLine();

//         // var? ex = this.ExercisesToday.Find(e => e.Name == name);
//         Weights w;
//         Cardio c;
//         if (this.ExercisesToday.Find(e => e.Name == name).Type == "Weights")
//         {
//             this.ExercisesToday.Find(e => e.Name == name);
//         }
//         if (ex == null)
//         {
//             Console.WriteLine("No exercise with that name was found");
//         }


//         else
//         {

//             bool done = true;

//             do
//             {

//                 if (ex.Type == "Weights")
//                 {
//                     Console.WriteLine("Enter attribute name to edit:\n-> Name\n-> Start time\n-> End time\n-> Reps\n-> Sets\n-> Pounds\n-> Muscle Group-> Difficulty\n-> Date Performed");
//                     w = ex;
//                 }
//                 else
//                 {
//                     Console.WriteLine("Select an attribute to edit:\n-> Name\n-> Start time\n-> End time\n-> Calories Burned\n-> Difficulty\n-> Date Performed");
//                 }

//                 string? choice = Console.ReadLine();
//                 while (choice == null)
//                 {
//                     Console.WriteLine("Please enter a valid option");
//                     choice = Console.ReadLine();
//                 }
//                 choice = choice.ToLower();

//                 string? edit;
//                 switch (choice)
//                 {
//                     case "name":
//                         Console.WriteLine("Enter a new name:");
//                         edit = modifyAttribute("name");
//                         ex.Name = edit;
//                         break;
//                     case "start time":
//                         Console.WriteLine("Enter a new start time:");
//                         edit = modifyAttribute("start time");
//                         ex.StartTime = edit;
//                         break;
//                     case "end time":
//                         Console.WriteLine("Enter a new end time:");
//                         edit = modifyAttribute("end time");
//                         ex.EndTime = edit;
//                         break;
//                     case "reps":
//                         if (ex.Type == "Cardio")
//                         {
//                             Console.WriteLine("Exercise type does not have this attribute");
//                         }
//                         Console.WriteLine("Enter a new number of reps:");
//                         edit = modifyAttribute("reps");
//                         ex.Reps = edit;
//                         break;
//                     case "sets":
//                         if (ex.Type == "Cardio")
//                         {
//                             Console.WriteLine("Exercise type does not have this attribute");
//                         }
//                         break;
//                     case "calories burned":
//                         if (ex.Type == "Weights")
//                         {
//                             Console.WriteLine("Exercise type does not have this attribute");
//                         }
//                         Console.WriteLine("Enter a new start time:");
//                         edit = modifyAttribute("start time");
//                         (Cardio)ex.CaloriesBurned = edit;
//                         break;
//                     case "difficulty":
//                         break;
//                     case "date performed":
//                         break;
//                     case "muscle group":
//                         if (ex.Type == "Cardio")
//                         {
//                             Console.WriteLine("Exercise type does not have this attribute");
//                         }
//                         break;
//                     case "pounds":
//                         if (ex.Type == "Cardio")
//                         {
//                             Console.WriteLine("Exercise type does not have this attribute");
//                         }
//                         break;
//                     case "exit":
//                         done = false;
//                         break;
//                     default:
//                         break;
//                 }



//                 Console.WriteLine("Are you finished modifying attributes?\n[Y] Yes\n[N] No");
//                 string ans = Console.ReadLine();
//                 if (ans == null) break;
//                 ans = ans.ToLower();
//                 if (ans == "yes" || ans == "y")
//                 {
//                     done = false;
//                 }
//                 else
//                 {
//                     Console.WriteLine("Continuing execution. If you did not mean to persist, type [exit] to exit go back.");
//                 }

//             } while (done);
//         }
//         runLoop = false;
//     }


// }

// public Exercise createCopy(string type, Exercise ex)
// {
//     Exercise copy;
//     if (type == "Weights")
//     {
//         copy = new Weights(ex.Name, ex.StartTime, ex.EndTime, ex.)
//     }
//     else
//     {

//     }

// }

// public string modifyAttribute(string attribute)
// {

//     string input = Console.ReadLine();

//     // check if attribute has to be numeric
//     if (attribute == "calories burned" || attribute == "reps" || attribute == "sets")
//     {
//         // if user input cannot be parsed as a 
//         while (!int.TryParse(input, out int result) || input == null)
//         {
//             Console.WriteLine("This attribute needs to be numeric (whole number)");
//             input = Console.ReadLine();
//         }
//     } // else the attribute is of type string
//     else if (attribute == "pounds")
//     {
//         // if user input cannot be parsed as a 
//         while (!double.TryParse(input, out double result) || input == null)
//         {
//             Console.WriteLine("This attribute needs to be numeric");
//             input = Console.ReadLine();
//         }
//     }
//     else if (attribute == "date performed")
//     {
//         // if user input cannot be parsed as a 
//         while (!int.TryParse(input, out int result) || input == null)
//         {
//             Console.WriteLine("This attribute needs to be numeric (whole number)");
//             input = Console.ReadLine();
//         }
//     }
//     else
//     {
//         // if user input cannot be parsed as a 
//         while (input == null)
//         {
//             Console.WriteLine("Please enter an attribute:");
//             input = Console.ReadLine();
//         }
//     }

//     Console.Write("out of modification is: ");
//     Console.WriteLine(input);

//     return input;
// }