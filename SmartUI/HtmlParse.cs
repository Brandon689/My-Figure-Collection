using AngleSharp.Html.Dom;
using AngleSharp.Html.Parser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace SmartUI
{
    public class HtmlParse
    {
        private HttpClient client = new();

        public async Task<MFCItem> GetItem(IHtmlDocument document)
        {
            var jon = ParseRest(document);
            return jon;
        }
        public async Task<List<string>> ImgSlides(IHtmlDocument document)
        {
            var list = await MangoImgSlideshow(document);
            return list;
        }

        public async Task<IHtmlDocument> Pars(string url)
        {
            var a = await Str(url);
            var parser = new HtmlParser();
            var document = await parser.ParseDocumentAsync(a);
            return document;
        }

        public async Task<string> Str(string url)
        {
            var str = await client.GetStringAsync(url);
            return str;
        }

        private async Task<List<string>> MangoImgSlideshow(IHtmlDocument document)
        {
            var g = document.QuerySelector(".item-picture");

            var c = g.QuerySelectorAll(".more");
            //int z = 1;
            List<string> ret = new();
            foreach (var item in c)
            {
                var x = item.GetAttribute("style");
                if (x.Contains(".gif")) continue;
                string ola = ".jpeg";
                if (x.Contains(ola) == false)
                {
                    ola = ".png";
                }
                if (x.Contains(ola) == false)
                {
                    ola = ".jpg";
                }
                var s = x.IndexOf("url(") + 4;
                var vv = x.IndexOf($"{ola})");
                var s3 = vv - s + ola.Length;
                if (s3 < s)
                {
                    ;
                }
                var cc = x.Substring(s, s3);
                var so = cc.Replace("/thumbnails", "");
                //;
                //var dir = moreimg + id;
                //Directory.CreateDirectory(dir);
                //var file = dir + "\\" + z + ola;
                //await service.HttpGetForLargeFileInRightWay(so, file);
                //++z;
                ret.Add(so);
            }
            return ret;
        }


        private MFCItem ParseRest(IHtmlDocument document)
        {
            //var pic = document.QuerySelector(".item-picture img");
            var idbar = document.QuerySelector("#ariadne").TextContent;
            var crumbs = idbar.Split("›");
            var id = crumbs.Last().Replace("Item #", "").Trim();
            //Console.WriteLine(id);
            var table = document.QuerySelector(".data .form");
            MFCItem t = new();
            t.MFCId = int.Parse(id);

            var community = document.QuerySelector(".data_2 .form");
            var communrows = community.QuerySelectorAll(".form-field");
            for (int i = 0; i < communrows.Length; i++)
            {
                var sk = communrows[i].TextContent;
                //Console.WriteLine(communrows[i].TextContent);
                if (sk.Contains("Owned by"))
                {
                    t.OwnedBy = int.Parse(sk.Replace("Owned by", "").Replace("users", "").Replace("user", "").Replace(",", "").Trim());
                }
                else if (sk.Contains("Wished by"))
                {
                    t.WishedBy = int.Parse(sk.Replace("Wished by", "").Replace("users", "").Replace("user", "").Replace(",", "").Trim());
                }
                else if (sk.Contains("Sold by"))
                {
                    t.SoldBy = int.Parse(sk.Replace("Sold by", "").Replace("users", "").Replace("user", "").Trim());
                }
                else if (sk.Contains("Ordered by"))
                {
                    t.OrderedBy = int.Parse(sk.Replace("Ordered by", "").Replace("users", "").Replace("user", "").Replace(",", "").Trim());
                }
                else if (sk.Contains("Reviewed by"))
                {
                    t.ReviewedBy = int.Parse(sk.Replace("Reviewed by", "").Replace("users", "").Replace("user", "").Trim());
                }
                else if (sk.Contains("Hunted by"))
                {
                    t.HuntedBy = int.Parse(sk.Replace("Hunted by", "").Replace("users", "").Replace("user", "").Trim());
                }
                else if (sk.Contains("Mentioned in"))
                {
                    var x = sk.Replace("Mentioned in", "").Replace("articles", "").Replace("article", "").Replace(",", "").Trim();
                    ;
                    t.MentionedIn = int.Parse(x);
                }
                else if (sk.Contains("Listed in"))
                {
                    t.ListedBy = int.Parse(sk.Replace("Listed in", "").Replace("lists", "").Replace("list", "").Replace(",", "").Trim());
                }
                else if (sk.Contains("Top 100"))
                {
                    sk = sk.Replace("Top 100", "");

                    if (sk.Contains("owned"))
                    {
                        var section = sk.Substring(0, sk.IndexOf(" Most owned"));
                        var zz = section.Replace("/100", "");
                        t.Top100Owned = int.Parse(zz); //int.Parse(sk.Replace("Top 100", "").Replace("/100", "").Replace("Most owned", "").Replace(",", "").Trim());
                    }
                    else if (sk.Contains("ordered"))
                    {
                        var section = sk.Substring(0, sk.IndexOf(" Most ordered"));
                        var zz = section.Replace("/100", "");
                        var pek = sk.Replace("Top 100", "").Replace("/100", "").Replace("Most wished", "").Replace("Most ordered", "").Replace(",", "").Trim();
                        ;
                        t.Top100Ordered = int.Parse(zz); //int.Parse(pek);
                    }
                    else if (sk.Contains("wished"))
                    {
                        var section = sk.Substring(0, sk.IndexOf(" Most wished"));
                        var zz = section.Replace("/100", "");
                        t.Top100Wished = int.Parse(zz); //int.Parse(sk.Replace("Top 100", "").Replace("/100", "").Replace("Most wished", "").Replace(",", "").Trim());
                    }
                }
                else if (sk.Contains("Average rating"))
                {
                    var rating = sk.Split('/');
                    var r = rating[0].Replace("Average rating", "");
                    t.AverageRating = Convert.ToDouble(r);

                    var ind = rating[1].IndexOf("rated ") + "rated ".Length;
                    var ss = rating[1];
                    var ssa = rating[1].Substring(ind).Replace(",", "");
                    var tim = ssa.Replace("times", "").Replace("time", "").Trim();
                    ;
                    t.TimesRated = int.Parse(tim);
                }
            }
            var cells = table.QuerySelectorAll(".form-field");

            var stats = document.QuerySelector(".object-stats");

            var kk = stats.TextContent.Split("•");
            ;
            t.Hits = int.Parse(kk[1].Replace("hits", "").Replace("hit", "").Replace(",", "").Trim());
            t.Comments = int.Parse(kk[2].Replace("comments", "").Replace("comment", "").Replace(",", "").Trim());
            var zzrot = kk[3].Replace("likes", "").Replace("like", "").Replace(",", "").Trim();
            t.Likes = int.Parse(zzrot);

            var title = document.QuerySelector(".h1-headline-value .headline");
            t.Title = title.TextContent;
            //t.ObjectStats = stats.TextContent.Replace("\u2022", "").Replace("Japanese", "").Trim();
            foreach (var item in cells)
            {
                var inner = item.QuerySelector(".form-label");
                var inner2 = item.QuerySelector(".form-input");
                if (inner != null)
                {
                    var label = inner.TextContent.Trim();
                    var field = inner2.TextContent.Trim();

                    var texj = "";
                    var span = inner2.QuerySelectorAll("span");
                    foreach (var sp in span)
                    {
                        var jp = sp.GetAttribute("switch");
                        texj += jp + " ";
                    }
                    texj.Trim();

                    switch (label)
                    {
                        case "Category":
                            t.Category = field;
                            t.JCategory = texj;
                            break;
                        case "Classification":
                            t.Classification = field;
                            t.JCategory = texj;
                            break;
                        case "Company":
                            t.Company = field;
                            t.JCompany = texj;
                            break;
                        case "Companies":
                            t.Company = field;
                            t.JCompany = texj;
                            break;
                        case "Character":
                            t.Character = field;
                            t.JCharacter = texj;
                            break;
                        case "Characters":
                            t.Character = field;
                            t.JCharacter = texj;
                            break;
                        case "Artists":
                            t.Artists = field;
                            t.JArtists = texj;
                            break;
                        case "Artist":
                            t.Artists = field;
                            t.JArtists = texj;
                            break;
                        case "Materials":
                            t.Materials = field;
                            break;
                        case "Price":
                            var itemprice = inner2.QuerySelector(".item-price");
                            t.Price = Convert.ToDecimal(itemprice.TextContent.Replace("¥", "").Replace(",", ""));
                            break;
                        case "Scale & Dimensions":
                            t.ScaleDimensions = field;
                            break;
                        case "Release date":
                            var date = inner2.QuerySelector(".time");
                            t.ReleaseDate = date.TextContent;
                            break;
                        case "Release dates":
                            var dates = inner2.QuerySelectorAll(".time");
                            t.ReleaseDate = String.Join(" . ", dates);
                            break;
                        case "JAN":
                            t.JAN = long.Parse(field);
                            break;
                        case "Version":
                            t.Version = field;
                            break;
                        case "Origin":
                            t.Origin = field;
                            break;
                        default:
                            break;
                    }

                }
            }
            return t;
        }

        //public async Task<string> HttpGet(string url)
        //{
        //    try
        //    {
        //        HttpResponseMessage respons = await client.GetAsync(new Uri(url));
        //        respons.EnsureSuccessStatusCode();
        //        return await respons.Content.ReadAsStringAsync();
        //    }
        //    catch (HttpRequestException e)
        //    {
        //        if (e.StatusCode == System.Net.HttpStatusCode.ServiceUnavailable)
        //        {
        //            Console.WriteLine("service unavailable");
        //        }
        //        return null;
        //    }
        //}
    }
}
