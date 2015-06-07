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
        public delegate void UpdateUIDateWithLine(string data, int line);
        public delegate void UpdateUIDateWithLineDetail(string data, int line,string staguid);
        private static WebInvoker proxy;
        private Window win;

        private static DataCenter instance;

        public static DataCenter Instance
        {
            get {
                if (instance == null)
                {
                    instance = new DataCenter();
                }
                return instance;
            }
        }

        public void Inital(Window window)
        {
            win = window;
        }

        //private static string userid;
        public DataCenter()
        {
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
        
        public void UpdateTrafficCenter(DateTime time)
        {
            string taskGuid = "91457eae-f7fc-42b4-a64b-f9825336dea7";
            XElement tfNode = XmlNodeHelper.GetDocumentNode("91457eae-f7fc-42b4-a64b-f9825336dea7", "RailAFC_Day_Info");
            XmlNodeHelper.AddSubValue(tfNode, "QueryDate", "TEXT", time.ToString("yyyy-MM-dd"));
            string xmlTransform = tfNode.ToString();

            proxy.TransformData(taskGuid, xmlTransform, (result) =>
            {
                win.Dispatcher.Invoke(new UpdateUIDate(UpdateTrainCenterUIData), result.Data);
            });
        }

        public void UpdateTrafficRight(DateTime time)
        {
            string taskGuid = "91457eae-f7fc-42b4-a64b-f9825336dea7";
            XElement tfNode = XmlNodeHelper.GetDocumentNode("91457eae-f7fc-42b4-a64b-f9825336dea7", "StationAFC_Day_Info");
            XmlNodeHelper.AddSubValue(tfNode, "LineGuid", "TEXT", "68BDB0FB-1118-1901-0922-3F7D78384FF5");
            string xmlTransform = tfNode.ToString();

            proxy.TransformData(taskGuid, xmlTransform, (result) =>
            {
                win.Dispatcher.Invoke(new UpdateUIDate(UpdateTrainRightUIData), result.Data);
            });
        }

        public void UpdateEquipmentLeft()
        {
            string taskGuid = "91457eae-f7fc-42b4-a64b-f9825336dea7";
            XElement tfNode = XmlNodeHelper.GetDocumentNode("91457eae-f7fc-42b4-a64b-f9825336dea7", "EquipLineQueryInfo");
            XmlNodeHelper.AddSubValue(tfNode, "LineGuid", "TEXT", "4ce1f6eb-5334-419b-bea3-a20e16b7e205");
            string xmlTransform = tfNode.ToString();

            proxy.TransformData(taskGuid, xmlTransform, (result) =>
            {
                win.Dispatcher.Invoke(new UpdateUIDateWithLine(UpdateEquipmentLeftLineUIData), result.Data,0);
            });


             
             tfNode = XmlNodeHelper.GetDocumentNode("91457eae-f7fc-42b4-a64b-f9825336dea7", "EquipLineQueryInfo");
            XmlNodeHelper.AddSubValue(tfNode, "LineGuid", "TEXT", "60f2d09f-1321-4111-955d-66a404d80fcd");
             xmlTransform = tfNode.ToString();

            proxy.TransformData(taskGuid, xmlTransform, (result) =>
            {
                win.Dispatcher.Invoke(new UpdateUIDateWithLine(UpdateEquipmentLeftLineUIData), result.Data ,1);
            });


        }

        public void UpdateEquipmentCenter()
        {
            string taskGuid = "91457eae-f7fc-42b4-a64b-f9825336dea7";
            XElement tfNode = XmlNodeHelper.GetDocumentNode("91457eae-f7fc-42b4-a64b-f9825336dea7", "EquipStationQueryInfo");
            XmlNodeHelper.AddSubValue(tfNode, "LineGuid", "TEXT", "4ce1f6eb-5334-419b-bea3-a20e16b7e205");
            string xmlTransform = tfNode.ToString();

            proxy.TransformData(taskGuid, xmlTransform, (result) =>
            {
                win.Dispatcher.Invoke(new UpdateUIDateWithLine(UpdateEquipmentCenterLineUIData), result.Data, 0);
            });



            tfNode = XmlNodeHelper.GetDocumentNode("91457eae-f7fc-42b4-a64b-f9825336dea7", "EquipStationQueryInfo");
            XmlNodeHelper.AddSubValue(tfNode, "LineGuid", "TEXT", "60f2d09f-1321-4111-955d-66a404d80fcd");
            xmlTransform = tfNode.ToString();

            proxy.TransformData(taskGuid, xmlTransform, (result) =>
            {
                win.Dispatcher.Invoke(new UpdateUIDateWithLine(UpdateEquipmentCenterLineUIData), result.Data, 1);
            });


        }

        public void UpdateEquipmentDetailCenter(string statguid,string euiptypes, int lineid)
        {
            string taskGuid = "91457eae-f7fc-42b4-a64b-f9825336dea7";
            XElement tfNode = XmlNodeHelper.GetDocumentNode("91457eae-f7fc-42b4-a64b-f9825336dea7", "EquipStationDetailInfo");
            //XmlNodeHelper.AddSubValue(tfNode, "StationGuid", "TEXT", "bdb2720a-a08b-43bd-a8ba-af9e1bc89998");
            XmlNodeHelper.AddSubValue(tfNode, "StationGuid", "TEXT", statguid);

            XmlNodeHelper.AddSubValue(tfNode, "PType", "TEXT", euiptypes);
            XmlNodeHelper.AddSubValue(tfNode, "PageSize", "LONG", "50");
            XmlNodeHelper.AddSubValue(tfNode, "CurrentPage", "LONG", "1");
            XmlNodeHelper.AddSubValue(tfNode, "IsPage", "LONG", "False");
            //1号线 4ce1f6eb-5334-419b-bea3-a20e16b7e205
            //2号线 60f2d09f-1321-4111-955d-66a404d80fcd
            string xmlTransform = tfNode.ToString();

            proxy.TransformData(taskGuid, xmlTransform, (result) =>
            {
                win.Dispatcher.Invoke(new UpdateUIDateWithLineDetail(UpdateEquipmentCenterLineDetailUIData), result.Data, lineid, statguid);
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
                
                TotalRate = (line1.TotalNumber ==0? 0: (int)((line1.InNumber / line1.TotalNumber) * 100))
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

        private void UpdateTraficStation(XmlNode linenode, SubLine line1)
        {
            XmlNodeList passnodes = linenode.SelectNodes("PassInfo");
            line1.Personrates.Clear();
            foreach (XmlNode passnode in passnodes)
            {
                
                line1.Personrates.Add(new PersonsRateSum()
                {
                    Time = passnode.SelectSingleNode("HourTime").InnerText,
                    Number = int.Parse(passnode.SelectSingleNode("PassTotal").InnerText)
                });
            }
        }

        private void UpdateBossUIData(string data)
        {
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(data);
            SubLine line1 = MonitorDataModel.Instance().SubWayLines[0];
            SubLine line2 = MonitorDataModel.Instance().SubWayLines[1];

            line1.Trains.Clear();
            line1.UptrainCount = 0;
            line1.DowntrainCount = 0;
            line2.Trains.Clear();
            line2.UptrainCount = 0;
            line2.DowntrainCount = 0;
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
                        line1.UptrainCount++;
                    }
                    else
                    {
                        train.IsDown = true;
                        line1.DowntrainCount++;
                    }
                    line1.Trains.Add(train);
                }
                else
                {
                    if (node.SelectSingleNode("Direction").InnerText == "往宝带桥南方向")
                    {
                        train.IsDown = false;
                        line2.UptrainCount++;
                    }
                    else
                    {
                        train.IsDown = true;
                        line2.DowntrainCount++;
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


        private void UpdateTrainCenterUIData(string data)
        {
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(data);
            SubLine line1 = MonitorDataModel.Instance().SubWayLines[0];
            SubLine line2 = MonitorDataModel.Instance().SubWayLines[1];


            XmlNodeList  nodes = doc.SelectNodes("/Document/RailLine");

            int line1Total = 0;
            int line2Total = 0;
            foreach (XmlNode node in nodes)
            {
                if (node.SelectSingleNode("Name").InnerText == "1号线")
                {
                    line1Total = int.Parse(node.SelectSingleNode("PassTotal").InnerText);
                    UpdateTraficStation(node, line1);
                   
                }
                else
                {
                    line2Total = int.Parse(node.SelectSingleNode("PassTotal").InnerText);
                    UpdateTraficStation(node, line2);
                }
            }

            if (line1Total + line2Total != 0)
            {
                line1.TotalRate_history.Clear();
                int line1rate = (int)(((float)line1Total / (line1Total + line2Total)) * 100);
                line1.TotalRate_history.Add(new InOutTotal()
                {
                    Name = "1",
                    TotalRate = line1rate
                });

                line2.TotalRate_history.Clear();
                line2.TotalRate_history.Add(new InOutTotal()
                {
                    Name = "2",
                    TotalRate = 100 - line1rate
                });
            }

        }

        private void UpdateTrainRightUIData(string data)
        {
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(data);
            SubLine line1 = MonitorDataModel.Instance().SubWayLines[0];
            SubLine line2 = MonitorDataModel.Instance().SubWayLines[1];

            line1.History_Stations.Clear();
            XmlNodeList nodes = doc.SelectNodes("/Document/Station");

            foreach (XmlNode node in nodes)
            {
                HistoryStation his = new HistoryStation();
                his.Name = node.SelectSingleNode("Name").InnerText;
                his.InNumber = int.Parse(node.SelectSingleNode("PassIn").InnerText);
                his.OutNumber = int.Parse(node.SelectSingleNode("PassOut").InnerText);
                his.TotalNumber = int.Parse(node.SelectSingleNode("PassTotal").InnerText);
                his.UpBeginTime = DateTime.Parse(node.SelectSingleNode("UStartTime").InnerText);
                his.UpEndTime = DateTime.Parse(node.SelectSingleNode("UEndTime").InnerText);
                his.DownBeginTime = DateTime.Parse(node.SelectSingleNode("DStartTime").InnerText);
                his.DownEndTime = DateTime.Parse(node.SelectSingleNode("DEndTime").InnerText);
                his.TrafficJam = int.Parse(node.SelectSingleNode("CrowdCount").InnerText);
                line1.History_Stations.Add(his);
            }
        }


        private void UpdateEquipmentLeftLineUIData(string data,int lineid)
        {
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(data);
            SubLine line1 = MonitorDataModel.Instance().SubWayLines[lineid];
            //SubLine line2 = MonitorDataModel.Instance().SubWayLines[1];
            line1.StaTroubles.Clear();
            line1.History_Stations.Clear();
            XmlNodeList nodes = doc.SelectNodes("/Document/RailLine/EquipInfo");
            line1.Troubles.Clear();
            foreach (XmlNode node in nodes)
            {
                TroubleStatusSum sta = new TroubleStatusSum();
                
                sta.EquipmentType = node.SelectSingleNode("Kind").InnerText;
                sta.Number =  int.Parse( node.SelectSingleNode("Count").InnerText);
                sta.BadNumber = int.Parse( node.SelectSingleNode("WarnCount").InnerText);
                line1.Troubles.Add(sta);
            }

             nodes = doc.SelectNodes("/Document/RailLine/Station");

             foreach (XmlNode node in nodes)
             {
                 StationTroubleStatus sta = new StationTroubleStatus();
                 string name = node.SelectSingleNode("Name").InnerText;
                 Station s = line1.Stations.SingleOrDefault(x => x.Name == name);
                 if (s != null)
                 {
                     int index = line1.Stations.IndexOf(s);
                     sta.ID = index;
                     sta.WarnCount = int.Parse(node.SelectSingleNode("WarnCount").InnerText);
                     line1.StaTroubles.Add(sta);
                 }

             }

             nodes = doc.SelectNodes("/Document/RailLine/SubVoltage");


             line1.VoltageStat.Clear();
             
             VoltageStatus volA = new VoltageStatus() { Name = "A相" };
             VoltageStatus volB = new VoltageStatus() { Name = "B相" };
             VoltageStatus volC = new VoltageStatus() { Name = "C相" };

             foreach (XmlNode node in nodes)
             {
                 VoltageStatus sta = new VoltageStatus();
                 if (node.SelectSingleNode("Kind").InnerText == "110kv")
                 {
                     volA.Val_110 = int.Parse(node.SelectSingleNode("VoltageA").InnerText);
                     volB.Val_110 = int.Parse(node.SelectSingleNode("VoltageB").InnerText);
                     volC.Val_110 = int.Parse(node.SelectSingleNode("VoltageC").InnerText);
                 }
                 else
                 {
                     volA.Val_35 = int.Parse(node.SelectSingleNode("VoltageA").InnerText);
                     volB.Val_35 = int.Parse(node.SelectSingleNode("VoltageB").InnerText);
                     volC.Val_35 = int.Parse(node.SelectSingleNode("VoltageC").InnerText);
                 }
             }

             volA.CabTemp = int.Parse(doc.SelectSingleNode("/Document/RailLine/CableTemp/TempA").InnerText);
             volB.CabTemp = int.Parse(doc.SelectSingleNode("/Document/RailLine/CableTemp/TempB").InnerText);
             volC.CabTemp = int.Parse(doc.SelectSingleNode("/Document/RailLine/CableTemp/TempC").InnerText);

             line1.VoltageStat.Add(volA);
             line1.VoltageStat.Add(volB);
             line1.VoltageStat.Add(volC);
        }

        private void UpdateEquipmentCenterLineUIData(string data, int lineid)
        {
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(data);
            SubLine line1 = MonitorDataModel.Instance().SubWayLines[lineid];
           

            XmlNodeList nodes = doc.SelectNodes("/Document/RailLine/Station");

            foreach (XmlNode node in nodes)
            {
               
                string name = node.SelectSingleNode("Name").InnerText;
                Station s = line1.Stations.SingleOrDefault(x => x.Name == name);
                if (s != null)
                {
                    s.BrokenNumber = int.Parse(node.SelectSingleNode("WarnCount").InnerText);
                    s.StaGUID = node.SelectSingleNode("GUID").InnerText;
                }
            }
           
        }


        private void UpdateEquipmentCenterLineDetailUIData(string data,int lineid,string guid)
        {
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(data);
            SubLine line1 = MonitorDataModel.Instance().SubWayLines[lineid];


            XmlNodeList nodes = doc.SelectNodes("/Document/Equipment");
            Station s = line1.Stations.SingleOrDefault(x => x.StaGUID == guid);
            if (s != null)
            {
                s.Equipments.Clear();
                foreach (XmlNode node in nodes)
                {
                    Equipment eqi = new Equipment();
                    eqi.Name = node.SelectSingleNode("PSignal").InnerText;
                    eqi.EquipmentType = node.SelectSingleNode("PType").InnerText;
                    eqi.Owner = node.SelectSingleNode("Name").InnerText;
                    eqi.Location = node.SelectSingleNode("PLocation").InnerText;
                    eqi.WaringLevel = node.SelectSingleNode("AlarmGrade").InnerText == "1" ? "警告" : "";
                    eqi.Status = node.SelectSingleNode("Status").InnerText == "1" ? "异常" : "";
                    s.Equipments.Add(eqi);
                }
            }
          

        }
    }
}
