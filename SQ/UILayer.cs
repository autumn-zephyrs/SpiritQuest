using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace SQ
{

    public class UILayer
    {
        List<UIpanel> UIpanels = new List<UIpanel>();

        public void AddPanel(UIpanel p)
        {
            UIpanels.Add(p);
        }
        public void RemovePanel(int index)
        {
            UIpanels.RemoveAt(index);
        }
        public void RemoveAllPanels()
        {
            UIpanels.Clear();
        }
        
        public void AddTextToPanel(int index, string text, Vector2 relativePosition, Color colour)
        {
            UIpanels[index].ScreenTexts.Add(new ScreenText(text, relativePosition, colour));
        }

        public void Draw(ref SpriteBatch spriteBatch)
        {
            foreach(UIpanel p in UIpanels)
            {
                p.Draw(ref spriteBatch);
            }
        }
        public void Update(ref GameTime gameTime, ref Camera cam)
        {
            foreach(UIpanel p in UIpanels)
            {
                p.Update(ref gameTime, ref cam);
            }
        }




    }

    public class UIelements
    {
        public SpriteFont font;
        public BasicSprite UiItem;
        public bool active;
        public Vector2 UIoffset;

        public string Name;
        public int Amount;

        public virtual void Update(GameTime gameTime, Camera cam)
        {
            UiItem.SpritePOS.X = (int)cam.Position.X + (int)UIoffset.X;
            UiItem.SpritePOS.Y = (int)cam.Position.Y + (int)UIoffset.Y;
        }

        public virtual void Update(GameTime gameTime, Camera cam, MouseCursor mouse, MouseState previousMouseState)
        {
            UiItem.SpritePOS.X = (int)cam.Position.X + (int)UIoffset.X;
            UiItem.SpritePOS.Y = (int)cam.Position.Y + (int)UIoffset.Y;
        }
        public virtual void Draw(SpriteBatch spriteBatch)
        {
            if (active == true)
            {
                UiItem.Draw(ref spriteBatch);
            }
        }

    }

    public class ScreenText
    {
        public String Text;
        public Vector2 RelativePosition;
        public Color Colour;
        public bool Centered = true;

        private Vector2 ActualPosition = Vector2.Zero;
        public ScreenText(String Text)
        {
            this.Text = Text;
            RelativePosition = Vector2.Zero;
            Colour = Color.Black;
            
        }

        public ScreenText(String Text, Vector2 RelativePosition)
        {
            this.Text = Text;
            this.RelativePosition = RelativePosition;
            Colour = Color.Black;

        }

        public ScreenText(String Text, Vector2 RelativePosition, Color Colour)
        {
            this.Text = Text;
            this.RelativePosition = RelativePosition;
            this.Colour = Colour;

        }

        public void Update(ref GameTime gameTime, ref Camera cam)
        {
            ActualPosition = new Vector2(cam.Position.X + RelativePosition.X, cam.Position.Y + RelativePosition.Y);
        }
        public void Update(ref GameTime gameTime, ref Camera cam, ref Vector2 Offset)
        {
            //offset for when the text is in on other object
            ActualPosition = new Vector2(cam.Position.X + Offset.X + RelativePosition.X, cam.Position.Y + Offset.Y + RelativePosition.Y);
        }

        public void Draw(ref SpriteBatch spriteBatch, ref SpriteFont font)
        {
            if (Centered)
            {
                spriteBatch.DrawString(font, Text, new Vector2(ActualPosition.X - (font.MeasureString(Text).X / 2), ActualPosition.Y - (font.MeasureString(Text).Y / 2) ), Colour);
            }
            else
            {
                spriteBatch.DrawString(font, Text, new Vector2(ActualPosition.X , ActualPosition.Y - (font.MeasureString(Text).Y / 2)), Colour);
            }
        }   
    }

    public class UIpanel
    {
        public List<ScreenText> ScreenTexts = new List<ScreenText>();
        private SpriteFont font;
        int BorderSize;
        Texture2D texture;

        //relative to camera
        public Vector2 RelativePosition;
        public int width, height;

        private Rectangle ActualPosition;
        public UIpanel(Rectangle RelativePosition, Texture2D texture, SpriteFont font, int BorderSize)
        {
            ActualPosition = RelativePosition;
            width = RelativePosition.Width;
            height = RelativePosition.Height;
            this.BorderSize = BorderSize;
            this.texture = texture;
            this.font = font;
            this.RelativePosition = new Vector2(RelativePosition.X, RelativePosition.Y);

        }

        public void Update(ref GameTime gameTime, ref Camera cam)
        {
            ActualPosition = new Rectangle((int)(cam.Position.X + RelativePosition.X), (int)(cam.Position.Y + RelativePosition.Y), width, height);
            foreach(ScreenText t in ScreenTexts)
            {
                t.Update(ref gameTime, ref cam, ref RelativePosition);
            }
        }

        public void Draw(ref SpriteBatch spriteBatch)
        {
            //TopBorder;
            spriteBatch.Draw(texture, new Rectangle(ActualPosition.X, ActualPosition.Y, ActualPosition.Width, BorderSize),new Rectangle(0, 0, texture.Width, BorderSize) , Color.White);
            //LeftBorder
            spriteBatch.Draw(texture, new Rectangle(ActualPosition.X, ActualPosition.Y + BorderSize, BorderSize, height - (BorderSize * 2)), new Rectangle(0, BorderSize, BorderSize, texture.Height - (BorderSize * 2)), Color.White);
            //Center
            spriteBatch.Draw(texture, new Rectangle(ActualPosition.X + BorderSize, ActualPosition.Y + BorderSize, ActualPosition.Width - BorderSize, ActualPosition.Height - BorderSize), new Rectangle(BorderSize, BorderSize, texture.Width - BorderSize, texture.Height - (BorderSize * 2)), Color.White);
            //RightBorder
            spriteBatch.Draw(texture, new Rectangle(ActualPosition.X + ActualPosition.Width - BorderSize, ActualPosition.Y + BorderSize, BorderSize, ActualPosition.Height - (BorderSize * 2)), new Rectangle(texture.Width - BorderSize, BorderSize, BorderSize, texture.Height - (BorderSize * 2)), Color.White);
            //BottomBorder
            spriteBatch.Draw(texture, new Rectangle(ActualPosition.X, ActualPosition.Y + ActualPosition.Height - BorderSize, ActualPosition.Width, BorderSize), new Rectangle(0, texture.Height - BorderSize, texture.Width, BorderSize ), Color.White);

            foreach (ScreenText text in ScreenTexts)
            {
                text.Draw(ref spriteBatch, ref font);
            }
        }


    }
}