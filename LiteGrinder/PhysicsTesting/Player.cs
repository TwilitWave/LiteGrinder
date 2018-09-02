using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Diagnostics;
using tainicom.Aether.Physics2D.Dynamics;

namespace LiteGrinder
{
    public class Player
    {
        private float jumpForce;
        private Vector2 startPos, endPos;

        private World world;
        private Body body;
        private Body endGoal;
        private Fixture bodySensor;
        private Vector2 circleOrigin, ballOrigin;
        private bool isGrounded = false;
        private Texture2D sprite1, sprite2 , startSprite, endSprite, ballSprite;
        public double jumpCD = 0;
        
        // Amount of time between frames
        private TimeSpan frameInterval = new TimeSpan(0, 0, 0, 0, 30);
        // Time passed since last frame
        private TimeSpan nextFrame;
        private int spriteNum = 0;


        public Player(World world, Vector2 startPos, Texture2D sprite1, Texture2D sprite2, Texture2D ballSprite, Texture2D startSprite, Texture2D endSprite, float jumpForce)
        {
            this.jumpForce = jumpForce;
            this.startPos = startPos;
            this.world = world;
            this.sprite1 = sprite1;
            this.sprite2 = sprite2;
            this.startSprite = startSprite;
            this.endSprite = endSprite;
            this.ballSprite = ballSprite;

            circleOrigin = new Vector2(sprite1.Width / 2f, sprite1.Height / 2f);
            ballOrigin = new Vector2(ballSprite.Width / 2f, ballSprite.Height / 2f);
            Vector2 circlePos = startPos;

            body = world.CreateCircle(ConvertUnits.ToSimUnits(30), 10f, circlePos, BodyType.Dynamic);
            body.SetRestitution(0.0f);
            body.SetFriction(1f);
            body.Tag = this;

            bodySensor = body.CreateCircle(ConvertUnits.ToSimUnits(36), 2f);
            bodySensor.IsSensor = true;
            bodySensor.OnCollision += (fixtureA, fixtureB, contact) =>
            {
                if(!fixtureB.IsSensor)
                    isGrounded = true;
                return true;
            };


            endGoal = world.CreateCircle(ConvertUnits.ToSimUnits(30), 10f, new Vector2(10000,10000));
            endGoal.SetIsSensor(true);
            Fixture goalSensor = endGoal.CreateCircle(ConvertUnits.ToSimUnits(36), 2f);
            goalSensor.IsSensor = true;
            goalSensor.OnCollision += (fixtureA, fixtureB, contact) =>
            {
                if (fixtureB.Body.Tag is Player)
                {
                    HitEndGoal();
                }
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

        // Called when player hits the end goal
        private void HitEndGoal()
        {
            Debug.Print("GOAOOOAL");
        }

        // Set start position
        public void SetStartPosition(Vector2 pos)
        {
            startPos = pos;
            body.Position = pos;
            ResetPosition();
        }

        // Set Goal position
        public void SetGoalPosition(Vector2 pos)
        {
            endPos = pos;
            endGoal.Position = pos;
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
            spriteBatch.Draw(ballSprite, ConvertUnits.ToDisplayUnits(body.Position), null, Color.White, body.Rotation, ballOrigin, new Vector2(1f / 2f, 1f / 2f), SpriteEffects.None, 0f);
            spriteBatch.Draw(startSprite, ConvertUnits.ToDisplayUnits(startPos), null, Color.White, 0, new Vector2(startSprite.Width / 2, startSprite.Height / 2), new Vector2(1f / 1.5f, 1f / 1.5f), SpriteEffects.None, 0f);
            spriteBatch.Draw(endSprite, ConvertUnits.ToDisplayUnits(endPos), null, Color.White, 0, new Vector2(endSprite.Width / 2, endSprite.Height / 2), new Vector2(1f / 1.5f, 1f / 1.5f), SpriteEffects.None, 0f);
        }
    }
}
