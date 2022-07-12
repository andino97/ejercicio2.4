using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace tareavideo2_4.Models
{
    public class VideoModel
    {
        [PrimaryKey, AutoIncrement]
        public int VideoId { get; set; }

        [MaxLength(200)]
        public string VideoUri { get; set; }

        [MaxLength(80)]
        public string VideoDescripcion { get; set; }

    }
}
