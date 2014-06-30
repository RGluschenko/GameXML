using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using System.IO;

namespace GameXML
{

    /// <summary>
    /// Класс для работы с локациями нашей игры.
    /// </summary>
    public class Location
    {
        /// <summary>
        /// Локация из которой осуществен переход.
        /// </summary>
        private Location parent;
        /// <summary>
        /// Список локаций, в которые можно перейти из текущей.
        /// </summary>
        public List<Location> children;
        /// <summary>
        /// Текст описывающий локацию.
        /// </summary>
        public string LevelText;
        /// <summary>
        /// Текст, который выводится в вариантах перхода.
        /// </summary>
        public string EnterText;
        /// <summary>
        /// Определяет тип финишной локации(победа, смерть).
        /// </summary>
        public string result;
        /// <summary>
        /// Метод, выводящий всю текстовую информацию в консоль.
        /// </summary>

        public void PlayLocation()
        {
            Console.WriteLine(LevelText);
            Console.WriteLine();
            for (int i = 0; i < children.Count; i++)
            {
                Console.WriteLine("{0} - {1}", i, children[i].EnterText);
            }
            if (this.parent!=null &&result==null)
            {
                Console.WriteLine("{0} - {1}", "9", "Вернуться в предидущую локацию.");
            }
            Console.WriteLine();
            if (this.result == "win")
            {
                Console.WriteLine("Вы победили=)");
                Console.ReadLine();
            }
            else if (this.result == "dead")
            {
                Console.WriteLine("Вы мертвы=(");
                Console.ReadLine();
            }
            else ChooseNewLocation();
        }
        /// <summary>
        /// Метод, считывающий введеный вариант. Проверяет правильность ввода и осуществляет переход к следующей локации.
        /// </summary>
        private void ChooseNewLocation()
        {
            Console.Write("Виберите вариант(введите цифру):");
            try
            {
                int variant = Int32.Parse(Console.ReadLine());
                if (variant == 9)
                {
                    parent.PlayLocation();
                }
                else {
                    children[variant].parent = this;
                    children[variant].PlayLocation();

                }
            }
            catch (Exception)
            {
                Console.WriteLine("Ошибка ввода");
                this.ChooseNewLocation();
            }

        }
        /// <summary>
        /// Метод, добавляющий к текущей локации дочерние локации(те в которые можно перейти).
        /// </summary>
        /// <param name="child">Локация, которую добавляют в список дочерних.</param>
        public void GetChild(Location child)
        {
            children.Add(child);
            //child.parent = this;
        }
        /// <summary>
        /// Конструктор, создающий обьект спика дочерних локаций.
        /// </summary>
        public Location()
        {
            children = new List<Location>();
        }
        public Location(Location parent)
            : this()
        {
            parent.GetChild(this);
        }

        /// <summary>
        /// Загрузка дерева локаций из XML-файла.
        /// </summary>
        /// <param name="fileName">Имя файла.</param>
        /// <returns>Локация первого уровня.</returns>
        public static Location DeserializeLocations(string fileName)
        {
            XmlAttributes attrs = new XmlAttributes();


            XmlElementAttribute attr = new XmlElementAttribute();
            attr.ElementName = "Location";
            attr.Type = typeof(Location);


            attrs.XmlElements.Add(attr);


            XmlAttributeOverrides attrOverrides = new XmlAttributeOverrides();


            attrOverrides.Add(typeof(Location), "Instruments", attrs);
            XmlSerializer writter = new XmlSerializer(typeof(Location), attrOverrides);
            var path = fileName;

            FileStream file = new FileStream(path, FileMode.Open);
            Location root = (Location)writter.Deserialize(file);
            file.Close();

            return root;
        }
        /// <summary>
        /// Сохранение дерева локаций в XML-файл.
        /// </summary>
        /// <param name="fileName">Имя файла.</param>
        public void SerializeLocations(string fileName)
        {
            XmlAttributes attrs = new XmlAttributes();


            XmlElementAttribute attr = new XmlElementAttribute();
            attr.ElementName = "Location";
            attr.Type = typeof(Location);


            attrs.XmlElements.Add(attr);


            XmlAttributeOverrides attrOverrides = new XmlAttributeOverrides();


            attrOverrides.Add(typeof(Location), "Instruments", attrs);
            XmlSerializer writter = new XmlSerializer(typeof(Location), attrOverrides);
            var path = fileName;

            FileStream file = File.Create(path);
            writter.Serialize(file, this);
            file.Close();
        }

    }
}
