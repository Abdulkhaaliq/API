using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace MiddlewareAPI.Models
{
    [Table("UserPlatformTable")]
    public partial class UserPlatformTable
    {
        [Key]
        public int PlatformId { get; set; }
        public int UserId { get; set; }

        [ForeignKey(nameof(UserId))]
        [InverseProperty(nameof(UserTable.UserPlatformTables))]
        public virtual UserTable User { get; set; }
        [InverseProperty("Platform")]
        public virtual PlatformTable PlatformTable { get; set; }
    }
}
