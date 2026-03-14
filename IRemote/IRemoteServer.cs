using System;
using QuestionLib;

namespace IRemote
{
	// Token: 0x02000007 RID: 7
	public interface IRemoteServer
	{
		// Token: 0x06000010 RID: 16
		EOSData ConductExam(RegisterData rd);

		// Token: 0x06000011 RID: 17
		SubmitStatus Submit(SubmitPaper submitPaper, ref string msg);

		// Token: 0x06000012 RID: 18
		bool Ping();
	}
}
