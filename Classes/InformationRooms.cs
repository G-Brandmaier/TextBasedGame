using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inlämningsuppgift3.Classes
{
    public static class InformationRooms
    {
        public static string GetRoomDescriptions(int choice)
        {
            List<string> roomDescriptions = new List<string>
            {
                "A small dark room with no windows. There is an old looking table with a small lamp on it in the middle and 3 doors, one infront of me and 2 on the sides.",
                "Another small room with a boarded window and only lit up by one hanging bulb from the ceiling. What looks like to be a broken chair in the corner also hides a strange looking box.",
                "This room also have no windows, the onlys things there is a big empty bookshelf with a piece of paper in it and next to it an old mouldy cardboard box.",
                "Another small room with nothing in it except wornout walls, you see a pile of wooden planks in the corner of the room. " +
                "You move them and find a hidden tunnel that leads to your escape."
            };
            return roomDescriptions[choice].ToString();
        }
        public static List<Item> GetAllItemsFirstRoom()
        {
            List<Item> itemsInRoom = new List<Item>();

            itemsInRoom.Add(new Item("Lamp", false, false, "An old rusty lamp.", false));
            itemsInRoom.Add(new Item("Table", false, false, "A small wodden table with cracks in it.", false));
            itemsInRoom.Add(new Item("North door", false, false, "A rather big wooden door, looks like it needs a key to open.", false));
            itemsInRoom.Add(new Item("West door", true, false, "A narrow wooden door without a handle.", false));
            itemsInRoom.Add(new Item("East door", true, false, "A narrow wooden door.", false));

            return itemsInRoom;

        }
        public static List<Item> GetAllItemsLeftRoom()
        {
            List<Item> itemsInRoom = new List<Item>();

            itemsInRoom.Add(new Item("Chair", true, false, "An old wooden chair, look like it's broken.", false));
            itemsInRoom.Add(new Item("Box", true, false, "A rather small box with a code lock on it.", false));
            itemsInRoom.Add(new Item("Bulb", false, false, "A dusty hanging bulb.", false));
            itemsInRoom.Add(new Item("Door", true, false, "A narrow wooden door without a handle.", false));

            return itemsInRoom;

        }
        public static List<Item> GetAllItemsRightRoom()
        {
            List<Item> itemsInRoom = new List<Item>();

            itemsInRoom.Add(new Item("Bookshelf", false, false, "A big bookshelf with a thick layer of dust on it, there is nothing in it except for some pieces of paper.", false));
            itemsInRoom.Add(new Item("Paper", true, false, "A torn book page with vague numbers written on it...743.", true));
            itemsInRoom.Add(new Item("Old box", false, false, "An old cardboard box with some stains on it, almost looks like blood.", false));
            itemsInRoom.Add(new Item("Door", true, false, "A narrow wooden door.", false));

            return itemsInRoom;

        }
        public static List<Item> GetAllItemsUpperRoom()
        {
            List<Item> itemsInRoom = new List<Item>();

            itemsInRoom.Add(new Item("Door", false, false, "A rather big wooden door, it's stuck and can't be opened.", false));

            return itemsInRoom;

        }
    }
}
