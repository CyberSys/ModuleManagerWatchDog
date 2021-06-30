/*
	This file is part of Watch Dog Watch Dog, a component for Module Manager Watch Dog
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

namespace WatchDogWatchDog
{
	/**
	 *  Who watch the watchers? :)
	 */
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
					String msg = CheckWatchDogWatchDog();
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

		private String CheckWatchDogWatchDog()
		{
			IEnumerable<AssemblyLoader.LoadedAssembly> loaded =
				from a in AssemblyLoader.loadedAssemblies
					let ass = a.assembly
					where "ModuleManagerWatchDog" == ass.GetName().Name
					orderby a.path ascending
					select a;

			if (0 == loaded.Count()) return "There's no 666_ModuleManagerWatchDog.dll on this KSP instalment! You need to install 666_ModuleManagerWatchDog.dll into GameData!!";
			if (1 != loaded.Count()) return "There're more than one 666_ModuleManagerWatchDog.dll on this KSP instalment! Please delete all but the one on GameData!";
			return null;
		}
	}
}
