using System;
using System.Collections.Generic;
using System.Text;

using Otter.Core;

namespace FOA.Scenes {
    class TestScene : Scene {

        public TestScene() {
            Entity player = new Entity();
            player.AddGraphic(Otter.Graphics.Drawables.Image.CreateRectangle(40, 40, Otter.Graphics.Color.White));
            player.Graphic.CenterOrigin();
            player.AddComponent<Components.PlayerController>();
            Add(player);
        }
    }
}
