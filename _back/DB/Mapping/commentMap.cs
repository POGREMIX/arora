using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace TestGuestbook.DB.Mapping
{
    public class CommentMap : ClassMap<Comment>
    {
        public CommentMap()
        {
            Id(x => x.ID).GeneratedBy.GuidComb();//.Assigned();
            Map(x => x.Author);
            Map(x => x.Content);
            Map(x => x.Created);//если упало, значит Created="00000000-0000-0000-0000-000000000000"
        }
    }
}
