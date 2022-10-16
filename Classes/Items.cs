using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inlämningsuppgift3.Classes
{
    public class Item
    {
        public string Name { get; set; }
        public bool Useable { get; set; }
        public bool Used { get; set; }
        public string Description { get; set; }
        public bool PickUp { get; set; }


        public Item(string name, bool useable, bool used, string description, bool pickUp)
        {
            Name = name;
            Useable = useable;
            Used = used;
            Description = description;
            PickUp = pickUp;
        }
        public Item()
        {

        }
        public static void UseItem(Player player, Room currentRoom)
        {
            try
            {
                Console.WriteLine("Which item do you want to use?:");
                Console.Write(" > ");
                string item = Console.ReadLine();

                var userItem = player.ItemBag.Where(i => i.Name.ToLower() == item.ToLower()).SingleOrDefault();
                var roomItem = new Item();

                if (userItem != null)
                {
                    if (userItem.Name.ToLower() == "flashlight")
                    {
                        Console.WriteLine("You turn on the flashlight, it flickers, better save the battery so you put it away again.\n");
                        return;
                    }
                    if(userItem.Name.ToLower() == "paper")
                    {
                        Console.WriteLine($"{userItem.Description}\n");
                        return;
                    }
                    else
                    {
                        Console.WriteLine("Where do you want to use it?:");
                        Console.Write(" > ");
                        string whereToUse = Console.ReadLine();

                        roomItem = currentRoom.ItemsInRoom.Where(i => i.Name.ToLower() == whereToUse.ToLower()).SingleOrDefault();
                    }
                }
                else
                { 
                    roomItem = currentRoom.ItemsInRoom.Where(i => i.Name.ToLower() == item.ToLower()).SingleOrDefault();
                }
                if (roomItem.Name.ToLower() == "box")
                {
                    if (roomItem.Used == false)
                    {
                        Console.WriteLine("The box is locked, enter the right code:");
                        Console.Write(" > ");
                        int code = int.Parse(Console.ReadLine());
                        int rightCode = 743;

                        if (code == rightCode)
                        {
                            Item newItem = new Item("Key", true, false, "big old key", true);
                            Console.WriteLine("The box contains a key.");
                            Console.WriteLine("The key is now added to your inventory.\n");
                            player.ItemBag.Add(newItem);
                            roomItem.Used = true;
                            roomItem.Name = "Empty box";
                            roomItem.Useable = false;
                            return;
                        }
                        else
                        {
                            Console.WriteLine("Wrong combination\n");
                            return;
                        }
                    }
                    else
                    {
                        Console.WriteLine($"{roomItem.Description}. Now it's empty.\n");
                        return;
                    }
                }
                if(roomItem.Name.ToLower() == "paper")
                {
                    Console.WriteLine($"{roomItem.Description}\n");
                    return;
                }
                if (roomItem.Name.ToLower() == "north door")
                {
                    if (userItem == null)
                    {
                        Console.WriteLine("This door is locked, you need a key to go through here.\n");
                        return;
                    }
                    else if (roomItem.Useable == false && userItem.Name.ToLower() == "key")
                    {
                        userItem.Used = true;
                        roomItem.Useable = true;
                        Console.WriteLine("You use the key on the door and it unlocks.");
                        Console.WriteLine("The key breaks in the lock and you can't get it out.\n");
                        player.ItemBag.Remove(userItem);
                        return;
                    }
                }
                if (roomItem.Name.ToLower() == "west door" && currentRoom.Name == "first room" || roomItem.Name.ToLower() == "east door" && currentRoom.Name == "first room" ||
                    roomItem.Name.ToLower() == "door" && currentRoom.Name == "right room" || roomItem.Name.ToLower() == "door" && currentRoom.Name == "left room")
                {
                    Console.WriteLine("The door is open, you can just walk through.\n");
                }
                else
                {
                    Console.WriteLine("You can't use this item.\n");
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine("Invalid input\n");
                Console.WriteLine("What do you want to do?:");
            }

        }
    }
}
