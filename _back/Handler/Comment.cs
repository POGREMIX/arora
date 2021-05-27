using CJE.Http.RequestAnswer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestGuestbook.DB.Mapping;

namespace TestGuestbook.Handler
{
    public class Comment : CJE.Http.RequestHandlers.RequestHandlerBase
    {
        public Comment(CJE.Http.HttpServer server, System.Net.HttpListenerContext context, CJE.Http.RequestHandlerData data) : base(server, context, data) { }

        public override IAnswer HandlePOST()
        {
            Form.Comment inputData = new Form.Comment(Data.Post.Input);
            Data.Comment inputComment = inputData.ToData();
            var messageId = inputData.MessageID;

            Data.Comment comment = DB.CommentController.SaveMessage(Server.DBSession, inputComment, messageId);

            return new CJE.Http.RequestAnswer.Json(comment);
        }

        public override IAnswer HandleGET()
        {
            Guid id = Data.Get.GetGuid("id");

            Data.Comment message = DB.CommentController.LoadComment(Server.DBSession, id);
            return new CJE.Http.RequestAnswer.Data(message);
        }

        public override IAnswer HandleDELETE()
        {
            Guid id = Data.Get.GetGuid("id");

            Data.Comment comment = DB.CommentController.DeleteComment(Server.DBSession, id);

            return new CJE.Http.RequestAnswer.Json(comment);
        }
    }
}
