using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace TestGame.Scenes
{
	/// <summary>
	/// ステージを選択するクラスです.
	/// </summary>
	public class StageSelector
	{
		/// <summary>
		/// ステージの番号.
		/// </summary>
		public int Index { set; get; }

		/// <summary>
		/// ステージをはじめからやるかどうか.
		/// </summary>
		public Options Option { set; get; } = Options.Init;

		/// <summary>
		/// ステージファイルへのパス.
		/// </summary>
		public string Path
		{
			get { return Environment.CurrentDirectory + System.IO.Path.DirectorySeparatorChar + ("Stage1_" + Index + ".text"); }
		}

		/// <summary>
		/// ステージを開くときのオプション.
		/// </summary>
		public enum Options {
			Init, Continue
		}

		public StageSelector()
		{
		}

		/// <summary>
		/// フィールドを書き換えずに、そのインデックスが選択されているときのパスを返します.
		/// </summary>
		/// <param name="index"></param>
		/// <returns></returns>
		public string GetPath(int index)
		{
			return Environment.CurrentDirectory + System.IO.Path.DirectorySeparatorChar + ("Stage1_" + index + ".text"); 
		}
	}
}
