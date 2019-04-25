# Web API prototype project
This repository stores a REST API project. It is a small prototype that is similar to a real project, that will be relevant for the back-end position.

Below is a few tasks that we have prepared for you. We only expect you to spend around 3 hours on this – not days. The most important is for us to get insight into your understanding/thinking. We ask you to do the following:

1. Fork this repo to your GitHub account and clone your fork to your machine. Run the application and get an overview over how it is working.
2. Review the code base and think about how it could be improved – especially the general structure and code patterns.
3. Choose to do some relevant changes, accompanied by inline comments explaining the change and why.
4. More overall thoughts/suggestions/ideas for the code or architecture should be added below in this README. This also gives you a chance to mention changes that you did not have time to do in the short timeframe.
5. Push and make a pull request to this repository with your changes.

----

#### Add general thoughts/suggestions/ideas here:

There is a problem with default routing and finding methods on controllers. I would need more time to find out the cause and be able to execute the code.

I would recommend to hide the domain model from the API clients. If we return directly our domain objects, any change in the domain model might break our UI or the client's applications. We could use Data Transfer Objects for this purpose.

Unit tests should be added to the solution. It is good that the solution has separate projects for different levels of the application. This helps testability.

I would pass domain objects to methods like GetMoreUserBooks(Guid userId, Guid bookId) instead of pure IDs. That would provide less coupled code.

The class BookExtended should inherit from class Book but it depends on the requirements. 
Having a system description of requirements would better help to find issues.

In this sample application using SQL Server is not posing performance issues, but in production environment SQL Server horizontal scaling is very challenging.