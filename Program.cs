using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;

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

			for (int port = start; port <= end; port++)
			{
				Socket socket = new Socket(SocketType.Stream, ProtocolType.Tcp);
				try
				{
					socket.Connect(address, port);
					Console.WriteLine($"Port: {port} \t OPEN");
				}
				catch (SocketException e)
				{
					Console.WriteLine($"Port: {port} \t CLOSED");
				}

			}

			Console.ReadLine();
			return 0;
		}
	}
}
