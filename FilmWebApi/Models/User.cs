using Amazon.DynamoDBv2.DataModel;

namespace FilmWebApi
{
    [DynamoDBTable("User")]
    public class User
    {
        [DynamoDBHashKey]
        public string Login { get; set; }
        [DynamoDBProperty]
        public string PasswordHash { get; set; }
    }
}