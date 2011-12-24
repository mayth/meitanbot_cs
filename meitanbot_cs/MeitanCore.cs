using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace Meitanbot
{
    class MeitanCore
    {
        TwitterClient client;
        Config config;
        System.Threading.Timer randomPostTimer;

        Dictionary<int, Tuple<string, string>> classTimeTable;
        List<int> ignoreIDList;

        // Responders
        IResponder keywordResponder;
        IResponder replaceResponder;

        /// <summary>
        /// Initializes a new instance of <see cref="MeitanCore"/> class with the specified credential file and the specified configuration file.
        /// </summary>
        /// <param name="credentialPath">A path for the credential file.</param>
        /// <param name="configPath">A path for the configation file.</param>
        public MeitanCore(string credentialPath, string configPath)
        {
            client = new TwitterClient(credentialPath);
            if (System.IO.File.Exists(configPath))
                config = Config.Load(configPath);
            else
            {
                config = new Config();
                config.Save(configPath);
            }

            classTimeTable = new Dictionary<int, Tuple<string, string>>();
            var doc = XElement.Load(config.TsukubaTimeTableFile);
            foreach (var i in Enumerable.Range(0, 6))
            {
                var el = doc.Element("period" + (i + 1));
                classTimeTable[i] = Tuple.Create(el.Attribute("begin").Value, el.Attribute("end").Value);
            }

            ignoreIDList = new List<int>();
            foreach (var s in System.IO.File.ReadAllLines(config.IgnoreIDFile))
                ignoreIDList.Add(int.Parse(s));

            keywordResponder = new KeywordResponder("KeywordResponse.xml");
            replaceResponder = new ReplaceResponder(config, "posts.db");

            client.StartStreaming(null, null, StatusCreated, null, null, null, null);
            randomPostTimer = new System.Threading.Timer(RandomPost, null, 0, config.RandomPostInterval * 1000);
        }

        void StatusCreated(Twitterizer.TwitterStatus status)
        {
            string response = keywordResponder.Response(status.Text);
            if (!string.IsNullOrEmpty(response))
            {
                client.Reply(status, response);
            }
            else
            {
                if (System.Text.RegularExpressions.Regex.IsMatch(status.Text, "^@meitanbot"))
                {
                    client.Reply(status, replaceResponder.Response(null, status.User.Id));
                }
            }
        }

        void RandomPost(object state)
        {
            client.Post(replaceResponder.Response(null));
        }
    }
}
