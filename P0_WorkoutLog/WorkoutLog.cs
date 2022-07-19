using System;
using Exercises;
using WorkoutDay;
using WorkoutProgram;

// How to structure a workout?
// Day, Week, Month, etc..?



namespace Test
{

    public struct UserInput
    {
        public bool valid;
        public string name;
    }

    class Testing
    {


        List<Program> programs = new List<Program>();


        public static void Main(string[] args)
        {

            // create workoutlog object
            Testing wlog = new Testing();

            // execute main menu
            int choice = 0;
            do
            {
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine("Enter a Program name to: Create/Print/Update/Delete/Retrieve From. Enter [exit] to exit the application.");
                UserInput output = wlog.getProgramName();
                if (output.valid == false) break;
                string name = output.name;

                Console.WriteLine("Enter a number based on the following options:\n[1] Create program\n[2] Print program\n[3] Update program (add exercises)\n[4] Delete program\n[5] Retrieve from");
                int.TryParse(Console.ReadLine(), out choice);

                // WANRNING; choice could be null, need to fix

                switch (choice)
                {
                    case 1:
                        // create program
                        if (wlog.checkProgramExistence(name)) { Console.WriteLine("Program exists already"); continue; } // check if program exists, if so then don't create another one with the same name
                        Console.WriteLine("create program");
                        wlog.createProgram(name);
                        wlog.printPrograms();
                        break;
                    case 2:
                        // print program
                        if (!wlog.checkProgramExistence(name)) { Console.WriteLine("Program isn't populated yet"); continue; } // check if program exists, if not then don't perform operation
                        Console.WriteLine("print program");
                        wlog.displayProgram(name);
                        break;
                    case 3:
                        // update program
                        if (!wlog.checkProgramExistence(name)) { Console.WriteLine("Program isn't populated yet"); continue; } // check if program exists, if not then don't perform operation
                        Console.WriteLine("update program");
                        wlog.updateProgram(name);
                        wlog.printPrograms();
                        break;
                    case 4:
                        // delete program
                        if (!wlog.checkProgramExistence(name)) { Console.WriteLine("Program isn't populated yet"); continue; } // check if program exists, if not then don't perform operation
                        Console.WriteLine("delete program");
                        wlog.deleteProgram(name);
                        Console.WriteLine("Program deleted");
                        wlog.printPrograms();
                        break;
                    case 5:
                        // retrieve from program
                        if (!wlog.checkProgramExistence(name)) { Console.WriteLine("Program isn't populated yet"); continue; } // check if program exists, if not then don't perform operation
                        Console.WriteLine("retrieve from program");
                        wlog.retrieveOther(name);
                        break;
                    default:
                        Console.WriteLine("input not valid");
                        break;
                }


            } while (choice != -1);

        }

        public void retrieveOther(string name)
        {
            int index = this.programs.FindIndex(p => p.Name == name);

            Console.WriteLine("Choose an option from the following list:\n[1] Get calories burned\n[2] See exercise difficulties");
            string? choice = Console.ReadLine();
            if (choice == null)
            {
                Console.WriteLine("Invalid input. Returning...");
                return;
            }

            while (choice != "1" && choice != "2")
            {
                Console.WriteLine("Invalid input. Please select [1] or [2], or enter [exit] to exit");
                choice = Console.ReadLine();
                if (choice == "exit")
                {
                    Console.WriteLine("Operation aborted");
                    return;
                }
            }


            if (choice == "1")
            {
                int cals = 0;
                foreach (var p in this.programs[index].program)// looping the the days of the program
                {
                    foreach (var e in p.ExercisesToday)
                    {
                        cals += e.CaloriesBurned;
                    }
                }
                Console.WriteLine($"You burn {cals} calories a workout");
            }
            if (choice == "2")
            {
                Dictionary<string, List<string>> table = new Dictionary<string, List<string>>();
                foreach (var p in this.programs[index].program)
                {
                    foreach (var ex in p.ExercisesToday)
                    {

                        string? diff = ex.Difficulty.ToLower();
                        if (table.ContainsKey(diff))
                        {
                            Console.WriteLine($"table contains {diff} already and {ex.Name} added to table");
                            table[diff].Add(ex.Name);
                        }
                        else
                        {

                            Console.WriteLine($"table doesnt contains {diff} already and {ex.Name} added to table");
                            table.Add(diff, new List<string>());
                            table[diff].Add(ex.Name);
                        }
                        // if (e.Difficulty == "Easy" || e.Difficulty == "easy")
                        // {
                        //     table["easy"].Add(e.Name);
                        // }
                        // else if (e.Difficulty == "Normal" || e.Difficulty == "normal")
                        // {
                        //     table["normal"].Add(e.Name);
                        // }
                        // else if (e.Difficulty == "Hard" || e.Difficulty == "hard")
                        // {
                        //     table["hard"].Add(e.Name);
                        // }

                    }
                }

                Console.WriteLine($"table count is: {table.Count}");

                int e = 0;
                int n = 0;
                int h = 0;
                if (table.ContainsKey("easy")) e = table["easy"].Count;
                if (table.ContainsKey("normal")) n = table["normal"].Count;
                if (table.ContainsKey("hard")) h = table["hard"].Count;

                Console.WriteLine($"e is: {e}");
                Console.WriteLine($"n is: {n}");
                Console.WriteLine($"h is: {h}");
                int max = Math.Max(e, Math.Max(n, h));
                Console.WriteLine("max: ", max);
                Console.WriteLine("EASY\t\t\tNORMAL\t\t\tHARD");
                int ein = 0, nin = 0, hin = 0;
                for (int i = 0; i < max; i++)
                {
                    if (ein < e)
                    {

                        Console.Write($"{table["easy"][ein++]}\t\t\t");
                    }
                    else
                    {

                        Console.Write("\t\t\t");
                    }

                    if (nin < n)
                    {

                        Console.Write($"{table["normal"][nin++]}\t\t\t");
                    }
                    else
                    {

                        Console.Write("\t\t\t");
                    }

                    if (hin < h)
                    {

                        Console.WriteLine($"{table["hard"][hin++]}");
                    }
                    else
                    {

                        Console.WriteLine("\t\t\t");
                    }
                }
                // foreach (var c in table)
                // {
                //     foreach ()
                // }
            }
            else
            {
                Console.WriteLine("something happened in retireveOTher()");
            }






        }

        public void deleteProgram(string name)
        {
            int index = this.programs.FindIndex(p => p.Name == name);
            this.programs.RemoveAt(index);

        }

        public void displayProgram(string name)
        {

            int index = this.programs.FindIndex(p => p.Name == name);

            Console.WriteLine("Program Name\t\tDays\t\tExercises");
            Console.WriteLine(name);

            foreach (var d in this.programs[index].program)
            {
                Console.WriteLine($"\t\t\t{d.Day}");
                foreach (var e in d.ExercisesToday)
                {
                    Console.WriteLine($"\t\t\t\t\t{e.Name}");
                }
            }
        }

        public bool checkProgramExistence(string name)
        {
            return this.programs.Find(e => e.Name == name) != null;
        }

        public Program createProgram(string name)
        {
            Program? p = new Program(name);

            if (p == null)
            {
                Console.WriteLine($"Couldn't create a program {name}");
            }
            else if (p.program.Count == 0)
            {
                Console.WriteLine("No days were added. Operation unsuccessful");
            }
            else
            {
                Console.WriteLine($"Successfully created program: {name}");
                programs.Add(p);
            }

            return p;
        }

        public void updateProgram(string name)
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            Program? p = this.programs.Find(e => e.Name == name);
            if (p == null)
            {
                Console.WriteLine($"Couldn't find program with name {name}");
                return;
            }

            p.updateProgram();
            printPrograms();
            Console.WriteLine("Updates complete");
        }

        // method gets a user input for a program name
        public UserInput getProgramName()
        {
            UserInput choice;
            string? name;

            // Console.WriteLine("Enter program name (or enter \"exit\" to exit process without saving):");
            name = Console.ReadLine();
            if (name == null || name.ToLower() == "exit")
            {
                choice.valid = false;
                choice.name = "null";
                return choice;
            }
            name = name.ToLower();
            choice.valid = true;
            choice.name = name;
            return choice;
        }



        public void printPrograms()
        {
            foreach (var p in this.programs)
            {
                Console.WriteLine($"\t{p.Name}");
                foreach (var d in p.program)
                {
                    Console.WriteLine($"\t\t{d.Day}");
                    foreach (var e in d.ExercisesToday)
                    {
                        Console.WriteLine($"\t\t\t{e.Name}");
                    }
                }
            }
        }





        // get average calories burned per week
        // createDay
        // addExerciseToDay
        // removeExerciseFromDay

        // avgCardioLength (informative for cardio/running)
        // avgCaloriesBurnedPerWeek (trend)
        // sortExercisesByMuscleGroup (to see which muscles may be lacking)

        // getExercisesFromDay
        // avgWeightsLength (informative for weightlifting)
        // avgMiles (informative for cardio)
        // avgSteps (informative for weightlifting)
    }



}