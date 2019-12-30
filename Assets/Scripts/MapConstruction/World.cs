using Assets.Scripts.Information.Items;
using Assets.Scripts.Information.Map;
using Assets.Scripts.Knowledge_Base;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Object = UnityEngine.Object;
using Random = UnityEngine.Random;

namespace Assets.Scripts.MapConstruction
{
    class World
    {
        // Charateristics
        private int width;
        private int height;
        private float scale;
        private int octaves;
        public GameObject WorldObject;
        private KnowledgeBase knowledgebase;

        // Setting variables for Map construction
        private float skyheight = 0.95f;
        private float voidheight = 0.03f;
        // Displacement is a variable that determines how sharp biomes and regions change.
        private int displacement = 2;
        // region height paramaters
        private float surfaceheight = 0.9f;
        private float hellheight = 0.1f;

        // placeholders for information used in the World generating process
        private Dictionary<Vector3, float> CoordinateGraph;
        private Dictionary<Vector3, TileHolder> TileMapping;
        private Dictionary<Vector3, Tile> Tiles;

        /// <summary>
        /// Initializing World : Its Characteristics and Terrain
        /// </summary>
        /// <param name="width">Width</param>
        /// <param name="height">Height</param>
        /// <param name="scale">Scale</param>
        /// <param name="octaves">Octaves</param>
        public World(KnowledgeBase _kb, int _width, int _height, float _scale = 0.1f, int _octaves = 3)
        {
            // initializing World's characteristics
            width = _width;
            height = _height;
            scale = _scale;
            octaves = _octaves;

            // Creating the coordinate Graph to use for Building the World
            AssignLocations(_kb);
            // Building the World using the CoordinateGraph generated in the previous function
            BuildWorld(_kb);
        }

        /// <summary>
        /// Function that when called generates this class's Coordinate Graph (Vector3 position to Float noise).
        /// </summary>
        private void AssignLocations(KnowledgeBase kb)
        {
            // initialzing variables
            CoordinateGraph = new Dictionary<Vector3, float>();
            TileMapping = new Dictionary<Vector3, TileHolder>();
            float offset = Random.Range(0f, 10000f); // This controls why maps look different each time, it's a different location on the generated perlin map.

            // Generating the Noise and Coordinate Map
            for (int x = 1; x < width; x++)
            {
                for (int y = 1; y < height; y++)
                {
                    Vector3 newpos = new Vector3(x, y, 0);

                    float CompleteNoise = 0;

                    for (int o = 0; o < octaves; o++)
                    {
                        float xCoord = x * scale;
                        float yCoord = y * scale;
                        float Noise = Mathf.PerlinNoise(xCoord + offset, yCoord + offset);
                        CompleteNoise += Noise;
                    }

                    CoordinateGraph.Add(newpos, CompleteNoise);
                }
            }

            foreach(Vector3 pos in CoordinateGraph.Keys)
            {
                TileHolder tile = new TileHolder();
                TileMapping.Add(pos, tile);

                // Setting Surface Region
                if ((pos.y >= height * surfaceheight + Random.Range(-displacement, displacement)))
                {
                    tile.home_region = kb.regions.Single(item => item.id == 0);

                    // Identifying Biomes
                    if (pos.x >= (width * 0.8) + Random.Range(-displacement, displacement))
                    {
                        if (pos.x >= (width * 0.87) + Random.Range(-displacement, displacement))
                        {
                            // Warm Ocean Biome
                            tile.home_biome = kb.biomes.Single(item => item.id == 4);
                        }
                        else
                        {
                            // Sandy Shore Biome
                            tile.home_biome = kb.biomes.Single(item => item.id == 2);
                        }
                    }
                    else if (pos.x <= (width * 0.2) + Random.Range(-displacement, displacement))
                    {
                        if (pos.x <= (width * 0.1) + Random.Range(-displacement, displacement))
                        {
                            // Cold Ocean Biome
                            tile.home_biome = kb.biomes.Single(item => item.id == 5);
                        }
                        else
                        {
                            // Taiga Biome
                            tile.home_biome = kb.biomes.Single(item => item.id == 3);
                        }
                    }
                }
                // Setting Core Region
                else if ((pos.y <= height * hellheight + Random.Range(-displacement, displacement)))
                {
                    tile.home_region = kb.regions.Single(item => item.id == 999);

                    // Identifying Biomes
                    if (pos.y <= height * voidheight)
                    {
                        // Molten Core Biome
                        tile.home_biome = kb.biomes.Single(item => item.id == 1);
                    }
                }
                else // Setting Underground Region
                {
                    tile.home_region = kb.regions.Single(item => item.id == 1);
                }
            }
        }

        /// <summary>
        /// Function that when called uses this class's Coordinate Graph to generate the terrain of this World.
        /// </summary>
        private void BuildWorld(KnowledgeBase kb)
        {
            // Initializing variables
            WorldObject = new GameObject();
            Tiles = new Dictionary<Vector3, Tile>();

            // Initial loop placing initial landmass down, and assigning tiles to biomes and regions
            foreach (Vector3 pos in CoordinateGraph.Keys)
            {
                // initializing variables
                float tile_noise = CoordinateGraph[pos];
                
                // Conditional for when a Tile shouldn't exist in this position
                if (pos.y >= height*skyheight)
                {
                    continue;
                }

                TileHolder tileholder = TileMapping[pos];

                // Checking if Tile is not part of a Cave
                if ((tileholder.home_biome == null && tileholder.home_region.region_class_id == 0 && tile_noise < 0.9) ||
                    (tileholder.home_biome!=null && tileholder.home_biome.region_class_id==0 && tile_noise < 0.9))
                {
                    continue;
                }

                Tile tile = new Tile(pos, this);
                tile.home_region = TileMapping[pos].home_region;
                tile.home_biome = TileMapping[pos].home_biome;
                Tiles.Add(pos, tile);
            }

            // Setting Base Tile(Item) and Foilage
            foreach(Vector3 pos in Tiles.Keys)
            {
                Tile tile = Tiles[pos];
                // If a tile has no home region or biome, no need to go through this loop which decorates the various tiles based on those factors.
                if (tile.home_region==null && tile.home_biome==null)
                {
                    continue;
                }

                var home_region = (tile.home_biome != null ? tile.home_biome : tile.home_region);
                var region_class = home_region.region_class;

                // Region Classification Rules
                if (region_class.id == 1) // Crater Class
                {
                    // Within Crater
                    if (System.Math.Abs(tile.body.transform.position.x - (width*0.01)) < (width * 0.02))
                    {
                        tile.SetItem(home_region.standard_liquid);
                    }
                    else // Outside of Crater
                    {
                        tile.SetItem(home_region.standard_block);
                    }
                }
                else if (region_class.id == 2) // Ocean Class
                {
                    // Placing the Ocean Bed
                    if (Math.Abs(pos.y - height * surfaceheight + Random.Range(-displacement, displacement)) < (height*0.01))
                    {
                        tile.SetItem(home_region.standard_block);
                    }
                    // Placing the Ocean
                    else {
                        tile.SetItem(home_region.standard_liquid);
                    }
                }
                else if (region_class.id == 3) // Beach Class
                {
                    // Orientating the Beach/Ocean
                    tile.SetItem(home_region.standard_block);
                }
                else // Standard Class
                {
                    tile.SetItem(home_region.standard_block);
                }

                // Foilage Rule
                // Checking if our Map has a tile associated with the above position, and if so, that it has no tile generated associated with it.
                Vector3 abovepos = new Vector3(tile.body.transform.position.x, tile.body.transform.position.y + 1, tile.body.transform.position.z);
                if (home_region.standard_block != home_region.standard_foilage && CoordinateGraph.ContainsKey(abovepos) && !Tiles.ContainsKey(abovepos))
                {
                    SpreadFoilage(home_region.standard_foilage, tile);
                }
            }
        }

        /// <summary>
        /// Helper function for flaura that recursively calls the initial block to be changed to specific Flaura Item,
        /// and if dice rolls succeeds, spreads to the tile below it.
        /// </summary>
        /// <param name="foilage">Foilage Item</param>
        /// <param name="tile">Tile to spread to/from</param>
        /// <param name="odds">Odds of spreading</param>
        private int SpreadFoilage(Item foilage, Tile tile, float odds = 100)
        {
            if (tile.GetItem() is Liquid) { return 0;  }
            // Initializing variables
            Vector3 pos = tile.body.transform.position;

            // Set the Tile to Flaura
            tile.SetItem(foilage);

            if (Random.Range(1f, 100f) <= odds) // Roll a dice based on odds, on success spread
            {
                // Get position of tile below current one
                Vector3 belowpos = new Vector3(pos.x, pos.y - 1, pos.z);
                // Checking if there is a tile in our world located just below this tile.
                // Then checking that the tile to spread to is in the same biome/region as the original.
                if (CoordinateGraph.ContainsKey(belowpos) && Tiles.ContainsKey(belowpos) && Tiles[belowpos]!=null && Tiles[belowpos].home_region==tile.home_region && Tiles[belowpos].home_biome == tile.home_biome)
                {
                    // Spread the foilage, but with reduced odds of spreading
                    SpreadFoilage(foilage, Tiles[belowpos], odds -= 10);
                }
            }

            return 1;
        }

        /// <summary>
        /// Function that when called clears the current terrain
        /// </summary>
        public void Clear()
        {
            // Deletes the Map
            Object.Destroy(WorldObject);
        }
    }
}
