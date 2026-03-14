using System;

namespace QuestionLib.Entity
{
	// Token: 0x02000013 RID: 19
	public class LO
	{
		// Token: 0x1700005A RID: 90
		// (get) Token: 0x060000E7 RID: 231 RVA: 0x00003DDC File Offset: 0x00002DDC
		// (set) Token: 0x060000E8 RID: 232 RVA: 0x00003DF4 File Offset: 0x00002DF4
		public int LOID
		{
			get
			{
				return this._LOID;
			}
			set
			{
				this._LOID = value;
			}
		}

		// Token: 0x1700005B RID: 91
		// (get) Token: 0x060000E9 RID: 233 RVA: 0x00003E00 File Offset: 0x00002E00
		// (set) Token: 0x060000EA RID: 234 RVA: 0x00003E18 File Offset: 0x00002E18
		public string CID
		{
			get
			{
				return this._CID;
			}
			set
			{
				this._CID = value;
			}
		}

		// Token: 0x1700005C RID: 92
		// (get) Token: 0x060000EB RID: 235 RVA: 0x00003E24 File Offset: 0x00002E24
		// (set) Token: 0x060000EC RID: 236 RVA: 0x00003E3C File Offset: 0x00002E3C
		public string LO_Name
		{
			get
			{
				return this._LO_Name;
			}
			set
			{
				this._LO_Name = value;
			}
		}

		// Token: 0x1700005D RID: 93
		// (get) Token: 0x060000ED RID: 237 RVA: 0x00003E48 File Offset: 0x00002E48
		// (set) Token: 0x060000EE RID: 238 RVA: 0x00003E60 File Offset: 0x00002E60
		public string LO_Desc
		{
			get
			{
				return this._LO_Desc;
			}
			set
			{
				this._LO_Desc = value;
			}
		}

		// Token: 0x1700005E RID: 94
		// (get) Token: 0x060000EF RID: 239 RVA: 0x00003E6C File Offset: 0x00002E6C
		// (set) Token: 0x060000F0 RID: 240 RVA: 0x00003E84 File Offset: 0x00002E84
		public string Dec_No
		{
			get
			{
				return this._Dec_No;
			}
			set
			{
				this._Dec_No = value;
			}
		}

		// Token: 0x060000F1 RID: 241 RVA: 0x00003E90 File Offset: 0x00002E90
		public override string ToString()
		{
			return this.LO_Name + " - " + this.LO_Desc;
		}

		// Token: 0x04000076 RID: 118
		private int _LOID;

		// Token: 0x04000077 RID: 119
		private string _CID;

		// Token: 0x04000078 RID: 120
		private string _LO_Name;

		// Token: 0x04000079 RID: 121
		private string _LO_Desc;

		// Token: 0x0400007A RID: 122
		private string _Dec_No;
	}
}
