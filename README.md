
TwitterApp
Sample project that pulls from the Twitter v2 API Sample feed and displays total tweets received as well as top 10 most popular tweets.

The project is built with .NET core 6 and EF core in memory database. 

This is basic case and for scale we would want to use some Event Bus or Messaging system together with external/separate services that would allow easier 
scalling of databases and services.

Test coverege is not ideal as this was rush project (less than a day)
Visual studio Console.WriteLine Encoding.UTF8 is not working correctly on my local machine and does not decode Chinese characters correctly. 
I did see this as non issue, but requires more research time to investigate. 
