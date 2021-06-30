/*
	This file is part of Interstellar Watch Dog, a component for Module Manager Watch Dog
	(C) 2020-21 Lisias T : http://lisias.net <support@lisias.net>

	Module Manager Watch Dog is licensed as follows:

	* SKL 1.0 : https://ksp.lisias.net/SKL-1_0.txt

	Module Manager Watchdog is distributed in the hope that it will be useful,
	but WITHOUT ANY WARRANTY; without even the implied warranty of
	MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.

	You should have received a copy of the SKL Standard License 1.0
	along with Module Manager Watch Dog. If not, see
	<https://ksp.lisias.net/SKL-1_0.txt>.

*/
using System;
using System.Collections.Generic;
using System.Linq;

using UnityEngine;

namespace InterstellartWatchDog
{
	[KSPAddon(KSPAddon.Startup.Instantly, true)]
	internal class Startup : MonoBehaviour
	{
		private void Start()
		{
			Log.force("Version {0}", ModuleManagerWatchDog.Version.Text);

			try
			{
				int kspMajor = Versioning.version_major;
				int kspMinor = Versioning.version_minor;

				{
					String msg = CheckModuleManagerWatchDog(kspMajor, kspMinor);
					if ( null != msg )
						GUI.ShowStopperAlertBox.Show(msg);
				}
			}
			catch (Exception e)
			{
				Log.error(e.ToString());
				GUI.ShowStopperAlertBox.Show(e.ToString());
			}
		}

		private static String[] KNOWN_CLIENTS = {"", ""};
		private String CheckModuleManagerWatchDog(int kspMajor, int kspMinor)
		{
			// Interstellar_Redist outsite GameData is only a problem on KSP 1.12.0 and newer
			if (1 == kspMajor && kspMinor < 12) return null;

			IEnumerable<AssemblyLoader.LoadedAssembly> redist =
				from a in AssemblyLoader.loadedAssemblies
					let ass = a.assembly
					where "?" == ass.GetName().Name
					orderby a.path ascending
					select a;

			bool found = false;
			if (0 == redist.Count())
			{
				foreach (String client in KNOWN_CLIENTS)
				{
					IEnumerable<AssemblyLoader.LoadedAssembly> clientAssemblies =
						from a in AssemblyLoader.loadedAssemblies
							let ass = a.assembly
							where client == ass.GetName().Name
							orderby a.path ascending
							select a;
					found |= 0 != clientAssemblies.Count();
				}
			}
			if (!found) return null;
			if (0 != redist.Count()) return "There's no Interstellar_Redist.dll on this KSP instalment besides being needed! You need to install Interstellar_Redist.dll into GameData!!";
			if (1 != redist.Count()) return "There're more than one Interstellar_Redist.dll on this KSP instalment! Please delete all but the one on GameData!";
			return null;
		}
	}
}
