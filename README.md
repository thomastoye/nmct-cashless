# What's this

This is my final project for my Business Applications class. It's a complete system for cashless payment. It consists of:

* An ASP.NET administration back-end, which can be used to create new organisations in the management database
* An organisation management WPF application, which uses REST calls to communicate with the back-end, authorized using OAuth
* A register, which sellers can use to sell products
* A client application where new clients can register using their eID to register or log in

# Using a Belgian eID

This project uses eID cards for registering clients. To try it out:

* Use the [Quickinstall](http://eid.belgium.be/en/using_your_eid/installing_the_eid_software/)
* Install the [middleware SDK, version 3.5.x](http://eid.belgium.be/en/developing_eid_applications/eid_software_development_kit/)
