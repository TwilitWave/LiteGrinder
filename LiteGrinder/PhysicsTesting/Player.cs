using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
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
        private Texture2D sprite1;
        private Texture2D sprite2;
        public double jumpCD = 0;
        
        // Amount of time between frames
        private TimeSpan frameInterval = new TimeSpan(0, 0, 0, 0, 30);
        // Time passed since last frame
        private TimeSpan nextFrame;
        private int spriteNum = 0;


        public Player(World world, Vector2 startPos, Texture2D sprite1, Texture2D sprite2, float jumpForce)
        {
            this.jumpForce = jumpForce;
            this.startPos = startPos;
            this.world = world;
            this.sprite1 = sprite1;
            this.sprite2 = sprite2;

            circleOrigin = new Vector2(sprite1.Width / 2f, sprite1.Height / 2f);
            Vector2 circlePos = startPos;

            body = world.CreateCircle(ConvertUnits.ToSimUnits(30), 10f, circlePos, BodyType.Dynamic);
            body.SetRestitution(0.0f);
            body.SetFriction(1f);

            bodySensor = body.CreateCircle(ConvertUnits.ToSimUnits(36), 2f);
            bodySensor.IsSensor = true;
            bodySensor.OnCollision += (fixtureA, fixtureB, contact) =>
            {
                if(!fixtureB.IsSensor)
                    isGrounded = true;
                return true;
            };
        }

        // Reset position
        public void ResetPosition()
        {
            body.Position = startPos;
            body.Rotation = 0;
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

        // Draw Player
        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            Texture2D tex = sprite1;

            // Check is it is a time to progress to the next frame
            if (nextFrame >= frameInterval)
            {
                nextFrame = TimeSpan.Zero;
                spriteNum++;
            }
            else
            {
                nextFrame += gameTime.ElapsedGameTime;
            }

            if(spriteNum % 2 == 0)
            {
                tex = sprite2;
            }

            spriteBatch.Draw(tex, ConvertUnits.ToDisplayUnits(body.Position), null, Color.White, body.Rotation, circleOrigin, new Vector2(1f/18f, 1f/18f), SpriteEffects.None, 0f);
        }
    }
}
