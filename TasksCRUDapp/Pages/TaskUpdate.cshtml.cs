using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;

namespace TasksCRUDapp.Pages
{
    public class TaskUpdateModel : PageModel
    {
        public void OnGet()
        {
        }
        public static string errorMessage = null;

        // Database connection string
        public static string connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=TasksDB;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False";
        public void OnPost()
        {           
            // Update task
            if (Request.Form["action"] == "update")
            {
                int id = int.Parse(Request.Form["id"]);
                string description = Request.Form["description"];
                DateTime date = DateTime.Parse(Request.Form["date"]);
                bool complete = Request.Form["complete"] == "on";

                try
                {
                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        connection.Open();
                        string query = "UPDATE tasks SET Description = @Description, Date = @Date, is_complete = @Complete WHERE Id = @Id";
                        SqlCommand command = new SqlCommand(query, connection);
                        command.Parameters.AddWithValue("@Id", id);
                        command.Parameters.AddWithValue("@Description", description);
                        command.Parameters.AddWithValue("@Date", date);
                        command.Parameters.AddWithValue("@Complete", complete);
                        command.ExecuteNonQuery();
                    }
                }
                catch (Exception ex)
                {
                    errorMessage = ex.Message;
                }
            }

          
        }
    }
}
