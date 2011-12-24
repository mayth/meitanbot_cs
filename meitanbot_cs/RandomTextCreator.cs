using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SQLite;

namespace Meitanbot
{
    class RandomTextCreator
    {
        static readonly string mecabBinDir = @"D:\ProgramFiles(x86)\MeCab-sjis\bin"; 
        string dbPath;

        /// <summary>
        /// Initializes a new instance of <see cref="RandomTextCreator"/> class.
        /// </summary>
        /// <param name="path">A path for the database file.</param>
        public RandomTextCreator(string path)
        {
            if (!System.IO.File.Exists(path))
                throw new System.IO.FileNotFoundException("The specified database file is not found.", path);

            dbPath = path;
        }

        string Create(int wordCount, double probability, Func<SQLiteCommand, string> statusSelector, Func<SQLiteCommand, IList<string>> wordSelector)
        {
            if (wordCount < 0)
                throw new ArgumentOutOfRangeException("wordCount", "wordCount must be greater than 0.");
            if (probability < 0.0 || 1.0 < probability)
                throw new ArgumentOutOfRangeException("probability", "probability must be in the range between 0.0 and 1.0.");

            string status;
            IList<string> words;
            using (var con = new SQLiteConnection("Data Source=" + dbPath))
            {
                con.Open();
                var command = con.CreateCommand();
                status = statusSelector(command);
                command = con.CreateCommand();
                words = wordSelector(command);
                con.Close();
            }

            // parse status
            string parseResult = string.Empty;
            var process = new System.Diagnostics.Process();
            process.StartInfo.FileName = System.IO.Path.Combine(mecabBinDir, "mecab.exe");
            process.StartInfo.UseShellExecute = false;
            process.StartInfo.RedirectStandardOutput = true;
            process.StartInfo.RedirectStandardInput = true;
            process.OutputDataReceived += (sender, e) => parseResult += e.Data;
            process.Start();
            process.BeginOutputReadLine();
            process.StandardInput.WriteLine(status);
            process.StandardInput.Flush();
            process.WaitForExit(10 * 1000);    // 10 sec. for timeout.
            process.Kill();

            // create the result
            Random rand = new Random();
            List<string> result = new List<string>();
            using (var reader = new System.IO.StringReader(parseResult))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    var surface = line.Split('\t')[0];
                    var feature = line.Split('\t')[1];
                    if (!feature.Contains("名詞,"))
                    {
                        result.Add(surface);
                        continue;
                    }
                    if ((probability - rand.NextDouble()) >= 0.0)
                        result.Add(words[rand.Next(words.Count)]);
                    else
                        result.Add(surface);
                }
            }
            string tmp = string.Empty;
            result.ForEach(s => tmp += s);
            return tmp;
        }

        public string Create(int wordCount, double probability)
        {
            return Create(wordCount, probability,
                command =>
                {
                    command.CommandText = "SELECT status FROM posts ORDER BY RANDOM() LIMIT 1;";
                    return command.ExecuteScalar() as string;
                },
                command =>
                {
                    List<string> words = new List<string>();
                    command.CommandText = "SELECT word FROM words ORDER BY RANDOM() LIMIT " + wordCount;
                    using (var reader = command.ExecuteReader())
                        while (reader.Read())
                            words.Add(reader.GetString(0));
                    return words;
                });
        }

        public string Create(int userWordCount, int otherWordCount, double probability, decimal userID)
        {
            int wordCount = userWordCount + otherWordCount;
            return Create(wordCount, probability,
                command =>
                {
                    command.CommandText = "SELECT status FROM posts WHERE user_id = " + userID + " ORDER BY RANDOM() LIMIT 1;";
                    return command.ExecuteScalar() as string;
                },
                command =>
                {
                    List<string> userWords = new List<string>();
                    command.CommandText = "SELECT word FROM words WHERE user_id = " + userID + " ORDER BY RANDOM() LIMIT " + userWordCount;
                    using (var reader = command.ExecuteReader())
                        while (reader.Read())
                            userWords.Add(reader.GetString(0));
                    List<string> otherWords = new List<string>();
                    int otherWordsToGetActual = otherWordCount;
                    if (userWords.Count < userWordCount)
                        otherWordsToGetActual += (userWordCount - userWords.Count);
                    command.CommandText = "SELECT word FROM words ORDER BY RANDOM() LIMIT " + otherWordsToGetActual;
                    using (var reader = command.ExecuteReader())
                        while (reader.Read())
                            otherWords.Add(reader.GetString(0));

                    return userWords.Concat(otherWords).Distinct().ToList();
                });
        }
    }
}
