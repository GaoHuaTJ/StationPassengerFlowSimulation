using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StationPassengerFlowSimulation
{/// <summary>
/// 接受Form中输入的分布参数
/// </summary>
    class ServerParameter
    {
        public double BuyTicketTime { get; set; }//购票时刻，分公交卡和单程票，公交卡为0，单程票定义为常数
        public double ArriveTime { get; set; }//到达时刻
        public double BuyTicketTimeSpanMu { get; set; }//售票时刻均值
        public double BuyTicketTimeSpanSigma { get; set; }//售票时刻方差
        public double WalkingTime { get; set; }//走行时间
        public int ServerCounter { get; set; }//闸机个数
        public  TimeSpan SimulationTimeLimit { get; set; }//仿真时间限制
        public  int SimulationTimes { get; set; }//仿真次数
        public int CounterNum { get; set; }//闸机个数
     
    }

struct ChooseMethod
{
    public string TableSearch;
    public string Rand_shortest;

}


}
