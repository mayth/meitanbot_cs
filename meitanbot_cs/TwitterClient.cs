using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Twitterizer;
using Twitterizer.Streaming;

namespace Meitanbot
{
    /// <summary>
    /// Represents a client for Twitter.
    /// </summary>
    class TwitterClient : IDisposable
    {
        static readonly string UserAgent = "Nowhere-type Meitan bot 2.0 by @maytheplic";
        OAuthTokens token;  // Contains consumer key/secret, access token/secret.
        TwitterStream stream;   // UserStreaming

        /// <summary>
        /// Initializes a new instance of <see cref="TwitterClient"/> class with the specified credential file.
        /// </summary>
        /// <param name="path">A path to the credential file.</param>
        /// <exception cref="System.IO.FileNotFoundException">The file specified by <paramref name="path"/> is not found.</exception>
        public TwitterClient(string path)
        {
            if (!System.IO.File.Exists(path))
                throw new System.IO.FileNotFoundException("The specified credential file is not found.", path);

            var tokenFileLines = System.IO.File.ReadAllLines(path);
            if (tokenFileLines.Length != 4 || tokenFileLines.Any(s => string.IsNullOrWhiteSpace(s)))
                throw new Exception("Cannot read your credential from credential file.");

            token = new OAuthTokens()
            {
                ConsumerKey = tokenFileLines[0],
                ConsumerSecret = tokenFileLines[1],
                AccessToken = tokenFileLines[2],
                AccessTokenSecret = tokenFileLines[3]
            };
        }

        /// <summary>
        /// Starts UserStream.
        /// </summary>
        /// <param name="friendsCallback">A callback called when UserStream is intialized. This can be null.</param>
        /// <param name="errorCallback">A callback called when UserStream is stopped. This can be null.</param>
        /// <param name="statusCreatedCallback">A callback when receive a new status. This can be null.</param>
        /// <param name="statusDeletedCallback">A callback when a status is deleted. This can be null.</param>
        /// <param name="dmCreatedCallback">A callback when receive a new direct message. This can be null.</param>
        /// <param name="dmDeletedCallback">A callback when a direct message is deleted. This can be null.</param>
        /// <param name="eventCallback">A callback when a new event is raised. This can be null.</param>
        public void StartStreaming(InitUserStreamCallback friendsCallback, StreamStoppedCallback errorCallback,
            StatusCreatedCallback statusCreatedCallback, StatusDeletedCallback statusDeletedCallback,
            DirectMessageCreatedCallback dmCreatedCallback, DirectMessageDeletedCallback dmDeletedCallback,
            EventCallback eventCallback)
        {
            var option = new StreamOptions()
            {
                Count = 0
            };
            stream = new TwitterStream(token, UserAgent, option);
            stream.StartUserStream(friendsCallback, errorCallback, statusCreatedCallback, statusDeletedCallback, dmCreatedCallback, dmDeletedCallback, eventCallback, null);
        }

        /// <summary>
        /// Stops UserStream.
        /// </summary>
        public void StopStreaming()
        {
            stream.EndStream();
        }

        #region Posting
        /// <summary>
        /// Replies to the specified status.
        /// </summary>
        /// <param name="replyTo">A status that replies to.</param>
        /// <param name="status">A new status text.</param>
        public void Reply(TwitterStatus replyTo, string status)
        {
            var option = new StatusUpdateOptions()
            {
                InReplyToStatusId = replyTo.Id
            };
            Post(string.Format("@{0} {1}", replyTo.User.ScreenName, status), option);
        }

        /// <summary>
        /// Posts the new status.
        /// </summary>
        /// <param name="status">A new status text.</param>
        public void Post(string status)
        {
            Post(status, null);
        }

        /// <summary>
        /// Posts the new status with the specified options.
        /// </summary>
        /// <param name="status">A new status text.</param>
        /// <param name="option">An option for updating status.</param>
        void Post(string status, StatusUpdateOptions option)
        {
            if (option == null)
                TwitterStatus.Update(token, status);
            else
                TwitterStatus.Update(token, status, option);
        }
        #endregion

        /// <summary>
        /// 
        /// </summary>
        public void Dispose()
        {
            if (stream != null)
            {
                stream.EndStream();
                stream.Dispose();
            }
        }
    }
}
