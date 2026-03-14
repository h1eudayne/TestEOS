using System;
using System.Collections;
using NHibernate;
using QuestionLib.Business;

namespace QuestionLib.Entity
{
	// Token: 0x02000015 RID: 21
	[Serializable]
	public class Passage
	{
		// Token: 0x17000069 RID: 105
		// (get) Token: 0x0600010A RID: 266 RVA: 0x00004074 File Offset: 0x00003074
		// (set) Token: 0x0600010B RID: 267 RVA: 0x0000408C File Offset: 0x0000308C
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

		// Token: 0x0600010C RID: 268 RVA: 0x00004096 File Offset: 0x00003096
		public Passage()
		{
			this._passageQuestions = new ArrayList();
		}

		// Token: 0x1700006A RID: 106
		// (get) Token: 0x0600010D RID: 269 RVA: 0x000040AC File Offset: 0x000030AC
		// (set) Token: 0x0600010E RID: 270 RVA: 0x000040C4 File Offset: 0x000030C4
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

		// Token: 0x1700006B RID: 107
		// (get) Token: 0x0600010F RID: 271 RVA: 0x000040D0 File Offset: 0x000030D0
		// (set) Token: 0x06000110 RID: 272 RVA: 0x000040E8 File Offset: 0x000030E8
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

		// Token: 0x1700006C RID: 108
		// (get) Token: 0x06000111 RID: 273 RVA: 0x000040F4 File Offset: 0x000030F4
		// (set) Token: 0x06000112 RID: 274 RVA: 0x0000410C File Offset: 0x0000310C
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

		// Token: 0x1700006D RID: 109
		// (get) Token: 0x06000113 RID: 275 RVA: 0x00004118 File Offset: 0x00003118
		// (set) Token: 0x06000114 RID: 276 RVA: 0x00004130 File Offset: 0x00003130
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

		// Token: 0x1700006E RID: 110
		// (get) Token: 0x06000115 RID: 277 RVA: 0x0000413C File Offset: 0x0000313C
		// (set) Token: 0x06000116 RID: 278 RVA: 0x00004154 File Offset: 0x00003154
		public ArrayList PassageQuestions
		{
			get
			{
				return this._passageQuestions;
			}
			set
			{
				this._passageQuestions = value;
			}
		}

		// Token: 0x06000117 RID: 279 RVA: 0x00004160 File Offset: 0x00003160
		public override string ToString()
		{
			return this._pid.ToString();
		}

		// Token: 0x06000118 RID: 280 RVA: 0x00004180 File Offset: 0x00003180
		public void LoadQuestions(ISessionFactory sessionFactory)
		{
			BOQuestion boq = new BOQuestion(sessionFactory);
			this._passageQuestions = (ArrayList)boq.LoadPassageQuestion(this._pid);
		}

		// Token: 0x06000119 RID: 281 RVA: 0x000041AC File Offset: 0x000031AC
		public void Preapare2Submit()
		{
			this.Text = null;
			this.CourseId = null;
			foreach (object obj in this.PassageQuestions)
			{
				Question q = (Question)obj;
				q.Preapare2Submit();
			}
		}

		// Token: 0x04000085 RID: 133
		private int _pid;

		// Token: 0x04000086 RID: 134
		private string _courseId;

		// Token: 0x04000087 RID: 135
		private int _chapterId;

		// Token: 0x04000088 RID: 136
		private string _text;

		// Token: 0x04000089 RID: 137
		private ArrayList _passageQuestions;

		// Token: 0x0400008A RID: 138
		private int _QBID;
	}
}
