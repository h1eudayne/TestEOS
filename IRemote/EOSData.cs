using System;
using QuestionLib;

namespace IRemote
{
	// Token: 0x02000003 RID: 3
	[Serializable]
	public class EOSData
	{
		// Token: 0x04000001 RID: 1
		public RegisterStatus Status;

		// Token: 0x04000002 RID: 2
		public Paper ExamPaper;

		// Token: 0x04000003 RID: 3
		public byte[] EOSPaper;

		// Token: 0x04000004 RID: 4
		public SubmitPaper StudentSubmitPaper;

		// Token: 0x04000005 RID: 5
		public byte[] GUI;

		// Token: 0x04000006 RID: 6
		public int OriginSize;

		// Token: 0x04000007 RID: 7
		public ServerInfo ServerInfomation;

		// Token: 0x04000008 RID: 8
		public RegisterData RegData;

		// Token: 0x04000009 RID: 9
		public byte[] ImageID;
	}
}
