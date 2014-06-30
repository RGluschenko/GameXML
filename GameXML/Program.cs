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
            Location root = Location.DeserializeLocations("Gena2.xml");
            root.PlayLocation();
        }

        private static void SetGame()
        {
            Location root = new Location() { LevelText = "Вы в комнате с бассейном по центру, в котором сидит крокодил" };
            Location ch1 = new Location(root) { LevelText = "Вы ушли и спокойно добрались домой", EnterText = "Выйти из комнаты", result = "win" };
            Location ch2 = new Location(root)
            {
                LevelText = "В басейне сидит и смотрит на вас огромный крокодил",
                EnterText = "Подойти к басейну"
            };
            Location ch22 = new Location(ch2)
            {
                LevelText = "Своей выходкой вы не на шутку разозлили Гену и стали его обедом",
                EnterText = "Ткнуть крокодила пальцев в глаз",
                result = "dead"
            };
            ch2.GetChild(ch1);
            root.SerializeLocations("Gena2.xml");
        }

     



        
    }
}
