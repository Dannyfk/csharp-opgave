using ClassLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CostumerWebpage
{
    public partial class Page1 : System.Web.UI.Page
    {
        string destination = "";
        List<VacationHouse> vacationHouses = new List<VacationHouse>();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                destinationList.DataSource = Service.Instance.getDestinations();
                destinationList.DataBind();
            }
            else
            {
                if (Session["displayweekcheck"] != null)
                {
                    Boolean b = (Boolean)Session["displayweekcheck"];
                    if (b == true)
                    {
                        int i = (int)Session["vacationHouseId"];
                        displayWeeks(i);
                    }
                }
                if (Session["weekcheck"] != null)
                {
                    Boolean b = (Boolean)Session["weekcheck"];
                    if (b == true)
                    {
                        Label newLbl = new Label();
                        newLbl.Text = "How many persons?";
                        PlaceHolder2.Controls.Add(newLbl);
                        TextBox tBoxPersons = new TextBox();
                        tBoxPersons.ID = "tbox1";
                        PlaceHolder2.Controls.Add(tBoxPersons);
                        Button newBtn2 = new Button();
                        newBtn2.Text = "Checkout";
                        newBtn2.Click += new EventHandler(newBtn2_Click);
                        PlaceHolder2.Controls.Add(newBtn2);
                    }
                }
            }
        }

        protected void destinationList_SelectedIndexChanged(object sender, EventArgs e)
        {
                lblPersons.Visible = false;
                lblShopping.Visible = false;
                lblBeach.Visible = false;
                destination = destinationList.SelectedValue;
                Session["destination"] = destination;
                vacationHouseList.Visible = true;
                vacationHouses = Service.Instance.getVacationHouses(destination);
                Session["vacationHouses"] = vacationHouses;
                vacationHouseList.DataSource = vacationHouses;
                vacationHouseList.DataBind();

                Boolean b = false;
                Session["displayweekcheck"] = b;
                Session["weekcheck"] = b;
        }

        protected void vacationHouseList_SelectedIndexChanged(object sender, EventArgs e)
        {
            lblPersons.Visible = true;
            lblShopping.Visible = true;
            lblBeach.Visible = true;
            vacationHouses = (List<VacationHouse>)Session["vacationHouses"];
            VacationHouse vacationhouse =vacationHouses[vacationHouseList.SelectedIndex];
            Session["vacationHouse"] = vacationhouse;
            lblPersons.Text = vacationhouse.MaxPersons+"";
            lblShopping.Text = vacationhouse.DistanceToShopping+"";
            lblBeach.Text = vacationhouse.DistanceToBeach+"";

            int i = Service.Instance.getVacationHouseId(vacationhouse);
            Session["vacationHouseId"] = i;
            displayWeeks(i);
            lblInfo.Visible = true;

            Boolean b = true;
            Session["displayweekcheck"] = b;
            b = false;
            Session["weekcheck"] = b;
        }
        protected void displayWeeks(int i)
        {
            List<VacationWeek> vacationWeeks = new List<VacationWeek>();
            vacationWeeks = Service.Instance.getVacationHousesWeeks(i);
            foreach (VacationWeek v in vacationWeeks)
            {
                Button newBtn = new Button();
                newBtn.ID = v.WeekNumber + "";
                if (v.Price == 0)
                {
                    newBtn.Text = "Week: " + v.WeekNumber;
                    newBtn.Enabled = false;
                }
                else if (v.IsBooked == false && v.Price != 0)
                {
                    newBtn.Text = "Week: " + v.WeekNumber + " - Price: " + v.Price + " Kr";
                    newBtn.BackColor = System.Drawing.Color.Green;
                }
                else if (v.IsBooked == true && v.Price != 0)
                {
                    newBtn.Text = "Week: " + v.WeekNumber + " - Price: " + v.Price + " Kr";
                    newBtn.Enabled = false;
                    newBtn.BackColor = System.Drawing.Color.Red;
                }
                newBtn.Click += new EventHandler(newBtn_Click);
                PlaceHolder1.Controls.Add(newBtn);
            }
        }
        protected void newBtn_Click(object sender, EventArgs e)
        {
            string s = ((Button)sender).ID;
            int i = Convert.ToInt32(s);
            Session["vacationWeekNumber"] = i;
            Boolean b = true;
            Session["weekcheck"] = b;
        }

        protected void newBtn2_Click(object sender, EventArgs e)
        {
            TextBox t = (TextBox)PlaceHolder1.FindControl("tbox1");
            Session["persons"] = t.Text;
            VacationHouse v = (VacationHouse)Session["vacationHouse"];
            string s = (string)Session["destination"];
            int i = (int)Session["vacationWeekNumber"];
            List<int> spots = Service.Instance.getAirplaneSpots(s, i);

            if (Convert.ToInt32(t.Text) > v.MaxPersons)
            {
                Label newLbl = new Label();
                newLbl.Text = "Too many persons";
                PlaceHolder2.Controls.Add(newLbl);
            }
            else
            Response.Redirect("BookingPage.aspx");
        }

        protected void btnLogout_Click(object sender, EventArgs e)
        {
            Response.Redirect("Default.aspx");
        }
    }
}