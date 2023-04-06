[![ASP.NET](https://img.shields.io/badge/Made%20with-ASP.NET-blueviolet)](https://dotnet.microsoft.com/en-us/apps/aspnet/apis) 

# E-commerce server

A project in the PBL3 (Project Based Learning #3) course, Da Nang University of Science Technology.


## Features 

- Product management for sellers 
- Calculate shipping fee price 
- Order processing for buyers 
- Compare price to other e-commerce platforms

## Run Locally

Clone the project

```bash
  git clone https://github.com/huynamboz/e-commerce-server.git
```

Checkout to branch "develop"

```bash
  git checkout develop
```

Run database migrations and seeds

```bash
  dotnet ef database update
```

Start the server

```bash
  dotnet run
```


## Environment Variables

To run this project, you will need to add the following environment variables to your .env file

`CONNECTION_STRING`: database connection string

`JWT_SECRET`
`EXPIRE_MINUTE`
`EXPIRE_DAY`: JWT related

`WORK_FACTOR`: password hashing

`CLIENT`: CORS allowance


## Authors

- [@phamhongphuc1403](https://github.com/phamhongphuc1403)
- [@huynamboz](https://github.com/huynamboz)
- [@redhoes](https://github.com/redhoes)

