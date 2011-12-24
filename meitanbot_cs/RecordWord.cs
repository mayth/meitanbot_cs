using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Meitanbot
{
    class RecordWord
    {
        public long ID { get; set; }
        public long UserID { get; set; }
        public DateTime RecordedAt { get; set; }
        public WordType Type { get; set; }
        public string Word { get; set; }
        public Category Category { get; set; }
    }
}
