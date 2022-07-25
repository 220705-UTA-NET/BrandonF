using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicApp.Logic
{
    public class Song
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Artist { get; set; }
        public DateTime Date { get; set; }
    }
}
