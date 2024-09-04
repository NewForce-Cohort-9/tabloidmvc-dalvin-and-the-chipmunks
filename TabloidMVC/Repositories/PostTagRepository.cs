using NuGet.Protocol.Plugins;
using TabloidMVC.Models;

namespace TabloidMVC.Repositories
{
    public class PostTagRepository : BaseRepository, IPostTagRepository
    {
        public PostTagRepository(IConfiguration config) : base(config) { }
        public void Add(PostTag postTag)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                        INSERT INTO PostTag ( PostId, TagId )
                        OUTPUT INSERTED.ID
                        VALUES ( @postId, @tagId )";
                    cmd.Parameters.AddWithValue("@postId", postTag.PostId);
                    cmd.Parameters.AddWithValue("@tagId", postTag.TagId);

                    postTag.Id = (int)cmd.ExecuteScalar();
                }
            }
        }
        public List<PostTag> GetAll()
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                        SELECT pt.Id, pt.PostId, pt.TagId
                        FROM PostTag pt
                        ";
                    var reader = cmd.ExecuteReader();

                    var postTags = new List<PostTag>();

                    while (reader.Read())
                    {
                        postTags.Add(new PostTag()
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("Id")),
                            PostId = reader.GetInt32(reader.GetOrdinal("PostId")),
                            TagId = reader.GetInt32(reader.GetOrdinal("TagId")),
                        });
                    }

                    reader.Close();

                    return postTags;
                }
            }
        }
    }
}
