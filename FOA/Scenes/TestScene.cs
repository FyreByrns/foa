using System;
using System.Collections.Generic;
using System.Text;
using FOA.Components;
using Otter.Core;

namespace FOA.Scenes {
    class TestScene : Scene {

        public TestScene() {
            SystemicEntity player = new SystemicEntity();
            player.AddComponent(Body.MakeHumanoid(fingers: 5, toes: 5, partProfile: MaterialProfile.FleshWithBones));

            player.AddGraphic(Otter.Graphics.Drawables.Image.CreateRectangle(40, 40, Otter.Graphics.Color.White));
            player.Graphic.CenterOrigin();
            player.AddComponent<PlayerController>();
            Add(player);
        }
    }
}
