using System;

namespace QuestionLib.Entity
{
	// Token: 0x02000010 RID: 16
	public class Course
	{
		// Token: 0x060000B8 RID: 184 RVA: 0x00003A8A File Offset: 0x00002A8A
		public Course()
		{
		}

		// Token: 0x060000B9 RID: 185 RVA: 0x00003B38 File Offset: 0x00002B38
		public Course(string _cid, string _name, byte[] _imagedata, string _imagename, int _imagesize, int _numberofpage)
		{
			this._cid = _cid;
			this._name = _name;
			this._imagedata = _imagedata;
			this._imagename = _imagename;
			this._imagesize = _imagesize;
			this._numberofpage = _numberofpage;
		}

		// Token: 0x17000045 RID: 69
		// (get) Token: 0x060000BA RID: 186 RVA: 0x00003B70 File Offset: 0x00002B70
		// (set) Token: 0x060000BB RID: 187 RVA: 0x00003B88 File Offset: 0x00002B88
		public string CID
		{
			get
			{
				return this._cid;
			}
			set
			{
				this._cid = value;
			}
		}

		// Token: 0x17000046 RID: 70
		// (get) Token: 0x060000BC RID: 188 RVA: 0x00003B94 File Offset: 0x00002B94
		// (set) Token: 0x060000BD RID: 189 RVA: 0x00003BAC File Offset: 0x00002BAC
		public string Name
		{
			get
			{
				return this._name;
			}
			set
			{
				this._name = value;
			}
		}

		// Token: 0x17000047 RID: 71
		// (get) Token: 0x060000BE RID: 190 RVA: 0x00003BB8 File Offset: 0x00002BB8
		// (set) Token: 0x060000BF RID: 191 RVA: 0x00003BD0 File Offset: 0x00002BD0
		public byte[] ImageData
		{
			get
			{
				return this._imagedata;
			}
			set
			{
				this._imagedata = value;
			}
		}

		// Token: 0x17000048 RID: 72
		// (get) Token: 0x060000C0 RID: 192 RVA: 0x00003BDC File Offset: 0x00002BDC
		// (set) Token: 0x060000C1 RID: 193 RVA: 0x00003BF4 File Offset: 0x00002BF4
		public string ImageName
		{
			get
			{
				return this._imagename;
			}
			set
			{
				this._imagename = value;
			}
		}

		// Token: 0x17000049 RID: 73
		// (get) Token: 0x060000C2 RID: 194 RVA: 0x00003C00 File Offset: 0x00002C00
		// (set) Token: 0x060000C3 RID: 195 RVA: 0x00003C18 File Offset: 0x00002C18
		public int ImageSize
		{
			get
			{
				return this._imagesize;
			}
			set
			{
				this._imagesize = value;
			}
		}

		// Token: 0x1700004A RID: 74
		// (get) Token: 0x060000C4 RID: 196 RVA: 0x00003C24 File Offset: 0x00002C24
		// (set) Token: 0x060000C5 RID: 197 RVA: 0x00003C3C File Offset: 0x00002C3C
		public int NumberOfPage
		{
			get
			{
				return this._numberofpage;
			}
			set
			{
				this._numberofpage = value;
			}
		}

		// Token: 0x04000061 RID: 97
		private string _cid;

		// Token: 0x04000062 RID: 98
		private string _name;

		// Token: 0x04000063 RID: 99
		private byte[] _imagedata;

		// Token: 0x04000064 RID: 100
		private string _imagename;

		// Token: 0x04000065 RID: 101
		private int _imagesize;

		// Token: 0x04000066 RID: 102
		private int _numberofpage;
	}
}
