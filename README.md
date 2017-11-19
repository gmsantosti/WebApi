# WebApi
This is a test webApi project which includes unit tests and also a postman collection with automated tests pointing to azure.

# Goals
1. Provide 2 http endpoints that accepts JSON base64 encoded binary data on both endpoints;
2. The provided data needs to be diff-ed and the results shall be available on a third end point;
3. The results shall provide the following info in JSON format;

# Achieves
1. Solution was created;
2. Unit tests were created covering PostLeft, PostRight and Get methods;
3. Azure was used to deploy this application and was configured with continuous integration mapping github repository;
4. Postman was used to create and configure automated tests which contains pre-request scripts and expected results;

# Azure
* URL: http://webapiguitest.azurewebsites.net

# Postman
This postman collection is pointing to azure.

1. Import postman collection
2. Click on Runner button
3. Select imported collection, configure delay time if necessary and click on Run
4. Verify the results (17 test cases must pass)
