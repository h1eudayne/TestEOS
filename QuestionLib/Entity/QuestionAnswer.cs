using System;

namespace QuestionLib.Entity
{
	// Token: 0x02000018 RID: 24
	[Serializable]
	public class QuestionAnswer
	{
		// Token: 0x1700007C RID: 124
		// (get) Token: 0x06000138 RID: 312 RVA: 0x00004508 File Offset: 0x00003508
		// (set) Token: 0x06000139 RID: 313 RVA: 0x00004520 File Offset: 0x00003520
		public int QBID
		{
			get
			{
				return this._QBID;
			}
			set
			{
				this._QBID = value;
			}
		}

		// Token: 0x0600013A RID: 314 RVA: 0x00003A8A File Offset: 0x00002A8A
		public QuestionAnswer()
		{
		}

		// Token: 0x0600013B RID: 315 RVA: 0x0000452A File Offset: 0x0000352A
		public QuestionAnswer(string text, bool chosen)
		{
			this._text = text;
			this._chosen = chosen;
		}

		// Token: 0x1700007D RID: 125
		// (get) Token: 0x0600013C RID: 316 RVA: 0x00004544 File Offset: 0x00003544
		// (set) Token: 0x0600013D RID: 317 RVA: 0x0000455C File Offset: 0x0000355C
		public int QAID
		{
			get
			{
				return this._qaid;
			}
			set
			{
				this._qaid = value;
			}
		}

		// Token: 0x1700007E RID: 126
		// (get) Token: 0x0600013E RID: 318 RVA: 0x00004568 File Offset: 0x00003568
		// (set) Token: 0x0600013F RID: 319 RVA: 0x00004580 File Offset: 0x00003580
		public int QID
		{
			get
			{
				return this._qid;
			}
			set
			{
				this._qid = value;
			}
		}

		// Token: 0x1700007F RID: 127
		// (get) Token: 0x06000140 RID: 320 RVA: 0x0000458C File Offset: 0x0000358C
		// (set) Token: 0x06000141 RID: 321 RVA: 0x000045A4 File Offset: 0x000035A4
		public string Text
		{
			get
			{
				return this._text;
			}
			set
			{
				this._text = value;
			}
		}

		// Token: 0x17000080 RID: 128
		// (get) Token: 0x06000142 RID: 322 RVA: 0x000045B0 File Offset: 0x000035B0
		// (set) Token: 0x06000143 RID: 323 RVA: 0x000045C8 File Offset: 0x000035C8
		public bool Chosen
		{
			get
			{
				return this._chosen;
			}
			set
			{
				this._chosen = value;
			}
		}

		// Token: 0x17000081 RID: 129
		// (get) Token: 0x06000144 RID: 324 RVA: 0x000045D4 File Offset: 0x000035D4
		// (set) Token: 0x06000145 RID: 325 RVA: 0x000045EC File Offset: 0x000035EC
		public bool Selected
		{
			get
			{
				return this._selected;
			}
			set
			{
				this._selected = value;
			}
		}

		// Token: 0x17000082 RID: 130
		// (get) Token: 0x06000146 RID: 326 RVA: 0x000045F8 File Offset: 0x000035F8
		// (set) Token: 0x06000147 RID: 327 RVA: 0x00004610 File Offset: 0x00003610
		public bool Done
		{
			get
			{
				return this._done;
			}
			set
			{
				this._done = value;
			}
		}

		// Token: 0x040000A0 RID: 160
		private int _qaid;

		// Token: 0x040000A1 RID: 161
		private int _qid;

		// Token: 0x040000A2 RID: 162
		private string _text;

		// Token: 0x040000A3 RID: 163
		private bool _chosen;

		// Token: 0x040000A4 RID: 164
		private bool _selected;

		// Token: 0x040000A5 RID: 165
		private bool _done;

		// Token: 0x040000A6 RID: 166
		private int _QBID;
	}
}
