using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection.PortableExecutable;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using TabloidMVC.Models;
using TabloidMVC.Utils;

namespace TabloidMVC.Repositories
{
    public class CommentRepository : BaseRepository
    {
        public CommentRepository(IConfiguration config) : base(config) { }

        public List<Comment> GetAllCommentsByPost(int PostId)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                       SELECT c.Id, c.PostId, c.UserProfileId, c.Subject, c.Content, 
                              c.CreateDateTime,
                              u.DisplayName,  
                         FROM Comment c
                              LEFT JOIN UserProfile u ON c.UserProfileId = u.id
                        WHERE IsApproved = 1 AND PublishDateTime < SYSDATETIME()
                               AND c.PostId = PostId";
                    var reader = cmd.ExecuteReader();

                    var comments = new List<Post>();

                    while (reader.Read())
                    {
                        comments.Add(NewPostFromReader(reader));
                    }

                    reader.Close();

                    return comments;
                }
            }
        }
        
    }
}
