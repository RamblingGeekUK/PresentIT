# PresentIT
- [PresentIT](#presentit)
  - [About](#about)
  - [Pre-requisites](#pre-requisites)
    - [Auth0 Account](#auth0-account)
    - [SQL Server](#sql-server)
  - [Connecting to the dockerised SQL database](#connecting-to-the-dockerised-sql-database)
    - [Connect to the DB via Visal Studio](#connect-to-the-db-via-visal-studio)
    - [Connect to the DB via Visal Studio Code](#connect-to-the-db-via-visal-studio-code)
  - [Create/update appsettings.json](#createupdate-appsettingsjson)
  - [Update the database to ensure that migrations have been applied](#update-the-database-to-ensure-that-migrations-have-been-applied)
    - [via Visual Studio Package Manager Console](#via-visual-studio-package-manager-console)
  - [Auth0 changes required](#auth0-changes-required)


## About

A web application to allow the easy uploading of videos for job applications

## Pre-requisites

Everything described here presumes running via Kestral Web Server, unless specified otherwise.

In order to run this project, there are a couple of pre-requisites

### Auth0 Account

Head on over to [Auth0](https://www.auth0.com) to create an account

Once signed up, create an application called `PresentIT` using the Regular Web Applications template, grab a note of the following, we'll need to add these to our [appsettings.json](#createupdate-appsettings.json)

- ClientID 
- ClientSecret
- Domain

We also need to add the following address in the `Allowed Callback URLs` section of the application settings: `https://localhost:5001/callback`
  
### SQL Server

- SqlServer 
  Can be run easily in a docker container by running `docker run -e 'ACCEPT_EULA=Y' -e 'SA_PASSWORD=P@55Word123!' -p 1433:1433 -d mcr.microsoft.com/mssql/server:2017-CU8-ubuntu
`

If you're inclined to, you can connect to the DB via Visual Studio or Visual Studio Code

## Connecting to the dockerised SQL database

  ### Connect to the DB via Visal Studio

- Open View -> Server Explorer
- Click on the `connect to database icon on the toolbar`
- Enter the `Server name` as `localhost`, change the authentication type to `SQL Server Authentication` and provide a username of `sa` and a password to whatever you provided the password to be in the docker command above
  
  ### Connect to the DB via Visal Studio Code
  - tbc
 

## Create/update appsettings.json

We need a place to store the settings, if you're not using user secrets, then next logical place is in appsettings.json, below is an example appsettings.json which you can copy/paste and change the relevant parts

```json
{
  "ConnectionStrings": {
    "DBConection": "Data Source=localhost;User ID=sa;Password=P@55Word123!"

  },
  "Auth0": {
    "Domain": "your-auth0-domain.eu.auth0.com",
    "ClientId": "your-auth0-ClientId",
    "ClientSecret": "your-auth0-ClientSecret"
  }
}
```

## Update the database to ensure that migrations have been applied

### via Visual Studio Package Manager Console
  `Update-Database`

## Auth0 changes required

Once you've ran the application, and created a new user go to the auth0 dashboard for your application.

- Go to `User Management` -> `Roles`, and create a role of `employer`
- Go to `User Management` -> `Users`, find and click on the user you want to assign the role to, and select the `Roles` tab, click the `Assign Roles` button, select the role and click `Assign`
- Go to `Auth Pipelines` -> `Rules`, click `Create Rule`, use the `Set roles to a user` template and paste the following code
  
  ```javascript
  function (user, context, callback) {
    const namespace = 'http://schemas.microsoft.com/ws/2008/06/identity/claims/roles';
    const assignedRoles = (context.authorization || {}).roles;
    const idTokenClaims = context.idToken || {};
    
    idTokenClaims[namespace] = assignedRoles;
    callback(null, user, context);
  }
  ```