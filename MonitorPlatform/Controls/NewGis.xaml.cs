using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml;
using Colligate;
using System.Diagnostics;

namespace MonitorPlatform.Controls
{
    public delegate void DelSetData_CallBack(string UserID, string TaskGuid,string DataGuid, string DataType, XmlNode xmlData);
    public delegate int DelSetData(string UserID, string TaskGuid, string DataGuid, string DataType,XmlNode node);
    public delegate XmlNode DelGetData(string UserID,string TaskGuid,string DataGuid,string DataType);
    /// <summary>
    /// NewGis.xaml 的交互逻辑
    /// </summary>
    public partial class NewGis : UserControl
    {
        private const string TaskGuid = "FF17D1B3-85C7-40F4-8CDC-73DC57CD29BC";
        private string PopupGuidFlag = "";
        private string SetPointGuid = "7FE46C81-7E7B-467F-B303-5351C30CC691";

        /// <summary>
        /// 提供给外部的回调函数
        /// </summary>
        public event DelSetData_CallBack FunSetData_CallBack;

        /// <summary>
        /// 构造函数
        /// </summary>
        public NewGis()
        {
            try
            {
                InitializeComponent();
                gis.CallbackFunctionName = SetData_CallBack;
                gis.MenuPopupEvent += map_MenuPopupEvent;
                gis.SetMenuItem("test", "船舶信息", new RoutedEventHandler(button_click));
                gis.SetMenuItem("test002", "轨迹回放", new RoutedEventHandler(button_click1));
                gis.SetMenuItem("test003", "联动追踪", new RoutedEventHandler(button_click2));
                gis.SetMenuItem("test005", "设置重点监控", new RoutedEventHandler(button_click4));
                gis.SetMenuItem("test005", "获取视窗", new RoutedEventHandler(button_click3));
                InitMapServer(null);
                SetSetExtentTest();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        void map_MenuPopupEvent(double lng, double lat, string id, string layername)
        {
            PopupGuidFlag = id;
            //MessageBox.Show("Lng:" + lng + ",Lat:" + lat + ",ID:" + id + ",Layer:" + layername);
        }

        void SetData_CallBack(string UserID, string TaskGuid, string DataGuid, string DataType, string xmlDate)
        {
            XmlNode node = LoadXmlToNode(xmlDate);//先转换成Node
            MessageBox.Show(node.OuterXml);
            string strGuid="";
            try
            {
                strGuid = XmlNodeManage.GetSubValue(node, "@DataGuid");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                strGuid = SetPointGuid;
            }
          
            
            switch (DataType.ToLower())
            {
                case "setcoordinate"://标注物标
                    #region 标注物标
                    //标注这个点位（给一个小红旗）
                    #region  拼接出一个画点的数据规范

                    XmlNode DrawNode = XmlNodeManage.GetDocumentNode(TaskGuid, strGuid, "DrawPoint");
                    XmlNodeManage.SetSubValue(DrawNode, "LName", "TEXT", "Top");
                    XmlNodeManage.SetSubValue(DrawNode, "AutoZoom", "TEXT", 0);
                    XmlNodeManage.SetSubValue(DrawNode, "AutoPan", "TEXT", 0);
                    XmlNodeManage.SetSubValue(DrawNode, "Transpatecy", 0.8);
                    XmlNodeManage.SetSubValue(DrawNode, "IsCluster", 0);

                        XmlNode cnode = XmlNodeManage.CreateNode("PData");

                        XmlNodeManage.SetSubValue(cnode, "Guid", strGuid);
                        // <Guid Type=”GUID”>船舶ID</Guid>
                        //XmlNodeManage.SetSubValue(cnode, "BoatGuid", XmlNodeManage.GetSubValue(list, "BoatGuid"));
                        // <Label Type = "TEXT">点位显示名称</Label>
                        XmlNodeManage.SetSubValue(cnode, "Label", "point");
                        // <LON Type = "SINGLE">经度</LON>
                        string sLon = node.SelectSingleNode("//LON").InnerText;
                        //string s = XmlNodeManage.GetSubValue(node, "LON");
                        XmlNodeManage.SetSubValue(cnode, "LON", sLon);
                        // <LAT Type = "SINGLE">纬度</LAT>
                        string sLat = node.SelectSingleNode("//LAT").InnerText;
                        XmlNodeManage.SetSubValue(cnode, "LAT", sLat);
                        // <Time Type = "DATE">点位时间（可选）</Time>
                        //MessageBox.Show(XmlNodeManage.GetSubValue(list, "TimeStamp"));
                        DateTime dtNode = DateTime.Now;
                        XmlNodeManage.SetSubValue(cnode, "Time", dtNode);
                        // <Speed Type = "DOUBLE">速度（可选）</Speed>
                        //XmlNodeManage.SetSubValue(cnode, "Speed", XmlNodeManage.GetSubValue(list, "BoatSOG"));
                        XmlNodeManage.SetSubValue(cnode, "Speed", 0);
                        // <Direction Type = "LONG">方向</Direction>
                        XmlNodeManage.SetSubValue(cnode, "Direction", "");
                        // <Status Type=”TEXT”>状态</Status>
                        XmlNodeManage.SetSubValue(cnode, "Status", "");
                        // <Remark Type = "TEXT">备注（中文，可选）</Remark>
                        XmlNodeManage.SetSubValue(cnode, "Remark", "");

                        XmlNodeManage.SetSubValue(cnode, "DetailURL", " ");

                        XmlNodeManage.SetSubValue(cnode, "IsLabel", 1);

                        XmlNodeManage.SetSubValue(cnode, "Angle", 0);
                        String StartupPath = System.IO.Path.GetDirectoryName(Process.GetCurrentProcess().MainModule.FileName);
                       // XmlNodeManage.SetSubValue(cnode, "Icon", StartupPath+"\\Falg.ico");
                        XmlNodeManage.SetSubValue(cnode, "Icon", "1012"); 
                       DrawNode.AppendChild(cnode);
                        //MessageBox.Show(DrawNode.OuterXml);
                        gis.SetData("test", TaskGuid, strGuid, "DrawPoint", DrawNode.OuterXml);
                       #endregion                       

                    //将该信息透传给外部
                   // FunSetData_CallBack(UserID, TaskGuid, SetPointGuid, DataType, node); 
                    #endregion
                    return;

            }  
            //MessageBox.Show(xmlDate);
           // FunSetData_CallBack(UserID, TaskGuid, DataGuid, DataType, node);
        }
        /// <summary>
        /// 船舶信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        void button_click(object sender, RoutedEventArgs args)
        {
            if (!string.IsNullOrWhiteSpace(PopupGuidFlag))
            {
                MessageBox.Show("当前点选的物标为："+PopupGuidFlag);
                XmlNode node=XmlNodeManage.GetDocumentNode(TaskGuid,PopupGuidFlag,"BoatInfo");
                XmlNodeManage.SetSubValue(node, "BoatGuid", PopupGuidFlag);
                FunSetData_CallBack("test", TaskGuid, PopupGuidFlag, "BoatInfo", node);
            } 
        }
        /// <summary>
        /// 轨迹回放
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        void button_click1(object sender, RoutedEventArgs args)
        {
            if (!string.IsNullOrWhiteSpace(PopupGuidFlag))
            {
                MessageBox.Show("当前点选的物标为：" + PopupGuidFlag);
                XmlNode node = XmlNodeManage.GetDocumentNode(TaskGuid, PopupGuidFlag, "TrajectoryPlayback ");
                XmlNodeManage.SetSubValue(node, "BoatGuid", PopupGuidFlag);
                FunSetData_CallBack("test", TaskGuid, PopupGuidFlag, "TrajectoryPlayback", node);
            }
        }
        /// <summary>
        /// 联动追踪
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        void button_click2(object sender, RoutedEventArgs args)
        {
            if (!string.IsNullOrWhiteSpace(PopupGuidFlag))
            {
                //TODO:视频联动追踪   代码需要完善
                MessageBox.Show("当前点选的物标为：" + PopupGuidFlag);
                XmlNode node = XmlNodeManage.GetDocumentNode(TaskGuid, PopupGuidFlag, "LinkageTrack");
                XmlNodeManage.SetSubValue(node, "BoatGuid", PopupGuidFlag);
                FunSetData_CallBack("test", TaskGuid, PopupGuidFlag, "LinkageTrack", node);
            }
        }
        /// <summary>
        /// 联动追踪
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        void button_click3(object sender, RoutedEventArgs args)
        {
           string node = gis.GetData("test", TaskGuid, "", "Region");
           MessageBox.Show("当前视窗位置为："+node);
        }
        /// <summary>
        /// 设置为重点监控
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        void button_click4(object sender, RoutedEventArgs args)
        {
            string StrGuid= Guid.NewGuid().ToString();
            XmlNode nodepar = XmlNodeManage.GetDocumentNode("", StrGuid, "KeymonitorInfo");
            XmlNodeManage.SetSubValue(nodepar, "Guid", StrGuid);
            XmlNodeManage.SetSubValue(nodepar, "BoatGuid", PopupGuidFlag);
            FunSetData_CallBack("test", TaskGuid, StrGuid, "KeymonitorInfo", nodepar);
        }

        #region LoadXmlToNode
        /// <summary>
        /// 将xml字符串转换成XmlNode
        /// </summary>
        /// <param name="strxml">xml字符串</param>
        /// <returns>XMLNode</returns>
        private XmlNode LoadXmlToNode(string strxml)
        {
            try
            {
                strxml = strxml.Replace('”', '\"');
                //Clipboard.SetText(strxml);
                XmlDocument xdoc = new XmlDocument();
                xdoc.LoadXml(strxml);
                return xdoc.FirstChild;
            }
            catch (Exception ex)
            {
                MessageBox.Show("XML转换失败，异常：" + ex.ToString());                
                return null;
            }
        }

        

        #endregion

        #region GetData
        /// <summary>
        /// 获取单条数据
        /// </summary>
        /// <param name="UserID">人员标志</param>
        /// <param name="TaskGuid">任务标识</param>
        /// <param name="DataGuid">数据标识</param>
        /// <param name="DataType">数据类型</param>
        /// <returns>获取的单条数据</returns>
        public XmlNode GetData(string UserID, string TaskGuid, string DataGuid, string DataType)
        {
            string nodestring = gis.GetData(UserID, TaskGuid, DataGuid, DataType);
            MessageBox.Show("GetData:"+nodestring);
            return LoadXmlToNode(nodestring);
            //return (XmlNode)gis.Dispatcher.Invoke(new DelGetData(FuncGetData), UserID, TaskGuid, DataGuid, DataType);
        }
        /// <summary>
        /// GetData的实现方法
        /// </summary>
        /// <param name="UserID"></param>
        /// <param name="TaskGuid"></param>
        /// <param name="DataGuid"></param>
        /// <param name="DataType"></param>
        /// <returns></returns>
        public XmlNode FuncGetData(string UserID, string TaskGuid, string DataGuid, string DataType)
        {
            return LoadXmlToNode(gis.GetData(UserID, TaskGuid, DataGuid, DataType));
        } 
        #endregion

        #region SetData
        /// <summary>
        /// 写入单条数据
        /// </summary>
        /// <param name="UserID">用户ID</param>
        /// <param name="TaskGuid">任务标志</param>
        /// <param name="DataGuid">数据标志</param>
        /// <param name="DataType">数据类型</param>
        /// <param name="node">插入数据的节点</param>
        /// <returns>插入数据的结果</returns>
        public int SetData(string UserID, string TaskGuid, string DataGuid, string DataType, XmlNode node)
        {
            gis.Dispatcher.Invoke(new DelSetData(FuncSetData), UserID, TaskGuid, DataGuid, DataType, node);
            return 0;
        }
        public int FuncSetData(string UserID, string TaskGuid, string DataGuid, string DataType, XmlNode node)
        {
            return gis.SetData(UserID, TaskGuid, DataGuid, DataType, node.OuterXml);
        } 
        #endregion

        #region TransferData
        /// <summary>
        /// 查询参数
        /// </summary>
        /// <param name="UserID">用户ID</param>
        /// <param name="TaskGuid">任务标志</param>
        /// <param name="node">查询的参数</param>
        /// <returns>查询的结果</returns>
        public XmlNode TransferData(string UserID, string TaskGuid, XmlNode node)
        {
            //return (XmlNode)gis.Dispatcher.Invoke(new DelGetData(FuncTransferData), UserID, TaskGuid, node);
            return null;
        }
        /// <summary>
        /// TransferData的实现函数
        /// </summary>
        /// <param name="UserID"></param>
        /// <param name="TaskGuid"></param>
        /// <param name="node"></param>
        /// <returns></returns>
        public XmlNode FuncTransferData(string UserID, string TaskGuid, XmlNode node)
        {
            //return LoadXmlToNode(gis.(UserID, TaskGuid, node));
            return null;
        }  
        #endregion

        /// <summary>
        /// 初始化地图服务
        /// </summary>
        /// <param name="node"></param>
        private void InitMapServer(XmlNode node)
        {
            StringBuilder xml = new StringBuilder();
            xml.Append("<Document TaskGuid = \"FF17D1B3-85C7-40F4-8CDC-73DC57CD29BC\" DataGuid = \"" + Guid.NewGuid().ToString() + "\" DataType = \"MapServer\">");
            xml.Append("<AutoZoom Type =\"TEXT\">0</AutoZoom>");
            xml.Append("<AutoPan Type =\"TEXT\">0</AutoPan>");
            xml.Append("<MData>");
            xml.Append("<MName Type = \"TEXT\">szmap</MName>");
            xml.Append("<MServer Type = \"TEXT\">http://58.211.90.18:8046/arcgis/rest/services/ChinaforServer/MapServer</MServer>");
            xml.Append("<MScaleLevel Type = \"TEXT\">320000,160000,80000,40000,20000,10000,5000,2500</MScaleLevel>");
            xml.Append(" </MData></Document>");

            gis.SetData("gistest", "FF17D1B3-85C7-40F4-8CDC-73DC57CD29BC", Guid.NewGuid().ToString(), "MapServer", xml.ToString());
        }
        /// <summary>
        /// 设置居中点
        /// </summary>
        private void SetSetExtentTest()
        {
            StringBuilder xml = new StringBuilder();
            xml.Append("<Document TaskGuid = \"FF17D1B3-85C7-40F4-8CDC-73DC57CD29BC\" DataGuid = \"" + Guid.NewGuid().ToString() + "\" DataType = \"InitMap\">");
            xml.Append("<LON Type = \"SINGLE\">" + 120.358895 + "</LON>");
            xml.Append("<LAT  Type = \"SINGLE\">" + 31.108965+ "</LAT >");
            xml.Append("<Level  Type = \"LONG\">" + 9 + "</Level>");
            xml.Append("<autoChangeMap Type = \"LONG\">0</autoChangeMap>");
            xml.Append("</Document>");
            gis.SetData("gistest", "FF17D1B3-85C7-40F4-8CDC-73DC57CD29BC", Guid.NewGuid().ToString(), "InitMap", xml.ToString());
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            
            
        }

        private void button2_Click(object sender, RoutedEventArgs e)
        {
            
        }

    }
}
