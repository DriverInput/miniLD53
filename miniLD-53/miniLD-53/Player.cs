using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace miniLD_53
{
    class Player
    {
        public Texture2D texture;
        public Rectangle rectangle;
        public Vector2 velocity;
        public Vector2 position = new Vector2(128, 32);
        public sbyte ctr = 1;
        public sbyte speed = 5;

        private bool hasJumped = false;

        //public Player(Texture2D newTexture, Rectangle newRectangle, sbyte newCtr, int rectX, int rectY)
        //{
          //  texture = newTexture;
            //rectangle = newRectangle;
            //rectangle.X = rectX;
            //rectangle.Y = rectY;
            //ctr = newCtr;
        //}

        public Player() { }

        public void Update()
        {
            position += velocity;
            rectangle = new Rectangle((int)position.X, (int)position.Y, (int)32, 64);

            movement();
        }

        public void Load(ContentManager Content)
        {
            texture = Content.Load<Texture2D>("Player");
        }

        public void draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, rectangle, Color.White);
        }

        public void Collision(Rectangle newRectangle, int xOffset, int yOffset)
        {
            if (rectangle.TouchTopOf(newRectangle))
            {
                rectangle.Y = newRectangle.Y - rectangle.Height;
                velocity.Y = 0f;
                hasJumped = false;
            }

            if (rectangle.TouchLeftOf(newRectangle))
            {
                position.X = newRectangle.X - rectangle.Width - 2;
            }
            if (rectangle.TouchRightOf(newRectangle))
            {
                position.X = newRectangle.X + newRectangle.Width + 2;
            }
            if (rectangle.TouchBottomOf(newRectangle))
                velocity.Y = 1f;

            if (position.X < 0) position.X = 0;
            if (position.X > xOffset - rectangle.Width) position.X = xOffset - rectangle.Width;
            if (position.Y < 0) velocity.Y = 1f;
            if (position.Y > yOffset - rectangle.Height) position.Y = yOffset - rectangle.Height;
        }

        public void movement()
        {
            switch (ctr)
            {
                case 1:
                    if (Keyboard.GetState().IsKeyDown(Keys.Left))
                    {
                        velocity.X = -speed;
                    }
                    else if (Keyboard.GetState().IsKeyDown(Keys.Right))
                    {
                        velocity.X = speed;
                    }
                    else { velocity.X = 0; }

                    if (Keyboard.GetState().IsKeyDown(Keys.Up) && hasJumped == false)
                    {
                        position.Y -= 5f;
                        velocity.Y = -10f;
                        hasJumped = true;
                    }

                    if (velocity.Y < 10)
                        velocity.Y += 0.4f;
                break;

                case 2:
                    if (Keyboard.GetState().IsKeyDown(Keys.A))
                    {
                        velocity.X = -speed;
                    }
                    else if (Keyboard.GetState().IsKeyDown(Keys.D))
                    {
                        velocity.X = speed;
                    }
                    else { velocity.X = 0; }
                break;
            }
        }
        
    }
}
