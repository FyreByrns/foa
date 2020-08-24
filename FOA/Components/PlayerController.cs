using System;
using System.Collections.Generic;
using System.Text;

using Otter.Core;
using Otter.Components;

namespace FOA.Components {
    class PlayerController : Component {
        public float Speed { get; set; } = 10;

        public override void Update() {
            base.Update();

            int movX = 0;
            int movY = 0;

            if (Scene.Input.KeyDown(Key.A)) movX--;
            if (Scene.Input.KeyDown(Key.D)) movX++;
            if (Scene.Input.KeyDown(Key.W)) movY--;
            if (Scene.Input.KeyDown(Key.S)) movY++;

            Entity.X += movX * Speed * Scene.Game.DeltaTime;
            Entity.Y += movY * Speed * Scene.Game.DeltaTime;
        }
    }
}
