using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace StationPassengerFlowSimulation
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            ServerParameter simulationParameter = new ServerParameter
            {
                ArriveTime = 5,//到达时刻参数
                BuyTicketTimeSpanMu = 1,//售票时间间隔均值
                BuyTicketTimeSpanSigma = 0.5,//售票时间间隔方差
                WalkingTime = 0.1,//走行时间参数数
                SimulationTimeLimit = TimeSpan.FromMinutes(180),//仿真时间间隔为3个小时
                CounterNum = 5,//闸机个数
            };
            DateTime starTime = DateTime.Now;//开始仿真时间
            DateTime currentTime = starTime;//仿真时钟
            List<DateTime> arriverSpans = new List<DateTime>();//存储到达售票机的时刻
            List<TimeSpan> buyingSpans = new List<TimeSpan>();//到服务台买票时间间隔
            List<DateTime> arriveCounter = new List<DateTime>();//存储到达闸机的时刻
            for (int i = 0; i < 1000; i++)
            {
                TimeSpan arriverSpan = TimeSpan.FromMinutes(-1 / simulationParameter.ArriveTime * Math.Log(new Random(Guid.NewGuid().GetHashCode()).NextDouble()));//产生一个的服从泊松分布分时间间隔
                currentTime += arriverSpan;
                TimeSpan buyingSpan = TimeSpan.FromMinutes(GetNormalRand(simulationParameter.BuyTicketTimeSpanMu, simulationParameter.BuyTicketTimeSpanSigma));

                if (starTime + simulationParameter.SimulationTimeLimit < currentTime) continue;
                arriverSpans.Add(currentTime);
                buyingSpans.Add(buyingSpan);
                arriveCounter.Add(currentTime + buyingSpan);
            }

            List<Counter> counters = new List<Counter>();//存储闸机
            for (int i = 0; i < simulationParameter.CounterNum; i++)
            {
                Counter counter = new Counter();
                counter.CounterQunueSituation.isQunuing.Add(false);//默认为没有排队
                counter.CounterQunueSituation.currentTime.Add(starTime);//仿真刚开始的时间，不具有实际意义
                counter.CounterIsBusy.isBusy.Add(false);//默认为不繁忙
                counter.CounterIsBusy.currentTime.Add(starTime);//仿真刚开始的时间，不具有实际意义
                counter.FutureTime = starTime; //仿真刚开始的时间，不具有实际意义
                counters.Add(counter);//加入柜台列表
            }
            int index =0;
            int numArriveCustomer = 0;
            List<DateTime> arriveCounterInTimes=new List<DateTime>();//实时仿真时的到达闸机时刻列表存储
            List<DateTime>leaveCounterInTimes=new List<DateTime>();//实时仿真是离开闸机时刻列表存储
            List<DateTime>eventTimes=new List<DateTime>();//事件发生时间的列表
            bool arriveFlag = false;//是否到达记录器
            while (numArriveCustomer < arriverSpans.Count)
            {
                arriveCounterInTimes.Add(arriveCounter.Min());//到达闸机的时刻
                //判断是是不是初始状态
                int flag = 0;//记录闸机离开时间为仿真开始时间的个数
                List<DateTime>leaveCounterInTimes_temp=new List<DateTime>();//暂时存储非0的离开时间
                foreach (var counter in counters)
                {
                    if (counter.FutureTime==starTime)
                    {
                        flag++;
                    }
                    else
                    {
                        leaveCounterInTimes_temp.Add(starTime);
                    }
                }
                if (flag==counters.Count)
                {
                   eventTimes[index]=arriveCounterInTimes[index];//将到达时间添加到事件时间列表中
                   arriveCounter.Remove(arriveCounterInTimes[index]);//到达事件集合中删除已经发生的事件
                   arriveFlag = true;
                }
                else
                {
                    leaveCounterInTimes.Add(leaveCounterInTimes_temp.Min());//寻找最小的离开时间
                    if (leaveCounterInTimes[index]>arriveCounterInTimes[index])//如果离开事件的时间晚于到达事件的时间，说明不会排队
                    {
                        eventTimes[index] = arriveCounterInTimes[index];
                        arriveCounter.Remove(arriveCounterInTimes[index]);//事件表中删除事件
                        arriveFlag = true;
                    }
                    else
                    {
                        eventTimes[index] = leaveCounterInTimes[index];
                        arriveFlag = false;
                    }
                }

                if (arriveFlag=true)
                {
                    numArriveCustomer += 1;

                }
                
 
                



            }






        }

        /// <summary>
        /// 获得均值为mu，方差为sigma的正态分布随机数
        /// </summary>
        /// <param name="mu">均值</param>
        /// <param name="sigma">方差</param>
        /// <returns>返回均值为mu，方差为sigma的正态分布随机数</returns>
        public double GetNormalRand(double mu, double sigma)
        {
            //如果方差小于0，返回的是均值
            if (sigma <= 0)
            {
                return mu;
            }
            var u1 = (new Random(Guid.NewGuid().GetHashCode())).NextDouble();//正态分布随机数种子
            var u2 = (new Random(Guid.NewGuid().GetHashCode())).NextDouble();//正态分布随机数种子
            var z = Math.Sqrt(-2 * Math.Log(u1)) * Math.Sin(2 * Math.PI * u2);
            var x = mu + sigma * z;
            return x;
        }

    }
}
