# Automated Finances

This project encompasses development of several finance automation, visualisation and management tools. Designed for personal (single user) use. Currently this supports:
* scraping NetBank data and preprocessing (then importing) data from NetBank as well as [ADP](https://developers.adp.com/metadata/collections/all/learn)(payroll service),
* calculating several useful statistics from the data,
* configurable categories and subcategories for expenditure, income and savings data,
* category and subcategory visualisations for expenditure, income and savings data (always more coming!),
* some dynamic control over the dataframe management to reduce hard coding labels, headers, etc... (more to come).

This project required setting up an EF class library that handles migrations. I wrote up my guide to that in [this Wiki](https://github.com/albert118/HomeNetwork/blob/master/wikis/efproject.md).


### Notes:

* Java 8+ required for tabula.
 * TODO: Find an alt. for tabula that doesn't require Java


# Startup Guide

## Frontend

To setup the frontend project, run these as typical

```
npm install

npm test
```

Then run the (React) frontend project,

```
npm start
```

