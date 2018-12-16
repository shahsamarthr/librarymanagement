using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Facebook;
using Newtonsoft.Json;
using System.Web.Security;

namespace LibraryManagementProject2.Controllers
{
    public class FacebookController : Controller
    {
        // GET: Facebook
        public ActionResult Index()
        {
            return View();
        }

        private Uri RedirectUri
        {
            get
            {
                var uriBuilder = new UriBuilder(Request.Url);
                uriBuilder.Query = null;
                uriBuilder.Fragment = null;
                uriBuilder.Path = Url.Action("facebookCallback");
                return (uriBuilder.Uri);
            }
        }
        [AllowAnonymous]
        public ActionResult Facbook()
        {
            var fb = new FacebookClient();
            var loginUrl = fb.GetLoginUrl(new
            {
                client_id= "309141016482535",
                client_secret = "8109fb16289c1bd065647988ed64860a",
                redirect_uri=RedirectUri.AbsoluteUri,
                response_type="code",
                scope="email"
            });
            return Redirect(loginUrl.AbsoluteUri);
        }

        public ActionResult FacbookCallback(string code)
        {
            var fb = new FacebookClient();
            dynamic result = fb.Post("oauth/access_token", new
            {
                client_id = "309141016482535",
                client_secret = "8109fb16289c1bd065647988ed64860a",
                redirect_uri = RedirectUri.AbsoluteUri,
                code = code
            });
            var accessToken = result.access_token;
            Session["AccessToken"] = accessToken;
            fb.AccessToken = accessToken;
            dynamic me = fb.Get("me?fields=link,first_name,currency,last_name,email,gender,locale,timezone,verified,picture,age_range");
            string email = me.email;
            string lastname = me.last;
            string picture = me.picture.data.url;
            FormsAuthentication.SetAuthCookie(email,false);
            return (RedirectToAction("Index","Facebook"));
        }

    }
}