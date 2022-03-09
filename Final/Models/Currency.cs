using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Final.Models
{
    public class Currency
    {
        public int Id { get; set; }

        [Required]
        [RegularExpression("[a-zA-Z]+")]
        [Display(Name = "Currency naming")]
        public string Name { get; set; }
    }
}