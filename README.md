## EN
# About the project.
This project is a mix of test task from company and a pet project.
Also, I will say that **this project** is cool, because I used modern backend patterns here. **Patterns**: _business layer_, _repository layer_, _controller layer_, _data-access layer_, _dependency injection_

Why I've implemented that? So, in the beginning of writing project, I didn't have any idea why I am using it, but after a couple of errors I found that this way of writing backend web api architecture is powerful.
It makes code more safety to write.

### This project contains a couple of features such as: 
    
#### -    Cookie authentication, 
#### -    Match contacts by same contact, 
#### -    And also self-oriented many-to-many relationship *(**Tree architecture**)*.


So, probably I will in next notes write about Deploying, Dependencies in this project. Then, I will write about functionality of this project

### Stack:
 - **C# 12.0**, 
 - **.NET 8 core**, 
 - **Asp.Net 8 core**, 
 - **EntityFramework 8.0.8**, 
 - **MySQL 8.0.39**

### Dependencies:
```xml 
<ItemGroup>
    <PackageReference Include="Microsoft.EntityFrameworkCore" Version="8.0.8" />
    <PackageReference Include="Microsoft.EntityFrameworkCore.Abstractions" Version="9.0.0-preview.7.24405.3" />
    <PackageReference Include="Microsoft.EntityFrameworkCore.Design" Version="8.0.8">
        <PrivateAssets>all</PrivateAssets>
        <IncludeAssets>runtime; build; native; contentfiles; analyzers; buildtransitive</IncludeAssets>
    </PackageReference>
    <PackageReference Include="Pomelo.EntityFrameworkCore.MySql" Version="8.0.2" />
    <PackageReference Include="Swashbuckle.AspNetCore" Version="6.4.0" />
</ItemGroup>
```

