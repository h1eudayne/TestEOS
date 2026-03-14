using System;
using System.Collections;
using NHibernate;
using QuestionLib.Entity;

namespace QuestionLib.Business
{
	// Token: 0x02000021 RID: 33
	public class BOEssayQuestion : BOBase
	{
		// Token: 0x060001A0 RID: 416 RVA: 0x0000485F File Offset: 0x0000385F
		public BOEssayQuestion(ISessionFactory sessionFactory)
			: base(sessionFactory)
		{
		}

		// Token: 0x060001A1 RID: 417 RVA: 0x00005CF8 File Offset: 0x00004CF8
		public EssayQuestion Load(int eqid)
		{
			IList result = null;
			this.session = this.sessionFactory.OpenSession();
			try
			{
				string query = "from EssayQuestion q Where q.EQID=:eqid";
				IQuery q = this.session.CreateQuery(query);
				q.SetParameter("eqid", eqid);
				result = q.List();
				this.session.Close();
			}
			catch (Exception ex)
			{
				this.session.Close();
				throw ex;
			}
			return (result.Count > 0) ? ((EssayQuestion)result[0]) : null;
		}

		// Token: 0x060001A2 RID: 418 RVA: 0x00005D98 File Offset: 0x00004D98
		public IList LoadByCourse(string courseId)
		{
			IList result = null;
			this.session = this.sessionFactory.OpenSession();
			try
			{
				string query = "from EssayQuestion q Where CourseId=:courseId";
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

		// Token: 0x060001A3 RID: 419 RVA: 0x00005E1C File Offset: 0x00004E1C
		public IList LoadByChapter(int chapterId)
		{
			IList result = null;
			this.session = this.sessionFactory.OpenSession();
			try
			{
				string query = "from EssayQuestion q Where ChapterId=:chapterId";
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

		// Token: 0x060001A4 RID: 420 RVA: 0x00005EA4 File Offset: 0x00004EA4
		public void Delete(int eqid)
		{
			this.session = this.sessionFactory.OpenSession();
			ITransaction tran = this.session.BeginTransaction();
			try
			{
				string qry = "from EssayQuestion q Where q.EQID=" + eqid.ToString();
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

		// Token: 0x060001A5 RID: 421 RVA: 0x00005F2C File Offset: 0x00004F2C
		public bool SaveList(IList list)
		{
			ISession session = this.sessionFactory.OpenSession();
			ITransaction tran = session.BeginTransaction();
			bool flag;
			try
			{
				foreach (object obj in list)
				{
					EssayQuestion q = (EssayQuestion)obj;
					session.Save(q);
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

		// Token: 0x060001A6 RID: 422 RVA: 0x00005FC8 File Offset: 0x00004FC8
		public void DeleteQuestionInChapter(int chapterId)
		{
			this.session = this.sessionFactory.OpenSession();
			ITransaction tran = this.session.BeginTransaction();
			try
			{
				string qry = "from EssayQuestion q Where q.ChapterId=" + chapterId.ToString();
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
