using System;

namespace QuestionLib.Entity
{
	// Token: 0x0200001B RID: 27
	public class TestTemplate
	{
		// Token: 0x1700008D RID: 141
		// (get) Token: 0x0600015E RID: 350 RVA: 0x00004782 File Offset: 0x00003782
		// (set) Token: 0x0600015F RID: 351 RVA: 0x0000478A File Offset: 0x0000378A
		public int TestTemplateID { get; set; }

		// Token: 0x1700008E RID: 142
		// (get) Token: 0x06000160 RID: 352 RVA: 0x00004793 File Offset: 0x00003793
		// (set) Token: 0x06000161 RID: 353 RVA: 0x0000479B File Offset: 0x0000379B
		public string CID { get; set; }

		// Token: 0x1700008F RID: 143
		// (get) Token: 0x06000162 RID: 354 RVA: 0x000047A4 File Offset: 0x000037A4
		// (set) Token: 0x06000163 RID: 355 RVA: 0x000047AC File Offset: 0x000037AC
		public string TestTemplateName { get; set; }

		// Token: 0x17000090 RID: 144
		// (get) Token: 0x06000164 RID: 356 RVA: 0x000047B5 File Offset: 0x000037B5
		// (set) Token: 0x06000165 RID: 357 RVA: 0x000047BD File Offset: 0x000037BD
		public string CreatedBy { get; set; }

		// Token: 0x17000091 RID: 145
		// (get) Token: 0x06000166 RID: 358 RVA: 0x000047C6 File Offset: 0x000037C6
		// (set) Token: 0x06000167 RID: 359 RVA: 0x000047CE File Offset: 0x000037CE
		public DateTime CreatedDate { get; set; }

		// Token: 0x17000092 RID: 146
		// (get) Token: 0x06000168 RID: 360 RVA: 0x000047D7 File Offset: 0x000037D7
		// (set) Token: 0x06000169 RID: 361 RVA: 0x000047DF File Offset: 0x000037DF
		public int DistinctWithLastTest { get; set; }

		// Token: 0x17000093 RID: 147
		// (get) Token: 0x0600016A RID: 362 RVA: 0x000047E8 File Offset: 0x000037E8
		// (set) Token: 0x0600016B RID: 363 RVA: 0x000047F0 File Offset: 0x000037F0
		public int Duration { get; set; }

		// Token: 0x17000094 RID: 148
		// (get) Token: 0x0600016C RID: 364 RVA: 0x000047F9 File Offset: 0x000037F9
		// (set) Token: 0x0600016D RID: 365 RVA: 0x00004801 File Offset: 0x00003801
		public string Note { get; set; }
	}
}
