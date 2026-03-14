using System;
using System.Collections;
using NHibernate;
using QuestionLib.Business;

namespace QuestionLib.Entity
{
	// Token: 0x02000017 RID: 23
	[Serializable]
	public class Question
	{
		// Token: 0x1700006F RID: 111
		// (get) Token: 0x0600011A RID: 282 RVA: 0x0000421C File Offset: 0x0000321C
		// (set) Token: 0x0600011B RID: 283 RVA: 0x00004234 File Offset: 0x00003234
		public int QBID
		{
			get
			{
				return this._QBID;
			}
			set
			{
				this._QBID = value;
			}
		}

		// Token: 0x0600011C RID: 284 RVA: 0x0000423E File Offset: 0x0000323E
		public Question()
		{
			this._questionAnswers = new ArrayList();
			this._questionLOs = new ArrayList();
		}

		// Token: 0x17000070 RID: 112
		// (get) Token: 0x0600011D RID: 285 RVA: 0x00004260 File Offset: 0x00003260
		// (set) Token: 0x0600011E RID: 286 RVA: 0x00004278 File Offset: 0x00003278
		public int QID
		{
			get
			{
				return this._qid;
			}
			set
			{
				this._qid = value;
			}
		}

		// Token: 0x17000071 RID: 113
		// (get) Token: 0x0600011F RID: 287 RVA: 0x00004284 File Offset: 0x00003284
		// (set) Token: 0x06000120 RID: 288 RVA: 0x0000429C File Offset: 0x0000329C
		public string CourseId
		{
			get
			{
				return this._courseId;
			}
			set
			{
				this._courseId = value;
			}
		}

		// Token: 0x17000072 RID: 114
		// (get) Token: 0x06000121 RID: 289 RVA: 0x000042A8 File Offset: 0x000032A8
		// (set) Token: 0x06000122 RID: 290 RVA: 0x000042C0 File Offset: 0x000032C0
		public int ChapterId
		{
			get
			{
				return this._chapterId;
			}
			set
			{
				this._chapterId = value;
			}
		}

		// Token: 0x17000073 RID: 115
		// (get) Token: 0x06000123 RID: 291 RVA: 0x000042CC File Offset: 0x000032CC
		// (set) Token: 0x06000124 RID: 292 RVA: 0x000042E4 File Offset: 0x000032E4
		public int PID
		{
			get
			{
				return this._pid;
			}
			set
			{
				this._pid = value;
			}
		}

		// Token: 0x17000074 RID: 116
		// (get) Token: 0x06000125 RID: 293 RVA: 0x000042F0 File Offset: 0x000032F0
		// (set) Token: 0x06000126 RID: 294 RVA: 0x00004308 File Offset: 0x00003308
		public string Text
		{
			get
			{
				return this._text;
			}
			set
			{
				this._text = value;
			}
		}

		// Token: 0x17000075 RID: 117
		// (get) Token: 0x06000127 RID: 295 RVA: 0x00004314 File Offset: 0x00003314
		// (set) Token: 0x06000128 RID: 296 RVA: 0x0000432C File Offset: 0x0000332C
		public float Mark
		{
			get
			{
				return this._mark;
			}
			set
			{
				this._mark = value;
			}
		}

		// Token: 0x17000076 RID: 118
		// (get) Token: 0x06000129 RID: 297 RVA: 0x00004338 File Offset: 0x00003338
		// (set) Token: 0x0600012A RID: 298 RVA: 0x00004350 File Offset: 0x00003350
		public ArrayList QuestionAnswers
		{
			get
			{
				return this._questionAnswers;
			}
			set
			{
				this._questionAnswers = value;
			}
		}

		// Token: 0x17000077 RID: 119
		// (get) Token: 0x0600012B RID: 299 RVA: 0x0000435C File Offset: 0x0000335C
		// (set) Token: 0x0600012C RID: 300 RVA: 0x00004374 File Offset: 0x00003374
		public QuestionType QType
		{
			get
			{
				return this._qType;
			}
			set
			{
				this._qType = value;
			}
		}

		// Token: 0x17000078 RID: 120
		// (get) Token: 0x0600012D RID: 301 RVA: 0x00004380 File Offset: 0x00003380
		// (set) Token: 0x0600012E RID: 302 RVA: 0x00004398 File Offset: 0x00003398
		public bool Lock
		{
			get
			{
				return this._lock;
			}
			set
			{
				this._lock = value;
			}
		}

		// Token: 0x17000079 RID: 121
		// (get) Token: 0x0600012F RID: 303 RVA: 0x000043A4 File Offset: 0x000033A4
		// (set) Token: 0x06000130 RID: 304 RVA: 0x000043BC File Offset: 0x000033BC
		public byte[] ImageData
		{
			get
			{
				return this._imageData;
			}
			set
			{
				this._imageData = value;
			}
		}

		// Token: 0x1700007A RID: 122
		// (get) Token: 0x06000131 RID: 305 RVA: 0x000043C8 File Offset: 0x000033C8
		// (set) Token: 0x06000132 RID: 306 RVA: 0x000043E0 File Offset: 0x000033E0
		public int ImageSize
		{
			get
			{
				return this._imageSize;
			}
			set
			{
				this._imageSize = value;
			}
		}

		// Token: 0x1700007B RID: 123
		// (get) Token: 0x06000133 RID: 307 RVA: 0x000043EC File Offset: 0x000033EC
		// (set) Token: 0x06000134 RID: 308 RVA: 0x00004404 File Offset: 0x00003404
		public ArrayList QuestionLOs
		{
			get
			{
				return this._questionLOs;
			}
			set
			{
				this._questionLOs = value;
			}
		}

		// Token: 0x06000135 RID: 309 RVA: 0x00004410 File Offset: 0x00003410
		public override string ToString()
		{
			return this._text;
		}

		// Token: 0x06000136 RID: 310 RVA: 0x00004428 File Offset: 0x00003428
		public void LoadAnswers(ISessionFactory sessionFactory)
		{
			BOQuestionAnswer boqa = new BOQuestionAnswer(sessionFactory);
			this._questionAnswers = (ArrayList)boqa.LoadAnswer(this._qid);
		}

		// Token: 0x06000137 RID: 311 RVA: 0x00004454 File Offset: 0x00003454
		public void Preapare2Submit()
		{
			this.Text = null;
			this.CourseId = null;
			this.ImageData = null;
			this.ImageSize = 0;
			bool flag = this.QType == QuestionType.FILL_BLANK_ALL;
			if (!flag)
			{
				bool flag2 = this.QType == QuestionType.FILL_BLANK_GROUP;
				if (!flag2)
				{
					bool flag3 = this.QType == QuestionType.FILL_BLANK_EMPTY;
					if (!flag3)
					{
						foreach (object obj in this.QuestionAnswers)
						{
							QuestionAnswer qa = (QuestionAnswer)obj;
							qa.Text = null;
						}
					}
				}
			}
		}

		// Token: 0x04000093 RID: 147
		private int _qid;

		// Token: 0x04000094 RID: 148
		private string _courseId;

		// Token: 0x04000095 RID: 149
		private int _chapterId;

		// Token: 0x04000096 RID: 150
		private int _pid;

		// Token: 0x04000097 RID: 151
		private string _text;

		// Token: 0x04000098 RID: 152
		private float _mark;

		// Token: 0x04000099 RID: 153
		private ArrayList _questionAnswers;

		// Token: 0x0400009A RID: 154
		private QuestionType _qType;

		// Token: 0x0400009B RID: 155
		private bool _lock;

		// Token: 0x0400009C RID: 156
		private byte[] _imageData;

		// Token: 0x0400009D RID: 157
		private int _imageSize;

		// Token: 0x0400009E RID: 158
		private ArrayList _questionLOs;

		// Token: 0x0400009F RID: 159
		private int _QBID;
	}
}
