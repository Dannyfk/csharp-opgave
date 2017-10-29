using ClassLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CostumerWebpage
{
    public partial class BookingPage : System.Web.UI.Page
    {
        Booking booking;
        string email;
        int vacationHouseId;
        int weekNumber;
        protected void Page_Load(object sender, EventArgs e)
        {
            string destination = (string)Session["destination"];
            Label1.Text = "Destination: " + destination;
            VacationHouse v = (VacationHouse)Session["vacationHouse"];
            Label2.Text = "Vacation house description: " + v.Description;
            string persons = (string)Session["persons"];
            Label3.Text = "Number of persons: " + persons;
            vacationHouseId = (int)Session["vacationHouseId"];
            weekNumber = (int)Session["vacationWeekNumber"];
            int destinationPrice = Service.Instance.getDestinationPrice(destination);
            int weekPrice = Service.Instance.getWeekPrice(vacationHouseId, weekNumber);
            int price = Convert.ToInt32(persons) * destinationPrice + weekPrice;
            Label4.Text = "Price total: " + price + " Kr";
            email = (string)Session["costumer"];
            booking = new Booking(Convert.ToInt32(persons), weekNumber, price);
        }

        protected void btnConfirm_Click(object sender, EventArgs e)
        {  
           string s = Service.Instance.createBooking(booking, email, vacationHouseId);
           if (s == "optaget")
           {
               System.Windows.Forms.MessageBox.Show("That house has just been booked for that week, you'll be returned to earlier page");
               Response.Redirect("Page1.aspx");
           }
           else
           {
               Service.Instance.bookHouse(vacationHouseId, weekNumber);
               System.Windows.Forms.MessageBox.Show("Booking confirmed and receipt send to email");
               Response.Redirect("Page1.aspx");
           }
        }

        protected void btnReturn_Click(object sender, EventArgs e)
        {
            Response.Redirect("Page1.aspx");
        }
    }
}