# Project 1 (VR): Into the dungeon

## About

Into the dungeon is a dungeon monster slaying game. The objective of the game is to explore each room in the dungeon and slay the monsters that are present. Each level contain different types of guns.

## Design Overview

#### Navigation via teleportation

Users will be able to navigate the dungeon via teleportation. Using the controllers users
will be able to point to the position in which they would like to teleport to.

#### Guiding audio/sound effects

Sound effects will be present in the game specificaly each weapon/item will contain their own sound effects. Such weapon include swords which will allow the user to slash the enemies. Guns in particular will make gun sounds.The monsters in the dungeon will also contain sound effects whenever they are present, and the moment the monsters are hit they would give visual and audio cues.

#### Text Instructional

In the starting room the user will be given a tutorial on how to move about the room i.e. how to use the teleporation. Additionally in the beginning room the user will be provided with a diagram of the controller mapping. Whenever the user enters a new room an instruction on how to use that weapon will appear. Once the user has read and tested the item the room will lock and monsters will come out in which the user has to slay the monsters using the item provided. This will repeat until the user has reached the final boss. Before entering the final boss the user will be able to choose the weapons of his choise to defeat the monster.

#### Instructional Signs/Wayfinders

Each room will contain an instruction on how to use the weapons as well as a visual clue where to move to get to the next room. The signs to the next room will be displayed on the center of the room.

#### Activity involving six degrees of freedom

Six degrees of freedom would be needed as the users action of attacking would be needed to be tracked. For example using a sword word require the user move or position themselves physically to swing the sword. The gun on the otherhand would require aiming that requires the user to use 6 degrees of freedom. Avoiding the enemy attack would require the user to strafe if needed.

## Development Platform

The project will be developed on Unity and will be targeting the oculus quest/rift device.

## Additional Features Considered

Considering implementing puzzles in each dungeon that would requires some kind of physics activity such as moving boulders to reveal items. Also thinking of including climbing elements to allow the user to traverse parts of the dungeon that require the user to climb to reach certain platforms.
