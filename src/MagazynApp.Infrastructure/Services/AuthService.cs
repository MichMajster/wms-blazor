using MagazynApp.Domain.Entities;
using MagazynApp.Infrastructure.Data;
using MagazynApp.Infrastructure.Migrations;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagazynApp.Infrastructure.Services
{
    public class AuthService
    {
        private readonly IDbContextFactory<AppDbContext> _contextFactory;
        private readonly PasswordHasher<User> _passwordHasher;

        public AuthService(IDbContextFactory<AppDbContext> contextFactory)
        {
            _contextFactory = contextFactory;
            _passwordHasher = new PasswordHasher<User>();
        }
        public async Task<bool> RegisterAsync(string username, string password, string role = "User")
        {
            using var context = await _contextFactory.CreateDbContextAsync();
            if (await context.Users.AnyAsync(u => u.Username == username))
            {
                return false; // User already exists
            }
            var user = new User
            {
                Username = username,
                Role = role
            };
            user.PasswordH = _passwordHasher.HashPassword(user, password);
            context.Users.Add(user);
            await context.SaveChangesAsync();
            return true;
        }
        public async Task<bool> EditAsync(string username, string password, string role = "User", string? NEWusername = "")
        {
            var user = await GetUserAsync(username);
            if (user == null)
            {
                return false;
            }

            using var context = await _contextFactory.CreateDbContextAsync();
            //if (!(await context.Users.AnyAsync(u => u.Username == NEWusername)))
            if (GetUserAsync(NEWusername) != null)
            {
                if ((NEWusername != user.Username) && !(string.IsNullOrWhiteSpace(NEWusername)))
                {
                    user.Username = NEWusername;
                }
            }
            if ((password != null) || !(string.IsNullOrWhiteSpace(password)))
            {
                user.PasswordH = _passwordHasher.HashPassword(user, password);
            }

            user.Role = role;
            context.Users.Update(user);
            await context.SaveChangesAsync();
            return true;
        }
        public async Task<bool> DeleteAsync(int id)
        {
            var user = await GetUserIdAsync(id);
            if (user == null)
            {
                return false;
            }

            using var context = await _contextFactory.CreateDbContextAsync();
            context.Users.Remove(user);
            await context.SaveChangesAsync();
            return true;
        }
        public async Task<User?> ValidateUserAsync(string username, string password)
        {
            using var context = await _contextFactory.CreateDbContextAsync();
            var user = await context.Users.FirstOrDefaultAsync(u => u.Username == username);

            if (user == null)
                return null;

            // weryfikacja wprowadzonego hasła z hashem z bazy
            var result = _passwordHasher.VerifyHashedPassword(user, user.PasswordH, password);

            return result == PasswordVerificationResult.Success ? user : null;
        }
        public async Task<List<User>> GetAllUsersAsync()
        {
            using var context = await _contextFactory.CreateDbContextAsync();
            return await context.Users.AsNoTracking().ToListAsync();
        }
        public async Task<User?> GetUserAsync(string nazwa)
        {
            using var context = await _contextFactory.CreateDbContextAsync();
            return await context.Users
                .AsNoTracking()
                .FirstOrDefaultAsync(p => p.Username == nazwa);
        }
        public async Task<User?> GetUserIdAsync(int id)
        {
            using var context = await _contextFactory.CreateDbContextAsync();
            return await context.Users
                .AsNoTracking()
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        //public async Task<User> DoNameExists(string nazwa)
        //{
        //    using var context = await _contextFactory.CreateDbContextAsync();
        //    return await context.Users
        //        .Where(p => p.Username == nazwa)
        //        .Select(p => p.Id)
        //        .FirstOrDefaultAsync();
        //}
    }
}
