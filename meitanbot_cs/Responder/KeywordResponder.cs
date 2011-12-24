using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.Text.RegularExpressions;

namespace Meitanbot
{
    class KeywordResponder : IResponder
    {
        List<Tuple<Regex, IList<string>>> responses;

        /// <summary>
        /// Initializes a new instance of <see cref="KeywordResponder"/> class with the specified configuration file.
        /// </summary>
        /// <param name="path">A path for configuration file.</param>
        public KeywordResponder(string path)
        {
            responses = new List<Tuple<Regex, IList<string>>>();
            var doc = XElement.Load(path);
            foreach (var elRule in doc.Elements())
            {
                IList<string> tmp = new List<string>();
                foreach(var elRes in elRule.Elements("response"))
                    tmp.Add(elRes.Value);
                responses.Add(Tuple.Create(new Regex(elRule.Element("pattern").Value), tmp));
            }
        }

        public string Response(string input, params object[] args)
        {
            Random rand = new Random();
            foreach (var res in responses)
            {
                if (res.Item1.IsMatch(input))
                    return res.Item2[rand.Next(res.Item2.Count)];
            }
            return null;
        }
    }
}
