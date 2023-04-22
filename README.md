# NHL Analyzer - A Fantasy Hockey tool

## Table of contents
* [General Information](#general-info)
* [Technologies](#technologies)
* [Setup](#setup)
* [Road Map](#road-map)
* [Known Issues](#known-issues)

---

## General Infomation
The idea comes from Yahoo Fantasy Hockey having player rankings for a given season based on the league format (which categories your league uses). This project aims to replicate that idea and expand on it.

The aim is to get to a point where a user can choose which categories they want to have ranked and which seasons (plural or singular) they want included. 

A signed in User will be able to save their preferences and re-use them.

This is an analysis model, their are no predictions being made at this time.

See [Road Map](#road-map) for other plans going forward. 

---

## Technologies
### Project was created with:
* ASP.Net Core
* Blazor Server App
* Entity Framework - Code First Migrations
* Radzen Blazor Components - (https://www.radzen.com/blazor-components/)

### Project was created using:
* A windows machine
* Visual Studio 2022
* SQL Server 2019

---

## Setup
* Download the source code and open with Visual Studio
* The connection string is set up for SQL Server using server authentication, but uses UserSecrets to get sensitive data (See Program.cs)
* Add the 3 user secrets: 
    1. DbPassword - The password to connect using sql server authentication.
    2. DbUserId - Your SQL Server user name.
    3. DbDataSource - The name of the server you want to connect to.
* Project contains regular season player data from 2010 to 2022 season in csv form (Thank you [Roto Wire](https://www.rotowire.com/hockey/stats.php) for the data). 
    - On first start up EF will create the Database and the seed method will read in all the player data from those files. 
    - *See [Known Issues](#known-issues) for some limitations on this process*
---

## Road Map
### Rank Players based on regular season stats
- [x] Seed Regular Season Data and display stats to user
- [ ] Only show selected categories in grid
- [ ] Display player data over multiple seasons (Sum of stats for selected seasons)
- [ ] Display ranked data for player stat categories (Based on selected seaon(s))
- [ ] Rank Players based on selected stats (Using above rankins)

### Display Individual Player Data
- [ ] See a players ranking over time (Individual Category and Overall)

### Allow Users to Sign Up and save Settings
- [ ] Saved selected categories for future use and across areas based on signed in user

### Add Team Statistics and use them for analyis

---

## Known Issues
1. A player's team is set the first time the player is found in the seed method. This means a player who has been traded may not have the right team set.
2. A player traded midway through a season is only considered to have played on a single team.
3. A players position may change over time, or they may be considered to be eligible for multiple positions in fantasy hockey. Once again the seed method doesn't update position for a player after their initial creation nor does the data allow for multiple positions at this time.
