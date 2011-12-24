using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace Meitanbot
{
    /// <summary>
    /// Represents a configures for Meitanbot.
    /// </summary>
    public class Config
    {
        #region Properties
        /// <summary>
        /// Gets or sets a max count of continuative retrying.
        /// </summary>
        public int MaxContinuativeRetryCount { get; set; }
        /// <summary>
        /// Gets or sets a short interval time for retrying.
        /// </summary>
        public int ShortRetryInterval { get; set; }
        /// <summary>
        /// Gets or sets a long interval time for retrying.
        /// </summary>
        public int LongRetryInterval { get; set; }
        /// <summary>
        /// Gets or sets a time for resetting the reply limit
        /// </summary>
        public int ReplyLimitResetTime { get; set; }
        /// <summary>
        /// Gets or sets a threshold for limitting replies.
        /// </summary>
        public int ReplyLimitThreshold { get; set; }
        /// <summary>
        /// Gets or sets a time for sleeping when the bot received HTTP Error 403: Forbidden.
        /// </summary>
        public int SleepTimeWhenForbidden { get; set; }
        /// <summary>
        /// Gets or sets a interval for statistics.
        /// </summary>
        public int StatisticInterval { get; set; }
        /// <summary>
        /// Gets or sets a interval for posting randomly.
        /// </summary>
        public int RandomPostInterval { get; set; }
        /// <summary>
        /// Gets or sets a probability for replacing words.
        /// </summary>
        public double WordReplaceProbability { get; set; }
        /// <summary>
        /// Gets or sets a minimum length for recording word.
        /// </summary>
        public int MinimumWordLength { get; set; }
        /// <summary>
        /// Gets or sets a minimum length for recording status.
        /// </summary>
        public int MinimumStatusLength { get; set; }
        /// <summary>
        /// Gets or sets how many words will take from the database when replacing words.
        /// </summary>
        public int NumberOfWord { get; set; }
        /// <summary>
        /// Gets or sets how many words recorded from the saying user will take from the database when replacing words.
        /// </summary>
        public int NumberOfUsersWord { get; set; }
        /// <summary>
        /// Gets or sets how many words recorded from except for the saying user will take from the database when replacing words.
        /// </summary>
        public int NumberOfOthersWord { get; set; }
        /// <summary>
        /// Gets or sets how many words will be saved on the memory to record to the database.
        /// </summary>
        public int MaxPendingWord { get; set; }
        /// <summary>
        /// Gets or sets how many words will remain after deleting the pending words.
        /// </summary>
        public int WordRemainAfterDelete { get; set; }
        /// <summary>
        /// Gets or sets a threshold to save to the database.
        /// </summary>
        public int WordRecordThreshold { get; set; }
        /// <summary>
        /// Gets or sets a prefix for statistics file.
        /// </summary>
        public string StatisticsFilePrefix { get; set; }
        /// <summary>
        /// Gets or sets a prefix for log file.
        /// </summary>
        public string LogFilePrefix { get; set; }
        /// <summary>
        /// Gets or sets a path for the Tsukuba University's class time table.
        /// </summary>
        public string TsukubaTimeTableFile { get; set; }
        /// <summary>
        /// Gets or sets a path for the ignoring users list file.
        /// </summary>
        public string IgnoreIDFile { get; set; }
        #endregion

        public static Config Load(string path)
        {
            var serializer = new System.Xml.Serialization.XmlSerializer(typeof(Config));
            using (var fs = new System.IO.FileStream(path, System.IO.FileMode.Open))
                return (Config)serializer.Deserialize(fs);
        }

        /// <summary>
        /// Initializes a new instance of <see cref="Config"/> class with default values.
        /// </summary>
        public Config()
        {
            MaxContinuativeRetryCount = 5;
            ShortRetryInterval = 15;
            LongRetryInterval = 60;
            ReplyLimitResetTime = 60;
            ReplyLimitThreshold = 20;
            SleepTimeWhenForbidden = 600;
            StatisticInterval = 60;
            RandomPostInterval = 600;
            WordReplaceProbability = 0.5;
            MinimumWordLength = 2;
            MinimumStatusLength = 2;
            NumberOfWord = 20;
            NumberOfUsersWord = 10;
            NumberOfOthersWord = 10;
            MaxPendingWord = 500;
            WordRemainAfterDelete = 400;
            WordRecordThreshold = 5;
            StatisticsFilePrefix = "stat_";
            LogFilePrefix = "log_";
            TsukubaTimeTableFile = "tsukuba_class_time.xml";
            IgnoreIDFile = "ignore_id.dat";
        }

        public void Save(string path)
        {
            var serializer = new System.Xml.Serialization.XmlSerializer(typeof(Config));
            using (var fs = new System.IO.FileStream(path, System.IO.FileMode.Create))
                serializer.Serialize(fs, this);
        }
    }
}
