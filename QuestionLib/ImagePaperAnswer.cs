using System;

namespace QuestionLib
{
	// Token: 0x02000007 RID: 7
	[Serializable]
	public class ImagePaperAnswer
	{
		// Token: 0x17000021 RID: 33
		// (get) Token: 0x0600004B RID: 75 RVA: 0x00002606 File Offset: 0x00001606
		// (set) Token: 0x0600004C RID: 76 RVA: 0x0000260E File Offset: 0x0000160E
		public string Answer { get; set; }

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x0600004D RID: 77 RVA: 0x00002617 File Offset: 0x00001617
		// (set) Token: 0x0600004E RID: 78 RVA: 0x0000261F File Offset: 0x0000161F
		public int PartCount { get; set; }

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x0600004F RID: 79 RVA: 0x00002628 File Offset: 0x00001628
		// (set) Token: 0x06000050 RID: 80 RVA: 0x00002630 File Offset: 0x00001630
		public bool IsLongAnswer { get; set; }

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x06000051 RID: 81 RVA: 0x00002639 File Offset: 0x00001639
		// (set) Token: 0x06000052 RID: 82 RVA: 0x00002641 File Offset: 0x00001641
		public float SectionMark { get; set; }

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x06000053 RID: 83 RVA: 0x0000264A File Offset: 0x0000164A
		// (set) Token: 0x06000054 RID: 84 RVA: 0x00002652 File Offset: 0x00001652
		public bool SectionBoolean { get; set; }

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x06000055 RID: 85 RVA: 0x0000265B File Offset: 0x0000165B
		// (set) Token: 0x06000056 RID: 86 RVA: 0x00002663 File Offset: 0x00001663
		public int NoOfSection { get; set; }
	}
}
