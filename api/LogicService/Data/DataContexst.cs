using LogicService.EO;
using MongoDB.Driver;


namespace LogicService.Data
{
    public class DataContexst
    {
        internal readonly IMongoCollection<OrganizationInfoEO> _Organization;
        internal readonly IMongoCollection<UserInfoEO> _Users;
        internal readonly IMongoCollection<Request> _requsts;


        public DataContexst()
        {

            //MongoClient client = new MongoClient("mongodb://backend-mongo-1:27017");

            MongoClient client = new MongoClient("mongodb://mongodb:27017");
            Console.WriteLine("Connected to MongoDB1!");

            IMongoDatabase database = client.GetDatabase("carpool");

            _Organization = database.GetCollection<OrganizationInfoEO>("organization");
            _Users = database.GetCollection<UserInfoEO>("users");
            _requsts = database.GetCollection<Request>("requsts");

            Console.WriteLine("Connected to MongoDB!");
        }

    }
}
