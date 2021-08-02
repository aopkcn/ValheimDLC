using BepInEx;
using HarmonyLib;

namespace ValheimDLC
{
    [BepInPlugin("AOPK.QIYE.ValheimDLC", "英灵神殿DLC开启", "0.1")]
    public class ValheimDLCopen : BaseUnityPlugin
    {
        [HarmonyPostfix]
        public static void Override(ref bool __result)
        {
            __result = true;
            return;
        }

        [HarmonyPostfix]
        public static void ReplaceSteam(ref DLCMan __instance)
        {
            foreach (DLCMan.DLCInfo dlc in __instance.m_dlcs)
            {
                dlc.m_installed = true;
            }
            return;
        }

        public void Awake()
        {
            System.Type[] uint_ = { typeof(uint) };
            System.Type[] string_ = { typeof(string) };
            System.Type[] DLCInfo_ = { typeof(DLCMan.DLCInfo) };

            Harmony harmony = new Harmony("AOPK.QIYE.ValheimDLC");
            harmony.Patch(AccessTools.Method(typeof(DLCMan), "IsDLCInstalled", uint_), null, new HarmonyMethod(typeof(ValheimDLCopen), "Override"));
            harmony.Patch(AccessTools.Method(typeof(DLCMan), "IsDLCInstalled", string_), null, new HarmonyMethod(typeof(ValheimDLCopen), "Override"));
            harmony.Patch(AccessTools.Method(typeof(DLCMan), "IsDLCInstalled", DLCInfo_), null, new HarmonyMethod(typeof(ValheimDLCopen), "Override"));
            harmony.Patch(AccessTools.Method(typeof(DLCMan), "CheckDLCsSTEAM"), null, new HarmonyMethod(typeof(ValheimDLCopen), "ReplaceSteam"));
        }
    }
}
