// 80% unc script
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace Student
{
	// Token: 0x02000002 RID: 2
	public static class Inj100
	{
		// Token: 0x06000001 RID: 1
		[DllImport("bin\\shareFacebook.dll", CallingConvention = CallingConvention.Cdecl)]
		private static extern void Initialize();

		// Token: 0x06000002 RID: 2
		[DllImport("bin\\shareFacebook.dll", CallingConvention = CallingConvention.Cdecl)]
		private static extern void Attach();

		// Token: 0x06000003 RID: 3
		[DllImport("bin\\shareFacebook.dll", CallingConvention = CallingConvention.Cdecl)]
		private static extern IntPtr GetClients();

		// Token: 0x06000004 RID: 4
		[DllImport("bin\\shareFacebook.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
		private static extern void Execute(byte[] scriptSource, string[] clientUsers, int numUsers);

		// Token: 0x06000005 RID: 5 RVA: 0x00002048 File Offset: 0x00000248
		public static void Inj101()
		{
			Inj100.Initialize();
			Inj100.Attach();
		}

		// Token: 0x06000006 RID: 6 RVA: 0x00002058 File Offset: 0x00000258
		public static void Inj102(string scriptSource)
		{
			string s = string.Format("getgenv().getexecutorname = function() return \"{0}\" end getgenv().identifyexecutor = function() return \"{1} {2}\" end getgenv().printidentity = function(arg) if arg == nil then print(\"Current identity is {3}\") else print(arg..\" {4}\") end end getgenv().getidentity = function() return \"{5}\" end", new object[]
			{
				Inj100.ApiName,
				Inj100.ApiName,
				Inj100.ApiVersion,
				Inj100.ApiIdentity,
				Inj100.ApiIdentity,
				Inj100.ApiIdentity
			});
			string[] array = (from c in Inj100.GetClientsList()
			select c.name).ToArray<string>();
			Inj100.Execute(Encoding.UTF8.GetBytes(s), array, array.Length);
			Inj100.Execute(Encoding.UTF8.GetBytes(scriptSource), array, array.Length);
		}

		// Token: 0x06000007 RID: 7 RVA: 0x00002110 File Offset: 0x00000310
		public static bool IsRobloxOpen()
		{
			return Process.GetProcessesByName("RobloxPlayerBeta").Any<Process>();
		}

		// Token: 0x06000008 RID: 8 RVA: 0x00002134 File Offset: 0x00000334
		public static void KillRoblox()
		{
			bool flag = Inj100.IsRobloxOpen();
			bool flag2 = flag;
			if (flag2)
			{
				foreach (Process process in Process.GetProcessesByName("RobloxPlayerBeta"))
				{
					process.Kill();
				}
			}
		}

		// Token: 0x06000009 RID: 9 RVA: 0x00002178 File Offset: 0x00000378
		public static void SetConfig(string name, string version, int identity = 3)
		{
			Inj100.ApiName = name;
			Inj100.ApiVersion = version;
			Inj100.ApiIdentity = ((identity >= 3 && identity <= 8) ? identity : 3);
		}

		// Token: 0x0600000A RID: 10 RVA: 0x00002198 File Offset: 0x00000398
		public static List<Inj100.ClientInfo> GetClientsList()
		{
			List<Inj100.ClientInfo> list = new List<Inj100.ClientInfo>();
			IntPtr intPtr = Inj100.GetClients();
			for (;;)
			{
				Inj100.ClientInfo clientInfo = Marshal.PtrToStructure<Inj100.ClientInfo>(intPtr);
				bool flag = clientInfo.name == null;
				bool flag2 = flag;
				if (flag2)
				{
					break;
				}
				list.Add(clientInfo);
				intPtr += Marshal.SizeOf<Inj100.ClientInfo>();
			}
			return list;
		}

		// Token: 0x04000001 RID: 1
		private static string ApiName = "XenoSource";

		// Token: 0x04000002 RID: 2
		private static string ApiVersion = "1.0.0";

		// Token: 0x04000003 RID: 3
		private static int ApiIdentity = 3;

		// Token: 0x02000003 RID: 3
		public struct ClientInfo
		{
			// Token: 0x04000004 RID: 4
			public string version;

			// Token: 0x04000005 RID: 5
			public string name;

			// Token: 0x04000006 RID: 6
			public int id;
		}
	}
}
