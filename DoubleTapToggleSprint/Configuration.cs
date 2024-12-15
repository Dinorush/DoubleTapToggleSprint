using BepInEx.Configuration;
using BepInEx;
using GTFO.API.Utilities;
using System.IO;

namespace DoubleTapToggleSprint
{
    internal static class Configuration
    {
        private readonly static ConfigEntry<float> _toggleBufferTime;
        public static float ToggleBufferTime => _toggleBufferTime.Value;

        private readonly static ConfigEntry<bool> _requireForward;
        public static bool RequireForward => _requireForward.Value;

        private readonly static ConfigFile _configFile;

        static Configuration()
        {
            _configFile = new ConfigFile(Path.Combine(Paths.ConfigPath, EntryPoint.MODNAME + ".cfg"), saveOnInit: true);

            string section = "General Settings";
            string description = "Requires a forward input for sprint to be toggled. If the forward input is released, the toggle is disabled.";
            _requireForward = _configFile.Bind(section, "Require Forward Input", true, description);

            description = "Maximum allowed time between sprint inputs to toggle sprint.";
            _toggleBufferTime = _configFile.Bind(section, "Toggle Buffer Time", 0.3f, description);
        }

        internal static void Init()
        {
            LiveEditListener listener = LiveEdit.CreateListener(Paths.ConfigPath, EntryPoint.MODNAME + ".cfg", false);
            listener.FileChanged += OnFileChanged;
        }

        private static void OnFileChanged(LiveEditEventArgs _)
        {
            _configFile.Reload();
        }
    }
}
