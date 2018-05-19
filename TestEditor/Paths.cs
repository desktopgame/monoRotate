using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestEditor
{
	public class Paths
	{
		public static readonly string ENV = Environment.CurrentDirectory + Path.DirectorySeparatorChar;
		public static readonly string RES = ENV + "res";
	}
}
