# DispatchScripts

A collection of scripts to help with Dispatch actions in NoPixel

## LINQPad Scripts

### Requirements

All of these scripts require LINQPad. You can download it from here:

[Download LINQPad](https://www.linqpad.net/)

### Scripts

* **Radio Channels:** Simple interface to enter what's going on with various channels. The second input is the number of dispatchers on each channel.
* **Report Builder:** Interface to build a report using the standard format. Make sure you edit the first line of the script to change it to your name/callsign.

## Macros

### Requirements

All of the files in the `Macros` folder require a Stream Deck with the Super Macros plugin by BarRaider.

### Scripts

* **13A.supermacro:** Will spam 311 3 times with the 13A template specified in `13A.txt` along with the `Channel` variable and `Location` variable
* **78.supermacro:** Does the same as the above 13A template, but for 78.
* **Channel.supermacro:** Sets the `Channel` variable
* **Date.supermacro:** Writes the `Date` variable
* **Location.supermacro:** Sets the `Location` variable
* **SetDate.supermacro:** Sets the `Date` variable

### Setup

To use these files, assign a button on your Stream Deck to the Super Macro action. Make sure `Load macros from files` is checked and pick the file you want to load.

Make sure the last option: `Don't treat "New Line" as Enter` is checked.

Both `13A.supermacro` and `78.supermacro` have a reference to the files. This needs to be an absolute reference, so change the path in those files to suit where you have them.

An example:

![Example](https://i.imgur.com/KwANGta.png)
