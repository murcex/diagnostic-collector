# Kiroku

```
       +------------------------------+
       |                              |
       |          myFunction          |      +---------------+
       |                              |      | (N) KCopy.exe |
       | +------------+ +-----------+ |      +------------+--+
       | | myApp.exe  | |           | |                   |
       | |            | | KCopy.exe +---------+           |
       | | Kiroku.dll | |           | |       |           |
       | +-+----------+ +---------+-+ |       |           |
       |   |                      ^   |       v           v
       |   |    +------------+    |   |    +--+-----------+--+
       |   +--->+  D:\Logs   +----+   |    |                 |
       |        +------------+        |    |  Cloud Storage  |
       |                              |    |                 |
       +------------------------------+    +--------+--------+
                                                    |
+---------------+                                   |
|    Metadata   |                                   v
+-------+-------+ +-------------------+    +--------+--------+
        |         |                   |    |                 |
        +---------+  Kiroku Database  +<---+    KLoad.exe    |
        |         |                   |    |                 |
+-------+-------+ +-+---------------+-+    +-----------------+
|    KMetric    |   |   KInstance   |
+---------------+   +---------------+ +
|    KResult    |   |    KBlock     | |
+---------------+   +---------------+ |
                    |     KLog      | v
                    +---------------+

```

## Purpose
Simple end-to-end logging system.

## Components

### Kiroku Library (and KFlow)
    - Logging Library, plus KFlow to load test and verfy end-to-end set-ups
    - Provides Metric, Result, Trace, Info, Warning and Error events
    - Use as a method logger, creates "blocks" for each metod, which are children of "instances"
    - Blocks are record for time, and results
    - Can operate as a single instance "Worker" or multi-instance "Dynamic" processor

### Kiroku LogCopy (KCopy)
    - Copys KLOG files from local device to central Kiroku storage blob for later processing

### Kiroku Log Loader (KLoad)
    - Reads centralized KLOG files into a central SQL Server

### Kiroku Database
    - Azure SQL Server database

