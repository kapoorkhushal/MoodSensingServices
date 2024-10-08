Below are the specifications for the application:

Technical Specification:
Tech-stack: .Net 8.0
Design Pattern: MediatR Pattern
Architecture: Clean Architecture/Onion Architecture
DB Approach: Using Code first approach
DB: SSMS

Overview of the App:
The application is a mood sensing app, which can provide the mood Rate of a user by taking in input the Image.
Also, it stores the user location, & can provide with the list of mood frequencies at different location & closest happy location for the given input place.

It Basically Provides 3 APIs:
1. Upload Image
2. Get All Mood Frequencies
3. Get Closest Happy Location

Following assumption has been to calculate the mood:
No 3rd API is used, and a Math Random number is providing with the mood level.
The Entry is then committed into the DB.

The project is also provisioned to provide CRUD operations, providing with policy handlers, and circuit breaker policy has also been employed
The project also provides versioning of the APIs.

Below is the Scaffold command attached to update the Entities:-

Scaffold-DbContext "Server=IN-910D5S3; Database=MoodSensing; Integrated Security=SSPI; Trusted_Connection=True; TrustServerCertificate=True" Microsoft.EntityFrameworkCore.SqlServer -OutputDir ../MoodSensingServices.Application/Entities -ContextDir Context -Context "MSAContext" -Project MoodSensingServices.Infrastructure -Namespace MoodSensingServices.Application.Entities -f


Note:
1. Connection String is kept in appsetting to make in configurable. The other provision is also to provide as user-secret, but keeping in mind the easy use by the user, it's not kept as user secret.
2. The context file is in Infra layer & Entities are kept at Application layer. Somehow, due to time constraints, I was unable to figure out how to keep same the namespace of context file, so with the successful finish of scaffold command, the namespace of context is updated to 'Application.Entities' instead of 'Infrastructure.Context'. Kindly please correct it manually during run.
3. Also, postman collection has been provided for the ease of use.

