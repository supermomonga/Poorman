using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.CommandLine;

namespace PoormanD
{
    public class Program
    {

        public static void Main(string[] args)
        {
            var address = "0.0.0.0";
            var port = 55301;
            ArgumentSyntax.Parse(args, syntax =>
            {
                syntax.DefineOption("a|address", ref address, "IP address to listen");
                syntax.DefineOption("p|port", ref port, "Port to listen");
            });

            ListenAsync(address, port).GetAwaiter().GetResult();
        }

        private static async Task ListenAsync(string address, int port)
        {
            try
            {
                // Get IP address from hostname and listen it.
                var ipAddress = IPAddress.Parse(address);
                var listener = new TcpListener(ipAddress, port);
                listener.Start();
                Console.WriteLine("Listen on {0}:{1}", ipAddress, port);
            }
            catch (Exception e)
            {
                Console.WriteLine("Failed to open TCP socket listener.");
                Console.WriteLine(e.ToString());
            }
        }
    }
}


