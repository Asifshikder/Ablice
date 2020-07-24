using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RugerRumble.Models
{
    public class PreOrder
    {
        [Key]
        public int  PreorderID { get; set; }
        public string UserID { get; set; }
        public int ProductID { get; set; }
        public DateTime? PreorderDate { get; set; }
    }
}
