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
            var port = 1234;
            var ipAddress = "127.0.0.1";

            var listener = new TcpListener(
                IPAddress.Parse(ipAddress),
                port
            );
            listener.Start();

            Console.WriteLine(
                "Listen on {0}:{1}",
                ((IPEndPoint)listener.LocalEndpoint).Address,
                ((IPEndPoint)listener.LocalEndpoint).Port
            );
        }
    }
}


