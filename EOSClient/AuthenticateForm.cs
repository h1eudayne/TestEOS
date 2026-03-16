using System;
using System.ComponentModel;
using System.Configuration;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Security.Principal;
using System.Windows.Forms;
using EncryptData;
using IRemote;
using NAudio.Wave;
using QuestionLib;

namespace EOSClient
{
	// Token: 0x02000002 RID: 2
	public partial class AuthenticateForm : Form
	{
		// Token: 0x06000001 RID: 1 RVA: 0x00002050 File Offset: 0x00000250
		public AuthenticateForm()
		{
			this.InitializeComponent();
		}

		// Token: 0x06000004 RID: 4 RVA: 0x00002B68 File Offset: 0x00000D68
		private void btnLogin_Click(object sender, EventArgs e)
		{
			this.btnLogin.Enabled = false;
			this.btnCancel.Enabled = false;
			bool flag = this.txtExamCode.Text.Trim().Equals("");
			if (flag)
			{
				MessageBox.Show("Please provide an Exam code", "Login");
				this.btnLogin.Enabled = true;
				this.btnCancel.Enabled = true;
			}
			else
			{
				bool flag2 = this.txtUser.Text.Trim().Equals("");
				if (flag2)
				{
					MessageBox.Show("Please provide an username", "Login");
					this.btnLogin.Enabled = true;
					this.btnCancel.Enabled = true;
				}
				else
				{
					bool flag3 = this.txtPassword.Text.Trim().Equals("");
					if (flag3)
					{
						MessageBox.Show("Please provide a password", "Login");
						this.btnLogin.Enabled = true;
						this.btnCancel.Enabled = true;
					}
					else
					{
						bool flag4 = this.txtDomain.Text.Trim().Equals("");
						if (flag4)
						{
							MessageBox.Show("Please provide a domain address", "Login");
							this.btnLogin.Enabled = true;
							this.btnCancel.Enabled = true;
						}
						else
						{
							bool flag5 = string.IsNullOrEmpty(this.cbServer.Text);
							if (flag5)
							{
								MessageBox.Show("Please provide a server", "Login");
								this.btnLogin.Enabled = true;
								this.btnCancel.Enabled = true;
							}
							else
							{
								try
								{
									string ipaddr = "0.0.0.0";
									int port = -1;
									string publicIp = "0.0.0.0";
									EndpointItem item = this.cbServer.SelectedItem as EndpointItem;
									bool flag6 = item != null;
									if (flag6)
									{
										ipaddr = item.IP;
										port = item.Port;
										publicIp = item.Public_IP;
									}
									this.si.IP = ipaddr;
									this.si.Port = port;
									bool flag7 = !publicIp.Trim().Equals("");
									if (flag7)
									{
										this.si.IP = publicIp;
									}
									IRemoteServer server = this.TryConnect(this.si.IP, this.si.Port);
									bool flag8 = server == null;
									if (flag8)
									{
										MessageBox.Show("Unable to connect to the exam server. Please check your network.", "Connection error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
										this.btnLogin.Enabled = true;
										this.btnCancel.Enabled = true;
									}
									else
									{
										RegisterData rd = new RegisterData();
										rd.Login = this.txtUser.Text.ToLower().Trim();
										rd.Password = this.txtPassword.Text;
										rd.ExamCode = this.txtExamCode.Text;
										rd.Machine = Environment.MachineName.ToUpper();
										rd.ClientVersion = this.clientVersion;
										EOSData ed = server.ConductExam(rd);
										bool flag9 = ed.Status == RegisterStatus.CLIENT_OUTDATED;
										if (flag9)
										{
											MessageBox.Show("Client version is outdated. Please update to the latest version!", "Start exam", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
											this.btnLogin.Enabled = true;
											this.btnCancel.Enabled = true;
										}
										else
										{
											bool flag10 = ed.Status == RegisterStatus.EXAM_CODE_NOT_EXISTS;
											if (flag10)
											{
												MessageBox.Show("Exam code is not available!", "Start exam", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
												this.btnLogin.Enabled = true;
												this.btnCancel.Enabled = true;
											}
											else
											{
												bool flag11 = ed.Status == RegisterStatus.FINISHED;
												if (flag11)
												{
													MessageBox.Show("The exam is finished!", "Start exam", MessageBoxButtons.OK, MessageBoxIcon.Hand);
													this.btnLogin.Enabled = true;
													this.btnCancel.Enabled = true;
												}
												else
												{
													bool flag12 = ed.Status == RegisterStatus.REGISTERED;
													if (flag12)
													{
														MessageBox.Show("This user [" + this.txtUser.Text + "] is already registered. Need re-assign to continue the exam.", "Exam Registering", MessageBoxButtons.OK, MessageBoxIcon.Hand);
														this.btnLogin.Enabled = true;
														this.btnCancel.Enabled = true;
													}
													else
													{
														bool flag13 = ed.Status == RegisterStatus.REGISTER_ERROR;
														if (flag13)
														{
															MessageBox.Show("Register ERROR, try again", "Exam Registering", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
															this.btnLogin.Enabled = true;
															this.btnCancel.Enabled = true;
														}
														else
														{
															bool flag14 = ed.Status == RegisterStatus.NOT_ALLOW_MACHINE;
															if (flag14)
															{
																MessageBox.Show("Your machine is not allowed to take the exam!", "Exam Registering", MessageBoxButtons.OK, MessageBoxIcon.Hand);
																this.btnLogin.Enabled = true;
																this.btnCancel.Enabled = true;
															}
															else
															{
																bool flag15 = ed.Status == RegisterStatus.NOT_ALLOW_STUDENT;
																if (flag15)
																{
																	MessageBox.Show("The account is NOT allowed to take the exam!", "Exam Registering", MessageBoxButtons.OK, MessageBoxIcon.Hand);
																	this.btnLogin.Enabled = true;
																	this.btnCancel.Enabled = true;
																}
																else
																{
																	bool flag16 = ed.Status == RegisterStatus.LOGIN_FAILED;
																	if (flag16)
																	{
																		MessageBox.Show("Sorry, unable to verify your information. Check [User Name] and [Password]!", "Login failed");
																		this.btnLogin.Enabled = true;
																		this.btnCancel.Enabled = true;
																	}
																}
															}
														}
													}
												}
											}
										}
										bool flag17 = ed.Status == RegisterStatus.NEW || ed.Status == RegisterStatus.RE_ASSIGN;
					if (flag17)
					{
						base.Hide();
						Assembly a = null;
						// Try loading UI from local MockExamClient.dll first (contains fixed UI)
						string localDll = Path.Combine(Application.StartupPath, "MockExamClient.dll");
						if (File.Exists(localDll))
						{
							a = Assembly.LoadFrom(localDll);
						}
						else
						{
							// Fall back to server-sent DLL
							ed.GUI = GZipHelper.DeCompress(ed.GUI, ed.OriginSize);
							a = Assembly.Load(ed.GUI);
						}
						Type fType = a.GetType("ExamClient.frmEnglishExamClient");
						Form f = (Form)Activator.CreateInstance(fType);
						IExamclient iec = (IExamclient)f;
						ed.GUI = null;
						ed.ServerInfomation = this.si;
						ed.RegData = rd;
						iec.SetExamData(ed);
						f.Show();
						}
									}
								}
								catch (Exception ex)
								{
									MessageBox.Show(ex.ToString());
									MessageBox.Show("Start Exam Error:\nCannot connect to the EOS server!\n", "Connecting...", MessageBoxButtons.OK, MessageBoxIcon.Hand);
									this.btnLogin.Enabled = true;
									this.btnCancel.Enabled = true;
								}
							}
						}
					}
				}
			}
		}

		// Token: 0x06000005 RID: 5 RVA: 0x000031A0 File Offset: 0x000013A0
		private IRemoteServer TryConnect(string ip, int port)
		{
			try
			{
				string url = string.Concat(new object[] { "tcp://", ip, ":", port, "/Server" });
				IRemoteServer s = (IRemoteServer)Activator.GetObject(typeof(IRemoteServer), url);
				bool flag = s != null && s.Ping();
				if (flag)
				{
					return s;
				}
			}
			catch
			{
			}
			return null;
		}

		// Token: 0x06000006 RID: 6 RVA: 0x00003228 File Offset: 0x00001428
		private void btnCancel_Click(object sender, EventArgs e)
		{
			Application.Exit();
			Environment.Exit(0);
		}

		// Token: 0x06000007 RID: 7 RVA: 0x00003238 File Offset: 0x00001438
		private void AuthenticateForm_Load(object sender, EventArgs e)
		{
			string serverInfoFile = "EOS_Server_Info.dat";
			bool flag = File.Exists(Application.StartupPath + "\\" + serverInfoFile);
			if (flag)
			{
				try
				{
					string key = "04021976";
					byte[] buf = EncryptSupport.DecryptQuestions_FromFile(Application.StartupPath + "\\" + serverInfoFile, key);
					this.si = (ServerInfo)EncryptSupport.ByteArrayToObject(buf);
					bool flag2 = !this.version.Equals(this.si.Version);
					if (flag2)
					{
						MessageBox.Show("Wrong EOS client version! Please copy the right EOS client version.", "Version Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
						Application.Exit();
					}
					bool flag3 = this.si.Port != -1 && !string.IsNullOrEmpty(this.si.IP);
					if (flag3)
					{
						this.cbServer.DisplayMember = "Display";
						this.cbServer.Items.Add(new EndpointItem
						{
							IP = this.si.IP,
							Port = this.si.Port,
							Public_IP = this.si.Public_IP,
							Display = this.si.PortName
						});
					}
					bool flag4 = this.si.SecondaryPort != -1 && !string.IsNullOrEmpty(this.si.SecondaryIP);
					if (flag4)
					{
						this.cbServer.DisplayMember = "Display";
						this.cbServer.Items.Add(new EndpointItem
						{
							IP = this.si.SecondaryIP,
							Port = this.si.SecondaryPort,
							Public_IP = this.si.Secondary_Public_IP,
							Display = this.si.SecondPortName
						});
					}
					bool flag5 = this.si.ThirdPort != -1 && !string.IsNullOrEmpty(this.si.ThirdIP);
					if (flag5)
					{
						this.cbServer.DisplayMember = "Display";
						this.cbServer.Items.Add(new EndpointItem
						{
							IP = this.si.ThirdIP,
							Port = this.si.ThirdPort,
							Public_IP = this.si.Third_Public_IP,
							Display = this.si.ThirdPortName
						});
					}
					bool flag6 = this.si.FourthPort != -1 && !string.IsNullOrEmpty(this.si.FourthIP);
					if (flag6)
					{
						this.cbServer.DisplayMember = "Display";
						this.cbServer.Items.Add(new EndpointItem
						{
							IP = this.si.FourthIP,
							Port = this.si.FourthPort,
							Public_IP = this.si.Fourth_Public_IP,
							Display = this.si.FourthPortName
						});
					}
					bool flag7 = this.si.FifthPort != -1 && !string.IsNullOrEmpty(this.si.FifthIP);
					if (flag7)
					{
						this.cbServer.DisplayMember = "Display";
						this.cbServer.Items.Add(new EndpointItem
						{
							IP = this.si.FifthIP,
							Port = this.si.FifthPort,
							Public_IP = this.si.Fifth_Public_IP,
							Display = this.si.FifthPortName
						});
					}
				}
				catch
				{
					MessageBox.Show("Wrong [" + serverInfoFile + "] file format! Please copy the right EOS client version.", "Version Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
					Application.Exit();
				}
			}
			else
			{
				MessageBox.Show("File [" + serverInfoFile + "] not found! Please copy the right EOS client version.", "Version Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				Application.Exit();
			}
		}

		// Token: 0x06000008 RID: 8 RVA: 0x0000363C File Offset: 0x0000183C
		private bool IsAdministrator()
		{
			WindowsIdentity identity = WindowsIdentity.GetCurrent();
			WindowsPrincipal principle = new WindowsPrincipal(identity);
			return principle.IsInRole(WindowsBuiltInRole.Administrator);
		}

		// Token: 0x06000009 RID: 9 RVA: 0x00003670 File Offset: 0x00001870
		private void linkLBLCheckFont_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			bool flag = this.fcf == null || this.fcf.IsDisposed;
			if (flag)
			{
				this.fcf = new frmCheckFont();
			}
			this.fcf.Show();
		}

		// Token: 0x0600000A RID: 10 RVA: 0x000036B0 File Offset: 0x000018B0
		private void lblLinkCheckSound_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			bool flag = !File.Exists("ghosts.mp3");
			if (flag)
			{
				MessageBox.Show("Audio file ghosts.mp3 cannot be found!", "Check sound");
			}
			else
			{
				this.PlayFromUrl("ghosts.mp3");
			}
		}

		// Token: 0x0600000B RID: 11 RVA: 0x000036F0 File Offset: 0x000018F0
		public void PlayFromUrl(string url)
		{
			FileStream fs = File.OpenRead(url);
			byte[] bufData = new byte[fs.Length];
			fs.Read(bufData, 0, (int)fs.Length);
			fs.Close();
			Stream ms = new MemoryStream(bufData);
			Mp3FileReader reader = new Mp3FileReader(ms);
			WaveOut waveOut = new WaveOut();
			waveOut.Init(reader);
			waveOut.Volume = 1f;
			waveOut.Play();
		}

		// Token: 0x0600000C RID: 12 RVA: 0x00003228 File Offset: 0x00001428
		private void AuthenticateForm_FormClosing(object sender, FormClosingEventArgs e)
		{
			Application.Exit();
			Environment.Exit(0);
		}

		// Token: 0x04000012 RID: 18
		private string clientVersion = "23.02.20.26";

		// Token: 0x04000013 RID: 19
		private string version = "C3AF3F4B-EA15-4EDA-9750-C0214649FEC8";

		// Token: 0x04000014 RID: 20
		private ServerInfo si = null;

		// Token: 0x04000015 RID: 21
		private frmCheckFont fcf = null;
	}
}
