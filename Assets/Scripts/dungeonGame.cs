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
    string playerName = "Escapee";
    int health = 30;
    int maxHealth = 30;
    int lowHealthThreshold = 4;
    int attack = 1;
    int defense = 1;
    int armorClass = 10;
    int agility = 1;
    int gold = 0;
    int cursedCoin = 10;
    //bool hasKey = false;
    //bool hasRope = false;
    bool playerAlive = true;
    int roomCount = 0;
    int attackRoll = 0;
    int currentRoom = -1;
    int openInvSlot = -1;

    int shortSwordBonus = 1;
    int swordBonus = 1;
    int cloakBonus = 1;
    bool shortSwordCheck = false;
    bool swordCheck = false;
    bool cloakCheck = false;
    int totalAttack = 0;
    int totalDefense = 0;

    bool inCombat = false;
    int currentEnemy = 0;
    int critRoll = 20;
    int critModifier = 2;
    int lowHealth = 4;
    int potionPrice = 30;
    int healAmount = 10;
    int turnCount = 0;
    int roomGlowCounter = 3;
    string[] roomNames =
    {
            "The Entrance Hall",
            "The Guard Room",
            "The Troll Cave",
            "The Spider Vault",
            "The Mess Hall",
            "The Barracks",
            "The Flooded Passage",
            "The Treasure Room",
            "The Armory",
            "The Merchant's Room",
            "The Broken Throneroom",
            "The Exit", //unused "room" to prevent array overflow if examine = true for ShowRoom()
        };

    int[] roomGold = { 0, 10, 3, 5, 5, 8, 5000, 0, 0, 0, 0 };

    string[] roomDescriptions =
    {
        "A torch flickers on the wall. A stone doorway leads north.",
        "A rusty sword rests on a table. A goblin snores in the corner.",
        "You walk into a large cave littered with bones.",
        "You walk into a cave, walls lined with thick layers of spider webs",
        "You enter a large hall. Tables with scattered seats fill the room, but it is devoid of inhabitants.",
        "You enter a room lined with cots, clearly a communal living space for whoever runs this dungeon. At the foot of each cot is a small chest for storing personal belongings.",
        "Ankle-deep water fills the hall. A broken door is at the end of the hallway.",
        "It seems this room has been raided.",
        "This room is filled with containers and storage racks for weapons and armor. Unfortunately, they are mostly empty, save for a small locked chest.",
        "You enter a large room, more akin to a hallway. It is lined with stalls, but all are abandoned...save one. A hooded figure calls out to you, 'Potions for sale!'",
        "A large dais with an imposing throne dominates the room, where the lord of this fortress presumably ruled from. 'Ruled,' as an oversized balista bolt has pierced the wall behind the throne and impaled its occupant.",
        "You have exited the dungeon!"
    };
    string[] inventory = new string[11]; //inventory can contain rusted shortsword, polished sword, key, rope, torch, and cloak, plus contents of loot[]

    string[] enemyNames = { "goblin", "ogre", "spider" };
    int[] enemyHealth = { 15, 20, 20 };
    int[] enemyAttack = { 2, 3, 2 };
    int[] enemyArmorClass = { 10, 10, 11 };
    int[] enemyDefense = { 1, 1, 1 };

    int[] roomEnemy = { -1, 0, 1, 2, -1, -1, -1, -1, -1, -1, -1 }; //index contains the enemy's index if room has an encounter, otherwise index is -1

    bool[] visited = new bool[11];
    string[] loot = { "big sword", "biggest sword", "super potion", "gleaming armor", "shoes" };

    void ShowTitle()
    {
        Debug.Log("=== THE DUNGEON ===");
    }

    void ShowRoom(int index)
    {
        Debug.Log(roomNames[index]);
        visited[index] = true;

        roomCount += 1;
        if ((roomCount % roomGlowCounter) == 0)
        {
            Debug.Log("As you enter, you notice the room is filled with a red glow");
        }

        Debug.Log(roomDescriptions[index]);
    }

    void ShowRoom(int index, bool examine)
    {
        Debug.Log("");
        Debug.Log(roomNames[index]);
        if (!visited[index])
        {
            visited[index] = true;
            Debug.Log(roomDescriptions[index]);
        }
        roomCount += 1;
        if ((roomCount % roomGlowCounter) == 0)
        {
            Debug.Log("As you enter, you notice the room is filled with a red glow");
        }

        if (examine)
        {                                                                                           
            if (index == 4)                                                                         //Temp solution to "looking ahead" to the room after the branching option of the barracks and the mess hall
            {                                                                                       //
                Debug.Log("You look ahead and see that the room exits to " + roomNames[index + 2]); //
            }
            Debug.Log("You look ahead and see that the room exits to " + roomNames[index + 1]);
            Debug.Log("You check the room, and see it has " + roomGold[index] + " gold in it");
        }
    }

    void CombatRound(int enemyIndex)
    {
        TurnPass();
        Debug.Log("Turn " + turnCount);
        Debug.Log("You attack the " + enemyNames[enemyIndex] + "!");
        attackRoll = RollDice() + totalAttack;
        Debug.Log("You attack with an attack roll of " + attackRoll + ".");

        if (attackRoll >= enemyArmorClass[enemyIndex])
        {
            if (attackRoll >= critRoll)
            {
                Debug.Log("You crit!");
                enemyHealth[enemyIndex] -= ((totalAttack * critModifier) - enemyDefense[enemyIndex]);
            }
            else
            {
                Debug.Log("You hit!");
                enemyHealth[enemyIndex] -= (totalAttack - enemyDefense[enemyIndex]);
            }
            if (enemyHealth[enemyIndex] <= 0)
            {
                Debug.Log("The " + enemyNames[enemyIndex] + " is defeated!");
                inCombat = false;
            }
        }
        else
        {
            Debug.Log("You miss...");
        }
        if (enemyHealth[enemyIndex] > 0)
        {
            Debug.Log("The " + enemyNames[enemyIndex] + " attacks you!");
            int enemyRoll = RollDice();
            Debug.Log("The " + enemyNames[enemyIndex] + " attacks with an attack roll of " + enemyRoll + ".");

            if (enemyRoll >= armorClass)
            {
                if (enemyRoll >= critRoll)
                {
                    Debug.Log("The " + enemyNames[enemyIndex] + " crits!");
                    health -= ((enemyAttack[enemyIndex] * critModifier) - totalDefense);
                }
                else
                {
                    Debug.Log("The " + enemyNames[enemyIndex] + " hits!");
                    health -= (enemyAttack[enemyIndex] - totalDefense);
                }
            }
            else
            {
                Debug.Log("The " + enemyNames[enemyIndex] + " misses...");
            }
        }
        Debug.Log("You have " + health + " HP!");

        ReportCondition(health);

        Debug.Log("The " + enemyNames[enemyIndex] + " has " + enemyHealth[enemyIndex] +" HP!");

    }

    void Combat(int enemyIndex)
    {
        inCombat = true;
        while (inCombat)
        {
            foreach (string item in inventory)
            {
                if (item == "rusted shortsword")
                {
                    shortSwordCheck = true;
                }
                if (item == "polished sword")
                {
                    swordCheck = true;
                }
                if (item == "cloak")
                {
                    cloakCheck = true;
                }
            }
            if (shortSwordCheck)
            {
                totalAttack = attack + shortSwordBonus;
            }
            if (swordCheck)
            {
                totalAttack = attack + swordBonus;
            }
            if (shortSwordCheck && swordCheck)
            {
                totalAttack = attack + shortSwordBonus + swordBonus;
            }
            if (cloakCheck)
            {
                totalDefense = defense + cloakBonus;
            }
            CombatRound(enemyIndex);
        }
    }

    bool IsAlive()
    {
        return health > 0;
    }

    int AddGold(int amount)
    {
        return gold += amount;
        
    }

    int RemoveCoin(int coinCleanse)
    {
        return cursedCoin -= coinCleanse;
    }
    
    int RollDice()
    {
        return Random.Range(1, 21);
    }

    int RollDice(int sides)
    {
        return Random.Range(1, (sides+1));
    }

    int RollDice(int min, int max)
    {
        return Random.Range(min, (max+1));
    }

    void ReportCondition(int health)
    {
        if (!IsAlive())
        {
            Debug.Log("You collapse to your sustained wounds");
            playerAlive = false;
            inCombat = false;
        }
        else if (health <= (maxHealth / lowHealthThreshold))
        {
            Debug.Log("You are badly wounded");
        }
        else if (health < maxHealth)
        {
            Debug.Log("You are wounded, but carry on");
        }
    }
    
    void Heal(int heal = 10)
    {
        health += heal;
        if (health > maxHealth)
        {
            health = maxHealth;
        }
    }

    int TurnPass(int count = 1)
    {
        return turnCount += count;
    }

    void Start()
    {

        ShowTitle();
        

        Debug.Log("Welcome, " + playerName + ". Your escape begins."); // replace Hero with the name of your player.

        Debug.Log("Map of the dungeon:");
        foreach (string name in roomNames) 
        {
            Debug.Log(name);
        }

        Debug.Log("Starting Loot: " + loot[4]);
        openInvSlot++;
        inventory[openInvSlot] += loot[4];

        RemoveCoin(1);
        Debug.Log("You have " + cursedCoin + " cursed coins.");

        //Debug.Log("The Entrance Hall");
        currentRoom = 0;
        ShowRoom(currentRoom);
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

        Debug.Log("You find " + roomGold[0] + " gold");
        AddGold(roomGold[0]);
        Debug.Log("You have " + gold + " gold.");
        Debug.Log("You move into the next room.");


        //Debug.Log("The Guard Room");
        currentRoom = 1;
        ShowRoom(currentRoom);
        Debug.Log("The goblin jolts awake, and seeing you, makes to charge you. You snatch up the sword in a panic and prepare to defend yourself.");
        openInvSlot++;
        inventory[openInvSlot] += "rusted shortsword";

        if (roomEnemy[currentRoom] != -1)
        {
            Debug.Log("The " + enemyNames[0] + " lunges at you!");
            Combat(0);
            //for (int i = 1; ; i++)
            //{
            //    foreach (string item in inventory)
            //    {
            //        if (item == "rusted shortsword")
            //        {
            //            shortSwordCheck = true;
            //        }
            //        if (item == "polished sword")
            //        {
            //            swordCheck = true;
            //        }
            //        if (item == "cloak")
            //        {
            //            cloakCheck = true;
            //        }
            //    }
            //    if (shortSwordCheck)
            //    {
            //        totalAttack = attack + shortSwordBonus;
            //    }
            //    if (swordCheck)
            //    {
            //        totalAttack = attack + swordBonus;
            //    }
            //    if (shortSwordCheck && swordCheck)
            //    {
            //        totalAttack = attack + shortSwordBonus + swordBonus;
            //    }
            //    if (cloakCheck)
            //    {
            //        totalDefense = defense + cloakBonus;
            //    }
            //    Debug.Log("Turn " + i + ".");
            //
            //    Debug.Log("The " + enemyNames[0] + " attacks you!");
            //    goblinRoll = Random.Range(1, 21);
            //    Debug.Log("The " + enemyNames[0] + " attacks with an attack roll of " + goblinRoll + ".");
            //
            //    if (goblinRoll >= armorClass)
            //    {
            //        if (goblinRoll == 20)
            //        {
            //            Debug.Log("The " + enemyNames[0] + " crits!");
            //            health -= ((enemyAttack[0] * 2) - totalDefense);
            //        }
            //        else
            //        {
            //            Debug.Log("The " + enemyNames[0] + " hits!");
            //            health -= (enemyAttack[0] - totalDefense);
            //        }
            //    }
            //    else
            //    {
            //        Debug.Log("The " + enemyNames[0] + " misses...");
            //    }
            //
            //    Debug.Log("You have " + health + " HP!");
            //
            //    if (health <= 0)
            //    {
            //        Debug.Log("You collapse to your sustained wounds");
            //        playerAlive = false;
            //        break;
            //    }
            //    else if (health <= (maxHealth / 4))
            //    {
            //        Debug.Log("You are badly wounded");
            //    }
            //    else if (health < maxHealth)
            //    {
            //        Debug.Log("You are wounded, but carry on");
            //    }
            //
            //    Debug.Log("You attack the " + enemyNames[0] + "!");
            //    attackRoll = Random.Range(1, 21) + totalAttack;
            //    Debug.Log("You attack with an attack roll of " + attackRoll + ".");
            //
            //    if (attackRoll >= enemyArmorClass[0])
            //    {
            //        if (attackRoll >= 20)
            //        {
            //            Debug.Log("You crit!");
            //            enemyHealth[0] -= ((totalAttack * 2) - enemyDefense[0]);
            //        }
            //        else
            //        {
            //            Debug.Log("You hit!");
            //            enemyHealth[0] -= (totalAttack - enemyDefense[0]);
            //        }
            //        if (enemyHealth[0] <= 0)
            //        {
            //            Debug.Log("You defeat the " + enemyNames[0] + "!");
            //            break;
            //        }
            //    }
            //    else
            //    {
            //        Debug.Log("You miss...");
            //    }
            //
            //    Debug.Log("The " + enemyNames[0] + " has " + enemyHealth[0] + " HP!");
            //
            //}
        }

        if(!IsAlive())
        {
            health = 1;
        }
        Debug.Log("You find " + roomGold[currentRoom] + " gold");
        AddGold(roomGold[currentRoom]);
        Debug.Log("You have " + gold + " gold.");
        Debug.Log("You move to the next room.");

        //Debug.Log("The Troll Cave");
        currentRoom = 2;
        ShowRoom(currentRoom);
        if (roomEnemy[currentRoom] != -1)
        {
            Debug.Log("An " + enemyNames[1] + " in the back of the room hears you enter, and charges at you!");
            Combat(1);
        }

        Debug.Log("You find " + roomGold[currentRoom] + " gold");
        AddGold(roomGold[currentRoom]);
        Debug.Log("You have " + gold + " gold.");

        currentRoom = 3;
        ShowRoom(currentRoom);

        if (roomEnemy[currentRoom] != -1)
        {
            Debug.Log("A great spider descends from a hole in the roof of the cave!");
            Combat(2);
            //int spiderPoison = 0;
            //
            //for (int i = 1; ; i++)
            //{
            //    foreach (string item in inventory)
            //    {
            //        if (item == "rusted shortsword")
            //        {
            //            shortSwordCheck = true;
            //        }
            //        if (item == "polished sword")
            //        {
            //            swordCheck = true;
            //        }
            //        if (item == "cloak")
            //        {
            //            cloakCheck = true;
            //        }
            //    }
            //    if (shortSwordCheck)
            //    {
            //        totalAttack = attack + shortSwordBonus;
            //    }
            //    if (swordCheck)
            //    {
            //        totalAttack = attack + swordBonus;
            //    }
            //    if (shortSwordCheck && swordCheck)
            //    {
            //        totalAttack = attack + shortSwordBonus + swordBonus;
            //    }
            //    if (cloakCheck)
            //    {
            //        totalDefense = defense + cloakBonus;
            //    }
            //    Debug.Log("Turn " + i + ".");
            //
            //    if (spiderPoison > 0)
            //    {
            //        Debug.Log("Icy poison runs in your veins...");
            //        spiderPoison -= 1;
            //        Debug.Log("You take 2 damage!");
            //        health -= 2;
            //        if (health <= 0)
            //        {
            //            Debug.Log("You collapse to your sustained wounds");
            //            playerAlive = false;
            //            break;
            //        }
            //        else if (health <= 5)
            //        {
            //            Debug.Log("You are badly wounded");
            //        }
            //        else if (health < 20)
            //        {
            //            Debug.Log("You are wounded, but carry on");
            //        }
            //    }
            //
            //    Debug.Log("The " + enemyNames[2] + " attacks you with its venomous bite!");
            //    spiderRoll = Random.Range(1, 21);
            //    Debug.Log("The " + enemyNames[2] + " attacks with an attack roll of " + spiderRoll + ".");
            //
            //    if (spiderRoll >= armorClass)
            //    {
            //        spiderPoison += 3;
            //        if (spiderRoll == 20)
            //        {
            //            Debug.Log("The " + enemyNames[2] + " crits!");
            //            health -= ((enemyAttack[2] * 2) - totalDefense);
            //        }
            //        else
            //        {
            //            Debug.Log("The " + enemyNames[2] + " hits!");
            //            health -= (enemyAttack[2] - totalDefense);
            //        }
            //    }
            //    else
            //    {
            //        Debug.Log("The " + enemyNames[2] + " misses...");
            //    }
            //
            //    Debug.Log("You have " + health + " HP!");
            //
            //    if (health <= 0)
            //    {
            //        Debug.Log("You collapse to your sustained wounds");
            //        playerAlive = false;
            //        break;
            //    }
            //    else if (health <= 5)
            //    {
            //        Debug.Log("You are badly wounded");
            //    }
            //    else if (health < 20)
            //    {
            //        Debug.Log("You are wounded, but carry on");
            //    }
            //
            //    Debug.Log("You attack the " + enemyNames[2] + "!");
            //    attackRoll = Random.Range(1, 21) + totalAttack;
            //    Debug.Log("You attack with an attack roll of " + attackRoll + ".");
            //
            //    if (attackRoll >= enemyArmorClass[2])
            //    {
            //        if (attackRoll >= 20)
            //        {
            //            Debug.Log("You crit!");
            //            enemyHealth[2] -= ((totalAttack * 2) - enemyDefense[2]);
            //        }
            //        else
            //        {
            //            Debug.Log("You hit!");
            //            enemyHealth[2] -= (totalAttack - enemyDefense[2]);
            //        }
            //        if (enemyHealth[2] <= 0)
            //        {
            //            Debug.Log("You defeat the " + enemyNames[2] + "!");
            //            break;
            //        }
            //    }
            //    else
            //    {
            //        Debug.Log("You miss...");
            //    }
            //
            //    Debug.Log("The " + enemyNames[2] + " has " + enemyHealth[2] + " HP!");
            //
            //}
        }
        if (!IsAlive())
        {
            health = 1;
        }

        Debug.Log("You find " + roomGold[currentRoom] + " gold");
        AddGold(roomGold[currentRoom]);
        Debug.Log("You have " + gold + " gold.");

        Debug.Log("You find the remains of a small campfire someone left in the cave. You light it to rest from the grueling gauntlet of combat.");
        for(int i = 5; i > 0; i--)
        {
            TurnPass();
            Debug.Log("Turn " + turnCount);

            Debug.Log("The fire will remain lit for " + i + " more turns.");
            Heal(2);
            if (health == maxHealth)
            {
                Debug.Log("As the warmth envelops you, you fully heal your wounds. Your HP is now " + health);
                Debug.Log("Well rested, you put out the fire.");
                break;
            }
            Debug.Log("As the warmth envelops you, you heal for 2 HP. Your HP is now " + health);
            
            
        }

        ReportCondition(health);


        // A3: The passage forks in two. Send the hero down one of the routes and
        //     describe each one - the two routes may even rejoin at the same
        //     place further on.

        bool pathChoiceLeft = true;

        Debug.Log("You come across a fork in the hall.");
        if (pathChoiceLeft == true)
        {
            Debug.Log("You decide to go down the left hall.");

            //Debug.Log("The Mess Hall");
            currentRoom = 4;
            ShowRoom(currentRoom);
            Debug.Log("In a large alcove you find a cookfire with a large pot filled with bubbling stew. You eat from it to regain your strength.");
            Heal(10);
            Debug.Log("You find " + roomGold[currentRoom] + " gold");
            AddGold(roomGold[currentRoom]);
            Debug.Log("You have " + gold + " gold.");
            Debug.Log("You exit the room. A short while later, the hall rejoins with the other path, and you continue onwards.");
        }
        else 
        {
            Debug.Log("You decide to go down the right hall.");

            //Debug.Log("The Barracks");
            currentRoom = 5;
            ShowRoom(currentRoom);
            Debug.Log("You check the chests for anything of use, but unfortunately find nothing that would aid your escape.");
            Debug.Log("You find " + roomGold[currentRoom] + " gold");
            AddGold(roomGold[currentRoom]);
            Debug.Log("You have " + gold + " gold.");
            Debug.Log("You exit the room. A short while later, the hall rejoins with the other path, and you continue onwards.");
        }


        //Debug.Log("The Flooded Passage");
        currentRoom = 6;
        ShowRoom(currentRoom);
        Debug.Log("You walk towards the broken door, and trip over a large, rusty key. You pocket it before proceeding to the exit.");
        openInvSlot++;
        inventory[openInvSlot] += "key";
        Debug.Log("You find " + roomGold[currentRoom] + " gold");
        AddGold(roomGold[currentRoom]);
        Debug.Log("You have " + gold + " gold.");
        Debug.Log("You move into the next room.");


        //Debug.Log("The Treasure Room");
        currentRoom = 7;
        Debug.Log(roomNames[currentRoom]);

        Debug.Log("You find " + roomGold[currentRoom] + " gold");
        AddGold(roomGold[currentRoom]);
        Debug.Log("You have " + gold + " gold.");
        Debug.Log("You move into the next room.");

        //Debug.Log("The Armory");
        currentRoom = 8; 
        ShowRoom(currentRoom);

        for (int i = 0; i < inventory.Length; i++)
        {
            if (inventory[i] == "key" && playerAlive)
            {

                Debug.Log("You try the key in the chest's lock. It works!");
                Debug.Log("You open the chest, revealing a polished sword, some rations, a cloak, and some rope. You take the contents in case they prove useful.");
                openInvSlot++;
                inventory[openInvSlot] += "polished sword";

                openInvSlot++;
                inventory[openInvSlot] += "cloak";

                openInvSlot++;
                inventory[openInvSlot] += "rope";
                break;
            }
            if (i == inventory.Length - 1 && IsAlive())
            {
                Debug.Log("You have no key for the chest");
            }
            if (i == inventory.Length - 1 && !IsAlive())
            {
                Debug.Log("You unfortunately have collapsed to your wounds, and therefore don't have the strength to open the chest");
            }
        }

        Debug.Log("You find " + roomGold[currentRoom] + " gold");
        AddGold(roomGold[currentRoom]);
        Debug.Log("You have " + gold + " gold.");
        Debug.Log("You move into the next room.");



        //Debug.Log("The Merchant's Room");
        currentRoom = 9;
        ShowRoom(currentRoom);
        Debug.Log("The Merchant is selling health potions for " + potionPrice + " gold apiece. You check your humble coinpurse, which currently has " + gold + " gold coins. You're uncertain if you have enough.");
        Debug.Log("You can currently afford " + (gold / potionPrice) + "potions, which would leave your coin purse with only " + (gold % potionPrice) + " coins. Thinking better on it, you decline the mysterious merchant's offer. Better to be frugal.");
        Debug.Log("You find " + roomGold[currentRoom] + " gold");
        AddGold(roomGold[currentRoom]);
        Debug.Log("You have " + gold + " gold.");
        Debug.Log("You leave through an ornate door behind the Merchant's stall that you did not see upon entering.");


        //Debug.Log("The Broken Throneroom");
        currentRoom = 10;
        ShowRoom(currentRoom);
        Debug.Log("You see that the throne would make for a good point to secure a rope to.");
        Debug.Log("You find " + roomGold[currentRoom] + " gold");
        AddGold(roomGold[currentRoom]);
        Debug.Log("You have " + gold + " gold.");
        for (int i = 0; i < inventory.Length; i++)
        {
            if (inventory[i] == "rope")
            {
                Debug.Log("You tie one end of the rope to the throne.");
                Debug.Log("You use the rope to climb down from the large opening in the wall made by the ballista bolt, and to your freedom!");
                break;
            }
            else if(i == inventory.Length - 1)
            {
                Debug.Log("You may want to look for something to use to escape through the breach in the wall...");
            }
        }

        foreach(string item in inventory)
        {
            if(item == "")
            {
                continue;
            }
            Debug.Log(item);
        }
        Debug.Log("You escape the dungeon with " + gold + " gold!");
    }
}
