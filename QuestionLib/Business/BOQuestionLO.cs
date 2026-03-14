using System;
using System.Collections;
using NHibernate;
using QuestionLib.Entity;

namespace QuestionLib.Business
{
	// Token: 0x02000028 RID: 40
	public class BOQuestionLO : BOBase
	{
		// Token: 0x060001C7 RID: 455 RVA: 0x0000485F File Offset: 0x0000385F
		public BOQuestionLO(ISessionFactory sessionFactory)
			: base(sessionFactory)
		{
		}

		// Token: 0x060001C8 RID: 456 RVA: 0x000076DC File Offset: 0x000066DC
		public IList LoadLO(QuestionType qType, int qid)
		{
			IList result = null;
			this.session = this.sessionFactory.OpenSession();
			try
			{
				string query = "from QuestionLO qlo Where qlo.QType=:qType and qlo.QID=:qid";
				IQuery q = this.session.CreateQuery(query);
				q.SetParameter("qType", qType);
				q.SetParameter("qid", qid);
				result = q.List();
				this.session.Close();
			}
			catch (Exception ex)
			{
				this.session.Close();
				throw ex;
			}
			ArrayList listLO = new ArrayList();
			BOLO bolo = new BOLO(NHHelper.GetSessionFactory());
			foreach (object obj in result)
			{
				QuestionLO qlo = (QuestionLO)obj;
				LO lo = bolo.Load(qlo.LOID);
				listLO.Add(lo);
			}
			return listLO;
		}

		// Token: 0x060001C9 RID: 457 RVA: 0x000077EC File Offset: 0x000067EC
		public void DeleteQuestionLO(int qid, QuestionType qType)
		{
			this.session = this.sessionFactory.OpenSession();
			ITransaction tran = this.session.BeginTransaction();
			try
			{
				string qry = string.Concat(new object[]
				{
					"from QuestionLO qlo Where qlo.QType=",
					(int)qType,
					" and qlo.QID=",
					qid
				});
				this.session.Delete(qry);
				tran.Commit();
				this.session.Close();
			}
			catch (Exception ex)
			{
				tran.Rollback();
				this.session.Close();
				throw ex;
			}
		}
	}
}
