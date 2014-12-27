# What's this?

This is my final project for my Business Applications class. It's a complete system for cashless payment. It consists of:

* An ASP.NET administration back-end, which can be used to create new organisations in the management database
* An organisation management WPF application, which uses REST calls to communicate with the back-end, authorized using OAuth
* A register, which sellers can use to sell products
* A client application where new clients can register using their eID to register or log in

# Using a Belgian eID

This project uses eID cards for registering clients. To try it out:

1. Use the [Quickinstall](http://eid.belgium.be/en/using_your_eid/installing_the_eid_software/)
1. Install the [middleware SDK, version 3.5.x](http://eid.belgium.be/en/developing_eid_applications/eid_software_development_kit/)
1. Copy the file `beid35libCS_Wrapper.dll` (found in `Belgium Identity Card SDK\beidlib\dotNet\bin`) to the `Bin\Debug` folders of the WPF projects

# My thoughts

## Database schema

While we had to figure out a lot on ourselves, we had some rules imposed upon us, the biggest one being the database layout. I found it rather annoying to work with. There's one administration database, which contains the details for all organisations. All organisations then have their own seperate database which stores clients, products, etc. Those seperate databases are accessed through a Web API, so no direct contact.

This lead to smelly code, like having to generate a connection string for each organisation request. But the major reason why I disliked this, was the referential integrity. For example, the company database stores all registers and the organisations they're assigned to. But each organisation database also keeps the registers assigned to them! Now if I delete a register in the company database, it'll happily let me, since it has no way of knowing what's inside the client DBs.

Other code smell includes dynamically generating new databases and users and needing a Windows service to keep the databases in sync.

Sure, I get that this containered approach makes for easier back-ups and marginally better security (only marginally, since all requests have to go through the Web API anyway). But it's just not worth it in my opinion.

My solution would have been to have one big database, for all organisations and the company. In the case of the registers mentioned above, there could be a table storing all registers, then a join table joining registers with organisations. The products table could be a big table with a FK pointing to which organisation they belong to.

## Not using Entity Framework

The time for this course was limited, so there was no time to teach us how to use the Entity Framework. This sucked hard, as we had to write all queries on our own. You get tired of it after writing all CRUD queries for 5 different classes, with all their parameters. Having worked with ORM frameworks before, I missed the ease and type safety. Try debugging for half an hour until you notice you wrote `product` instead of `products`.

## ASP.NET

While I'm glad that ASP.NET matured into a modern framework, I still have some gripes with it. The main thing is its lack of type safety. I'll probably write a blog post on that, so stay tuned
