using System;
using System.Collections.Generic;
using System.Text;

using Otter.Core;
using Otter.Components;
using Otter.Utility;

namespace FOA.Components {
    class PlayerController : Component {
        public float Speed { get; set; } = 5;

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

            // Rotate the body hitbox
            GetComponent<BodyHitbox>().LeftSide.Rotation = Util.Angle(Entity.X, Entity.Y, Scene.MouseX, Scene.MouseY);
            GetComponent<BodyHitbox>().RightSide.Rotation = Util.Angle(Entity.X, Entity.Y, Scene.MouseX, Scene.MouseY);

            // Set the target of the weapon manager
            GetComponent<WeaponManager>().TargetX = Scene.MouseX;
            GetComponent<WeaponManager>().TargetY = Scene.MouseY;

            // Attack
            if (Scene.Input.MouseButtonDown(MouseButton.Left))
                GetComponent<WeaponManager>().Attack();
        }
    }
}
