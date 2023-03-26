## Candidate Brief 

You are a software engineer for a company that sells cars online.
The company website uses an internal repository to manage its inventory of cars which is provided in BizCover.Repository.Cars.dll.

The following methods are provided in the third party datasource "BizCover.Repository.Cars.CarRepository"

* GetAllCars
* Add
* Update

You will need to write a .Net REST API (using C#.Net) which will be used by the company website to consume this repository. 

This should be production ready code that can be supported!

This service should be able to perform the following:
1. Insert new car
2. Update existing car.
3. Calculate discount on a list of cars to purchase according to the rules mentioned below.

Discount calculation rule:
1. If total cost exceeds $100,000 apply 5% discount
2. If number of cars is more than 2, apply 3% discount
3. If a car is built before January 2000, discount it by 10% 
4. The above rules are cumulative (i.e. all of them can be applicable at the same time)

#### NOTES

Please upload the completed test to https://www.dropbox.com/request/VYQbQMXUuXUy2eISGVjB (dont need to include .net dependencies/packages)

We can't provide any more information, so please make the relevant assumptions. 
If you like, you can include documentation/readme on these assumptions.


#### HINTS

1. Follow the brief and the requirements!

2. You should use this opportunity to demonstrate that you have the technical knowledge to be successful in a senior engineer.
Remember that a senior engineer will be a person who is not just an "order taker" that does the work, but is someone who can provide input to design decisions, follow best practices, perform peer reviews, and mentoring other developers.