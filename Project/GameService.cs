using System;
using System.Collections.Generic;
using CastleGrimtol.Project.Interfaces;
using CastleGrimtol.Project.Models;

namespace CastleGrimtol.Project
{
    public class GameService : IGameService
    {
        bool playing = true;
        public Room CurrentRoom { get; set; }
        public Player CurrentPlayer { get; set; }

        internal void Run()
        {

            Setup();
            StartGame();

        }

        public void GetUserInput()
        {
            {
                string input = System.Console.ReadLine().ToLower();
                string[] arrChoice = input.Split(" ");
                string action = arrChoice[0];
                string direction = "";
                if (arrChoice.Length > 1)
                {
                    direction = arrChoice[1];
                }
                switch (action)
                {
                    case "quit":
                        Quit();
                        break;
                    case "help":
                        Help();
                        break;
                    case "look":
                        Look();
                        break;
                    case "ride":
                        Ride(direction);
                        break;
                    case "inventory":
                        Inventory();
                        break;
                    case "take":
                        TakeItem(direction);
                        break;
                    case "use":
                        UseItem(direction);
                        break;
                    default:
                        System.Console.WriteLine("I dont understand m'lord");
                        break;
                }
            }
        }

        public void Ride(string direction)
        {
            //clear previous text
            System.Console.Clear();
            if (CurrentRoom.isComplete)
            {
                if (direction == "north" && CurrentRoom.Name.ToString() == "Bridge of Death")
                {
                    CurrentRoom = (Room)CurrentRoom.Exits[direction];
                    System.Console.WriteLine(@"
                
                     -|                  [-_-_-_-_-_-_-_-]                  |-
                     [-_-_-_-_-]          |             |          [-_-_-_-_-]
                      | o   o |           [  0   0   0  ]           | o   o |
                       |     |    -|       |           |       |-    |     |
                       |     |_-___-___-___-|         |-___-___-___-_|     |
                       |  o  ]              [    0    ]              [  o  |
                       |     ]   o   o   o  [ _______ ]  o   o   o   [     | ----__________
            _____----- |     ]              [ ||||||| ]              [     |
                       |     ]              [ ||||||| ]              [     |
                   _-_-|_____]--------------[_|||||||_]--------------[_____|-_-_
                ( (__________------------_____________-------------_________) )
                
                
                ");
                    System.Console.WriteLine("You have reached the castle of the Frenchman, Guy de Lombard. You call up to the soldier above you on the castle parapet to see if he will ask his master if he will accompany you on your quest for the holy grail. He responds, 'Well, I'll ask 'im, but I don't think 'e'll be very keen-- 'e's already got one, you see?' Astonished you implore him in the name of all that is holy to show you the grail. He responds 'Oh yes, it's ver' naahs you silly king. I blow my nose at you, you silly english kniggits *blow rasbery* your mother was a hamster and your father smelt of elderberris! ");
                }
                if (CurrentRoom.Name == "Castle of Guy de Lombard")
                {

                    System.Console.WriteLine("Sire, we have reached the end of our journey, surely the grail must  be in this castle. Let us draw our swords and take the fight to the frenchmen!");
                }
                else if (CurrentRoom.Exits.ContainsKey(direction))
                {
                    CurrentRoom = (Room)CurrentRoom.Exits[direction];
                }
                else
                {
                    System.Console.WriteLine("Sire, we are unable to ride in that direction!");
                }
                System.Console.WriteLine(CurrentRoom.Name.ToString());
            }
            else //Else = isCompleted flase AKA boss room
            {
                if (direction == "east")
                {
                    System.Console.WriteLine(" M'lord you are unable to proceed until the threat is eliminated, please 'look' around....");
                }
                if (direction == "west" && CurrentRoom.Exits.ContainsKey(direction))
                {
                    CurrentRoom = (Room)CurrentRoom.Exits[direction];
                    Look();
                }
            }
        }

        public void Help()
        {
            System.Console.Clear();
            //Objectives of game
            Console.WriteLine("You have had a vision that you were to seek the holy grail.");
            Console.WriteLine("Locate the Holy Grail to complete your quest!");
            //actions list
            Console.WriteLine("----- ACTION LIST -----");
            //navigation
            Console.WriteLine("To move in 'Grail' you must enter both the action 'Ride' as well as a direction");
            Console.WriteLine("ex: 'ride east'");
            //take item
            Console.WriteLine("To aquire an item in 'Grail' you must enter both the action 'Take' as well as the item's name");
            Console.WriteLine("ex:'take Excalibur'");
            //use item
            Console.WriteLine("To use an item in 'Grail' you must enter both the action 'Use' as well as the item's name");
            System.Console.WriteLine("For example:'use Excalibur");
            //look
            System.Console.WriteLine("Type 'look' to look around and gather additional observations about your surroundings");
            //inventory
            System.Console.WriteLine("Type 'inventory' to check your current item inventory, attempting to use an item you do not currently have may result in instant death!");
            System.Console.WriteLine("---Close Help ---");
            System.Console.WriteLine("Hit ENTER to return to game.");
        }

        public void Inventory()
        {
            CurrentPlayer.Inventory.ForEach(i =>
            {
                System.Console.WriteLine("You have: ");
                System.Console.WriteLine($"{i.Name}");
            });
        }

        public void Look()
        {
            if (CurrentRoom.Name == "Cliff of CaerBannog")
            {
                Console.WriteLine(CurrentRoom.Description.ToString());
                Console.WriteLine(@"
                
                                                   - __ -
                                                     \/
                                                  _  --_ _
                                                 |;|_|;|_|;|
                                                 \\.    .  /
                                                  \\:  .  /
                                                   ||:   |
                                                   ||:.  |
                                                   ||:  .|
                                                   ||:   |       \,/
                                                   ||: , |            /`\
                                                   ||:   |
                                                   ||: . |
                                            ___.   ||_   |
     ____--``    '--``__            __ ----`    ``---,___|_.           
-`--`                   `---__ ,--`'                        `_____-`
                
                
                
                
                
                ");
                Console.WriteLine("Reset Game? Y/N");
                GetUserInput();
                Reset();

            }
            Console.Clear();
            Console.WriteLine(CurrentRoom.Name.ToString());
            Console.WriteLine(CurrentRoom.Description.ToString());
            Console.WriteLine(@"
            What is your order M'lord?");
            GetUserInput();
        }

        public void Quit()
        {
            System.Console.WriteLine("The Quest has failed...");
            System.Console.WriteLine("You have abandoned your quest, please try again later!");
            playing = false;
        }

        public void Reset()
        {
            CurrentPlayer.Inventory = new List<Item>();
            Setup();
        }

        public void Setup()
        {
            //create rooms
            Room camelot = new Room("Camelot", "We’re Knights of the Round Table. We dance whene’er we’re able...On second thought , let’s not go to Camelot. ‘Tis a silly place. The only exit to the castle lies to the south. (Don't forget to 'TAKE' your sword 'EXCALIBUR'!)");
            Room village = new Room("Howden Village", "Upon exiting the main gate of the Camelot Castle you see Sir Bedivere who appears to be attempting to determine if a woman dressed (poorly) as a witch weighs the same as a duck? After asking him to join your quest he informs you that from the center of the village you can travel south to the home of the 'Shrubber' or east to the bridge leading to the Forrest of Brechfa,he does however warn you that the bridge is guarded by the 'Black Knight' and you may need to 'USE' 'EXCALIBUR' to defeat him. Sir Bedivere also warns that you may not get so many explicit instructions from this point onward, however you can still ask for 'HELP' at any time.");
            Room house = new Room("Home of Roger the Shrubber", " Upon entering the home the Shrubber you get a sense that the owner is not to be trifled with. A quick look around the room reveals that there are no other exits to the home. There are tables covered in tools and various other shrubological equipment. A man exits a back room and begins to speak. 'Even those who arrange and design shrubberies are under considerable economic stress at this period in history, but shrubberies are my trade. I am a shrubber. My name is Roger the Shrubber. I arrange, design, and sell shrubberies. Please take one [SHRUBBERY], it's dangerous to go alone!");
            Room bridge = new Room("Bridge of the Black Knight", "You arrive at the bridge outside of the village. As expected it is guarded by the infamous 'Black Knight'. Upon walking up to be bridge he states ' I move.. for no man...' A quick look around confirms that the only directions you are able to go are either back the way you came, or to the East... past the 'Black Knight'. ");
            Room forest = new Room("Forest of Brechfa", "The path into the forest is ever winding. It grows darker as you delve deeper into the trees. The foliage is too thick to venture off the path. And although it seems to wind endlessly as it becomes more and more narrow you are confident that you will reach the Plains of Caerbannog to seek out an Enchanter who may assist you on your Quest. Suddenly, without warning a group of suspiciously tall knights emerge from the woods and begin making a dreadful noise.'NIIIIIIIII!' The man who appears to be their leader is easily twice your height, and has large deer horns attached to his helm.'NIIIIIIIII!' Just as you begin to wonder if this noise will ever stop the leader begins to speak.The knight explains that they are the keepers of the sacred words 'Ni', 'Peng', and 'Neee-Wom', and they demand a 'sacrifice'.");
            Room plains = new Room("Plains of Caerbannog", "As you leave the forest you are greeted by desolate plains and dead foliage...It all appears to have been burned. As you survey the charred landscape you order Brother Maynard to setup his camp to the north on the ledge overlooking the plains. You see what appears to be the sea to the south under some sharp cliffs with what appears to be a grail on the horizon?!. It is at that point that you see a man recklessly shooting fireballs in various directions. He sees you in the distance and in and instant materializes at your side. 'There are some who call me... 'Tim'?' says the Enchanter. 'To the East there lies a cave-- the Cave of Caerbannog-- wherein, carved in mystic runes upon the very living rock, the last words of Olfin Bedwere of Rheged...make plain the last resting place of the most Holy Grail.' 'Follow... But! Follow only if ye be men of valour, for the entrance to this cave is guarded by a creature so foul, so cruel that no man yet has fought with it and lived! Bones of full fifty men lie strewn about its lair. So, brave knights, if you do doubt your courage or your strength, come no further, for death awaits you all with nasty, big, pointy teeth.'");
            Room ledge = new Room("Ledge of Caerbannog", "Upon reaching the ledge you enter the camp of Brother Maynard. You hear his other brothers as you approach. 'Pie Iesu domine, dona eis requiem. Pie Iesu domine, dona eis requiem.' Brother Maynard carries with him many sacred relics including the 'Holy Hand [GRENADE] of Antioch'.");
            Room cliff = new Room("Cliff of CaerBannog", "As you approach the cliffs edge you see what in retrospect has to be a grail shaped beacon. You conclude this as you fall to your imminent death upon the rocky shores below");
            Room cave = new Room("Cave of CaerBannog", "Tim says 'Behold the cave of Caerbannog!...There he is!...that's no ordinary rabbit!...That's the most foul, cruel, and bad-tempered rodent you ever set eyes on!. Look, that rabbit's got a vicious streak a mile wide! It's a killer!'He's got huge, sharp-- eh-- he can leap about-- look at the bones! ...' ");
            Room chasm = new Room("Bridge of Death", "Upon exiting the Cave you enter a narrow chasm with fierce winds. The only direction you can possibly go is North along a narrow ledge to what appears to be a bridge across the chasm. Just on this side of the bridge sits an old man. ( But he is asleep so you can most likely ride past him) ");
            Room fort = new Room("Castle of Guy de Lombard", "In the mists lies a Castle on a small island. The water is shallow and the castle appears to have occupants.");

            bridge.isComplete = false;
            forest.isComplete = false;
            cave.isComplete = false;


            //create items
            Item excalibur = new Item("Excalibur", "Listen, strange women lyin in ponds distributin swords is no basis for a system of government! Supreme executive power derives from a mandate from the masses,not from some farcical aquatic ceremony!");
            Item shrubbery = new Item("Shrubbery", " Oh, what sad times are these when passing ruffians can say Ni at will to old ladies. There is a pestilence upon this land. Nothing is sacred. ");
            Item grenade = new Item("Grenade", "Holy Hand [GRENADE] of Antioch - And the Lord spoke, saying, First shalt thou take out the Holy Pin. Then, shalt thou count to three, no more, no less. Three shalt be the number thou shalt count, and the number of the counting shalt be three. Four shalt thou not count nor either count thou two, excepting that thou then proceed to three. Five is right out. Once the number three, being the third number, be reached, then lobbest thou thy Holy Hand Grenade of Antioch towards thy foe, who being naughty in my sight, shall snuf it.");

            //add items to rooms
            camelot.Items.Add(excalibur);
            house.Items.Add(shrubbery);
            ledge.Items.Add(grenade);

            //set starting room to current room
            CurrentRoom = camelot;

            //Set exits for rooms
            camelot.Exits.Add("south", village); //camelot 1
            village.Exits.Add("south", house); //village 3
            village.Exits.Add("east", bridge);
            village.Exits.Add("north", camelot);
            house.Exits.Add("north", village); //house 1
            bridge.Exits.Add("east", forest); //bridge 2
            bridge.Exits.Add("west", village);
            forest.Exits.Add("east", plains); //forest 2
            forest.Exits.Add("west", bridge);
            plains.Exits.Add("north", ledge); //plains 4
            plains.Exits.Add("south", cliff);
            plains.Exits.Add("east", cave);
            plains.Exits.Add("west", forest);
            ledge.Exits.Add("south", plains); //ledge 1
            cliff.Exits.Add("north", plains); //cliff 1
            cave.Exits.Add("west", plains); // cave 2
            cave.Exits.Add("east", chasm);
            chasm.Exits.Add("west", cave); // chasm 2
            chasm.Exits.Add("north", fort);


        }

        public void StartGame()
        {

            bool introComplete = false;
            while (playing && !introComplete)
            {
                Console.Clear();
                Console.WriteLine(@"



                                _________ - - - 
                               |o^o^o^o^o| \ \ \ 
                               {   _!_   }   \ \ 
                                \   !   /    \ \ \         _ _
                                 `.   .'       \ \        (_) |
                                   )=(      __ _ _ __ __ _ _| |
                                  ( + )    / _` | '__/ _` | | |
                                   ) (    | (_| | | | (_| | | |
                               .--'   `--. \__, |_|  \__,_|_|_|
                               `---------'   _/ |   
                                            |___/      

                
                
                ");

                Console.WriteLine(@"          
                    Sire, Welcome to Grail. The Grail Quest for the holiest of Grails. ");
                Console.WriteLine(@"
            
            
                                        What is thy name?:");

                string CurrentPlayerName = Console.ReadLine();
                CurrentPlayer = new Player(CurrentPlayerName);
                // CurrentPlayer = PlayerName;

                Console.WriteLine($@"         
                                        Welcome Lord {CurrentPlayerName}");
                Console.WriteLine(@"
                                
                                        Art thou ready to begin thy quest? (Y/N)");
                ConsoleKeyInfo cki = Console.ReadKey();
                introComplete = cki.KeyChar == 'y';
                Console.Clear();
            };
            Console.WriteLine(@"
                
                For a list of commands please enter the command 'help' at any time
                
                Tip : When entering a room enter the word 'look' to investigate the current room!
                
                ");
            Console.WriteLine($@"
                
                Press ENTER to begin!");
            Console.ReadLine();
            Look();
            while (playing && introComplete)
            {
                Console.WriteLine(@"
                What is your order M'lord?");
                GetUserInput();


            }
        }

        public void TakeItem(string itemName)
        {
            Item item = CurrentRoom.Items.Find(i =>
      {
          return i.Name.ToLower() == itemName;
      });
          
            if (item != null)  // if the room does contain an item
            {
                  if (CurrentPlayer.Inventory.Count == 0 && CurrentRoom.Name =="Home of Roger the Shrubber")
            {
                Console.WriteLine("Roger the Shrubber does not fear you, for you are no king.  You do not weild Excalibur!");
                return;
            }
                CurrentRoom.Items.Remove(item); //remove from room
                CurrentPlayer.Inventory.Add(item); // add to inventory
                System.Console.WriteLine($@"You have obtained {item.Name}!");
                if (CurrentPlayer.Inventory[0].Name.ToLower() == "excalibur" && itemName.ToLower() == "excalibur")
                {
                    Console.Clear();
                    System.Console.WriteLine($@"
                    
                             />_________________________________
                    [########[]_________________________________>
                             \>

                             Remember: While this blade is eternal, you are not, only use it in the most dire of situations! (and when you know it is a fair and just fight)
                    ");
                }
                else if (CurrentPlayer.Inventory[0].Name.ToLower() == "excalibur" && CurrentPlayer.Inventory[1].Name.ToLower() == "shrubbery" && itemName.ToLower() == "shrubbery")
                {
                    Console.Clear();
                    System.Console.WriteLine($@"
                                .o00o
                                o000000oo
                             00000000000o
                             00000000000000
                          oooooo  00000000  o88o
                        ooOOOOOOOoo  ```''  888888
                        OOOOOOOOOOOO'.qQQQQq. `8888'
                       oOOOOOOOOOO'.QQQQQQQQQQ/.88'
                         OOOOOOOOOO'.QQQQQQQQQQ/ /q
                         OOOOOOOOO QQQQQQQQQQ/ /QQ
                          OOOOOOOOO `QQQQQQ/ /QQ'
                             OO:F_P:O `QQQ/  /Q'
                                \\. \ |  // |
                                d\ \\\|_////
                                 qP| \\ _' `|Ob
                                     \  / \  \Op
                                    |  | O| |
                        _          /\. \_/ /\
                            `---__/|_\\   //|  __
                                `-'  `-'`-'-'

Remember: Its just a shrubbery, use it any time to try and impress someone!
                    ");
                }

                else if (CurrentPlayer.Inventory[2].Name.ToLower() == "grenade" && itemName.ToLower() == "grenade")
                {
                    Console.Clear();
                    System.Console.WriteLine($@"
                    
                                  ______
                                 \\/  \/
                                __\\  /__
                               ||  //\   |
                               ||__\\/ __|
                                  ||  |    ,---,
                                  ||  |====`\  |
                                  ||  |    '---'
                                ,--'*`--,
                               _||#|***|#|
                            _,/.-'#|* *|#`-._
                          ,,-'#####|   |#####`-.
                        ,,'########|   |########`,
                       //##########| o |##########\
                      ||###########|   |###########|
                     ||############| o |############|
                     ||------------'   '------------|
                     ||o  o  o  o  o   o  o  o  o  o|
                     |-----------------------------|
                     ||###########################|
                      \\#########################/
                       `..#####################,'
                         ``..###############_,'
                             ``--.._____..--'
                                `''-----''`
                    
                    
                    Remember: While this grenade is eternal, you are not, only use it in the most dire of situations!
                    
                    ");
                }

                System.Console.WriteLine($@"{ item.Description}");
            }
            else
            {
                System.Console.WriteLine("M'lord! please try again, I dont undertstand...");  //no items in room
            }
        }

        public void UseItem(string direction)
        {
            string itemName = direction;
            Item item = CurrentPlayer.Inventory.Find(i =>
     {
         return i.Name.ToLower() == direction;
     });


            if (!CurrentPlayer.Inventory.Contains(item))
            {
                Console.WriteLine("Sire.. You cant use something you dont have yet.");
                return;
            }

            if (itemName == "excalibur" && CurrentPlayer.Inventory.Contains(item) && CurrentRoom.Name == "Bridge of the Black Knight" && !CurrentRoom.isComplete)
            {
                Console.Clear();
                // CurrentPlayer.Inventory.Remove(usedItem); use this if I want to consume item on use
                Console.Clear();
                Console.WriteLine("You swing Excalibur with so much might that you have cleaved all the limbs clear off your enemy. The 'Black Knight' says 'Tis but a scratch'");
                Console.WriteLine(@"
                                {}
                               .--.
                              /.--.\  
                              |====|
                              |`::`|
                            ;`\..../`;
                            |...::...|  
                            |'''::'''|
                            \   ::   /
                             >._::_.<
                            /   ^^   \
                            |        |
                            |        |
                            |___/\___| 
                    
                    ");
                Console.WriteLine("The bridge is now clear of danger. You again survey the area and determine that you can either return the way you came or ride east, what will you do?");
                CurrentRoom.isComplete = true;
                CurrentRoom.Description = "You have defeated the terrible 'Black Knight' the way is clear. You can ride either west to the village or east to the forest";
                GetUserInput();
                return;
            }
            if (itemName == "excalibur" && CurrentPlayer.Inventory.Contains(item) && CurrentRoom.Name == "Home of Roger the Shrubber")
            {
                Console.Clear();
                Console.WriteLine("You have 'accidently' slain Roger the Shrubber,you will be unable to complete your quest, please reset to try again!");
                Console.WriteLine("Reset Game? Y/N");
                GetUserInput();
                Console.Clear();
                Reset();
                Look();
                return;
            }
            if (itemName == "excalibur" && CurrentPlayer.Inventory.Contains(item) && CurrentRoom.Name == "Howden Village")
            {
                Console.Clear();
                Console.WriteLine("You have 'accidently' slain Sir Bedevere, you will be unable to complete your quest, please reset to try again!");
                Console.WriteLine("Reset Game? Y/N");
                GetUserInput();
                Console.Clear();
                Reset();
                Look();
                return;
            }
            if (itemName == "excalibur" &&CurrentPlayer.Inventory.Contains(item) && CurrentRoom.Name == "Camelot")
            {
                Console.Clear();
                Console.WriteLine("You have 'accidently' slain yourself, you silly king. You will be unable to complete your quest due to the fact that you are dead, please reset to try again!");
                Console.WriteLine("Reset Game? Y/N");
                GetUserInput();
                Console.Clear();
                Reset();
                Look();
                return;
            }
            if (itemName == "excalibur" && CurrentPlayer.Inventory.Contains(item) && CurrentRoom.Name == "Forest of Brechfa")
            {
                Console.WriteLine("In a sad attempt to engage the Knights who say 'Ni', 'Peng', and 'Neee-Wom'you were subdued and offered up to NI as a sacrifice, please reset to try again!");
                Console.WriteLine(@"
                
                     |           _|\
                 `._.\_            |
                     | `.         |
         __   ||      `._\_ ,'\,'|                __
     ,._/|||,.||________,_._ _,_'_____________,-;|||\_________
     '.----; /||------------.-.-\------------/ '.---,---------'
        \__,' ||      / (_)|`-'-__;           `.___/
        / :|  ''      |    |,-.\|              |::.\
       /___|          | .: ||_, |    __...     |____\
        | |  _..._    |.:  `.__,|  ,'   .:|     | |
        | | `-. .:''._|___:_....'-'-. .:' /     | |
        ;-:---'`.:' _\`._           /`.-./--..__;-:
       /_,'------\,'     `--...__,-'   \---..../_`_\
                 ' `.:       .          |
                     \       ::    |   :|
                     |      .:     ;  .:;
                     |    .::     /  ::/
          ,-.       / __...----.._ .::/  ,-.
         <'-'>     /-'.::::' `::::`-./  <'-'>
  ,      ,'`, \  ,'_.::.-----....__:| _,' :`.
 /|\    /  ; \ \/                 _,-' _,'\  \       .
 |:\\  ; ,'   \ \_...---'''''---,' _,-'    `. \     /|\
  \:\`-'/     .\ \               ,'/         `.\   //.|
   \:\ /      `.`._          _,-','            \`-'/:/
    `-'=*       `-.`''----'''_,-'               \ /:/
                   `'''---'''                  *=`-'


                ");
                Console.WriteLine("Reset Game? Y/N");
                GetUserInput();
                Console.Clear();
                Reset();
                Look();
                return;
            }
            if (itemName == "excalibur" && CurrentPlayer.Inventory.Contains(item) && CurrentRoom.Name == "Plains of Caerbannog")
            {
                Console.Clear();
                Console.WriteLine(@"
                                 (  .      )
                         )           (              )
                            .  '   .   '  .  '  .
                        (    , )       (.   )  (   ',    )
                        .' ) ( . )    ,  ( ,     )   ( .
                        ). , ( .   (  ) ( , ')  .' (  ,    )
                      (_,) . ), ) _) _,')  (, ) '. )  ,. (' )
                    ^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^
                ");
                Console.WriteLine("In an absolutely pathetic attempt to draw your weapon against Tim the Enchanter you were reduced to a pile of ash before you could even draw your blade, please reset to try again!");
                Console.WriteLine("Reset Game? Y/N");
                GetUserInput();
                Console.Clear();
                Reset();
                Look();
                return;
            }
            if (itemName == "excalibur" && CurrentPlayer.Inventory.Contains(item) && CurrentRoom.Name == "Cave of CaerBannog")
            {
                Console.Clear();
                Console.WriteLine(@"
                                    ***       
                                    ** **
                                    **   **
                                    **   **         **** 
                                    **   **       **   ****
                                    **  **       *   **   **
                                     **  *      *  **  ***  **
                                    **  *    *  **     **  *
                                    ** **  ** **        **
                                    **   **  **
                                   *           *
                                  *             *
                                 *    0     0    *
                                 *   /   @   \   *
                                 *   \__/ \__/   *
                                  * ( \/ V \/  ) *
                                   **|_ ^ ^ ^_|**   
                                       *****
                ");
                Console.WriteLine("In an absolutely pathetic attempt to draw your weapon against the Beast you were reduced to a pile of bones and sinew before you could even draw your blade, please reset to try again!");
                Console.WriteLine("Reset Game? Y/N");
                GetUserInput();
                Console.Clear();
                Reset();
                Look();
                return;
            }
            if (itemName == "excalibur" && CurrentPlayer.Inventory.Contains(item) && CurrentRoom.Name == "Bridge of Death")
            {
                Console.Clear();
                Console.WriteLine("In an absolutely pathetic attempt to draw your weapon against the Bridge Keeper you were reduced to a pile of bones and sinew before you could even draw your blade, please reset to try again!");
                Console.WriteLine("Reset Game? Y/N");
                GetUserInput();
                Console.Clear();
                Reset();
                Look();
                return;
            }
            if (itemName == "excalibur" && CurrentPlayer.Inventory.Contains(item) && CurrentRoom.Name == "Ledge of Caerbannog")
            {
                Console.Clear();
                Console.WriteLine("Your Draw your blade and begin slaying the brothers as they chant, upon attempting to slay Brother Maynard he pulled the pin on the Holy Hand Grenade and yells 'THREE', please reset to try again!");
                Console.WriteLine("Reset Game? Y/N");
                GetUserInput();
                Console.Clear();
                Reset();
                Look();
                return;
            }
            if (itemName == "excalibur" && CurrentPlayer.Inventory.Contains(item) && CurrentRoom.Name == "Castle of Guy de Lombard")
            {
                Console.Clear();
                Console.WriteLine(@"
                
                     -|                  [-_-_-_-_-_-_-_-]                  |-
                     [-_-_-_-_-]          |             |          [-_-_-_-_-]
                      | o   o |           [  0   0   0  ]           | o   o |
                       |     |    -|       |           |       |-    |     |
                       |     |_-___-___-___-|         |-___-___-___-_|     |
                       |  o  ]              [    0    ]              [  o  |
                       |     ]   o   o   o  [ _______ ]  o   o   o   [     | ----__________
            _____----- |     ]              [ ||||||| ]              [     |
                       |     ]              [ ||||||| ]              [     |
                   _-_-|_____]--------------[_|||||||_]--------------[_____|-_-_
                ( (__________------------_____________-------------_________) )
                                                                            
                                                                            
                         You draw Excalibur and begin to charge the castle  _-{_}-_
                                                                           //  ||\ \
                               Just then the police show up          _____//___||_\ \___
                                                                     )  _          _    \
                                      You are being arrested?        |_/ \________/ \___|
                               ________________________________________\_/________\_/______

                
                ");
                Console.WriteLine("You and your party have been arrested under the suspicion of the murder of the black knight, animal cruelty, and arson... You win? I guess?");
                Console.WriteLine("Press ANY key to restart the game or 'QUIT' to end game");
                GetUserInput();
                Console.Clear();
                Reset();
                Look();
                return;
            }
            if (itemName == "shrubbery" && CurrentPlayer.Inventory.Contains(item) && CurrentRoom.Name == "Forest of Brechfa")
            {
                Console.Clear();
                Console.WriteLine("You present the shrubbery to the Knights who say Ni, they agree it is very nice.. but they have requested ANOTHER SHRUBBERY!");
                Console.WriteLine(@"

                     |           _|\
                 `._.\_            |
                     | `.         |
         __   ||      `._\_ ,'\,'|                __
     ,._/|||,.||________,_._ _,_'_____________,-;|||\_________
     '.----; /||------------.-.-\------------/ '.---,---------'
        \__,' ||      / (_)|`-'-__;           `.___/
        / :|  ''      |    |,-.\|              |::.\
       /___|          | .: ||_, |    __...     |____\
        | |  _..._    |.:  `.__,|  ,'   .:|     | |
        | | `-. .:''._|___:_....'-'-. .:' /     | |
        ;-:---'`.:' _\`._           /`.-./--..__;-:
       /_,'------\,'     `--...__,-'   \---..../_`_\
                 ' `.:       .          |
                     \       ::    |   :|
                     |      .:     ;  .:;
                     |    .::     /  ::/
          ,-.       / __...----.._ .::/  ,-.
         <'-'>     /-'.::::' `::::`-./  <'-'>
  ,      ,'`, \  ,'_.::.-----....__:| _,' :`.
 /|\    /  ; \ \/                 _,-' _,'\  \       .
 |:\\  ; ,'   \ \_...---'''''---,' _,-'    `. \     /|\
  \:\`-'/     .\ \               ,'/         `.\   //.|
   \:\ /      `.`._          _,-','            \`-'/:/
    `-'=*       `-.`''----'''_,-'               \ /:/
                   `'''---'''                  *=`-'


                ");
                Console.WriteLine("Ser Bedevere begins taunting them with  their own terrible word 'NI' and you realize that they are no longer a threat. You are now free to continue to ride east out of the forest, or return to the bridge to the west");
                CurrentRoom.isComplete = true;
                CurrentRoom.Description = "The 'Knights of Ni' are still bickering over what to do with you for not obtaining a second shrubbery. Sir Bedevere suggests that you continue east to the 'Plains of CaerBannog'";
                GetUserInput();
                return;
            }
            if (itemName == "shrubbery" && CurrentPlayer.Inventory.Contains(item) && CurrentRoom.Name != "Forest of Brechfa")
            {
                Console.Clear();
                Console.WriteLine("You cant use this here you silly king, I blow my nose at you");
                GetUserInput();
                return;
            }
            if (itemName == "grenade" && CurrentPlayer.Inventory.Contains(item) && CurrentRoom.Name == "Cave of CaerBannog")
            {
                Console.Clear();
                Console.WriteLine("...And Saint Attila raised the hand grenade up on high, saying, O LORD, bless this Thy hand grenade that with it Thou mayest blow Thine enemiess to tiny bits, in Thy mercy. And the LORD did grin and the people did feast upon the lambs and sloths and carp and anchovies and orangutans and breakfast cereals, and fruit bats and large chu... And the LORD spoke saying, 'First shalt thou take out the Holy Pin, then shalt thou count to three, no more, no less. Three shall be the number thou shalt count, and the number of the counting shall be three. Four shalt thou not count, neither count thou two, excepting that thou then proceed to three. Five is right out. Once the number three, being the third number, be reached, then lobbest thou thy Holy Hand Grenade of Antioch towards thy foe, who, being naughty in My sight, shall snuff it");
                Console.WriteLine(@"
                                _.-^^---....,,--       
                            _--                  --_  
                            <                        >)
                            |                         | 
                             \._                   _./  
                               ```--. . , ; .--'''       
                                      | |   |             
                                   .-=||  | |=-.   
                                   `-=#$%&%$#=-'   
                                     | ;  :|     
                            _____.,-#%&$@%#&#~,._____
                
                
                ");
                Console.WriteLine("The rabbit has been defeated and you and your knights are  free to either continue to ride east to the chasm that leads to the 'bridge of death' or turn back");
                CurrentRoom.isComplete = true;
                CurrentRoom.Description = "Bits of fur and rabbit meat lie in every corner of the cave. While you were successful in defeating your foe you sense there is something 'AHHHHH'-ful in this cave , you should proceed through to the east";
                GetUserInput();
                return;
            }
            if (itemName == "grenade" && CurrentPlayer.Inventory.Contains(item) && CurrentRoom.Name != "Cave of CaerBannog")
            {
                Console.Clear();
                Console.WriteLine("One, two, FIVE!!!");
                Console.WriteLine(@"
                                _.-^^---....,,--       
                            _--                  --_  
                            <                        >)
                            |                         | 
                             \._                   _./  
                               ```--. . , ; .--'''       
                                      | |   |             
                                   .-=||  | |=-.   
                                   `-=#$%&%$#=-'   
                                     | ;  :|     
                            _____.,-#%&$@%#&#~,._____
                
                
                ");
                Console.WriteLine("You have blown yourself up please reset to try again! Reset Game? Y/N");
                GetUserInput();
                Console.Clear();
                Reset();
                Look();
                return;
            }


        }





        // public void Ride(string direction)
        // {
        //     throw new NotImplementedException();
        // }
    }
}