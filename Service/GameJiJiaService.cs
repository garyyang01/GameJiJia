using GameJiJia.Helper;
using GameJiJia.Models;
using GameJiJia.Repository;
using System.Threading.Tasks;

namespace GameJiJia.Service
{
    public class GameJiJiaService
    {
        private GameJiJiaRepository _gameJiJiaRepository;

        public GameJiJiaService()
        {
            _gameJiJiaRepository = new GameJiJiaRepository();
        }

        public async Task<bool> RegisterUser(UserInfo userInfo)
        {
            if (userInfo != null)
            {
                var dbInfo = await _gameJiJiaRepository.GetUserInfo(userInfo.AccountId);
                if (dbInfo != null)
                {
                    return false;
                }
                var encryptPassword = EncryptHelper.ComputeHash(userInfo.Password);
                userInfo.Password = encryptPassword;
                return await _gameJiJiaRepository.RegisterUser(userInfo);
            }

            return false;
        }

        public async Task<bool> LoginUser(UserInfo userInfo)
        {
            if (userInfo != null)
            {
                var dbInfo = await _gameJiJiaRepository.GetUserInfo(userInfo.AccountId);
                return dbInfo != null && EncryptHelper.VerifyHash(userInfo.Password, dbInfo.Password);
            }

            return false;
        }
    }
}