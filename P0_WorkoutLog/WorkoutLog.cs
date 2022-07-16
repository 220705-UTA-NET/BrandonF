using System;
using Exercises;
using WorkoutDay;
using WorkoutProgram;

// How to structure a workout?
// Day, Week, Month, etc..?



namespace Test
{


    class Testing
    {
        public static void Main(string[] args)
        {
            // Exercise e = new Cardio("brandon", DateTime.Now.DayOfYear, Difficulty.Easy, Duration[0]);
            Testing e = new Testing();
            e.createProgram();
        }
        // Console.WriteLine("Choose your workout days\nSun, Mon, Tue, Wed, Thu, Fri, Sat\nEnter in format: \"SM-W-FS\"\nWhich means you would like to add days Sunday, Monday, Wednesday, Friday, Saturday\nAnd omit days Tuesday and Thursday\nEnter \"exit\" to exit");


        public Program createProgram()
        {
            Day[] days = new Day[8];
            foreach (var d in days) Console.WriteLine(d);
            int index = 0;

            Console.WriteLine("Choose your workout days\n1. Sun\n2. Mon\n3. Tue\n4. Wed\n5. Thu\n6. Fri\n7. Sat\nEnter \"exit\" to exit.");
            while (true)
            {
                index = getIndex();
                if (index == -1) break;
                else if (days[index] != Day.Empty)
                {
                    Console.WriteLine("Day already added. Please choose a different day, or press -1 to complete adding days or to exit.");
                }
                else
                {
                    days[index] = (Day)index; // cast the index value to a day value
                }
            }

            return new Program(days);
        }

        public int getIndex()
        {
            int index;
            do
            {
                Console.WriteLine("Please enter a number corresponding to a day of the week, or enter -1 to exit.");
                int.TryParse(Console.ReadLine(), out index);
                if (index == -1) return -1;
            } while (index < 1 || index > 7);
            Console.WriteLine($"{(Day)index} added to workout program.");
            return index;
        }


        // public void printMenu()
        // {

        //     // create workout plan/program ( list of workout plans )
        //     // see workout plan/programs ( formatted nicely )
        //     // delete workout plan
        //     // update workout plan

        // }


        // do
        // {

        //     // Choose your workout days
        //     // Sun, Mon, Tue, Wed, Thu, Fri, Sat
        //     // Enter in format: "SM-W-FS"
        //     // Which means you would like to add days Sunday, Monday, Wednesday, Friday, Saturday
        //     // And omit days Tuesday and Thursday


        // } while (true);

        // get average calories burned per week
        // 

        // createDay
        // addExerciseToDay
        // removeExerciseFromDay
        // getExercisesFromDay

        // sortExercisesByMuscleGroup (to see which muscles may be lacking)
        // avgCaloriesBurnedPerWeek (trend)
        // avgWeightsLength (informative for weightlifting)
        // avgCardioLength (informative for cardio/running)
        // avgMiles (informative for cardio)
        // avgSteps (informative for weightlifting)
    }



}