using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AutoShutdown
{
	static class Program
	{
		/// <summary>
		/// 应用程序的主入口点。
		/// </summary>
		[STAThread]
		static void Main()
		{
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());


		}
		static String executeCommand(String argument)
		{
			// Start the child process.
			Process p = new Process();
			// Redirect the output stream of the child process.
			p.StartInfo.UseShellExecute = false; p.StartInfo.CreateNoWindow = true;
			p.StartInfo.RedirectStandardOutput = true;
			p.StartInfo.FileName = "shutdown.exe";
			p.StartInfo.Arguments = argument;
			p.Start();
			String output = null;
			while (true)
			{
				String tmp = p.StandardOutput.ReadToEnd();
				if (tmp == "")
					break;
				output += tmp;
			}
			//p.WaitForExit();
			return output;
		}
		public static void excuteShutdown(string text)
		{
			int minute;
			if (Int32.TryParse(text, out minute))
			{
				executeCommand("-s -t " + minute * 60);
			}
		}
		public static void cancelShutdown() {
			executeCommand("-a");
		}
    }
}
