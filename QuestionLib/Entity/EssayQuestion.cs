using System;

namespace QuestionLib.Entity
{
	// Token: 0x02000011 RID: 17
	[Serializable]
	public class EssayQuestion
	{
		// Token: 0x1700004B RID: 75
		// (get) Token: 0x060000C6 RID: 198 RVA: 0x00003C46 File Offset: 0x00002C46
		// (set) Token: 0x060000C7 RID: 199 RVA: 0x00003C4E File Offset: 0x00002C4E
		public int EQID { get; set; }

		// Token: 0x1700004C RID: 76
		// (get) Token: 0x060000C8 RID: 200 RVA: 0x00003C57 File Offset: 0x00002C57
		// (set) Token: 0x060000C9 RID: 201 RVA: 0x00003C5F File Offset: 0x00002C5F
		public string CourseId { get; set; }

		// Token: 0x1700004D RID: 77
		// (get) Token: 0x060000CA RID: 202 RVA: 0x00003C68 File Offset: 0x00002C68
		// (set) Token: 0x060000CB RID: 203 RVA: 0x00003C70 File Offset: 0x00002C70
		public int ChapterId { get; set; }

		// Token: 0x1700004E RID: 78
		// (get) Token: 0x060000CC RID: 204 RVA: 0x00003C79 File Offset: 0x00002C79
		// (set) Token: 0x060000CD RID: 205 RVA: 0x00003C81 File Offset: 0x00002C81
		public byte[] Question { get; set; }

		// Token: 0x1700004F RID: 79
		// (get) Token: 0x060000CE RID: 206 RVA: 0x00003C8A File Offset: 0x00002C8A
		// (set) Token: 0x060000CF RID: 207 RVA: 0x00003C92 File Offset: 0x00002C92
		public int QuestionFileSize { get; set; }

		// Token: 0x17000050 RID: 80
		// (get) Token: 0x060000D0 RID: 208 RVA: 0x00003C9B File Offset: 0x00002C9B
		// (set) Token: 0x060000D1 RID: 209 RVA: 0x00003CA3 File Offset: 0x00002CA3
		public string Name { get; set; }

		// Token: 0x17000051 RID: 81
		// (get) Token: 0x060000D2 RID: 210 RVA: 0x00003CAC File Offset: 0x00002CAC
		// (set) Token: 0x060000D3 RID: 211 RVA: 0x00003CB4 File Offset: 0x00002CB4
		public byte[] Guide2Mark { get; set; }

		// Token: 0x17000052 RID: 82
		// (get) Token: 0x060000D4 RID: 212 RVA: 0x00003CBD File Offset: 0x00002CBD
		// (set) Token: 0x060000D5 RID: 213 RVA: 0x00003CC5 File Offset: 0x00002CC5
		public int Guide2MarkFileSize { get; set; }

		// Token: 0x17000053 RID: 83
		// (get) Token: 0x060000D6 RID: 214 RVA: 0x00003CCE File Offset: 0x00002CCE
		// (set) Token: 0x060000D7 RID: 215 RVA: 0x00003CD6 File Offset: 0x00002CD6
		public string Development { get; set; }

		// Token: 0x17000054 RID: 84
		// (get) Token: 0x060000D8 RID: 216 RVA: 0x00003CE0 File Offset: 0x00002CE0
		// (set) Token: 0x060000D9 RID: 217 RVA: 0x00003CF8 File Offset: 0x00002CF8
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

		// Token: 0x060000DA RID: 218 RVA: 0x00003D02 File Offset: 0x00002D02
		public void Preapare2Submit()
		{
			this.CourseId = null;
			this.Question = null;
			this.Name = null;
			this.Guide2Mark = null;
		}

		// Token: 0x04000070 RID: 112
		private int _QBID;
	}
}
