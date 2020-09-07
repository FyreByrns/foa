using System;
using System.Collections.Generic;
using System.Text;
using FOA.Components;
using Otter.Core;

namespace FOA.Scenes {
    class TestScene : Scene {

        public TestScene() {
            SystemicEntity player = new SystemicEntity();
            player.AddComponent<BodyHitbox>();
            player.AddComponent<PlayerController>();
            player.AddComponent<WeaponManager>();
            Add(player);

            SystemicEntity dummy = new SystemicEntity();
            dummy.AddComponent<BodyHitbox>();
            Add(dummy);

            CameraFocus = player;
        }
    }
}
