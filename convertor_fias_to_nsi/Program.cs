using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace convertor_fias_to_nsi
{
    class Program
    {
        private static XmlDocument newXmlDocument = new XmlDocument();
        static void Main(string[] args)
        {
            save_to_xml("param1", "value1");
        rep: if(!loadXml())
            {
                Console.ReadKey();
                goto rep; }
            else
            {
                Console.WriteLine("Ты смог");
                Console.ReadKey();
            }
        }
        private static void save_to_xml(string p1, string p2)
        {
            XmlDocument document = new XmlDocument();
            document.Load("test_out.xml");
            XmlNode record = document.CreateElement("record");
            record.InnerText = "Hello"; // и значение
            record.AppendChild(record); // указываем родителя
            document.Save("test_out.xml");

            //XmlNode record = document.CreateElement("record");
            //document.DocumentElement.AppendChild(record); // указываем родителя
            //XmlAttribute attribute = document.CreateAttribute("number"); // создаём атрибут
            //attribute.Value = "1"; // устанавливаем значение атрибута
            //record.Attributes.Append(attribute); // добавляем атрибут

            //subElement1.InnerText = "Hello"; // и значение
            //record.AppendChild(subElement1); // и указываем кому принадлежит
            //document.Save("test_out.xml");
            Console.ReadKey();
        }
        private static bool loadXml()
        {
            Console.WriteLine("Введите имя файла:");
            //string FileName = Console.ReadLine();
            string FileName = "test.xml";
            Console.WriteLine("test.xml");
            //XmlTextReader xmlKptReader = new XmlTextReader(FileName);
            try
            {
                //newXmlDocument.Load(xmlKptReader);
                parsingXml2();
            }
            //catch (XmlException)
            catch (Exception)
            {
                Console.WriteLine("Выбранный файл не соответствует формату. Попробуйте еще раз.");
                return false;
            }
            return true;
        }
        private static void parsingXml2()
        {
            int counter = 0;
            int counter2 = 0;
            int counter3 = 0;
            int counter4 = 0;
            using (XmlReader xml = XmlReader.Create("test.xml"))
            {
                counter4++;
                while (xml.Read())
                {
                    counter2++;
                    switch (xml.NodeType)
                    { 
                        case XmlNodeType.Element:
                            // нашли элемент member
                            if (xml.Name == "Object")
                            {
                                // передаем данные в класс Member
                                XmlDocument XmlDoc = new XmlDocument();
                                XmlDoc.LoadXml(xml.ReadOuterXml());
                                XmlNode n = XmlDoc.SelectSingleNode("Object");
                                Console.WriteLine("AOGUID: {0}", n.Attributes["AOGUID"].Value);
                                Console.WriteLine("AOID: {0}", n.Attributes["AOID"].Value);
                                //Console.WriteLine("Псевдоним: {0}", n.SelectSingleNode("nickname").InnerText);
                                //Console.WriteLine("Имя: {0}", n.SelectSingleNode("firstName").InnerText);
                                //Console.WriteLine("Фамилия: {0}", n.SelectSingleNode("lastName").InnerText);
                                counter++;
                            }
                            break;
                        default: counter3++; break;
                    }
                }
                Console.WriteLine("Counter успешно показаны: {0}", counter);
                Console.WriteLine("Counter в while: {0}", counter2);
                Console.WriteLine("Counter ушел по дефолту: {0}", counter3);
                Console.WriteLine("Counter: {0}", counter4);
            }
        }
        private static void parsingXml()
        {
            //Проверим наличие элемента Main_Root_Element в нашем файле:
            XmlNode mainRootElementNode = newXmlDocument.SelectSingleNode("/Main_Root_Element");
            if (mainRootElementNode != null)
            {
                //Убедившись в существовании элемента Main_Root_Element приступим к элементу Document:
                var documentNode = mainRootElementNode.SelectSingleNode("Document");
                if (documentNode != null)
                {
                    //Элемент имеет несколько атрибутов - создадим коллекцию атрибутов и проверим её на существование:
                    XmlAttributeCollection documentAttributes = documentNode.Attributes;
                    if (documentAttributes != null)
                    {
                        //Убедившись, что атрибуты существуют, получим их значения и запишем в соответствующие свойства
                        //экземпляра класса newParsedXmlDocument:
                        foreach (XmlAttribute documentAttribute in documentAttributes)
                        {
                            switch (documentAttribute.Name)
                            {
                                case "Code":
                                    Console.WriteLine(XmlConvert.ToInt32(documentAttribute.Value));
                                    break;
                                case "GUID":
                                    Console.WriteLine(documentAttribute.Value);
                                    break;
                            }
                        }
                    }
                }
            }
        }
    }
}
