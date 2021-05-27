using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestGuestbook.Data
{
    public class Comment : CJE.ISerializable
    {
        public Guid ID { get; set; }
        public string Author { get; set; }
        public string Content { get; set; }
        public DateTime Created { get; set; }
    }
}
