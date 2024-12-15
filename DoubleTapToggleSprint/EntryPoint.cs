using BepInEx;
using BepInEx.Unity.IL2CPP;
using GTFO.API;
using HarmonyLib;
using Il2CppInterop.Runtime.Injection;
using UnityEngine;

namespace DoubleTapToggleSprint
{
    [BepInPlugin("Dinorush." + MODNAME, MODNAME, "1.0.0")]
    [BepInDependency("dev.gtfomodding.gtfo-api", BepInDependency.DependencyFlags.HardDependency)]
    internal sealed class EntryPoint : BasePlugin
    {
        public const string MODNAME = "DoubleTapToggleSprint";

        public override void Load()
        {
            Log.LogMessage("Loading " + MODNAME);
            new Harmony(MODNAME).PatchAll();

            Configuration.Init();
            AssetAPI.OnStartupAssetsLoaded += OnStartupAssetsLoaded;

            Log.LogMessage("Loaded " + MODNAME);
        }

        private void OnStartupAssetsLoaded()
        {
            ClassInjector.RegisterTypeInIl2Cpp<SprintCheckHandler>();
            var go = new GameObject(MODNAME);
            Object.DontDestroyOnLoad(go);
            go.AddComponent<SprintCheckHandler>();
        }
    }
}