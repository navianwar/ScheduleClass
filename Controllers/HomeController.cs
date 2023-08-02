using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ScheduleClass.Controllers
{
    public class HomeController : Controller
    {
        public string conn = @"Data Source =DESKTOP-7UNDENP\SQLEXPRESS; Integrated Security = true; Initial Catalog = Taskfor";
        DataTable dt = new DataTable();
        public ActionResult Index()
        {
            using (SqlConnection con = new SqlConnection(conn))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("spGetScheduleClasses", con);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);

            }

            return View(dt);
        }
        [HttpPost]
        public JsonResult Save(string Id)
        {
            try
            {
                SqlParameter[] parameters =
               {
                new SqlParameter("@Id",Id),

            };
                using (SqlConnection con = new SqlConnection(conn))
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("spInsertSchedule", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddRange(parameters);
                    long n = cmd.ExecuteNonQuery();
                }
            }
            catch(Exception ex)
            { 

            }
            return Json("Success");
        }


    }
}

