﻿using System;
using System.Net;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.CommandLine;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.Extensions.Configuration;
using PoormanD.Models;

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

            try
            {
                Benri().GetAwaiter().GetResult();
                //ListenAsync(address, port).GetAwaiter().GetResult();
                Console.WriteLine("done.");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }

        private static async Task Benri()
        {
            using (var db = new LoggingContext())
            {
                db.Database.EnsureCreated();
                db.Database.ExecuteSqlCommand("DELETE FROM events");
                db.Database.ExecuteSqlCommand("VACUUM");
                db.Events.Add(new Event()
                {
                    Context = "capture1",
                    Identifier = "GET /",
                    StartedAt = DateTime.Now,
                    FinishedAt = DateTime.Now,
                    Duration = TimeSpan.FromMilliseconds(1000),
                    Parameters = @"
                    {
                        ""key1"": ""value1-1"",
                        ""key2"": ""value2-1""
                    }
                    "
                });
                db.Events.Add(new Event()
                {
                    Context = "capture1",
                    Identifier = "GET /",
                    StartedAt = DateTime.Now,
                    FinishedAt = DateTime.Now,
                    Duration = TimeSpan.FromMilliseconds(1000),
                    Parameters = @"
                    {
                        ""key1"": ""value1-2"",
                        ""key2"": ""value2-2""
                    }
                    "
                });
                await db.SaveChangesAsync();
                Console.WriteLine(await db.Events.CountAsync());
                Console.WriteLine(await db.Events.CountAsync(
                        e => Event.JsonExtract(e.Parameters, "$.key1") == "value1-1"
                ));
                Console.WriteLine(
                await (from e in db.Events
                where Event.JsonExtract(e.Parameters, "$.key1") == "value1-1"
                select e).CountAsync()
                );
            }

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


