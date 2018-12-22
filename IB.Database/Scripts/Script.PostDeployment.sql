/*
Post-Deployment Script Template							
--------------------------------------------------------------------------------------
 This file contains SQL statements that will be appended to the build script.		
 Use SQLCMD syntax to include a file in the post-deployment script.			
 Example:      :r .\myfile.sql								
 Use SQLCMD syntax to reference a variable in the post-deployment script.		
 Example:      :setvar TableName MyTable							
               SELECT * FROM [$(TableName)]					
--------------------------------------------------------------------------------------
*/
:r .\Insert\Roles.sql
:r .\Insert\Users.sql
:r .\Insert\UserRoles.sql
:r .\Insert\Permissions.sql
:r .\Insert\UserPermissions.sql
:r .\Insert\BankAccounts.sql
:r .\Insert\BankCards.sql
