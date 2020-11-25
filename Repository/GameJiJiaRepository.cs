using GameJiJia.Helper;
using GameJiJia.Models;
using MongoDB.Bson;
using System.Linq;
using System.Threading.Tasks;

namespace GameJiJia.Repository
{
    public class GameJiJiaRepository
    {
        public async Task<bool> RegisterUser(UserInfo userInfo)
        {
            if (userInfo != null)
            {
                return await MongoDBHelper.Insert(userInfo, "UserInfo");
            }

            return false;
        }

        public async Task<UserInfo> GetUserInfo(string username)
        {
            if (username != null)
            {
                var userInfos = await MongoDBHelper.Read<UserInfo>("UserInfo", new BsonDocument("AccountId", username));
                if (userInfos?.Count==0)
                {
                    return null;
                }

                return userInfos.First();
            }

            return null;
        }
    }
}