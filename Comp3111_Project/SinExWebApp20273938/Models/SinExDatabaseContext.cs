﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace SinExWebApp20273938.Models
{
    public class SinExDatabaseContext : DbContext
    {
        // You can add custom code to this file. Changes will not be overwritten.
        // 
        // If you want Entity Framework to drop and regenerate your database
        // automatically whenever you change your model schema, please use data migrations.
        // For more information refer to the documentation:
        // http://msdn.microsoft.com/en-us/data/jj591621.aspx
    
        public SinExDatabaseContext() : base("name=SinExDatabaseContext")
        {
        }

        public System.Data.Entity.DbSet<SinExWebApp20273938.Models.PackageType> PackageTypes { get; set; }

        public System.Data.Entity.DbSet<SinExWebApp20273938.Models.ServiceType> ServiceTypes { get; set; }

        public System.Data.Entity.DbSet<SinExWebApp20273938.Models.ServicePackageFee> ServicePackageFees { get; set; }

        public System.Data.Entity.DbSet<SinExWebApp20273938.Models.Currency> Currencies { get; set; }

        public System.Data.Entity.DbSet<SinExWebApp20273938.Models.Destination> Destinations { get; set; }

        public System.Data.Entity.DbSet<SinExWebApp20273938.Models.PackageTypeSize> PackageTypeSizes { get; set; }

        public System.Data.Entity.DbSet<SinExWebApp20273938.Models.Shipment> Shipments { get; set; }

        public System.Data.Entity.DbSet<SinExWebApp20273938.Models.ShippingAccount> ShippingAccounts { get; set; }

        public System.Data.Entity.DbSet<SinExWebApp20273938.Models.Package> Packages { get; set; }

        public System.Data.Entity.DbSet<SinExWebApp20273938.Models.PickupInforation> PickupInforations { get; set; }
    }
}
