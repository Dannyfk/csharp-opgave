using ClassLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CostumerWebpage
{
    public partial class Register : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnRegister_Click(object sender, EventArgs e)
        {
            Costumer costumer = new Costumer(tBoxName.Text, tBoxEmail.Text, tBoxPassword.Text);
            Service.Instance.createCostumer(costumer);
            Session["costumer"] = costumer.Email;
            Response.Redirect("Page1.aspx");
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("Default.aspx");
        }
    }
}