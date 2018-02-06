using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace SQ
{
    // Class will be for Interactable items such as loot, doors, switches and NPCs.

    // Interactable items will have two properties: a tag identifying that they are interactable, and a subtype. 
    // The subtype will denote whether an item is an item, door, switch, NPC or other interactable.
    // All interactables will have an 'Activate' code, which will provide functionality when executed.
    // When left clicking on interactables, left click will tell the interactable to activate and
    // all functionality will be present within the subtype of the interactable.

       
    class Interactable
    {
    }
}
