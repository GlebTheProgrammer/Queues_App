using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Queues_App
{
    class Processor
    {
        public Client[] queue { get; set; }

        private int processTime;
        public int ProcessTime
        {
            get { return processTime; }

            private set 
            { 
                if (value < 1)
                {
                    processTime = 1;
                    return;
                }
                if (value > 10)
                {
                    processTime = 10;
                    return;
                }
                processTime = value; 
            }
        }

        private int imputTime;
        public int InputTime
        {
            get { return imputTime; }
            private set 
            {
                if (value < 0)
                {
                    imputTime = 0;
                    return;
                }
                if (value > 10)
                {
                    InputTime = 10;
                    return;
                }
                imputTime = value; 
            }
        }

        public int timeNeeded = 0;
        public int timeSpent = 0;
        public int timeWaiting = 0;


        public Processor()
        {
            queue = new Client[]
            {
                new Client
                {
                    workTime = new int[] {9,6,3,2,8,7,4},
                    priority = 1
                },
                new Client
                {
                    workTime = new int[] {3,7,2,4,7,3,1},
                    priority = 2
                },
                new Client
                {
                    workTime = new int[] {4,5,6,4,2,3,4,5},
                    priority = 2
                },
                new Client
                {
                    workTime = new int[] {3,5,4,2,1,6,8,3},
                    priority = 2
                },
                new Client
                {
                    workTime = new int[] {2,3,2,1,4,2,3,4},
                    priority = 3
                },
                new Client
                {
                    workTime = new int[] {3,2,1,2,3,4,3,4},
                    priority = 3
                }
            };

            Console.Write("Process Time: ");
            ProcessTime = int.Parse(Console.ReadLine());

            Console.Write("Input Time: ");
            InputTime = int.Parse(Console.ReadLine());

            for (int i = 0; i < queue.Length; i++)
            {
                for (int j = 0; j < queue[i].workTime.Length; j++)
                {
                    timeNeeded += queue[i].workTime[j];
                }
            }

            Console.Clear();
        }
    }
}
