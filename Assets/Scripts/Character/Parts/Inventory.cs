using Assets.Scripts.Information.Items;
using System.Collections.Generic;

// This will serve as the logic and information of the inventory slots

namespace Assets.Scripts.Character.Parts
{
    class Inventory
    {
        private List<Item> slots;

        /// <summary>
        /// Initializes Inventory with an empty list of Items.
        /// </summary>
        public Inventory()
        {
            slots = new List<Item>();
        }
    }
}
