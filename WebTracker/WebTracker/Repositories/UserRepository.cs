using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WebTracker.Data;
using WebTracker.Models;


namespace WebTracker.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _context;
        public UserRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public List<User> GetAllUsers()
        {
            //var users = _context.Users.Include(u => u.Web).Include(v => v.Location);
           
           return _context.Users
                    .Include(u => u.Location)
                    .Include(v => v.Web)
                        .ThenInclude(w => w.Flows)
                            .ThenInclude(x => x.Urls)
                                .ThenInclude(y => y.Actions)
                                    .ThenInclude(z => z.Data)
                    .ToList();
        }
        // {
        //     var users =  _context.Users.Select(x => new User
        //     {
        //         Id = x.Id,
        //         VisitCount = x.VisitCount,
        //         DeviceType = x.DeviceType,
        //         Browser = x.Browser,
        //         LastConnection = x.LastConnection,
        //         Location = x.Location,
        //         Website = x.Website

        //     }).ToList();
        //     return users;
        // }
        public User GetUserById(int id)
        {
            return _context.Users
                    .Include(u => u.Location)
                    .Include(v => v.Web)
                        .ThenInclude(w => w.Flows)
                            .ThenInclude(x => x.Urls)
                                .ThenInclude(y => y.Actions)
                                    .ThenInclude(z => z.Data)
                    .FirstOrDefault(u => u.Id == id);

            // return _context.Users
            // .Where(z => z.Id == id)
            // .Include(x => x.Web)
            // .Include(y => y.Location)
            // .FirstOrDefault();

            
            // try{
            //     var user = _context.Users.Where(x => x.Id == id).Select(x => new User
            //     {
            //         Id = x.Id,
            //         VisitCount = x.VisitCount,
            //         DeviceType = x.DeviceType,
            //         Browser = x.Browser,
            //         LastConnection = x.LastConnection,
            //         Location = x.Location,
            //         Website = x.Website
            //     }).FirstOrDefault();
            //     return user;
            // }catch(Exception e){
            //     Console.WriteLine(e.Message);
            //     return null;
            // }   
        }
        public bool AddUser(User user)
        {
            try{
                _context.Users.Add(user);
                _context.SaveChanges();
                return true;
            }catch(Exception e){
                Console.WriteLine(e.Message);
                return false;
            }
        }
        public bool DeleteUser(int id)
        {
            var user = _context.Users
                        .Include(u => u.Location)
                        .Include(v => v.Web)
                            .ThenInclude(w => w.Flows)
                                .ThenInclude(x => x.Urls)
                                    .ThenInclude(y => y.Actions)
                                        .ThenInclude(z => z.Data)
                        .Where(x => x.Id == id)
                        .FirstOrDefault();
            if (user != null)
            {
                _context.Users.Remove(user);
                _context.SaveChanges();
                return true;
            }
            return false;
        }
        public bool UpdateUser(int id, User user)
        {
            var userToUpdate = _context
                                .Users
                                .Where(t => t.Id == id)
                                .Include(u => u.Location)
                                .Include(v => v.Web)
                                    .ThenInclude(w => w.Flows)
                                        .ThenInclude(x => x.Urls)
                                            .ThenInclude(y => y.Actions)
                                                .ThenInclude(z => z.Data)
                                .FirstOrDefault();
            if(userToUpdate != null)
            {
                userToUpdate.DeviceType = user.DeviceType;
                userToUpdate.Browser = user.Browser;
                userToUpdate.LastConnection = user.LastConnection;
                userToUpdate.Location = user.Location;
                userToUpdate.Web = user.Web;
                _context.SaveChanges();
                return true;
            }
            return false;
        }
    }
}