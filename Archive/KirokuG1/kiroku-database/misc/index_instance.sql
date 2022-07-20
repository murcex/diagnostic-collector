CREATE NONCLUSTERED INDEX [index_Instance_instanceid] ON [dbo].[tbl_Kiroku_KInstances]
(
	[ui_instanceid] ASC
)
INCLUDE ([dt_instancestart],
[ui_applicationid]) 
WITH (PAD_INDEX = OFF, 
STATISTICS_NORECOMPUTE = OFF, 
SORT_IN_TEMPDB = OFF, 
DROP_EXISTING = OFF, 
ONLINE = OFF, 
ALLOW_ROW_LOCKS = ON, 
ALLOW_PAGE_LOCKS = ON)
GO