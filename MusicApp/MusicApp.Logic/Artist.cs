using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicApp.Logic
{
    public class Artist
    {  
        public string Name { get; set; }
        public Artist() { }
        public Artist (string name)
        {
            Name = name;
        }
    }
}
