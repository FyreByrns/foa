using System;

using Otter.Core;

namespace FOA {
    class Program {
        static void Main() {
            Game game = new Game("Forest of Ages", 1400, 800) {
                MouseVisible = true
            };

            game.Start(new Scenes.TestScene());
            game.Dispose();
        }
    }
}
