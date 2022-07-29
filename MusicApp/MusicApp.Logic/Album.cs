using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicApp.Logic
{
    public class Album
    {

        public string Title { get; set; }
        public string Artist { get; set; }

        public Album ()
        {

        }

        public Album(string title, string artist)
        {
            Title = title;
            Artist = artist;
        }
    }
}
