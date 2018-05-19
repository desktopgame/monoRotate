using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Media;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Xna2D.Contents
{
	/// <summary>
	/// Song, SoundEffectを内包するMediaPlayerのラッパーです.
	/// </summary>
	public class Sound
	{
		private ContentManager contentManager;
		private Dictionary<string, SoundEffect> soundEffectDictionary;
		private Dictionary<string, Song> songDictionary;
		private string currentSong;

		public Sound(ContentManager contentManager)
		{
			this.contentManager = contentManager;
			this.soundEffectDictionary = new Dictionary<string, SoundEffect>();
			this.songDictionary = new Dictionary<string, Song>();
		}

		public void LoadBGM(string assetName)
		{
			songDictionary[assetName] = contentManager.Load<Song>(assetName);
		}

		public void LoadSoundEffect(string assetName)
		{
			soundEffectDictionary[assetName] = contentManager.Load<SoundEffect>(assetName);
		}

		public void Unload()
		{
			soundEffectDictionary.Clear();
			songDictionary.Clear();
		}

		#region BGM
		/// <summary>
		/// BGMを再生します.
		/// </summary>
		/// <param name="assetName"></param>
		public void PlayBGM(string assetName)
		{
			MediaPlayer.Volume = 0.05f;
			//MediaPlayer.Volume = 0f;
			MediaPlayer.Play(songDictionary[assetName]);
		}

		/// <summary>
		/// BGMを終了します.
		/// </summary>
		public void StopBGM()
		{
			MediaPlayer.Stop();
		}

		/// <summary>
		/// BGMを一時停止します.
		/// </summary>
		public void PauseBGM()
		{
			MediaPlayer.Pause();
		}

		/// <summary>
		/// BGMを一時停止したところから再生します.
		/// </summary>
		public void ResumeBGM()
		{
			MediaPlayer.Resume();
		}
		#endregion

		/// <summary>
		/// 指定のエフェクトを再生します.
		/// </summary>
		/// <param name="assetName"></param>
		public void PlayEffect(string assetName)
		{
			soundEffectDictionary[assetName].Play();
		}

		/// <summary>
		/// 指定のエフェクトを作成します.
		/// </summary>
		/// <param name="assetName"></param>
		/// <returns></returns>
		public SoundEffectInstance CreateEffect(string assetName)
		{
			return soundEffectDictionary[assetName].CreateInstance();
		}
	}
}
