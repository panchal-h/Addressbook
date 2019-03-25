using System;
using System.Web;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealmDigital.Infrastructure
{
    public class ProjectSession
    {
        public static int UserID
        {
            get
            {
                if (HttpContext.Current.Session["UserID"] == null)
                {
                    return 0;
                }
                return Convert.ToInt32(HttpContext.Current.Session["UserID"]);
            }

            set
            {
                HttpContext.Current.Session["UserID"] = value;
            }
        }
    }
}
