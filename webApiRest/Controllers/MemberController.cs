using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http.Controllers;
using webApiRest.Models;

namespace webApiRest.Controllers
{
    public class MemberController : IHttpController
    {
        public async Task<HttpResponseMessage> ExecuteAsync(HttpControllerContext controllerContext, CancellationToken cancellationToken)
        {
            HttpResponseMessage result = null;
            if (controllerContext.Request.Method == HttpMethod.Get)
            {
                result = HttpGet(controllerContext);

            }

            else if (controllerContext.Request.Method==HttpMethod.Post)
            {

            }

            return result;
        }

        public HttpResponseMessage HttpGet(HttpControllerContext controllerContext)
        {
            HttpResponseMessage rest = null;
            var id = controllerContext.RouteData.Values["id"];

            if(id == null)
            {
                var context = MemberRepository.Get();
                rest = controllerContext.Request.CreateResponse(HttpStatusCode.OK,context);
            }

            else
            {
                int idAsInteger;
                if (!int.TryParse(id.ToString(),out idAsInteger))
                {
                    rest = controllerContext.Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Id degeri numeric olmali");
                }
                else
                {
                    var context = MemberRepository.Get(idAsInteger);
                    rest = controllerContext.Request.CreateResponse(HttpStatusCode.OK, context);
                }
            }

            return rest;
        }


        public async Task<HttpResponseMessage> HttpPost(HttpControllerContext controllerContext, CancellationToken cancellationToken)
        {
            HttpResponseMessage retVal = null;

            string contentString = await controllerContext.Request.Content.ReadAsStringAsync();

            Member postMember = Newtonsoft.Json.JsonConvert.DeserializeObject<Member>(contentString);


            if (MemberRepository.IsExists(postMember.FullName))
            {
                retVal = controllerContext.Request.CreateErrorResponse(HttpStatusCode.BadRequest,"Ayni isimde baska bir kullanici var");
            }

            else
            {
                Member createMember = MemberRepository.Add(postMember.FullName, postMember.Age);
                retVal = controllerContext.Request.CreateResponse(HttpStatusCode.OK,createMember);
            }

            return retVal;
        }


    }
}