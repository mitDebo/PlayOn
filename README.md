# PlayOn
A coding challenge for PlayOn

## Description
This was a coding challenge for PlayOn that tested good and efficient ways of retrieving, storing, and organizing Star Wars data from an API

## Requirements
This solution was built using Visual Studio Code. It also uses RestSharp (https://github.com/restsharp/RestSharp), Moq, and xUnit. All of which should be available on the NuGet package manager.

If you need help setting up your Visual Studio Code environment, this post is a good primer (https://medium.com/edgefund/c-development-with-visual-studio-code-b860cc71a5ec)

## To Run
The program takes no parameters, or a single parameter. To run in Visual Studio Code, open a terminal in the program's folder, and type

`dotnet run`

or

`dotnet run <planet_name>`

You can run the tests in the test project using

`dotnet tests`

## A quick note
Originally, looking at the API, I assumed it may be possible for someone to consider a particular planet their homeworld, and also be considered a resident of a different world. After completing this challenge, though, and testing it out a few times, I could not find any examples of this being the case. As such, this program maybe does twice as much work as it probably actually needed to do, since it stores references for each planet's members that consider it a homeworld, and its residents, and these two lists are apparently always the same.

Oh well. My tests test for that possibility at least, and it does this extra work without making any additional calls to the API.