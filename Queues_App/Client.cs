using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Queues_App
{
    class Client
    {
        public int[] workTime { get; set; }
        public int priority { get; set; }
        public bool isDone = false;
        public bool isWorking = false;
        public bool isWaiting = false;
        public int workingTimeLeft;
    }
}
