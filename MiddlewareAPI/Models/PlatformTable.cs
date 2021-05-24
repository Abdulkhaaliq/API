using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace MiddlewareAPI.Models
{
    [Table("PlatformTable")]
    public partial class PlatformTable
    {
        public PlatformTable()
        {
            PlatformBotTables = new HashSet<PlatformBotTable>();
        }

        [Key]
        public int PlatformId { get; set; }
        [Required]
        [StringLength(50)]
        public string PlatformName { get; set; }
        [Required]
        [StringLength(50)]
        public string PlatformEmail { get; set; }
        [Required]
        [StringLength(50)]
        public string PlatformPassword { get; set; }

        [ForeignKey(nameof(PlatformId))]
        [InverseProperty(nameof(UserPlatformTable.PlatformTable))]
        public virtual UserPlatformTable Platform { get; set; }
        [InverseProperty(nameof(PlatformBotTable.Platform))]
        public virtual ICollection<PlatformBotTable> PlatformBotTables { get; set; }
    }
}
