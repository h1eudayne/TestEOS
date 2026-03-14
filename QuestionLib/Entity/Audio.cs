using System;

namespace QuestionLib.Entity
{
	// Token: 0x0200000E RID: 14
	[Serializable]
	public class Audio
	{
		// Token: 0x0600009C RID: 156 RVA: 0x00003936 File Offset: 0x00002936
		public Audio()
		{
			this._audioData = null;
		}

		// Token: 0x17000039 RID: 57
		// (get) Token: 0x0600009D RID: 157 RVA: 0x00003948 File Offset: 0x00002948
		// (set) Token: 0x0600009E RID: 158 RVA: 0x00003960 File Offset: 0x00002960
		public int AuID
		{
			get
			{
				return this._auID;
			}
			set
			{
				this._auID = value;
			}
		}

		// Token: 0x1700003A RID: 58
		// (get) Token: 0x0600009F RID: 159 RVA: 0x0000396C File Offset: 0x0000296C
		// (set) Token: 0x060000A0 RID: 160 RVA: 0x00003984 File Offset: 0x00002984
		public int ChID
		{
			get
			{
				return this._chID;
			}
			set
			{
				this._chID = value;
			}
		}

		// Token: 0x1700003B RID: 59
		// (get) Token: 0x060000A1 RID: 161 RVA: 0x00003990 File Offset: 0x00002990
		// (set) Token: 0x060000A2 RID: 162 RVA: 0x000039A8 File Offset: 0x000029A8
		public string AudioFile
		{
			get
			{
				return this._audioFile;
			}
			set
			{
				this._audioFile = value;
			}
		}

		// Token: 0x1700003C RID: 60
		// (get) Token: 0x060000A3 RID: 163 RVA: 0x000039B4 File Offset: 0x000029B4
		// (set) Token: 0x060000A4 RID: 164 RVA: 0x000039CC File Offset: 0x000029CC
		public int AudioSize
		{
			get
			{
				return this._audioSize;
			}
			set
			{
				this._audioSize = value;
			}
		}

		// Token: 0x1700003D RID: 61
		// (get) Token: 0x060000A5 RID: 165 RVA: 0x000039D8 File Offset: 0x000029D8
		// (set) Token: 0x060000A6 RID: 166 RVA: 0x000039F0 File Offset: 0x000029F0
		public byte[] AudioData
		{
			get
			{
				return this._audioData;
			}
			set
			{
				this._audioData = value;
			}
		}

		// Token: 0x1700003E RID: 62
		// (get) Token: 0x060000A7 RID: 167 RVA: 0x000039FC File Offset: 0x000029FC
		// (set) Token: 0x060000A8 RID: 168 RVA: 0x00003A14 File Offset: 0x00002A14
		public int AudioLength
		{
			get
			{
				return this._audioLength;
			}
			set
			{
				this._audioLength = value;
			}
		}

		// Token: 0x1700003F RID: 63
		// (get) Token: 0x060000A9 RID: 169 RVA: 0x00003A20 File Offset: 0x00002A20
		// (set) Token: 0x060000AA RID: 170 RVA: 0x00003A38 File Offset: 0x00002A38
		public byte RepeatTime
		{
			get
			{
				return this._repeatTime;
			}
			set
			{
				this._repeatTime = value;
			}
		}

		// Token: 0x17000040 RID: 64
		// (get) Token: 0x060000AB RID: 171 RVA: 0x00003A44 File Offset: 0x00002A44
		// (set) Token: 0x060000AC RID: 172 RVA: 0x00003A5C File Offset: 0x00002A5C
		public int PaddingTime
		{
			get
			{
				return this._paddingTime;
			}
			set
			{
				this._paddingTime = value;
			}
		}

		// Token: 0x17000041 RID: 65
		// (get) Token: 0x060000AD RID: 173 RVA: 0x00003A68 File Offset: 0x00002A68
		// (set) Token: 0x060000AE RID: 174 RVA: 0x00003A80 File Offset: 0x00002A80
		public byte PlayOrder
		{
			get
			{
				return this._playOrder;
			}
			set
			{
				this._playOrder = value;
			}
		}

		// Token: 0x04000055 RID: 85
		private int _auID;

		// Token: 0x04000056 RID: 86
		private int _chID;

		// Token: 0x04000057 RID: 87
		private string _audioFile;

		// Token: 0x04000058 RID: 88
		private int _audioSize;

		// Token: 0x04000059 RID: 89
		private byte[] _audioData;

		// Token: 0x0400005A RID: 90
		private int _audioLength;

		// Token: 0x0400005B RID: 91
		private byte _repeatTime;

		// Token: 0x0400005C RID: 92
		private int _paddingTime;

		// Token: 0x0400005D RID: 93
		private byte _playOrder;
	}
}
