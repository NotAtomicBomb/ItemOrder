using System;
using BepInEx;
using BepInEx.Configuration;
using UnityEngine;

namespace ItemOrder
{
	[BepInPlugin("com.atomic.ItemOrderport", "Item Order Port", "0.2.0")]
	public class First_Mod_Test : BaseUnityPlugin
	{
		private static ConfigWrapper<bool> NotificationsOn { get; set; }

		private static ConfigWrapper<int> DelayTimer { get; set; }

		public static bool NotisOn
		{
			get
			{
				return First_Mod_Test.NotificationsOn.Value;
			}
			protected set
			{
				First_Mod_Test.NotificationsOn.Value = value;
			}
		}
		public static int Delaytime
		{
			get
			{
				return First_Mod_Test.DelayTimer.Value;
			}
			protected set
			{
				First_Mod_Test.DelayTimer.Value = value;
			}
		}

		public static GameObject RootObject { get; set; }
		public static ItemOrder modHandler { get; set; }

		public void Awake()
		{
			First_Mod_Test.NotificationsOn = base.Config.Wrap<bool>("Game", "Notifications", "Weather or not notifications are on.", true);
			First_Mod_Test.DelayTimer = base.Config.Wrap<int>("Game", "Delay Time", "Delay between when you can sequence in seconds.", 60);
			Debug.Log("Item Order Started!");
			First_Mod_Test.RootObject = new GameObject("ModHandler");
			Object.DontDestroyOnLoad(First_Mod_Test.RootObject);
			First_Mod_Test.modHandler = First_Mod_Test.RootObject.AddComponent<ItemOrder>();
		}
	}
}
