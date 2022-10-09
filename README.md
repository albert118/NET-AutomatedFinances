# Automated Finances

# EF Core Backend

This project required setting up an EF class library that handles migrations. I wrote up my guide to that in [this Wiki](https://github.com/albert118/HomeNetwork/blob/master/wikis/efproject.md).

You will need to update the `appsettings.json` in both the `Core` as well as the `AppHost` projects configure you database connection(s). 

# Startup Guide

## Frontend

The frontend project comprises of a few elements. Namely, the UI react SPA project "Icarus UI". This runs the frontend and watches the various 
required files. To setup and run the frontend app, change into the `frontend/icarusUI` directory and then run these as typical,

```
npm install
npm test
```

Then run the (React) frontend project,

```
npm start
```

See the *Icarus UI* README for more information.
