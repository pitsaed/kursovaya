using System;
using System.Collections.Generic;
using System.Xml;

namespace _4sem_kurs
{
    using Enums;
    [Serializable]
    public class Database
    {
        const string FILE_NAME_USERS = "C:\\Users\\User\\source\\repos\\4sem_kurs\\4sem_kurs\\users.xml";
        const string FILE_NAME_REQUESTS = "C:\\Users\\User\\source\\repos\\4sem_kurs\\4sem_kurs\\requests.xml";
        const string FILE_NAME_POWERBLOCKS = "C:\\Users\\User\\source\\repos\\4sem_kurs\\4sem_kurs\\powerBlocks.xml";
        const string FILE_NAME_PLACES = "C:\\Users\\User\\source\\repos\\4sem_kurs\\4sem_kurs\\places.xml";

        public List<User> Users { get; set; }
        public List<Request> Requests { get; set; }
        public List<PowerBlock> PowerBlocks { get; set; }
        public List<Place> Places { get; set; }
        public Database()
        {
            
        }
       
        
        public void LoadUsers()
        {
            XmlDocument xDoc = new XmlDocument();
            xDoc.Load(FILE_NAME_USERS);
            XmlElement xRoot = xDoc.DocumentElement;
            Users = new List<User>();
            foreach (XmlElement xnode in xRoot)
            {
                User user = new User();
                XmlNode attr = xnode.Attributes.GetNamedItem("login");
                if (attr != null)
                    user.Login = attr.Value;

                foreach (XmlNode childnode in xnode.ChildNodes)
                {
                    if (childnode.Name == "password")
                        user.Password = childnode.InnerText;

                    if (childnode.Name == "userRole")
                        user.UserRole = (UserRole)Int32.Parse(childnode.InnerText);
                }
                Users.Add(user);
            }

        }

        public void LoadRequests()
        {
            XmlDocument xDoc = new XmlDocument();
            xDoc.Load(FILE_NAME_REQUESTS);
            XmlElement xRoot = xDoc.DocumentElement;
            Requests = new List<Request>();
            foreach (XmlElement xnode in xRoot)
            {
                Request request = new Request();
               
               

                foreach (XmlNode childnode in xnode.ChildNodes)
                {
                    if (childnode.Name == "electricalStation")
                    {
                        switch (childnode.InnerText)
                        {
                            case "HeatElectricalStation":
                                {
                                    request.Station = new HeatElectricalStation();
                                    break;
                                }
                            case "NuclearElectricalStation":
                                {
                                    request.Station = new NuclearElectricalStation();
                                    break;
                                }
                        }
                    }
                    if (childnode.Name == "place")
                    {
                        request.Place = new Place();
                        foreach(var item in Places)
                        {
                            if (item.Street == childnode.InnerText)
                            {
                                request.Place = item;
                            }
                        }
                    }   
                    if (childnode.Name == "block")
                    {
                        request.Block = new PowerBlock();
                        foreach(var item in PowerBlocks)
                        {
                            if (item.Name == childnode.InnerText)
                            {
                                request.Block = item;
                            }
                        }
                    }
                }
                Requests.Add(request);
            }
        }
        public void LoadPowerBlocks()
        {
            XmlDocument xDoc = new XmlDocument();
            xDoc.Load(FILE_NAME_POWERBLOCKS);
            XmlElement xRoot = xDoc.DocumentElement;
            PowerBlocks = new List<PowerBlock>();
            foreach (XmlElement xnode in xRoot)
            {
                PowerBlock powerBlock = new PowerBlock();



                foreach (XmlNode childnode in xnode.ChildNodes)
                {
                    if (childnode.Name == "name")
                    {
                        powerBlock.Name = childnode.InnerText;
                    }
                    if (childnode.Name == "heat")
                    {
                        powerBlock.Heat = Convert.ToInt32(childnode.InnerText);
                    }
                    if (childnode.Name == "inAir")
                    {
                        powerBlock.InAir = Convert.ToInt32(childnode.InnerText);
                    }
                    if (childnode.Name == "inWater")
                    {
                        powerBlock.InWater = Convert.ToInt32(childnode.InnerText);
                    }
                    if (childnode.Name == "limitPower")
                    {
                        powerBlock.LimitPower = Convert.ToInt32(childnode.InnerText);
                    }
                    if (childnode.Name == "minSquare")
                    {
                        powerBlock.MinSpace = Convert.ToInt32(childnode.InnerText);
                    }
                    if (childnode.Name == "price")
                    {
                        powerBlock.Price = Convert.ToDouble(childnode.InnerText);
                    }
                }
                PowerBlocks.Add(powerBlock);
            }
        }
        public void LoadPlaces()
        {
            XmlDocument xDoc = new XmlDocument();
            xDoc.Load(FILE_NAME_PLACES);
            XmlElement xRoot = xDoc.DocumentElement;
            Places = new List<Place>();
            foreach (XmlElement xnode in xRoot)
            {
                Place place = new Place();



                foreach (XmlNode childnode in xnode.ChildNodes)
                {
                    if (childnode.Name == "street")
                    {
                        place.Street = childnode.InnerText;
                    }
                    if (childnode.Name == "heat")
                    {
                        place.Heat = Convert.ToInt32(childnode.InnerText);
                    }
                    if (childnode.Name == "length")
                    {
                        place.Length = Convert.ToInt32(childnode.InnerText);
                    }
                    if (childnode.Name == "air")
                    {
                        place.Air = Convert.ToInt32(childnode.InnerText);
                    }
                    if (childnode.Name == "water")
                    {
                        place.Water = Convert.ToInt32(childnode.InnerText);
                    }
                    if (childnode.Name == "square")
                    {
                        place.Square = Convert.ToInt32(childnode.InnerText);
                    }
                    if (childnode.Name == "noice")
                    {
                        place.NoiceLevel = Convert.ToInt32(childnode.InnerText);
                    }
                }
                Places.Add(place);
            }
        }
        public void Save()
        {
            SaveRequests();
            
        }
        //private void SavePlaces()
        //{
        //    XmlDocument xDoc = new XmlDocument();
        //    xDoc.Load(FILE_NAME_REQUESTS);
        //    XmlElement xRoot = xDoc.DocumentElement;
        //    xRoot.RemoveAll();
        //    xDoc.Save(FILE_NAME_REQUESTS);
        //    foreach (var item in Requests)
        //    {
        //        // создаем новый элемент user
        //        XmlElement reqElem = xDoc.CreateElement("request");
        //        // создаем атрибут name
        //        XmlElement stationElem = xDoc.CreateElement("electricalStation");
        //        // создаем элементы company и age
        //        XmlElement placeElem = xDoc.CreateElement("place");
        //        XmlElement blockElem = xDoc.CreateElement("block");

        //        // создаем текстовые значения для элементов и атрибута
        //        XmlText stationText = xDoc.CreateTextNode(item.Station.XmlToString());
        //        XmlText placeText = xDoc.CreateTextNode(item.Place.Street);
        //        XmlText blockText = xDoc.CreateTextNode(item.Block.Name);
        //        XmlText squareText = xDoc.CreateTextNode(item.Place.Square.ToString());


        //        //добавляем узлы
        //        stationElem.AppendChild(stationText);
        //        placeElem.AppendChild(placeText);
        //        blockElem.AppendChild(blockText);
        //        reqElem.AppendChild(stationElem);
        //        reqElem.AppendChild(placeElem);
        //        reqElem.AppendChild(blockElem);
        //        xRoot.AppendChild(reqElem);
        //    }
        //    xDoc.Save(FILE_NAME_REQUESTS);
        //}
        private void SaveRequests()
        {
            XmlDocument xDoc = new XmlDocument();
            xDoc.Load(FILE_NAME_PLACES);
            XmlElement xRoot = xDoc.DocumentElement;
            xRoot.RemoveAll();
            xDoc.Save(FILE_NAME_PLACES);
            foreach (var item in Requests)
            {
            // создаем новый элемент user
                XmlElement placeElem = xDoc.CreateElement("place");
            // создаем атрибут name
                XmlElement streetElem = xDoc.CreateElement("street");
            // создаем элементы company и age
                XmlElement lengthElem = xDoc.CreateElement("length");
                XmlElement heatElem = xDoc.CreateElement("heat");
                XmlElement airElem = xDoc.CreateElement("air");

                XmlElement waterElem = xDoc.CreateElement("water");
                XmlElement squareElem = xDoc.CreateElement("square");
                XmlElement noiceElem = xDoc.CreateElement("noice");




                // создаем текстовые значения для элементов и атрибута
                
                XmlText streetText = xDoc.CreateTextNode(item.Place.Street);
                XmlText lengthText = xDoc.CreateTextNode(item.Place.Length.ToString());
                XmlText heatText = xDoc.CreateTextNode(item.Place.Heat.ToString());
                XmlText airText = xDoc.CreateTextNode(item.Place.Air.ToString());
                XmlText waterText = xDoc.CreateTextNode(item.Place.Water.ToString());
                XmlText squareText = xDoc.CreateTextNode(item.Place.Square.ToString());
                XmlText noiceText = xDoc.CreateTextNode(item.Place.NoiceLevel.ToString());


                //добавляем узлы
                streetElem.AppendChild(streetText);
                lengthElem.AppendChild(lengthText);
                heatElem.AppendChild(heatText);
                airElem.AppendChild(airText);
                waterElem.AppendChild(waterText);
                squareElem.AppendChild(squareText);
                noiceElem.AppendChild(noiceText);

                placeElem.AppendChild(streetElem);
                placeElem.AppendChild(lengthElem);
                placeElem.AppendChild(heatElem);
                placeElem.AppendChild(airElem);
                placeElem.AppendChild(waterElem);
                placeElem.AppendChild(squareElem);
                placeElem.AppendChild(noiceElem);

                xRoot.AppendChild(placeElem);
            }
            xDoc.Save(FILE_NAME_PLACES);
        }
        
    }
}
