using CJE.Component;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestGuestbook.DB
{
    public static class RatingController
    {
        public static Data.Rating UpdateRating(IDBSession dbs, Data.Rating rating)
        {
            using (NHibernate.ITransaction transaction = dbs.Session.BeginTransaction())
            {
                DB.Message dbMessage = null;
                DB.Rating dbRating = null;
                if (rating.ID != Guid.Empty) dbMessage = dbs.Session.Get<DB.Message>(rating.ID);
                if (dbMessage == null)
                {
                    throw new Exception("Нет существующего комментария");
                }
                else
                {
                    dbRating = new DB.Rating(rating);
                    dbMessage.Ratings.Add(dbRating);
                }

                dbs.Session.Save(dbRating);
                dbs.Session.SaveOrUpdate(dbMessage);
                transaction.Commit();
                rating = dbRating.toData();
            }

            return rating;
        }
    }
}
