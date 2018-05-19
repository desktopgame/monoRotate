using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EditorUtilities
{
	/// <summary>
	/// 現在編集されているファイル.
	/// </summary>
	public class SourceFile
	{
		/// <summary>
		/// 参照しているパスの変更を監視するリスナーのリスト.
		/// </summary>
		public event EventHandler OnPathChanged;

		/// <summary>
		/// 編集状態の変更を監視するリスナーのリスト.
		/// </summary>
		public event EventHandler OnDirtyChanged;

		/// <summary>
		/// ウィンドウタイトルの変更を監視するリスナーのリスト.
		/// </summary>
		public event EventHandler OnTitleChanged;

		/// <summary>
		/// ファイルパス.
		/// </summary>
		public string Path
		{
			set
			{
				this._path = value;
				OnPathChanged?.Invoke(this, EventArgs.Empty);
				OnTitleChanged?.Invoke(this, EventArgs.Empty);
			}
			get { return _path; }
		}
		private string _path;

		/// <summary>
		/// 編集されているならtrue.
		/// </summary>
		public bool IsDirty
		{
			set
			{
				this._isDirty = value;
				OnDirtyChanged?.Invoke(this, EventArgs.Empty);
				OnTitleChanged?.Invoke(this, EventArgs.Empty);
			}
			get { return _isDirty; }
		}
		private bool _isDirty;

		/// <summary>
		/// 現在参照しているパスにファイルが存在するならtrue.
		/// </summary>
		public bool Exists
		{
			get { return File.Exists(Path); }
		}


		public SourceFile(string path)
		{
			this.Path = path;
			this.IsDirty = false;
		}

		/// <summary>
		/// 編集状態を表す文字列を付与した名前を返します.
		/// </summary>
		/// <param name="baseName"></param>
		/// <returns></returns>
		public string GetWindowTitle(string baseName)
		{
			return baseName + " - " + Path + (IsDirty ? "*" : "");
		}

		/// <summary>
		/// まだ保存されていないとき例外をスローします.
		/// <exception cref="NotSavedSourceException">まだ保存されていないとき</exception>
		/// </summary>
		public void Throw()
		{
			if(IsDirty)
			{
				throw new NotSavedSourceException();
			}
		}

		/// <summary>
		/// ファイルを新規作成します.
		/// </summary>
		/// <param name="path"></param>
		/// <exception cref="NotSavedSourceException">まだ保存されていないとき</exception>
		public void New(string path)
		{
			if(IsDirty)
			{
				throw new NotSavedSourceException();
			}
			this.Path = path;
			this.IsDirty = false;
		}

		/// <summary>
		/// ファイルを開きます.
		/// </summary>
		/// <param name="path"></param>
		/// <exception cref="NotSavedSourceException">まだ保存されていないとき</exception>
		public void Open(string path)
		{
			if(IsDirty)
			{
				throw new NotSavedSourceException();
			}
			New(path);
		}

		/// <summary>
		/// ファイルを編集します.
		/// </summary>
		public void Edit()
		{
			this.IsDirty = true;
		}

		/// <summary>
		/// 変更を破棄します.
		/// </summary>
		public void Clear()
		{
			this.IsDirty = false;
		}

		/// <summary>
		/// ファイルを保存します.
		/// </summary>
		public void Save()
		{
			this.IsDirty = false;
		}

		/// <summary>
		/// 名前を変更して保存します.
		/// </summary>
		/// <param name="path"></param>
		public void SaveAs(string path)
		{
			this.Path = path;
			Save();
		}
	}
}
