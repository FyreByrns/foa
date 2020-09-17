using System;
using System.Collections.Generic;
using System.Text;
using FOA.Components;
using Otter.Colliders;
using Otter.Core;
using Otter.Graphics.Drawables;
using Otter.Utility;

namespace FOA.Scenes {
    class TestScene : Scene {

        public TestScene() {
            SystemicEntity player = new SystemicEntity();
            player.AddComponent<BodyHitbox>();
            player.AddComponent<PlayerController>();
            player.AddComponent<WeaponManager>();
            player.AddComponent<MovingEntityCollider>(new PolygonCollider(Polygon.CreateCircle(30), CollisionTags.Wall));
            Add(player);

            SystemicEntity dummy = new SystemicEntity();
            dummy.AddComponent<BodyHitbox>();
            Add(dummy);

            Entity wall = new Entity(70, 70);
            wall.AddGraphic(Image.CreateCircle(50));
            wall.AddCollider(new PolygonCollider(Polygon.CreateCircle(50), CollisionTags.Wall));
            Add(wall);

            Entity wall2 = new Entity(40, 40);
            wall2.AddGraphic(Image.CreateCircle(50));
            wall2.AddCollider(new PolygonCollider(Polygon.CreateCircle(50), CollisionTags.Wall));
            //Add(wall2);

            Entity wall3 = new Entity(90, 70);
            wall3.AddGraphic(Image.CreateRectangle(1000, 40));
            wall3.AddCollider(new PolygonCollider(Polygon.CreateRectangle(1000, 40), CollisionTags.Wall));
            Add(wall3);

            CameraFocus = player;
        }
    }
}
