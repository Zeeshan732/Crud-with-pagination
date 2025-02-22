﻿using Microsoft.EntityFrameworkCore;

namespace Crud_with_pagination.Models
{
    public class UserContext : DbContext
    {
        public UserContext(DbContextOptions<UserContext> options) : base(options)
        {
            
        }


        public DbSet<User> Users { get; set; }
    }

}
