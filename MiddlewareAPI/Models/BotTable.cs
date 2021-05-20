using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace MiddlewareAPI.Models
{
    [Table("BotTable")]
    public partial class BotTable
    {
        public BotTable()
        {
            PlatformBotTables = new HashSet<PlatformBotTable>();
        }

        [Key]
        public int BotId { get; set; }
        [Required]
        [StringLength(100)]
        public string BotPlatformId { get; set; }
        [Required]
        [StringLength(50)]
        public string BotName { get; set; }
        [Required]
        [StringLength(100)]
        public string BotDescription { get; set; }

        [InverseProperty(nameof(PlatformBotTable.Bot))]
        public virtual ICollection<PlatformBotTable> PlatformBotTables { get; set; }
    }
}
