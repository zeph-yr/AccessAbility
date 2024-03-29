# AccessAbility
**Not all of us can play with two hands or arms, or dive to avoid walls, but that shouldn't stop any of us from being able to enjoy the game!**
**A true one-hand and sitting-friendly mod. Play any map. With only the blocks of your color-choice in their original patterns. Turn off wall-damage, Delete crouch walls, Unbreakable bombs.**

**And still keep your scores.**

This mod allows you to play the original map with blocks and other elements turned off _without forced score deduction or NF_.
I want to make this game as accessible and enjoyable for as many of us as possible. It's your game, your ability, and your definition of skill and fun :) 

Supports Left/Righthand playersettings, Multiplayer, OST/DLC, Custom and Base Campaigns, Party, Practice, and Leaderboard Score Submission! For BS 1.18.3+

<p><img src="https://github.com/zeph-yr/AccessAbility/blob/master/Screenshots/AccessAbility_5.1.0_screenshot_1.png" width="600"/></p>

## How Is This Different From AlternativePlay?
This mod *does not convert* all the blocks to one color or to dot blocks, which can make a map difficult or unpleasant to play single-handed.
AccessAbility *deletes the blocks* of a color and thus accurately represents playing the original map with only one hand.

If you want to see the whole map but play only one color, AccessAbility can set a color to vanish without counting as miss.

## New Features
**v7.0.0+** (BS 1.21.0 - BS 1.31.0+)
- **No Multiplayer Platform Movement**: Play next to your friends. Also turns off overhead score differences.
- Compatibility update for 1.31.0
  
**v6.0.0** (BS.1.21.0 to 1.29.1 only)
- **No Rotated Dots:** Removes any rotation applied on Dot blocks in V2/V3 maps and returns them to vanilla format with the bottom edge parallel to the floor. Will not affect score.

**v5.1.0** (BS.1.21.0+ only)
- Compatibility update for CJD in 1.21.0

**v5.0.0** (BS 1.20.0+ only)
- **No Arcs:** Play without these blocking your view
- **No Chains:** Chains are seamlessly replaced by single blocks
- **NoFail++:** Recover from zero energy. Overrides base game NF modifier
- **No Score Modifiers:** Play using bomb and wall features without -5% and -10%
- **Toggle for Mod:** Keep your settings and quickly turn them all on or off at once
- In Multiplayer and Party, all features can be used without affecting scores regardless if leaderboards or CustomCampaigns are installed. Play MP alongside your friends, not looking up from a mile below.
- In Local, score submission is turned off for `No Arcs` and `No Chains`. `NoFail++` and `No Score Modifiers` will not activate if SS/CC are installed. Upload to online OST/DLC leaderboard is turned off. To save scores or activate these features in Local, uninstall SS/CC.

**v4.0.0** (BS 1.20.0+ only)
- **Unmodified Scores:** Play using the bomb and wall features without score deduction for local, party, multiplayer and base campaign scores if SS/CC are not installed.

**v3.0.0** (≤BS 1.19.0)
- **Indestructible Bombs:** Enjoy the visual effect of bombs. Sabers will pass through them
- **Friendly Walls:** Play with the visual and haptic effects of walls without needing to physically avoid them
- **No Crouch Walls:** Play with only full-height walls. All crouch-height walls are deleted regardless if crouching is necessary to avoid them. Can be combined with `Friendly Walls`
- These features when enabled will override behavior of base game wall and bomb modifiers

## How To Use
- Place AccessAbility.dll in your Plugins folder
- Set the toggles to what is comfortable for you and have fun!
- Adjust `Distance` slider for how near the blocks are to you when they vanish. 6.0 keeps them out of saber-reach. At 3.0 and below, score submission is automatically turned off in Local if SS or CC installed
- Choose how you want your scores to be counted. **Score submission is on by default (see below)**
- Requires BSML and BS Utils

## Scores
- **In-Map:** Only the color you are playing will be scored :)
- **`No Score Modifiers`**: Multiplayer and Party scores will not be affected by `Friendly Bombs`, `Friendly Walls`, or `No Crouch Walls`. Upload to online OST/DLC leaderboards is turned off. This feature will only activate in Local (even if toggled on) if leaderboards and CustomCampaigns are not installed.
- **Results Page and Submission:** If submitting, your passing score will be as if turned-off or disappearing blocks were 0's. Rank and FC is for the color you played :) You can turn off submission and keep your BeatSavior and SliceVisualizer Data but local scores and replays will not be saved. `Friendly Walls` or `Turn Off Crouch Walls` subtracts 5% from score like the base game No Obstacles modifier. `Indestructable Bombs` subtracts 10%.
- **`Turn Off Score Submission`**: Only applies when _one or more mod features are enabled_. If you want to turn off submission across your game, see my other mod [PlayFirstSubmitLater](https://github.com/zeph-yr/PlayFirstSubmitLater)

## Play One Handed Dodge-Cube
- Set `Distance` to 0 for the color you're not playing, then try not to hit those blocks as you play the color you want! ^^

## Quick Demo
<b>Scoring Options</b><br>
<img src="https://github.com/zeph-yr/AccessAbility/blob/master/Screenshots/AccessAbility_4.0.0_menu_2.png" width="500"/><br><br>
<b>Map played with red blocks turned off. Only the blue blocks are being scored</b><br>
<img src="https://github.com/zeph-yr/AccessAbility/blob/master/Screenshots/AccessAbility_screenshot_3A_small.png" width="500"/><br><br>
<b>Map played with disappearing red blocks. Again, only the blue blocks are being scored</b><br>
<img src="https://github.com/zeph-yr/AccessAbility/blob/master/Screenshots/AccessAbility_screenshot_5_small.png" width="500"/><br><br>
<b>Final score counts turned-off and disappearing blocks as 0's but FC and letter Rank is not affected</b><br>(disabled score is from other mods irrelevant to the example)<br>
<img src="https://github.com/zeph-yr/AccessAbility/blob/master/Screenshots/AccessAbility_screenshot_4_small.png" width="500"/><br><br>
<b>Modifiers are applied when using bomb and wall features in Local when SS or CC are installed</b><br>
<img src="https://github.com/zeph-yr/AccessAbility/blob/master/Screenshots/AccessAbility_2.0.0_screenshot_3.png" width="500"/><br><br>
<b>Map played with No Score Modifiers, Friendly Bombs, and Friendly Walls in Party. Notice NB and NO are not registered and -15% was not applied to score</b><br>
<img src="https://github.com/zeph-yr/AccessAbility/blob/master/Screenshots/AccessAbility_4.0.0_score_2.png" width="500"/><br>

## About
Copyright © 2021 - 2023 Zephyr | www.xephai.com

Not all disabilities are visible or obvious. And that doesn't make them any less valid 💖 Don't let others tell you what you can do.
<br>Thank you to all of you who shared your stories and appreciation with me. It means a lot to know this mod has brought joy to others.
<br>
