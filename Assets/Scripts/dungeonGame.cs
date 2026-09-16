using UnityEngine;

public class dungeonGame : MonoBehaviour
{

}

// ==========================================================================
// THE DUNGEON - Homework (Intro to C#  +  Variables & Operators)
// --------------------------------------------------------------------------
// One growing program - keep building this SAME file.
//   PART A: do after the INTRO lecture (uses only Debug.Log).
//   PART B: finish after the VARIABLES lecture (variables & operators).
// Attach to an empty GameObject and press Play to test as you go.
// ==========================================================================
public class DungeonGame : MonoBehaviour
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
        int health = 20;
        int attack = 0;
        int agility = 1;
        bool hasKey = false;
        Debug.Log("Welcome, " + playerName + ". Your escape begins."); // replace Hero with the name of your player.

        Debug.Log("");
        Debug.Log("The Entrance Hall");
        Debug.Log("A torch flickers on the wall. A stone doorway leads north.");
        Debug.Log("You move into the next room.");

        int goblinHealth = 15;
        int goblinAttack = 1;

        Debug.Log("");
        Debug.Log("The Guard Room");
        Debug.Log("A rusty sword rests on a table. A goblin snores in the corner.");
        Debug.Log("You move into the next room.");

        // ======================================================================
        // PART A  -  after the INTRO lecture (Debug.Log only)
        // ======================================================================

        // TODO A1: FIX THE BROKEN ROOM below. It has bugs that stops the program
        //          from running. Un-comment the lines, find the bug(s), fix it,
        //          and add a // comment saying what was wrong.
        
        Debug.Log("The Flooded Passage"); // line missing closing semi-colon
        Debug.Log("Ankle-deep water fills the hall. A broken door is at the end of the hallway.");
        Debug.Log("You walk towards the broken door, and trip over a large, rusty key. You pocket it before proceeding to the exit.");
        hasKey = true;
        Debug.Log("hasKey:" + hasKey);
        Debug.Log("You move into the next room.");

        // TODO A2: write at least one of your OWN room - a Room Name line,
        //          a description line, and a line describing how you exit. 
        Debug.Log("The Armory");
        Debug.Log("This room is filled with containers and storage racks for weapons and armor. Unfortunately, they are mostly empty, save for a small but well-maintained short sword, and a rope, which you take.");
        attack += 1;
        Debug.Log("Your attack has increased to " + attack + ".");
        bool hasRope = true;
        Debug.Log("hasRope: " + hasRope);
        Debug.Log("You move into the next room.");
        // TODO A3: write the EXIT room - a final "room" and description that leads the
        //          player out of the dungeon.
        Debug.Log("The Broken Throneroom");
        Debug.Log("A large dais with an imposing throne dominates the room, where the lord of this fortress presumably ruled from. 'Ruled,' as an oversized balista bolt has pierced the wall behind the throne and impaled its occupant.");
        Debug.Log("You see that the throne would make for a good point to secure the rope to.");
            if(hasRope == true)
        {
            Debug.Log("You tie one end of the rop to the throne.");
            hasRope = false;
            Debug.Log("hasRope: " + hasRope);
        }
        Debug.Log("You use the rope to climb down from the large opening in the wall made by the ballista bolt, and to your freedom!");

        // ======================================================================
        // PART B  -  after the VARIABLES lecture (variables & operators)
        // ======================================================================

        // TODO B2: FIX THE BROKEN ROOM below. It has bugs that stops the program
        //          from running. Un-comment the lines, find the bug(s), fix it,
        //          and add a // comment saying what was wrong.
        int gold = 15;
        Debug.Log("The Treasure Room"); // string was missing quotation marks
        Debug.Log("It seems this room has been raided. You find " + 5000 + " gold."); // wow that's a lot of gold! no programming bug, however
        gold += 5000;
        Debug.Log("You move into the next room."); // line was missing closing semicolon and string was missing closing quotation mark

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
    }
}
