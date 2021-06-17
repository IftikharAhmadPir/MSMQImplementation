using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Messaging;

namespace MSMQImplementation
{
    class Program
    {
        static void Main(string[] args)
        {
            sendQueue();
            receiveQueue();
        }

        private static void sendQueue()
        {
            var emp = new Employee() {
                Id = 100, Name = "John Doe", Hours = 55, Rate = 21.0
            };
            System.Messaging.Message msg =  new System.Messaging.Message();
            msg.Body = emp;
            MessageQueue msgQ = new MessageQueue(".\\Private$\\newPrivateQueue");
            
            msgQ.Send(msg);
        }

        private static void receiveQueue()
        {
            MessageQueue msgQ = new MessageQueue(".\\Private$\\newPrivateQueue");
            var emp = new Employee();
            Object o = new Object();
            System.Type[] arrTypes = new System.Type[2]; 
            arrTypes[0] = emp.GetType();
            arrTypes[1] = o.GetType();
            msgQ.Formatter = new XmlMessageFormatter(arrTypes);
            emp = ((Employee)msgQ.Receive().Body);
            Console.WriteLine("Employee Name:{0} Salary: {1}", emp.Name, (emp.Hours * emp.Rate));
            Console.ReadKey();
        }
    }

    public class Employee
    {
        public int Id;
        public string Name;
        public int Hours;
        public double Rate;
    }
}
