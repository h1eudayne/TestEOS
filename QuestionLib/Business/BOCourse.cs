using System;
using System.Collections;
using NHibernate;

namespace QuestionLib.Business
{
	// Token: 0x02000020 RID: 32
	public class BOCourse : BOBase
	{
		// Token: 0x06000199 RID: 409 RVA: 0x0000485F File Offset: 0x0000385F
		public BOCourse(ISessionFactory sessionFactory)
			: base(sessionFactory)
		{
		}

		// Token: 0x0600019A RID: 410 RVA: 0x00005994 File Offset: 0x00004994
		public IList LoadChapterByCourse(string courseId)
		{
			this.session = this.sessionFactory.OpenSession();
			IList result;
			try
			{
				string query = "from Chapter ch Where CID=:courseId";
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

		// Token: 0x0600019B RID: 411 RVA: 0x00005A14 File Offset: 0x00004A14
		public bool IsCourseExists(string courseId)
		{
			bool exists = false;
			this.session = this.sessionFactory.OpenSession();
			try
			{
				string query = "from Course c Where CID=:courseId";
				IQuery q = this.session.CreateQuery(query);
				q.SetParameter("courseId", courseId);
				IList result = q.List();
				this.session.Close();
				bool flag = result.Count > 0;
				if (flag)
				{
					exists = true;
				}
			}
			catch (Exception ex)
			{
				this.session.Close();
				throw ex;
			}
			return exists;
		}

		// Token: 0x0600019C RID: 412 RVA: 0x00005AA8 File Offset: 0x00004AA8
		public bool IsCourseImage(string courseId)
		{
			bool exists = false;
			this.session = this.sessionFactory.OpenSession();
			try
			{
				string query = "from Course c Where CID=:courseId and ImageSize != 0";
				IQuery q = this.session.CreateQuery(query);
				q.SetParameter("courseId", courseId);
				IList result = q.List();
				this.session.Close();
				bool flag = result.Count > 0;
				if (flag)
				{
					exists = true;
				}
			}
			catch (Exception ex)
			{
				this.session.Close();
				throw ex;
			}
			return exists;
		}

		// Token: 0x0600019D RID: 413 RVA: 0x00005B3C File Offset: 0x00004B3C
		public byte[] GetImageDataByCourse(string courseId)
		{
			byte[] imageData = null;
			this.session = this.sessionFactory.OpenSession();
			try
			{
				string query = "select c.ImageData from Course c Where c.CID = :courseId";
				IQuery q = this.session.CreateQuery(query);
				q.SetParameter("courseId", courseId);
				object result = q.UniqueResult();
				bool flag = result != null;
				if (flag)
				{
					imageData = result as byte[];
				}
				this.session.Close();
			}
			catch (Exception ex)
			{
				this.session.Close();
				throw ex;
			}
			return imageData;
		}

		// Token: 0x0600019E RID: 414 RVA: 0x00005BD0 File Offset: 0x00004BD0
		public int GetImageSizeByCourse(string courseId)
		{
			int imageSize = 0;
			this.session = this.sessionFactory.OpenSession();
			try
			{
				string query = "select c.ImageSize from Course c Where c.CID = :courseId";
				IQuery q = this.session.CreateQuery(query);
				q.SetParameter("courseId", courseId);
				object result = q.UniqueResult();
				bool flag = result != null;
				if (flag)
				{
					imageSize = (int)result;
				}
				this.session.Close();
			}
			catch (Exception ex)
			{
				this.session.Close();
				throw ex;
			}
			return imageSize;
		}

		// Token: 0x0600019F RID: 415 RVA: 0x00005C64 File Offset: 0x00004C64
		public int GetImageNumberOfPageByCourse(string courseId)
		{
			int numberOfPage = 0;
			this.session = this.sessionFactory.OpenSession();
			try
			{
				string query = "select c.NumberOfPage from Course c Where c.CID = :courseId";
				IQuery q = this.session.CreateQuery(query);
				q.SetParameter("courseId", courseId);
				object result = q.UniqueResult();
				bool flag = result != null;
				if (flag)
				{
					numberOfPage = (int)result;
				}
				this.session.Close();
			}
			catch (Exception ex)
			{
				this.session.Close();
				throw ex;
			}
			return numberOfPage;
		}
	}
}
