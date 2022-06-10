﻿using Microsoft.EntityFrameworkCore;
using SpendingApp.Entities;

namespace SpendingApp
{
    public class Seeder
    {
        private readonly AppDbContext _dbContext;

        public Seeder(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }


        public void Seed()
        {
            if (_dbContext.Database.CanConnect())
            {
                var pendingMigrations = _dbContext.Database.GetPendingMigrations();
                if (pendingMigrations != null && pendingMigrations.Any())
                {
                    _dbContext.Database.Migrate();
                }
                if (!_dbContext.Roles.Any())
                {
                    var roles = GetRoles();
                    _dbContext.Roles.AddRange(roles);
                    _dbContext.SaveChanges();
                }
            }
        }
        private IEnumerable<Role> GetRoles()
        {
            var roles = new List<Role>()
        {
            new Role()
            {
                Name = "User"
            },
            new Role()
            {
                Name = "Admin"
            },
        };
            return roles;
        }
    }
}
