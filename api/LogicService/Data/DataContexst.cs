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
            MongoClient client = new MongoClient("mongodb://localhost:27017");
            IMongoDatabase database = client.GetDatabase("carpool");

            _Organization = database.GetCollection<OrganizationInfoEO>("organization");
            _Users = database.GetCollection<UserInfoEO>("users");
            _requsts = database.GetCollection<Request>("requsts");
        }

    }
}
