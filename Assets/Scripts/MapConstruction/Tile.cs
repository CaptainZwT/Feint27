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
        //


        /// <summary>
        /// Initialize a Tile : It's Body and Characteristics
        /// </summary>
        /// <param name="pos">Position</param>
        /// <param name="parent">Parent</param>
        public Tile(Vector3 pos, World parent)
        {
            // Creating initial Object
            body = GameObject.CreatePrimitive(PrimitiveType.Cube);
            // Setting Dimensions
            body.transform.localScale = new Vector3(1, 1, 1);
            // Setting Position
            body.transform.position = pos;
            // Setting World to be its parent
            _world = parent;
            body.transform.parent = _world.WorldObject.transform;
        }

        /// <summary>
        /// Initializes Item that Tile is associated with, allowing for the Graphics to be rendered accordingly.
        /// </summary>
        /// <param name="item">Associated Item</param>
        public void SetItem(Item item)
        {
            _item = item;

            //Get the Renderer component from the new cube
            var cubeRenderer = body.GetComponent<Renderer>();

            //Call SetColor using the shader property name "_Color" and setting the color to red
            cubeRenderer.material.SetColor("_Color", item.tilecolor);
        }

        public Item GetItem()
        {
            return _item;
        }

    }
}
