using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestGuestbook.Data
{
    public class Rating : CJE.ISerializable
    {
        public Guid ID { get; set; }
        public int Value { get; set; }
    }
}
