# Diagnostic Collector
Platform Diagnostic Collector

## Components
### Sentry
- sentry-agent
    - SQL Server Agent jobs designed to collect instance data (T-SQL)
- sentry-api
    - Web API for extracting Sentry data (CSharp)
- sentry-korekuta
    - Sentry data collector, insert data into Azure SQL Server (CSharp)

### Sensor
- sensor-application
    - DNS scanner worker (CSharp)
- sensor-database
    - Sensor database (T-SQL)

### Crane
- crane-deploysql
    - T-SQL database deployment tool (CSharp)

### Kiroku
- kiroku-library
    - embedded logging reference (CSharp)