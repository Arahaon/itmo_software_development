using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Final.Data
{
    public class AppDbInit : DropCreateDatabaseIfModelChanges<AppDbContext>
    {
        protected override void Seed(AppDbContext context)
        {
            context.Currencies.Add(new Final.Models.Currency { Name = "RUB" });
            context.Currencies.Add(new Final.Models.Currency { Name = "USD" }); 
            context.Currencies.Add(new Final.Models.Currency { Name = "EUR" });
            base.Seed(context);
        }
    }
}