using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace SQ
{
    class InventoryManager
    {
        UILayer Inventory;
        bool Open;
        public void LoadContent(ref ContentManager content, ref Player player)
        {
            Open = false;
            Inventory = new UILayer();
            Inventory.AddPanel(new UIpanel(new Rectangle(20, 20, 800, 800), content.Load<Texture2D>("menu"), content.Load<SpriteFont>("font"), 20));
            Inventory.AddTextToPanel(0, "Strength: " + player.STR.ToString(), new Vector2(400,50), Color.Black);
            Inventory.AddTextToPanel(0, "Dexterity: " + player.DEX.ToString(), new Vector2(400, 75), Color.Black);
            Inventory.AddTextToPanel(0, "Willpower: " + player.WIL.ToString(), new Vector2(400, 100), Color.Black);
            Inventory.AddTextToPanel(0, "Intelligence: " + player.INT.ToString(), new Vector2(400, 125), Color.Black);
            Inventory.AddTextToPanel(0, "Charisma: " + player.CHA.ToString(), new Vector2(400, 150), Color.Black);
            Inventory.AddTextToPanel(0, "Vitality: " + player.VIT.ToString(), new Vector2(400, 175), Color.Black);
            Inventory.AddTextToPanel(0, "Luck: " + player.LCK.ToString(), new Vector2(400, 200), Color.Black);
        }

        public void Update(ref GameTime gameTime, ref Camera cam, ref KeyboardState PKS)
        {
            if (PKS.IsKeyUp(Keys.Escape) && Keyboard.GetState().IsKeyDown(Keys.Escape))
            {
                Open = !Open;
            }
            if(Open)
                Inventory.Update(ref gameTime, ref cam);
        }

        public void Draw(ref SpriteBatch spriteBatch)
        { 
            if(Open)
                Inventory.Draw(ref spriteBatch);
        }
    }
}
