using System;

namespace QuestionLib
{
	// Token: 0x0200000B RID: 11
	[Serializable]
	public class AudioInPaper : IComparable<AudioInPaper>
	{
		// Token: 0x17000033 RID: 51
		// (get) Token: 0x06000078 RID: 120 RVA: 0x000027CC File Offset: 0x000017CC
		// (set) Token: 0x06000079 RID: 121 RVA: 0x000027E4 File Offset: 0x000017E4
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

		// Token: 0x17000034 RID: 52
		// (get) Token: 0x0600007A RID: 122 RVA: 0x000027F0 File Offset: 0x000017F0
		// (set) Token: 0x0600007B RID: 123 RVA: 0x00002808 File Offset: 0x00001808
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

		// Token: 0x17000035 RID: 53
		// (get) Token: 0x0600007C RID: 124 RVA: 0x00002814 File Offset: 0x00001814
		// (set) Token: 0x0600007D RID: 125 RVA: 0x0000282C File Offset: 0x0000182C
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

		// Token: 0x17000036 RID: 54
		// (get) Token: 0x0600007E RID: 126 RVA: 0x00002838 File Offset: 0x00001838
		// (set) Token: 0x0600007F RID: 127 RVA: 0x00002850 File Offset: 0x00001850
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

		// Token: 0x17000037 RID: 55
		// (get) Token: 0x06000080 RID: 128 RVA: 0x0000285C File Offset: 0x0000185C
		// (set) Token: 0x06000081 RID: 129 RVA: 0x00002874 File Offset: 0x00001874
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

		// Token: 0x17000038 RID: 56
		// (get) Token: 0x06000082 RID: 130 RVA: 0x00002880 File Offset: 0x00001880
		// (set) Token: 0x06000083 RID: 131 RVA: 0x00002898 File Offset: 0x00001898
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

		// Token: 0x06000084 RID: 132 RVA: 0x000028A4 File Offset: 0x000018A4
		public int CompareTo(AudioInPaper aip)
		{
			return this.PlayOrder.CompareTo(aip.PlayOrder);
		}

		// Token: 0x0400003B RID: 59
		private int _audioSize;

		// Token: 0x0400003C RID: 60
		private byte[] _audioData;

		// Token: 0x0400003D RID: 61
		private int _audioLength;

		// Token: 0x0400003E RID: 62
		private byte _repeatTime;

		// Token: 0x0400003F RID: 63
		private int _paddingTime;

		// Token: 0x04000040 RID: 64
		private byte _playOrder;
	}
}
