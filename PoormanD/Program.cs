using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Net.Sockets;

namespace PoormanD
{
    public class Program
    {
        public static void Main(string[] args)
        {
            // TODO: getopt
            const int port = 1234;

            var ipAddress = Dns.Resolve(Dns.GetHostName()).AddressList[0];
            var ipLocalEndPoint = new IPEndPoint(ipAddress, port);

            try
            {
                var listener = new TcpListener(ipLocalEndPoint);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }

            listener.Start();
            Console.WriteLine("Listen on {0}:{1}", ipLocalEndpoint.Address, ipLocalEndpoint.Port);
        }
    }
}


