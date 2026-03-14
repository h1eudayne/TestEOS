using System;

namespace EOSClient
{
	// Token: 0x02000003 RID: 3
	public class EndpointItem
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x0600000D RID: 13 RVA: 0x0000375C File Offset: 0x0000195C
		// (set) Token: 0x0600000E RID: 14 RVA: 0x00003764 File Offset: 0x00001964
		public string Display { get; set; }

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x0600000F RID: 15 RVA: 0x0000376D File Offset: 0x0000196D
		// (set) Token: 0x06000010 RID: 16 RVA: 0x00003775 File Offset: 0x00001975
		public string IP { get; set; }

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000011 RID: 17 RVA: 0x0000377E File Offset: 0x0000197E
		// (set) Token: 0x06000012 RID: 18 RVA: 0x00003786 File Offset: 0x00001986
		public string Public_IP { get; set; }

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000013 RID: 19 RVA: 0x0000378F File Offset: 0x0000198F
		// (set) Token: 0x06000014 RID: 20 RVA: 0x00003797 File Offset: 0x00001997
		public int Port { get; set; }
	}
}
