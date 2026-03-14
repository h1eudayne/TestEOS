using System;
using System.Collections.Generic;

namespace QuestionLib
{
	// Token: 0x02000008 RID: 8
	[Serializable]
	public class PaperSection
	{
		// Token: 0x17000027 RID: 39
		// (get) Token: 0x06000058 RID: 88 RVA: 0x0000266C File Offset: 0x0000166C
		// (set) Token: 0x06000059 RID: 89 RVA: 0x00002674 File Offset: 0x00001674
		public int SectionNo { get; set; }

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x0600005A RID: 90 RVA: 0x0000267D File Offset: 0x0000167D
		// (set) Token: 0x0600005B RID: 91 RVA: 0x00002685 File Offset: 0x00001685
		public int QFrom { get; set; }

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x0600005C RID: 92 RVA: 0x0000268E File Offset: 0x0000168E
		// (set) Token: 0x0600005D RID: 93 RVA: 0x00002696 File Offset: 0x00001696
		public int QTo { get; set; }

		// Token: 0x1700002A RID: 42
		// (get) Token: 0x0600005E RID: 94 RVA: 0x0000269F File Offset: 0x0000169F
		// (set) Token: 0x0600005F RID: 95 RVA: 0x000026A7 File Offset: 0x000016A7
		public string InAnyOrderGroup { get; set; }

		// Token: 0x1700002B RID: 43
		// (get) Token: 0x06000060 RID: 96 RVA: 0x000026B0 File Offset: 0x000016B0
		// (set) Token: 0x06000061 RID: 97 RVA: 0x000026B8 File Offset: 0x000016B8
		public List<ImagePaperAnswer> Answers { get; set; }

		// Token: 0x06000062 RID: 98 RVA: 0x000026C4 File Offset: 0x000016C4
		public int GetAnswerCount()
		{
			return this.QTo - this.QFrom + 1;
		}

		// Token: 0x06000063 RID: 99 RVA: 0x000026E5 File Offset: 0x000016E5
		public PaperSection()
		{
			this.Answers = new List<ImagePaperAnswer>();
		}
	}
}
