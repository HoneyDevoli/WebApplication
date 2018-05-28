using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NoNameApp.Entities;
using NoNameApp.BLL.Infrastructure;
using System.Security.Claims;

namespace NoNameApp.LogicContracts
{
    public interface IUserService:IDisposable 
    {
        Task<OperationDetails> Create(User user);
        Task<ClaimsIdentity> Authenticate(String email, String password);
        List<Episode> GetUserEpisode(User user);
        List<IGrouping<Serial, IGrouping<Season, Episode>>> GetUserSerials(User user);
        
        User GetUser(string email);
        void AddSerial(User user, Serial serial);
        bool CheckSerial(User user, Serial serial);
        void DelSerial(User user, Serial serial);
        void AddEpisode(User user, Episode episode);
        void DelAllEpisode(User user, Serial serial);
        Serial WatchedEp(User user, Serial serial);
        void AddRating(User user, Serial serial, int star);
        int GetRateUser(User user, Serial serial);
        int SpentTime(User user);
        OperationDetails EditUser(User user, string OldPassword, string NewPassword);
        void Remove(int? id);
        void AddOrder(string email);
        Order getOrder(string email);
        string ChangeRoleVip(int idOrder);
        List<Order> getOrders();

    }
}
