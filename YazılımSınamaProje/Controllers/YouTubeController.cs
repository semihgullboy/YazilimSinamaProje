using System;
using System.Net.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using YazılımSınamaProje.Models;

namespace YazılımSınamaProje.Controllers
{
    public class YouTubeController : Controller
    {
        // YouTube Data API key
        private const string ApiKey = "Your_API_Key";

        // Video ID
        private const string VideoId = "S-sW7Rixvfk";

        public IActionResult Index()
        {
            return View();
        }

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
                            using (var c = new Context())
                            {
                                var user = c.Users.SingleOrDefault(x => x.Email == usermail);
                                if (user != null)
                                {
                                    user.Money += 10;
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
