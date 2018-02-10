using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

using Microsoft.Xna;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using LitJson;

namespace SQ
{
    class MapGeneration
    {
        private int[] BaseLayer;
        private int[] Layer1;
        private int[] Layer2;
        private int[] Collision;
        public int NumberOfFloorObjects;
        private Texture2D BaseTexture;
        private texture[] BaseTextureArray;
        private Texture2D Layer1Texture;
        private texture[] Layer1TextureArray;
        private Texture2D Layer2Texture;
        private texture[] Layer2TextureArray;

        JsonData jsonData;

        public void CreateMap(ContentManager content)
        {
            if (!File.Exists("MapData/Ground.json"))
            {
                Directory.CreateDirectory("MapData");
              
                string json = @"
                    {
                        ""NumberOfFloorObjects"" : 10,
                        
                        ""MapData"" : [
                         [1, 1, 1, 1, 1, 1, 1, 1, 1, 1,
                          1, 1, 1, 1, 1, 1, 1, 1, 1, 1,
                          1, 1, 1, 1, 1, 1, 1, 1, 1, 1,
                          1, 1, 1, 1, 1, 1, 1, 1, 1, 1,
                          1, 1, 1, 1, 1, 1, 1, 1, 1, 1,
                          1, 1, 1, 1, 1, 1, 1, 1, 1, 1,
                          1, 1, 1, 1, 1, 1, 1, 1, 1, 1,
                          1, 1, 1, 1, 1, 1, 1, 1, 1, 1,
                          1, 1, 1, 1, 1, 1, 1, 1, 1, 1,
                          1, 1, 1, 1, 1, 1, 1, 1, 1, 1],     

                         [1, 1, 1, 1, 1, 1, 1, 1, 1, 1,
                          1, 1, 1, 1, 1, 1, 1, 1, 1, 1,
                          1, 1, 1, 1, 1, 1, 1, 1, 1, 1,
                          1, 1, 1, 1, 1, 1, 1, 1, 1, 1,
                          1, 1, 1, 1, 1, 1, 1, 1, 1, 1,
                          1, 1, 1, 1, 1, 1, 1, 1, 1, 1,
                          1, 1, 1, 1, 1, 1, 1, 1, 1, 1,
                          1, 1, 1, 1, 1, 1, 1, 1, 1, 1,
                          1, 1, 1, 1, 1, 1, 1, 1, 1, 1,
                          1, 1, 1, 1, 1, 1, 1, 1, 1, 1] ,

                         [0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
                          0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
                          0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
                          0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
                          0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
                          0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
                          0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
                          0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
                          0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
                          0, 0, 0, 0, 0, 0, 0, 0, 0, 0] ,
                        
                         [1, 1, 1, 1, 1, 1, 1, 1, 1, 1,
                          1, 0, 0, 0, 0, 0, 0, 0, 0, 1,
                          1, 0, 0, 0, 0, 0, 0, 0, 0, 1,
                          1, 0, 0, 0, 0, 0, 0, 0, 0, 1,
                          1, 0, 0, 0, 0, 0, 0, 0, 0, 1,
                          1, 0, 0, 0, 0, 0, 0, 0, 0, 1,
                          1, 0, 0, 0, 0, 0, 0, 0, 0, 1,
                          1, 0, 0, 0, 0, 0, 0, 0, 0, 1,
                          1, 0, 0, 0, 0, 0, 0, 0, 0, 1,
                          1, 1, 1, 1, 1, 1, 1, 1, 1, 1] 
                          ]
                    }
                ";

                

                jsonData = JsonMapper.ToObject(json);
                File.WriteAllText("MapData/Ground.json", jsonData.ToJson());
            }
            else
            {

                jsonData = JsonMapper.ToObject(File.ReadAllText("MapData/Ground.json").ToString());

            }
            JsonReader reader = new JsonReader( jsonData["MapData"][0].ToJson().ToString());
            BaseLayer = JsonMapper.ToObject<int[]>(reader);

            reader = new JsonReader(jsonData["MapData"][1].ToJson().ToString());
            Layer1 = JsonMapper.ToObject<int[]>(reader);

            reader = new JsonReader(jsonData["MapData"][2].ToJson().ToString());
            Layer2 = JsonMapper.ToObject<int[]>(reader);

            reader = new JsonReader(jsonData["MapData"][3].ToJson().ToString());
            Collision = JsonMapper.ToObject<int[]>(reader);

            NumberOfFloorObjects = (int)jsonData["NumberOfFloorObjects"];

            if(NumberOfFloorObjects * NumberOfFloorObjects != BaseLayer.Length || NumberOfFloorObjects * NumberOfFloorObjects != Layer1.Length)
            {

                Array.Clear(BaseLayer,0, BaseLayer.Length);
                Array.Clear(Layer1, 0, Layer1.Length);
                Array.Clear(Collision, 0, Collision.Length);
                Array.Clear(Layer2, 0, Layer2.Length);
                Layer1 = new int[NumberOfFloorObjects * NumberOfFloorObjects];
                Layer2 = new int[NumberOfFloorObjects * NumberOfFloorObjects];
                BaseLayer = new int[NumberOfFloorObjects * NumberOfFloorObjects];
                Collision = new int[NumberOfFloorObjects * NumberOfFloorObjects];

                for (int i = 0; i < NumberOfFloorObjects * NumberOfFloorObjects; i++)
               {
                    Layer1[i] = 0;
                    Layer2[i] = -1;
                    BaseLayer[i] = 0;
                    Collision[i] = 0;
                }
                JsonData jsonDataBase = JsonMapper.ToJson(BaseLayer);
                JsonData jsonDataLayer1 = JsonMapper.ToJson(Layer1);
                JsonData jsonDataLayer2 = JsonMapper.ToJson(Layer2);
                JsonData jsonDataCollision = JsonMapper.ToJson(Collision);
                string jsondata = @"{ ""NumberOfFloorObjects"" : " + NumberOfFloorObjects.ToString() +  @", ""MapData"" : {[" + jsonDataBase.ToJson().ToString() + @"], [" + jsonDataLayer1.ToJson().ToString() + "]," + @" [" + jsonDataLayer2.ToJson().ToString() + "]," + @"[" + jsonDataCollision.ToJson().ToString() +"]} }";
                File.WriteAllText("MapData/Ground.json", jsondata);


            }
            BaseTexture = content.Load<Texture2D>("GroundTiles");
            Layer1Texture = content.Load<Texture2D>("Fence");
            Layer2Texture = content.Load<Texture2D>("Fence");
            BaseTextureArray = new texture[NumberOfFloorObjects * NumberOfFloorObjects];
            Layer1TextureArray = new texture[NumberOfFloorObjects * NumberOfFloorObjects];
            Layer2TextureArray = new texture[NumberOfFloorObjects * NumberOfFloorObjects];
            for (int i = 0; i < NumberOfFloorObjects; i++)
            {
                for (int I = 0; I < NumberOfFloorObjects; I++)
                {
                    //change the the number that xy are divided by (in this case 8) to change the number of
                    //items on a row in the sprite sheet 
                    int X, Y;
                    X = (BaseLayer[(I + (i * NumberOfFloorObjects))] % 8) * 32;
                    Y = (BaseLayer[(I + (i * NumberOfFloorObjects))] / 8) * 32;
                    BaseTextureArray[(i * NumberOfFloorObjects) + I] = new texture(new Rectangle(32 * I, 32 * i, 32, 32), new Rectangle(X, Y, 32, 32));

                    if (Layer1[(I + (i * NumberOfFloorObjects))] != -1)
                    {
                        X = (Layer1[(I + (i * NumberOfFloorObjects))] % 3) * 32;
                        Y = (Layer1[(I + (i * NumberOfFloorObjects))] / 3) * 32;
                        Layer1TextureArray[(i * NumberOfFloorObjects) + I] = new texture(new Rectangle(32 * I, 32 * i, 32, 32), new Rectangle(X, Y, 32, 32));
                    }

                    if(Layer2[(I + (i * NumberOfFloorObjects))] != -1)
                    {
                        X = (Layer2[(I + (i * NumberOfFloorObjects))] % 3) * 32;
                        Y = (Layer2[(I + (i * NumberOfFloorObjects))] / 3) * 32;
                        Layer2TextureArray[(i * NumberOfFloorObjects) + I] = new texture(new Rectangle(32 * I, 32 * i, 32, 32), new Rectangle(X, Y, 32, 32));

                    }
                }
            }

        }

        public void Update(GameTime gameTime, Camera cam)
        {

        }

        public bool  CanPlayerMoveToTile(int tileNumber)
        {
            if (tileNumber < Collision.Length && tileNumber > 0)
            {
                if (Collision[tileNumber] == 1)
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
            return true;
        }


        public void DrawBeforePlayer(int playerPos, SpriteBatch spriteBatch)
        {
            for(int i = 0; i <= playerPos - 2; i++)
            {
                if(Layer1TextureArray[i] != null)
                    Layer1TextureArray[i].Draw(spriteBatch, Layer1Texture);
            }
        }

        public void DrawAfterPlayer(int playerPos, SpriteBatch spriteBatch)
        {
            for (int i = playerPos - 2; i < Layer1TextureArray.Length; i++)
            {
                if (Layer1TextureArray[i] != null && i >= 0)
                    Layer1TextureArray[i].Draw(spriteBatch, Layer1Texture);
            }
            for (int i = 0; i < Layer2TextureArray.Length; i++)
            {
                if (Layer2TextureArray[i] != null && i >= 0)
                    Layer2TextureArray[i].Draw(spriteBatch, Layer2Texture);
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (texture t in BaseTextureArray)
            {
                t.Draw(spriteBatch, BaseTexture);
            }            
        }
    }
    public class texture
    {

        Rectangle Position;
        Rectangle Source;

        public texture(Rectangle Pos, Rectangle source)
        {
            Position = Pos;
            Source = source;
        }

        public void Draw(SpriteBatch spriteBatch, Texture2D texture) 
        {
            spriteBatch.Draw(texture,Position,Source,Color.White);
        }
    }

    
}
