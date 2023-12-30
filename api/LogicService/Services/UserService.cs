using AutoMapper;
using LogicService.Data;
using LogicService.Dto;
using LogicService.EO;
using MongoDB.Driver;
using Newtonsoft.Json;
using System.IdentityModel.Tokens.Jwt;

namespace LogicService.Services
{
    public class UserService
    {
        private readonly DataContexst _DataContexst;
        private readonly IMapper _mapper;
        private readonly TokenService _tokenService;


        public UserService(DataContexst dataContexst, IMapper mapper, TokenService tokenService)
        {
            _DataContexst = dataContexst;
            _mapper = mapper;
            _tokenService = tokenService;
        }

        public async Task<OrgResponseDto> CreateUser(userSignInDto user)
        {
            if (await CheckUserExist(user.Phone)) return new(false, "user Exist");
            try
            {
                var userMapped = _mapper.Map<UserInfoEO>(user);
                await _DataContexst._Users.InsertOneAsync(userMapped);

                return new(true, "user created", "", _tokenService.GenerateJwtTokenUser(user.Phone, "user"));
            }
            catch (Exception e)
            {
                await Console.Out.WriteLineAsync("UserService" + e);
                return new(false, "somthing went worng");
            }
        }

        public async Task<UserInfoEO?> GetUser(string? jwt)
        {
            var handler = new JwtSecurityTokenHandler();
            var jsonToken = handler.ReadToken(jwt) as JwtSecurityToken;
            var phone = jsonToken?.Claims.FirstOrDefault(claim => claim.Type == "MobilePhone")?.Value;
            if (phone == null) return null;

            FilterDefinition<UserInfoEO> filter = Builders<UserInfoEO>.Filter.Eq("Phone", phone);
            try
            {
                var user = await _DataContexst._Users.Find(filter).FirstOrDefaultAsync();
                if (user == null) return null;
                return user;
            }
            catch (Exception)
            {
                return null;
            }
        }
        public async Task<userSignInDto?> GetUserDto(string? jwt)
        {
            var user = await GetUser(jwt);
            if (user == null) return null;
            var userMapped = _mapper.Map<userSignInDto>(user);
            return userMapped;
        }

        public async Task<OrgResponseDto> UpdateParameters(UserInfoEO user, string? jwt)
        {
            var handler = new JwtSecurityTokenHandler();
            var jsonToken = handler.ReadToken(jwt) as JwtSecurityToken;
            var phone = jsonToken?.Claims.FirstOrDefault(claim => claim.Type == "MobilePhone")?.Value;
            if (phone == null) return new(false, "שגיאה");

            try
            {
                if (!await CheckUserExist(user.Phone)) return new(false, "משתמש לא קיים");

                FilterDefinition<UserInfoEO> filter = Builders<UserInfoEO>.Filter.Eq("Phone", phone);
                var update = Builders<UserInfoEO>.Update.Set("Nickname", user.Nickname)
                           .Set(x => x.Car!.Year, user?.Car?.Year)
                           .Set(x => x.Car!.TypeCar, user?.Car?.TypeCar)
                           .Set(x => x.Car!.Seats, user?.Car?.Seats)
                           .Set(x => x.Car!.TrunkSize, user?.Car?.TrunkSize);

                await _DataContexst._Users.UpdateOneAsync(filter, update);

                return new(true, "עודכן בהצלחה");
            }
            catch (Exception)
            {
                return new(false, "שגיאה");
            }
        }

        public async Task<bool> CheckUserExist(string? phone)
        {
            if (phone == null) return false;

            FilterDefinition<UserInfoEO> filter = Builders<UserInfoEO>.Filter.Eq("Phone", phone);
            try
            {
                var user = await _DataContexst._Users.Find(filter).FirstOrDefaultAsync();
                return user != null;
            }
            catch (Exception)
            {
                return false;
            }
        }


        public async Task<SimpelResponse> CanExecuteTask(string jwt, RequstDto task)
        {
            var user = await GetUserDto(jwt);
            if (user == null)
            {
                return new SimpelResponse(false, "הלקוח לא נמצא");
            }

            var filter = Builders<Request>.Filter.Eq(r => r.Id, task.Id);

            var existingTask = await _DataContexst._requsts.Find(filter).FirstOrDefaultAsync();

            if (existingTask == null || existingTask.Executed || existingTask.lastModified != task.lastModified)
            {
                return new SimpelResponse(false, "המשימה נלקחה");
            }

            var update = Builders<Request>.Update.Set("User", user)
            .Set("Executed", true)
            .Set("Executed_Time", DateTime.Now)
            .Set("Status", "בביצוע");
            await _DataContexst._requsts.UpdateOneAsync(filter, update);

            await AddTask(user, task);
            return new SimpelResponse(true, "קיבלת את המשימה");
        }

        public async Task AddTask(userSignInDto user, RequstDto task)
        {
            var filter = Builders<UserInfoEO>.Filter.Eq(u => u.Phone, user.Phone);
            var update = Builders<UserInfoEO>.Update.Push(u => u.Tasks, task);

            var result = await _DataContexst._Users.UpdateOneAsync(filter, update);
        }

        public async Task<List<RequstDto>?> GetTasksHistory(string? jwt)
        {
            var user = await GetUser(jwt);
            if (user == null) return null;
            return user.Tasks;
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
                double distance = CalculateDistance(coord.origin, new LatLng(item.Origin.Lat, item.Origin.Lng)) + CalculateDistance(coord.destination, new LatLng(item.Destination.Lat, item.Destination.Lng));

                if (sortedList.Count < 8 || distance < sortedList.Max(r => r.Distance))
                {
                    // Add the request with distance to the sorted list
                    var Mappditem = _mapper.Map<RequstDto>(item);
                    Mappditem.Distance = distance;
                    sortedList.Add(Mappditem);

                    // Keep the list sorted by distance in ascending order
                    sortedList = sortedList.OrderBy(r => r.Distance).ToList();

                    // If the list size exceeds 6, remove the last element (max distance)
                    if (sortedList.Count > 7)
                    {
                        sortedList.RemoveAt(sortedList.Count - 1);
                    }

                }
            }

            //sort by time 
            foreach (var item in sortedList)
            {
                item.DistanceMinutes = await RealTimeTraffic(coord, item) ?? 0;

            }

            sortedList = sortedList.OrderBy(r => r.DistanceMinutes).ToList();

            return sortedList;
        }

        async Task<double?> RealTimeTraffic(UserLatLng coord, RequstDto item)
        {
            try
            {
                var http = new HttpClient();

                string Baseurl = "https://api.geoapify.com/v1/routing?waypoints=";
                string waypoints = $"{coord.origin.Lat},{coord.origin.Lng}|{item.Origin.Lat},{item.Origin.Lng}|{item.Destination.Lat},{item.Destination.Lng}|{coord.destination.Lat},{coord.destination.Lng}";
                string mode = $"&mode=drive&lang=en&apiKey=a22c04fed7ef4822a95c96d549236102\r\n";
                var response = await http.GetAsync($"{Baseurl}{waypoints}{mode}");
                if (response == null) { return null; }
                response.EnsureSuccessStatusCode();

                // Read and return the response content as a string
                var jsonResponse = await response.Content.ReadAsStringAsync();
                Root dto = JsonConvert.DeserializeObject<Root>(jsonResponse);

                if (dto != null)
                {
                    return dto.Features[0].Properties.Time;
                }
            }
            catch (Exception)
            {

            }
            return null;
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
