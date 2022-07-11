using Customer;

namespace Appointment
{

    abstract class Appointment
    {

        private string day;
        private string time;
        private double cost;
        private Customer.Customer customer;

    }


    class HairAppointment : Appointment
    {

        private string time;
        private string day;
        private double cost = 50.0;
        private Customer.Customer customer;
        private string category;

        public HairAppointment(Customer.Customer customer, string time, string day, string category)
        {
            this.time = time;
            this.day = day;
            this.customer = customer;
        }

        public string Time
        {
            get { return time; }
            set { time = value; }
        }

        public string Day
        {
            get { return day; }
            set { day = value; }
        }

        public double Cost
        {
            get { return cost; }
        }

        public string Category
        {
            get { return category; }
            set { category = value; }
        }
    }

    class MassageAppointment : Appointment
    {
        private string category = "";

    }

    class BeardAppointment : Appointment
    {

    }

}