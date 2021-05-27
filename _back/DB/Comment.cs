using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestGuestbook.DB
{
    public class Comment
    {
        public virtual Guid ID { get; set; }
        public virtual string Author { get; set; }
        public virtual string Content { get; set; }
        public virtual DateTime Created { get; set; }


        public Comment() { }
        public Comment(Data.Comment source)
        {
            this.ID = source.ID;
            this.Author = source.Author;
            this.Content = source.Content;
            this.Created = source.Created;
        }
        public virtual Data.Comment ToData(bool flag)
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