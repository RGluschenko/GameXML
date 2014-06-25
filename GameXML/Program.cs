using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using System.IO;

namespace GameXML
{
    class Program
    {
        static void Main(string[] args)
        {
            Location root = Location.DeserializeLocations("Gena.xml");
            root.PlayLocation();
            
        }

     



        
    }
}
