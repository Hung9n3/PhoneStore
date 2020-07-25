
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication4.Data;
using WebApplication4.Data.ModifyFilters;
using WebApplication4.Data.UserModel;

namespace WebApplication4.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class PhoneController : ControllerBase
    {
        private AppDbContext _db;
        private UserManager<User> _userManager;
        public PhoneController(AppDbContext _db, UserManager<User> _userManager)
        {
            this._db = _db;
            this._userManager = _userManager;
        }

        [HttpGet]

        public List<PhoneGet> GetPhoneByInclude()
        {

            var listP = _db.Phones.Include(x => x.PhoneBattery)
                                  .Select(x => new PhoneGet
                                  {
                                      PhoneName = x.PhoneName,
                                      BatteryVolume = (double)x.PhoneBattery.Volume,
                                      OSID = (int)x.OSID,
                                      Price = (int)x.Price
                                  })
                                  .ToList();

            return listP;
        }

        [HttpGet("{id}")]
        public Phone GetPhoneByID(int id)
        {
            var ListP = _db.Phones
                           .FirstOrDefault(s => s.PhoneId == id);
            return ListP;
        }
        [HttpPost]
       
        [HttpPost]
        public Phone AddPhone([FromBody] PhonePOST phone)
        {
            var pB = _db.Batteries.FirstOrDefault(x => x.BatteryID == phone.Battery);
            var P = new Phone();
            P.PhoneName = phone.PhoneName;
            P.Price = phone.Price;
            P.OSID = phone.OSID;
            P.PhoneBattery = pB;
            _db.Phones.Add(P);
            _db.SaveChanges();
            return P;
        }

        [HttpPost]
        public void AddOS(OS os)
        {
            _db.OS.Add(os);
            _db.SaveChanges();
        }
        [HttpGet]
        public List<OS> GetOS()
        {
            var listOS = _db.OS.ToList();
            return listOS;
        }

        [HttpPost("{name}")]
        public void DeletePhoneByName(string name)
        {
            var phones = _db.Phones.Where(x => x.PhoneName.Contains(name)).FirstOrDefault();
            _db.Remove(phones);
            _db.SaveChanges();
        }
        [HttpGet("{name}")]
        public async Task<IActionResult> Search(string name)
        {
            var listP =
            from Phone in _db.Phones.AsNoTracking().Where(x => x.PhoneName.Contains(name))
            join osName in _db.OS on Phone.OSID equals osName.OSID into RequestPhone
            from m in RequestPhone.DefaultIfEmpty()
            select new
            {
                PhoneName = Phone.PhoneName,
                OsName = m.OsName,
                Battery = Phone.PhoneBattery.Volume

            };

            return Ok(await listP.AsNoTracking().ToListAsync());
        }
        [HttpGet]
        public async Task<IActionResult> GetPhoneByJoin()
        {
            var result = from Phone in _db.Phones.AsNoTracking()
                         join osName in _db.OS on Phone.OSID equals osName.OSID into RequestPhone
                         from m in RequestPhone.DefaultIfEmpty()
                         select new
                         {
                             PhoneName = Phone.PhoneName,
                             OsName = m.OsName,
                             Battery = Phone.PhoneBattery.Volume,
                             Price = Phone.Price

                         };
            return Ok(await result.AsNoTracking().ToListAsync());
        }
        [HttpPost("{id}")]
        public IActionResult UpdatePhone(int id, Phone phone)
        {
            var P = GetPhoneByID(id);
            if (phone.OSID.HasValue)
            {
                P.OSID = phone.OSID;
            }
            if(phone.Price.HasValue)
            {
                P.Price = phone.Price;
            }
            //if(phone.PhoneBattery.Volume.HasValue)
            //{
            //    P.PhoneBattery.Volume = phone.PhoneBattery.Volume;
            //}
            _db.Phones.Update(P);
            //if(P == null)
            //{
            //    return BadRequest();
            //}
            //P.OSID = phone.OSID;
            //_db.Phones.Attach(P);

            //_db.Entry(P).State = EntityState.Modified;

         //   _db.Phones.Update(phone);
            _db.SaveChanges();
            return Ok();
        }
        [HttpPost("{name}")]
        public void DeleteListPhoneByName(string name)
        {
            var phones = _db.Phones.Where(x => x.PhoneName.Contains(name)).ToList();
            foreach (var phone in phones)
            {
                _db.Phones.Remove(phone);
            }
            _db.SaveChanges();
        }
       [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> AddOrder(OrderPost order)
        {
            string userId = User.Claims.First(c => c.Type == "UserId").Value;
            var user = await _userManager.FindByIdAsync(userId);
            var O = new Order();
            O.Owner = user;
            foreach (Phone p in order.ListPhones)
            {
                var phone = _db.Phones.FirstOrDefault(x => x.PhoneName == p.PhoneName);
                if(phone == null)
                {
                    return BadRequest();
                }
                else
                {
                    OrderPhone orderPhone = new OrderPhone();
                    orderPhone.Phone = phone;
                    orderPhone.Order = O;
                    _db.OrderPhones.Add(orderPhone);
                }
            }
            
            _db.Orders.Add(O);
            _db.SaveChanges();
            return Ok(O);
        }
    }
}
