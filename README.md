# Automated-Finances

This project encompasses development of several finance automation, visualisation and management tools. Designed for personal (single user) use. Currently this supports:
* an employment library that integrates several external data sources for searching, viewing and saving.
* scraping NetBank data and preprocessing (then importing) data from NetBank as well as [ADP](https://developers.adp.com/metadata/collections/all/learn)(payroll service),
* data reporting,
  * configurable categories and subcategories for expenditure, income and savings data,
  * category and subcategory visualisations for expenditure, income and savings data (always more coming!),
  * some dynamic control over the dataframe management to reduce hard coding labels, headers, etc... (more to come).

## Current Features:
### Beta release SPA
#### Job Searching Library
* Search and save jobs with a supped up API + integrations.

### Python prototype

#### Monthly overview of bank accounts
* incoming review; avg., total p/mnth, total p each week, by category:
* outgoing overview; total, avg., by categories: e.g. coffee, shopping, eating out, groceries, utilities, health, etc...
* savings overview: avg., total p/mnth, total p each week

#### Paycheck integrations from ADP payroll solutions
* last received,
* hourly and commission based stats

# Updates Overview

<strong>September 2020:</strong>

<del>
   <strike>Given my recent foray into Javascript, React and making it work with Django</strike>, I decided on a revamp to bring everything together.
 Since this project was always a report generating tool I planned to run permanently and have sent to me, having it run as webservice on a server makes sense.
</del>
<br />
<strong> July 2021:</strong> 

<ins> 
  Since the last update to these docs, I've added a React SPA to this project that implements a .NET backend. This project is slowly moving away from the original Python + extras prototype and will integrate multiple projects "under one roof".
</ins>
<br />

These are the mock ups I've developed so far for some of the reporting. Idk when i'll bother making these, as I'm busy writing the frontend and backend services for everything else...

![Income View page #1](AutomatedFinances/designSpecs/viewMockUps/Incomes%20(view)%20mock%20up/1.jpg)

![Income View page #2](AutomatedFinances/designSpecs/viewMockUps/Incomes%20(view)%20mock%20up/2.jpg)

![Income View page #3](AutomatedFinances/designSpecs/viewMockUps/Incomes%20(view)%20mock%20up/4.jpg)

![Budget View page #1](AutomatedFinances/designSpecs/viewMockUps/Budgets%20(view)%20mock%20up/1.jpg)

![Budget View page #2](AutomatedFinances/designSpecs/viewMockUps/Budgets%20(view)%20mock%20up/2.jpg)

![Budget View page #3](AutomatedFinances/designSpecs/viewMockUps/Budgets%20(view)%20mock%20up/3.jpg)


## Planned features will include:
### Reporting
#### Monthly overview of bank accounts
* opening balance : closing balance : delta
* Category and subcategory visualisations to help track progress to 
 goals and see current position!
* planned/upcoming expenditure integrations (see calendar integrations)
* user savings goals and calculator tools for interest - fee accumulations

#### Paycheck and Income Reporting
* 4-week average,
* Super: Review of fees
* Super: Investment gains/losses
* Super: Overview of investment strategy(ies)

### Integrations
#### Google
* iCal (event notification integration), Gmail (email reporting for daily blasts and notifications)
#### Seek, Indeed
* Job data for employment library.

# Misc notes:
* Java 8+ required for tabula. TODO: Find an alt. for tabula that doesn't require Java
