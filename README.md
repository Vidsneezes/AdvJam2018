# AdvJam2018
Adventure Jam 2018 is over, you can get the source code here!

![alt text](https://i.imgur.com/S3sKakz.png)

Link to playable game.
https://gamejolt.com/games/theitism/339989

All the code was made from scratch, with a couple snippets I borrow from preivous prototype projects.
## you probably need to remove the .vs and make your own if you want to edit scripts ##

The game is a platformer with an adventure game touch.

It uses a Interaction dialogue system based on scriptable objects. 

You can make interesting interactions with the interaction dialogue system I made.

Step 1:

Create Dialogue/DialogueData
  A DialogueData is used to store basic text branches, it has a speech node list. 
  Here you place dialogue text like so
  
    1
      MeetBoss.dialoguedata
      Node 1
        actor name : boss
        text : hello
      Node 2
        actor name : Billy
        text : Good morning
    2
      MeetBoss2.dialoguedata
      Node 1
        actor name : boss
        text : get to work!

Step 2: 
Create Dialogue/ActorResponseSheet
   A ActorResponseSheet is used to build the player interaction, it has a default response list.
   Which basically store a dialoguedata, global variable condition checks, and an event.
   
     Example
     Boss_Scene_1.actorresponsesheet
      default responses:
        2
        dialogue: MeetBoss2.dialoguedata
        globalconditions: talkedtoboss -> 1
        event : none
        1
        dialogue: MeetBoss.dialoguedata
        globalconditions: none
        event : TalkedToBoss.keyitemchangeevent
    
 Step 3:
 Create KeyItemChangeEvent
    A keyitemchangeevent are used to change global variables and/or to transport to new scenes. Multiple global vars can be changed at once via the Global Vars to Change list
    
    Example
    TalkedToBoss.keyitemchangeevent
      GlobalVarsToChange:
        talkedtoboss = 1
      scene transport = false
      scene to transport = none

Events are used to change global variables and move the game along. 

Basic run down of global variables.

Assets/Data/GlobalVariables - This scriptable object stores all variables that might be interacted with in game. Add a variable into the Variables (leave the dynamic list alone).
The dynamic list is used in runtime and the Variables list is the initial template. To reset the runtime variables to match the template variables click
Level Editor -> Build Variables.


This was made over the course of 2 weeks, while I was also designing, creating art and music.

Contact 
Website : [Vidsneezes](vidsneezes.wordpress.com)
Twitter : [@Moraleszez](https://twitter.com/Moraleszez)
