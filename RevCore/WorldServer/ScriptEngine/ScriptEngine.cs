using Data.Enums;
using Data.Structures.Player;
using Global.Interfaces;
using Global.Logic;
using NLua;
using System;
using System.Collections.Generic;
using System.IO;
using WorldServer.OuterNetwork.Write;

namespace WorldServer.ScriptEngine
{
    internal class ScriptEngine : Global.Global, IScriptEngine
    {
        public LuaFunction MonsterDeadEvent;
        public LuaFunction OpenItemEvent;
        public LuaFunction UseItemEvent;
        public Dictionary<int, LuaFunction> QuestEventLists = new Dictionary<int, LuaFunction>();
        public Lua Lua;
        public string LuaPath;

        public ScriptEngine()
        {
            Init();
            LuaPath = Path.GetFullPath("Scripts/");
            GetUrlDirectory(LuaPath);
            Event();
        }

        public void Init()
        {
            Lua = new Lua();
            Lua.RegisterFunction("SendMessage", this, this.GetType().GetMethod("SendMessage"));
            Lua.RegisterFunction("RemoveItem", this, this.GetType().GetMethod("RemoveItem"));
            Lua.RegisterFunction("RemoveItemById", this, this.GetType().GetMethod("RemoveItemById"));
            Lua.RegisterFunction("UpdateHpAndMp", this, this.GetType().GetMethod("UpdateHpAndMp"));
        }

        public void Action()
        {

        }

        public void SendWelComeMessage(Player player)
        {
            LuaFunction function = (Global.Global.ScriptEngine as ScriptEngine).Lua.GetFunction("WelComeMessage");
            if (function != null)
            {
                function.Call(player, ChatType.Announce);
            }
        }

        public void UseItem(Player player, long itemId, int position, long itemCount)
        {
            UseItemEvent.Call(player, itemId, position, itemCount);
        }

        public void SendMessage(Player player, string message, ChatType chatType)
        {
            var con = player.Connection;
            new SpChatMessage(message, chatType).Send(con);
        }

        public void RemoveItem(Player player, int slot, int counter)
        {
            Global.Global.StorageService.RemoveItem(player, player.Inventory, slot, counter);
        }

        public void RemoveItemById(Player player, int itemId, int counter)
        {
            Global.Global.StorageService.RemoveItemById(player, player.Inventory, itemId, counter);
        }

        public void UpdateHpAndMp(Player player, int hp, int mp) 
        {
            player.LifeStats.Hp += hp;
            if (player.LifeStats.Hp > player.MaxHp)
            {
                player.LifeStats.Hp = player.MaxHp;
            }

            player.LifeStats.Mp += mp;
            if (player.LifeStats.Mp > player.MaxMp)
            {
                player.LifeStats.Mp = player.MaxMp;
            }

            CreatureLogic.UpdateCreatureStats(player);
        }

        public void Event()
        {
            this.UseItemEvent = Lua.GetFunction("UseItmeTrigGer");
            this.OpenItemEvent = Lua.GetFunction("OpenItmeTrigGer");
            this.MonsterDeadEvent = Lua.GetFunction("DestroyMonster");
        }

        public void SetUrlFile(string filepath)
        {
            string extension = Path.GetExtension(filepath);
            if (extension == ".lua")
            {
                try
                {
                    Lua.DoFile(filepath);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }

        public void GetUrlDirectory(string filepath)
        {
            if (!Directory.Exists(filepath))
            {
                return;
            }
            string[] files = Directory.GetFiles(filepath);
            string[] array = files;
            for (int i = 0; i < array.Length; i++)
            {
                string urlFile = array[i];
                try
                {
                    this.SetUrlFile(urlFile);
                }
                catch (Exception value)
                {
                    Console.Write(value);
                }
            }
            this.GetUrlDirectoryS(filepath);
        }

        public void GetUrlDirectoryS(string filepath)
        {
            string[] directories = Directory.GetDirectories(filepath);
            string[] array = directories;
            for (int i = 0; i < array.Length; i++)
            {
                string string_ = array[i];
                this.GetUrlDirectory(string_);
            }
        }
    }
}
