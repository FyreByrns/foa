using System;
using System.Collections.Generic;
using System.Text;
using FOA.Components;
using Otter.Core;

namespace FOA.Scenes {
    class TestScene : Scene {

        public TestScene() {
            SystemicEntity player = new SystemicEntity();
            Add(player);
            player.AddComponent(Body.MakeHumanoid(fingers: 5, toes: 5, partProfile: MaterialProfile.FleshWithBones));
            player.AddComponent<PlayerController>();
            player.GetComponent<Body>().AddAllPartsToScene();
            player.GetComponent<Body>().GetPart("Head").Debug = true;

            CameraFocus = player;
        }
    }
}
