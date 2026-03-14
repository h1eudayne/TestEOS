using System;

namespace QuestionLib.Entity
{
	// Token: 0x0200001A RID: 26
	public class Test
	{
		// Token: 0x17000087 RID: 135
		// (get) Token: 0x06000152 RID: 338 RVA: 0x000046AC File Offset: 0x000036AC
		// (set) Token: 0x06000153 RID: 339 RVA: 0x000046C4 File Offset: 0x000036C4
		public string TestId
		{
			get
			{
				return this._testId;
			}
			set
			{
				this._testId = value;
			}
		}

		// Token: 0x17000088 RID: 136
		// (get) Token: 0x06000154 RID: 340 RVA: 0x000046D0 File Offset: 0x000036D0
		// (set) Token: 0x06000155 RID: 341 RVA: 0x000046E8 File Offset: 0x000036E8
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

		// Token: 0x17000089 RID: 137
		// (get) Token: 0x06000156 RID: 342 RVA: 0x000046F4 File Offset: 0x000036F4
		// (set) Token: 0x06000157 RID: 343 RVA: 0x0000470C File Offset: 0x0000370C
		public string Questions
		{
			get
			{
				return this._questions;
			}
			set
			{
				this._questions = value;
			}
		}

		// Token: 0x1700008A RID: 138
		// (get) Token: 0x06000158 RID: 344 RVA: 0x00004718 File Offset: 0x00003718
		// (set) Token: 0x06000159 RID: 345 RVA: 0x00004730 File Offset: 0x00003730
		public int NumOfQuestion
		{
			get
			{
				return this._numOfQuestion;
			}
			set
			{
				this._numOfQuestion = value;
			}
		}

		// Token: 0x1700008B RID: 139
		// (get) Token: 0x0600015A RID: 346 RVA: 0x0000473C File Offset: 0x0000373C
		// (set) Token: 0x0600015B RID: 347 RVA: 0x00004754 File Offset: 0x00003754
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

		// Token: 0x1700008C RID: 140
		// (get) Token: 0x0600015C RID: 348 RVA: 0x00004760 File Offset: 0x00003760
		// (set) Token: 0x0600015D RID: 349 RVA: 0x00004778 File Offset: 0x00003778
		public string StudentGuide
		{
			get
			{
				return this._studentGuide;
			}
			set
			{
				this._studentGuide = value;
			}
		}

		// Token: 0x040000AB RID: 171
		private string _testId;

		// Token: 0x040000AC RID: 172
		private string _courseId;

		// Token: 0x040000AD RID: 173
		private string _questions;

		// Token: 0x040000AE RID: 174
		private int _numOfQuestion;

		// Token: 0x040000AF RID: 175
		private float _mark;

		// Token: 0x040000B0 RID: 176
		private string _studentGuide;
	}
}
