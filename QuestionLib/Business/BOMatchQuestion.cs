using System;
using System.Collections;
using System.Data.SqlClient;
using NHibernate;
using QuestionLib.Entity;

namespace QuestionLib.Business
{
	// Token: 0x02000024 RID: 36
	public class BOMatchQuestion : BOBase
	{
		// Token: 0x060001B0 RID: 432 RVA: 0x0000485F File Offset: 0x0000385F
		public BOMatchQuestion(ISessionFactory sessionFactory)
			: base(sessionFactory)
		{
		}

		// Token: 0x060001B1 RID: 433 RVA: 0x000065D0 File Offset: 0x000055D0
		public MatchQuestion LoadMatch(int mid)
		{
			this.session = this.sessionFactory.OpenSession();
			IList result;
			try
			{
				string query = "from MatchQuestion mq Where  mq.MID=:mid";
				IQuery q = this.session.CreateQuery(query);
				q.SetParameter("mid", mid);
				result = q.List();
				this.session.Close();
			}
			catch (Exception ex)
			{
				this.session.Close();
				throw ex;
			}
			return (result.Count > 0) ? ((MatchQuestion)result[0]) : null;
		}

		// Token: 0x060001B2 RID: 434 RVA: 0x0000666C File Offset: 0x0000566C
		public IList LoadMatchOfCourse(string courseId)
		{
			this.session = this.sessionFactory.OpenSession();
			IList result;
			try
			{
				string query = "from MatchQuestion mq Where  mq.CourseId=:courseId";
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

		// Token: 0x060001B3 RID: 435 RVA: 0x000066EC File Offset: 0x000056EC
		public bool SaveList(IList list)
		{
			ISession session = this.sessionFactory.OpenSession();
			ITransaction tran = session.BeginTransaction();
			bool flag;
			try
			{
				foreach (object obj in list)
				{
					MatchQuestion q = (MatchQuestion)obj;
					session.Save(q);
					q.QuestionLOs = BOLO.RemoveDupLO(q.QuestionLOs);
					foreach (object obj2 in q.QuestionLOs)
					{
						QuestionLO qlo = (QuestionLO)obj2;
						qlo.QID = q.MID;
						qlo.QType = QuestionType.MATCH;
						session.Save(qlo);
					}
				}
				tran.Commit();
				flag = true;
			}
			catch
			{
				tran.Rollback();
				flag = false;
			}
			return flag;
		}

		// Token: 0x060001B4 RID: 436 RVA: 0x0000680C File Offset: 0x0000580C
		public bool Delete(int chapterID, string conStr)
		{
			int qt = 3;
			SqlConnection con = new SqlConnection(conStr);
			con.Open();
			SqlTransaction tran = con.BeginTransaction();
			string sqlLO = string.Concat(new object[] { "DELETE FROM QuestionLO WHERE QType = ", qt, " AND qid IN (SELECT mid FROM MatchQuestion WHERE chapterID=", chapterID, ")" });
			string sqlMQ = "DELETE FROM MatchQuestion WHERE chapterID=" + chapterID;
			SqlCommand cmdLO = new SqlCommand(sqlLO, con);
			cmdLO.Transaction = tran;
			SqlCommand cmdMQ = new SqlCommand(sqlMQ, con);
			cmdMQ.Transaction = tran;
			bool flag;
			try
			{
				cmdLO.ExecuteNonQuery();
				cmdMQ.ExecuteNonQuery();
				tran.Commit();
				con.Close();
				flag = true;
			}
			catch (Exception ex)
			{
				tran.Rollback();
				con.Close();
				throw ex;
			}
			return flag;
		}

		// Token: 0x060001B5 RID: 437 RVA: 0x000068E8 File Offset: 0x000058E8
		public void Delete(MatchQuestion m)
		{
			int mid = m.MID;
			this.session = this.sessionFactory.OpenSession();
			ITransaction tran = this.session.BeginTransaction();
			try
			{
				string qry = "from MatchQuestion mq Where mq.MID=" + mid.ToString();
				this.session.Delete(qry);
				int qT = 3;
				qry = string.Concat(new object[] { "from QuestionLO qlo Where qlo.QType=", qT, " and qlo.QID=", mid });
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
