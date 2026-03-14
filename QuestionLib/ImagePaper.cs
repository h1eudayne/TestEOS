using System;
using System.Collections.Generic;

namespace QuestionLib
{
	// Token: 0x0200000A RID: 10
	[Serializable]
	public class ImagePaper
	{
		// Token: 0x1700002F RID: 47
		// (get) Token: 0x0600006E RID: 110 RVA: 0x0000277C File Offset: 0x0000177C
		// (set) Token: 0x0600006F RID: 111 RVA: 0x00002784 File Offset: 0x00001784
		public List<PaperSection> Sections { get; set; }

		// Token: 0x17000030 RID: 48
		// (get) Token: 0x06000070 RID: 112 RVA: 0x0000278D File Offset: 0x0000178D
		// (set) Token: 0x06000071 RID: 113 RVA: 0x00002795 File Offset: 0x00001795
		public byte[] Image { get; set; }

		// Token: 0x17000031 RID: 49
		// (get) Token: 0x06000072 RID: 114 RVA: 0x0000279E File Offset: 0x0000179E
		// (set) Token: 0x06000073 RID: 115 RVA: 0x000027A6 File Offset: 0x000017A6
		public int NumberOfPage { get; set; }

		// Token: 0x17000032 RID: 50
		// (get) Token: 0x06000074 RID: 116 RVA: 0x000027AF File Offset: 0x000017AF
		// (set) Token: 0x06000075 RID: 117 RVA: 0x000027B7 File Offset: 0x000017B7
		public string LongAnswerGuide { get; set; }

		// Token: 0x06000076 RID: 118 RVA: 0x000027C0 File Offset: 0x000017C0
		public void Preapare2Submit()
		{
			this.Image = null;
		}
	}
}
