using Data.Structures.World;
using Utilities;
using WorldServer.OuterNetwork.Write;

namespace WorldServer.Extensions
{
    public static class MapInstanceExtensions
    {
        public static void AddDrop(this MapInstance instance, Item item)
        {
            item.UID = instance.DropUID.GetNext();
            instance.Items.Add(item);

            new DelayedAction(() => instance.RemoveItem(item), 60000);
        }

        public static void RemoveItem(this MapInstance instance, Item item)
        {
            try
            {
                instance.Items.Remove(item);
                Global.Global.VisibleService.Send(item, new SpDropRemove(item));
                item.Release();
            }
            catch
            {
                //Already removed
            }
        }
    }
}
