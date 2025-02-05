

# NewsAPI

This is an ASP.NET Core API application that retrieves top articles details from the Hacker News API.

## Prerequisites

- .NET 8 SDK
- Visual Studio 2022 or VS Code

## How to run the API:

1. **Clone the repository and change the directory to NewsAPI:**

   ```bash
   git clone https://github.com/ppcosys/NewsAPI.git
   cd NewsAPI
   ```

2. **Restore NuGet Packages:**

   ```bash
   dotnet restore
   ```

3. **Launch the application:**

   ```bash
   dotnet run
   ```

## Application Overview

The NewsAPI application uses the public Hacker News API to retrieve a requested number of top articles and detailed information about them. The main functionality is the ability to download an array of the first *n* "best stories" returned by the Hacker News API. The array is sorted by their score in descending order.

### Key Endpoint

- **GET `/api/news/best-n-stories`**  
  To get the best *n* stories, use the following GET request with the query parameter `n` set to the desired number of stories. For example:  
  ```
  GET /api/news/best-n-stories?n=100
  ```

## Additional Endpoints

- **GET `/api/news/top-stories`**  
  Retrieves a list of top story IDs.

- **GET `/api/news/story/{id}`**  
  Retrieves details about a specific story. Replace `{id}` with the actual story identifier.

## Optimization

Currently, to reduce the number of calls to the Hacker News API, the application uses a local caching mechanism using `MemoryCache` for a single instance. However, there is significant room for optimization. Below are some of the improvements that can be applied:

## Future Optimization Improvements

1. **Using the Redis Cache**  
   To achieve greater optimization of the application's performance, consider using a Redis distributed cache. Redis allows you to configure multiple caching modes and strategies to improve performance across multiple instances.

2. **Scalability and Load Balancing**  
   In the future, it is recommended to scale the application horizontally. The application can be deployed in a scalable environment (e.g., Azure App Service, AWS Elastic Beanstalk, or Kubernetes) where you can add more instances as demand increases. With distributed caching like Redis, there are many possibilities to optimize caching across multiple instances. Using a load balancer to distribute incoming requests evenly across instances can help ensure that no single instance is overloaded.

3. **Background Data Fetching**  
   Implementing background jobs to fetch and update Hacker News data at regular intervals (e.g., every hour) decouples client requests from real-time external API calls, allowing the API to serve cached or pre-fetched data.

## Security Improvements

1. **Authentication and Authorization**  
   Utilize an API key for registration and use of the API. Additionally, implement OAuth 2.0 or JWT-based authentication to secure API endpoints.

2. **Secrets Management**  
   Use secure storage for sensitive configuration data (like API keys and connection strings) by integrating with solutions such as Azure Key Vault, AWS Secrets Manager, or HashiCorp Vault.

3. **Data Validation and Sanitization**  
   Enhance protection against common attacks like injection or cross-site scripting (XSS) by implementing robust data validation and sanitization techniques.

## Future Improvements for Application Users

1. **Push Notifications**  
   Sending notifications about new articles could be a useful functionality for API users. Consider using Firebase Cloud Messaging (FCM) or SignalR for this purpose.

2. **Adding a User Interface**  
   If needed, create a user interface using a front-end framework such as Angular, React, or Vue to provide a better user experience.

---

Creating a high-quality API application is a complex task that goes beyond the points mentioned above. Over time, there are many ways to improve the API to respond to changing requirements and security threats. Developers must always be ready to introduce and implement new improvements.




Best regards,  
Piotr Przybylski


