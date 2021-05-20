using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace MiddlewareAPI.Models
{
    [Table("RefreshTokenTable")]
    public partial class RefreshTokenTable
    {
        [Key]
        public int RefreshTokenId { get; set; }
        [StringLength(50)]
        public string Token { get; set; }
        [StringLength(50)]
        public string JwtId { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreationDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? ExpiryDate { get; set; }
        [StringLength(50)]
        public bool Used { get; set; }
        public int UserId { get; set; }

        [ForeignKey(nameof(UserId))]
        [InverseProperty(nameof(UserTable.RefreshTokenTables))]
        public virtual UserTable User { get; set; }
    }
}
