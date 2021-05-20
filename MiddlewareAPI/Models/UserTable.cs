using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace MiddlewareAPI.Models
{
    [Table("UserTable")]
    public partial class UserTable
    {
        public UserTable()
        {
            RefreshTokenTables = new HashSet<RefreshTokenTable>();
            UserPlatformTables = new HashSet<UserPlatformTable>();
        }

        [Key]
        public int UserId { get; set; }
        [StringLength(50)]
        public string FirstName { get; set; }
        [StringLength(50)]
        public string LastName { get; set; }
        [Required]
        [StringLength(50)]
        public string Email { get; set; }
        [Required]
        [StringLength(50)]
        public string Password { get; set; }

        [InverseProperty(nameof(RefreshTokenTable.User))]
        public virtual ICollection<RefreshTokenTable> RefreshTokenTables { get; set; }
        [InverseProperty(nameof(UserPlatformTable.User))]
        public virtual ICollection<UserPlatformTable> UserPlatformTables { get; set; }
    }
}
