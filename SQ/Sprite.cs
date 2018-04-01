using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SQ
{

        public class Sprite
        {
            #region Variables
            public Texture2D SpriteTexture;
            public Rectangle SpritePOS;
            public Rectangle SourceRectangle;
            public double ElapsedTime = 0;
            public int AmountOfFramesOnEachRow, AmountOfRows;
            public double TimeBetweenFrames;
            public int CurrentFrame = 1;
            public int CurrentRow = 1;
            #endregion

            #region BaseFunctions
            public virtual void Draw(ref SpriteBatch spriteBatch) { }
            public virtual void Update(ref GameTime gameTime) { }
            public virtual void Animation(ref GameTime gameTime) { }
            #endregion
        }
        public class BasicSprite : Sprite
        {
            #region BasicSpriteSpecificFunctions
            //constructor
            public BasicSprite(Texture2D SpriteTexture, Rectangle SpritePOS, Rectangle SourceRect, int AmountOfFrames, double TimeBetweenFrames, int AmountOfRows)
            {
                base.SpriteTexture = SpriteTexture;
                base.SpritePOS = SpritePOS;
                SourceRectangle = SourceRect;
                AmountOfFramesOnEachRow = AmountOfFrames + 1;
                base.TimeBetweenFrames = TimeBetweenFrames;
                base.AmountOfRows = AmountOfRows;


            }
            #endregion

            #region BaseFunctions
            public override void Animation(ref GameTime gameTime)
            {
                base.Animation(ref gameTime);
                ElapsedTime += gameTime.ElapsedGameTime.TotalMilliseconds;

                if (ElapsedTime >= TimeBetweenFrames)
                {
                    ElapsedTime -= TimeBetweenFrames;

                    SourceRectangle.X = SourceRectangle.Width * CurrentFrame;
                    CurrentFrame += 1;
                    if (CurrentRow < AmountOfRows && CurrentFrame == AmountOfFramesOnEachRow)
                    {

                        SourceRectangle.Y = SourceRectangle.Height * CurrentRow;
                        SourceRectangle.X = 0;
                        CurrentRow += 1;
                        if (CurrentRow >= AmountOfRows)
                        {
                            CurrentRow = 1;
                        }
                    }
                    else if (CurrentFrame >= AmountOfFramesOnEachRow)
                    {
                        SourceRectangle.X = 0;
                        CurrentFrame = 1;
                    }
                }


            }

            public override void Update(ref GameTime gameTime)
            {
                base.Update(ref gameTime);
                Animation(ref gameTime);
            }

            public override void Draw(ref SpriteBatch spriteBatch)
            {
                base.Draw(ref spriteBatch);
                spriteBatch.Draw(SpriteTexture, SpritePOS, SourceRectangle, Color.White);
            }
            #endregion
        }
    }

