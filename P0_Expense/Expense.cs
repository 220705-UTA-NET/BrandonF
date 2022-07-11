using System;

namespace Finances
{

    class Expense
    {
        // private string name;
        // private string category;
        // private string description;
        // private double cost;
        // private DateTime date;

        public string name { get; set; }
        public string category { get; set; }
        public string description { get; set; }
        public double cost { get; set; }
        public DateTime date { get; set; }

        public Expense(string name, string category, string description, double cost)
        {
            this.name = name;
            this.category = category;
            this.description = description;
            this.cost = cost;
            this.date = DateTime.Now;
        }

        public Expense getExpense()
        {
            return this;
        }



    }


}