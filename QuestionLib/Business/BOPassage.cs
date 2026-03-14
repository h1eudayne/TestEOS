using System;
using System.Collections;
using System.Data.SqlClient;
using NHibernate;
using QuestionLib.Entity;

namespace QuestionLib.Business
{
	// Token: 0x02000025 RID: 37
	public class BOPassage : BOBase
	{
		// Token: 0x060001B6 RID: 438 RVA: 0x0000485F File Offset: 0x0000385F
		public BOPassage(ISessionFactory sessionFactory)
			: base(sessionFactory)
		{
		}

		// Token: 0x060001B7 RID: 439 RVA: 0x000069B8 File Offset: 0x000059B8
		public Passage LoadPassage(int pid)
		{
			this.session = this.sessionFactory.OpenSession();
			IList result;
			try
			{
				string query = "from Passage p Where p.PID=:pid";
				IQuery q = this.session.CreateQuery(query);
				q.SetParameter("pid", pid);
				result = q.List();
				this.session.Close();
			}
			catch (Exception ex)
			{
				this.session.Close();
				throw ex;
			}
			return (result.Count > 0) ? ((Passage)result[0]) : null;
		}

		// Token: 0x060001B8 RID: 440 RVA: 0x00006A54 File Offset: 0x00005A54
		public IList LoadPassageByCourse(string courseId)
		{
			this.session = this.sessionFactory.OpenSession();
			IList result;
			try
			{
				string query = "from Passage p Where CourseId=:courseId";
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

		// Token: 0x060001B9 RID: 441 RVA: 0x00006AD4 File Offset: 0x00005AD4
		public void Delete(int pid, int[] qid_list)
		{
			this.session = this.sessionFactory.OpenSession();
			ITransaction tran = this.session.BeginTransaction();
			try
			{
				string qry = "from Passage p Where p.PID=" + pid.ToString();
				this.session.Delete(qry);
				qry = "from Question q Where q.PID=" + pid.ToString();
				this.session.Delete(qry);
				foreach (int qid in qid_list)
				{
					qry = "from QuestionAnswer qa Where qa.QID=" + qid.ToString();
					this.session.Delete(qry);
					int qt = 0;
					qry = string.Concat(new object[]
					{
						"from QuestionLO qLO Where Qtype=",
						qt,
						" and qLO.QID=",
						qid.ToString()
					});
					this.session.Delete(qry);
				}
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

		// Token: 0x060001BA RID: 442 RVA: 0x00006BF4 File Offset: 0x00005BF4
		public bool Delete(int chapterID, string conStr)
		{
			int qt = 0;
			SqlConnection con = new SqlConnection(conStr);
			con.Open();
			SqlTransaction tran = con.BeginTransaction();
			string sqlLO = string.Concat(new object[] { "DELETE FROM QuestionLO WHERE QType = ", qt, " AND qid IN (SELECT qid FROM Question WHERE pid IN (SELECT pid FROM Passage WHERE chapterID=", chapterID, "))" });
			string sqlA = "DELETE FROM QuestionAnswer WHERE qid IN (SELECT qid FROM Question WHERE pid IN (SELECT pid FROM Passage WHERE chapterID=" + chapterID + "))";
			string sqlQ = "DELETE FROM Question WHERE pid IN (SELECT pid FROM Passage WHERE chapterID=" + chapterID + ")";
			string sqlR = "DELETE FROM Passage WHERE chapterID=" + chapterID;
			SqlCommand cmdLO = new SqlCommand(sqlLO, con);
			cmdLO.Transaction = tran;
			SqlCommand cmdA = new SqlCommand(sqlA, con);
			cmdA.Transaction = tran;
			SqlCommand cmdQ = new SqlCommand(sqlQ, con);
			cmdQ.Transaction = tran;
			SqlCommand cmdR = new SqlCommand(sqlR, con);
			cmdR.Transaction = tran;
			bool flag;
			try
			{
				cmdLO.ExecuteNonQuery();
				cmdA.ExecuteNonQuery();
				cmdQ.ExecuteNonQuery();
				cmdR.ExecuteNonQuery();
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

		// Token: 0x060001BB RID: 443 RVA: 0x00006D34 File Offset: 0x00005D34
		public bool SaveList(IList list)
		{
			ISession session = this.sessionFactory.OpenSession();
			ITransaction tran = session.BeginTransaction();
			bool flag;
			try
			{
				foreach (object obj in list)
				{
					Passage p = (Passage)obj;
					session.Save(p);
					foreach (object obj2 in p.PassageQuestions)
					{
						Question q = (Question)obj2;
						q.PID = p.PID;
						session.Save(q);
						foreach (object obj3 in q.QuestionAnswers)
						{
							QuestionAnswer qa = (QuestionAnswer)obj3;
							qa.QID = q.QID;
							session.Save(qa);
						}
						q.QuestionLOs = BOLO.RemoveDupLO(q.QuestionLOs);
						foreach (object obj4 in q.QuestionLOs)
						{
							QuestionLO qlo = (QuestionLO)obj4;
							qlo.QID = q.QID;
							qlo.QType = q.QType;
							session.Save(qlo);
						}
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
	}
}
