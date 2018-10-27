
[![NuGet](https://img.shields.io/nuget/v/ResponseMasking.AspNetCore.Filter.svg?style=popout)](https://www.nuget.org/packages/ResponseMasking.AspNetCore.Filter/)
[![Build status](https://ci.appveyor.com/api/projects/status/nrvk81jcwu6f5a3l?svg=true)](https://ci.appveyor.com/project/suadev/responsemasking-aspnetcore-filter)
[![Quality Gate](https://sonarcloud.io/api/project_badges/measure?project=suadev_ResponseMasking.AspNetCore.Filter&metric=alert_status)](https://sonarcloud.io/dashboard?id=suadev_ResponseMasking.AspNetCore.Filter)
<img width="100" src="https://sonarcloud.io/images/project_badges/sonarcloud-orange.svg" />

## Asp.net Core MVC filter attribute for response masking. 

Supports;

- Generic List
- Generic Paged List
- Plaint Text
- Complex Types (not so complex (: ))


Example for Generic List Response;

### Model

![alt text](https://github.com/suadev/ResponseMasking.AspNetCore.Filter/blob/master/SampleApi/screenshots/userModel.JPG)

### Sample Response

![alt text](https://github.com/suadev/ResponseMasking.AspNetCore.Filter/blob/master/SampleApi/screenshots/fakeData.JPG)

### Using In Controller

![alt text](https://github.com/suadev/ResponseMasking.AspNetCore.Filter/blob/master/SampleApi/screenshots/controller.JPG)

### Postman Result - http://localhost:5000/api/users

![alt text](https://github.com/suadev/ResponseMasking.AspNetCore.Filter/blob/master/SampleApi/screenshots/postman.JPG)

### Postman Result (Paged List) - http://localhost:5000/api/users/pagedlist

![alt text](https://github.com/suadev/ResponseMasking.AspNetCore.Filter/blob/master/SampleApi/screenshots/postmantPaged.JPG)

### Postman Result (Single Item) - http://localhost:5000/api/users/1

![alt text](https://github.com/suadev/ResponseMasking.AspNetCore.Filter/blob/master/SampleApi/screenshots/postmanSingle.JPG)

### Postman Result (Plain Text) - http://localhost:5000/api/users/1/citizenshipnumber

![alt text](https://github.com/suadev/ResponseMasking.AspNetCore.Filter/blob/master/SampleApi/screenshots/postmanPlainText.JPG)
