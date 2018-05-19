using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Xna2D.Contents;
using Xna2D.Scenes;
using Xna2D.Input;
using Microsoft.Xna.Framework.Input;
using System.Diagnostics;
using System.IO;

namespace TestGame.Scenes.Select
{
	/// <summary>
	/// ステージ選択.
	/// </summary>
	public class SelectScene : SceneBase, ILayered
	{
		private StageSelector stageSelector;
		private int selectedRow;
		private int selectedCol;
		private int tabIndex;
		private Sound sound;

		public static readonly Handle ENTER = new Handle(
			(index, mouse, key) => key.IsKeyDown(Keys.Space),
			(index, pad) => pad.Buttons.A == ButtonState.Pressed
		);
		private static readonly Vector2 STAGE_SELECT_POS = new Vector2(100, 50);
		private static readonly List<List<Vector2>> STAGE_PAGE_POS = new List<List<Vector2>>
		{
			new List<Vector2>() { STAGE_SELECT_POS + new Vector2(0, 100), STAGE_SELECT_POS + new Vector2(600, 100) },
			new List<Vector2>() { STAGE_SELECT_POS + new Vector2(0, 300), STAGE_SELECT_POS + new Vector2(600, 300) },
			new List<Vector2>() { STAGE_SELECT_POS + new Vector2(0, 500), STAGE_SELECT_POS + new Vector2(400, 500) , STAGE_SELECT_POS + new Vector2(800, 500) },
		};
		public SelectScene(StageSelector stageSelector, Sound sound)
		{
			this.stageSelector = stageSelector;
			this.Next = (int)SceneTypes.Play;
			this.selectedRow = 0;
			this.selectedCol = 0;
			this.tabIndex = 0;
			this.stageSelector.Index = GetIndex();
			this.sound = sound;
		}

		public override void Update(GameTime gameTime)
		{
			Detector detector = Detector.GetInstance();
			//選択項目の移動
			if(detector.IsDetect(Handle.UP))
			{
				this.selectedRow--;
			} else if(detector.IsDetect(Handle.DOWN))
			{
				this.selectedRow++;
			} else if(detector.IsDetect(Handle.LEFT))
			{
				this.selectedCol--;
			} else if(detector.IsDetect(Handle.RIGHT))
			{
				this.selectedCol++;
			}
			//はみ出ないように
			if(selectedRow >= STAGE_PAGE_POS.Count)
			{
				this.selectedRow = 0;
			} else if(selectedRow < 0)
			{
				this.selectedRow = STAGE_PAGE_POS.Count - 1;
			}
			if(selectedCol >= STAGE_PAGE_POS[selectedRow].Count)
			{
				this.selectedCol = 0;
			} else if(selectedCol < 0)
			{
				this.selectedCol = STAGE_PAGE_POS[selectedRow].Count - 1;
			}
			UpdateEnter();
		}

		/// <summary>
		/// 次の画面へ遷移します.
		/// </summary>
		private void UpdateEnter()
		{
			Detector detector = Detector.GetInstance();
			if(!detector.IsDetect(ENTER))
			{
				return;
			}
			if(selectedRow != 2)
			{
				//そのステージがない
				int index = GetIndex();
				//Debug.WriteLine(index);
				if(!File.Exists(stageSelector.GetPath(index)))
				{
					return;
				}
				this.Next = (int)SceneTypes.Play;
				this.IsEnd = true;
				return;
			}
			//最下段が選択されているので、プレイ画面以外へ遷移する
			if(selectedCol == 0)
			{
				this.Next = (int)SceneTypes.Title;
				this.IsEnd = true;
			}
			else if(selectedCol == 1)
			{
				if(tabIndex > 0)
				{
					this.tabIndex--;
				}
			}
			else if(selectedCol == 2)
			{
				tabIndex++;
			}
		}

		public override void Draw(GameTime gameTime, Renderer renderer)
		{
			renderer.Begin();
			renderer.Draw("Textures/Back/Black", Vector2.Zero, Color.White);
			renderer.Draw("Textures/Select/StageSelect", STAGE_SELECT_POS, Color.White);
			for(int i=0; i<STAGE_PAGE_POS.Count; i++)
			{
				for(int j=0; j<STAGE_PAGE_POS[i].Count; j++)
				{
					Vector2 pagePos = STAGE_PAGE_POS[i][j];
					renderer.Draw(GetAssetName(i, j), pagePos, Color.White);
					if(i == 2)
					{
						continue;
					}
					int index = ((tabIndex * 4) + (i * 2) + j) + 1;
				//	Debug.WriteLine(index);
					renderer.DrawNumber("Textures/NumberBlack30", pagePos + new Vector2(10, 10), Color.White, index, Resource.NUMBER_RECTANGLES);
				}
			}
			renderer.End();
		}

		public override void Show()
		{
			base.Show();
			this.tabIndex = 0;
			sound.PlayBGM("Sound/Song/Select");
		}

		public override void Hide()
		{
			base.Hide();
			this.stageSelector.Index = GetIndex();
			sound.StopBGM();
		}

		private string GetAssetName(int i, int j)
		{
			bool selected = (i == selectedRow && j == selectedCol);
			string ret = null;
			//最下段なので別アイコンを描画
			if(i == 2)
			{
				if(j == 0)
				{
					ret = "Textures/Select/Select_Back";
				} else if(j == 1)
				{
					ret = "Textures/Select/SelectPrevPage";
				} else if(j == 2)
				{
					ret = "Textures/Select/SelectNextPage";
				}
				if(selected)
				{
					ret += "_Active";
				}
				return ret;
			}
			//ステージ選択中
			bool exists = File.Exists(stageSelector.GetPath(GetIndex(i, j)));
			if(selected)
			{
				ret ="Textures/Select/PageSelected";
			} else
			{
				ret = "Textures/Select/Page";
			}
			if(!exists)
			{
				ret += "_Disable";
			}
			return ret;
		}

		private int GetIndex(int i, int j)
		{
			return ((tabIndex * 4) + (i * 2) + j) + 1;
		}

		private int GetIndex()
		{
			return GetIndex(selectedRow, selectedCol);
		}

		public bool IsNeedBackLayer(LayeredScene scene)
		{
			return false;
		}

		public bool IsNeedFrontLayer(LayeredScene scene)
		{
			return false;
		}
	}
}
