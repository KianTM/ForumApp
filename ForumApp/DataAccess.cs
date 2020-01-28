using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;

namespace ForumApp
{
    public class DataAccess
    {
        public List<Comment> GetAllComments()
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(Helper.CnnVal("ForumDB")))
            {
                var output = connection.Query<Comment>("dbo.Comments_GetAllCommentsByPostTime").ToList();
                return output;
            }
        }

        public void InsertComment(string text, string censoredText)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(Helper.CnnVal("ForumDB")))
            {
                List<Comment> comments = new List<Comment>();

                comments.Add(new Comment() { Text = text, CensoredText = censoredText, PostTime = DateTime.Now });

                connection.Execute("dbo.Comments_Insert @Text, @CensoredText, @PostTime", comments);
            }
        }

        public void DeleteComment(int id)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(Helper.CnnVal("ForumDB")))
            {
                connection.Execute("dbo.Comments_Delete @Id", new { Id = id });
            }
        }

        public void UpdateComment(int id, string text, string censoredText)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(Helper.CnnVal("ForumDB")))
            {
                connection.Execute("dbo.Comments_Update @Id, @Text, @CensoredText", new { Id = id, Text = text, CensoredText = censoredText });
            }
        }
    }
}
