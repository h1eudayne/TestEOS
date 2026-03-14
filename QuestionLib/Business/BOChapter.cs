using System;
using System.Collections;
using NHibernate;
using QuestionLib.Entity;

namespace QuestionLib.Business
{
	// Token: 0x0200001F RID: 31
	public class BOChapter : BOBase
	{
		// Token: 0x06000194 RID: 404 RVA: 0x0000485F File Offset: 0x0000385F
		public BOChapter(ISessionFactory sessionFactory)
			: base(sessionFactory)
		{
		}

		// Token: 0x06000195 RID: 405 RVA: 0x00005728 File Offset: 0x00004728
		public IList LoadFillBlankQuestionByChapter(int chapterId)
		{
			QuestionType qt = QuestionType.FILL_BLANK_ALL;
			QuestionType qt2 = QuestionType.FILL_BLANK_GROUP;
			QuestionType qt3 = QuestionType.FILL_BLANK_EMPTY;
			this.session = this.sessionFactory.OpenSession();
			IList result;
			try
			{
				string query = "from Question q Where (q.QType=:type1 OR q.QType=:type2 OR q.QType=:type3)  AND ChapterId=:chapterId";
				IQuery q = this.session.CreateQuery(query);
				q.SetParameter("type1", qt);
				q.SetParameter("type2", qt2);
				q.SetParameter("type3", qt3);
				q.SetParameter("chapterId", chapterId.ToString());
				result = q.List();
				this.session.Close();
			}
			catch (Exception ex)
			{
				this.session.Close();
				throw ex;
			}
			return result;
		}

		// Token: 0x06000196 RID: 406 RVA: 0x000057F4 File Offset: 0x000047F4
		public IList LoadQuestionByChapter(QuestionType qt, int chapterId)
		{
			this.session = this.sessionFactory.OpenSession();
			IList result;
			try
			{
				string query = "from Question q Where q.QType=:type and ChapterId=:chapterId";
				IQuery q = this.session.CreateQuery(query);
				q.SetParameter("type", qt);
				q.SetParameter("chapterId", chapterId.ToString());
				result = q.List();
				this.session.Close();
			}
			catch (Exception ex)
			{
				this.session.Close();
				throw ex;
			}
			return result;
		}

		// Token: 0x06000197 RID: 407 RVA: 0x0000588C File Offset: 0x0000488C
		public IList LoadPassageByChapter(int chapterId)
		{
			this.session = this.sessionFactory.OpenSession();
			IList result;
			try
			{
				string query = "from Passage p Where ChapterId=:chapterId";
				IQuery q = this.session.CreateQuery(query);
				q.SetParameter("chapterId", chapterId);
				result = q.List();
				this.session.Close();
			}
			catch (Exception ex)
			{
				this.session.Close();
				throw ex;
			}
			return result;
		}

		// Token: 0x06000198 RID: 408 RVA: 0x00005910 File Offset: 0x00004910
		public IList LoadMatchQuestionByChapter(int chapterId)
		{
			this.session = this.sessionFactory.OpenSession();
			IList result;
			try
			{
				string query = "from MatchQuestion m Where ChapterId=:chapterId";
				IQuery q = this.session.CreateQuery(query);
				q.SetParameter("chapterId", chapterId);
				result = q.List();
				this.session.Close();
			}
			catch (Exception ex)
			{
				this.session.Close();
				throw ex;
			}
			return result;
		}
	}
}
