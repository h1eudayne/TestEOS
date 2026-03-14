using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Text;
using System.Windows.Forms;

namespace EOSClient
{
	// Token: 0x02000005 RID: 5
	public partial class frmCheckFont : Form
	{
		// Token: 0x0600001E RID: 30 RVA: 0x00003C61 File Offset: 0x00001E61
		public frmCheckFont()
		{
			this.InitializeComponent();
		}

		// Token: 0x0600001F RID: 31 RVA: 0x00003C7C File Offset: 0x00001E7C
		private bool checkFont(string fontName)
		{
			bool flag2;
			using (Font f = new Font(fontName, 12f, FontStyle.Regular))
			{
				bool flag = f.Name.Equals(fontName);
				if (flag)
				{
					flag2 = true;
				}
				else
				{
					flag2 = false;
				}
			}
			return flag2;
		}

		// Token: 0x06000020 RID: 32 RVA: 0x00003CCC File Offset: 0x00001ECC
		private void frmCheckFont_Load(object sender, EventArgs e)
		{
			bool isSimSunExists = false;
			bool isKaiTiExists = false;
			bool isMinchoExists = false;
			bool isHGSeiExists = false;
			bool isNtMotoyaExists = false;
			InstalledFontCollection installedFontCollection = new InstalledFontCollection();
			FontFamily[] fontFamilies = installedFontCollection.Families;
			foreach (FontFamily ff in fontFamilies)
			{
				string fname = ff.GetName(0).Trim().ToUpper();
				bool flag = fname.StartsWith("SimSun".ToUpper());
				if (flag)
				{
					isSimSunExists = true;
				}
				bool flag2 = fname.StartsWith("KaiTi".ToUpper());
				if (flag2)
				{
					isKaiTiExists = true;
				}
				bool flag3 = fname.StartsWith("Ms Mincho".ToUpper());
				if (flag3)
				{
					isMinchoExists = true;
				}
				bool flag4 = fname.StartsWith("HGSeikai".ToUpper());
				if (flag4)
				{
					isHGSeiExists = true;
				}
				bool flag5 = fname.StartsWith("NtMotoya".ToUpper());
				if (flag5)
				{
					isNtMotoyaExists = true;
				}
			}
			string str = "CHECK FONT RESULT:\r\n\r\n";
			string ss_font = "SimSun";
			bool flag6 = isSimSunExists;
			if (flag6)
			{
				str = str + "Chinese font ('" + ss_font + "') : OK.\r\n\r\n";
			}
			else
			{
				str = str + "Chinese font ('" + ss_font + "') : NOT FOUND.\r\n\r\n";
			}
			string cn_font = "KaiTi";
			bool flag7 = isKaiTiExists;
			if (flag7)
			{
				str = str + "Chinese font ('" + cn_font + "') : OK.\r\n\r\n";
			}
			else
			{
				str = str + "Chinese font ('" + cn_font + "') : NOT FOUND.\r\n\r\n";
			}
			string jp_font = "MS Mincho";
			bool flag8 = isMinchoExists;
			if (flag8)
			{
				str = str + "Japanese font 1 ('" + jp_font + "') : OK.\r\n\r\n";
			}
			else
			{
				str = str + "Japanese font 1 ('" + jp_font + "') :  NOT FOUND.\r\n\r\n";
			}
			jp_font = "HGSeikaishotaiPRO";
			bool flag9 = isHGSeiExists;
			if (flag9)
			{
				str = str + "Japanese font 2 ('" + jp_font + "') : OK.\r\n\r\n";
			}
			else
			{
				str = str + "Japanese font 2 ('" + jp_font + "') :  NOT FOUND.\r\n\r\n";
			}
			jp_font = "NtMotoya Kyotai";
			bool flag10 = isNtMotoyaExists;
			if (flag10)
			{
				str = str + "Japanese font 3 ('" + jp_font + "') : OK.\r\n\r\n";
			}
			else
			{
				str = str + "Japanese font 3 ('" + jp_font + "') :  NOT FOUND.\r\n\r\n";
			}
			bool flag11 = !isSimSunExists || !isMinchoExists || !isKaiTiExists || !isHGSeiExists || !isNtMotoyaExists;
			if (flag11)
			{
				str += "\r\n\r\nINSTALLING FONTS ON Windows:\r\n\r\nThere are several ways to install fonts on Windows.\r\nKeep in mind that you must be an Administrator on the target machine to install fonts.\r\n\r\n - Download the font.\r\n - Double-click on a font file to open the font preview and select 'Install'.\r\n\r\nOR\r\n\r\n - Right-click on a font file, and then select 'Install'.";
			}
			this.txtFontGuide.Text = str;
		}

		// Token: 0x06000021 RID: 33 RVA: 0x00003F0E File Offset: 0x0000210E
		private void btnClose_Click(object sender, EventArgs e)
		{
			base.Close();
		}
	}
}
