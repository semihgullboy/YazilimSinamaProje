using System;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using Microsoft.AspNetCore.Mvc;
using YazılımSınamaProje.Models;

namespace YazılımSınamaProje.Controllers
{
    public class YouTubeController : Controller
    {

        public IActionResult Index()
        {

            return View();
        }

        // YouTube Data API key
        private const string ApiKey = "AIzaSyATTCLNgFG0fpW6yD-_s5CeWST5oRLqne4";

        // Video ID
        private const string VideoId = "S-sW7Rixvfk";

        public ActionResult GetVideoComments(string kullaniciadi)
        {
            try
            {
                using (var httpClient = new HttpClient())
                {
                    // YouTube Data API endpoint
                    var apiEndpoint = $"https://www.googleapis.com/youtube/v3/commentThreads?part=snippet&videoId={VideoId}&key={ApiKey}";

                    // API'ye GET isteği gönderme
                    var response = httpClient.GetStringAsync(apiEndpoint).Result;

                    // JSON yanıtını işleme
                    var data = JObject.Parse(response);

                    var loggedInUsername = kullaniciadi;
                    Console.WriteLine(loggedInUsername);

                    // Yorumları işleme
                    bool userCommentFound = false; // Kullanıcının yorumu bulundu mu?

                    foreach (var item in data["items"])
                    {
                        // Yorum atan kullanıcının adını alın
                        var username = item["snippet"]["topLevelComment"]["snippet"]["authorDisplayName"].Value<string>();

                        if (username.ToString() == loggedInUsername)
                        {
                            userCommentFound = true;

                            var usermail = User.Identity.Name;
                            using (Context c = new Context())
                            {
                                var user = c.users.SingleOrDefault(x => x.email == usermail);
                                if (user != null)
                                {
                                    user.money += 10;
                                    c.SaveChanges();
                                }
                            }

                            // Eşleşme bulundu, istediğiniz işlemleri gerçekleştirin
                            Console.WriteLine("Giriş yapan kullanıcı yorum yaptı!");
                        }

                        Console.WriteLine($"Yorum Atan Kullanıcı Adı: {username}");
                    }

                    // Kullanıcı yorumu bulunamadıysa
                    if (!userCommentFound)
                    {
                        ViewBag.Comments = "Giriş yapan kullanıcı henüz yorum yapmamış.";
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Hata Oluştu: {ex.Message}");
            }

            return View();
        }


    }
}
