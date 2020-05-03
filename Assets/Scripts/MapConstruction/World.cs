using Assets.Scripts.Information.Items;
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
        private float amplitude, offset;
        private float caving;

        public GameObject WorldObject;
        private KnowledgeBase knowledgebase;
        private Dictionary<Vector2, float> MapGraph;
        private Dictionary<Vector2, Tile> MapTiles;
        private Dictionary<Vector2, Tile> StructureTiles;
        private List<int> features;

        // region height paramaters
        private float skyheight = 0.95f;
        private float surfaceheight = 0.7f;
        private float coreheight = 0.1f;
        private float displacement = 2;

        // normal debugging
        private bool debugging = true;

        // Public functions
        public World(int _width, int _height, KnowledgeBase _kb, GameObject _obj)
        {
            // setting variables
            width = _width;
            height = _height;
            knowledgebase = _kb;
            WorldObject = _obj;

            amplitude = 14f;
            caving = 0.5f;
            offset = Random.Range(5, 50);
            features = new List<int>();

            // initializing variables
            MapGraph = new Dictionary<Vector2, float>();
            MapTiles = new Dictionary<Vector2, Tile>();
            StructureTiles = new Dictionary<Vector2, Tile>();

            // creating functions
            BuildGraph();

            BuildTerrain();

            BuildBiomes();

            GrowFoilage();

            BuildOres();

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
                if (pos.y > (height * surfaceheight) + Random.Range(-displacement, displacement))
                {
                    tile.home_region = knowledgebase.regions.Single(item => item.id == 0); // surface
                }
                else if (pos.y < (height * coreheight) + Random.Range(-displacement, displacement))
                {
                    tile.home_region = knowledgebase.regions.Single(item => item.id == 2); // core
                }
                else
                {
                    tile.home_region = knowledgebase.regions.Single(item => item.id == 1); // underground
                }

                // Add tile to List
                MapTiles.Add(pos, tile);
            }
        }

        private void BuildBiomes()
        {
            int basetiles = 0;

            foreach (Tile tile in MapTiles.Values)
            {
                if (tile.home_region.id == 2)
                { 
                    tile.home_biome = knowledgebase.biomes.Single(item => item.id == 5);
                    basetiles++;
                }
            }

            foreach (Biome b in knowledgebase.biomes.ToList())
            {
                int tiles = BuildBiome(b);
                if (debugging)
                {
                    Debug.Log(tiles + " " + b.name + " tiles created.");
                }
            }

            foreach (Tile tile in MapTiles.Values)
            {
                if (tile.home_biome == null)
                {
                    if (tile.home_region.id == 0)
                    {
                        tile.home_biome = knowledgebase.biomes.Single(item => item.id == 1);
                        basetiles++;
                    }
                    else
                    {
                        tile.home_biome = knowledgebase.biomes.Single(item => item.id == 4);
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
                    if (tile.home_region.id == 0) // surface
                    {
                        if (Noise <= ( 0.6f + biome.top_occurance) && (biome.top_occurance>0))
                        {
                            tile.home_biome = biome;
                            tiles++;
                        }
                    } // underground
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
            // Need to be changed to follow the pattern of Biome
            // Instead of generating a single noise map and generating all ores from it
            // need a noise map per ore type

            // Noise map parameters
            int ore_octaves = 4;
            float[] ore_offsets = new float[ore_octaves];

            for (int i = 0; i < (ore_octaves-1); i++)
            {
                ore_offsets[i] = Random.Range(0, 999999);
            }

            float b_freq = 20f;
            float ore_rarity = 0.7f;

            // Debugging variable
            int ores_created = 0;

            // generating noise map and assigning ores based on map
            foreach (Tile tile in MapTiles.Values)
            {
                Vector2 pos = tile.position;

                // Setting the Coord using scale for our PerlinNoise
                float xCoord = ((float)pos.x / (b_freq));
                float yCoord = ((float)pos.y / (b_freq));

                //
                bool canSpawn = true;

                for (int i = 1; i <= (displacement*3); i++)
                {
                    Vector2 abovepos = new Vector2(pos.x, pos.y + i);

                    // Ore can only spawn at least twice the displacement number of blocks below the surface
                    if (canSpawn &&  (!MapTiles.Keys.Contains(abovepos)))
                    {
                        canSpawn = false;
                    }
                }

                float dice_roll = Random.Range(0, 100);

                foreach (float offset in ore_offsets)
                {
                    float Noise = Mathf.PerlinNoise(xCoord + offset, yCoord + offset);

                    if (canSpawn && tile.position.y < ((height * skyheight) - (displacement * 2)))
                    {
                        if (Noise > ore_rarity)
                        {
                            if (tile.home_region.id == 0) // surface
                            {
                                if (dice_roll <= 20)
                                {
                                    tile.SetItem(knowledgebase.items.Single(item => item.id == 10)); // Tin
                                }
                                else
                                {
                                    tile.SetItem(knowledgebase.items.Single(item => item.id == 32)); // Copper
                                }
                            }
                            else if (tile.home_region.id == 1) // underground
                            {
                                if (dice_roll <= 2)
                                {
                                    tile.SetItem(knowledgebase.items.Single(item => item.id == 13)); // Emerald
                                }
                                else if (dice_roll <= 40)
                                {
                                    tile.SetItem(knowledgebase.items.Single(item => item.id == 31)); // Coal
                                }
                                else
                                {
                                    tile.SetItem(knowledgebase.items.Single(item => item.id == 9)); // Iron
                                }
                            }
                            else if (tile.home_region.id == 2) // the core
                            {
                                if (dice_roll <= 3)
                                {
                                    tile.SetItem(knowledgebase.items.Single(item => item.id == 11)); // Ruby
                                }
                                else if (dice_roll <= 8)
                                {
                                    tile.SetItem(knowledgebase.items.Single(item => item.id == 12)); // Silver
                                }
                                else if (dice_roll <= 30)
                                {
                                    tile.SetItem(knowledgebase.items.Single(item => item.id == 19)); // Gold
                                }
                                else if (dice_roll <= 50)
                                {
                                    tile.SetItem(knowledgebase.items.Single(item => item.id == 27)); // Cobalt
                                }
                                else
                                {
                                    tile.SetItem(knowledgebase.items.Single(item => item.id == 9)); // Iron
                                }
                            }

                            ores_created++;
                        }
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
                if ( (pos.y <= ((height*coreheight)/2) + Random.Range(-displacement, displacement) && (!MapTiles.Keys.Contains(pos)) ) )
                {
                    Tile newtile = new Tile(pos, this);

                    newtile.SetItem(knowledgebase.liquids.Single(item => item.id == 1));

                    MapTiles.Add(pos, newtile);
                }
            }
            
        }

        private void GrowFoilage()
        {
            foreach (Tile tile in MapTiles.Values)
            {
                // Check for Large Structures I.E. Pyramids
                Check_LargeStructures(tile);
            }

            foreach (Tile tile in MapTiles.Values)
            {
                // Check for Turf I.E. Grass, Moss, Ash
                CheckTurf(tile);

                // Check for Small Structures I.E. Trees
                Check_SmallStructures(tile);
            }

            foreach (Tile tile in MapTiles.Values)
            {
                //Check for Foilage I.E. Cactus
                Check_Foilage(tile);
            }

            if (debugging)
            {
                Debug.Log(features.Count + " features created.");
            }
        }

        private void RenderWorld()
        {
            foreach (Tile tile in MapTiles.Values)
            { 
                tile.Render();
            }

            foreach (Tile tile in StructureTiles.Values)
            {
                tile.Render();
            }
        }

        /* helper functions */


        private void CheckTurf(Tile tile)
        {
            Vector2 poscheck = new Vector2(tile.position.x, tile.position.y + 1);

            if (!MapTiles.ContainsKey(poscheck))
            {
                if (tile.position.y < (height * surfaceheight))
                {
                    GrowTurf(tile, 150);
                }
                else
                {
                    GrowTurf(tile);
                }
            }
        }

        private void Check_LargeStructures(Tile tile)
        {
            Vector2 poscheck = new Vector2(tile.position.x, tile.position.y + 1);

            float dice_roll = Random.Range(1, 100);

            if (!MapTiles.ContainsKey(poscheck))
            {
                if (tile.home_biome.standard_foilage_id == 3) // Biome that has Grass
                {
                    if ((dice_roll <= 10) && // Wooden Windmill
                        CheckStructure(8, tile.position))
                    {
                        features.Add(8);
                    }
                    else if ( (dice_roll <= 20) && // Cabin
                        CheckStructure(3, tile.position) )
                    {
                        features.Add(3);
                    }
                }
                else if (tile.home_biome.standard_foilage_id == 8) // Biome that has Basalt
                { 
                    if (CheckStructure(9, tile.position)) // Pantheon
                    {
                        features.Add(9);
                    }
                }
                else if (tile.home_biome.standard_foilage_id == 23) // Graveyard Dirt
                {
                    if (dice_roll<25 &&(CheckStructure(7, tile.position)) ) // Crypt
                    {
                        features.Add(7);
                    }
                }
                else if (tile.home_biome.standard_foilage_id == 5) // Biome that has Sand
                {
                    if (CheckStructure(2, tile.position))  // Pyramid
                    {
                        features.Add(2);
                    }
                }
                else if (tile.home_biome.standard_foilage_id == 4) // Ash
                {
                    if (CheckStructure(11, tile.position)) // Skyscraper
                    {
                        features.Add(11);
                    }
                }
            }
        }

        private void Check_SmallStructures(Tile tile)
        {
            Vector2 poscheck = new Vector2(tile.position.x, tile.position.y + 1);

            float dice_roll = Random.Range(1, 100);

            if (!MapTiles.ContainsKey(poscheck))
            {
               if (tile.home_biome.standard_foilage_id == 3) // Grass
                {
                    if (CheckStructure(0, tile.position)) // Evergreen Tree Wide
                    {
                        features.Add(0);
                    }
                    else if (CheckStructure(14, tile.position)) // Evergreen Tree #2
                    {
                        features.Add(14);
                    }
                }
                else if (tile.home_biome.standard_foilage_id == 5) // Sand
                {

                }
                else if (tile.home_biome.standard_foilage_id == 7) // Snow
                {
                    if (CheckStructure(4, tile.position)) //
                    {
                        features.Add(4);
                    }
                }
                else if (tile.home_biome.standard_foilage_id == 8) // Basalt
                {
                    if (CheckStructure(12, tile.position)) // Diorite Column
                    {
                        features.Add(12);
                    }
                }
            }
        }

        private void Check_Foilage(Tile tile)
        {
            Vector2 poscheck = new Vector2(tile.position.x, tile.position.y + 1);

            float dice_roll = Random.Range(1, 100);

            if (!MapTiles.ContainsKey(poscheck))
            {
                if (tile.home_biome.standard_foilage_id == 3) // Grass
                {
                    if (CheckStructure(6, tile.position)) // Flower
                    {
                        features.Add(6);
                    }
                }
                else if (tile.home_biome.standard_foilage_id == 1) // Dirt
                {
                    if (CheckStructure(13, tile.position)) // Strawberry Bush
                    {
                        features.Add(13);
                    }
                }
                else if (tile.home_biome.standard_foilage_id == 5) // Sand
                {
                    if (CheckStructure(1, tile.position)) // Cactus
                    {
                        features.Add(1);
                    }
                }
                else if (tile.home_biome.standard_foilage_id == 7) // Snow
                {

                }
                else if (tile.home_biome.standard_foilage_id == 23) // Graveyard Dirt
                {
                    if ((dice_roll <= 50) && CheckStructure(5, tile.position)) // Grave
                    {
                        features.Add(5);
                    }
                }
            }
        }

        private bool CheckStructure(int strucid, Vector2 startpos)
        {
            Debug.Log(strucid + " structure attempted.");

            Structure struc = translateStruc(strucid, startpos);

            bool canbuild = true;

            foreach (Vector2 pos in struc.spacechecks)
            {
                if (canbuild && (!(MapGraph.Keys.Contains(pos)) || MapTiles.Keys.Contains(pos) || StructureTiles.Keys.Contains(pos))) {
                    canbuild = false;
                }
            }

            foreach (Vector2[] array in struc.pos_array)
            {
                foreach (Vector2 pos in array)
                {
                    if (canbuild && (!(MapGraph.Keys.Contains(pos)) || MapTiles.Keys.Contains(pos) || StructureTiles.Keys.Contains(pos)) )
                    {
                        canbuild = false;
                    }
                }
            }

            if (struc.ground_check && !grounding_structure(struc.groundspots, struc.biome_id))
            {
                canbuild = false;
            }


            if (canbuild)
            {
                for (int i = 0; i < struc.Itemids.Length; i++)
                {
                    foreach (Vector2 pos in struc.pos_array[i])
                    {
                        Tile newtile = new Tile(pos, this);

                        newtile.SetItem(knowledgebase.items.Single(item => item.id == struc.Itemids[i]));

                        StructureTiles.Add(pos, newtile);
                    }
                }
            }

            return canbuild;
        }

        /* tier 2 helper functions */

        private void GrowTurf(Tile tile, int odds = 100)
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

            if (odds > 100)
            {
                // Spread it below
                Vector2 spreadCheck = new Vector2(tile.position.x+1, tile.position.y - 1);

                if (MapTiles.ContainsKey(spreadCheck) && tile.home_biome == MapTiles[spreadCheck].home_biome)
                {
                    GrowTurf(MapTiles[spreadCheck], odds - Random.Range(10, 40));
                }

                // Spread it below
                Vector2 spreadCheck2 = new Vector2(tile.position.x - 1, tile.position.y - 1);

                if (MapTiles.ContainsKey(spreadCheck2) && tile.home_biome == MapTiles[spreadCheck2].home_biome)
                {
                    GrowTurf(MapTiles[spreadCheck2], odds - Random.Range(10, 40));
                }
            }


            if (odds >= 50)
            {
                // Spread it below
                Vector2 spreadCheck3 = new Vector2(tile.position.x, tile.position.y - 1);

                if (MapTiles.ContainsKey(spreadCheck3) && tile.home_biome == MapTiles[spreadCheck3].home_biome)
                {
                    GrowTurf(MapTiles[spreadCheck3], odds - Random.Range(10, 40));
                }
            }
        }

        private Structure translateStruc(int strucid, Vector2 startpos)
        {
            switch (strucid)
            {
                case 0: // evergreen tree
                    return knowledgebase.strucLoader.getTree(startpos);
                case 1: // cactus
                    return knowledgebase.strucLoader.getCactus(startpos);
                case 2: // pyramid
                    return knowledgebase.strucLoader.getPyramid(startpos);
                case 3: // stone tower
                    return knowledgebase.strucLoader.getStoneCabin(startpos);
                case 4: // pine tree
                    return knowledgebase.strucLoader.getPineTree(startpos);
                case 5: // grave
                    return knowledgebase.strucLoader.getGrave(startpos);
                case 6: // flower
                    return knowledgebase.strucLoader.getFlower(startpos);
                case 7: // crypt
                    return knowledgebase.strucLoader.getStoneCrypt(startpos);
                case 8: // wooden windmill
                    return knowledgebase.strucLoader.getWoodWindmill(startpos);
                case 9: // pantheon
                    return knowledgebase.strucLoader.getPantheon(startpos);
                case 11: // skyscraper
                    return knowledgebase.strucLoader.getSkyscraper(startpos);
                case 12: // diorite column
                    return knowledgebase.strucLoader.getDioriteColumn(startpos);
                case 13: // strawberry bush
                    return knowledgebase.strucLoader.getStrawberryBush(startpos);
                case 14: // evergreen tree #2
                    return knowledgebase.strucLoader.getTree2(startpos);
                default: // no structure found
                    return null;
            }
        }

        private bool grounding_structure(Vector2[] locs, int biome_id)
        {

            foreach (Vector2 pos in locs)
            {
                if (!MapTiles.Keys.Contains(pos) ||
                   (MapTiles.Keys.Contains(pos) && MapTiles[pos].home_biome == null) ||
                   (MapTiles.Keys.Contains(pos) && MapTiles[pos].home_biome != null && MapTiles[pos].home_biome.id != biome_id))
                {
                    return false;
                }
            }

            return true;
        }

        

    }
}