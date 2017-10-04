using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace PortScanner
{
	class Program
	{
		static int Main(string[] args)
		{
			// Check for valid argument count
			if (args.Length != 2)
			{
				System.Console.WriteLine("Please enter an IP address followed by a valid port range.");
			}

			// Parse the address passed in as argument 0
			var address = IPAddress.Parse(args[0]);
			// Set the port range to a string
			string portRange = args[1];

			// Split the port range into an array delimited by a -
			string[] split = portRange.Split('-');
			if (split.Length != 2)
			{
				Console.WriteLine("Invalid port range entered.  Exiting program.");
				return -1;
			}

			// Parse the start address
			int start = Int32.Parse(split[0]);
			// Parse the end address
			int end = Int32.Parse(split[1]);

			Console.WriteLine("----------------------------");
			Console.WriteLine("TCP Port Scanner");
			Console.WriteLine("----------------------------");

			List<int> portList = new List<int>();

			Parallel.For(start, end, port =>
			{
				{
					// Create a new socket upon each iteration of the loop
					Socket socket = new Socket(SocketType.Stream, ProtocolType.Tcp);
					try
					{
						// Try to connect to the port at the address
						socket.Connect(address, port);
						portList.Add(port);
					}
					catch (SocketException e)
					{
						// Do nothing here
					}

				}
			});

			foreach (int port in portList)
			{
				Console.WriteLine($"Port: {port} \t OPEN");
			}


			Console.WriteLine("Port scanning complete.");
			Console.ReadLine();

			return 0;
		}
	}
}
