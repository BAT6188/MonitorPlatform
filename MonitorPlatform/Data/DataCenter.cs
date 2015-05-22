using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZNJT.WebProxy;
using System.Configuration;
using System.Xml.Linq;
using System.Xml.XPath;
using System.Xml;
using MonitorPlatform.ViewModel;
using System.Threading;
using System.Windows;

namespace MonitorPlatform.Data
{
    public class DataCenter
    {
        public const string taskGuid = "91457eae-f7fc-42b4-a64b-f9825336dea7";
        public delegate void UpdateUIDate(string data);
        private static WebInvoker proxy;
        private Window win;
        //private static string userid;
        public DataCenter(Window window)
        {
            win = window;
            string webUrl = string.Empty;
            if (ConfigurationManager.AppSettings.AllKeys.Contains("WebUrl"))
            {
                webUrl = ConfigurationManager.AppSettings["WebUrl"];
            }
            proxy = new WebInvoker(webUrl);
          
        }

        public void Login()
        {
            AutoResetEvent eve = new AutoResetEvent(false);
            
            proxy.Login("test1", "1", "abcd", (result) =>
            {
                eve.Set();
                string c = result.Data;
            });

            eve.WaitOne();
        }

        
        
        
        public  void UpdateBoss()
        {
            XElement tfNode = XmlNodeHelper.GetDocumentNode(taskGuid, "MainPageInitInfo");
            string xmlTransform = tfNode.ToString();
          
            proxy.TransformData(taskGuid, xmlTransform, (result) =>
            {
                win.Dispatcher.Invoke(new UpdateUIDate(UpdateBossUIData), result.Data);
            });
        }



        private void UpdateBossStation(XmlNode firstrail, SubLine line1, bool isfirst)
        {

            line1.InNumber = float.Parse(firstrail.SelectSingleNode("PassIn").InnerText);
            line1.OutNumber = float.Parse(firstrail.SelectSingleNode("PassOut").InnerText);
            line1.TotalNumber = float.Parse(firstrail.SelectSingleNode("PassTotal").InnerText);

            line1.TotalRate.Add(new InOutTotal()
            {
                Name = "1",
                TotalRate = (int)((line1.InNumber / line1.TotalNumber) * 100)
            });
            foreach (XmlNode sta in firstrail.SelectNodes("Station"))
            {
                int stano = 0;
                if (isfirst)
                {
                    stano = int.Parse(sta.SelectSingleNode("StationNo").InnerText);
                }
                else
                {
                    stano = 23 - int.Parse(sta.SelectSingleNode("StationNo").InnerText);
                }
                Station station = line1.Stations[stano - 1];
                station.InNumber = int.Parse(firstrail.SelectSingleNode("PassIn").InnerText);
                station.OutNumber = int.Parse(firstrail.SelectSingleNode("PassOut").InnerText);
            }
            XmlNode equip = firstrail.SelectSingleNode("Equipment");
            line1.Troubles.Clear();
            line1.Troubles.Add(new TroubleStatusSum()
            {
                EquipmentType = equip.SelectSingleNode("Kind").InnerText,
                Number = int.Parse(equip.SelectSingleNode("Count").InnerText),
                BadNumber = int.Parse(equip.SelectSingleNode("WarnCount").InnerText)
            });
        }
        private void UpdateBossUIData(string data)
        {
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(data);
            SubLine line1 = MonitorDataModel.Instance().SubWayLines[0];
            SubLine line2 = MonitorDataModel.Instance().SubWayLines[1];

            line1.Trains.Clear();
            line2.Trains.Clear();
            XmlNodeList nodes = doc.SelectNodes("/Document/ServiceLine");
            foreach (XmlNode node in nodes)
            {
                Train train = new Train()
                {
                    Location = double.Parse(node.SelectSingleNode("Train/CurrentStationNo").InnerText),
                    TrainCount = int.Parse(node.SelectSingleNode("TrainCount").InnerText)
                };
                if (node.SelectSingleNode("Name").InnerText == "1号线")
                {
                    if (node.SelectSingleNode("Direction").InnerText == "往钟南街方向")
                    {
                        train.IsDown = false;
                    }
                    else
                    {
                        train.IsDown = true;
                    }
                    line1.Trains.Add(train);
                }
                else
                {
                    if (node.SelectSingleNode("Direction").InnerText == "往宝带桥南方向")
                    {
                        train.IsDown = false;
                    }
                    else
                    {
                        train.IsDown = true;
                    }
                    line2.Trains.Add(train);
                }
            }

            nodes = doc.SelectNodes("/Document/RailLine");
            UpdateBossStation(nodes[0], line1, true);
            UpdateBossStation(nodes[1], line2, false);

            line1.TrainIsWarn = doc.SelectSingleNode("/Document/TrainIsWarn").InnerText == "1";
            line1.PassIsWarn = doc.SelectSingleNode("/Document/PassIsWarn").InnerText == "1";
            line1.EquipIsWarn = doc.SelectSingleNode("/Document/EquipIsWarn").InnerText == "1";

        }
    }
}
