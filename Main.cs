using DisableCSO.Utility;
using HarmonyLib;
using Kingmaker;
using Kingmaker.Blueprints;
using Kingmaker.View;
using System.Reflection;
using UnityModManagerNet;
using UnityEngine;
using DisableCSO.CSO;
using UnityEngine.UIElements;

namespace DisableCSO
{
#if (DEBUG)
    [EnableReloading]
#endif
    internal static class Main
    {
        public static UnityModManager.ModEntry ModEntry { get; private set; }
        public static Settings Settings;
        public static Utility.Logger Logger;
        public static ModEventHandler ModEventHandler;

        private static bool Load(UnityModManager.ModEntry modEntry)
        {
            ModEntry = modEntry;
            Settings = UnityModManager.ModSettings.Load<Settings>(modEntry);
            Logger = new(modEntry.Logger);
            ModEventHandler = new();

            modEntry.OnToggle = OnToggle;
            modEntry.OnSaveGUI = OnSaveGUI;
#if (DEBUG)
            modEntry.OnGUI = OnGUIDebug;
            modEntry.OnUnload = Unload;
            return true;
        }

        private static bool Unload(UnityModManager.ModEntry modEntry)
        {
            ModEventHandler.Disable(modEntry, true);
            return true;
        }

#else
            modEntry.OnGUI = OnGUI;
            return true;
        }
#endif

        private static bool OnToggle(UnityModManager.ModEntry modEntry, bool value)
        {
            if (value)
            {
                Assembly assembly = Assembly.GetExecutingAssembly();
                ModEventHandler.Enable(modEntry, assembly);
            }
            else
            {
                ModEventHandler.Disable(modEntry);
            }

            return true;
        }

        private static void OnSaveGUI(UnityModManager.ModEntry modEntry)
        {
            Settings.Save(modEntry);
        }

        private static void OnGUI(UnityModManager.ModEntry modEntry)
        {
            Settings.LetterBox = GUILayout.Toggle(Settings.LetterBox, "Show Letter Box", GUILayout.ExpandWidth(false));
        }

        private static void OnGUIDebug(UnityModManager.ModEntry modEntry)
        {
            if (GUILayout.Button("Test"))
                CSOController.SetCSO();

            OnGUI(modEntry);
        }
    }
}
