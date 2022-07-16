using System;
using WorkoutDay;

namespace WorkoutProgram
{

    class Program
    {

        public ProgramDay[] program = new ProgramDay[7]; // A workout program has a 7 day list, each containing exercises for that day


        public Program()
        {
            // a program can be empty while the user does something else
        }

        public Program(string day1)
        {
            program[0] = new WorkoutDay();
        }

        public Program(string day1, string day2)
        {

        }

        public Program(string day1, string day2, string day3)
        {

        }

        public Program(string day1, string day2, string day3, string day4)
        {

        }

        public Program(string day1, string day2, string day3, string day4, string day5)
        {

        }

        public Program(string day1, string day2, string day3, string day4, string day5, string day6)
        {

        }

        public Program(string day1, string day2, string day3, string day4, string day5, string day6, string day7)
        {

        }



        public bool programIsEmpty()
        {
            return program.Length == 0 ? true : false;
        }



    }
}