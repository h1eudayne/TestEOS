using System;
using System.Collections;

namespace QuestionLib.Entity
{
	// Token: 0x02000014 RID: 20
	[Serializable]
	public class MatchQuestion
	{
		// Token: 0x1700005F RID: 95
		// (get) Token: 0x060000F3 RID: 243 RVA: 0x00003EB8 File Offset: 0x00002EB8
		// (set) Token: 0x060000F4 RID: 244 RVA: 0x00003ED0 File Offset: 0x00002ED0
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

		// Token: 0x060000F5 RID: 245 RVA: 0x00003EDA File Offset: 0x00002EDA
		public MatchQuestion()
		{
			this._questionLOs = new ArrayList();
		}

		// Token: 0x17000060 RID: 96
		// (get) Token: 0x060000F6 RID: 246 RVA: 0x00003EF0 File Offset: 0x00002EF0
		// (set) Token: 0x060000F7 RID: 247 RVA: 0x00003F08 File Offset: 0x00002F08
		public int MID
		{
			get
			{
				return this._mid;
			}
			set
			{
				this._mid = value;
			}
		}

		// Token: 0x17000061 RID: 97
		// (get) Token: 0x060000F8 RID: 248 RVA: 0x00003F14 File Offset: 0x00002F14
		// (set) Token: 0x060000F9 RID: 249 RVA: 0x00003F2C File Offset: 0x00002F2C
		public string CourseId
		{
			get
			{
				return this._courseId;
			}
			set
			{
				this._courseId = value;
			}
		}

		// Token: 0x17000062 RID: 98
		// (get) Token: 0x060000FA RID: 250 RVA: 0x00003F38 File Offset: 0x00002F38
		// (set) Token: 0x060000FB RID: 251 RVA: 0x00003F50 File Offset: 0x00002F50
		public int ChapterId
		{
			get
			{
				return this._chapterId;
			}
			set
			{
				this._chapterId = value;
			}
		}

		// Token: 0x17000063 RID: 99
		// (get) Token: 0x060000FC RID: 252 RVA: 0x00003F5C File Offset: 0x00002F5C
		// (set) Token: 0x060000FD RID: 253 RVA: 0x00003F74 File Offset: 0x00002F74
		public string ColumnA
		{
			get
			{
				return this._columnA;
			}
			set
			{
				this._columnA = value;
			}
		}

		// Token: 0x17000064 RID: 100
		// (get) Token: 0x060000FE RID: 254 RVA: 0x00003F80 File Offset: 0x00002F80
		// (set) Token: 0x060000FF RID: 255 RVA: 0x00003F98 File Offset: 0x00002F98
		public string ColumnB
		{
			get
			{
				return this._columnB;
			}
			set
			{
				this._columnB = value;
			}
		}

		// Token: 0x17000065 RID: 101
		// (get) Token: 0x06000100 RID: 256 RVA: 0x00003FA4 File Offset: 0x00002FA4
		// (set) Token: 0x06000101 RID: 257 RVA: 0x00003FBC File Offset: 0x00002FBC
		public string Solution
		{
			get
			{
				return this._solution;
			}
			set
			{
				this._solution = value;
			}
		}

		// Token: 0x17000066 RID: 102
		// (get) Token: 0x06000102 RID: 258 RVA: 0x00003FC8 File Offset: 0x00002FC8
		// (set) Token: 0x06000103 RID: 259 RVA: 0x00003FE0 File Offset: 0x00002FE0
		public float Mark
		{
			get
			{
				return this._mark;
			}
			set
			{
				this._mark = value;
			}
		}

		// Token: 0x17000067 RID: 103
		// (get) Token: 0x06000104 RID: 260 RVA: 0x00003FEC File Offset: 0x00002FEC
		// (set) Token: 0x06000105 RID: 261 RVA: 0x00004004 File Offset: 0x00003004
		public string SudentAnswer
		{
			get
			{
				return this._studentAnswer;
			}
			set
			{
				this._studentAnswer = value;
			}
		}

		// Token: 0x17000068 RID: 104
		// (get) Token: 0x06000106 RID: 262 RVA: 0x00004010 File Offset: 0x00003010
		// (set) Token: 0x06000107 RID: 263 RVA: 0x00004028 File Offset: 0x00003028
		public ArrayList QuestionLOs
		{
			get
			{
				return this._questionLOs;
			}
			set
			{
				this._questionLOs = value;
			}
		}

		// Token: 0x06000108 RID: 264 RVA: 0x00004034 File Offset: 0x00003034
		public override string ToString()
		{
			return this._mid.ToString();
		}

		// Token: 0x06000109 RID: 265 RVA: 0x00004051 File Offset: 0x00003051
		public void Preapare2Submit()
		{
			this.Solution = null;
			this.ColumnA = null;
			this.ColumnB = null;
			this.CourseId = null;
		}

		// Token: 0x0400007B RID: 123
		private int _mid;

		// Token: 0x0400007C RID: 124
		private string _courseId;

		// Token: 0x0400007D RID: 125
		private int _chapterId;

		// Token: 0x0400007E RID: 126
		private string _columnA;

		// Token: 0x0400007F RID: 127
		private string _columnB;

		// Token: 0x04000080 RID: 128
		private string _solution;

		// Token: 0x04000081 RID: 129
		private float _mark;

		// Token: 0x04000082 RID: 130
		private string _studentAnswer;

		// Token: 0x04000083 RID: 131
		private ArrayList _questionLOs;

		// Token: 0x04000084 RID: 132
		private int _QBID;
	}
}
