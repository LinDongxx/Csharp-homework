using System;
namespace AlarmEvent
{
    public class AlarmClock
    {
        static void Main(string[] args)
        {
            AlarmClock clock = new AlarmClock();
            clock.Tick += new TickEventHandler((sender, e) => Console.WriteLine("处理Tick"));
            clock.Alarm += new AlarmEventHandler((sender, e) => Console.WriteLine("处理Alarm"));
            clock.OnTick();
            clock.OnAlarm();
        }

        public delegate void TickEventHandler(object sender, EventArgs e);
        public event TickEventHandler Tick;

        public delegate void AlarmEventHandler(object sender, EventArgs e);
        public event AlarmEventHandler Alarm;

        public void OnTick()
        {
            Console.WriteLine("闹钟Tick");
            Tick.Invoke(this, new EventArgs());
        }

        public void OnAlarm()
        {
            Console.WriteLine("闹钟Alarm");
            Alarm.Invoke(this, new EventArgs());
        }


    }
}