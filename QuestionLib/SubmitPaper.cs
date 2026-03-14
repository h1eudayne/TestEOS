using System;

namespace QuestionLib
{
	// Token: 0x0200000D RID: 13
	[Serializable]
	public class SubmitPaper
	{
		// Token: 0x0600009A RID: 154 RVA: 0x000038EC File Offset: 0x000028EC
		public override bool Equals(object obj)
		{
			SubmitPaper s = (SubmitPaper)obj;
			return this.ID.Equals(s.ID) && this.SPaper.ExamCode.Equals(s.SPaper.ExamCode);
		}

		// Token: 0x04000047 RID: 71
		public string LoginId;

		// Token: 0x04000048 RID: 72
		public int TimeLeft;

		// Token: 0x04000049 RID: 73
		public int IndexFill;

		// Token: 0x0400004A RID: 74
		public int IndexReading;

		// Token: 0x0400004B RID: 75
		public int IndexG;

		// Token: 0x0400004C RID: 76
		public int IndexIndiM;

		// Token: 0x0400004D RID: 77
		public int IndexMatch;

		// Token: 0x0400004E RID: 78
		public bool Finish;

		// Token: 0x0400004F RID: 79
		public Paper SPaper;

		// Token: 0x04000050 RID: 80
		public int TabIndex;

		// Token: 0x04000051 RID: 81
		public DateTime SubmitTime;

		// Token: 0x04000052 RID: 82
		public byte[] byteImage;

		// Token: 0x04000053 RID: 83
		public bool imageSaved;

		// Token: 0x04000054 RID: 84
		public string ID;
	}
}
