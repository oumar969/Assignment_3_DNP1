# Oumar-Assignment_3_DNP1


Minimum requirement

This assignment is open-ended, meaning that the minimum requirements are few, but there is plenty of room to expand upon the assignment, if you wish to challenge yourself. See optional suggestions in the next section.

As a minimum, you must be able to:

1. Register, log in, and out

2. Create a new post (when logged in), containing a title and a body (create a new page for this, when you have learned Blazor)

3. View all posts, i.e. just a Title (either use the front page or create a new page, when you have learned Blazor)

4. Click on a post in the post overview, to go to a new page, where you can view the entire post (when you have learned Blazor).

So, what you can start with, is do the Web API part, which needs to e.g. handle POST requests for user and post, and GET requests for a list of post headers, and GET request for a single post.

In the second part, you have been introduced to Blazor, and can do the pages.

Assignment 2

In this second part you create a Blazor WASM client. Assignment 1 covered the Web API, so we are now ready to add a client as well.
Minimum requirements

You should be able to support the minimum required features mentioned in Assignment 1:

1. Register, log in, and out

2. Create a new post (when logged in), containing a title and a body (create a new page for this)

3. View all posts, i.e. just a Title (either use the front page or create a new page)

4. Click on a post in the post overview, to go to a new page, where you can view the entire post.

Class Diagram:

![Class Diagram0](https://github.com/oumar969/Assignment1-2_DNP1/assets/114076085/f0f358ac-070e-4931-9837-063a7a8ff68f)

Assignment3

Code changes

The new EfcDataAccess component will have classes similar to the JsonDataAccess. You will have:

· A ForumContext class (or call it whatever), inheriting from DbContext, which defines the DbSets<> you can interact with.

· DAO implementations. These will implement the relevant DAO-interfaces from Application component. They will through their constructor receive an instance of your ForumContext

Remember:

In your WebAPI/Program.cs file you will need to change the registered services, so that the JSON-DAOs are swapped out with EFC-DAOs. Given that the Application layer just uses the DAO interfaces, and doesn’t know about the implementations, it should be fairly easy to swap out the JSON for EFC.

Note:

To lessen the risk of mistakes, you might want to remove the dependency from WebAPI to JsonDataAccess.

This can be done by openening WebAPI.csproj (either open explorer or go to File System in Rider to find the file).

Formalities

You may continue to work in your group on this assignment.

This assignment must be handed in to be allowed to attend the exam.

Deadline will be posted on itslearning.

What to hand in

A link to GitHub, where you have your code.

An updated class diagram, in .svg file format.
