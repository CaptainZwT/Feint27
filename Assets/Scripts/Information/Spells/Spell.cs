using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Information.Spells
{
    class Spell
    {
        // initializing variables
        public int ID;
        private string name;
        private float cast_time;
        
        /// <summary>
        /// Initializes a Spell class, with the following characteristics
        /// </summary>
        /// <param name="_ID">ID</param>
        /// <param name="_name">Name</param>
        /// <param name="_cast_time">Cast Time</param>
        public Spell(int _ID, string _name, float _cast_time)
        {
            ID = _ID;
            name = _name;
            cast_time = _cast_time;
        }
    }
}
