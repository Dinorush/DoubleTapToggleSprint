using HarmonyLib;
using Player;

namespace DoubleTapToggleSprint.SprintPatches
{
    [HarmonyPatch]
    internal static class SprintPatch
    {
        // Wish I could avoid patching an update function but I don't have a choice without serious jank
        [HarmonyPatch(typeof(PlayerLocomotion), nameof(PlayerLocomotion.RunInput))]
        [HarmonyPrefix]
        private static bool AllowToggle(ref bool __result)
        {
            if (SprintCheckHandler.ToggleSprint)
            {
                __result = true;
                return false;
            }
            return true;
        }

        [HarmonyPatch(typeof(PLOC_Crouch), nameof(PLOC_Crouch.Enter))]
        [HarmonyPostfix]
        private static void StopSprinting()
        {
            SprintCheckHandler.StopSprint();
        }
    }
}
