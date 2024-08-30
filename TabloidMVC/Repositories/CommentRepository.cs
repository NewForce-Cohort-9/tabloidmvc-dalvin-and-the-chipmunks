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
    public class CommentRepository : BaseRepository, ICommentRepository
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
                              u.DisplayName 
                         FROM Comment c
                              LEFT JOIN UserProfile u ON c.UserProfileId = u.id
                        ORDER BY c.CreateDateTime DESC
                        WHERE c.PostId = @PostId";

                    cmd.Parameters.AddWithValue("@PostId", PostId);

                    var reader = cmd.ExecuteReader();

                    var comments = new List<Comment>();

                    while (reader.Read())
                    {
                        comments.Add(NewCommentFromReader(reader));
                    }

                    reader.Close();

                    return comments;
                }
            }
        }
        
        private Comment NewCommentFromReader(SqlDataReader reader)
        {
            return new Comment()
            {
                Id = reader.GetInt32(reader.GetOrdinal("Id")),
                PostId = reader.GetInt32(reader.GetOrdinal("PostId")),
                Subject = reader.GetString(reader.GetOrdinal("Subject")),
                Content = reader.GetString(reader.GetOrdinal("Content")),
                CreateDateTime = reader.GetDateTime(reader.GetOrdinal("CreateDateTime")),
                UserProfile = new UserProfile()
                {
                    Id = reader.GetInt32(reader.GetOrdinal("UserProfileId")),
                    DisplayName = reader.GetString(reader.GetOrdinal("DisplayName"))
                }
            };
        }
    }
}
