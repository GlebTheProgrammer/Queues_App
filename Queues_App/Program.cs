using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Queues_App
{
    class Program
    {
        static void WorkWithClient(Processor processor, int index)
        {
            for (int i = 0; i < processor.queue[index].workTime.Length; i++)  // Работаем с первым человеком с приоритетом
            {

                if (processor.queue[index].workTime[i] <= 0)  // Если его таск уже выполнен -> идём дальше в поисках того, который он ещё не выполнил
                    continue;

                if (processor.queue[index].workTime[i] > 0)  // Если ещё необходимо время на выполнение таска - работаем
                    processor.queue[index].workTime[i] -= processor.ProcessTime;


                if (i + 1 == processor.queue[index].workTime.Length && processor.queue[index].workTime[i] <= 0)  // Если последняя задача завершена - с клиентом мы закончили
                {
                    processor.queue[index].isDone = true;
                    break;
                }

                if (processor.queue[index].workTime[i] == 0)  // Если больше времени не нужно - начинаем ввод
                {
                    if (processor.InputTime != 0)  // Если время ввода = 0, то вводить нам не нужно, следовательно break
                    {
                        processor.queue[index].isWorking = true;
                        processor.queue[index].workingTimeLeft = processor.InputTime;
                        break;
                    }
                    else
                        break;
                }
                if (processor.queue[index].workTime[i] < 0)  // Если было затрачено лишнее время - ввод занимает меньше времени
                {
                    if (processor.InputTime != 0)  // Если время ввода = 0, то вводить нам не нужно, следовательно break
                    {
                        processor.queue[index].isWorking = true;
                        processor.queue[index].workingTimeLeft = processor.InputTime - Math.Abs(processor.queue[index].workTime[i]);
                        break;
                    }
                    else
                        break;
                }
                break;  // Если время ещё осталось - просто выходим из цикла
            }
        }

        static void CheckClientsInput(Processor processor)
        {
            for (int i = 0; i < processor.queue.Length; i++)  // Проходимся по всей очереди и выполняем ввод данных, если это необходимо
            {
                if (processor.queue[i].isWorking == true)  // Если клиент находится в стадии ввода данных, то уменьшаем время этого ввода
                    processor.queue[i].workingTimeLeft -= processor.ProcessTime;
                if (processor.queue[i].workingTimeLeft <= 0)  // Если ввод завершился - мы должны изменить состояние переменных
                {
                    processor.queue[i].workingTimeLeft = 0;
                    processor.queue[i].isWorking = false;
                }
            }
        }

        static void Main(string[] args)
        {
            Processor processor = new Processor();

           

            while (true)
            {

                if (processor.queue[0].isDone == false && processor.queue[0].isWorking == false)  // Если все задачи ещё не выполнены и он не находится в стадии ввода
                {
                    CheckClientsInput(processor: processor);

                    WorkWithClient(processor: processor, index: 0);

                    continue;
                }

                if ((processor.queue[1].isDone == false && processor.queue[1].isWorking == false) || (processor.queue[2].isDone == false && processor.queue[2].isWorking == false)  || (processor.queue[3].isDone == false && processor.queue[3].isWorking == false))
                {
                    if (processor.queue[1].isDone == false && processor.queue[1].isWorking == false)
                    {
                        CheckClientsInput(processor: processor);

                        WorkWithClient(processor: processor, index: 1);
                    }

                    if (processor.queue[2].isDone == false && processor.queue[2].isWorking == false)
                    {
                        CheckClientsInput(processor: processor);

                        WorkWithClient(processor: processor, index: 2);
                    }

                    if (processor.queue[3].isDone == false && processor.queue[3].isWorking == false)
                    {
                        CheckClientsInput(processor: processor);

                        WorkWithClient(processor: processor, index: 3);
                    }
                    continue;
                }

                if ((processor.queue[4].isDone == false && processor.queue[4].isWorking == false) || (processor.queue[5].isDone == false && processor.queue[5].isWorking == false))
                {
                    if (processor.queue[4].isDone == false && processor.queue[4].isWorking == false)
                    {
                        CheckClientsInput(processor: processor);

                        WorkWithClient(processor: processor, index: 4);
                    }

                    if (processor.queue[5].isDone == false && processor.queue[5].isWorking == false)
                    {
                        CheckClientsInput(processor: processor);

                        WorkWithClient(processor: processor, index: 5);
                    }
                    continue;
                }

                // Если мы добрались до конца цикла и ни один continue не сработал, значит имеем следующее: либо все клиенты нуждаются в большем времени для ввода данных, либо же все клинты закончили свою работу

                if (processor.queue[0].isDone == true && processor.queue[1].isDone == true && processor.queue[2].isDone == true && processor.queue[3].isDone == true && processor.queue[4].isDone == true && processor.queue[5].isDone == true)
                {
                    // Если все клиенты закончили работу - просто выходим из цикла
                    break;
                }

                if ((processor.queue[0].isWorking == true || processor.queue[0].isDone == true) && (processor.queue[1].isWorking == true || processor.queue[1].isDone == true) && (processor.queue[2].isWorking == true || processor.queue[2].isDone == true) && (processor.queue[3].isWorking == true || processor.queue[3].isDone == true) && (processor.queue[4].isWorking == true || processor.queue[4].isDone == true) && (processor.queue[5].isWorking == true || processor.queue[5].isDone == true))
                {
                    // Если все клиенты ожидают ввода - процессор простаивает, а клиенты продолжают ввод данных

                    processor.timeSpent += processor.ProcessTime;

                    CheckClientsInput(processor: processor);
                }
            }

            Console.WriteLine($"Время, которое необходимо было для обработки всех клиентов, составило: T треб. = {processor.timeNeeded}\n");

            Console.WriteLine($"Простой процессора за то время, пока все клиенты находились в стадии ввода, составил: T' простой. = {processor.timeSpent}\n");

            for (int i = 0; i < processor.queue.Length; i++)
            {
                for (int j = 0; j < processor.queue[i].workTime.Length; j++)
                {
                    if (processor.queue[i].workTime[j] < 0)
                        processor.timeSpent += Math.Abs(processor.queue[i].workTime[j]);
                }
            }

            Console.WriteLine($"Простой процессора за всё время составил: T простой. = {processor.timeSpent}\n");

            Console.WriteLine($"КПД процессора составил: n = {(float)((100 * processor.timeNeeded)/(processor.timeNeeded + processor.timeSpent))}");

            Console.ReadLine();












        }
    }
}
