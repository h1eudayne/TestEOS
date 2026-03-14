using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Text.RegularExpressions;
using QuestionLib.Entity;

namespace QuestionLib
{
	// Token: 0x0200000C RID: 12
	public class QuestionHelper
	{
		// Token: 0x06000086 RID: 134 RVA: 0x000028CC File Offset: 0x000018CC
		public static void SaveSubmitPaper(string folder, SubmitPaper submitPaper)
		{
			bool flag = !QuestionHelper.IsValidSubmitPaper(submitPaper);
			if (!flag)
			{
				string file = Path.Combine(folder, submitPaper.LoginId.Trim().ToLower() + ".dat");
				string tmp = file + ".tmp";
				string bak = file + ".bak";
				object @lock = QuestionHelper.GetLock(submitPaper.LoginId.Trim().ToLower());
				lock (@lock)
				{
					try
					{
						using (FileStream fs = new FileStream(tmp, FileMode.Create, FileAccess.Write, FileShare.None))
						{
							BinaryFormatter bf = new BinaryFormatter();
							bf.Serialize(fs, submitPaper);
						}
						bool flag2 = File.Exists(file);
						if (flag2)
						{
							File.Replace(tmp, file, bak);
						}
						else
						{
							File.Move(tmp, file);
						}
					}
					catch
					{
					}
					finally
					{
						bool flag3 = File.Exists(tmp);
						if (flag3)
						{
							File.Delete(tmp);
						}
					}
				}
			}
		}

		// Token: 0x06000087 RID: 135 RVA: 0x000029F4 File Offset: 0x000019F4
		private static object GetLock(string key)
		{
			Dictionary<string, object> locks = QuestionHelper._locks;
			object obj;
			lock (locks)
			{
				bool flag = !QuestionHelper._locks.ContainsKey(key);
				if (flag)
				{
					QuestionHelper._locks[key] = new object();
				}
				obj = QuestionHelper._locks[key];
			}
			return obj;
		}

		// Token: 0x06000088 RID: 136 RVA: 0x00002A5C File Offset: 0x00001A5C
		public static SubmitPaper LoadSubmitPaper(string savedFile)
		{
			bool flag = string.IsNullOrEmpty(savedFile) || !File.Exists(savedFile);
			SubmitPaper submitPaper;
			if (flag)
			{
				submitPaper = null;
			}
			else
			{
				try
				{
					using (FileStream fs = new FileStream(savedFile, FileMode.Open, FileAccess.Read, FileShare.Read))
					{
						BinaryFormatter bf = new BinaryFormatter();
						SubmitPaper sp = (SubmitPaper)bf.Deserialize(fs);
						bool flag2 = !QuestionHelper.IsValidSubmitPaper(sp);
						if (flag2)
						{
							submitPaper = null;
						}
						else
						{
							submitPaper = sp;
						}
					}
				}
				catch (Exception ex)
				{
					string bak = savedFile + ".bak";
					bool flag3 = File.Exists(bak);
					if (flag3)
					{
						try
						{
							using (FileStream fs2 = new FileStream(bak, FileMode.Open, FileAccess.Read, FileShare.Read))
							{
								BinaryFormatter bf2 = new BinaryFormatter();
								SubmitPaper sp2 = (SubmitPaper)bf2.Deserialize(fs2);
								bool flag4 = QuestionHelper.IsValidSubmitPaper(sp2);
								if (flag4)
								{
									return sp2;
								}
							}
						}
						catch
						{
						}
					}
					submitPaper = null;
				}
			}
			return submitPaper;
		}

		// Token: 0x06000089 RID: 137 RVA: 0x00002B78 File Offset: 0x00001B78
		private static bool IsValidSubmitPaper(SubmitPaper sp)
		{
			bool flag = sp == null;
			bool flag2;
			if (flag)
			{
				flag2 = false;
			}
			else
			{
				bool flag3 = string.IsNullOrEmpty(sp.LoginId);
				if (flag3)
				{
					flag2 = false;
				}
				else
				{
					bool flag4 = sp.SPaper == null;
					flag2 = !flag4;
				}
			}
			return flag2;
		}

		// Token: 0x0600008A RID: 138 RVA: 0x00002BBC File Offset: 0x00001BBC
		private static Passage GetPassage(Paper oPaper, int pid)
		{
			foreach (object obj in oPaper.ReadingQuestions)
			{
				Passage p = (Passage)obj;
				bool flag = p.PID == pid;
				if (flag)
				{
					return p;
				}
			}
			return null;
		}

		// Token: 0x0600008B RID: 139 RVA: 0x00002C30 File Offset: 0x00001C30
		private static bool ReConstructQuestion(Question sq, Question oq)
		{
			bool flag = sq.QID == oq.QID;
			bool flag8;
			if (flag)
			{
				sq.QBID = oq.QBID;
				sq.CourseId = oq.CourseId;
				sq.Text = oq.Text;
				sq.Mark = oq.Mark;
				sq.ImageData = oq.ImageData;
				sq.ImageSize = oq.ImageSize;
				bool isFillBlank = false;
				bool flag2 = sq.QType == QuestionType.FILL_BLANK_ALL;
				if (flag2)
				{
					isFillBlank = true;
				}
				bool flag3 = sq.QType == QuestionType.FILL_BLANK_GROUP;
				if (flag3)
				{
					isFillBlank = true;
				}
				bool flag4 = sq.QType == QuestionType.FILL_BLANK_EMPTY;
				if (flag4)
				{
					isFillBlank = true;
				}
				foreach (object obj in sq.QuestionAnswers)
				{
					QuestionAnswer sqa = (QuestionAnswer)obj;
					foreach (object obj2 in oq.QuestionAnswers)
					{
						QuestionAnswer oqa = (QuestionAnswer)obj2;
						bool flag5 = sqa.QAID == oqa.QAID;
						if (flag5)
						{
							bool flag6 = isFillBlank;
							if (flag6)
							{
								string s = QuestionHelper.RemoveSpaces(sqa.Text).Trim().ToLower();
								string s2 = QuestionHelper.RemoveSpaces(oqa.Text).Trim().ToLower();
								bool flag7 = s.Equals(s2);
								if (flag7)
								{
									sqa.Chosen = true;
									sqa.Selected = true;
								}
							}
							else
							{
								sqa.Text = oqa.Text;
								sqa.Chosen = oqa.Chosen;
							}
							break;
						}
					}
				}
				flag8 = true;
			}
			else
			{
				flag8 = false;
			}
			return flag8;
		}

		// Token: 0x0600008C RID: 140 RVA: 0x00002E3C File Offset: 0x00001E3C
		private static void ReConstructEssay(EssayQuestion sEssay, EssayQuestion oEssay)
		{
			bool flag = sEssay.EQID == oEssay.EQID;
			if (flag)
			{
				sEssay.QBID = oEssay.QBID;
				sEssay.CourseId = oEssay.CourseId;
				sEssay.Question = oEssay.Question;
			}
		}

		// Token: 0x0600008D RID: 141 RVA: 0x00002E85 File Offset: 0x00001E85
		private static void ReConstructImagePaper(ImagePaper sIP, ImagePaper oIP)
		{
			sIP.Image = oIP.Image;
		}

		// Token: 0x0600008E RID: 142 RVA: 0x00002E98 File Offset: 0x00001E98
		public static Paper Re_ConstructPaper(Paper oPaper, SubmitPaper submitPaper)
		{
			Paper sPaper = submitPaper.SPaper;
			foreach (object obj in sPaper.ReadingQuestions)
			{
				Passage sp = (Passage)obj;
				Passage op = QuestionHelper.GetPassage(oPaper, sp.PID);
				sp.QBID = op.QBID;
				sp.Text = op.Text;
				sp.CourseId = op.CourseId;
				foreach (object obj2 in sp.PassageQuestions)
				{
					Question sq = (Question)obj2;
					foreach (object obj3 in op.PassageQuestions)
					{
						Question oq = (Question)obj3;
						bool flag = QuestionHelper.ReConstructQuestion(sq, oq);
						if (flag)
						{
							break;
						}
					}
				}
			}
			foreach (object obj4 in sPaper.MatchQuestions)
			{
				MatchQuestion sm = (MatchQuestion)obj4;
				foreach (object obj5 in oPaper.MatchQuestions)
				{
					MatchQuestion om = (MatchQuestion)obj5;
					bool flag2 = sm.MID == om.MID;
					if (flag2)
					{
						sm.QBID = om.QBID;
						sm.CourseId = om.CourseId;
						sm.ColumnA = om.ColumnA;
						sm.ColumnB = om.ColumnB;
						sm.Solution = om.Solution;
						sm.Mark = om.Mark;
						break;
					}
				}
			}
			foreach (object obj6 in sPaper.GrammarQuestions)
			{
				Question sq2 = (Question)obj6;
				foreach (object obj7 in oPaper.GrammarQuestions)
				{
					Question oq2 = (Question)obj7;
					bool flag3 = QuestionHelper.ReConstructQuestion(sq2, oq2);
					if (flag3)
					{
						break;
					}
				}
			}
			foreach (object obj8 in sPaper.IndicateMQuestions)
			{
				Question sq3 = (Question)obj8;
				foreach (object obj9 in oPaper.IndicateMQuestions)
				{
					Question oq3 = (Question)obj9;
					bool flag4 = QuestionHelper.ReConstructQuestion(sq3, oq3);
					if (flag4)
					{
						break;
					}
				}
			}
			foreach (object obj10 in sPaper.FillBlankQuestions)
			{
				Question sq4 = (Question)obj10;
				foreach (object obj11 in oPaper.FillBlankQuestions)
				{
					Question oq4 = (Question)obj11;
					bool flag5 = QuestionHelper.ReConstructQuestion(sq4, oq4);
					if (flag5)
					{
						break;
					}
				}
			}
			bool flag6 = oPaper.EssayQuestion != null;
			if (flag6)
			{
				QuestionHelper.ReConstructEssay(sPaper.EssayQuestion, oPaper.EssayQuestion);
			}
			bool flag7 = oPaper.ImgPaper != null;
			if (flag7)
			{
				QuestionHelper.ReConstructImagePaper(sPaper.ImgPaper, oPaper.ImgPaper);
			}
			sPaper.OneSecSilence = oPaper.OneSecSilence;
			sPaper.ListAudio = oPaper.ListAudio;
			return sPaper;
		}

		// Token: 0x0600008F RID: 143 RVA: 0x00003360 File Offset: 0x00002360
		public static string RemoveSpaces(string s)
		{
			s = s.Trim();
			string temp;
			do
			{
				temp = s;
				s = s.Replace("  ", " ");
			}
			while (s.Length != temp.Length);
			return s;
		}

		// Token: 0x06000090 RID: 144 RVA: 0x000033A8 File Offset: 0x000023A8
		public static string RemoveAllSpaces(string s)
		{
			s = s.Trim();
			string temp;
			do
			{
				temp = s;
				s = s.Replace(" ", "");
			}
			while (s.Length != temp.Length);
			return s;
		}

		// Token: 0x06000091 RID: 145 RVA: 0x000033F0 File Offset: 0x000023F0
		public static bool IsFillBlank(QuestionType qt)
		{
			bool flag = qt == QuestionType.FILL_BLANK_ALL;
			bool flag2;
			if (flag)
			{
				flag2 = true;
			}
			else
			{
				bool flag3 = qt == QuestionType.FILL_BLANK_GROUP;
				if (flag3)
				{
					flag2 = true;
				}
				else
				{
					bool flag4 = qt == QuestionType.FILL_BLANK_EMPTY;
					flag2 = flag4;
				}
			}
			return flag2;
		}

		// Token: 0x06000092 RID: 146 RVA: 0x00003428 File Offset: 0x00002428
		private static string RemoveNewLine(string s)
		{
			s = s.Replace(Environment.NewLine, "");
			s = QuestionHelper.RemoveSpaces(s);
			return s;
		}

		// Token: 0x06000093 RID: 147 RVA: 0x00003458 File Offset: 0x00002458
		public static string WordWrap(string str, int width)
		{
			string pattern = QuestionHelper.fillBlank_Pattern;
			Regex r = new Regex(pattern, RegexOptions.IgnoreCase | RegexOptions.Singleline);
			MatchCollection mc = r.Matches(str);
			str = r.Replace(str, "(###)");
			string[] lines = QuestionHelper.SplitLines(str);
			StringBuilder strBuilder = new StringBuilder();
			for (int i = 0; i < lines.Length; i++)
			{
				string line = lines[i];
				bool flag = i < lines.Length - 1;
				if (flag)
				{
					line = lines[i] + Environment.NewLine;
				}
				ArrayList words = QuestionHelper.Explode(line, QuestionHelper.splitChars);
				int curLineLength = 0;
				for (int j = 0; j < words.Count; j++)
				{
					string word = (string)words[j];
					bool flag2 = curLineLength + word.Length > width;
					if (flag2)
					{
						bool flag3 = curLineLength > 0;
						if (flag3)
						{
							bool flag4 = !strBuilder.ToString().EndsWith(Environment.NewLine);
							if (flag4)
							{
								strBuilder.Append(Environment.NewLine);
							}
							curLineLength = 0;
						}
						while (word.Length > width)
						{
							strBuilder.Append(word.Substring(0, width - 1) + "-");
							word = word.Substring(width - 1);
							bool flag5 = !strBuilder.ToString().EndsWith(Environment.NewLine);
							if (flag5)
							{
								strBuilder.Append(Environment.NewLine);
							}
							strBuilder.Append(Environment.NewLine);
						}
						word = word.TrimStart(new char[0]);
					}
					strBuilder.Append(word);
					curLineLength += word.Length;
				}
			}
			str = strBuilder.ToString();
			pattern = "\\(###\\)";
			r = new Regex(pattern, RegexOptions.IgnoreCase | RegexOptions.Singleline);
			for (int k = 0; k < mc.Count; k++)
			{
				string ans = QuestionHelper.RemoveNewLine(mc[k].Value);
				str = r.Replace(str, ans, 1);
			}
			return str;
		}

		// Token: 0x06000094 RID: 148 RVA: 0x00003668 File Offset: 0x00002668
		private static ArrayList Explode(string str, char[] splitChars)
		{
			ArrayList parts = new ArrayList();
			int startIndex = 0;
			for (;;)
			{
				int index = str.IndexOfAny(splitChars, startIndex);
				bool flag = index == -1;
				if (flag)
				{
					break;
				}
				string word = str.Substring(startIndex, index - startIndex);
				char nextChar = str.Substring(index, 1)[0];
				bool flag2 = char.IsWhiteSpace(nextChar);
				if (flag2)
				{
					parts.Add(word);
					parts.Add(nextChar.ToString());
				}
				else
				{
					parts.Add(word + nextChar.ToString());
				}
				startIndex = index + 1;
			}
			parts.Add(str.Substring(startIndex));
			return parts;
		}

		// Token: 0x06000095 RID: 149 RVA: 0x00003714 File Offset: 0x00002714
		private static string[] SplitLines(string str)
		{
			string pattern = Environment.NewLine;
			Regex r = new Regex(pattern, RegexOptions.IgnoreCase | RegexOptions.Compiled);
			return r.Split(str);
		}

		// Token: 0x06000096 RID: 150 RVA: 0x0000373C File Offset: 0x0000273C
		public static string[] GetFillBlankWord(string text)
		{
			string pattern = QuestionHelper.fillBlank_Pattern;
			Regex r = new Regex(pattern, RegexOptions.IgnoreCase | RegexOptions.Singleline);
			MatchCollection mc = r.Matches(text);
			string[] temp = new string[mc.Count];
			for (int i = 0; i < mc.Count; i++)
			{
				string val = mc[i].Value.Remove(0, 1);
				val = val.Remove(val.Length - 1, 1);
				temp[i] = val;
			}
			return temp;
		}

		// Token: 0x06000097 RID: 151 RVA: 0x000037C0 File Offset: 0x000027C0
		public static string Sec2TimeString(int sec)
		{
			int h = sec / 3600;
			int i = sec % 3600 / 60;
			int s = sec % 60;
			string hS = "0" + h;
			hS = hS.Substring(hS.Length - 2, 2);
			string mS = "0" + i;
			mS = mS.Substring(mS.Length - 2, 2);
			string sS = "0" + s;
			sS = sS.Substring(sS.Length - 2, 2);
			return string.Concat(new string[] { hS, ":", mS, ":", sS });
		}

		// Token: 0x04000041 RID: 65
		public static char[] lo_deli = new char[] { ';' };

		// Token: 0x04000042 RID: 66
		private static readonly Dictionary<string, object> _locks = new Dictionary<string, object>();

		// Token: 0x04000043 RID: 67
		public static string[] MultipleChoiceQuestionType = new string[] { "Grammar", "Indicate Mistake" };

		// Token: 0x04000044 RID: 68
		public static string fillBlank_Pattern = "\\([0-9a-zA-Z;:=?<>/`,'’ .+_~!@#$%^&*\\r\\n-]+\\)";

		// Token: 0x04000045 RID: 69
		private static char[] splitChars = new char[] { ' ', '-', '\t' };

		// Token: 0x04000046 RID: 70
		public static int LineWidth = 100;
	}
}
