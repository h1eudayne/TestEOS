using System;
using System.ComponentModel;
using System.Drawing;
using System.Security.Principal;
using System.Windows.Forms;

namespace EOSClient
{
	// Token: 0x02000004 RID: 4
	public partial class frmAnnoucement : Form
	{
		// Token: 0x06000016 RID: 22 RVA: 0x000037A9 File Offset: 0x000019A9
		public frmAnnoucement()
		{
			this.InitializeComponent();
		}

		// Token: 0x06000017 RID: 23 RVA: 0x000037C4 File Offset: 0x000019C4
		private void btnNext_Click(object sender, EventArgs e)
		{
			AuthenticateForm f = new AuthenticateForm();
			f.Show();
			base.Hide();
		}

		// Token: 0x06000018 RID: 24 RVA: 0x000037E6 File Offset: 0x000019E6
		private void chbRead_CheckedChanged(object sender, EventArgs e)
		{
			this.btnNext.Enabled = this.chbRead.Checked;
		}

		// Token: 0x06000019 RID: 25 RVA: 0x00003800 File Offset: 0x00001A00
		private bool IsAdministrator()
		{
			WindowsIdentity identity = WindowsIdentity.GetCurrent();
			WindowsPrincipal principle = new WindowsPrincipal(identity);
			return principle.IsInRole(WindowsBuiltInRole.Administrator);
		}

		// Token: 0x0600001A RID: 26 RVA: 0x00003834 File Offset: 0x00001A34
		private void frmAnnoucement_Load(object sender, EventArgs e)
		{
			string exePath = Application.ExecutablePath.ToLower();
			bool flag = exePath.Contains("\\temp\\") || exePath.Contains("\\appdata\\local\\temp\\") || exePath.EndsWith(".zip") || exePath.EndsWith(".rar");
			if (flag)
			{
				MessageBox.Show("Please extract the software to a folder before running it.", "Runtime environment error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
				Application.Exit();
				Environment.Exit(0);
			}
			// Disabled: Admin check not required for development
			// bool flag2 = !this.IsAdministrator();
			// if (flag2)
			// {
			// 	MessageBox.Show("You must login Windows as System Administrator or Run [EOS Client] as Administrator.", "Run as Administrator!", MessageBoxButtons.OK, MessageBoxIcon.Hand);
			// 	Application.Exit();
			// }
		}

		// Token: 0x0600001B RID: 27 RVA: 0x000038D0 File Offset: 0x00001AD0
		private void txtNoiQuy_TextChanged(object sender, EventArgs e)
		{
		}
	}
}
