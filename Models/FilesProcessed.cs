using System;

namespace Gas.Models
{
    public partial class FilesProcessed
    {
        public int Id { get; set; }

        public string FilePath { get; set; }

        public DateTime FileDate { get; set; }

        public DateTime ProcessedDateTime { get; set; }
    }
}
