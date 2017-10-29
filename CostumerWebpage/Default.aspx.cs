using ClassLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CostumerWebpage
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void btnLogin_Click(object sender, EventArgs e)
        {
            string s = Service.Instance.login(tBoxEmail.Text,tBoxPassword.Text);
            if (s == "")
            {
                lblError.Text = "Wrong email or password";
                lblError.Visible = true;
            }
            else
            {
                Session["costumer"] = s;
                Response.Redirect("Page1.aspx");
            }
        }

        protected void btnRegister_Click(object sender, EventArgs e)
        {
            Response.Redirect("Register.aspx");
        }
    }
}