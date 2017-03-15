using Nop.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Data.Mapping
{
    public partial class Nop_DemoMap : NopEntityTypeConfiguration<Nop_Demo>
    {
        public Nop_DemoMap()
        {
            //this.ToTable("NewsComment");
            //this.HasKey(pr => pr.Id);

            //this.HasRequired(nc => nc.NewsItem)
            //    .WithMany(n => n.NewsComments)
            //    .HasForeignKey(nc => nc.NewsItemId);

            //this.HasRequired(cc => cc.Customer)
            //    .WithMany()
            //    .HasForeignKey(cc => cc.CustomerId);
            this.ToTable("Nop_Demo");
            this.HasKey(pr=>pr.Id);
            this.Property(mt => mt.describe).IsRequired().HasMaxLength(200);
            //this.HasRequired(nd => nd.describe)
            //    .WithMany();
        }
    }
}
