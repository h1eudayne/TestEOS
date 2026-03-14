using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using NHibernate;
using QuestionLib.Entity;

namespace QuestionLib.Business
{
	// Token: 0x0200001D RID: 29
	public class BOAudio : BOBase
	{
		// Token: 0x0600017A RID: 378 RVA: 0x0000485F File Offset: 0x0000385F
		public BOAudio(ISessionFactory sessionFactory)
			: base(sessionFactory)
		{
		}

		// Token: 0x0600017B RID: 379 RVA: 0x0000486C File Offset: 0x0000386C
		public Audio Load(int auid)
		{
			IList result = null;
			this.session = this.sessionFactory.OpenSession();
			try
			{
				string query = "from Audio a Where a.AuID=:auid";
				IQuery a = this.session.CreateQuery(query);
				a.SetParameter("auid", auid);
				result = a.List();
				this.session.Close();
			}
			catch (Exception ex)
			{
				this.session.Close();
				throw ex;
			}
			return (result.Count > 0) ? ((Audio)result[0]) : null;
		}

		// Token: 0x0600017C RID: 380 RVA: 0x0000490C File Offset: 0x0000390C
		public DataTable LoadAllChapterAudio(int chid, string conStr)
		{
			IList result = new ArrayList();
			SqlConnection con = new SqlConnection(conStr);
			string sql = "SELECT AuID, AudioFile, AudioSize, AudioLength, RepeatTime, PaddingTime, PlayOrder FROM Audio WHERE ChID = " + chid;
			SqlDataAdapter da = new SqlDataAdapter(sql, con);
			DataTable dt = new DataTable();
			da.Fill(dt);
			con.Close();
			return dt;
		}

		// Token: 0x0600017D RID: 381 RVA: 0x00004960 File Offset: 0x00003960
		public Audio LoadAudio(int auid, string conStr)
		{
			SqlConnection con = new SqlConnection(conStr);
			con.Open();
			string sql = "SELECT AudioFile, AudioSize, AudioLength, AudioData, RepeatTime, PaddingTime, PlayOrder FROM Audio WHERE AuID= " + auid;
			SqlCommand cmd = new SqlCommand(sql, con);
			SqlDataReader dr = cmd.ExecuteReader();
			Audio au = null;
			bool flag = dr.Read();
			if (flag)
			{
				au = new Audio();
				au.AudioFile = dr["AudioFile"].ToString();
				au.AudioSize = (int)dr["AudioSize"];
				au.AudioData = new byte[au.AudioSize];
				dr.GetBytes(3, 0L, au.AudioData, 0, au.AudioSize);
				au.AudioLength = (int)dr["AudioLength"];
			}
			dr.Close();
			con.Close();
			return au;
		}

		// Token: 0x0600017E RID: 382 RVA: 0x00004A40 File Offset: 0x00003A40
		public List<AudioInPaper> LoadChapterAudio(int chapterID, string conStr)
		{
			List<AudioInPaper> result = new List<AudioInPaper>();
			SqlConnection con = new SqlConnection(conStr);
			con.Open();
			string sql = "SELECT AudioSize, AudioData, AudioLength, RepeatTime, PaddingTime, PlayOrder FROM Audio WHERE ChID= " + chapterID;
			SqlCommand cmd = new SqlCommand(sql, con);
			SqlDataReader dr = cmd.ExecuteReader();
			while (dr.Read())
			{
				AudioInPaper aip = new AudioInPaper();
				aip.AudioSize = (int)dr["AudioSize"];
				aip.AudioData = new byte[aip.AudioSize];
				dr.GetBytes(1, 0L, aip.AudioData, 0, aip.AudioSize);
				aip.AudioLength = (int)dr["AudioLength"];
				aip.RepeatTime = (byte)dr["RepeatTime"];
				aip.PaddingTime = (int)dr["PaddingTime"];
				aip.PlayOrder = (byte)dr["PlayOrder"];
				result.Add(aip);
			}
			dr.Close();
			con.Close();
			return result;
		}

		// Token: 0x0600017F RID: 383 RVA: 0x00004B70 File Offset: 0x00003B70
		public static bool Delete(int auid, string conStr)
		{
			SqlConnection con = new SqlConnection(conStr);
			con.Open();
			string sql = "DELETE FROM Audio WHERE AuID= " + auid;
			SqlCommand cmd = new SqlCommand(sql, con);
			int r = cmd.ExecuteNonQuery();
			con.Close();
			return r > 0;
		}

		// Token: 0x06000180 RID: 384 RVA: 0x00004BC0 File Offset: 0x00003BC0
		public static bool AudioExist(int auid, string conStr)
		{
			SqlConnection con = new SqlConnection(conStr);
			con.Open();
			string sql = "SELECT * FROM Audio WHERE AuID= " + auid;
			SqlCommand cmd = new SqlCommand(sql, con);
			SqlDataReader dr = cmd.ExecuteReader();
			bool hasRows = dr.HasRows;
			bool flag;
			if (hasRows)
			{
				dr.Close();
				con.Close();
				flag = true;
			}
			else
			{
				dr.Close();
				con.Close();
				flag = false;
			}
			return flag;
		}

		// Token: 0x06000181 RID: 385 RVA: 0x00004C34 File Offset: 0x00003C34
		public static bool AudioFileExist(int chapterID, string audioFile, int audioSize, int audioLength, string conStr)
		{
			SqlConnection con = new SqlConnection(conStr);
			con.Open();
			string sql = "SELECT * FROM Audio WHERE ChID= @chapterID AND AudioFile=@audioFile AND AudioSize = @audioSize AND AudioLength = @audioLength";
			SqlCommand cmd = new SqlCommand(sql, con);
			cmd.Parameters.Add("chapterID", SqlDbType.Int);
			cmd.Parameters["chapterID"].Value = chapterID;
			cmd.Parameters.Add("audioFile", SqlDbType.NVarChar);
			cmd.Parameters["audioFile"].Value = audioFile;
			cmd.Parameters.Add("audioSize", SqlDbType.Int);
			cmd.Parameters["audioSize"].Value = audioSize;
			cmd.Parameters.Add("audioLength", SqlDbType.Int);
			cmd.Parameters["audioLength"].Value = audioLength;
			SqlDataReader dr = cmd.ExecuteReader();
			bool hasRows = dr.HasRows;
			bool flag;
			if (hasRows)
			{
				dr.Close();
				con.Close();
				flag = true;
			}
			else
			{
				dr.Close();
				con.Close();
				flag = false;
			}
			return flag;
		}

		// Token: 0x06000182 RID: 386 RVA: 0x00004D50 File Offset: 0x00003D50
		public static string GetAudioFile(int auid, string conStr)
		{
			SqlConnection con = new SqlConnection(conStr);
			con.Open();
			string sql = "SELECT AudioFile FROM Audio WHERE AuID= " + auid;
			SqlCommand cmd = new SqlCommand(sql, con);
			SqlDataReader dr = cmd.ExecuteReader();
			bool flag = dr.Read();
			string text;
			if (flag)
			{
				string str = dr["AudioFile"].ToString();
				dr.Close();
				con.Close();
				text = str;
			}
			else
			{
				dr.Close();
				con.Close();
				text = null;
			}
			return text;
		}

		// Token: 0x06000183 RID: 387 RVA: 0x00004DD8 File Offset: 0x00003DD8
		public static string GetChapterAudioFile(int chapterID, string conStr)
		{
			SqlConnection con = new SqlConnection(conStr);
			con.Open();
			string sql = "SELECT AudioFile FROM Audio WHERE ChID= " + chapterID;
			SqlCommand cmd = new SqlCommand(sql, con);
			SqlDataReader dr = cmd.ExecuteReader();
			string str = null;
			while (dr.Read())
			{
				bool flag = str == null;
				if (flag)
				{
					str = dr["AudioFile"].ToString();
				}
				else
				{
					str = str + ", " + dr["AudioFile"].ToString();
				}
			}
			dr.Close();
			con.Close();
			return str;
		}

		// Token: 0x06000184 RID: 388 RVA: 0x00004E7C File Offset: 0x00003E7C
		public static int GetAudioLength(int chapterID, string conStr)
		{
			SqlConnection con = new SqlConnection(conStr);
			con.Open();
			string sql = "SELECT AudioLength, RepeatTime, PaddingTime FROM Audio WHERE ChID= " + chapterID;
			SqlCommand cmd = new SqlCommand(sql, con);
			SqlDataReader dr = cmd.ExecuteReader();
			int lengthInSecs = 0;
			while (dr.Read())
			{
				int len = Convert.ToInt32(dr["AudioLength"].ToString());
				int repeatTime = (int)Convert.ToByte(dr["RepeatTime"].ToString());
				int paddingTime = Convert.ToInt32(dr["PaddingTime"].ToString());
				lengthInSecs += (len + paddingTime) * repeatTime;
			}
			dr.Close();
			con.Close();
			return lengthInSecs;
		}

		// Token: 0x06000185 RID: 389 RVA: 0x00004F34 File Offset: 0x00003F34
		public static int GetFileAudioLength(int auid, string conStr)
		{
			SqlConnection con = new SqlConnection(conStr);
			con.Open();
			string sql = "SELECT AudioLength FROM Audio WHERE AuID= " + auid;
			SqlCommand cmd = new SqlCommand(sql, con);
			SqlDataReader dr = cmd.ExecuteReader();
			int lengthInSecs = 0;
			bool flag = dr.Read();
			if (flag)
			{
				lengthInSecs = Convert.ToInt32(dr["AudioLength"].ToString());
			}
			dr.Close();
			con.Close();
			return lengthInSecs;
		}

		// Token: 0x06000186 RID: 390 RVA: 0x00004FB0 File Offset: 0x00003FB0
		public static bool UpdateAudioPlayingInfo(List<Audio> listAudio, string conStr)
		{
			SqlConnection con = new SqlConnection(conStr);
			con.Open();
			string sql = "UPDATE Audio SET RepeatTime=@repeatTime, PaddingTime=@paddingTime, PlayOrder=@playOrder WHERE AuID= @auID";
			SqlCommand cmd = new SqlCommand(sql, con);
			bool flag;
			try
			{
				cmd.Parameters.Add("repeatTime", SqlDbType.TinyInt);
				cmd.Parameters.Add("paddingTime", SqlDbType.Int);
				cmd.Parameters.Add("playOrder", SqlDbType.TinyInt);
				cmd.Parameters.Add("auID", SqlDbType.Int);
				cmd.Transaction = con.BeginTransaction();
				foreach (Audio a in listAudio)
				{
					cmd.Parameters["repeatTime"].Value = a.RepeatTime;
					cmd.Parameters["paddingTime"].Value = a.PaddingTime;
					cmd.Parameters["playOrder"].Value = a.PlayOrder;
					cmd.Parameters["auID"].Value = a.AuID;
					cmd.ExecuteNonQuery();
				}
				cmd.Transaction.Commit();
				con.Close();
				flag = true;
			}
			catch
			{
				cmd.Transaction.Rollback();
				con.Close();
				flag = false;
			}
			return flag;
		}

		// Token: 0x06000187 RID: 391 RVA: 0x00005160 File Offset: 0x00004160
		public static bool CheckAudioPlayingInfo(int chapterID, string conStr)
		{
			bool valid = true;
			SqlConnection con = new SqlConnection(conStr);
			con.Open();
			string sql = "SELECT AudioLength, RepeatTime, PaddingTime, PlayOrder FROM Audio WHERE ChID= " + chapterID;
			SqlCommand cmd = new SqlCommand(sql, con);
			SqlDataReader dr = cmd.ExecuteReader();
			while (dr.Read())
			{
				int len = Convert.ToInt32(dr["AudioLength"].ToString());
				int repeatTime = (int)Convert.ToByte(dr["RepeatTime"].ToString());
				int paddingTime = Convert.ToInt32(dr["PaddingTime"].ToString());
				int playOrder = Convert.ToInt32(dr["PlayOrder"].ToString());
				bool flag = len * repeatTime * paddingTime * playOrder == 0;
				if (flag)
				{
					valid = false;
					break;
				}
			}
			dr.Close();
			con.Close();
			return valid;
		}
	}
}
