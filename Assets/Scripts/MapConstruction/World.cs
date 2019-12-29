using Assets.Scripts.Information.Items;
using Assets.Scripts.Information.Map;
using Assets.Scripts.Knowledge_Base;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Scripts.MapConstruction
{
    class World
    {
        // Charateristics
        private int _width;
        private int _height;
        private float _scale;
        private int _octaves;
        public GameObject WorldObject;
        private KnowledgeBase knowledgebase;

        // Setting variables for Map construction
        private float skyheight = 0.95f;
        private float voidheight = 0.03f;
        // Displacement is a variable that determines how sharp biomes and regions change.
        private int displacement = 2;
        // region height paramaters
        private float surfaceheight = 0.85f;
        private float hellheight = 0.15f;

        // placeholders for information used in the World generating process
        private Dictionary<Vector3, float> CoordinateGraph;
        private Dictionary<Vector3, Tile> TileMapping;

        /// <summary>
        /// Initializing World : Its Characteristics and Terrain
        /// </summary>
        /// <param name="width">Width</param>
        /// <param name="height">Height</param>
        /// <param name="scale">Scale</param>
        /// <param name="octaves">Octaves</param>
        public World(KnowledgeBase kb, int width, int height, float scale, int octaves = 3)
        {
            // initializing World's characteristics
            _width = width;
            _height = height;
            _scale = scale;
            _octaves = octaves;

            // Creating the coordinate Graph to use for Building the World
            AssignLocations();
            // Building the World using the CoordinateGraph generated in the previous function
            BuildWorld(kb);
        }

        /// <summary>
        /// Function that when called generates this class's Coordinate Graph (Vector3 position to Float noise).
        /// </summary>
        private void AssignLocations()
        {
            // initialzing variables
            CoordinateGraph = new Dictionary<Vector3, float>();
            float offset = Random.Range(0f, 10000f); // This controls why maps look different each time, it's a different location on the generated perlin map.

            // Generating the Noise and Coordinate Map
            for (int x = 1; x < _width; x++)
            {
                for (int y = 1; y < _height; y++)
                {
                    Vector3 newpos = new Vector3(x, y, 0);

                    float CompleteNoise = 0;

                    for (int o = 0; o < _octaves; o++)
                    {
                        float xCoord = x * _scale;
                        float yCoord = y * _scale;
                        float Noise = Mathf.PerlinNoise(xCoord + offset, yCoord + offset);
                        CompleteNoise += Noise;
                    }

                    CoordinateGraph.Add(newpos, CompleteNoise);
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
            TileMapping = new Dictionary<Vector3, Tile>();

            // Initial loop placing initial landmass down, and assigning tiles to biomes and regions
            foreach (Vector3 pos in CoordinateGraph.Keys)
            {
                // initializing variables
                float tile_noise = CoordinateGraph[pos];
                
                // Conditional for when a Tile shouldn't exist in this position
                if ( ((pos.y > _height*skyheight) || 
                    (tile_noise < 0.9)
                   )
                    && (pos.y > _height * voidheight))
                {
                    continue;
                }

                Tile tile = new Tile(pos, this);
                TileMapping.Add(pos, tile);

                // Setting the Surface Region
                if ((pos.y >= _height*surfaceheight + Random.Range(-displacement, displacement)))
                {
                    tile.home_region = kb.regions.Single(item => item.id == 0);
                }

                // Checking the Core Region
                else if ((pos.y <= _height * hellheight + Random.Range(-displacement, displacement)))
                {
                    tile.home_region = kb.regions.Single(item => item.id == 999);
                    // Check if in the Lava Lake Biome
                    if (pos.y <= _height * voidheight)
                    {
                        // Lava Lake Biome within Core Region
                        tile.home_biome = kb.biomes.Single(item => item.id == 1);
                    }
                }
            }

            //
            foreach(Tile tile in TileMapping.Values)
            {
                if (tile.home_region==null && tile.home_biome==null)
                {
                    continue;
                }

                var home_region = (tile.home_biome != null ? tile.home_biome : tile.home_region);

                // Checking if our Map has a tile associated with the above position, and if so, that it has no tile generated associated with it.
                Vector3 abovepos = new Vector3(tile.body.transform.position.x, tile.body.transform.position.y + 1, tile.body.transform.position.z);
                if ( home_region.standard_block != home_region.standard_foilage && CoordinateGraph.ContainsKey(abovepos) && !TileMapping.ContainsKey(abovepos) )
                {
                    SpreadFoilage(home_region.standard_foilage, tile);
                }
                else
                {
                    tile.SetItem(home_region.standard_block);
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
        private void SpreadFoilage(Item foilage, Tile tile, float odds = 100)
        {
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
                if (CoordinateGraph.ContainsKey(belowpos) && TileMapping.ContainsKey(belowpos) && TileMapping[belowpos]!=null && TileMapping[belowpos].home_region==tile.home_region && TileMapping[belowpos].home_biome == tile.home_biome)
                {
                    // Spread the foilage, but with reduced odds of spreading
                    SpreadFoilage(foilage, TileMapping[belowpos], odds -= 10);
                }
            }
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
