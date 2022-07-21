CREATE NONCLUSTERED INDEX [index_KLog_logtype1] ON [dbo].[tbl_Kiroku_KLogs]
(
	[nvc_logtype] ASC
)
INCLUDE ([ui_blockid]) 
WITH (PAD_INDEX = OFF, 
STATISTICS_NORECOMPUTE = OFF, 
SORT_IN_TEMPDB = OFF, 
DROP_EXISTING = OFF, 
ONLINE = OFF, 
ALLOW_ROW_LOCKS = ON, 
ALLOW_PAGE_LOCKS = ON)
GO

CREATE NONCLUSTERED INDEX [index_KLog_logtype2] ON [dbo].[tbl_Kiroku_KLogs]
(
	[nvc_logtype] ASC
)
INCLUDE ([dt_event],
[ui_blockid],
[nvc_blockname],
[nvc_logdata]) 
WITH (PAD_INDEX = OFF, 
STATISTICS_NORECOMPUTE = OFF, 
SORT_IN_TEMPDB = OFF, 
DROP_EXISTING = OFF, 
ONLINE = OFF, 
ALLOW_ROW_LOCKS = ON, 
ALLOW_PAGE_LOCKS = ON)
GO