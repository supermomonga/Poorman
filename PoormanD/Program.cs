using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Threading.Tasks;
using System.Net.Sockets;

namespace PoormanD
{
    public class Program
    {
        public static void Main(string[] args)
        {
            MainAsync(args).GetAwaiter().GetResult();
        }

        static async Task MainAsync(string[] args)
        {
            // TODO: getopt from args
            const int port = 1234;
            const string host = "localhost";

            try
            {
                // Get IP address from hostname and listen it.
                var ipAddresses = await Dns.GetHostAddressesAsync(host);
                var ipLocalEndPoint = new IPEndPoint(ipAddresses[1], port);
                var listener = new TcpListener(ipLocalEndPoint);
                listener.Start();
                Console.WriteLine("Listen on {0}:{1}", ipLocalEndPoint.Address, ipLocalEndPoint.Port);
            }
            catch (Exception e)
            {
                Console.WriteLine("Failed to open TCP socket listener.");
                Console.WriteLine(e.ToString());
            }
        }
    }
}


