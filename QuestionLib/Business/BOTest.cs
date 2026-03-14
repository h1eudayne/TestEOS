using System;
using System.Collections;
using NHibernate;
using QuestionLib.Entity;

namespace QuestionLib.Business
{
	// Token: 0x02000029 RID: 41
	public class BOTest : BOBase
	{
		// Token: 0x060001CA RID: 458 RVA: 0x0000485F File Offset: 0x0000385F
		public BOTest(ISessionFactory sessionFactory)
			: base(sessionFactory)
		{
		}

		// Token: 0x060001CB RID: 459 RVA: 0x00007894 File Offset: 0x00006894
		public IList LoadTest(string courseId)
		{
			this.session = this.sessionFactory.OpenSession();
			IList result;
			try
			{
				string query = "from Test t Where CourseId=:courseId";
				IQuery q = this.session.CreateQuery(query);
				q.SetParameter("courseId", courseId);
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

		// Token: 0x060001CC RID: 460 RVA: 0x00007914 File Offset: 0x00006914
		public Test LoadTestByTestId(string testId)
		{
			this.session = this.sessionFactory.OpenSession();
			IList result;
			try
			{
				string query = "from Test t Where TestId=:testId";
				IQuery q = this.session.CreateQuery(query);
				q.SetParameter("testId", testId);
				result = q.List();
				this.session.Close();
			}
			catch (Exception ex)
			{
				this.session.Close();
				throw ex;
			}
			bool flag = result.Count > 0;
			Test test;
			if (flag)
			{
				test = (Test)result[0];
			}
			else
			{
				test = null;
			}
			return test;
		}

		// Token: 0x060001CD RID: 461 RVA: 0x000079B4 File Offset: 0x000069B4
		public IList LoadTestByCourse(string courseId)
		{
			this.session = this.sessionFactory.OpenSession();
			IList result;
			try
			{
				string query = "from Test t Where CourseId=:courseId";
				IQuery q = this.session.CreateQuery(query);
				q.SetParameter("courseId", courseId);
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

		// Token: 0x060001CE RID: 462 RVA: 0x00007A34 File Offset: 0x00006A34
		public bool IsTestExists(string testId)
		{
			this.session = this.sessionFactory.OpenSession();
			IList result;
			try
			{
				string query = "from Test t Where TestId=:testId";
				IQuery q = this.session.CreateQuery(query);
				q.SetParameter("testId", testId);
				result = q.List();
				this.session.Close();
			}
			catch (Exception ex)
			{
				this.session.Close();
				throw ex;
			}
			bool flag = result.Count == 0;
			return !flag;
		}
	}
}
