using AutoMapper;
using LogicService.Data;
using LogicService.Dto;
using LogicService.EO;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Drawing;

namespace LogicService.Services
{
    public class UserService
    {
        private readonly DataContexst _DataContexst;
        private readonly IMapper _mapper;

        public UserService(DataContexst dataContexst, IMapper mapper)
        {
            _DataContexst = dataContexst;
            _mapper = mapper;
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
                var user = _DataContexst._Users.Find(filter).FirstOrDefault();
                return user != null;
            }
            catch (Exception)
            {
                return false;
            }

        }

        public async Task<List<RequstDto>> GetDistanceAsync(UserLatLng coord)
        {

            var collection = _DataContexst._requsts;
            if (collection == null)
            {
                return new List<RequstDto>();
            }
                var requests = await collection.Find(_ => true).ToListAsync();

                // Use a list instead of an array for dynamic sizing
                List<RequstDto> sortedList = new List<RequstDto>();

            foreach (var item in requests)
            {
                double distance = CalculateDistance(coord.origin,new LatLng(item.Origin.Lat,item.Origin.Lng)) + CalculateDistance(coord.destination, new LatLng(item.Destination.Lat, item.Destination.Lng));

                if (sortedList.Count < 6 || distance < sortedList.Max(r => r.Distance))
                {
                    // Add the request with distance to the sorted list
                    var Mappditem = _mapper.Map<RequstDto>(item);
                    Mappditem.Distance = distance;
                    sortedList.Add(Mappditem);

                    // Keep the list sorted by distance in ascending order
                    sortedList = sortedList.OrderBy(r => r.Distance).ToList();

                    // If the list size exceeds 6, remove the last element (max distance)
                    if (sortedList.Count > 6)
                    {
                        sortedList.RemoveAt(sortedList.Count - 1);
                    }
                }
            }

           

            return sortedList;
        }

        // Helper method to calculate Haversine distance between two coordinates
        private double CalculateDistance(LatLng coord1, LatLng coord2)
        {
            const double earthRadius = 6371; // Earth's radius in kilometers

            double dLat = DegreesToRadians(trans(coord2.Lat) - trans(coord1.Lat));
            double dLng = DegreesToRadians(trans(coord2.Lng) - trans(coord1.Lng));

            double a = Math.Sin(dLat / 2) * Math.Sin(dLat / 2) +
                       Math.Cos(DegreesToRadians(trans(coord1.Lat))) * Math.Cos(DegreesToRadians(trans(coord2.Lat))) *
                       Math.Sin(dLng / 2) * Math.Sin(dLng / 2);

            double c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));

            return earthRadius * c;
        }

        // Helper method to convert degrees to radians
        private double DegreesToRadians(double degrees)
        {
            return degrees * (Math.PI / 180);
        }

        private double trans(string str)
        {
           return double.Parse(str);
        }
    }
}
