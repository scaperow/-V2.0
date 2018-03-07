using System;
using System.Collections.Generic;
using System.Text;
using RabbitMQ.Client;
using System.Threading;
using RabbitMQ.Client.Events;

namespace Yqun.BO.BusinessManager
{
    /// <summary>
    /// 消息队列处理类
    /// </summary>
    public class CaiJiRabbitMQHelper
    {

        private static readonly log4net.ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private static readonly object objMQ = new object();
        public void StartApplyQueue(string LineID)//Guid _DataID, Guid _TestDataID
        {
            ThreadParameter p = new ThreadParameter();
            p.LineID = LineID;
            //p.DataID = _DataID;
            //p.TestDataID = _TestDataID;
            ThreadPool.QueueUserWorkItem(new WaitCallback(ApplyQueue), p);
        }
        private void ApplyQueue(object paremeter)
        {
            ThreadParameter p = paremeter as ThreadParameter;
            var factory = new ConnectionFactory() { HostName = "localhost" };
            using (var connection = factory.CreateConnection(999999))
            {
                using (var channel = connection.CreateModel())
                {
                    //logger.Error(string.Format("{0}RabbitMQ Received start", p.LineID));
                    QueueingBasicConsumer consumer = null;
                    lock (objMQ)
                    {

                        //随机创建一个队列
                        //var queue_name = channel.QueueDeclare(p.DataID.ToString(), false, false, true, null);//临时队列

                        var queue_name = channel.QueueDeclare(p.LineID, true, false, false, null);//持久化队列

                        if (queue_name.ConsumerCount > 0)
                        {
                            return;
                        }
                        consumer = new QueueingBasicConsumer(channel);
                        channel.BasicConsume(p.LineID, false, consumer);
                    }
                    //channel.BasicQos(0, 1, false);
                    Boolean hasNext = true;
                    while (hasNext)
                    {
                        byte[] body = null;
                        var message = "";
                        BasicDeliverEventArgs ea = null;
                        try
                        {
                            hasNext = consumer.Queue.Dequeue(5000, out ea);

                            if (ea != null)
                            {
                                body = ea.Body;
                                message = Encoding.UTF8.GetString(body);
                                //logger.Info(string.Format("RabbitMQ Received queue:{0} message:{1}", p.LineID, message));
                                //Console.WriteLine(" [x] Received {0}", message);
                                string[] ss = message.Split(',');
                                //logger.Info(string.Format("message:{0} ss.length:{1} ss[0]:{2} ss[1]:{3}", message, ss.Length, ss[0], ss[1]));
                                if (ss.Length == 2)
                                {
                                    Guid ModuleID = Guid.Empty;
                                    DealCaiJi(new Guid(ss[0]), new Guid(ss[1]), out ModuleID);

                                    //添加到"打开资料，保存"队列
                                    //string strMQDocName = "MQDealDoc";
                                    //channel.QueueDeclare(strMQDocName, true, false, false, null);
                                    //message = p.LineID + "," + ss[0] + "," + ModuleID.ToString();
                                    //body = Encoding.UTF8.GetBytes(message);
                                    //IBasicProperties properties = channel.CreateBasicProperties();
                                    //properties.DeliveryMode = 2;
                                    //channel.BasicPublish("", strMQDocName, properties, body);
                                }
                                channel.BasicAck(ea.DeliveryTag, false);
                            }
                            else
                            {
                                hasNext = false;
                            }
                        }
                        catch (Exception ex)
                        {
                            logger.Error(string.Format("{0}RabbitMQ Received message:{1} error:{2}", p.LineID, message, ex.ToString()));
                            hasNext = false;

                        }
                    }
                    //logger.Error(string.Format("{0}RabbitMQ Received finished", p.LineID));

                }
            }
        }
        private void DealCaiJi(Guid DataID, Guid TestDataID, out Guid ModuleID)
        {
            CaiJiHelper caiji = new CaiJiHelper();
            ModuleID = Guid.Empty;
            if (DataID != Guid.Empty)
            {
                BizCommon.Sys_TestData mdlTestData = caiji.GetTestDataModel(TestDataID);
                if (mdlTestData != null)
                {
                    ModuleID = mdlTestData.ModuleID;
                    Boolean inStadiumRange = true;

                    if (mdlTestData.StadiumID == Guid.Empty)
                    {
                        inStadiumRange = caiji.InStadiumRange(DataID, mdlTestData.CreatedTime,mdlTestData.TestRoomCode);
                    }
                    if (inStadiumRange)
                    {
                        if (mdlTestData.Status == 0)
                        {
                            caiji.ApplyUploadData(mdlTestData.ID, mdlTestData.DataID, mdlTestData.ModuleID, mdlTestData.StadiumID, mdlTestData.WTBH, mdlTestData.TestRoomCode, mdlTestData.SerialNumber, mdlTestData.UserName, mdlTestData.TestData, mdlTestData.RealTimeData, mdlTestData.TotallNumber, mdlTestData.MachineCode,
                                 mdlTestData.UploadInfo, mdlTestData.UploadCode);
                        }
                    }
                    else
                    {
                        caiji.SaveTestOverTime(mdlTestData.ID, mdlTestData.DataID, mdlTestData.ModuleID, mdlTestData.StadiumID, mdlTestData.WTBH, mdlTestData.TestRoomCode, mdlTestData.SerialNumber, mdlTestData.UserName, mdlTestData.TestData, mdlTestData.RealTimeData, mdlTestData.TotallNumber, mdlTestData.MachineCode, mdlTestData.UploadInfo, mdlTestData.UploadCode);
                        caiji.UpdateTestDataStatus(TestDataID, 3);
                    }
                }
                else
                {
                    logger.Info("DealCaiJi TestDataModel Is Null.TestDataID:" + TestDataID);
                }
            }
            else
            {
                caiji.UpdateTestDataStatus(TestDataID, -2);
            }
        }
        private class ThreadParameter
        {
            public string LineID;
            public Guid DataID;
            public Guid TestDataID;
        }
    }

}
