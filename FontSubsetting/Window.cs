using System;
using System.Collections;
using System.Drawing;
using System.Drawing.Text;
using System.IO;
using System.Text;
using System.Windows.Forms;
using System.Windows.Media;
using Ionic.Zip;
using System.Collections.Generic;

namespace FontSubsetting
{
	public partial class Window : Form
	{
		public Window()
		{
			InitializeComponent();
		}

		public void CreateSubSet(string sourceText, Uri fontURI)
		{
			GlyphTypeface glyphTypeface = new GlyphTypeface(fontURI);
			System.Collections.Generic.ICollection<ushort> Index = null;
			Index = new System.Collections.Generic.List<ushort>();
			Encoding e = Encoding.Unicode;
			byte[] sourceTextBytes = e.GetBytes(sourceText);
			char[] sourceTextChars = e.GetChars(sourceTextBytes);
			int sourceTextCharVal = 0;
			int glyphIndex = 0;
			for (int sourceTextCharPos = 0; sourceTextCharPos <= sourceTextChars.GetUpperBound(0); sourceTextCharPos++)
			{
				try
				{
					sourceTextCharVal = Convert.ToInt32(sourceTextChars[sourceTextCharPos]);
					glyphIndex = glyphTypeface.CharacterToGlyphMap[sourceTextCharVal];
					Index.Add((ushort)glyphIndex);
				}
				catch
				{
					// character not in CharacterToGlyphMap!
				}
			}
			byte[] filebytes = glyphTypeface.ComputeSubset(Index);

			String fontPath = fontURI.AbsolutePath;
			int start = fontPath.LastIndexOf("/") + 1;
			int end = fontPath.LastIndexOf(".");
			int length = end - start;
			MessageBox.Show(fontPath.Substring(end));
			using (FileStream fileStream = new FileStream("D:\\Users\\Henry\\Desktop\\Kleymissky_0283-sub.otf", FileMode.Create))
			{
				fileStream.Write(filebytes, 0, filebytes.Length);
			}
		}

		private void Form1_Load(object sender, System.EventArgs e)
		{
			MessageBox.Show(FontNameFile.getFontFileName("方正魏碑简体"));

			/*String s1 = "123abc内容简介顺为凡逆则仙只在心中一念间修真到底是修的什么修真修仙修道吞魂神通霸气且看一少年几经转折以平庸的资质踏入仙途历经坎坷凭着其聪睿的心智迈向崎岖仙路夺基大法杀人夺宝尸阴秘宗域外战场古神之地一个平庸的山村少年几经转折最终踏入修仙门派他如何以平凡的资质修得仙法如何一步一步走向巅峰，跻身枭雄、宗师之列，谱下一曲逆天的仙道之路。";
			String s2 = "123abc内容简介\r\n顺为凡，逆则仙，只在心中一念间……\r\n修真，到底是修的什么？修真，修仙，修道。吞魂，神通，霸气。\r\n且看一少年，几经转折，以平庸的资质踏入仙途，历经坎坷，凭着其聪睿的心智，迈向崎岖仙路。\r\n夺基大法、杀人夺宝、尸阴秘宗、域外战场、古神之地……\r\n一个平庸的山村少年，几经转折最终踏入修仙门派，他如何以平凡的资质修得仙法，如何一步一步走向巅峰，跻身枭雄、宗师之列，谱下一曲逆天的仙道之路。";*/
			/*String s1 = "a";
			String s2 = "abcdefghijklmnokprstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";
			CreateSubSet(s1, new Uri("D:\\Users\\Henry\\Desktop\\Kleymissky_0283.otf"));
			PrivateFontCollection privateFontCollection = new PrivateFontCollection();
			privateFontCollection.AddFontFile("D:\\Users\\Henry\\Desktop\\Kleymissky_0283-sub.otf");
			Font regFont = new Font(privateFontCollection.Families[0], 20, FontStyle.Regular, GraphicsUnit.Pixel);
			this.textBox1.Font = regFont;
			this.textBox1.Text = s2;*/
		}

	}
}
