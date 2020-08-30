using System;
using System.Collections.Generic;
using System.Text;

using Otter.Core;

namespace FOA {
    /// <summary>
    /// Responsible for loading, unloading, and playing sounds.
    /// </summary>
    public static class SoundManager {
        public static Dictionary<string, SubtitledSound> Sounds { get; } = new Dictionary<string, SubtitledSound>();

        /// <summary>
        /// Load a sound from a file and assign it a name.
        /// </summary>
        public static void LoadSound(string filePath, string name, string subtitle) {
            Sounds[name] = new SubtitledSound(new Sound(filePath), subtitle);
        }

        public class SubtitledSound {
            public Sound Sound { get; }
            public string Subtitle { get; }

            public SubtitledSound(Sound sound, string subtitle) {
                Sound = sound;
                Subtitle = subtitle;
            }
        }
    }
}
