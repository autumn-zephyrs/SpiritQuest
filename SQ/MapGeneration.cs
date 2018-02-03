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
        private int[] FloorArray;
        private int[] ObjectArray;
        private int[] CollisionArray;

        public int NumberOfFloorObjects;

        private Texture2D GroundTexture;
        private texture[] GroundSprites;
        private Texture2D ObjectTextures;
        private texture[] ObjectSprites;
        JsonData jsonData;

        public void CreateMap(ContentManager content)
        {
            if (!File.Exists("MapData/Ground.json"))
            {
                Directory.CreateDirectory("MapData");
                // initialises a basic 10 by 10 map if there is no map data stored
                string json = @"
                    {
                        ""NumberOfFloorObjects"" : 10,
                        
                        ""FloorArray"" : 
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

                        ""ObjectArray"" : 
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

                          
                        ""CollisionArray"" : 
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
                          
                    }
                ";

                
                // turns the long string above into a Json object and saves it
                jsonData = JsonMapper.ToObject(json);
                File.WriteAllText("MapData/Ground.json", jsonData.ToJson());
            }
            else
            {
                // gathers map data
                jsonData = JsonMapper.ToObject(File.ReadAllText("MapData/Ground.json").ToString());

            }
            //converts the Json arrays into c# arrays
            JsonReader reader = new JsonReader( jsonData["FloorArray"].ToJson().ToString());
            FloorArray = JsonMapper.ToObject<int[]>(reader);
            reader = new JsonReader(jsonData["ObjectArray"].ToJson().ToString());
            ObjectArray = JsonMapper.ToObject<int[]>(reader);
            reader = new JsonReader(jsonData["CollisionArray"].ToJson().ToString());
            CollisionArray = JsonMapper.ToObject<int[]>(reader);

            NumberOfFloorObjects = (int)jsonData["NumberOfFloorObjects"];

            // number of floor objects != number of objects in the arrays(collider, etc)
            if(NumberOfFloorObjects * NumberOfFloorObjects != FloorArray.Length || NumberOfFloorObjects * NumberOfFloorObjects != ObjectArray.Length)
            {
                // clear the arrays 
                Array.Clear(FloorArray,0, FloorArray.Length);
                Array.Clear(ObjectArray, 0, ObjectArray.Length);
                Array.Clear(CollisionArray, 0, CollisionArray.Length);

                // create new ones with the real number of floor objects
                ObjectArray = new int[NumberOfFloorObjects * NumberOfFloorObjects];
                FloorArray = new int[NumberOfFloorObjects * NumberOfFloorObjects];
                CollisionArray = new int[NumberOfFloorObjects * NumberOfFloorObjects];

                // populate them with texture number 0
                for (int i = 0; i < NumberOfFloorObjects * NumberOfFloorObjects; i++)
               {
                    ObjectArray[i] = 0;
                    FloorArray[i] = 0;
                    CollisionArray[i] = 0;
                }

                // save to file
                JsonData jsonDataFloor = JsonMapper.ToJson(FloorArray);
                JsonData jsonDataObject = JsonMapper.ToJson(ObjectArray);
                JsonData jsonDataCollision = JsonMapper.ToJson(CollisionArray);
                string jsondata = @"{ ""NumberOfFloorObjects"" : " + NumberOfFloorObjects.ToString() +  @", ""FloorArray"" : [" + jsonDataFloor.ToJson().ToString() + @"], ""ObjectArray"" : [" + jsonDataObject.ToJson().ToString() + "]," + @" ""CollisionArray"": [" + jsonDataCollision.ToJson().ToString() +"] }";
                File.WriteAllText("MapData/Ground.json", jsondata);


            }
     
            // creates visuals from the arrays
            GroundTexture = content.Load<Texture2D>("GroundTiles");
            ObjectTextures = content.Load<Texture2D>("Fence");
            GroundSprites = new texture[NumberOfFloorObjects * NumberOfFloorObjects];
            ObjectSprites = new texture[NumberOfFloorObjects * NumberOfFloorObjects];


            //This creates and changes the position of the sourceREctangle for each texture based on the array 
            for (int i = 0; i < NumberOfFloorObjects; i++)
            {
                for (int I = 0; I < NumberOfFloorObjects; I++)
                {
                    int X, Y;
                    // 8 = amount of sprites on each row of the sprite sheet
                    X = (FloorArray[(I + (i * NumberOfFloorObjects))] % 8) * 32;
                    Y = (FloorArray[(I + (i * NumberOfFloorObjects))] / 8) * 32;
                    GroundSprites[(i * NumberOfFloorObjects) + I] = new texture(new Rectangle(32 * I, 32 * i, 32, 32), new Rectangle(X, Y, 32, 32));

                    if (ObjectArray[(I + (i * NumberOfFloorObjects))] != -1)
                    {
                        X = (ObjectArray[(I + (i * NumberOfFloorObjects))] % 3) * 32;
                        Y = (ObjectArray[(I + (i * NumberOfFloorObjects))] / 3) * 32;
                        ObjectSprites[(i * NumberOfFloorObjects) + I] = new texture(new Rectangle(32 * I, 32 * i, 32, 32), new Rectangle(X, Y, 32, 32));
                    }
                }
            }

        }

        public void Update(GameTime gameTime, Camera cam)
        {

        }

        public bool  CanPlayerMoveToTile(int tileNumber)
        {
            if (tileNumber < CollisionArray.Length && tileNumber > 0)
            {
                if (CollisionArray[tileNumber] == 1)
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
                if(ObjectSprites[i] != null)
                    ObjectSprites[i].Draw(spriteBatch, ObjectTextures);
            }
        }

        public void DrawAfterPlayer(int playerPos, SpriteBatch spriteBatch)
        {
            for (int i = playerPos - 2; i < ObjectSprites.Length; i++)
            {
                if(ObjectSprites[i] != null && i >= 0)
                    ObjectSprites[i].Draw(spriteBatch, ObjectTextures);
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (texture t in GroundSprites)
            {
                t.Draw(spriteBatch, GroundTexture);
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
