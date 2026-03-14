using System;
using System.Collections;
using NHibernate;

namespace QuestionLib.Business
{
	// Token: 0x02000027 RID: 39
	public class BOQuestionAnswer : BOBase
	{
		// Token: 0x060001C5 RID: 453 RVA: 0x0000485F File Offset: 0x0000385F
		public BOQuestionAnswer(ISessionFactory sessionFactory)
			: base(sessionFactory)
		{
		}

		// Token: 0x060001C6 RID: 454 RVA: 0x00007658 File Offset: 0x00006658
		public IList LoadAnswer(int qid)
		{
			this.session = this.sessionFactory.OpenSession();
			IList result;
			try
			{
				string query = "from QuestionAnswer qa Where qa.QID=:qid";
				IQuery q = this.session.CreateQuery(query);
				q.SetParameter("qid", qid);
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
