﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Skypoint.Domain.Entities
{
    public class User
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Email { get; set; } = default!;
        public string PasswordHash { get; set; } = default!;
        public string UserName { get; set; } = default!;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public ICollection<UserFollow> Followers { get; set; } = new List<UserFollow>(); // Users who follow me
        public ICollection<UserFollow> Following { get; set; } = new List<UserFollow>(); // Users I follow

        public ICollection<Post> Posts { get; set; } = new List<Post>();
        public ICollection<Vote> Votes { get; set; } = new List<Vote>();
    }
}
