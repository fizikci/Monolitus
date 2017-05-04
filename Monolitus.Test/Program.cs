using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Monolitus.Test
{
    class Program
    {
        static void Main(string[] args)
        {
            OGMeta meta = HtmlTool.FetchOG("https://www.amazon.com/dp/B014W1XAT0/ref=strm_sims_201_nad_undefined_undefined");
        }
    }

    public class OGVideo
    {
        public string Content { set; get; }
        public string Type { set; get; }
        public string Width { set; get; }
        public string Height { set; get; }
        public string Tag { set; get; }
        public string SecureUrl { set; get; }
    }
    public class OGMeta
    {
        public OGMeta()
        {
            Video = new OGVideo();
        }

        public string AppId { set; get; }
        public string SiteName { set; get; }
        public string Locale { set; get; }
        public string Type { set; get; }
        public string Title { set; get; }
        public string Description { set; get; }
        public string Url { set; get; }
        public string Image { set; get; }
        public string Audio { set; get; }
        public OGVideo Video { set; get; }
    }
    public class HtmlTool
    {

        public static OGMeta FetchOG(string url)
        {
            OGMeta meta = new OGMeta();

            string html = FetchHtml(url).Trim();

            var doc = new HtmlDocument();
            doc.LoadHtml(html);

            var list = doc.DocumentNode.SelectNodes("//meta");
            
            foreach (var node in list)
            {
                if (!node.HasAttributes) continue;

                if (node.Attributes["property"] != null && node.Attributes["content"] != null)
                {
                    switch (node.Attributes["property"].Value.ToLowerInvariant())
                    {
                        case "fb:app_id":
                            meta.AppId = node.Attributes["content"].Value;
                            break;
                        case "og:site_name":
                            meta.SiteName = node.Attributes["content"].Value;
                            break;
                        case "og:locale":
                            meta.Locale = node.Attributes["content"].Value;
                            break;
                        case "og:type":
                            meta.Type = node.Attributes["content"].Value;
                            break;
                        case "og:title":
                            meta.Title = node.Attributes["content"].Value;
                            break;
                        case "og:description":
                            meta.Description = node.Attributes["content"].Value;
                            break;
                        case "description":
                            if (meta.Description.IsEmpty())
                                meta.Description = node.Attributes["content"].Value;
                            break;
                        case "og:url":
                            meta.Url = node.Attributes["content"].Value;
                            break;
                        case "og:image":
                            meta.Image = node.Attributes["content"].Value;
                            break;
                        case "og:audio":
                            meta.Audio = node.Attributes["content"].Value;
                            break;
                        case "og:video":
                            meta.Video.Content = node.Attributes["content"].Value;
                            break;
                        case "og:video:type":
                            meta.Video.Type = node.Attributes["content"].Value;
                            break;
                        case "og:video:width":
                            meta.Video.Width = node.Attributes["content"].Value;
                            break;
                        case "og:video:height":
                            meta.Video.Height = node.Attributes["content"].Value;
                            break;
                        case "og:video:tag":
                            meta.Video.Tag = node.Attributes["content"].Value;
                            break;
                        case "og:video:secure_url":
                            meta.Video.SecureUrl = node.Attributes["content"].Value;
                            break;
                    }
                }

                if (node.Attributes["name"] != null && node.Attributes["content"] != null)
                {
                    switch (node.Attributes["name"].Value.ToLowerInvariant())
                    {

                        case "description":
                            if (meta.Description.IsEmpty())
                                meta.Description = node.Attributes["content"].Value;
                            break;
                        case "twitter:title":
                            if (meta.Title.IsEmpty())
                                meta.Title = node.Attributes["content"].Value;
                            break;
                        case "twitter:image":
                            if (meta.Image.IsEmpty())
                                meta.Image = node.Attributes["content"].Value;
                            break;
                    }
                }

            }

            if (meta.Title.IsEmpty())
            {
                var node = doc.DocumentNode.SelectSingleNode("//title");
                if(node != null)
                    meta.Title = node.InnerText;
            }


            return meta;
        }

        public static string FetchHtml(string url)
        {
            string o = "";

            try
            {
                HttpWebRequest oReq = (HttpWebRequest)WebRequest.Create(url);
                oReq.UserAgent = "Mozilla/5.0 (compatible; MSIE 9.0; Windows NT 6.1; Trident/5.0)";
                HttpWebResponse resp = (HttpWebResponse)oReq.GetResponse();
                Stream stream = resp.GetResponseStream();
                StreamReader reader = new StreamReader(stream);
                o = reader.ReadToEnd();
            }
            catch (Exception ex) { }

            return o;
        }

    }
}
