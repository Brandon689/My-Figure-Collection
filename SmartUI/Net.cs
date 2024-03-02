using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

namespace SmartUI
{
    public class Net
    {
        private HttpClient client = new();

        public async Task<string> Image(string pic, string path)
        {
            //string x = $"{Glob.Pic}{mfcid}.jpg";

            if (!File.Exists(pic))
            {
                await HttpGetForLargeFileInRightWay(pic, path);
            }
            return path;
        }
        //public async Task<string> Image(int mfcid)
        //{
        //    string fo = $"https://static.myfigurecollection.net/upload/items/large/{mfcid}.jpg";
        //    string x = $"{Glob.Pic}{mfcid}.jpg";
        //    if (!File.Exists(x))
        //    {
        //        await HttpGetForLargeFileInRightWay(fo, x);
        //    }
        //    return x;
        //}

        public async Task HttpGetForLargeFileInRightWay(string uri, string path)
        {
            using HttpResponseMessage response = await client.GetAsync(uri, HttpCompletionOption.ResponseHeadersRead);
            if (response.StatusCode != System.Net.HttpStatusCode.NotFound)
            {
                using Stream streamToReadFrom = await response.Content.ReadAsStreamAsync();
                string fileToWriteTo = path;
                using Stream streamToWriteTo = File.Open(fileToWriteTo, FileMode.Create);
                await streamToReadFrom.CopyToAsync(streamToWriteTo);
            }
            else
            {
                uri = uri.Replace("large", "big");
                using HttpResponseMessage response2 = await client.GetAsync(uri, HttpCompletionOption.ResponseHeadersRead);
                using Stream streamToReadFrom = await response2.Content.ReadAsStreamAsync();
                string fileToWriteTo = path;
                using Stream streamToWriteTo = File.Open(fileToWriteTo, FileMode.Create);
                await streamToReadFrom.CopyToAsync(streamToWriteTo);
            }
        }
    }
}
