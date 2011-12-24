using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Meitanbot
{
    class RecordStatus
    {
        public long ID { get; set; }
        public long UserID { get; set; }
        public long InReplyToStatusID { get; set; }
        public DateTime RecordedAt { get; set; }
        public string Text { get; set; }
        public Category Category { get; set; }
    }
}
