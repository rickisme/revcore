using Data.Enums;
using Data.Interfaces;
using Data.Structures.World;
using Data.Structures.World.Party;

namespace Data.Structures.Player
{
    public class Player : Creature.Creature
    {
        public Creature.Creature ObservedCreature = null;

        public PlayerData PlayerData;

        public IVisible Visible;

        public IConnection Connection;

        public IController Controller;

        public Account.Account Account { get; set; }

        public int PlayerId;

        public int ServerId { get; set; }

        public int Level = 1;

        public int JobLevel = 1;

        public long Exp = 0;

        public int SkillPoint = 0;

        public int AbilityPoint = 0;

        public int AscensionPoint = 0;

        public int CurrentAscensionPoint = 0;

        public int HonorPoint = 0;

        public int KarmaPoint = 0;

        public int DPoint = 0;

        public int CraftType = 0;

        public int CraftLevel = 0;

        public int CraftExp = 0;

        public bool IsRage = false;

        public Storage Inventory = new Storage { StorageType = StorageType.Inventory };

        public Abilities Abilities = new Abilities();

        public Abilities AscensionAbilities = new Abilities();

        public Skills PassiveSkills = new Skills();

        public Skills Skills = new Skills();

        public Skills AscensionSkills = new Skills();

        public Crafts Crafts = new Crafts();

        public Quests Quests = new Quests();

        public Party Party;

        public PrivateShop PrivateShop;

        public WorldPosition ClosestBindPoint = null;

        public PlayerMode PlayerMode = PlayerMode.Normal;

        public Settings Settings = new Settings();

        public Player()
        {
            PlayerData = new PlayerData();
            Position = new WorldPosition();
        }

        public override long UID
        {
            get { return Account.SessionID; }
        }

        public override int GetLevel()
        {
            return Level;
        }

        public int GetJobLevel()
        {
            return JobLevel;
        }
    }
}
