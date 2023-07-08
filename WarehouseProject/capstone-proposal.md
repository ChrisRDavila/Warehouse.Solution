## Capstone Proposal

## Name of Student:
# Chrisopher Davila

## Name of Project:
# Warehouse Project Solution

## Projects Purpose:
# This is A C# MVC framework project for a real company to set up a inventory system to catalogue and track product movement and staging in various warehouses

## MVP
# Product will allow consumer to create pick sheets to locate products for fullfillment, give a list of available products at current wherehouse and a list of products staged at other warehouses to fullfill orders. User should be able to log in securely create aithentic List based on User profile that can only be used by said User.  

## Technologies, framework, libraries used for MVP

* _dot net 6_
* _cshtml_
* _C#_
* _ASP Core MVC_
* _MS Build_
* _MS identity Core_


## Spread Goals
# I hope to also create a sales dept that directly changes inventory with sales instead of updating product. I would also like to connect to an Currency/Crypto exchane API to calculate costs based on international denominations and current rates of crypto currency. Site should be hosted by Azure, have multiple step authentication.  Would also like to connect it to Carrier API to schedule delivery or recieve shipments. Should be able to scan products into warehouse inventory based on Bar code, or update inventory with FTP, schedule auto updates to invetory relating to FTP.

## Setup/Installation Requirements

<!-- Going forward, don't forget to include setup instructions in your README for an appsettings.json with a database connection string. -->

* _1. Clone this repo._
* _X. _dotnet add package MySqlConnector -v 2.2.0_
* _8. _create the file appsettings.json, and what code to include in it. We recommend using the above formatting and directing users to replace [YOUR-USERNAME-HERE] and [YOUR-PASSWORD-HERE] with the user's own user and password values. also add [YOUR-DB-NAME] with database used_
* _this format -> 
<!-- {
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost;Port=3306;database=[YOUR-DB-NAME];uid=[YOUR-USER-HERE];pwd=[YOUR-PASSWORD-HERE];"
  }
} -->
* _2. Open your terminal (e.g., Terminal or GitBash) and navigate to this project's production directory called "ProjectFile"._
* _3. In the command line, run the command `\$ dotnet run` to compile and execute the console application. Since this is a console application, you'll interact with it through text commands in your terminal._
* _4. Optionally, you can run `\$ dotnet build` to compile this console app without running it._
* _5. Use `\$ dotnet test run` in the Test directory to run test on the application_
* _6. use `\$ dotnet watch run` to cycle the server_
* _7. use `\$ dotnet watch run --launch-profile "production"` to run in production mode_


## Known Bugs

* _Any known issues_
* _should go here_

## License
[MIT](https://yourlicesnepage)
