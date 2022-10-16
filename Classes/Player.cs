using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inlämningsuppgift3.Classes
{
    public class Player
    {
        public string Name { get; set; }
        public List<Item> ItemBag { get; set; }
        public int PlayerPosX { get; set; }
        public int PlayerPosY { get; set; }

        public Player()
        {            
            PlayerPosX = 0;
            PlayerPosY = 0;
            ItemBag = new List<Item>();
            ItemBag.Add(new Item("Flashlight", true, false, "A small pocket size flashlight", true));
        }
        public void PickUpItem(Item item, Room currentRoom)
        {
            if (item.Useable && item.PickUp == true)
            {
                ItemBag.Add(item);
                currentRoom.ItemsInRoom.Remove(item);
                Console.WriteLine($"\nYou have added the {item.Name.ToLower()} to your inventory.");
            }
            if(item.Useable && item.PickUp == false)
            {
                Console.WriteLine($"You can't put it in your inventory but maybe you can use it for something.\n");
            }
            else if(item.PickUp == false)
            {
                Console.WriteLine($"You can't pick this up.\n");
            }                
        }
        public void DropItem(Item item, Room Currentroom)
        {
            ItemBag.Remove(item);
            Currentroom.ItemsInRoom.Add(item);
            Console.WriteLine($"You no longer have {item.Name} in your inventory.\n");
        }

    }
}
