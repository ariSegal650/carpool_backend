using LogicService.Data;
using LogicService.Dto;
using LogicService.EO;
using MongoDB.Bson;
using MongoDB.Driver;

namespace LogicService.Services
{
    public class UserService
    {
        private readonly DataContexst _DataContexst;
        public UserService(DataContexst dataContexst)
        {
            _DataContexst = dataContexst;
        }

        public async Task<ErrorResponse> CreateUser(UserInfoEO user)
        {
            if (CheckUserExist(user.Phone)) return new(false, "user Exist");
            try
            {
                await _DataContexst._Users.InsertOneAsync(user);
                 return new(true, "user created");
            }
            catch (Exception e)
            {
                await Console.Out.WriteLineAsync("UserService"+e);
                 return new(false, "somthing went worng");
            }
        }
        public UserInfoEO? GetUser(string id)
        {
            FilterDefinition<UserInfoEO> filter = Builders<UserInfoEO>.Filter.Eq("Id", id);
            try
            {
                var user = (_DataContexst._Users.Find(filter).FirstOrDefault());
                return user;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public bool CheckUserExist(string phone)
        {
            FilterDefinition<UserInfoEO> filter = Builders<UserInfoEO>.Filter.Eq("Phone", phone);
            try
            {
                var user = (_DataContexst._Users.Find(filter).FirstOrDefault());
                return user != null;
            }
            catch (Exception)
            {
                return false;
            }

        }
    }
}
