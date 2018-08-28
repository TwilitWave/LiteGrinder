using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Diagnostics;
using tainicom.Aether.Physics2D.Dynamics;

namespace PhysicsTesting
{
    public class Player
    {
        private float jumpForce;
        private Vector2 startPos;

        private World world;
        private Body body;
        private Fixture bodySensor;
        private Vector2 circleOrigin;
        private bool isGrounded = false;
        private Texture2D sprite;
        public double jumpCD = 0;

        public Player(World world, Vector2 startPos, Texture2D sprite, float jumpForce)
        {
            this.jumpForce = jumpForce;
            this.startPos = startPos;
            this.world = world;
            this.sprite = sprite;

            circleOrigin = new Vector2(sprite.Width / 2f, sprite.Height / 2f);
            Vector2 circlePos = startPos;

            body = world.CreateCircle(ConvertUnits.ToSimUnits(30), 10f, circlePos, BodyType.Dynamic);
            body.SetRestitution(0.0f);
            body.SetFriction(1f);

            bodySensor = body.CreateCircle(ConvertUnits.ToSimUnits(36), 2f);
            bodySensor.IsSensor = true;

            bodySensor.OnCollision += (fixtureA, fixtureB, contact) =>
            {
                isGrounded = true;
                return true;
            };
        }

        // Reset position
        public void ResetPosition()
        {
            body.Position = startPos;
            body.LinearVelocity = new Vector2(0, -1f);
            body.AngularVelocity = 0;
        }
        
        // Get Body
        public Body GetBody()
        {
            return body;
        }

        // Jump command
        public void Jump()
        {
            if (isGrounded && (jumpCD >= 500))
            {
                jumpCD = 0;
                isGrounded = false;
                body.ApplyLinearImpulse(new Vector2(0, jumpForce));
            }
        }

        // Jump command
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(sprite, ConvertUnits.ToDisplayUnits(body.Position), null, Color.Green, body.Rotation, circleOrigin, new Vector2(20, 20), SpriteEffects.None, 0f);
        }
    }
}
