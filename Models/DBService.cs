using System.Data;
using System.Data.SqlClient;
using System.Linq;
using Dapper;

namespace MontageWebsite.Models
{
    public class DBService
    {
        IDbConnection conn;
        private string connString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=MontageList;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
        public DBService()
        {
            //conn = new SqlConnection(connString);
        }

        public Dictionary<string, string> GetStatuses(List<Video> videos)
        {
            Dictionary<string, string> vidStatuses = new Dictionary<string, string>();
            string sql = "SELECT ID, Status FROM videos WHERE ID IN ('";
            for(int i = 0; i < videos.Count - 1; i++)
            {
                sql += videos[i].ID + "','";
            }
            sql += videos[videos.Count-1].ID + "');";
            using (conn = new SqlConnection(connString))
            {
                try
                {
                    conn.Open();
                    vidStatuses = conn.Query(sql).ToDictionary(
                        row => (string)row.ID,
                        row => (string)row.Status);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    return vidStatuses;
                }
                return vidStatuses;
            }
        }

        public bool InsertSubmittedVideo(Video video)
        {
            string sql = "INSERT INTO videos (ID, Title, Description, ChannelID, ChannelTitle, ThumbnailUrl, Status) VALUES ('" +
                "" + video.ID + "', '" + video.Title + "', '" + video.Description + "', '" + video.ChannelID + "', '" + video.ChannelTitle +
                "', '" + video.ThumbnailUrl +  "', 'Submitted');";
            using (conn = new SqlConnection(connString))
            {
                try
                {
                    conn.Open();
                    conn.Execute(sql);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    return false;
                }
                return true;
            }
        }
    }
}
