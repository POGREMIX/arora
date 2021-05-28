using CJE.Component;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestGuestbook.DB
{
    public static class MessageController
    {
        public static List<Data.Message> LoadMessageList(IDBSession dbs)
        {
            IList<DB.Message> dbMessages = dbs.Session.QueryOver<DB.Message>().Fetch(x => x.Comments).Eager.TransformUsing(NHibernate.Transform.Transformers.DistinctRootEntity).List<DB.Message>();
            dbMessages = dbs.Session.QueryOver<DB.Message>().Fetch(x => x.Ratings).Eager.TransformUsing(NHibernate.Transform.Transformers.DistinctRootEntity).List<DB.Message>();

            List<Data.Message> messages = dbMessages.Select(x => x.ToData(true, true)).ToList();
            return messages;
        }

        public static Data.Message LoadMessage(IDBSession dbs, Guid id)
        {
            DB.Message dbMessage = dbs.Session.QueryOver<DB.Message>().Fetch(x => x.Comments).Eager.Where(x => x.ID == id).SingleOrDefault<DB.Message>();
            if(dbMessage == null)
            {
                throw new Exception("Сообщение не найдено по полю id");
            }
            Data.Message message = dbMessage?.ToData(true, true);
            return message;
        }

        public static Data.Message SaveMessage(IDBSession dbs, Data.Message message)
        {
            using (NHibernate.ITransaction transaction = dbs.Session.BeginTransaction())
            {
                DB.Message dbMessage = null;
                if (message.ID != Guid.Empty) dbMessage = dbs.Session.Get<DB.Message>(message.ID);
                if (dbMessage == null)
                {
                    dbMessage = new DB.Message(message);
                    dbMessage.Created = DateTime.UtcNow;
                }
                else
                {
                    dbMessage.Author = message.Author;
                    dbMessage.Title = message.Title;
                    dbMessage.Content = message.Content;
                }

                dbs.Session.SaveOrUpdate(dbMessage);
                transaction.Commit();
                message = dbMessage.ToData(true, true);
            }
            return message;
        }

        public static Data.Message DeleteMessage(IDBSession dbs, Guid id)
        {
            DB.Message dbMessage;
            using (NHibernate.ITransaction transaction = dbs.Session.BeginTransaction())
            {
                dbMessage = dbs.Session.Get<DB.Message>(id);
                if (dbMessage == null) return null;
                dbs.Session.Delete(dbMessage);
                transaction.Commit();
            }
            return dbMessage.ToData(true, true);
        }

    }
}
