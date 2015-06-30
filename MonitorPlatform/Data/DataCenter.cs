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
using Utility;
using System.Collections.ObjectModel;

namespace MonitorPlatform.Data
{
    public class DataCenter
    {
        public delegate void UpdateTrainLocation();
        public event UpdateTrainLocation UpdateTrainLocationEvent;
        public const string taskGuid = "91457eae-f7fc-42b4-a64b-f9825336dea7";
        public delegate void UpdateUIDate(string data);
        public delegate void UpdateUIDateWithLine(string data, int line);
        public delegate void UpdateUIDateWithLineDetail(string data, int line, string staguid);
        private static WebInvoker proxy;
        private Window win;

        private static DataCenter instance;

        public enum QueryType
        { 
            Month =0,
            Quarter,
            Year,
            All,
            Addup
        }

        public static DataCenter Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new DataCenter();
                }
                return instance;
            }
        }

        public string LoginUser
        {
            get;
            set;
        }

        public DateTime SelectTime
        {
            get;
            set;
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

        public void RaiseTrainLocationUpdate()
        {
            if (UpdateTrainLocationEvent != null)
            {
                UpdateTrainLocationEvent();
            }
        }

        public bool Login(string username, string password)
        {
            bool islogin = false;
            try
            {
                AutoResetEvent eve = new AutoResetEvent(false);
                //"test1", "1", 
                proxy.Login(username, password, "abcd", (result) =>
                {
                    if (result.Status != 0)
                    {
                        LogCenter.LogMessage("登录失败! username:" + username);
                        islogin = false;
                    }
                    else
                    {
                        LoginUser = username;
                        islogin = true;
                    }
                    eve.Set();

                });

                eve.WaitOne();
            }
            catch (Exception exp)
            {
                LogCenter.Log(exp);
            }
            return islogin;
        }

        public void UpdateBoss()
        {
            XElement tfNode = XmlNodeHelper.GetDocumentNode(taskGuid, "MainPageInitInfo");
            string xmlTransform = tfNode.ToString();

            proxy.TransformData(taskGuid, xmlTransform, (result) =>
            {
                win.Dispatcher.Invoke(new UpdateUIDate(UpdateBossUIData), result.Data);
            });
        }

        public void UpdateTrafficLeft()
        {
            string taskGuid = "91457eae-f7fc-42b4-a64b-f9825336dea7";
            XElement tfNode = XmlNodeHelper.GetDocumentNode("91457eae-f7fc-42b4-a64b-f9825336dea7", "RailAFCFifthQueryInfo");
            string xmlTransform = tfNode.ToString();

            proxy.TransformData(taskGuid, xmlTransform, (result) =>
            {
                if (result.Status == 0)
                {
                    win.Dispatcher.Invoke(new UpdateUIDate(UpdateTrafficLeftUIData), result.Data);
                }
                else
                {
                    LogCenter.LogMessage("UpdateTrafficLeftUIData fail." + result.ErrMessage);
                }
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
                if (result.Status == 0)
                {
                    win.Dispatcher.Invoke(new UpdateUIDate(UpdateTrainCenterUIData), result.Data);
                }
                else
                {
                    LogCenter.LogMessage("UpdateTrafficCenter fail." + result.ErrMessage);
                }
            });
        }

        public void UpdateTrafficRight(DateTime time, int lineid)
        {
            string taskGuid = "91457eae-f7fc-42b4-a64b-f9825336dea7";
            XElement tfNode = XmlNodeHelper.GetDocumentNode("91457eae-f7fc-42b4-a64b-f9825336dea7", "StationAFC_Day_Info");
            XmlNodeHelper.AddSubValue(tfNode, "QueryDate", "TEXT", time.ToString("yyyy-MM-dd"));
            if (lineid == 0)
            {
                XmlNodeHelper.AddSubValue(tfNode, "LineGuid", "TEXT", "4ce1f6eb-5334-419b-bea3-a20e16b7e205");
            }
            else
            {
                XmlNodeHelper.AddSubValue(tfNode, "LineGuid", "TEXT", "60f2d09f-1321-4111-955d-66a404d80fcd");
            }
            string xmlTransform = tfNode.ToString();

            proxy.TransformData(taskGuid, xmlTransform, (result) =>
            {
                if (result.Status == 0)
                {
                    win.Dispatcher.Invoke(new UpdateUIDateWithLine(UpdateTrafficRightUIData), result.Data, lineid);
                }
                else
                {
                    LogCenter.LogMessage("UpdateTrafficRight fail. " + result.ErrMessage);
                }
            });
        }

        public void UpdateTrafficRight_DetailStation(DateTime time, int lineid, string stationid)
        {
            string taskGuid = "91457eae-f7fc-42b4-a64b-f9825336dea7";
            XElement tfNode = XmlNodeHelper.GetDocumentNode("91457eae-f7fc-42b4-a64b-f9825336dea7", "StationAFC_Hour_Info");
            XmlNodeHelper.AddSubValue(tfNode, "QueryDate", "TEXT", time.ToString("yyyy-MM-dd"));
            XmlNodeHelper.AddSubValue(tfNode, "StationGuid", "TEXT", stationid);

            string xmlTransform = tfNode.ToString();

            proxy.TransformData(taskGuid, xmlTransform, (result) =>
            {
                if (result.Status == 0)
                {
                    win.Dispatcher.Invoke(new UpdateUIDateWithLineDetail(UpdateTrafficRight_DetailStationUIData), result.Data, lineid, stationid);
                }
                else
                {
                    LogCenter.LogMessage("UpdateTrafficRight fail. " + result.ErrMessage);
                }
            });
        }


        public void UpdaeTrainLocationLeft()
        {
            string taskGuid = "91457eae-f7fc-42b4-a64b-f9825336dea7";
            XElement tfNode = XmlNodeHelper.GetDocumentNode("91457eae-f7fc-42b4-a64b-f9825336dea7", "RailInStationTimeQueryInfo");
            string xmlTransform = tfNode.ToString();

            proxy.TransformData(taskGuid, xmlTransform, (result) =>
            {
                if (result.Status == 0)
                {
                    win.Dispatcher.Invoke(new UpdateUIDate(UpdateLocationLeftUIData), result.Data);
                }
                else
                {
                    LogCenter.LogMessage("TrainLocationLeft fail. " + result.ErrMessage);
                }
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
                if (result.Status == 0)
                {
                    win.Dispatcher.Invoke(new UpdateUIDateWithLine(UpdateEquipmentLeftLineUIData), result.Data, 0);
                }
                else
                {
                    LogCenter.LogMessage("UpdateEquipmentLeft fail. " + result.ErrMessage);
                }
            });



            tfNode = XmlNodeHelper.GetDocumentNode("91457eae-f7fc-42b4-a64b-f9825336dea7", "EquipLineQueryInfo");
            XmlNodeHelper.AddSubValue(tfNode, "LineGuid", "TEXT", "60f2d09f-1321-4111-955d-66a404d80fcd");
            xmlTransform = tfNode.ToString();

            proxy.TransformData(taskGuid, xmlTransform, (result) =>
            {
                if (result.Status == 0)
                {
                    win.Dispatcher.Invoke(new UpdateUIDateWithLine(UpdateEquipmentLeftLineUIData), result.Data, 1);
                }
                else
                {
                    LogCenter.LogMessage("UpdateEquipmentLeft fail. " + result.ErrMessage);
                }
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
                if (result.Status == 0)
                {
                    win.Dispatcher.Invoke(new UpdateUIDateWithLine(UpdateEquipmentCenterLineUIData), result.Data, 0);
                }
                else
                {
                    LogCenter.LogMessage("UpdateEquipmentCenter fail. " + result.ErrMessage);
                }
            });



            tfNode = XmlNodeHelper.GetDocumentNode("91457eae-f7fc-42b4-a64b-f9825336dea7", "EquipStationQueryInfo");
            XmlNodeHelper.AddSubValue(tfNode, "LineGuid", "TEXT", "60f2d09f-1321-4111-955d-66a404d80fcd");
            xmlTransform = tfNode.ToString();

            proxy.TransformData(taskGuid, xmlTransform, (result) =>
            {
                if (result.Status == 0)
                {
                    win.Dispatcher.Invoke(new UpdateUIDateWithLine(UpdateEquipmentCenterLineUIData), result.Data, 1);
                }
                else
                {
                    LogCenter.LogMessage("UpdateEquipmentCenter fail. " + result.ErrMessage);
                }
            });


        }

        public void UpdateEquipmentDetailCenter(string statguid, string euiptypes, int lineid)
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
                if (result.Status == 0)
                {
                    win.Dispatcher.Invoke(new UpdateUIDateWithLineDetail(UpdateEquipmentCenterLineDetailUIData), result.Data, lineid, statguid);
                }
                else
                {
                    LogCenter.LogMessage("UpdateEquipmentDetailCenter fail. " + result.ErrMessage);
                }
            });
        }

        public void UpdateCameraInfo()
        {
            string taskGuid = "91457eae-f7fc-42b4-a64b-f9825336dea7";
            XElement tfNode = XmlNodeHelper.GetDocumentNode("91457eae-f7fc-42b4-a64b-f9825336dea7", "RailCameraQueryInfo");
            //1号线 4ce1f6eb-5334-419b-bea3-a20e16b7e205
            //2号线 60f2d09f-1321-4111-955d-66a404d80fcd
            string xmlTransform = tfNode.ToString();

            proxy.TransformData(taskGuid, xmlTransform, (result) =>
            {
                if (result.Status == 0)
                {
                    win.Dispatcher.Invoke(new UpdateUIDate(UpdateCameraUIData), result.Data);
                }
                else
                {
                    LogCenter.LogMessage("UpdateCameraInfo fail. " + result.ErrMessage);
                }
            });

        }

        private void UpdateBossStation(XmlNode firstrail, SubLine line1, bool isfirst)
        {

            line1.InNumber = float.Parse(firstrail.SelectSingleNode("PassIn").SafeInnerText());
            line1.OutNumber = float.Parse(firstrail.SelectSingleNode("PassOut").SafeInnerText());
            line1.TotalNumber = float.Parse(firstrail.SelectSingleNode("PassTotal").SafeInnerText());

            line1.TotalRate.Add(new InOutTotal()
            {
                Name = "1",

                TotalRate = (line1.TotalNumber == 0 ? 0 : (int)((line1.InNumber / line1.TotalNumber) * 100))
            });
            foreach (XmlNode sta in firstrail.SelectNodes("Station"))
            {
                int stano = 0;
                if (isfirst)
                {
                    stano = sta.SelectSingleNode("StationNo").SafeInnerInt();
                }
                else
                {
                    stano = 23 - sta.SelectSingleNode("StationNo").SafeInnerInt();
                }
                Station station = line1.Stations[stano - 1];
                station.InNumber = sta.SelectSingleNode("PassIn").SafeInnerInt();
                station.OutNumber = sta.SelectSingleNode("PassOut").SafeInnerInt();



            }
            //line1.Troubles.Clear();
            //foreach (XmlNode equip in firstrail.SelectNodes("Equipment"))
            //{
            //    line1.Troubles.Add(new TroubleStatusSum()
            //    {
            //        EquipmentType = equip.SelectSingleNode("Kind").SafeInnerText(),
            //        Number = equip.SelectSingleNode("Count").SafeInnerInt(),
            //        BadNumber = equip.SelectSingleNode("WarnCount").SafeInnerInt()
            //    });
            //}



        }

        private void UpdateTraficStation(XmlNode linenode, SubLine line1)
        {
            XmlNodeList passnodes = linenode.SelectNodes("PassInfo");
            //line1.LinePersonrates.Clear();
            foreach (PersonsRateSum p in line1.LinePersonrates)
            {
                p.InNumber = 0;
                p.Outnumber = 0;
            }


            foreach (XmlNode passnode in passnodes)
            {
                string Time = passnode.SelectSingleNode("HourTime").SafeInnerText();
                PersonsRateSum p = line1.LinePersonrates.SingleOrDefault(x => x.Time == Time);
                if (p != null)
                {
                    int k = line1.LinePersonrates.IndexOf(p);
                    line1.LinePersonrates[k].InNumber = passnode.SelectSingleNode("PassIn").SafeInnerInt();
                    line1.LinePersonrates[k].Outnumber = passnode.SelectSingleNode("PassOut").SafeInnerInt();
                }

            }
            ObservableCollection<PersonsRateSum> temp = line1.LinePersonrates;
            line1.LinePersonrates = null;
            line1.LinePersonrates = temp;
        }

        private void UpdateTrafficLeftStationUIData(XmlNode linenode, SubLine line1, bool isfirst)
        {
            foreach (XmlNode sta in linenode.SelectNodes("Station"))
            {
                int stano = 0;
                if (isfirst)
                {
                    stano = sta.SelectSingleNode("StationNo").SafeInnerInt();
                }
                else
                {
                    stano = 23 - sta.SelectSingleNode("StationNo").SafeInnerInt();
                }
                Station station = line1.Stations[stano - 1];
                // CrowdStatus +1
                station.Status = sta.SelectSingleNode("CrowdStatus").SafeInnerInt() + 1;

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



                if (node.SelectSingleNode("Name").SafeInnerText() == "1号线")
                {


                    if (node.SelectSingleNode("Direction").SafeInnerText() == "往钟南街方向")
                    {
                        line1.UptrainCount = node.SelectSingleNode("TrainCount").SafeInnerInt();
                        line1.UptrainStartTime = node.SelectSingleNode("StartTime").SafeInnerText();
                        line1.UptrainEndTime = node.SelectSingleNode("EndTime").SafeInnerText();

                        XmlNodeList trainnodes = node.SelectNodes("Train");
                        if (trainnodes != null)
                        {
                            foreach (XmlNode trainnode in trainnodes)
                            {
                                Train train = new Train();
                                train.TrainNumber = trainnode.SelectSingleNode("TrainNo").SafeInnerText();
                                train.Location = double.Parse(trainnode.SelectSingleNode("CurrentStationNo").SafeInnerText());
                                train.IsDown = false;
                                line1.Trains.Add(train);
                            }

                        }

                    }
                    else
                    {
                        line1.DowntrainStartTime = node.SelectSingleNode("StartTime").SafeInnerText();
                        line1.DowntrainEndTime = node.SelectSingleNode("EndTime").SafeInnerText();
                        line1.DowntrainCount = node.SelectSingleNode("TrainCount").SafeInnerInt();
                        XmlNodeList trainnodes = node.SelectNodes("Train");
                        if (trainnodes != null)
                        {
                            foreach (XmlNode trainnode in trainnodes)
                            {
                                Train train = new Train();
                                train.TrainNumber = trainnode.SelectSingleNode("TrainNo").SafeInnerText();
                                train.Location = double.Parse(trainnode.SelectSingleNode("CurrentStationNo").SafeInnerText());
                                train.IsDown = true;
                                line1.Trains.Add(train);
                            }

                        }
                    }

                }
                else
                {
                    if (node.SelectSingleNode("Direction").SafeInnerText() == "往宝带桥南方向")
                    {
                        line2.UptrainCount = node.SelectSingleNode("TrainCount").SafeInnerInt();
                        line2.UptrainStartTime = node.SelectSingleNode("StartTime").SafeInnerText();
                        line2.UptrainEndTime = node.SelectSingleNode("EndTime").SafeInnerText();

                        XmlNodeList trainnodes = node.SelectNodes("Train");
                        if (trainnodes != null)
                        {
                            foreach (XmlNode trainnode in trainnodes)
                            {
                                Train train = new Train();
                                train.TrainNumber = trainnode.SelectSingleNode("TrainNo").SafeInnerText();
                                train.Location = double.Parse(trainnode.SelectSingleNode("CurrentStationNo").SafeInnerText());
                                train.IsDown = false;
                                line2.Trains.Add(train);
                            }

                        }
                    }
                    else
                    {
                        line2.DowntrainStartTime = node.SelectSingleNode("StartTime").SafeInnerText();
                        line2.DowntrainEndTime = node.SelectSingleNode("EndTime").SafeInnerText();
                        line2.DowntrainCount = node.SelectSingleNode("TrainCount").SafeInnerInt();
                        XmlNodeList trainnodes = node.SelectNodes("Train");
                        if (trainnodes != null)
                        {
                            foreach (XmlNode trainnode in trainnodes)
                            {
                                Train train = new Train();
                                train.TrainNumber = trainnode.SelectSingleNode("TrainNo").SafeInnerText();
                                train.Location = double.Parse(trainnode.SelectSingleNode("CurrentStationNo").SafeInnerText());
                                train.IsDown = true;
                                line2.Trains.Add(train);
                            }

                        }
                    }

                }
            }

            nodes = doc.SelectNodes("/Document/RailLine");
            UpdateBossStation(nodes[0], line1, true);
            UpdateBossStation(nodes[1], line2, false);

            line1.TrainIsWarn = doc.SelectSingleNode("/Document/TrainIsWarn").SafeInnerText() == "1";
            line1.PassIsWarn = doc.SelectSingleNode("/Document/PassIsWarn").SafeInnerText() == "1";
            line1.EquipIsWarn = doc.SelectSingleNode("/Document/EquipIsWarn").SafeInnerText() == "1";
            RaiseTrainLocationUpdate();
        }

        private void UpdateTrafficLeftUIData(string data)
        {
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(data);
            SubLine line1 = MonitorDataModel.Instance().SubWayLines[0];
            SubLine line2 = MonitorDataModel.Instance().SubWayLines[1];


            XmlNodeList nodes = doc.SelectNodes("/Document/RailLine");

            foreach (XmlNode node in nodes)
            {
                //Line 1
                if (node.SelectSingleNode("Guid").SafeInnerText() == "4ce1f6eb-5334-419b-bea3-a20e16b7e205")
                {
                    line1.CrowdStation = node.SelectSingleNode("CrowdStation").SafeInnerInt();
                    line1.BlockStation = node.SelectSingleNode("BlockStation").SafeInnerInt();
                    UpdateTrafficLeftStationUIData(node, line1, true);

                }
                else
                {
                    line2.CrowdStation = node.SelectSingleNode("CrowdStation").SafeInnerInt();
                    line2.BlockStation = node.SelectSingleNode("BlockStation").SafeInnerInt();
                    UpdateTrafficLeftStationUIData(node, line2, false);
                }
            }

        }

        private void UpdateTrainCenterUIData(string data)
        {
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(data);
            SubLine line1 = MonitorDataModel.Instance().SubWayLines[0];
            SubLine line2 = MonitorDataModel.Instance().SubWayLines[1];


            XmlNodeList nodes = doc.SelectNodes("/Document/RailLine");

            int line1Total = 0;
            int line2Total = 0;
            foreach (XmlNode node in nodes)
            {
                if (node.SelectSingleNode("Name").SafeInnerText() == "1号线")
                {
                    line1Total = node.SelectSingleNode("PassTotal").SafeInnerInt();
                    line1.History_totalnumber = line1Total;
                    UpdateTraficStation(node, line1);

                }
                else
                {
                    line2Total = node.SelectSingleNode("PassTotal").SafeInnerInt();
                    line2.History_totalnumber = line2Total;
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

        private void UpdateTrafficRightUIData(string data, int lineid)
        {
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(data);
            SubLine line1 = MonitorDataModel.Instance().SubWayLines[lineid];
            line1.History_Stations.Clear();
            XmlNodeList nodes = doc.SelectNodes("/Document/Station");

            foreach (XmlNode node in nodes)
            {
                HistoryStation his = new HistoryStation();
                //GUID
                his.StaGUID = node.SelectSingleNode("GUID").SafeInnerText();
                his.Name = node.SelectSingleNode("Name").SafeInnerText();
                his.InNumber = node.SelectSingleNode("PassIn").SafeInnerInt();
                his.OutNumber = node.SelectSingleNode("PassOut").SafeInnerInt();
                his.TotalNumber = node.SelectSingleNode("PassTotal").SafeInnerInt();
                his.UpBeginTime = DateTime.Parse(node.SelectSingleNode("UStartTime").SafeInnerText());
                his.UpEndTime = DateTime.Parse(node.SelectSingleNode("UEndTime").SafeInnerText());
                his.DownBeginTime = DateTime.Parse(node.SelectSingleNode("DStartTime").SafeInnerText());
                his.DownEndTime = DateTime.Parse(node.SelectSingleNode("DEndTime").SafeInnerText());
                his.TrafficJam = node.SelectSingleNode("CrowdCount").SafeInnerInt();



                his.StationInOut.Add(new StationInOut()
                {
                    Name = "进站人数",
                    Number = his.InNumber
                });
                his.StationInOut.Add(new StationInOut()
                {
                    Name = "出站人数",
                    Number = his.OutNumber
                });
                //Martin 伪造数据 Personrates,分时段进出站客流
                for (int i = 6; i <= 22; i++)
                {
                    his.Personrates.Add(new PersonsRateSum() { Time = i.ToString("D2"), InNumber = 0, Outnumber = 0 });
                }
                line1.History_Stations.Add(his);
            }
        }


        private void UpdateTrafficRight_DetailStationUIData(string data, int lineid, string guid)
        {
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(data);
            SubLine line1 = MonitorDataModel.Instance().SubWayLines[lineid];


            XmlNodeList passnodes = doc.SelectNodes("/Document/PassInfo");
            HistoryStation s = line1.History_Stations.SingleOrDefault(x => x.StaGUID == guid);

            if (s != null)
            {
                foreach (PersonsRateSum p in s.Personrates)
                {
                    p.InNumber = 0;
                    p.Outnumber = 0;
                }
                foreach (XmlNode passnode in passnodes)
                {
                    string Time = passnode.SelectSingleNode("HourTime").SafeInnerText();
                    PersonsRateSum p = s.Personrates.SingleOrDefault(x => x.Time == Time);
                    if (p != null)
                    {
                        int k = s.Personrates.IndexOf(p);
                        s.Personrates[k].InNumber = passnode.SelectSingleNode("PassIn").SafeInnerInt();
                        s.Personrates[k].Outnumber = passnode.SelectSingleNode("PassOut").SafeInnerInt();
                    }

                }

                PersonsRateSum notify = new PersonsRateSum();
                s.Personrates.Add(notify);
                s.Personrates.Remove(notify);
            }

        }

        private void UpdateEquipmentLeftLineUIData(string data, int lineid)
        {
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(data);
            SubLine line1 = MonitorDataModel.Instance().SubWayLines[lineid];
            //SubLine line2 = MonitorDataModel.Instance().SubWayLines[1];
            line1.StaTroubles.Clear();
            //line1.History_Stations.Clear();
            XmlNodeList nodes = doc.SelectNodes("/Document/RailLine/EquipInfo");
            line1.Troubles.Clear();
            foreach (XmlNode node in nodes)
            {
                TroubleStatusSum sta = new TroubleStatusSum();

                sta.EquipmentType = node.SelectSingleNode("Kind").SafeInnerText();
                sta.Number = node.SelectSingleNode("Count").SafeInnerInt();
                sta.BadNumber = node.SelectSingleNode("WarnCount").SafeInnerInt();
                //Martin Add 1 for display
                if (sta.BadNumber == 0)
                {
                    sta.BadNumber = 1;
                }
                line1.Troubles.Add(sta);
            }

            nodes = doc.SelectNodes("/Document/RailLine/Station");

            foreach (XmlNode node in nodes)
            {
                StationTroubleStatus sta = new StationTroubleStatus();
                string name = node.SelectSingleNode("Name").SafeInnerText();
                Station s = line1.Stations.SingleOrDefault(x => x.Name == name);
                if (s != null)
                {
                    int index = line1.Stations.IndexOf(s);
                    sta.ID = index;
                    sta.WarnCount = node.SelectSingleNode("WarnCount").SafeInnerInt();
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
                if (node.SelectSingleNode("Kind").SafeInnerText() == "110kv")
                {
                    volA.Val_110 = node.SelectSingleNode("VoltageA").SafeInnerInt();
                    volB.Val_110 = node.SelectSingleNode("VoltageB").SafeInnerInt();
                    volC.Val_110 = node.SelectSingleNode("VoltageC").SafeInnerInt();
                }
                else
                {
                    volA.Val_35 = node.SelectSingleNode("VoltageA").SafeInnerInt();
                    volB.Val_35 = node.SelectSingleNode("VoltageB").SafeInnerInt();
                    volC.Val_35 = node.SelectSingleNode("VoltageC").SafeInnerInt();
                }
            }

            volA.CabTemp = doc.SelectSingleNode("/Document/RailLine/CableTemp/TempA").SafeInnerInt();
            volB.CabTemp = doc.SelectSingleNode("/Document/RailLine/CableTemp/TempB").SafeInnerInt();
            volC.CabTemp = doc.SelectSingleNode("/Document/RailLine/CableTemp/TempC").SafeInnerInt();

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

                string name = node.SelectSingleNode("Name").SafeInnerText();
                Station s = line1.Stations.SingleOrDefault(x => x.Name == name);
                if (s != null)
                {
                    s.BrokenNumber = node.SelectSingleNode("WarnCount").SafeInnerInt();
                    s.StaGUID = node.SelectSingleNode("GUID").SafeInnerText();
                }
            }

        }


        private void UpdateEquipmentCenterLineDetailUIData(string data, int lineid, string guid)
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
                    eqi.Name = node.SelectSingleNode("PSignal").SafeInnerText();
                    eqi.EquipmentType = node.SelectSingleNode("PType").SafeInnerText();
                    eqi.Owner = node.SelectSingleNode("Name").SafeInnerText();
                    eqi.Location = node.SelectSingleNode("PLocation").SafeInnerText();
                    eqi.WaringLevel = node.SelectSingleNode("AlarmGrade").SafeInnerText() == "1" ? "警告" : "";
                    eqi.Status = node.SelectSingleNode("Status").SafeInnerText() == "1" ? "异常" : "正常";
                    s.Equipments.Add(eqi);
                }
            }


        }


        private void UpdateCameraUIData(string data)
        {
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(data);
            //SubLine line1 = MonitorDataModel.Instance().SubWayLines[lineid];


            XmlNodeList nodes = doc.SelectNodes("/Document/RailLine");

            foreach (XmlNode node in nodes)
            {
                SubLine line;
                string name = node.SelectSingleNode("Name").SafeInnerText();
                if (name == "1号线")
                {
                    line = MonitorDataModel.Instance().SubWayLines[0];
                }
                else
                {
                    line = MonitorDataModel.Instance().SubWayLines[1];
                }
                int linecamecount = 0;
                foreach (XmlNode stationnode in node.SelectNodes("Station"))
                {
                    string stationname = stationnode.SelectSingleNode("Name").SafeInnerText();
                    Station s = line.Stations.SingleOrDefault(x => x.Name == stationname);
                    if (s != null)
                    {
                        s.Cameras.Clear();
                        s.CameraNumber = 0;
                        XmlNodeList cameralist = stationnode.SelectNodes("Camera");
                        if (cameralist != null)
                        {
                            foreach (XmlNode camenode in cameralist)
                            {
                                Camera camera = new Camera();
                                camera.Name = camenode.SelectSingleNode("Name").SafeInnerText();
                                camera.Location = camenode.SelectSingleNode("Location").SafeInnerText();
                                camera.Code = camenode.SelectSingleNode("Code").SafeInnerText();
                                camera.Type = camenode.SelectSingleNode("Type").SafeInnerText();
                                camera.APIID = camenode.SelectSingleNode("APIID").SafeInnerText();
                                camera.APIIP = camenode.SelectSingleNode("APIIP").SafeInnerText();
                                camera.Remark = camenode.SelectSingleNode("Remark").SafeInnerText();
                                s.Cameras.Add(camera);
                                s.CameraNumber++;
                                linecamecount++;
                            }
                        }
                    }
                }
                line.CameraTotalNumber = linecamecount;
            }

        }


        private void UpdateLocationLeftByLineUp(XmlNode node, SubLine line1)
        {
            XmlNodeList stationnodes = node.SelectNodes("Station");
            if (stationnodes != null)
            {
                foreach (XmlNode stanode in stationnodes)
                {
                    string name = stanode.SelectSingleNode("Name").SafeInnerText();
                    Station s = line1.Stations.SingleOrDefault(x => x.Name == name);
                    if (s != null)
                    {
                        s.UpFirstTime = stanode.SelectSingleNode("FirstTime").SafeInnerInt();
                        s.UpSecondTime = stanode.SelectSingleNode("SecondTime").SafeInnerInt();
                    }
                }
            }
        }

        private void UpdateLocationLeftByLineDown(XmlNode node, SubLine line1)
        {
            XmlNodeList stationnodes = node.SelectNodes("Station");
            if (stationnodes != null)
            {
                foreach (XmlNode stanode in stationnodes)
                {
                    string name = stanode.SelectSingleNode("Name").SafeInnerText();
                    Station s = line1.Stations.SingleOrDefault(x => x.Name == name);
                    if (s != null)
                    {
                        s.DownFirstTime = stanode.SelectSingleNode("FirstTime").SafeInnerInt();
                        s.DownSecondTime = stanode.SelectSingleNode("SecondTime").SafeInnerInt();
                    }
                }
            }
        }

        private void UpdateLocationLeftUIData(string data)
        {
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(data);
            SubLine line1 = MonitorDataModel.Instance().SubWayLines[0];
            SubLine line2 = MonitorDataModel.Instance().SubWayLines[1];
            XmlNodeList nodes = doc.SelectNodes("/Document/ServiceLine");
            foreach (XmlNode node in nodes)
            {
                if (node.SelectSingleNode("Name").SafeInnerText() == "1号线")
                {
                    if (node.SelectSingleNode("Direction").SafeInnerText() == "往钟南街方向")
                    {
                        UpdateLocationLeftByLineUp(node, line1);
                    }
                    else
                    {
                        UpdateLocationLeftByLineDown(node, line1);
                    }

                }
                else
                {
                    if (node.SelectSingleNode("Direction").SafeInnerText() == "往宝带桥南方向")
                    {
                        UpdateLocationLeftByLineUp(node, line2);
                    }
                    else
                    {
                        UpdateLocationLeftByLineDown(node, line2);
                    }

                }
            }
        }
    }
}
