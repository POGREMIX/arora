using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestGuestbook.DB.Mapping
{
    public class RatingMap : ClassMap<Rating>
    {
        public RatingMap()
        {
            Id(x => x.ID).GeneratedBy.GuidComb();//.Assigned();
            Map(x => x.Value);
        }
    }
}
