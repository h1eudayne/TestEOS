using System;

namespace QuestionLib.Entity
{
	// Token: 0x02000012 RID: 18
	public class EOSLog
	{
		// Token: 0x17000055 RID: 85
		// (get) Token: 0x060000DC RID: 220 RVA: 0x00003D28 File Offset: 0x00002D28
		// (set) Token: 0x060000DD RID: 221 RVA: 0x00003D40 File Offset: 0x00002D40
		public int LogID
		{
			get
			{
				return this._LogID;
			}
			set
			{
				this._LogID = value;
			}
		}

		// Token: 0x17000056 RID: 86
		// (get) Token: 0x060000DE RID: 222 RVA: 0x00003D4C File Offset: 0x00002D4C
		// (set) Token: 0x060000DF RID: 223 RVA: 0x00003D64 File Offset: 0x00002D64
		public string Log_Name
		{
			get
			{
				return this._Log_Name;
			}
			set
			{
				this._Log_Name = value;
			}
		}

		// Token: 0x17000057 RID: 87
		// (get) Token: 0x060000E0 RID: 224 RVA: 0x00003D70 File Offset: 0x00002D70
		// (set) Token: 0x060000E1 RID: 225 RVA: 0x00003D88 File Offset: 0x00002D88
		public string Log_Account
		{
			get
			{
				return this._Log_Account;
			}
			set
			{
				this._Log_Account = value;
			}
		}

		// Token: 0x17000058 RID: 88
		// (get) Token: 0x060000E2 RID: 226 RVA: 0x00003D94 File Offset: 0x00002D94
		// (set) Token: 0x060000E3 RID: 227 RVA: 0x00003DAC File Offset: 0x00002DAC
		public string Log_MACAddress
		{
			get
			{
				return this._Log_MACAddress;
			}
			set
			{
				this._Log_MACAddress = value;
			}
		}

		// Token: 0x17000059 RID: 89
		// (get) Token: 0x060000E4 RID: 228 RVA: 0x00003DB8 File Offset: 0x00002DB8
		// (set) Token: 0x060000E5 RID: 229 RVA: 0x00003DD0 File Offset: 0x00002DD0
		public DateTime CreatedDate
		{
			get
			{
				return this._CreatedDate;
			}
			set
			{
				this._CreatedDate = value;
			}
		}

		// Token: 0x04000071 RID: 113
		private int _LogID;

		// Token: 0x04000072 RID: 114
		private string _Log_Name;

		// Token: 0x04000073 RID: 115
		private string _Log_Account;

		// Token: 0x04000074 RID: 116
		private string _Log_MACAddress;

		// Token: 0x04000075 RID: 117
		private DateTime _CreatedDate;
	}
}
