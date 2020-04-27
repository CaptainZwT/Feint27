using Assets.Scripts.Information.Map;
using Assets.Scripts.Knowledge_Base;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Assets.Scripts.MapConstruction
{
    class World
    {

        // Charateristics
        private int width, height;
        private float amplitude, frequency, offset;
        private float caving;

        public GameObject WorldObject;
        private KnowledgeBase knowledgebase;
        private Dictionary<Vector2, float> MapGraph;
        private Dictionary<Vector2, Tile> MapTiles;


        // Displacement is a variable that determines how sharp biomes and regions change.
        private int displacement = 3;

        // region height paramaters
        private float skyheight = 0.95f;
        private float surfaceheight = 0.6f;
        private float coreheight = 0.02f;

        // normal debugging
        private bool debugging =  true;

        // Public functions
        public World(int _width, int _height, KnowledgeBase _kb, GameObject _obj)
        {
            // setting variables
            width = _width;
            height = _height;
            knowledgebase = _kb;
            WorldObject = _obj;

            amplitude = 14f;
            frequency = 2f;
            caving = 0.5f;
            offset = Random.Range(5, 50);

            // initializing variables
            MapGraph = new Dictionary<Vector2, float>();
            MapTiles = new Dictionary<Vector2, Tile>();


            // creating functions
            BuildGraph();

            BuildTerrain();

            BuildBiomes();

            GrowFoilage();

            //BuildOres();

            FillLiquids();

            RenderWorld();
        }

        // Private functions
        private void BuildGraph()
        {

            // building graph
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    Vector2 pos = new Vector2(x, y);

                    // Setting the Coord using scale for our PerlinNoise
                    float xCoord = ((float)x / (width)) * (amplitude);
                    float yCoord = ((float)y / (height)) * (amplitude);

                    // Creating Noise based on the number of octaves
                    float Noise = Mathf.PerlinNoise(xCoord + offset, yCoord + offset);

                    //xCoord = ((float)x / (8*frequency)) * (amplitude);
                    //yCoord = ((float)y / (8*frequency)) * (amplitude);

                    //Noise += Mathf.PerlinNoise(xCoord, yCoord);

                    //Debug.Log(Noise);

                    // Assigning it to the noise position in the graph
                    MapGraph.Add(pos, Noise);
                }
            }
        }

        private void BuildTerrain()
        {

            // building world
            foreach (Vector2 pos in MapGraph.Keys)
            {
                // initializing variables
                float Noise = MapGraph[pos];

                // Conditional for when a Tile shouldn't exist in this position
                if ( (pos.y >= height * skyheight) || (Noise > caving) )
                {
                    continue; 
                }

                // initializing tile and its characteristics
                Tile tile = new Tile(pos, this);

                // Setting up region of the Tile
                if (pos.y > height * surfaceheight)
                {
                    tile.home_region = knowledgebase.regions.Single(item => item.id == 1);
                }
                else
                {
                    tile.home_region = knowledgebase.regions.Single(item => item.id == 1);
                }

                // Add tile to List
                MapTiles.Add(pos, tile);
            }
        }

        private void BuildBiomes()
        {

            foreach (Biome b in knowledgebase.biomes.ToList())
            {
                int tiles = BuildBiome(b);
                if (debugging)
                {
                    Debug.Log(tiles + " " + b.name + " tiles created.");
                }
            }

            int basetiles = 0;

            foreach (Tile tile in MapTiles.Values)
            {
                if (tile.home_biome == null)
                {
                    if (tile.position.y >= ((height * surfaceheight)))
                    {
                        tile.home_biome = knowledgebase.biomes.Single(item => item.id == 2);
                        basetiles++;
                    }
                    else
                    {
                        tile.home_biome = knowledgebase.biomes.Single(item => item.id == 6);
                        basetiles++;
                    }
                }
            }

            if (debugging)
            {
                Debug.Log(basetiles + " base biome tiles created.");
            }
        }

        private int BuildBiome(Biome biome)
        {
            float b_amp = 4f, b_offset = Random.Range(0, 99999);
            float freq = 4f;
            int tiles = 0;

            // building biomes
            foreach (Tile tile in MapTiles.Values)
            {
                Vector2 pos = tile.position;

                float xCoord = ((float)pos.x / width);
                float yCoord = ((float)pos.y / height);

                // Setting the Coord using scale for our PerlinNoise
                float xNoise = (xCoord / (freq / 2) * (b_amp * 2));
                float yNoise = (yCoord / (freq / 2) * (b_amp * 2));

                // Creating Noise based on the number of octaves
                float Noise = Mathf.PerlinNoise(xNoise + b_offset, yNoise + b_offset);

                xNoise = (xCoord / (freq * 2)) * (b_amp / 2);
                yNoise = (yCoord / (freq * 2)) * (b_amp / 2);

                // Creating Noise based on the number of octaves
                Noise += Mathf.PerlinNoise(xNoise + b_offset, yNoise + b_offset);

                if (tile.home_biome == null)
                {
                    if (tile.position.y >= ((height * surfaceheight)))
                    {
                        if (Noise <= ( 0.6f + biome.top_occurance) && (biome.top_occurance>0))
                        {
                            tile.home_biome = biome;
                            tiles++;
                        }
                    }
                    else if (Noise <= (0.6f + biome.bottom_occurance) && (biome.bottom_occurance > 0))
                    {
                        tile.home_biome = biome;
                        tiles++;
                    }
                }
            }

            return tiles;
        }

        private void BuildOres()
        {
            float b_amp = 10f, b_offset = Random.Range(0, 99999);
            float b_freq = 5f;

            int ores_created = 0, wave;

            // building biomes
            foreach (Tile tile in MapTiles.Values)
            {
                Vector2 pos = tile.position;
                wave = 5;

                // Setting the Coord using scale for our PerlinNoise
                float xCoord = ((float)pos.x / (b_freq / wave)) * (b_amp * wave);
                float yCoord = ((float)pos.y / (b_freq / wave)) * (b_amp * wave);

                // Creating Noise based on the number of octaves
                float Noise = Mathf.PerlinNoise(xCoord + b_offset, yCoord + b_offset);

                wave = 2;

                xCoord = ((float)pos.x / (b_freq * wave)) * (b_amp / wave);
                yCoord = ((float)pos.y / (b_freq * wave)) * (b_amp / wave);

                // Creating Noise based on the number of octaves
                Noise += Mathf.PerlinNoise(xCoord + b_offset, yCoord + b_offset);

                //Debug.Log(Noise);

                if (tile.position.y < ((height * skyheight)-10) )
                {
                    if (Noise > 1.2)
                    {
                        float dice_roll = Random.Range(1f, 100f);

                        if (pos.y >= (height * surfaceheight) / 2)
                        {
                            if (dice_roll < 50)
                            {
                                tile.SetItem(knowledgebase.items.Single(item => item.id == 9)); // Iron
                            }
                            else
                            {
                                tile.SetItem(knowledgebase.items.Single(item => item.id == 10)); // Tin
                            }
                        }
                        else
                        {
                            if (dice_roll < 40)
                            {
                                tile.SetItem(knowledgebase.items.Single(item => item.id == 9)); // Iron
                            }
                            else
                            {
                                tile.SetItem(knowledgebase.items.Single(item => item.id == 13)); // Emerald
                            }
                        }

                        ores_created++;
                    }
                }
            }

            if (debugging)
            {
                Debug.Log(ores_created + " ore blocks created.");
            }
        }

        private void FillLiquids()
        {
            foreach (Vector2 pos in MapGraph.Keys)
            {
                if ( (pos.y <= height*coreheight) && (MapGraph[pos] > caving) )
                {
                    Tile newtile = new Tile(pos, this);

                    newtile.SetItem(knowledgebase.liquids.Single(item => item.id == 2));

                    MapTiles.Add(pos, newtile);
                }
            }
            
        }

        private void GrowFoilage()
        {
            foreach (Tile tile in MapTiles.Values)
            {
                // Check for Grass
                CheckGrass(tile);
            }
        }

        private void RenderWorld()
        {
            foreach (Tile tile in MapTiles.Values)
            { 

                // Finally, Render Tiles
                tile.Render();
            }
        }

        /* helper functions */


        private void CheckGrass(Tile tile)
        {
            Vector2 poscheck = new Vector2(tile.position.x, tile.position.y + 1);

            if (!MapTiles.ContainsKey(poscheck))
            {
                GrowGrass(tile);
            }
        }

        private void GrowGrass(Tile tile, int odds = 100)
        {

            // Grow Grass
            if (tile.home_biome != null)
            {
                tile.SetItem(tile.home_biome.standard_foilage);
            }
            else
            {
                tile.SetItem(tile.home_region.standard_foilage);
            }


            if (odds >= 50)
            {
                // Spread it below
                Vector2 spreadCheck = new Vector2(tile.position.x, tile.position.y - 1);

                if (MapTiles.ContainsKey(spreadCheck) && tile.home_biome == MapTiles[spreadCheck].home_biome)
                {
                    GrowGrass(MapTiles[spreadCheck], odds - Random.Range(10, 40));
                }
            }
        }

    }
}