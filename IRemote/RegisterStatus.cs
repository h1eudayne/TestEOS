using System;

namespace IRemote
{
	// Token: 0x02000005 RID: 5
	public enum RegisterStatus
	{
		// Token: 0x04000011 RID: 17
		NEW,
		// Token: 0x04000012 RID: 18
		RE_ASSIGN,
		// Token: 0x04000013 RID: 19
		FINISHED,
		// Token: 0x04000014 RID: 20
		REGISTERED,
		// Token: 0x04000015 RID: 21
		REGISTER_ERROR,
		// Token: 0x04000016 RID: 22
		EXAM_CODE_NOT_EXISTS,
		// Token: 0x04000017 RID: 23
		NOT_ALLOW_MACHINE,
		// Token: 0x04000018 RID: 24
		NOT_ALLOW_STUDENT,
		// Token: 0x04000019 RID: 25
		LOGIN_FAILED,
		// Token: 0x0400001A RID: 26
		CLIENT_OUTDATED
	}
}
