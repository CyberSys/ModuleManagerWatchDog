﻿/*
	This file is part of Watch Dog for Intestellar Redist, a component from Module Manager Watch Dog
		©2020-2023 Lisias T : http://lisias.net <support@lisias.net>

	Module Manager Watch Dog is licensed as follows:
		* SKL 1.0 : https://ksp.lisias.net/SKL-1_0.txt

	Module Manager Watchdog is distributed in the hope that it will be useful,
	but WITHOUT ANY WARRANTY; without even the implied warranty of
	MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.

	You should have received a copy of the SKL Standard License 1.0
	along with Module Manager Watch Dog. If not, see
	<https://ksp.lisias.net/SKL-1_0.txt>.

*/
using System.Diagnostics;

namespace WatchDogForInterstellarRedist
{
	public static class Log
	{
		internal static void force(string msg, params object[] @params)
		{
			UnityEngine.Debug.LogFormat("[WatchDogForInterstellarRedist] " + msg, @params);
		}

		internal static void info(string msg, params object[] @params)
		{
			UnityEngine.Debug.LogFormat("[WatchDogForInterstellarRedist] INFO: " + msg, @params);
		}

		internal static void detail(string msg, params object[] @params)
		{
			UnityEngine.Debug.LogFormat("[WatchDogForInterstellarRedist] DETAIL: " + msg, @params);
		}

		internal static void error(string msg, params object[] @params)
		{
			UnityEngine.Debug.LogErrorFormat("[WatchDogForInterstellarRedist] ERROR: " + msg, @params);
		}

		[ConditionalAttribute("DEBUG")]
		internal static void dbg(string msg, params object[] @params)
		{
			UnityEngine.Debug.LogFormat("[WatchDogForInterstellarRedist] DEBUG: " + msg, @params);
		}
	}
}
