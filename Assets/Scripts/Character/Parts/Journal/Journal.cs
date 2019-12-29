using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Character.Parts.Journal
{
    class Journal
    {
        private QuestLog _questlog;
        private Pouch _pouch;

        /// <summary>
        /// Initializes a Journal with an empty QuestLog and Pouch.
        /// </summary>
        public Journal()
        {
            _questlog = new QuestLog();
            _pouch = new Pouch();
        }
    }
}
