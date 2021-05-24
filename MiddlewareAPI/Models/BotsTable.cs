using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace MiddlewareAPI.Models
{
    [Table("BotsTable")]
    public partial class BotsTable
    {
        [Key]
        public int BotId { get; set; }
        [Required]
        [StringLength(100)]
        public string BotPlatformId { get; set; }
        [Required]
        [StringLength(100)]
        public string BotName { get; set; }
        [StringLength(100)]
        public string BotDescription { get; set; }
        [Required]
        [StringLength(50)]
        public string PlatformName { get; set; }

        [ForeignKey(nameof(BotId))]
        [InverseProperty(nameof(PlatformBotTable.BotsTable))]
        public virtual PlatformBotTable Bot { get; set; }
        [ForeignKey(nameof(BotId))]
        [InverseProperty(nameof(TriggerTable.BotsTable))]
        public virtual TriggerTable BotNavigation { get; set; }
    }
}
