using UnityEngine;
using System.Collections;
using UnityEngine.UI;

// ==========================================================================
// THE DUNGEON - Homework (Intro to C#  +  Variables & Operators)
// --------------------------------------------------------------------------
// One growing program - keep building this SAME file.
//   PART A: do after the INTRO lecture (uses only Debug.Log).
//   PART B: finish after the VARIABLES lecture (variables & operators).
// Attach to an empty GameObject and press Play to test as you go.
// ==========================================================================
public class dungeonGame : MonoBehaviour
{
    void Start()
    {
        // (PART B) TODO B1: declare your stats here, at the very top of Start,
        //          once you have had the Variables lecture. You will need:
        //            playerName (string), health (int), attack (int),
        //            agility (int), gold (int), hasKey (bool),
        //            goblinHealth (int), goblinAttack (int).

        // ===== ALREADY BUILT IN CLASS (Intro lecture): the opening + two rooms =====

        Debug.Log("=== THE DUNGEON ===");
        string playerName = "Escapee";
        int health = 30;
        int maxHealth = 30;
        int attack = 1;
        int defense = 1;
        int armorClass = 10;
        int agility = 1;
        bool hasKey = false;
        bool hasRope = false;
        bool playerAlive = true;
        int roomCount = 0;
        int attackRoll = 0;
        int currentRoom = -1;
        int openInvSlot = -1;

        string[] roomNames = 
        {   
            "The Entrance Hall",
            "The Guard Room",
            "The Troll Cave",
            "The Mess Hall",
            "The Barracks",
            "The Flooded Passage",
            "The Treasure Room",
            "The Armory",
            "The Merchant's Room",
            "The Broken Throneroom",
        };

        string[] roomDescsriptions = 
        {   
            "A torch flickers on the wall. A stone doorway leads north.",
            "A rusty sword rests on a table. A goblin snores in the corner.",
            "You walk into a large cave littered with bones.",
            "You enter a large hall. Tables with scattered seats fill the room, but it is devoid of inhabitants.",
            "You enter a room lined with cots, clearly a communal living space for whoever runs this dungeon. At the foot of each cot is a small chest for storing personal belongings.",
            "Ankle-deep water fills the hall. A broken door is at the end of the hallway.",
            "It seems this room has been raided. You find " + 5000 + " gold.",
            "This room is filled with containers and storage racks for weapons and armor. Unfortunately, they are mostly empty, save for a small locked chest.",
            "You enter a large room, more akin to a hallway. It is lined with stalls, but all are abandoned...save one. A hooded figure calls out to you, 'Potions for sale!'",
            "A large dais with an imposing throne dominates the room, where the lord of this fortress presumably ruled from. 'Ruled,' as an oversized balista bolt has pierced the wall behind the throne and impaled its occupant.",
        };

        string[] inventory = { "", "", "", "", "", "" }; //inventory can contain rusted sword, polished sword, key, rope, torch, and cloak

        Debug.Log("Welcome, " + playerName + ". Your escape begins."); // replace Hero with the name of your player.

        Debug.Log("Map of the dungeon:");
        foreach (string name in roomNames) 
        {
            Debug.Log(name);
        }

        Debug.Log("");
        //Debug.Log("The Entrance Hall");
        currentRoom += 1;
        Debug.Log(roomNames[currentRoom]);

        roomCount += 1;
        if ((roomCount % 3) == 0)
        {
            Debug.Log("As you enter, you notice the room is filled with a red glow");
        }

        Debug.Log("A torch flickers on the wall. A stone doorway leads north.");
        Debug.Log("You take the torch. With the light it casts, you feel you can move a bit quicker.");
        openInvSlot += 1;
        inventory[openInvSlot] += "torch";
        foreach (string itemCheck in inventory)
        {
            if (itemCheck == "torch")
            {
                agility += 1;
            }
        }
        Debug.Log("Your agility increases to " + agility);
        Debug.Log("You move into the next room.");

        int goblinHealth = 15;
        int goblinAttack = 2;
        int goblinDefense = 1;
        int goblinArmor = 10;
        int goblinRoll = 0;

        Debug.Log("");
        //Debug.Log("The Guard Room");
        currentRoom += 1;
        Debug.Log(roomNames[currentRoom]);

        roomCount += 1;
        if ((roomCount % 3) == 0)
        {
            Debug.Log("As you enter, you notice the room is filled with a red glow");
        }

        Debug.Log("A rusty sword rests on a table. A goblin snores in the corner.");
        Debug.Log("The goblin jolts awake, and seeing you, makes to charge you. You snatch up the sword in a panic and prepare to defend yourself.");
        openInvSlot += 1;
        inventory[openInvSlot] += "rusted shortsword";
        foreach (string itemCheck in inventory)
        {
            if (itemCheck == "rusted shortsword")
            {
                attack += 1;
            }
        }
        Debug.Log("Your attack increases to " + attack);

        Debug.Log("The goblin lunges at you!");

        for (int i = 1; ; i++)
        {
            Debug.Log("Turn " + i + ".");

            Debug.Log("The goblin attacks you!");
            goblinRoll = Random.Range(1, 21);
            Debug.Log("The goblin attacks with an attack roll of " + goblinRoll + ".");

            if (goblinRoll >= armorClass)
            {
                if (goblinRoll == 20)
                {
                    Debug.Log("The goblin crits!");
                    health -= ((goblinAttack*2) - defense);
                }
                else
                {
                    Debug.Log("The goblin hits!");
                    health -= (goblinAttack - defense);
                }
            }
            else
            {
                Debug.Log("The goblin misses...");
            }

            Debug.Log("You have " + health + " HP!");

            if (health <= 0)
            {
                Debug.Log("You collapse to your sustained wounds");
                playerAlive = false;
                break;
            }
            else if (health <= (maxHealth / 4))
            {
                Debug.Log("You are badly wounded");
            }
            else if (health < maxHealth)
            {
                Debug.Log("You are wounded, but carry on");
            }

            Debug.Log("You attack the goblin!");
            attackRoll = Random.Range(1, 21);
            Debug.Log("You attack with an attack roll of " + attackRoll + ".");

            if (attackRoll >= goblinArmor)
            {
                if (attackRoll == 20)
                {
                    Debug.Log("You crit!");
                    goblinHealth -= ((attack * 2) - goblinDefense);
                }
                else
                {
                    Debug.Log("You hit!");
                    goblinHealth -= (attack - goblinDefense);
                }
                if(goblinHealth <= 0)
                {
                    Debug.Log("You defeat the goblin!");
                    break;
                }
            }
            else
            {
                Debug.Log("You miss...");
            }

            Debug.Log("The goblin has " + goblinHealth + " HP!");

        }

        if(playerAlive == false)
        {
            playerAlive = true;
            health = 1;
        }

        Debug.Log("You move to the next room.");

        Debug.Log("");
        //Debug.Log("The Troll Cave");
        currentRoom += 1;
        Debug.Log(roomNames[currentRoom]);

        currentRoom += 1;
        Debug.Log(roomNames[currentRoom]);
        Debug.Log("You walk into a large cave littered with bones.");

        roomCount += 1;
        if ((roomCount % 3) == 0)
        {
            Debug.Log("As you enter, you notice the room is filled with a red glow");
        }

        Debug.Log("An ogre in the back of the room hears you enter, and charges at you!");

        int ogreHealth = 20;
        int ogreAttack = 3;
        int ogreDefense = 1;
        int ogreArmor = 10;
        int ogreRoll = 0;

        for (int i = 1; ; i++)
        {
            Debug.Log("Turn " + i + ".");

            if (i % 2 == 0)
            {
                Debug.Log("The ogre struggles to regain its balance after its swing and cannot attack!");
            }
            else
            {
                Debug.Log("The ogre attacks you with its mighty club!");
                ogreRoll = Random.Range(1, 21);
                Debug.Log("The ogre attacks with an attack roll of " + ogreRoll + ".");

                if (ogreRoll >= armorClass)
                {
                    if (ogreRoll == 20)
                    {
                        Debug.Log("The ogre crits!");
                        health -= ((ogreAttack * 2) - defense);
                    }
                    else
                    {
                        Debug.Log("The ogre hits!");
                        health -= (ogreAttack - defense);
                    }
                }
                else
                {
                    Debug.Log("The ogre misses...");
                }

                Debug.Log("You have " + health + " HP!");

                if (health <= 0)
                {
                    Debug.Log("You collapse to your sustained wounds");
                    playerAlive = false;
                    break;
                }
                else if (health <= 5)
                {
                    Debug.Log("You are badly wounded");
                }
                else if (health < 20)
                {
                    Debug.Log("You are wounded, but carry on");
                }
            }
            Debug.Log("You attack the ogre!");
            attackRoll = Random.Range(1, 21);
            Debug.Log("You attack with an attack roll of " + attackRoll + ".");

            if (attackRoll >= ogreArmor)
            {
                if (attackRoll == 20)
                {
                    Debug.Log("You crit!");
                    ogreHealth -= ((attack * 2) - ogreDefense);
                }
                else
                {
                    Debug.Log("You hit!");
                    ogreHealth -= (attack - ogreDefense);
                }
                if (ogreHealth <= 0)
                {
                    Debug.Log("You defeat the ogre!");
                    break;
                }
            }
            else
            {
                Debug.Log("You miss...");
            }

            Debug.Log("The ogre has " + ogreHealth + " HP!");

        }

        if (playerAlive == false)
        {
            playerAlive = true;
            health = 1;
        }


        Debug.Log("A great spider descends from a hole in the roof of the cave!");

        int spiderHealth = 20;
        int spiderAttack = 2;
        int spiderDefense = 1;
        int spiderArmor = 11;
        int spiderRoll = 0;
        int spiderPoison = 0;

        for (int i = 1; ; i++)
        {
            Debug.Log("Turn " + i + ".");

            if (spiderPoison > 0)
            {
                Debug.Log("Icy poison runs in your veins...");
                spiderPoison -= 1;
                Debug.Log("You take 2 damage!");
                health -= 2;
                if (health <= 0)
                {
                    Debug.Log("You collapse to your sustained wounds");
                    playerAlive = false;
                    break;
                }
                else if (health <= 5)
                {
                    Debug.Log("You are badly wounded");
                }
                else if (health < 20)
                {
                    Debug.Log("You are wounded, but carry on");
                }
            }

            Debug.Log("The spider attacks you with its venomous bite!");
            spiderRoll = Random.Range(1, 21);
            Debug.Log("The spider attacks with an attack roll of " + spiderRoll + ".");

            if (spiderRoll >= armorClass)
            {
                spiderPoison += 3;
                if (spiderRoll == 20)
                {
                    Debug.Log("The spider crits!");
                    health -= ((spiderAttack * 2) - defense);
                }
                else
                {
                    Debug.Log("The spider hits!");
                    health -= (spiderAttack - defense);
                }
            }
            else
            {
                Debug.Log("The spider misses...");
            }

            Debug.Log("You have " + health + " HP!");

            if (health <= 0)
            {
                Debug.Log("You collapse to your sustained wounds");
                playerAlive = false;
                break;
            }
            else if (health <= 5)
            {
                Debug.Log("You are badly wounded");
            }
            else if (health < 20)
            {
                Debug.Log("You are wounded, but carry on");
            }

            Debug.Log("You attack the spider!");
            attackRoll = Random.Range(1, 21);
            Debug.Log("You attack with an attack roll of " + attackRoll + ".");

            if (attackRoll >= spiderArmor)
            {
                if (attackRoll == 20)
                {
                    Debug.Log("You crit!");
                    spiderHealth -= ((attack * 2) - spiderDefense);
                }
                else
                {
                    Debug.Log("You hit!");
                    spiderHealth -= (attack - spiderDefense);
                }
                if (spiderHealth <= 0)
                {
                    Debug.Log("You defeat the spider!");
                    break;
                }
            }
            else
            {
                Debug.Log("You miss...");
            }

            Debug.Log("The spider has " + spiderHealth + " HP!");

        }

        if (!playerAlive)
        {
            playerAlive = true;
            health = 1;
        }

        Debug.Log("You find the remains of a small campfire someone left in the cave. You light it to rest from the grueling gauntlet of combat.");
        for(int i = 5; i > 0; i--)
        {
            Debug.Log("The fire will remain lit for " + i + " more turns.");
            health += 2;
            if (health >= maxHealth)
            {
                health = maxHealth;
                Debug.Log("As the warmth envelops you, you fully heal your wounds. Your HP is now " + health);
                Debug.Log("Well rested, you put out the fire.");
                break;
            }
            Debug.Log("As the warmth envelops you, you heal for 2 HP. Your HP is now " + health);
            
            
        }

        if (health <= 0)
        {
            Debug.Log("You collapse to your sustained wounds");
            playerAlive = false;
        }
        else if (health <= 5)
        {
            Debug.Log("You are badly wounded");
        }
        else if (health < 20)
        {
            Debug.Log("You are wounded, but carry on");
        }


        // A3: The passage forks in two. Send the hero down one of the routes and
        //     describe each one - the two routes may even rejoin at the same
        //     place further on.

        bool pathChoiceLeft = true;

        Debug.Log("You come across a fork in the hall.");
        if (pathChoiceLeft == true)
        {
            Debug.Log("You decide to go down the left hall.");

            Debug.Log("");
            //Debug.Log("The Mess Hall");
            currentRoom += 1;
            Debug.Log(roomNames[currentRoom]);

            roomCount += 1;
            if ((roomCount % 3) == 0)
            {
                Debug.Log("As you enter, you notice the room is filled with a red glow");
            }
            Debug.Log("You enter a large hall. Tables with scattered seats fill the room, but it is devoid of inhabitants.");
            Debug.Log("In a large alcove you find a cookfire with a large pot filled with bubble stew. You eat from it to regain your strength.");
            health += 3;
            if (health >= maxHealth)
            {
                health = maxHealth;
            }
            Debug.Log("You exit the room. A short while later, the hall rejoins with the other path, and you continue onwards.");
        }
        else 
        {
            Debug.Log("You decide to go down the right hall.");

            Debug.Log("");
            //Debug.Log("The Barracks");
            currentRoom += 1;
            Debug.Log(roomNames[currentRoom]);

            roomCount += 1;
            if ((roomCount % 3) == 0)
            {
                Debug.Log("As you enter, you notice the room is filled with a red glow");
            }

            Debug.Log("You enter a room lined with cots, clearly a communal living space for whoever runs this dungeon. At the foot of each cot is a small chest for storing personal belongings.");
            Debug.Log("You check the chests for anything of use, but unfortunately find nothing that would aid your escape.");
            Debug.Log("You exit the room. A short while later, the hall rejoins with the other path, and you continue onwards.");
        }


        // ======================================================================
        // PART A  -  after the INTRO lecture (Debug.Log only)
        // ======================================================================

        // TODO A1: FIX THE BROKEN ROOM below. It has bugs that stops the program
        //          from running. Un-comment the lines, find the bug(s), fix it,
        //          and add a // comment saying what was wrong.

        Debug.Log("");
        //Debug.Log("The Flooded Passage"); // line missing closing semi-colon
        currentRoom += 1;
        Debug.Log(roomNames[currentRoom]);

        roomCount += 1;
        if ((roomCount % 3) == 0)
        {
            Debug.Log("As you enter, you notice the room is filled with a red glow");
        }

        Debug.Log("Ankle-deep water fills the hall. A broken door is at the end of the hallway.");
        Debug.Log("You walk towards the broken door, and trip over a large, rusty key. You pocket it before proceeding to the exit.");
        openInvSlot += 1;
        inventory[openInvSlot] += "key";
        
        Debug.Log("You move into the next room.");

        int gold = 15;

        Debug.Log("");
        //Debug.Log("The Treasure Room"); // string was missing quotation marks
        currentRoom += 1;
        Debug.Log(roomNames[currentRoom]);

        roomCount += 1;
        if ((roomCount % 3) == 0)
        {
            Debug.Log("As you enter, you notice the room is filled with a red glow");
        }

        Debug.Log("It seems this room has been raided. You find " + 5000 + " gold."); // wow that's a lot of gold! no programming bug, however
        gold += 5000;
        Debug.Log("You move into the next room."); // line was missing closing semicolon and string was missing closing quotation mark


        // TODO A2: write at least one of your OWN room - a Room Name line,
        //          a description line, and a line describing how you exit. 
        Debug.Log("");
        //Debug.Log("The Armory");
        currentRoom += 1;
        Debug.Log(roomNames[currentRoom]);

        roomCount += 1;
        if ((roomCount % 3) == 0)
        {
            Debug.Log("As you enter, you notice the room is filled with a red glow");
        }

        Debug.Log("This room is filled with containers and storage racks for weapons and armor. Unfortunately, they are mostly empty, save for a small locked chest.");
        
        if (hasKey && playerAlive)
        {
            Debug.Log("You try the key in the chest's lock. It works!");
            hasKey = false;
            Debug.Log("haskey: " + hasKey);
            Debug.Log("You open the chest, revealing a polished shortsword, some rations, a cloak, and some rope. You take the contents in case they prove useful.");
            openInvSlot += 1;
            inventory[openInvSlot] += "polished shortsword";
            foreach (string itemCheck in inventory)
            {
                if (name == "polished shortsword")
                {
                    attack += 1;
                }
            }
            Debug.Log("You put the polished shortsword in your belt, increasing your attack to " + attack + ".");

            openInvSlot += 1;
            inventory[openInvSlot] += "cloak";
            foreach (string itemCheck in inventory)
            {
                if (itemCheck == "cloak")
                {
                    defense += 1;
                }
            }
            Debug.Log("You don the cloak, and your defense increases to " + defense + ".");
            
            openInvSlot += 1;
            inventory[openInvSlot] += "rope";
            Debug.Log("You put the rope over one shoulder like a sash for safekeeping.");
            Debug.Log("hasRope: " + hasRope);
        }
        else if (!hasKey)
        {
            Debug.Log("You have no key for the chest");
        }
        else if (!playerAlive)
        {
            Debug.Log("You unfortunately have collapsed to your wounds, and therefore don't have the strength to open the chest");
        }

        Debug.Log("You move into the next room.");

        int potionCost = 5;
        // int potionCount = 0;
        Debug.Log("");
        //Debug.Log("The Merchant's Room");
        currentRoom += 1;
        Debug.Log(roomNames[currentRoom]);

        roomCount += 1;
        if ((roomCount % 3) == 0)
        {
            Debug.Log("As you enter, you notice the room is filled with a red glow");
        }

        Debug.Log("You enter a large room, more akin to a hallway. It is lined with stalls, but all are abandoned...save one. A hooded figure calls out to you, 'Potions for sale!'");
        Debug.Log("The Merchant is selling health potions for " + potionCost + " gold apiece. You check your humble coinpurse, which currently has " + gold + " gold coins. You're uncertain if you have enough.");
        Debug.Log("You can currently afford " + (gold / potionCost) + "potions, which would leave your coin purse with only " + (gold % potionCost) + " coins. Thinking better on it, you decline the mysterious merchant's offer. Better to be frugal.");
        Debug.Log("You leave through an ornate door behind the Merchant's stall that you did not see upon entering.");

        // TODO A3: write the EXIT room - a final "room" and description that leads the
        //          player out of the dungeon.
        Debug.Log("");
        //Debug.Log("The Broken Throneroom");
        currentRoom += 1;
        Debug.Log(roomNames[currentRoom]);

        roomCount += 1;
        if ((roomCount % 3) == 0)
        {
            Debug.Log("As you enter, you notice the room is filled with a red glow");
        }

        Debug.Log("A large dais with an imposing throne dominates the room, where the lord of this fortress presumably ruled from. 'Ruled,' as an oversized balista bolt has pierced the wall behind the throne and impaled its occupant.");
        Debug.Log("You see that the throne would make for a good point to secure the rope to.");
            if(hasRope == true)
        {
            Debug.Log("You tie one end of the rop to the throne.");
            hasRope = false;
            Debug.Log("hasRope: " + hasRope);
        }
        Debug.Log("You use the rope to climb down from the large opening in the wall made by the ballista bolt, and to your freedom!");

        foreach(string item in inventory)
        {
            Debug.Log(item);
        }

        // ======================================================================
        // PART B  -  after the VARIABLES lecture (variables & operators)
        // ======================================================================

        // TODO B2: FIX THE BROKEN ROOM below. It has bugs that stops the program
        //          from running. Un-comment the lines, find the bug(s), fix it,
        //          and add a // comment saying what was wrong.

        // TODO B3: Go back through your rooms above and add an event/item to each
        //          one that changes a stat/variable, printing the new value right
        //          after the event. Like with the gold in the treasure room, keep
        //          each event inside the room where it happens. Possible events:
        //            pick up a sword to increase attack      
        //            step on a trap        
        //            grab the rusty key    
        //            (your own event)

        // TODO B4: In the appropriate room, add a goblin. The goblin attacks the
        //          player and the player attacks the goblin. Use the variables
        //          you've created in PART A to simulate this with code. After the
        //          simulation is done, Print both healths, then print whether or 
        //          not the goblin is defeated. 

        // TODO B5: Add a new room somewhere before the exit that has a merchant. 
        //          The merchant sells potions for 5 gold each. Print how many
        //          potions you can afford and how much gold you will have left over. 

        // TODO B6: Add a defense stat (declare it up top with the others). Use it
        //          in your combat so the goblin's hit damage is reduced by your
        //          defense and your hit damage is reduced by the goblin's defense
        //          Then, add an item that raises defense in a room. 


        // ================= PART A - CONDITIONALS (do after L7) =================

        // A1: After the goblin fight, report whether the goblin was defeated or
        //     the hero was the one who fell.                                                    check
        // A2: At a vault door, the hero may pass only if they are carrying the
        //     key and are still alive. If they cannot pass, report which of the
        //     two requirements they are missing.                                             check

        // A4: As the hero explores, every third room they enter has a red glow.
        //     Given the number of the room the hero is standing in, report
        //     whether this room has the red glow.                                            check
        // A5: Whenever the hero takes damage, report their condition: collapsed
        //     if no health remains, badly wounded if their health has dropped
        //     dangerously low, or otherwise hurt but steady. This is a snippet
        //     you will reuse a lot - go back through everything you have already
        //     written (INCLUDING your previous assignment) and drop it in right
        //     after every place the hero loses health (the spike trap, and
        //     anywhere else). You will add it again in Part B after each hit the
        //     hero takes in a fight.                                                         check


        // ================= PART B - LOOPS (do after L8) =================

        // Place the following combat encounters in different rooms of your choice.
        // B1: A goblin blocks the way - fight it round by round until one of you
        //     runs out of health. Give the hero and the goblin each an armor
        //     class. On every swing, roll a die (Random.Range works well) and
        //     compare it to the target's armor class: the blow only lands if the
        //     roll meets or beats that armor class. On top of that, any of the
        //     hero's landed hits can be a critical hit that deals extra damage.
        //     Report each roll and its result, and after any round in which the
        //     hero takes damage, run your A5 condition check.
        // B2: Beyond the goblin waits an ogre - slow, but brutal. Fight it the
        //     same way (armor classes, dice rolls to hit, and the hero's chance
        //     to crit), except the ogre is so sluggish it only swings every
        //     other round. Fight until one of them falls, and keep running your
        //     A5 check whenever the hero takes damage.
        // B3: Then a giant spider drops from the ceiling. Fight it just like the
        //     ogre - dice-and-armor-class swings, the hero's crits, and it too
        //     only strikes every other round - but its bite is venomous: any
        //     time it lands a hit, the hero is poisoned and loses 2 health at
        //     the start of each of the next three rounds, on top of the bite
        //     itself. Fight until one of them falls, running your A5 check after
        //     any damage (including the poison ticks).
        // B4: With the fights behind them, the hero rests at a campfire,
        //     recovering a little health each turn until fully healed or the
        //     fire dies after a set number of turns. Report their health as it
        //     climbs, and never let it rise above the maximum.



        // A1: ROOMS INTO ARRAYS. Put your existing room names and descriptions into
        //     two arrays (roomNames[i] and roomDescsriptions[i] describe the SAME
        //     room). Keep the same rooms you already have (do not add or remove any).
        // 

        //     Hint: How many elements should be in the roomNames array? What about
        //     the roomDescriptions array? Should they be different lengths or the same?


        // A2: SHOW THE MAP. At the very start of the game, output every room name with
        //     a single loop (a quick map of the dungeon) using Debug.Log
        //

        //      Hint: What new type of loop can you use for this that works specifically
        //      with containers?


        // A3: MOVE BY INDEX. Keep a currentRoom number that starts at 0. Each time the
        //     player moves on, add 1 to it and show that room's name and description
        //     from the arrays, instead of hand-writing each room's header.



        // A4: ONE INVENTORY ARRAY. Replace your separate item flags (hasKey, the
        //     sword, the shield, and so on) with a single inventory array. Add an item
        //     to it each time the player picks something up.
        //
        //     Hint: How big does this array need to be? What data type should it be?



        // A5: SHOW THE LOOT. At the end of the run, print the player's whole inventory



        // A6: KEY FROM THE INVENTORY. At the vault door, do not use a hasKey bool.
        //     Check whether "key" is in the inventory array to decide if the hero may
        //     pass.
        //     

        //     Hint: How could you use what we've previously learned (loops and ifs) to
        //     find if a specific item is in an array?



        // A7: COMBAT ITEMS, NOT PERMANENT UPGRADES. Right now picking up the
        //     sword or shield permanently raises attack or defense. Stop doing that. Leave the
        //     Do not increase the base stats (attack and defense) of the player.
        //

        //     Instead, INSIDE each fight, check the inventory and apply the appropriate bonus
        //     only in the attack and damage calculations. For example:
        //     If the hero is carrying the sword, add swordBonus to the attack and damage roll
        //     If the hero is carrying the shield, subtract shieldBonus when reducing damage taken



        // A8: ENEMIES INTO ARRAYS (the data, not the encounters). Right now each enemy
        //     is a pile of separate variables (goblinHealth, goblinAttack, ogreHealth,
        //     and so on). Put them into arrays instead (enemyNames[], enemyHealth[],
        //     enemyAttack[], enemyArmorClass[] and enemyDefense[]), one array element
        //     per enemy (element 0 = goblin, element 1 = ogre, element 2 = spider)
        //    Keep each fight in its OWN room and read the enemy from the arrays by index.

        //
        //     Hint: How many arrays do you need? How many elements per array?


        // A9: GOLD BY ROOM. Store the gold each room holds in a roomGold[] array and
        //     total it with a loop, instead of adding gold by hand in each room. As the player
        //     walks through each room, add however much gold is in the room to the player's
        //     overall gold.
        //

        //     Hint: How many elements do you need in this array? What should you do if
        //     a room doesn't have gold in it?


        // A10: ROOM ENEMIES. Create a roomEnemy[] array alongside your room arrays,
        //      one slot per room, holding the index of the enemy waiting there (or -1 for none).
        //      As currentRoom advances, if roomEnemy[currentRoom] is not -1, run the
        //      enemy's fight. This ties each enemy to its room through the arrays.
        //
        //      Hint: What data type should the roomEnemy array be if it is an array of indexes?


        // A11: TRACK VISITED ROOMS: Create a "visited" array of bools, one per room,
        //      so you can mark the rooms the player has already been through.
        //

        //      Hint: How big should this array be? What data type should it be? What should
        //      the starting values be?


        // A12: LOOT TABLE: an array of possible rewards. Print the whole table (or
        //      pick from it) with a loop instead of writing each reward out by hand. We won't
        //      be using this table for anything yet, but add at least three possible rewards.
        //      Example rewards could be some kind of healing item, weapon, armor, etc.
    }
}
