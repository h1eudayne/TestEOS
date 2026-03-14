using System;
using System.Collections;
using System.Data.SqlClient;
using NHibernate;
using QuestionLib.Entity;

namespace QuestionLib.Business
{
	// Token: 0x0200001E RID: 30
	public class BOBase
	{
		// Token: 0x06000188 RID: 392 RVA: 0x00003A8A File Offset: 0x00002A8A
		public BOBase()
		{
		}

		// Token: 0x06000189 RID: 393 RVA: 0x00005242 File Offset: 0x00004242
		public BOBase(ISessionFactory sessionFactory)
		{
			this.sessionFactory = sessionFactory;
		}

		// Token: 0x0600018A RID: 394 RVA: 0x00005254 File Offset: 0x00004254
		public object SaveOrUpdate(object obj)
		{
			this.session = this.sessionFactory.OpenSession();
			using (ITransaction tx = this.session.BeginTransaction())
			{
				try
				{
					this.session.SaveOrUpdate(obj);
					this.session.Flush();
					tx.Commit();
					this.session.Close();
				}
				catch (Exception ex)
				{
					tx.Rollback();
					this.session.Close();
					throw ex;
				}
			}
			return obj;
		}

		// Token: 0x0600018B RID: 395 RVA: 0x000052F0 File Offset: 0x000042F0
		public object Save(object obj)
		{
			this.session = this.sessionFactory.OpenSession();
			using (ITransaction tx = this.session.BeginTransaction())
			{
				try
				{
					this.session.Save(obj);
					this.session.Flush();
					tx.Commit();
					this.session.Close();
				}
				catch (Exception ex)
				{
					tx.Rollback();
					this.session.Close();
					throw ex;
				}
			}
			return obj;
		}

		// Token: 0x0600018C RID: 396 RVA: 0x0000538C File Offset: 0x0000438C
		public object Save(object obj, ISession mySession)
		{
			try
			{
				mySession.Save(obj);
				mySession.Flush();
			}
			catch (Exception ex)
			{
				throw ex;
			}
			return obj;
		}

		// Token: 0x0600018D RID: 397 RVA: 0x000053C4 File Offset: 0x000043C4
		public object Update(object obj)
		{
			this.session = this.sessionFactory.OpenSession();
			using (ITransaction tx = this.session.BeginTransaction())
			{
				try
				{
					this.session.Update(obj);
					this.session.Flush();
					tx.Commit();
					this.session.Close();
				}
				catch (Exception ex)
				{
					tx.Rollback();
					this.session.Close();
					throw ex;
				}
			}
			return obj;
		}

		// Token: 0x0600018E RID: 398 RVA: 0x00005460 File Offset: 0x00004460
		public object Update(object obj, ISession mySession)
		{
			try
			{
				mySession.Update(obj);
				mySession.Flush();
			}
			catch (Exception ex)
			{
				throw ex;
			}
			return obj;
		}

		// Token: 0x0600018F RID: 399 RVA: 0x00005498 File Offset: 0x00004498
		public void Load(object obj, object id)
		{
			this.session = this.sessionFactory.OpenSession();
			try
			{
				this.session.Load(obj, id);
				this.session.Close();
			}
			catch (Exception ex)
			{
				this.session.Close();
				throw ex;
			}
		}

		// Token: 0x06000190 RID: 400 RVA: 0x000054F8 File Offset: 0x000044F8
		public void Delete(object obj)
		{
			this.session = this.sessionFactory.OpenSession();
			using (ITransaction tx = this.session.BeginTransaction())
			{
				try
				{
					this.session.Delete(obj);
					this.session.Flush();
					tx.Commit();
					this.session.Close();
				}
				catch (Exception ex)
				{
					tx.Rollback();
					this.session.Close();
					throw ex;
				}
			}
		}

		// Token: 0x06000191 RID: 401 RVA: 0x00005598 File Offset: 0x00004598
		public IList List(string typeName)
		{
			IList result = null;
			this.session = this.sessionFactory.OpenSession();
			using (ITransaction tx = this.session.BeginTransaction())
			{
				IQuery q = this.session.CreateQuery("from " + typeName);
				result = q.List();
				tx.Commit();
				this.session.Close();
			}
			return result;
		}

		// Token: 0x06000192 RID: 402 RVA: 0x0000561C File Offset: 0x0000461C
		public IList ListID(string typeName, QuestionType qt, int chapterID)
		{
			IList result = null;
			bool flag = qt == QuestionType.READING;
			string id;
			string qType;
			if (flag)
			{
				id = "pid";
				qType = "=0";
			}
			else
			{
				bool flag2 = qt == QuestionType.MULTIPLE_CHOICE;
				if (flag2)
				{
					id = "qid";
					qType = "=1";
				}
				else
				{
					bool flag3 = qt == QuestionType.INDICATE_MISTAKE;
					if (flag3)
					{
						id = "qid";
						qType = "=2";
					}
					else
					{
						bool flag4 = qt == QuestionType.MATCH;
						if (flag4)
						{
							id = "mid";
							qType = "=3";
						}
						else
						{
							id = "qid";
							qType = ">3";
						}
					}
				}
			}
			string sql = string.Concat(new object[] { "SELECT ", id, " FROM ", typeName, " WHERE chapterId=", chapterID, " AND qType=", qType });
			SqlConnection con = (SqlConnection)this.sessionFactory.ConnectionProvider.GetConnection();
			return result;
		}

		// Token: 0x06000193 RID: 403 RVA: 0x00005710 File Offset: 0x00004710
		public IList ListID(string typeName, QuestionType qt, string courseID)
		{
			return null;
		}

		// Token: 0x040000BE RID: 190
		protected ISessionFactory sessionFactory;

		// Token: 0x040000BF RID: 191
		protected ISession session;
	}
}
