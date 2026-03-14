using System;
using System.Data;
using System.Data.SqlClient;
using NHibernate;
using QuestionLib.Entity;

namespace QuestionLib.Business
{
	// Token: 0x0200002B RID: 43
	public class BOTestTemplateDetails : BOBase
	{
		// Token: 0x060001D4 RID: 468 RVA: 0x0000485F File Offset: 0x0000385F
		public BOTestTemplateDetails(ISessionFactory sessionFactory)
			: base(sessionFactory)
		{
		}

		// Token: 0x060001D5 RID: 469 RVA: 0x00007DD4 File Offset: 0x00006DD4
		public static DataTable LoadTestTemplateDetails(string testTemplateID, string conStr)
		{
			SqlConnection con = new SqlConnection(conStr);
			con.Open();
			string sql = "SELECT tt.TestTemplateName AS 'Test template name', tt.CID, ch.Name AS 'Chapter', ttd.NoQInTest, tmp.QString AS 'Question type' FROM TestTemplateDetails AS ttd INNER JOIN TestTemplate AS tt ON tt.TestTemplateID = ttd.TestTemplateID INNER JOIN Chapter AS ch ON ch.ChID = ttd.ChapterID INNER JOIN (SELECT 0 AS QType, 'Reading' AS QString UNION SELECT 1 AS QType, 'Multiple choice' AS QString UNION SELECT 2 AS QType, 'Indicate mistake' AS QString UNION SELECT 3 AS QType, 'Match' AS QString UNION SELECT 4 AS QType, 'Fill blank' AS QString ) AS tmp ON ttd.QuestionType = tmp.QType WHERE tt.TestTemplateID = @testTemplateID";
			SqlCommand cmd = new SqlCommand(sql, con);
			cmd.Parameters.Add("testTemplateID", SqlDbType.Int);
			cmd.Parameters["testTemplateID"].Value = testTemplateID;
			SqlDataAdapter da = new SqlDataAdapter(cmd);
			DataTable dt = new DataTable();
			da.Fill(dt);
			con.Close();
			return dt;
		}

		// Token: 0x060001D6 RID: 470 RVA: 0x00007E50 File Offset: 0x00006E50
		public static DataTable LoadTestTemplateDetails(QuestionType questionType, int testTemplateID, string conStr)
		{
			SqlConnection con = new SqlConnection(conStr);
			con.Open();
			string sql = "SELECT ChapterId,QuestionType,NoQInTest FROM TestTemplateDetails WHERE TestTemplateID = @testTemplateID AND QuestionType = @questionType ";
			SqlCommand cmd = new SqlCommand(sql, con);
			cmd.Parameters.Add("testTemplateID", SqlDbType.Int);
			cmd.Parameters["testTemplateID"].Value = testTemplateID;
			cmd.Parameters.Add("questionType", SqlDbType.Int);
			cmd.Parameters["questionType"].Value = questionType;
			SqlDataAdapter da = new SqlDataAdapter(cmd);
			DataTable dt = new DataTable();
			da.Fill(dt);
			con.Close();
			return dt;
		}
	}
}
