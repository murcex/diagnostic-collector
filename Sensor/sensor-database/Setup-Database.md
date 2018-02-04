# Sensor DB Setup
## Create Database

## Create SQL logins

### Deployment Login
```
CREATE LOGIN [<sensorDeploymentLoginHere>] WITH PASSWORD = '<enterStrongPasswordHere>';
ALTER ROLE [db_owner] ADD MEMBER [<sensorDeploymentLoginHere>];
GO
```

### Application Login
```
CREATE LOGIN [<sensorExecuteLoginHere>] WITH PASSWORD = '<enterStrongPasswordHere>';  
GO
```

### ReadOnly Login
```
CREATE LOGIN [<sensorReadOnlyLoginHere>] WITH PASSWORD = '<enterStrongPasswordHere>';  
GO
```
