using CJE.Component;
using FluentNHibernate.Conventions;
using NHibernate.Criterion;
using NHibernate.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestGuestbook.DB
{
    public static class CommentController
    {
        public static Data.Comment LoadComment(IDBSession dbs, Guid id)
        {
            DB.Comment dbMessage = dbs.Session.CreateCriteria<DB.Comment>().Add(Restrictions.Eq("ID", id)).UniqueResult<DB.Comment>();//.List<DB.Comment>();
            return dbMessage.ToData(false);
        }

        public static Data.Comment SaveMessage(IDBSession dbs, Data.Comment comment, Guid messageId)
        {
            using (NHibernate.ITransaction transaction = dbs.Session.BeginTransaction())
            {
                DB.Comment dbComment = null;
                DB.Message dbMessage = null;
                if (comment.ID != Guid.Empty) dbComment = dbs.Session.Get<DB.Comment>(comment.ID);
                 
                if (dbComment == null)
                {
                    //добавить проверку на наличие сообщений 
                    //добавить проверку на наличие дубля
                    dbComment = new DB.Comment(comment);
                    dbComment.Created = DateTime.UtcNow;
                    dbs.Session.Save(dbComment);

                    dbMessage = dbs.Session.Get<DB.Message>(messageId);
                    dbMessage.Comments.Add(dbComment);
                                        
                    dbs.Session.SaveOrUpdate(dbMessage);
                }
                else
                {
                    dbComment.Author = comment.Author;
                    dbComment.Content = comment.Content;
                    dbs.Session.Update(dbComment);
                }

                transaction.Commit();
                comment = dbComment.ToData(false);
            }
            return comment;
        }

        public static Data.Comment DeleteComment(IDBSession dbs, Guid id)
        {
            DB.Comment dbComment;
            using (NHibernate.ITransaction transaction = dbs.Session.BeginTransaction())
            {
                dbComment = dbs.Session.Get<DB.Comment>(id);
                if (dbComment == null) return null;
                dbs.Session.Delete(dbComment);
                transaction.Commit();
            }
            return dbComment.ToData(false);
        }
    }
}
