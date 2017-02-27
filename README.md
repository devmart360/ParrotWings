# ParrotWings
 ParrotWings test app

 [Documentation](Docs/)


---


-   Open your solution on Visual Studio 2013 or later.

-   Select the 'Web' project as startup project.

-   Open Package Manager Console, select 'EntityFramework' project as Default project and run the EntityFramework's 'Update-Database' command. This will create the database

-   Run the application. User name is 'admin' and password is '123qwe' as default.


---


If you want to change the database connection string, go to web.config and edit the following line:

&lt;add name="Default" connectionString="Server=localhost; Database=ParrotWings; Trusted_Connection=True;" providerName="System.Data.SqlClient" /&gt;


