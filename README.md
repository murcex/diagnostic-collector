# PlatformDiagnosticCollector
Platform Diagnostic Collector

# Components
## sentry-database
SQL Server data collector
- T-SQL

## sentry-api
Web API for extracting Sentry data
- C#

## korekuta
Sentry data collector worker, insert data into Centrifuge
- C#

## centrifuge
Azure SQL Server database / data warehouse
- T-SQL

## sensor
DNS scanner
- C#
