using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StationPassengerFlowSimulation
{

    /// <summary>
    /// 闸机类定义
    /// </summary>
    class Counter
    {
        public qunueSituation CounterQunueSituation { get; set; }//闸机排队情况
        public counterIsBusy CounterIsBusy { get; set; }//闸机繁忙情况
        public DateTime FutureTime { get; set; }//离开闸机的时间
        public List<int> ConsumerNum { get; set; }//闸机服务的乘客个数记录
        public List<TimeSpan> PassTimeSpans { get; set; }//闸机的通过时间
        public List<DateTime> StartServeTime { get; set; }//闸机开始服务的时刻
        public List<DateTime> LeaveCounterTime { get; set; }//离开闸机的时刻
        public List<TimeSpan> StayCounterTime { get; set; }//停留时间
    }

    /// <summary>
    /// 排队状况
    /// </summary>
    struct  qunueSituation
    {
        public List<DateTime> currentTime;//当前时间
        public List<bool>  isQunuing;//是否存在排队情况
    }

    /// <summary>
    /// 柜台是否繁忙
    /// </summary>
    struct counterIsBusy
    {
        public List<DateTime> currentTime;//当前时间
        public List<bool>  isBusy;//是否存在排队情况
    }
}
