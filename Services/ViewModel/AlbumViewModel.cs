using System;
using System.Collections.Generic;
using System.Text;

namespace Services.ViewModel
{
   public class AlbumViewModel
    {
        public string Name { get; set; }
        public string Image { get; set; }
        public DateTime? ReleasedDate { get; set; }
        public int? Rating { get; set; }
        public string Review { get; set; }
        public decimal? Price { get; set; }
        public long? ArtistId { get; set; }
    }
}
