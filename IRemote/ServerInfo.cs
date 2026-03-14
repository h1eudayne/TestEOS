using System;

namespace IRemote
{
	// Token: 0x02000009 RID: 9
	[Serializable]
	public class ServerInfo
	{
		// Token: 0x06000014 RID: 20 RVA: 0x00002132 File Offset: 0x00000332
		public ServerInfo()
		{
		}

		// Token: 0x06000015 RID: 21 RVA: 0x0000213C File Offset: 0x0000033C
		public ServerInfo(string ip, string secondaryip, string thirdip, string fourthip, string fifthip, int port, int secondaryport, int thirdport, int fourthport, int fifthport, string portname, string secondportname, string thirdportname, string fourthportname, string fifthportname, string public_ip, string secondary_public_ip, string third_public_ip, string fourth_public_ip, string fifth_public_ip, string serverAlias, string version, string ip_range_wlan)
		{
			this._ip = ip;
			this._secondaryip = secondaryip;
			this._thirdip = thirdip;
			this._fourthip = fourthip;
			this._fifthip = fifthip;
			this._port = port;
			this._secondaryport = secondaryport;
			this._thirdport = thirdport;
			this._fourthport = fourthport;
			this._fifthport = fifthport;
			this._portname = portname;
			this._secondaryportname = secondportname;
			this._thirdportname = thirdportname;
			this._fourthportname = fourthportname;
			this._fifthportname = fifthportname;
			this._public_ip = public_ip;
			this._secondary_public_ip = secondary_public_ip;
			this._third_public_ip = third_public_ip;
			this._fourth_public_ip = fourth_public_ip;
			this._fifth_public_ip = fifth_public_ip;
			this._serverAlias = serverAlias;
			this._version = version;
			this._ip_range_wlan = ip_range_wlan;
		}

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000016 RID: 22 RVA: 0x00002208 File Offset: 0x00000408
		// (set) Token: 0x06000017 RID: 23 RVA: 0x00002220 File Offset: 0x00000420
		public string IP
		{
			get
			{
				return this._ip;
			}
			set
			{
				this._ip = value;
			}
		}

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000018 RID: 24 RVA: 0x0000222C File Offset: 0x0000042C
		// (set) Token: 0x06000019 RID: 25 RVA: 0x00002244 File Offset: 0x00000444
		public string SecondaryIP
		{
			get
			{
				return this._secondaryip;
			}
			set
			{
				this._secondaryip = value;
			}
		}

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x0600001A RID: 26 RVA: 0x00002250 File Offset: 0x00000450
		// (set) Token: 0x0600001B RID: 27 RVA: 0x00002268 File Offset: 0x00000468
		public string ThirdIP
		{
			get
			{
				return this._thirdip;
			}
			set
			{
				this._thirdip = value;
			}
		}

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x0600001C RID: 28 RVA: 0x00002274 File Offset: 0x00000474
		// (set) Token: 0x0600001D RID: 29 RVA: 0x0000228C File Offset: 0x0000048C
		public string FourthIP
		{
			get
			{
				return this._fourthip;
			}
			set
			{
				this._fourthip = value;
			}
		}

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x0600001E RID: 30 RVA: 0x00002298 File Offset: 0x00000498
		// (set) Token: 0x0600001F RID: 31 RVA: 0x000022B0 File Offset: 0x000004B0
		public string FifthIP
		{
			get
			{
				return this._fifthip;
			}
			set
			{
				this._fifthip = value;
			}
		}

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x06000020 RID: 32 RVA: 0x000022BC File Offset: 0x000004BC
		// (set) Token: 0x06000021 RID: 33 RVA: 0x000022D4 File Offset: 0x000004D4
		public int Port
		{
			get
			{
				return this._port;
			}
			set
			{
				this._port = value;
			}
		}

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x06000022 RID: 34 RVA: 0x000022E0 File Offset: 0x000004E0
		// (set) Token: 0x06000023 RID: 35 RVA: 0x000022F8 File Offset: 0x000004F8
		public int SecondaryPort
		{
			get
			{
				return this._secondaryport;
			}
			set
			{
				this._secondaryport = value;
			}
		}

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x06000024 RID: 36 RVA: 0x00002304 File Offset: 0x00000504
		// (set) Token: 0x06000025 RID: 37 RVA: 0x0000231C File Offset: 0x0000051C
		public int ThirdPort
		{
			get
			{
				return this._thirdport;
			}
			set
			{
				this._thirdport = value;
			}
		}

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x06000026 RID: 38 RVA: 0x00002328 File Offset: 0x00000528
		// (set) Token: 0x06000027 RID: 39 RVA: 0x00002340 File Offset: 0x00000540
		public int FourthPort
		{
			get
			{
				return this._fourthport;
			}
			set
			{
				this._fourthport = value;
			}
		}

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x06000028 RID: 40 RVA: 0x0000234C File Offset: 0x0000054C
		// (set) Token: 0x06000029 RID: 41 RVA: 0x00002364 File Offset: 0x00000564
		public int FifthPort
		{
			get
			{
				return this._fifthport;
			}
			set
			{
				this._fifthport = value;
			}
		}

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x0600002A RID: 42 RVA: 0x00002370 File Offset: 0x00000570
		// (set) Token: 0x0600002B RID: 43 RVA: 0x00002388 File Offset: 0x00000588
		public string PortName
		{
			get
			{
				return this._portname;
			}
			set
			{
				this._portname = value;
			}
		}

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x0600002C RID: 44 RVA: 0x00002394 File Offset: 0x00000594
		// (set) Token: 0x0600002D RID: 45 RVA: 0x000023AC File Offset: 0x000005AC
		public string SecondPortName
		{
			get
			{
				return this._secondaryportname;
			}
			set
			{
				this._secondaryportname = value;
			}
		}

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x0600002E RID: 46 RVA: 0x000023B8 File Offset: 0x000005B8
		// (set) Token: 0x0600002F RID: 47 RVA: 0x000023D0 File Offset: 0x000005D0
		public string ThirdPortName
		{
			get
			{
				return this._thirdportname;
			}
			set
			{
				this._thirdportname = value;
			}
		}

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x06000030 RID: 48 RVA: 0x000023DC File Offset: 0x000005DC
		// (set) Token: 0x06000031 RID: 49 RVA: 0x000023F4 File Offset: 0x000005F4
		public string FourthPortName
		{
			get
			{
				return this._fourthportname;
			}
			set
			{
				this._fourthportname = value;
			}
		}

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x06000032 RID: 50 RVA: 0x00002400 File Offset: 0x00000600
		// (set) Token: 0x06000033 RID: 51 RVA: 0x00002418 File Offset: 0x00000618
		public string FifthPortName
		{
			get
			{
				return this._fifthportname;
			}
			set
			{
				this._fifthportname = value;
			}
		}

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x06000034 RID: 52 RVA: 0x00002424 File Offset: 0x00000624
		// (set) Token: 0x06000035 RID: 53 RVA: 0x0000243C File Offset: 0x0000063C
		public string ServerAlias
		{
			get
			{
				return this._serverAlias;
			}
			set
			{
				this._serverAlias = value;
			}
		}

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x06000036 RID: 54 RVA: 0x00002448 File Offset: 0x00000648
		// (set) Token: 0x06000037 RID: 55 RVA: 0x00002460 File Offset: 0x00000660
		public string Version
		{
			get
			{
				return this._version;
			}
			set
			{
				this._version = value;
			}
		}

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x06000038 RID: 56 RVA: 0x0000246C File Offset: 0x0000066C
		// (set) Token: 0x06000039 RID: 57 RVA: 0x00002484 File Offset: 0x00000684
		public string MonitorServer_IP
		{
			get
			{
				return this._monitor_IP;
			}
			set
			{
				this._monitor_IP = value;
			}
		}

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x0600003A RID: 58 RVA: 0x00002490 File Offset: 0x00000690
		// (set) Token: 0x0600003B RID: 59 RVA: 0x000024A8 File Offset: 0x000006A8
		public int MonitorServer_Port
		{
			get
			{
				return this._monitor_port;
			}
			set
			{
				this._monitor_port = value;
			}
		}

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x0600003C RID: 60 RVA: 0x000024B4 File Offset: 0x000006B4
		// (set) Token: 0x0600003D RID: 61 RVA: 0x000024CC File Offset: 0x000006CC
		public string IP_Range_WLAN
		{
			get
			{
				return this._ip_range_wlan;
			}
			set
			{
				this._ip_range_wlan = value;
			}
		}

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x0600003E RID: 62 RVA: 0x000024D8 File Offset: 0x000006D8
		// (set) Token: 0x0600003F RID: 63 RVA: 0x000024F0 File Offset: 0x000006F0
		public string Public_IP
		{
			get
			{
				return this._public_ip;
			}
			set
			{
				this._public_ip = value;
			}
		}

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x06000040 RID: 64 RVA: 0x000024FC File Offset: 0x000006FC
		// (set) Token: 0x06000041 RID: 65 RVA: 0x00002514 File Offset: 0x00000714
		public string Secondary_Public_IP
		{
			get
			{
				return this._secondary_public_ip;
			}
			set
			{
				this._secondary_public_ip = value;
			}
		}

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x06000042 RID: 66 RVA: 0x00002520 File Offset: 0x00000720
		// (set) Token: 0x06000043 RID: 67 RVA: 0x00002538 File Offset: 0x00000738
		public string Third_Public_IP
		{
			get
			{
				return this._third_public_ip;
			}
			set
			{
				this._third_public_ip = value;
			}
		}

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x06000044 RID: 68 RVA: 0x00002544 File Offset: 0x00000744
		// (set) Token: 0x06000045 RID: 69 RVA: 0x0000255C File Offset: 0x0000075C
		public string Fourth_Public_IP
		{
			get
			{
				return this._fourth_public_ip;
			}
			set
			{
				this._fourth_public_ip = value;
			}
		}

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x06000046 RID: 70 RVA: 0x00002568 File Offset: 0x00000768
		// (set) Token: 0x06000047 RID: 71 RVA: 0x00002580 File Offset: 0x00000780
		public string Fifth_Public_IP
		{
			get
			{
				return this._fifth_public_ip;
			}
			set
			{
				this._fifth_public_ip = value;
			}
		}

		// Token: 0x0400001F RID: 31
		private string _ip;

		// Token: 0x04000020 RID: 32
		private string _secondaryip;

		// Token: 0x04000021 RID: 33
		private string _thirdip;

		// Token: 0x04000022 RID: 34
		private string _fourthip;

		// Token: 0x04000023 RID: 35
		private string _fifthip;

		// Token: 0x04000024 RID: 36
		private int _port;

		// Token: 0x04000025 RID: 37
		private int _secondaryport;

		// Token: 0x04000026 RID: 38
		private int _thirdport;

		// Token: 0x04000027 RID: 39
		private int _fourthport;

		// Token: 0x04000028 RID: 40
		private int _fifthport;

		// Token: 0x04000029 RID: 41
		private string _portname;

		// Token: 0x0400002A RID: 42
		private string _secondaryportname;

		// Token: 0x0400002B RID: 43
		private string _thirdportname;

		// Token: 0x0400002C RID: 44
		private string _fourthportname;

		// Token: 0x0400002D RID: 45
		private string _fifthportname;

		// Token: 0x0400002E RID: 46
		private string _serverAlias;

		// Token: 0x0400002F RID: 47
		private string _version;

		// Token: 0x04000030 RID: 48
		private string _monitor_IP;

		// Token: 0x04000031 RID: 49
		private int _monitor_port;

		// Token: 0x04000032 RID: 50
		private string _ip_range_wlan;

		// Token: 0x04000033 RID: 51
		private string _public_ip;

		// Token: 0x04000034 RID: 52
		private string _secondary_public_ip;

		// Token: 0x04000035 RID: 53
		private string _third_public_ip;

		// Token: 0x04000036 RID: 54
		private string _fourth_public_ip;

		// Token: 0x04000037 RID: 55
		private string _fifth_public_ip;
	}
}
