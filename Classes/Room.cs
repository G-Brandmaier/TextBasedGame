using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inlämningsuppgift3.Classes
{
    public class Room
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public List<Item> ItemsInRoom { get; set; }
        public int RoomPosX { get; set; }
        public int RoomPosY { get; set; }


        public Room(string name, string description, List<Item> itemsInRoom, int posX, int posY)
        {
            Name = name;
            Description = description;
            ItemsInRoom = itemsInRoom;
            RoomPosX = posX;
            RoomPosY = posY;
        }
    }
}
