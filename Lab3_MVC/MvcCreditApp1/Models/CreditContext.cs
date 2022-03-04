using Microsoft.AspNet.Identity.EntityFramework;
using MvcCreditApp1.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace MvcCreditApp1.Models
{
    public class CreditContext : DbContext
    {
        public DbSet<Credit> Credits { get; set; }
        public DbSet<Bid> Bids { get; set; }
    }
}