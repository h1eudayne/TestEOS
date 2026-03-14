using System;

namespace QuestionLib
{
	// Token: 0x02000009 RID: 9
	[Serializable]
	public class ImageRef
	{
		// Token: 0x1700002C RID: 44
		// (get) Token: 0x06000064 RID: 100 RVA: 0x000026FB File Offset: 0x000016FB
		// (set) Token: 0x06000065 RID: 101 RVA: 0x00002703 File Offset: 0x00001703
		public byte[] ImgData { get; set; }

		// Token: 0x06000066 RID: 102 RVA: 0x0000270C File Offset: 0x0000170C
		public byte[] Get_ImgData()
		{
			return this.ImgData;
		}

		// Token: 0x1700002D RID: 45
		// (get) Token: 0x06000067 RID: 103 RVA: 0x00002724 File Offset: 0x00001724
		// (set) Token: 0x06000068 RID: 104 RVA: 0x0000272C File Offset: 0x0000172C
		public int ImgSize { get; set; }

		// Token: 0x06000069 RID: 105 RVA: 0x00002738 File Offset: 0x00001738
		public int Get_ImgSize()
		{
			return this.ImgSize;
		}

		// Token: 0x1700002E RID: 46
		// (get) Token: 0x0600006A RID: 106 RVA: 0x00002750 File Offset: 0x00001750
		// (set) Token: 0x0600006B RID: 107 RVA: 0x00002758 File Offset: 0x00001758
		public int NumberOfPage { get; set; }

		// Token: 0x0600006C RID: 108 RVA: 0x00002764 File Offset: 0x00001764
		public int Get_NumberOfPage()
		{
			return this.NumberOfPage;
		}
	}
}
