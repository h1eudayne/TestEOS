using System;

namespace QuestionLib.Entity
{
	// Token: 0x02000019 RID: 25
	public class QuestionLO
	{
		// Token: 0x17000083 RID: 131
		// (get) Token: 0x06000148 RID: 328 RVA: 0x0000461C File Offset: 0x0000361C
		// (set) Token: 0x06000149 RID: 329 RVA: 0x00004634 File Offset: 0x00003634
		public int QuestionLOID
		{
			get
			{
				return this._QuestionLOID;
			}
			set
			{
				this._QuestionLOID = value;
			}
		}

		// Token: 0x17000084 RID: 132
		// (get) Token: 0x0600014A RID: 330 RVA: 0x00004640 File Offset: 0x00003640
		// (set) Token: 0x0600014B RID: 331 RVA: 0x00004658 File Offset: 0x00003658
		public QuestionType QType
		{
			get
			{
				return this._QType;
			}
			set
			{
				this._QType = value;
			}
		}

		// Token: 0x17000085 RID: 133
		// (get) Token: 0x0600014C RID: 332 RVA: 0x00004664 File Offset: 0x00003664
		// (set) Token: 0x0600014D RID: 333 RVA: 0x0000467C File Offset: 0x0000367C
		public int QID
		{
			get
			{
				return this._QID;
			}
			set
			{
				this._QID = value;
			}
		}

		// Token: 0x17000086 RID: 134
		// (get) Token: 0x0600014E RID: 334 RVA: 0x00004688 File Offset: 0x00003688
		// (set) Token: 0x0600014F RID: 335 RVA: 0x000046A0 File Offset: 0x000036A0
		public int LOID
		{
			get
			{
				return this._LOID;
			}
			set
			{
				this._LOID = value;
			}
		}

		// Token: 0x040000A7 RID: 167
		private int _QuestionLOID;

		// Token: 0x040000A8 RID: 168
		private QuestionType _QType;

		// Token: 0x040000A9 RID: 169
		private int _QID;

		// Token: 0x040000AA RID: 170
		private int _LOID;
	}
}
