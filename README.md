#Outdated
The CarX devs feel the need to use something called "il2cpp", which to me always implies that they have something to hide. Since this is the case this mod will no longer work with the public main branch of the game. That being said, there is a "moddable" beta branch available and while this branch undoes the il2cpp bullshit there's no guarentee this mod will still work. I would check myself but whenever I load CarX with BepInEx to mod the game, the game just tells me steam isnt loaded. 

CarX devs, if you're reading this, your decision to use il2cpp speaks volumes. 

## CarX Real Car Names
Replaces CarX car names with their real life counterparts.

## Info
> is there a mod available to get the actual names of the cars  
instead of the off brand aldi versions  
~ Adthefum1#9823

Yes! This mod aims to replace all the car names with their actual real life counterparts.

All names are pulled from the `names.txt` file within this repo, the mod will fetch the file on each game start so in the event that names are changed or added, all you should need to do is restart your game. You should only ever redownload the mod if the changelog specifies it's a mod update and not a name update.

## Installation
1) Download the [latest release](https://github.com/Hi-ImKyle/CarX-Real-Car-Names/releases/latest)
2) Place `CarNames.dll` in `CarX Drift Racing Online\BepInEx\plugins`
3) Open CarX Drift Racing

## Config

```ini
[Data]

## Override Key/Value Pairs of car names, left is in game car name, right is irl car name.
# Setting type: String
# Default value: 
JsonData = {"Horizon GT4": "Nissan Skyline", "Ingame Car Name": "Irl Car Name"}

```

## Screenshots
![image](https://user-images.githubusercontent.com/25551312/120836965-15e72680-c55e-11eb-9acf-1a9f299b8f2d.png)
![image](https://user-images.githubusercontent.com/25551312/120837026-24cdd900-c55e-11eb-83e2-e1e601af6200.png)
