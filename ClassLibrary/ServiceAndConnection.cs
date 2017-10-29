using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ClassLibrary
{
    public class Dal
    {
        static String connStr;
        static SqlConnection con;
        public Dal()
        {
            connStr = @"Data Source=BroMachine\SQLEXPRESS;" +
            "user id=sa; password=122333;" +
            "database=vacationreservation";
            con = new SqlConnection(connStr);
        }

        public SqlConnection getConnection()
        {
            return con;
        }
    }

    public class Service
    {
        Dal d = new Dal();
        SqlConnection con;
        private static Service instance;
        DataTable destinationTable = null;
        DataTable vacationHouseTable = null;
        private Service() { con = d.getConnection(); }

        public static Service Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new Service();
                }
                return instance;
            }
        }

        public DataTable LoadDestinationTable()
        {
            string sql = "select * from destination";
            SqlDataAdapter da = new SqlDataAdapter(sql, con);
            destinationTable = new DataTable();
            da.Fill(destinationTable);

            return destinationTable;
        }

        public DataTable LoadVacationHouseTable()
        {
            string sql = "select * from vacationhouse";
            SqlDataAdapter da = new SqlDataAdapter(sql, con);
            vacationHouseTable = new DataTable();
            da.Fill(vacationHouseTable);

            return vacationHouseTable;
        }
        public void executeQuery(SqlCommand cmd)
        {
            try
            {
                con.Open();
                int n = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message.ToString());
            }
            finally
            {
                if (con.State == ConnectionState.Open) con.Close();
            }
        }

        // Destination
        public void createDestination(Destination d)
        {
            string sql = "";
            sql += "INSERT INTO";
            sql += " destination(name,dayofchange,priceprperson)";
            sql += " VALUES ('" + d.Name + "','" + d.DayOfChange + "'," + d.PricePrPerson + ")";
            SqlCommand cmd = new SqlCommand(sql, con);

            executeQuery(cmd);
        }

        public void deleteDestination(string name)
        {
            string sql = "DELETE FROM destination WHERE name='" + name + "'";
            SqlCommand cmd = new SqlCommand(sql, con);

            executeQuery(cmd);
        }

        public void updateDestination(Destination destination)
        {
            string sql = "";
            sql += "UPDATE destination";
            sql += " SET dayofchange='" + destination.DayOfChange;
            sql += "' , priceprperson=" + destination.PricePrPerson;
            sql += " WHERE name='" + destination.Name+"'";
            SqlCommand cmd = new SqlCommand(sql, con);

            executeQuery(cmd);
        }
        public List<String> getDestinations()
        {
            List<String> destinations = new List<string>();
            string sql = "SELECT name FROM destination";
            SqlCommand cmd = new SqlCommand(sql, con);

            try
            {
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    string line = reader["name"] as string;
                    if(!destinations.Contains(line))
                        destinations.Add(line);
                }
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message.ToString());
            }
            finally
            {
                if (con.State == ConnectionState.Open) con.Close();
            }
            return destinations;
        }

        // VacationHouse
        public void createVacationHouse(VacationHouse v, string destination)
        {
            string sql = "";
            sql += "INSERT INTO";
            sql += " vacationhouse(description,maxpersons,distancetoshopping,distancetobeach,destination)";
            sql += " VALUES ('" + v.Description + "'," + v.MaxPersons + "," + v.DistanceToShopping + "," + v.DistanceToBeach + ",'" + destination + "')";
            SqlCommand cmd = new SqlCommand(sql, con);

            executeQuery(cmd);
        }

        public void deleteVacationHouse(int id)
        {
            string sql = "DELETE FROM vacationhouse WHERE id=" + id;
            SqlCommand cmd = new SqlCommand(sql, con);

            executeQuery(cmd);
        }

        public void updateVacationHouse(VacationHouse vacationHouse, int id)
        {
            string sql = "";
            sql += "UPDATE vacationhouse";
            sql += " SET description='" + vacationHouse.Description;
            sql += "' , maxpersons=" + vacationHouse.MaxPersons;
            sql += " , distancetoshopping=" + vacationHouse.DistanceToShopping;
            sql += " , distancetobeach=" + vacationHouse.DistanceToBeach;
            sql += " WHERE id=" + id;
            SqlCommand cmd = new SqlCommand(sql, con);

            executeQuery(cmd);
        }

        public List<VacationHouse> getVacationHouses(string destination)
        {
            List<VacationHouse> vacationHouses = new List<VacationHouse>();
            string sql = "SELECT description,maxpersons,distancetoshopping,distancetobeach FROM vacationhouse WHERE destination='" + destination + "'";
            SqlCommand cmd = new SqlCommand(sql, con);

            try
            {
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    string desc = reader["description"] as string;
                    int persons = (int)reader["maxpersons"];
                    int shopping = (int)reader["distancetoshopping"];
                    int beach = (int)reader["distancetobeach"];
                    VacationHouse vacationHouse = new VacationHouse(desc, persons, shopping, beach);
                    if(!vacationHouses.Contains(vacationHouse))
                    vacationHouses.Add(vacationHouse);
                }
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message.ToString());
            }
            finally
            {
                if (con.State == ConnectionState.Open) con.Close();
            }
            return vacationHouses;
        }

        public int getVacationHouseId(VacationHouse v)
        {
            int id = 0;
            string sql = "SELECT id FROM vacationhouse WHERE description='" + v.Description + "'";
            SqlCommand cmd = new SqlCommand(sql, con);

            try
            {
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    int line = (int)reader["id"];
                    id = line;
                }
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message.ToString());
            }
            finally
            {
                if (con.State == ConnectionState.Open) con.Close();
            }
            return id;
        }

        // VacationHouseWeeks
        public void createVacationWeek(int vacationHouseId, VacationWeek vacationWeek)
        {
            string sql = "";
            sql += "INSERT INTO";
            sql += " vacationweek(weeknumber,price,isBooked,vacationhouseid)";
            sql += " VALUES (" + vacationWeek.WeekNumber + "," + vacationWeek.Price + ",'" + vacationWeek.IsBooked + "'," + vacationHouseId + ")";
            SqlCommand cmd = new SqlCommand(sql, con);

            executeQuery(cmd);
        }

        public void updateVacationWeek(int vacationHouseId, VacationWeek vacationWeek)
        {
            string sql = "";
            sql += "UPDATE vacationweek";
            sql += " SET price=" + vacationWeek.Price;
            sql += " WHERE vacationhouseid=" + vacationHouseId + " and weeknumber=" + vacationWeek.WeekNumber;
            SqlCommand cmd = new SqlCommand(sql, con);

            executeQuery(cmd);
        }

        public List<VacationWeek> getVacationHousesWeeks(int i)
        {
            List<VacationWeek> vacationWeeks = new List<VacationWeek>();
            string sql = "SELECT weeknumber,price,isBooked FROM vacationweek WHERE vacationhouseid=" + i + "";
            SqlCommand cmd = new SqlCommand(sql, con);

            try
            {
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    int week = (int)reader["weeknumber"];
                    int price = (int)reader["price"];
                    Boolean isBooked = (Boolean)reader["isBooked"];
                    VacationWeek vacationWeek = new VacationWeek(week, price);
                    vacationWeek.IsBooked = isBooked;
                    if (!vacationWeeks.Contains(vacationWeek))
                        vacationWeeks.Add(vacationWeek);
                }
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message.ToString());
            }
            finally
            {
                if (con.State == ConnectionState.Open) con.Close();
            }
            return vacationWeeks;
        }

        // Costumer
        public void createCostumer(Costumer c)
        {
            string sql = "";
            sql += "INSERT INTO";
            sql += " costumer(name,email,password)";
            sql += " VALUES ('" + c.Name + "','" + c.Email + "','" + c.Password + "')";
            SqlCommand cmd = new SqlCommand(sql, con);

            executeQuery(cmd);
        }

        // Booking
        public string createBooking(Booking b, string costumer, int vacationHouseId)
        {
            string s = "";
            string sql = "";
            sql += "BEGIN tran";
            sql += " IF exists(select * from booking where startweek="+b.StartWeek+" and vacationhouse="+ vacationHouseId+")";
            sql += " select 'optaget'";
            sql += " else";
            sql += " begin";
            sql += " INSERT INTO";
            sql += " booking(numberofpeople,startweek,price,costumer,vacationhouse)";
            sql += " VALUES (" + b.NumberOfPeople + " , " + b.StartWeek + " , " + b.Price + " , '" + costumer + "' , " + vacationHouseId + ")";
            sql += " select 'oprettet'";
            sql += " end";
            sql += " commit tran";
            SqlCommand cmd = new SqlCommand(sql, con);

            try
            {
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    string myString = reader.GetString(0);
                    s = myString;
                }
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message.ToString());
            }
            finally
            {
                if (con.State == ConnectionState.Open) con.Close();
            }
            return s;
        }

        public string login(string email, string password)
        {
            string s = "";
            string sql = "SELECT email,password FROM costumer WHERE email='" + email + "' and password='"+ password+"'";
            SqlCommand cmd = new SqlCommand(sql, con);

            try
            {
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    string line = (string)reader["email"];
                    s = line;
                }
            }
            catch (Exception ex)
            {
            }
            finally
            {
                if (con.State == ConnectionState.Open) con.Close();
            }
            return s;
        }

        public int getWeekPrice(int vacationhouseid, int weeknumber)
        {
            int price = 0;
            string sql = "SELECT price FROM vacationweek WHERE vacationhouseid=" + vacationhouseid + " and weeknumber=" + weeknumber;
            SqlCommand cmd = new SqlCommand(sql, con);

            try
            {
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    int line = (int)reader["price"];
                    price = line;
                }
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message.ToString());
            }
            finally
            {
                if (con.State == ConnectionState.Open) con.Close();
            }
            return price;
        }

        public int getDestinationPrice(string destination)
        {
            int price = 0;
            string sql = "SELECT priceprperson FROM destination WHERE name='" + destination+"'";
            SqlCommand cmd = new SqlCommand(sql, con);

            try
            {
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    int line = (int)reader["priceprperson"];
                    price = line;
                }
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message.ToString());
            }
            finally
            {
                if (con.State == ConnectionState.Open) con.Close();
            }
            return price;
        }

        public void bookHouse(int vacationHouseId, int weekNumber)
        {
            Boolean b = true;
            string sql = "";
            sql += "UPDATE vacationweek";
            sql += " SET isBooked='" + b;
            sql += "' WHERE vacationhouseid=" + vacationHouseId + " and weeknumber=" + weekNumber;
            SqlCommand cmd = new SqlCommand(sql, con);

            executeQuery(cmd);
        }

        public List<int> getAirplaneSpots(string s, int i)
        {
            List<int> spots = new List<int>();
            string sql = "SELECT todestination FROM airplanedestination WHERE destination='" + s + "' AND weeknumber=" + i;
            SqlCommand cmd = new SqlCommand(sql, con);

            try
            {
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    int to = (int)reader["todestination"];
                    spots.Add(to);
                }
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message.ToString());
            }
            finally
            {
                if (con.State == ConnectionState.Open) con.Close();
            }

            if (i + 1 == 53)
            {
                // siden programmet kun virker for 1 år så har jeg valgt at hvis kunden bestiller i uge 52 så er der plads til til hjemrejsen næste år
                spots.Add(500);
            }
            else
            {
                sql = "SELECT fromdestination FROM airplanedestination WHERE destination='" + s + "' AND weeknumber=" + i + 1;
                cmd = new SqlCommand(sql, con);

                try
                {
                    con.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        int from = (int)reader["fromdestination"];
                        spots.Add(from);
                    }
                }
                catch (Exception ex)
                {
                    System.Windows.Forms.MessageBox.Show(ex.Message.ToString());
                }
                finally
                {
                    if (con.State == ConnectionState.Open) con.Close();
                }
            }
            return spots;
        }
    }
}
