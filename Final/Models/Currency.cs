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
        public string Name { get; set; }
    }
}