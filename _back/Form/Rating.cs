using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestGuestbook.Form
{
    public class Rating : CJE.Form.FormData
    {
        public Rating(CJE.Form.DataRaw data) : base(data, true) { }

        [CJE.Form.Value("MessageID", typeof(CJE.Form.Values.GuidParser))]
        public Guid MessageID;

        [CJE.Form.Value("Value", typeof(CJE.Form.Values.IntegerParser))]
        public int Value;

        public Data.Rating ToData()
        {
            return new Data.Rating()
            {
                ID = this.MessageID,
                Value = this.Value,
            };
        }
    }
}
