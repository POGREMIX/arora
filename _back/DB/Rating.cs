using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestGuestbook.DB
{
    public class Rating
    {
        public virtual Guid ID { get; set; }
        public virtual int Value { get; set; }
        public Rating() { }
        public Rating(Data.Rating source)
        {
            this.ID = source.ID;
            this.Value = source.Value;
        }
        public virtual Data.Rating toData()
        {
            return new Data.Rating()
            {
                ID = this.ID,
                Value = this.Value
            };
        }
    }
}
