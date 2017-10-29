using ClassLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinqConsoleApplication
{
    class Program
    {
        LinqEntities db = new LinqEntities();
        static void Main(string[] args)
        {
            Boolean b = true;
            string s = "";
            Program p = new Program();
            Console.WriteLine("Press the following button to enter these funktions");
            Console.WriteLine("D to see income for the destinations");
            Console.WriteLine("C to cancel the renting of a vacationhouse");
            Console.WriteLine("V to see the cheepest vacationhouse for a week");
            while (b == true)
            {
                s = Console.ReadLine();
                if (s == "d")
                {
                    p.destinationIncome();
                    Console.WriteLine("Press a new button or type exit to close");
                }
                else if (s == "c")
                {
                    Console.WriteLine("Insert the ID of the house you want to cancel renting for");
                    s = Console.ReadLine();
                    p.cancelVacationhouseRenting(Convert.ToInt32(s));
                    Console.WriteLine("Press a new button or type exit to close");

                }
                else if (s == "v")
                {
                    Console.WriteLine("Insert the weeknumber where you want to find the cheepest vacation house");
                    s = Console.ReadLine();
                    p.cheepestVacationhouse(Convert.ToInt32(s));
                    Console.WriteLine("Press a new button or type exit to close");
                }
                else if (s == "exit")
                {
                    b = false;
                }
                else
                {
                    Console.WriteLine("Please input a correct character");
                }
            }
        }
        public void destinationIncome()
        {
            int totalPrice = 0;
            foreach (destination d in db.destinations)
            {
                totalPrice += (int)d.priceprperson;
                foreach (vacationhouse v in db.vacationhouses)
                {
                    if (v.destination == d.name)
                    {
                        foreach (vacationweek vw in db.vacationweeks)
                        {
                            if (vw.vacationhouseid == v.id)
                            {
                                totalPrice += (int)vw.price;
                            }
                        }
                    }
                }
                Console.WriteLine("Destinationame: "+ d.name + " TotalEarnings: " + totalPrice + " Kr");
                totalPrice = 0;
            }
        }

        public void cancelVacationhouseRenting(int i)
        {
            foreach (vacationhouse v in db.vacationhouses)
            {
                if (v.id == i)
                {
                    foreach (vacationweek vw in db.vacationweeks)
                    {
                        if (vw.vacationhouseid == v.id)
                        {
                            if (vw.isBooked == false)
                            {
                                vw.price = 0;
                            }
                        }
                    }
                }
            }
            db.SaveChanges();
        }

        public void cheepestVacationhouse(int i)
        {
            vacationhouse vacationhouse = null;
            int price = 0;
            foreach (vacationhouse v in db.vacationhouses)
            {
                foreach (vacationweek vw in db.vacationweeks)
                {
                    if (vw.vacationhouseid == v.id && vw.weeknumber == i)
                    {
                        if (vw.isBooked == false)
                        {
                            if (vw.price < price || price == 0)
                            {
                                vacationhouse = v;
                                price = (int)vw.price;
                            }
                        }
                    }
                }
            }
            if (price == 0)
            {
                Console.WriteLine("No vacationhouse available that week");
            }
            else
            {
                Console.WriteLine("Cheepest vacationhouse: "+ vacationhouse.destination+", "+vacationhouse.id+", "+vacationhouse.description+", "+price+" Kr");
            }
        }
    }
}
