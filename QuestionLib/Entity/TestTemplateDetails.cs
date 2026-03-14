using System;

namespace QuestionLib.Entity
{
	// Token: 0x0200001C RID: 28
	public class TestTemplateDetails
	{
		// Token: 0x17000095 RID: 149
		// (get) Token: 0x0600016F RID: 367 RVA: 0x0000480A File Offset: 0x0000380A
		// (set) Token: 0x06000170 RID: 368 RVA: 0x00004812 File Offset: 0x00003812
		public int TestTemplateDetailsID { get; set; }

		// Token: 0x17000096 RID: 150
		// (get) Token: 0x06000171 RID: 369 RVA: 0x0000481B File Offset: 0x0000381B
		// (set) Token: 0x06000172 RID: 370 RVA: 0x00004823 File Offset: 0x00003823
		public int TestTemplateID { get; set; }

		// Token: 0x17000097 RID: 151
		// (get) Token: 0x06000173 RID: 371 RVA: 0x0000482C File Offset: 0x0000382C
		// (set) Token: 0x06000174 RID: 372 RVA: 0x00004834 File Offset: 0x00003834
		public int ChapterId { get; set; }

		// Token: 0x17000098 RID: 152
		// (get) Token: 0x06000175 RID: 373 RVA: 0x0000483D File Offset: 0x0000383D
		// (set) Token: 0x06000176 RID: 374 RVA: 0x00004845 File Offset: 0x00003845
		public QuestionType QuestionType { get; set; }

		// Token: 0x17000099 RID: 153
		// (get) Token: 0x06000177 RID: 375 RVA: 0x0000484E File Offset: 0x0000384E
		// (set) Token: 0x06000178 RID: 376 RVA: 0x00004856 File Offset: 0x00003856
		public int NoQInTest { get; set; }
	}
}
