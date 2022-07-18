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
                        wlog.printPrograms();
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
                        break;
                    case 5:
                        // retrieve from program
                        if (!wlog.checkProgramExistence(name)) { Console.WriteLine("Program isn't populated yet"); continue; } // check if program exists, if not then don't perform operation
                        Console.WriteLine("retrieve from program");
                        break;
                    default:
                        Console.WriteLine("input not valid");
                        break;
                }


            } while (choice != -1);

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