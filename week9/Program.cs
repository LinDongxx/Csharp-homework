using System;
using System.Text.RegularExpressions;
using System.Net.Http;
using System.Collections.Generic;
using System.IO;

namespace SimpleCrawler
{
    public class Crawler
    {

        static void Main(string[] args)
        {
            Crawler crawler = new Crawler("http://www.cnblogs.com/dstang2000/");
            //crawler.MaxCount = 20;

            //new Thread(() =>
            //{
            //    crawler.Crawl();
            //    Console.WriteLine("---------------------------------------------------------------");
            //    Console.WriteLine("crawled urls:\n");
            //    crawler.CrawledUrls.ForEach(url => Console.WriteLine(url));
            //    Console.WriteLine("\nwrong urls:\n");
            //    crawler.WrongUrls.ForEach(url => Console.WriteLine(url));
            //}).Start();
        }
        //private Hashtable urls = new Hashtable();
        //A queue would be better than hashtable,
        //and use dictionary instead of Hashtable to record the visited url
        private readonly Queue<string> urlsQueue = new Queue<string>();
        private readonly Dictionary<string, bool> isUrlVisited = new Dictionary<string, bool>();
        private int count = 0;
        private readonly HttpClient httpClient = new HttpClient();
        private List<string> crawledUrls = new List<string>();
        private List<string> wrongUrls = new List<string>();

        public int MaxCount { get; set; }
        public List<string> CrawledUrls => crawledUrls;
        public List<string> WrongUrls => wrongUrls;
        public Crawler(string startUrl)
        {
            urlsQueue.Enqueue(startUrl);
        }
        public void Crawl()
        {
            string isHtml = @"^<(?:!DOCTYPE)\s*html>";
            while (urlsQueue.Count > 0)
            {
                string current = urlsQueue.Dequeue();
                if (current == null) break;
                Console.WriteLine(current);
                string html = Download(current);
                count++;
                if (count >= MaxCount) break;
                if (Regex.IsMatch(html, isHtml)) //only parse html
                    Parse(html, current);
            }
        }

        public string Download(string url)
        {
            try
            {
                //WebClient webClient = new WebClient();
                //WebClient is obsolete in .Net6; Use HttpClient instead;
                string html = httpClient.GetStringAsync(url).Result;
                string fileName = count.ToString();
                File.WriteAllText(fileName, html, System.Text.Encoding.UTF8);
                crawledUrls.Add(url);
                return html;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                wrongUrls.Add(url);
                return "";
            }
        }

        public void Parse(string html, string url)
        {
            if (string.IsNullOrEmpty(html)) { return; }

            string hrefPattern = @"href\s*=\s*(?:[""'](?<1>[^""']*)[""']|(?<1>[^>\s]+))";
            MatchCollection matchCollection = Regex.Matches(html, hrefPattern);
            Uri baseUri = new Uri(url);
            foreach (Match match in matchCollection)
            {
                string strRef = match.Groups[1].Value;
                if (string.IsNullOrEmpty(strRef)) continue;
                if (!strRef.StartsWith("http"))//relativePath to absolutePath
                {
                    strRef = new Uri(baseUri, strRef).ToString();
                }
                Uri refUri = new Uri(strRef);
                if (baseUri.Host == refUri.Host)//only crawl the website of the initial host
                {
                    if (isUrlVisited.ContainsKey(strRef)) continue;
                    isUrlVisited[strRef] = true;
                    urlsQueue.Enqueue(strRef);
                }
            }

        }
    }
}