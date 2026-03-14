using System;
using System.Collections;
using System.Globalization;
using NHibernate;

namespace QuestionLib.Business
{
	// Token: 0x02000022 RID: 34
	public class BOEOSLog : BOBase
	{
		// Token: 0x060001A7 RID: 423 RVA: 0x0000485F File Offset: 0x0000385F
		public BOEOSLog(ISessionFactory sessionFactory)
			: base(sessionFactory)
		{
		}

		// Token: 0x060001A8 RID: 424 RVA: 0x00006050 File Offset: 0x00005050
		public IList LoadLog(string username, string fromdate, string todate)
		{
			this.session = this.sessionFactory.OpenSession();
			IList result;
			try
			{
				string where = "";
				string query = "from EOSLog t Where 1=1 ";
				bool flag = username != "";
				if (flag)
				{
					where += " and Log_Account like :username ";
				}
				bool flag2 = fromdate != "";
				if (flag2)
				{
					where += " and CreatedDate >= :fromdate ";
				}
				bool flag3 = todate != "";
				if (flag3)
				{
					where += " and CreatedDate <= :todate ";
				}
				query += where;
				IQuery q = this.session.CreateQuery(query);
				bool flag4 = username != "";
				if (flag4)
				{
					q.SetParameter("username", "%" + username + "%");
				}
				bool flag5 = fromdate != "";
				if (flag5)
				{
					q.SetParameter("fromdate", DateTime.ParseExact(fromdate + " 00:00:00", "dd/MM/yyyy HH:mm:ss", CultureInfo.InvariantCulture));
				}
				bool flag6 = todate != "";
				if (flag6)
				{
					q.SetParameter("todate", DateTime.ParseExact(todate + " 23:59:59", "dd/MM/yyyy HH:mm:ss", CultureInfo.InvariantCulture));
				}
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
