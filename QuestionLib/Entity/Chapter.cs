using System;

namespace QuestionLib.Entity
{
	// Token: 0x0200000F RID: 15
	public class Chapter
	{
		// Token: 0x060000AF RID: 175 RVA: 0x00003A8A File Offset: 0x00002A8A
		public Chapter()
		{
		}

		// Token: 0x060000B0 RID: 176 RVA: 0x00003A94 File Offset: 0x00002A94
		public Chapter(int _chid, string _cid, string _name)
		{
			this._chid = _chid;
			this._cid = _cid;
			this._name = _name;
		}

		// Token: 0x17000042 RID: 66
		// (get) Token: 0x060000B1 RID: 177 RVA: 0x00003AB4 File Offset: 0x00002AB4
		// (set) Token: 0x060000B2 RID: 178 RVA: 0x00003ACC File Offset: 0x00002ACC
		public int ChID
		{
			get
			{
				return this._chid;
			}
			set
			{
				this._chid = value;
			}
		}

		// Token: 0x17000043 RID: 67
		// (get) Token: 0x060000B3 RID: 179 RVA: 0x00003AD8 File Offset: 0x00002AD8
		// (set) Token: 0x060000B4 RID: 180 RVA: 0x00003AF0 File Offset: 0x00002AF0
		public string CID
		{
			get
			{
				return this._cid;
			}
			set
			{
				this._cid = value;
			}
		}

		// Token: 0x17000044 RID: 68
		// (get) Token: 0x060000B5 RID: 181 RVA: 0x00003AFC File Offset: 0x00002AFC
		// (set) Token: 0x060000B6 RID: 182 RVA: 0x00003B14 File Offset: 0x00002B14
		public string Name
		{
			get
			{
				return this._name;
			}
			set
			{
				this._name = value;
			}
		}

		// Token: 0x060000B7 RID: 183 RVA: 0x00003B20 File Offset: 0x00002B20
		public override string ToString()
		{
			return this._name;
		}

		// Token: 0x0400005E RID: 94
		private int _chid;

		// Token: 0x0400005F RID: 95
		private string _cid;

		// Token: 0x04000060 RID: 96
		private string _name;
	}
}
