using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary
{
    public class Destination
    {
        string name;
        string dayOfChange;
        int pricePrPerson;

        public Destination(string name, string dayOfChange, int pricePrPerson)
        {
            this.name = name;
            this.dayOfChange = dayOfChange;
            this.pricePrPerson = pricePrPerson;
        }
        public string Name
        {
            get { return name; }
            set { name = value; }
        }
        public string DayOfChange
        {
            get { return dayOfChange; }
            set { dayOfChange = value; }
        }
        public int PricePrPerson
        {
            get { return pricePrPerson; }
            set { pricePrPerson = value; }
        }
    }
        public class VacationHouse
        {
            string description;
            int maxPersons;
            int distanceToShopping;
            int distanceToBeach;

            public VacationHouse(string description, int maxPersons, int distanceToShopping, int distanceToBeach)
            {
                this.description = description;
                this.maxPersons = maxPersons;
                this.distanceToShopping = distanceToShopping;
                this.distanceToBeach = distanceToBeach;
            }
            public string Description
            {
                get { return description; }
                set { description = value; }
            }
            public int MaxPersons
            {
                get { return maxPersons; }
                set { maxPersons = value; }
            }
            public int DistanceToShopping
            {
                get { return distanceToShopping; }
                set { distanceToShopping = value; }
            }
            public int DistanceToBeach
            {
                get { return distanceToBeach; }
                set { distanceToBeach = value; }
            }
            public override string ToString()
            {
                return description;
            }
        }
        public class VacationWeek
        {
            int weekNumber;
            int price;
            Boolean isBooked;

            public VacationWeek(int weekNumber, int price)
            {
                this.weekNumber = weekNumber;
                this.price = price;
                this.isBooked = false;
            }
            public VacationWeek(int weekNumber)
            {
                this.weekNumber = weekNumber;
                this.isBooked = false;
            }
            public int WeekNumber
            {
                get { return weekNumber; }
                set { weekNumber = value; }
            }
            public int Price
            {
                get { return price; }
                set { price = value; }
            }
            public Boolean IsBooked
            {
                get { return isBooked; }
                set { isBooked = value; }
            }
        }
        public class Costumer
        {
            string name;
            string email;
            string password;

            public Costumer(string name, string email, string password)
            {
                this.name = name;
                this.email = email;
                this.password = password;
            }
            public string Name
            {
                get { return name; }
                set { name = value; }
            }
            public string Email
            {
                get { return email; }
                set { email = value; }
            }
            public string Password
            {
                get { return password; }
                set { password = value; }
            }
        }
    public class Booking
    {
        int numberOfPeople;
        int startWeek;
        int price;
        public Booking(int numberOfPeople, int startWeek, int price)
        {
            this.numberOfPeople = numberOfPeople;
            this.startWeek = startWeek;
            this.price = price;
        }
        public int NumberOfPeople
        {
            get { return numberOfPeople; }
            set { numberOfPeople = value; }
        }
        public int StartWeek
        {
            get { return startWeek; }
            set { startWeek = value; }
        }
        public int Price
        {
            get { return price; }
            set { price = value; }
        }
    }
}
