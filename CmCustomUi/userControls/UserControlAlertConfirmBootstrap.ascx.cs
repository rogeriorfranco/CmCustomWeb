using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CmCustomUi.userControls
{
    public partial class UserControlAlertConfirmBootstrap : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        public event EventHandler btn_Click;

        protected void btnConfirmaOk_Click(object sender, EventArgs e)
        {
            if (btn_Click != null)
            {
                btn_Click(sender, e);
            }
        }

      

    }
}