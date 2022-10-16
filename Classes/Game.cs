using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.CompilerServices;
using System.Runtime.Intrinsics.X86;
using System.Text;
using System.Threading.Tasks;

namespace Inlämningsuppgift3.Classes
{
    public class Game
    {
        public Player NewPlayer { get; set; }
        public List<Room> Rooms { get; set; }
        public Room CurrentRoom { get; set; }

        public bool GameRunning = true;

        public Game()
        {
            Rooms = new List<Room>();
            Room startingRoom = new Room("first room", InformationRooms.GetRoomDescriptions(0), InformationRooms.GetAllItemsFirstRoom(), 0, 0);
            Room leftRoom = new Room("left room", InformationRooms.GetRoomDescriptions(1), InformationRooms.GetAllItemsLeftRoom(), 0, -1);
            Room rightRoom = new Room("right room", InformationRooms.GetRoomDescriptions(2), InformationRooms.GetAllItemsRightRoom(), 0, 1);
            Room upperRoom = new Room("upper room", InformationRooms.GetRoomDescriptions(3), InformationRooms.GetAllItemsUpperRoom(), 1, 0);
            Rooms.Add(startingRoom);
            Rooms.Add(leftRoom);
            Rooms.Add(rightRoom);
            Rooms.Add(upperRoom);
            CurrentRoom = startingRoom;
            NewPlayer = new Player();
        }
        public void StartGame()
        {
            if (GameRunning)
            {
                PrintStartText();
            }
        }
        public void PrintStartText()
        {
            Console.WriteLine("\n\t\t\t   Welcome to THE GAME, please read the following instructions:\n");
            HelpText();
            bool userInputOk = false;
            do
            {
                Console.Write("Enter your name to start the game: ");
                NewPlayer.Name = Console.ReadLine();
                if (NewPlayer.Name != "")
                {
                    userInputOk = true;
                    Console.Clear();
                    StartRoomText();
                }
                else
                {
                    Console.WriteLine("Not a valid input, try again\n");
                }
            }
            while (userInputOk == false);
        }
        public void HelpText()
        {
            Console.WriteLine("---------------------------------------------------------------------------------------------------------------------");
            Console.WriteLine("Type NORTH/N, SOUTH/S, EAST/E or WEST/W to move from room to room");
            Console.WriteLine("Type PICK UP to pick up items");
            Console.WriteLine("Type LOOK if you want to inspect an item");
            Console.WriteLine("Type DROP if you want to drop an item");
            Console.WriteLine("Type USE if you want to use an item");
            Console.WriteLine("Type BAG to get the items you have");
            Console.WriteLine("Type ROOM if you want to get the room description again");
            Console.WriteLine("Type HELP if you want to see this information again");
            Console.WriteLine("---------------------------------------------------------------------------------------------------------------------");
        }
        public void StartRoomText()
        {
            Console.WriteLine("\nYou wake up disoriented and notice a small flashlight on the floor.");
            Console.WriteLine("You have a feeling you might need it later and pick it up.");
            Console.WriteLine($"\n{CurrentRoom.Description}");
            Console.WriteLine("\nWhat is your next move?:");
            GameNextMove();
        }
        public void GameNextMove()
        {
            if (CurrentRoom.Name != "upper room")
            {
                Console.Write(" > ");
                Move(Console.ReadLine());
                Console.WriteLine();
            }
            else if (GameRunning)
            {
                Console.Clear();
                EndRoom();
            }
        }
        public void Move(string choice)
        {
            try
            {
                if (choice.ToLower().Contains("west") || choice.ToLower() == "w")
                {
                    if (CurrentRoom.Name == "first room" || CurrentRoom.Name == "right room")
                    {
                        NewPlayer.PlayerPosX = 0;
                        NewPlayer.PlayerPosY -= 1;
                        RoomUppdate(NewPlayer.PlayerPosX, NewPlayer.PlayerPosY);
                        GameNextMove();
                    }
                    if (CurrentRoom.Name == "left room")
                    {
                        Console.WriteLine("You can't go that way.\n");
                        GameNextMove();
                    }

                }
                if (choice.ToLower().Contains("east") || choice.ToLower() == "e")
                {
                    if (CurrentRoom.Name == "first room" || CurrentRoom.Name == "left room")
                    {
                        NewPlayer.PlayerPosX = 0;
                        NewPlayer.PlayerPosY += 1;
                        RoomUppdate(NewPlayer.PlayerPosX, NewPlayer.PlayerPosY);
                        GameNextMove();
                    }
                    if (CurrentRoom.Name == "right room")
                    {
                        Console.WriteLine("You can't go that way.\n");
                        GameNextMove();
                    }

                }
                if (choice.ToLower() == "north" || choice.ToLower() == "go north" || choice.ToLower() == "n")
                {
                    if (CurrentRoom.Name == "first room")
                    {
                        var lockedDoor = CurrentRoom.ItemsInRoom.Where(i => i.Name.ToLower().Contains("north door")).SingleOrDefault();

                        if (lockedDoor.Useable == true)
                        {
                            NewPlayer.PlayerPosX += 1;
                            NewPlayer.PlayerPosY = 0;
                            RoomUppdate(NewPlayer.PlayerPosX, NewPlayer.PlayerPosY);
                            GameNextMove();
                        }
                        else
                        {
                            Console.WriteLine("This door is locked, you need a key to go through here.\n");
                            GameNextMove();
                        }
                    }
                    if (CurrentRoom.Name == "left room" || CurrentRoom.Name == "right room")
                    {
                        Console.WriteLine("You can't go that way.\n");
                        GameNextMove();
                    }
                }
                if (choice.ToLower().Contains("south") || choice.ToLower() == "s")
                {
                    if (CurrentRoom.Name == "upper room")
                    {
                        NewPlayer.PlayerPosX -= 1;
                        NewPlayer.PlayerPosY = 0;
                        RoomUppdate(NewPlayer.PlayerPosX, NewPlayer.PlayerPosY);
                        GameNextMove();
                    }
                    else
                    {
                        Console.WriteLine("You can't go that way.\n");
                        GameNextMove();
                    }
                }
                if (choice.ToLower().Contains("look"))
                {
                    Console.WriteLine("What item do you want to take a closer look at?:");
                    Console.Write(" > ");
                    string userInputItem = Console.ReadLine();
                    var result = (from item in CurrentRoom.ItemsInRoom
                                    where item.Name.ToLower() == userInputItem.ToLower()
                                    select item).SingleOrDefault();
                    if (result != null)
                    {
                        Console.WriteLine($"{result.Description}\n");
                        GameNextMove();
                    }
                    else
                    {
                        var result2 = (from item in NewPlayer.ItemBag
                                        where item.Name.ToLower() == userInputItem.ToLower()
                                        select item).SingleOrDefault();
                        if (result2 != null)
                        {
                            Console.WriteLine($"{result2.Description}\n");
                            GameNextMove();
                        }
                        else
                        {
                            Console.WriteLine("Invalid choice.\n");
                            GameNextMove();
                        }
                    }
                }
                if (choice.ToLower().Contains("room"))
                {
                    Console.WriteLine(CurrentRoom.Description);
                    Console.WriteLine("Items and doors in the room:");
                    foreach (var item in CurrentRoom.ItemsInRoom)
                    {
                        Console.WriteLine($"* {item.Name}");
                    }
                    Console.WriteLine();
                    GameNextMove();
                }
                if (choice.ToLower() == "pick up")
                {
                    Console.WriteLine("\nWhat item do you want to pick up?:");
                    Console.Write(" > ");
                    string userInputItem = Console.ReadLine();
                    var result = (from item in CurrentRoom.ItemsInRoom
                                    where item.Name.ToLower() == userInputItem.ToLower()
                                    select item).SingleOrDefault();
                    if (result != null)
                    {
                        NewPlayer.PickUpItem(result, CurrentRoom);
                        GameNextMove();
                    }
                    else
                    {
                        Console.WriteLine("Invalid choice.\n");
                        GameNextMove();
                    }
                }
                if (choice.ToLower().Contains("drop"))
                {
                    Console.WriteLine("\nWhat item do you want to drop?:");
                    Console.Write("> ");
                    string userInputItem = Console.ReadLine();
                    var result = (from item in NewPlayer.ItemBag
                                    where item.Name.ToLower() == userInputItem.ToLower()
                                    select item).SingleOrDefault();
                    if (result != null)
                    {
                        NewPlayer.DropItem(result, CurrentRoom);
                        GameNextMove();
                    }
                    else
                    {
                        Console.WriteLine("Invalid choice.\n");
                        GameNextMove();
                    }
                }
                if (choice.ToLower().Contains("bag"))
                {
                    Console.WriteLine("Your bag contains:");
                    foreach (var item in NewPlayer.ItemBag)
                    {
                        Console.WriteLine(item.Name);
                    }
                    Console.WriteLine();
                    GameNextMove();

                }
                if (choice.ToLower() == "use")
                {
                    Item.UseItem(NewPlayer, CurrentRoom);
                    GameNextMove();
                }
                if (choice.ToLower().Contains("help"))
                {
                    HelpText();
                    GameNextMove();
                }
                if (GameRunning == false)
                {

                }
                else
                {
                    Console.WriteLine("Not a valid choice.\n");
                    GameNextMove();
                }                
            }
            catch(Exception ex)
            {
                Console.WriteLine("Invalid input.\n");
            }
        }
        public void CurrentRoomActions(string currentRoom)
        {
            Console.WriteLine("---------------------------------------------------------------------------------------------------------------------");
            Console.WriteLine($"{CurrentRoom.Description}\n");
            GameNextMove();
        }
        public void RoomUppdate(int posX, int posY)
        {
            foreach (var room in Rooms)
            {
                if (room.RoomPosX == posX && room.RoomPosY == posY)
                {
                    CurrentRoom = room;
                }
            }
            if (CurrentRoom.Name.ToLower() != "upper room")
            {
                CurrentRoomActions(CurrentRoom.Name);
            }
        }
        public void EndRoom()
        {
            Console.WriteLine("-------------------------------------------------------------------------\n");
            Console.WriteLine("As you enter the room the door shuts behind you and it's too dark to see.");
            Console.WriteLine("\n-------------------------------------------------------------------------");


            var result = NewPlayer.ItemBag.Where(i => i.Name.ToLower() == "flashlight").SingleOrDefault();
            try
            {
                if (result != null)
                {
                    Console.WriteLine("You reach for your flashlight, do you want to use it? Y/N:");
                    Console.Write("> ");
                    string choice = Console.ReadLine();

                    if (choice.ToLower() == "yes" || choice.ToLower() == "y")
                    {
                        Console.WriteLine("You turn on your flashlight.\n");
                        Console.WriteLine($"{CurrentRoom.Description}");
                        Console.WriteLine($"\nCongratulation {NewPlayer.Name} you beat the game!");
                        GameRunning = false;
                    }
                    if (choice.ToLower() == "no" || choice.ToLower() == "n")
                    {
                        Console.WriteLine("In the moment of choice you accidentally drop the flashlight, it hits the floor and is now useless.");
                        Console.WriteLine("You feel overwhelmed by the darkness surrounding you.");
                        Console.WriteLine($"\nUnfortunately you didn't beat the game.");
                        GameRunning = false;
                    }
                }
                else
                {
                    Console.WriteLine("\nAs you reach for your flashlight you remembered that you left it in another room.");
                    Console.WriteLine("You feel overwhelmed by the darkness surrounding you.");
                    Console.WriteLine($"\nUnfortunately you didn't beat the game.");
                    GameRunning = false;
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine("Invalid input");
            }
        }        
    }    
}
