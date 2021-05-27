using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestGuestbook.Form
{
    public class Comment : CJE.Form.FormData
    {
        public Comment(CJE.Form.DataRaw data) : base(data, true) { }

        [CJE.Form.Value("ID", typeof(CJE.Form.Values.GuidParser))]
        public Guid ID;

        [CJE.Form.Value("MessageID", typeof(CJE.Form.Values.GuidParser))]
        public Guid MessageID;

        [CJE.Form.Value("Author", typeof(CJE.Form.Values.StringParser))]
        public string Author;

        [CJE.Form.Value("Content", typeof(CJE.Form.Values.StringParser))]
        public string Content;

        [CJE.Form.Value("Created", typeof(CJE.Form.Values.DateTimeParser))]
        public DateTime Created;

        public Data.Comment ToData()
        {
            return new Data.Comment()
            {
                ID = this.ID,
                Author = this.Author,
                Content = this.Content,
                Created = this.Created
            };
        }
    }
}
