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

            try
            {

                var settings = MongoClientSettings.FromConnectionString(connectionUri);

                // Set the ServerApi field of the settings object to Stable API version 1
                settings.ServerApi = new ServerApi(ServerApiVersion.V1);

                // Create a new client and connect to the server
                var client = new MongoClient(settings);

                Console.WriteLine("Connected to MongoDB1!");

                IMongoDatabase database = client.GetDatabase("carpool");

                _Organization = database.GetCollection<OrganizationInfoEO>("organization");
                _Users = database.GetCollection<UserInfoEO>("users");
                _requsts = database.GetCollection<Request>("requsts");

                Console.WriteLine("Connected to MongoDB!");
            }
            catch (System.Exception e)
            {
                System.Console.WriteLine(e);
                throw;
            }

        }

    }
}


