

using Octokit;
using System.Security.Policy;
using System;
using System.Threading.Tasks;
using System.Windows;
using System.Net.Http.Headers;
using System.IO;
using System.Net.Http;
using System.Text;
using Octokit.Internal;

namespace EpicrisisWord.Core.Helpers;

internal class UpdaterHelper
{
    internal static async Task DowloadLatexAsync()
    {
        //var token = "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";
        //var gitHubClient = new GitHubClient(new Octokit.ProductHeaderValue("my-cool-app"));
        //var tokenAuth = new Credentials(token); // NOTE: not real token
        //gitHubClient.Credentials = tokenAuth;
        //var releases = await gitHubClient.Repository.Release.GetAll("nordpixilus", "MedKartaFull");
        //var latest = releases[0];
        //var release = gitHubClient.Repository.Release.Get("nordpixilus", "MedKartaFull", 1);

        //MessageBox.Show(latest.TagName);

        var url = "https://github.com/nordpixilus/MedKartaFull/releases/download/v1.0/EpicrisisWord.zip";

        using (var client = new System.Net.Http.HttpClient())
        {
            var token = "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";
            //string basicValue = Convert.ToBase64String(Encoding.UTF8.GetBytes("nordpixilus:MedKartaFull"));
            //client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", basicValue);
            //client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Token", token);
            client.DefaultRequestHeaders.Add("User-Agent", "MedKartaFull");
            //client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/octet-stream"));
            //var nurl = Path.Combine(latest.HtmlUrl, ".zip");
            //MessageBox.Show(latest.AssetsUrl);

            using (var stream = await client.GetStreamAsync(url))
            using (var lile = new FileStream("karta.zip", System.IO.FileMode.CreateNew))
                await stream.CopyToAsync(lile);


            //var contents = client.GetByteArrayAsync(latest.Url).Result;
            //System.IO.File.WriteAllBytes("EpicrisisWord.txt", contents);
        }

        //var asset = await client.Repository.Release.UploadAsset(release, assetUpload);
        //var user = await client.User.Current();

    }

    //    using (var client = new HttpClient())
    //            {
    //                ResponseModel responseModel = new ResponseModel();
    //client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", "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");
    ////client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", "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");
    ////client.BaseAddress = new Uri("https://api.github.com/nordpixilus/MedKartaFull");
    //client.DefaultRequestHeaders.Accept.Clear();
    //                client.DefaultRequestHeaders.Add("User-Agent", "MedKartaFull");
    //                //client.DefaultRequestHeaders.UserAgent.Add(new ProductInfoHeaderValue("MedKartaFull", "v1.3"));

    //                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/octet-stream"));
    //                //client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/vnd.github.raw+json"));

    //                //HttpResponseMessage response = await client.GetAsync("https://api.github.com/repos/nordpixilus/MedKartaFull/releases/98561311/assets/EpicrisisWord.zip");
    //                //using var stream = await client.GetAsync(" https://github.com/nordpixilus/MedKartaFull/releases/tag/v1.0");
    //                using (var response = await client.GetAsync("https://api.github.com/repos/nordpixilus/MedKartaFull/releases/98561311/.zip"))
    //                using (var stream = await response.Content.ReadAsStreamAsync())
    //                using (var file = File.OpenWrite("fileName.txt"))
    //                {
    //                    stream.CopyTo(file);
    //                }



    //                //using var fileStream = new FileStream("300.txt", FileMode.CreateNew);
    //                //stream.Content.CopyToAsync(fileStream);


    //                //using (var response = await client.GetAsync(address))
    //                //using (var stream = await response.Content.ReadAsStreamAsync())
    //                //using (var file = File.OpenWrite("fileName.zip"))
    //                //{
    //                //    stream.CopyTo(file);
    //                //}
    //                //MessageBox.Show(response.Content.ReadAsStringAsync().ToString());

    //                //using (HttpContent content = response.Content)
    //                //{
    //                //    string responseBody = await response.Content.ReadAsStringAsync();
    //                //    var articles = JsonConvert.DeserializeObject<List<Employee>>(responseBody);
    //                //    MessageBox.Show(response.ToString());
    //                //// https://github.com/nordpixilus/MedKartaFull/releases/tag/v1.0
    //                // https://api.github.com/repos/nordpixilus/MedKartaFull/releases/98561311
    //                //}



    //            }
}
