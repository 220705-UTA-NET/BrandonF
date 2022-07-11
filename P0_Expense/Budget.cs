using System;
using System.Collections.Generic;
using Finances;

namespace BudgetTracker
{

    class Budget
    {

        private List<Expense> expenses;

        public Budget()
        {
            this.expenses = new List<Expense>();
        }

        public static void Main(string[] args)
        {
            Budget budget = new Budget();

            budget.createExpense("netflix", "entertainment", "month 1", 14.99);
            budget.createExpense("hulu", "entertainment", "first signup", 17.99);
            budget.createExpense("netflix", "entertainment", "month 2", 14.99);
            budget.createExpense("deposit", "finance", "transfer", 25);

            budget.findExpense("netflix");
            Console.WriteLine();
            budget.getSumByCategory("entertainment");
        }

        public Expense createExpense(string name, string category, string description, double cost)
        {
            Expense e = new Expense(name, category, description, cost);
            this.expenses.Add(e);
            return e;
        }



        public void findExpense(string name)
        {
            List<Expense> expenses = this.expenses.FindAll(e => e.name == name);
            Console.WriteLine("Expense\tCost\tCategory\tDescription\tDate");
            foreach (var exp in expenses)
            {
                Console.WriteLine($"{exp.name}\t{exp.cost}\t{exp.category}\t{exp.description}\t\t{exp.date}");
            }
        }

        public List<Expense> getExpense(string name)
        {

            return this.expenses.FindAll(e => e.name == name);
        }

        public void updateExpense()
        {

            Console.WriteLine("Choose attribute to update from the list below:\n1. Update ");
            // what if the user wants to update multiple attributes at once?


        }

        public void getSum(string name)
        {

        }

        public void getSumByCategory(string category)
        {
            List<Expense> expenses = this.expenses.FindAll(e => e.category == category);
            Console.WriteLine("Expense\tCost\tCategory\tDescription\tDate");
            double sum = 0;
            foreach (var exp in expenses)
            {
                sum += exp.cost;
                Console.WriteLine($"{exp.name}\t{exp.cost}\t{exp.category}\t{exp.description}\t\t{exp.date}");
            }

            Console.WriteLine("--------------------------------------");
            Console.WriteLine($"Sum:\t${sum}");
        }


    }




}