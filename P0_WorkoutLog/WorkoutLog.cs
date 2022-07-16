using System;
using Exercises;

// How to structure a workout?
// Day, Week, Month, etc..?



namespace Test
{


    class Testing
    {

        public static void Main(string[] args)
        {
            // Exercise e = new Cardio("brandon", DateTime.Now.DayOfYear, Difficulty.Easy, Duration[0]);
        }
    }

    public string getDayInput()
    {
        string days = "";

        do
        {
            Console.WriteLine("Choose your workout days\nSun, Mon, Tue, Wed, Thu, Fri, Sat\nEnter in format: \"SM-W-FS\"\nWhich means you would like to add days Sunday, Monday, Wednesday, Friday, Saturday\nAnd omit days Tuesday and Thursday\nEnter \"exit\" to exit");

            days = Console.ReadLine();
            days = days.ToLower();

            if (days.Equals("exit")) break;

        } while (days == null || days.Length == 0 ||
            (days[0] != 's' || days[1] != 'm' || days[2] != 't' || days[3] != 'w' || days[4] != 't' || days[5] != 'f' || days[6] != 's'));


        return days;
    }


    public void printMenu()
    {

        // create workout plan/program ( list of workout plans )
        // see workout plan/programs ( formatted nicely )
        // delete workout plan
        // update workout plan

    }


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