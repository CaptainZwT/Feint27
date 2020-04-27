using Assets.Scripts.Information.Items;
using Assets.Scripts.Information.Map;
using Assets.Scripts.MapConstruction;
using UnityEngine;

namespace Assets.Scripts
{
    class Tile
    {
        // Charateristics of a Tile
        private Item _item; // Holds the Item that will drop from this Tile
        private World _world; // Holds the World parent of this Tile
        public GameObject body; // Holds the GameObject this Tile is associated with

        public Region home_region;
        public Biome home_biome;

        public Vector2 position;
        //


        /// <summary>
        /// Initialize a Tile : It's Body and Characteristics
        /// </summary>
        /// <param name="pos">Position</param>
        /// <param name="parent">Parent</param>
        public Tile(Vector2 pos, World parent)
        {
            home_biome = null;
            home_region = null;

            position = pos;
            _world = parent;
        }

        /// <summary>
        /// Initializes Item that Tile is associated with, allowing for the Graphics to be rendered accordingly.
        /// </summary>
        /// <param name="item">Associated Item</param>
        public void SetItem(Item item)
        {
            _item = item;
        }

        public Item GetItem()
        {
            return _item;
        }

        public void Render()
        {
            // Checking for defaul tiles
            if (_item == null)
            {
                if (home_biome != null)
                {
                    _item = home_biome.standard_block;
                }
                else
                {
                    _item = home_region.standard_block;
                }
            }

            // Creating initial Object
            body = GameObject.CreatePrimitive(PrimitiveType.Cube);
            // Setting Dimensions
            body.transform.localScale = new Vector3(1, 1, 0.1f);
            // Setting Position and Parent
            body.transform.position = position;
            body.transform.parent = _world.WorldObject.transform;

            //Get the Renderer component from the new cube
            var cubeRenderer = body.GetComponent<Renderer>();

            //Call SetColor using the shader property name "_Color" and setting the color to red
            cubeRenderer.material.SetColor("_Color", _item.tilecolor);
        }

    }
}
