using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace MiddlewareAPI.Models
{
    [Table("TriggerTable")]
    public partial class TriggerTable
    {
        [Key]
        public int BotId { get; set; }
        [Required]
        [StringLength(50)]
        public string IsRunning { get; set; }

        [InverseProperty("BotNavigation")]
        public virtual BotsTable BotsTable { get; set; }
    }
}
