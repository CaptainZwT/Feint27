

namespace Assets.Scripts.Information.Quests
{
    class Quest
    {
        public int ID;
        public string name;
        public bool completed = false;

        public Quest(int _ID, string _name)
        {
            ID = _ID;
            name = _name;        }
    }
}
