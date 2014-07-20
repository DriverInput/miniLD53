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
    class players
    {
        public Texture2D texture;
        public Rectangle rectangle;
        public Vector2 velocity;
        public sbyte ctr;
        public sbyte speed = 8;

        public players(Texture2D newTexture, Rectangle newRectangle, sbyte newCtr, int rectX, int rectY)
        {
            texture = newTexture;
            rectangle = newRectangle;
            rectangle.X = rectX;
            rectangle.Y = rectY;
            ctr = newCtr;
        }

        public void update()
        {
            movement();
        }

        public void draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, rectangle, Color.White);
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
