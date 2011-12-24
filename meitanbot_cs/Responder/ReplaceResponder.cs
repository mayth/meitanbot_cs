using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Meitanbot
{
    class ReplaceResponder : IResponder
    {
        RandomTextCreator creator;
        Config config;

        public ReplaceResponder(Config config, string path)
        {
            creator = new RandomTextCreator(path);
            this.config = config;
        }

        public string Response(string input, params object[] args)
        {
            if (args.Length == 1)
                return creator.Create(config.NumberOfUsersWord, config.NumberOfOthersWord, config.WordReplaceProbability, (decimal)args[0]);
            else
                return creator.Create(config.NumberOfWord, config.WordReplaceProbability);
        }
    }
}
