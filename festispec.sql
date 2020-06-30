USE master
DROP DATABASE IF EXISTS [festispec];
GO

CREATE DATABASE festispec
GO

USE festispec
GO
 IF NOT EXISTS(SELECT * FROM sys.schemas WHERE [name] = N'db_accessadmin')      
     EXEC (N'CREATE SCHEMA db_accessadmin')                                   
 GO                                                               

USE festispec
GO
 IF NOT EXISTS(SELECT * FROM sys.schemas WHERE [name] = N'db_backupoperator')      
     EXEC (N'CREATE SCHEMA db_backupoperator')                                   
 GO                                                               

USE festispec
GO
 IF NOT EXISTS(SELECT * FROM sys.schemas WHERE [name] = N'db_datareader')      
     EXEC (N'CREATE SCHEMA db_datareader')                                   
 GO                                                               

USE festispec
GO
 IF NOT EXISTS(SELECT * FROM sys.schemas WHERE [name] = N'db_datawriter')      
     EXEC (N'CREATE SCHEMA db_datawriter')                                   
 GO                                                               

USE festispec
GO
 IF NOT EXISTS(SELECT * FROM sys.schemas WHERE [name] = N'db_ddladmin')      
     EXEC (N'CREATE SCHEMA db_ddladmin')                                   
 GO                                                               

USE festispec
GO
 IF NOT EXISTS(SELECT * FROM sys.schemas WHERE [name] = N'db_denydatareader')      
     EXEC (N'CREATE SCHEMA db_denydatareader')                                   
 GO                                                               

USE festispec
GO
 IF NOT EXISTS(SELECT * FROM sys.schemas WHERE [name] = N'db_denydatawriter')      
     EXEC (N'CREATE SCHEMA db_denydatawriter')                                   
 GO                                                               

USE festispec
GO
 IF NOT EXISTS(SELECT * FROM sys.schemas WHERE [name] = N'db_owner')      
     EXEC (N'CREATE SCHEMA db_owner')                                   
 GO                                                               

USE festispec
GO
 IF NOT EXISTS(SELECT * FROM sys.schemas WHERE [name] = N'db_securityadmin')      
     EXEC (N'CREATE SCHEMA db_securityadmin')                                   
 GO                                                               

USE festispec
GO
 IF NOT EXISTS(SELECT * FROM sys.schemas WHERE [name] = N'dbo')      
     EXEC (N'CREATE SCHEMA dbo')                                   
 GO                                                               

USE festispec
GO
 IF NOT EXISTS(SELECT * FROM sys.schemas WHERE [name] = N'guest')      
     EXEC (N'CREATE SCHEMA guest')                                   
 GO                                                               

USE festispec
GO
 IF NOT EXISTS(SELECT * FROM sys.schemas WHERE [name] = N'INFORMATION_SCHEMA')      
     EXEC (N'CREATE SCHEMA INFORMATION_SCHEMA')                                   
 GO                                                               

USE festispec
GO
 IF NOT EXISTS(SELECT * FROM sys.schemas WHERE [name] = N'festispec')      
     EXEC (N'CREATE SCHEMA festispec')                                   
 GO                                                               

USE festispec
GO
 IF NOT EXISTS(SELECT * FROM sys.schemas WHERE [name] = N'sys')      
     EXEC (N'CREATE SCHEMA sys')                                   
 GO                                                               

USE festispec
GO
IF EXISTS (SELECT * FROM sys.objects so JOIN sys.schemas sc ON so.schema_id = sc.schema_id WHERE so.name = N'answer_'  AND sc.name = N'festispec'  AND type in (N'U'))
BEGIN

  DECLARE @drop_statement nvarchar(500)

  DECLARE drop_cursor CURSOR FOR
      SELECT 'alter table '+quotename(schema_name(ob.schema_id))+
      '.'+quotename(object_name(ob.object_id))+ ' drop constraint ' + quotename(fk.name) 
      FROM sys.objects ob INNER JOIN sys.foreign_keys fk ON fk.parent_object_id = ob.object_id
      WHERE fk.referenced_object_id = 
          (
             SELECT so.object_id 
             FROM sys.objects so JOIN sys.schemas sc
             ON so.schema_id = sc.schema_id
             WHERE so.name = N'answer_'  AND sc.name = N'festispec'  AND type in (N'U')
           )

  OPEN drop_cursor

  FETCH NEXT FROM drop_cursor
  INTO @drop_statement

  WHILE @@FETCH_STATUS = 0
  BEGIN
     EXEC (@drop_statement)

     FETCH NEXT FROM drop_cursor
     INTO @drop_statement
  END

  CLOSE drop_cursor
  DEALLOCATE drop_cursor

  DROP TABLE [festispec].[answer_]
END 
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE 
[festispec].[answer_]
(
   [id] int  NOT NULL Identity(1,1),
   [user_id] int  NOT NULL,
   [form_question_id] int  NOT NULL,
   [value] nvarchar(255)  NOT NULL
)
WITH (DATA_COMPRESSION = NONE)
GO
BEGIN TRY
    EXEC sp_addextendedproperty
        N'MS_SSMA_SOURCE', N'festispec.answer_',
        N'SCHEMA', N'festispec',
        N'TABLE', N'answer_'
END TRY
BEGIN CATCH
    IF (@@TRANCOUNT > 0) ROLLBACK
    PRINT ERROR_MESSAGE()
END CATCH
GO

USE festispec
GO
IF EXISTS (SELECT * FROM sys.objects so JOIN sys.schemas sc ON so.schema_id = sc.schema_id WHERE so.name = N'assignees'  AND sc.name = N'festispec'  AND type in (N'U'))
BEGIN

  DECLARE @drop_statement nvarchar(500)

  DECLARE drop_cursor CURSOR FOR
      SELECT 'alter table '+quotename(schema_name(ob.schema_id))+
      '.'+quotename(object_name(ob.object_id))+ ' drop constraint ' + quotename(fk.name) 
      FROM sys.objects ob INNER JOIN sys.foreign_keys fk ON fk.parent_object_id = ob.object_id
      WHERE fk.referenced_object_id = 
          (
             SELECT so.object_id 
             FROM sys.objects so JOIN sys.schemas sc
             ON so.schema_id = sc.schema_id
             WHERE so.name = N'assignees'  AND sc.name = N'festispec'  AND type in (N'U')
           )

  OPEN drop_cursor

  FETCH NEXT FROM drop_cursor
  INTO @drop_statement

  WHILE @@FETCH_STATUS = 0
  BEGIN
     EXEC (@drop_statement)

     FETCH NEXT FROM drop_cursor
     INTO @drop_statement
  END

  CLOSE drop_cursor
  DEALLOCATE drop_cursor

  DROP TABLE [festispec].[assignees]
END 
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE 
[festispec].[assignees]
(
   [user_id] int  NOT NULL,
   [inspection_form_id] int  NOT NULL,
   [completed] BIT default 'FALSE' 
)
WITH (DATA_COMPRESSION = NONE)
GO
BEGIN TRY
    EXEC sp_addextendedproperty
        N'MS_SSMA_SOURCE', N'festispec.assignees',
        N'SCHEMA', N'festispec',
        N'TABLE', N'assignees'
END TRY
BEGIN CATCH
    IF (@@TRANCOUNT > 0) ROLLBACK
    PRINT ERROR_MESSAGE()
END CATCH
GO

USE festispec
GO
IF EXISTS (SELECT * FROM sys.objects so JOIN sys.schemas sc ON so.schema_id = sc.schema_id WHERE so.name = N'customer'  AND sc.name = N'festispec'  AND type in (N'U'))
BEGIN

  DECLARE @drop_statement nvarchar(500)

  DECLARE drop_cursor CURSOR FOR
      SELECT 'alter table '+quotename(schema_name(ob.schema_id))+
      '.'+quotename(object_name(ob.object_id))+ ' drop constraint ' + quotename(fk.name) 
      FROM sys.objects ob INNER JOIN sys.foreign_keys fk ON fk.parent_object_id = ob.object_id
      WHERE fk.referenced_object_id = 
          (
             SELECT so.object_id 
             FROM sys.objects so JOIN sys.schemas sc
             ON so.schema_id = sc.schema_id
             WHERE so.name = N'customer'  AND sc.name = N'festispec'  AND type in (N'U')
           )

  OPEN drop_cursor

  FETCH NEXT FROM drop_cursor
  INTO @drop_statement

  WHILE @@FETCH_STATUS = 0
  BEGIN
     EXEC (@drop_statement)

     FETCH NEXT FROM drop_cursor
     INTO @drop_statement
  END

  CLOSE drop_cursor
  DEALLOCATE drop_cursor

  DROP TABLE [festispec].[customer]
END 
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE 
[festispec].[customer]
(
   [id]					int  NOT NULL Identity(1,1),
   [location_id]		int  NOT NULL,
   [name]				nvarchar(45)  NOT NULL,
   [email]				nvarchar(45)  NOT NULL,
   [telephone_number]	nvarchar(45)  NULL,
   [kvk]				nvarchar(8)  NOT NULL,
   [created_at]			datetime  NOT NULL,
   [updated_at]			datetime  NULL,
   [deleted_at]			datetime  NULL
)
WITH (DATA_COMPRESSION = NONE)
GO
BEGIN TRY
    EXEC sp_addextendedproperty
        N'MS_SSMA_SOURCE', N'festispec.customer',
        N'SCHEMA', N'festispec',
        N'TABLE', N'customer'
END TRY
BEGIN CATCH
    IF (@@TRANCOUNT > 0) ROLLBACK
    PRINT ERROR_MESSAGE()
END CATCH
GO

USE festispec
GO
IF EXISTS (SELECT * FROM sys.objects so JOIN sys.schemas sc ON so.schema_id = sc.schema_id WHERE so.name = N'event'  AND sc.name = N'festispec'  AND type in (N'U'))
BEGIN

  DECLARE @drop_statement nvarchar(500)

  DECLARE drop_cursor CURSOR FOR
      SELECT 'alter table '+quotename(schema_name(ob.schema_id))+
      '.'+quotename(object_name(ob.object_id))+ ' drop constraint ' + quotename(fk.name) 
      FROM sys.objects ob INNER JOIN sys.foreign_keys fk ON fk.parent_object_id = ob.object_id
      WHERE fk.referenced_object_id = 
          (
             SELECT so.object_id 
             FROM sys.objects so JOIN sys.schemas sc
             ON so.schema_id = sc.schema_id
             WHERE so.name = N'event'  AND sc.name = N'festispec'  AND type in (N'U')
           )

  OPEN drop_cursor

  FETCH NEXT FROM drop_cursor
  INTO @drop_statement

  WHILE @@FETCH_STATUS = 0
  BEGIN
     EXEC (@drop_statement)

     FETCH NEXT FROM drop_cursor
     INTO @drop_statement
  END

  CLOSE drop_cursor
  DEALLOCATE drop_cursor

  DROP TABLE [festispec].[event]
END 
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE 
[festispec].[event]
(
   [id]					int  NOT NULL Identity(1,1),
   [customer_id]		int  NOT NULL,
   [location_id]		int  NOT NULL,
   [event_status]		nvarchar(15)  NOT NULL,
   [payment_status]		nvarchar(15)  NOT NULL,
   [name]				nvarchar(45)  NOT NULL,
   [start_date]			datetime2(0)  NOT NULL,
   [end_date]			datetime2(0)  NOT NULL,
   [about]				nvarchar(max)  NULL,
   [created_at]			datetime  NOT NULL,
   [updated_at]			datetime  NULL,
   [deleted_at]			datetime  NULL
)
WITH (DATA_COMPRESSION = NONE)
GO
BEGIN TRY
    EXEC sp_addextendedproperty
        N'MS_SSMA_SOURCE', N'festispec.event',
        N'SCHEMA', N'festispec',
        N'TABLE', N'event'
END TRY
BEGIN CATCH
    IF (@@TRANCOUNT > 0) ROLLBACK
    PRINT ERROR_MESSAGE()
END CATCH
GO

USE festispec
GO
IF EXISTS (SELECT * FROM sys.objects so JOIN sys.schemas sc ON so.schema_id = sc.schema_id WHERE so.name = N'event_inspection'  AND sc.name = N'festispec'  AND type in (N'U'))
BEGIN

  DECLARE @drop_statement nvarchar(500)

  DECLARE drop_cursor CURSOR FOR
      SELECT 'alter table '+quotename(schema_name(ob.schema_id))+
      '.'+quotename(object_name(ob.object_id))+ ' drop constraint ' + quotename(fk.name) 
      FROM sys.objects ob INNER JOIN sys.foreign_keys fk ON fk.parent_object_id = ob.object_id
      WHERE fk.referenced_object_id = 
          (
             SELECT so.object_id 
             FROM sys.objects so JOIN sys.schemas sc
             ON so.schema_id = sc.schema_id
             WHERE so.name = N'event_inspection'  AND sc.name = N'festispec'  AND type in (N'U')
           )

  OPEN drop_cursor

  FETCH NEXT FROM drop_cursor
  INTO @drop_statement

  WHILE @@FETCH_STATUS = 0
  BEGIN
     EXEC (@drop_statement)

     FETCH NEXT FROM drop_cursor
     INTO @drop_statement
  END

  CLOSE drop_cursor
  DEALLOCATE drop_cursor

  DROP TABLE [festispec].[event_inspection]
END 
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE 
[festispec].[event_inspection]
(
   [id]				int  NOT NULL Identity(1,1),
   [event_id]		int  NOT NULL,
   [name]			nvarchar(45)  NOT NULL,
   [execution_date] datetime  NOT NULL,
   [created_at]		datetime  NOT NULL,
   [updated_at]		datetime  NULL,
   [deleted_at]		datetime  NULL
)
WITH (DATA_COMPRESSION = NONE)
GO
BEGIN TRY
    EXEC sp_addextendedproperty
        N'MS_SSMA_SOURCE', N'festispec.event_inspection',
        N'SCHEMA', N'festispec',
        N'TABLE', N'event_inspection'
END TRY
BEGIN CATCH
    IF (@@TRANCOUNT > 0) ROLLBACK
    PRINT ERROR_MESSAGE()
END CATCH
GO

USE festispec
GO
IF EXISTS (SELECT * FROM sys.objects so JOIN sys.schemas sc ON so.schema_id = sc.schema_id WHERE so.name = N'event_status'  AND sc.name = N'festispec'  AND type in (N'U'))
BEGIN

  DECLARE @drop_statement nvarchar(500)

  DECLARE drop_cursor CURSOR FOR
      SELECT 'alter table '+quotename(schema_name(ob.schema_id))+
      '.'+quotename(object_name(ob.object_id))+ ' drop constraint ' + quotename(fk.name) 
      FROM sys.objects ob INNER JOIN sys.foreign_keys fk ON fk.parent_object_id = ob.object_id
      WHERE fk.referenced_object_id = 
          (
             SELECT so.object_id 
             FROM sys.objects so JOIN sys.schemas sc
             ON so.schema_id = sc.schema_id
             WHERE so.name = N'event_status'  AND sc.name = N'festispec'  AND type in (N'U')
           )

  OPEN drop_cursor

  FETCH NEXT FROM drop_cursor
  INTO @drop_statement

  WHILE @@FETCH_STATUS = 0
  BEGIN
     EXEC (@drop_statement)

     FETCH NEXT FROM drop_cursor
     INTO @drop_statement
  END

  CLOSE drop_cursor
  DEALLOCATE drop_cursor

  DROP TABLE [festispec].[event_status]
END 
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE 
[festispec].[event_status]
(
   [status] nvarchar(15)  NOT NULL
)
WITH (DATA_COMPRESSION = NONE)
GO
BEGIN TRY
    EXEC sp_addextendedproperty
        N'MS_SSMA_SOURCE', N'festispec.event_status',
        N'SCHEMA', N'festispec',
        N'TABLE', N'event_status'
END TRY
BEGIN CATCH
    IF (@@TRANCOUNT > 0) ROLLBACK
    PRINT ERROR_MESSAGE()
END CATCH
GO

USE festispec
GO
IF EXISTS (SELECT * FROM sys.objects so JOIN sys.schemas sc ON so.schema_id = sc.schema_id WHERE so.name = N'file'  AND sc.name = N'festispec'  AND type in (N'U'))
BEGIN

  DECLARE @drop_statement nvarchar(500)

  DECLARE drop_cursor CURSOR FOR
      SELECT 'alter table '+quotename(schema_name(ob.schema_id))+
      '.'+quotename(object_name(ob.object_id))+ ' drop constraint ' + quotename(fk.name) 
      FROM sys.objects ob INNER JOIN sys.foreign_keys fk ON fk.parent_object_id = ob.object_id
      WHERE fk.referenced_object_id = 
          (
             SELECT so.object_id 
             FROM sys.objects so JOIN sys.schemas sc
             ON so.schema_id = sc.schema_id
             WHERE so.name = N'file'  AND sc.name = N'festispec'  AND type in (N'U')
           )

  OPEN drop_cursor

  FETCH NEXT FROM drop_cursor
  INTO @drop_statement

  WHILE @@FETCH_STATUS = 0
  BEGIN
     EXEC (@drop_statement)

     FETCH NEXT FROM drop_cursor
     INTO @drop_statement
  END

  CLOSE drop_cursor
  DEALLOCATE drop_cursor

  DROP TABLE [festispec].[file_link]
END 
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE 
[festispec].[file_link]
(
   [id]				int  NOT NULL Identity(1,1),
   [path]			nvarchar(255)  NOT NULL,
   [created_at]		datetime  NOT NULL,
   [updated_at]		datetime  NULL,
   [deleted_at]		datetime  NULL
)
WITH (DATA_COMPRESSION = NONE)
GO
BEGIN TRY
    EXEC sp_addextendedproperty
        N'MS_SSMA_SOURCE', N'festispec.file_link',
        N'SCHEMA', N'festispec',
        N'TABLE', N'file_link'
END TRY
BEGIN CATCH
    IF (@@TRANCOUNT > 0) ROLLBACK
    PRINT ERROR_MESSAGE()
END CATCH
GO

USE festispec
GO
IF EXISTS (SELECT * FROM sys.objects so JOIN sys.schemas sc ON so.schema_id = sc.schema_id WHERE so.name = N'form_question'  AND sc.name = N'festispec'  AND type in (N'U'))
BEGIN

  DECLARE @drop_statement nvarchar(500)

  DECLARE drop_cursor CURSOR FOR
      SELECT 'alter table '+quotename(schema_name(ob.schema_id))+
      '.'+quotename(object_name(ob.object_id))+ ' drop constraint ' + quotename(fk.name) 
      FROM sys.objects ob INNER JOIN sys.foreign_keys fk ON fk.parent_object_id = ob.object_id
      WHERE fk.referenced_object_id = 
          (
             SELECT so.object_id 
             FROM sys.objects so JOIN sys.schemas sc
             ON so.schema_id = sc.schema_id
             WHERE so.name = N'form_question'  AND sc.name = N'festispec'  AND type in (N'U')
           )

  OPEN drop_cursor

  FETCH NEXT FROM drop_cursor
  INTO @drop_statement

  WHILE @@FETCH_STATUS = 0
  BEGIN
     EXEC (@drop_statement)

     FETCH NEXT FROM drop_cursor
     INTO @drop_statement
  END

  CLOSE drop_cursor
  DEALLOCATE drop_cursor

  DROP TABLE [festispec].[form_question]
END 
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE 
[festispec].[form_question]
(
   [id]						int  NOT NULL Identity(1,1),
   [inspection_id]			int  NOT NULL,
   [type]					nvarchar(45)  NOT NULL,
   [order]					int  NOT NULL,
   [instructions]			nvarchar(max) NULL,
   [question]				nvarchar(255)  NOT NULL,
   [created_at]				datetime  NOT NULL,
   [updated_at]				datetime  NULL,
   [deleted_at]				datetime  NULL
)
WITH (DATA_COMPRESSION = NONE)
GO
BEGIN TRY
    EXEC sp_addextendedproperty
        N'MS_SSMA_SOURCE', N'festispec.form_question',
        N'SCHEMA', N'festispec',
        N'TABLE', N'form_question'
END TRY
BEGIN CATCH
    IF (@@TRANCOUNT > 0) ROLLBACK
    PRINT ERROR_MESSAGE()
END CATCH
GO

USE festispec
GO
IF EXISTS (SELECT * FROM sys.objects so JOIN sys.schemas sc ON so.schema_id = sc.schema_id WHERE so.name = N'inspection_form'  AND sc.name = N'festispec'  AND type in (N'U'))
BEGIN

  DECLARE @drop_statement nvarchar(500)

  DECLARE drop_cursor CURSOR FOR
      SELECT 'alter table '+quotename(schema_name(ob.schema_id))+
      '.'+quotename(object_name(ob.object_id))+ ' drop constraint ' + quotename(fk.name) 
      FROM sys.objects ob INNER JOIN sys.foreign_keys fk ON fk.parent_object_id = ob.object_id
      WHERE fk.referenced_object_id = 
          (
             SELECT so.object_id 
             FROM sys.objects so JOIN sys.schemas sc
             ON so.schema_id = sc.schema_id
             WHERE so.name = N'inspection_form'  AND sc.name = N'festispec'  AND type in (N'U')
           )

  OPEN drop_cursor

  FETCH NEXT FROM drop_cursor
  INTO @drop_statement

  WHILE @@FETCH_STATUS = 0
  BEGIN
     EXEC (@drop_statement)

     FETCH NEXT FROM drop_cursor
     INTO @drop_statement
  END

  CLOSE drop_cursor
  DEALLOCATE drop_cursor

  DROP TABLE [festispec].[inspection_form]
END 
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE 
[festispec].[inspection_form]
(
   [id]				int  NOT NULL Identity(1,1),
   [event_inspection_id] int  NULL,
   [Name]			nvarchar(45)  NOT NULL,
   [floor_plan]		nvarchar(255)  NULL,
   [created_at]		datetime  NOT NULL,
   [updated_at]		datetime  NULL,
   [deleted_at]		datetime  NULL
)
WITH (DATA_COMPRESSION = NONE)
GO
BEGIN TRY
    EXEC sp_addextendedproperty
        N'MS_SSMA_SOURCE', N'festispec.inspection_form',
        N'SCHEMA', N'festispec',
        N'TABLE', N'inspection_form'
END TRY
BEGIN CATCH
    IF (@@TRANCOUNT > 0) ROLLBACK
    PRINT ERROR_MESSAGE()
END CATCH
GO

USE festispec
GO
IF EXISTS (SELECT * FROM sys.objects so JOIN sys.schemas sc ON so.schema_id = sc.schema_id WHERE so.name = N'law'  AND sc.name = N'festispec'  AND type in (N'U'))
BEGIN

  DECLARE @drop_statement nvarchar(500)

  DECLARE drop_cursor CURSOR FOR
      SELECT 'alter table '+quotename(schema_name(ob.schema_id))+
      '.'+quotename(object_name(ob.object_id))+ ' drop constraint ' + quotename(fk.name) 
      FROM sys.objects ob INNER JOIN sys.foreign_keys fk ON fk.parent_object_id = ob.object_id
      WHERE fk.referenced_object_id = 
          (
             SELECT so.object_id 
             FROM sys.objects so JOIN sys.schemas sc
             ON so.schema_id = sc.schema_id
             WHERE so.name = N'law'  AND sc.name = N'festispec'  AND type in (N'U')
           )

  OPEN drop_cursor

  FETCH NEXT FROM drop_cursor
  INTO @drop_statement

  WHILE @@FETCH_STATUS = 0
  BEGIN
     EXEC (@drop_statement)

     FETCH NEXT FROM drop_cursor
     INTO @drop_statement
  END

  CLOSE drop_cursor
  DEALLOCATE drop_cursor

  DROP TABLE [festispec].[law]
END 
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE 
[festispec].[law]
(
   [id] int  NOT NULL IDENTITY(1,1),
   --[region] int  NULL,
   [location_id] int Null,
   [name] nvarchar(255)  NOT NULL,
   [content] nvarchar(max)  NOT NULL,
   [created_at]		datetime  NOT NULL DEFAULT getdate(),
   [updated_at]		datetime  NULL,
   [deleted_at]		datetime  NULL
)
WITH (DATA_COMPRESSION = NONE)
GO
BEGIN TRY
    EXEC sp_addextendedproperty
        N'MS_SSMA_SOURCE', N'festispec.law',
        N'SCHEMA', N'festispec',
        N'TABLE', N'law'
END TRY
BEGIN CATCH
    IF (@@TRANCOUNT > 0) ROLLBACK
    PRINT ERROR_MESSAGE()
END CATCH
GO

USE festispec
GO
IF EXISTS (SELECT * FROM sys.objects so JOIN sys.schemas sc ON so.schema_id = sc.schema_id WHERE so.name = N'location'  AND sc.name = N'festispec'  AND type in (N'U'))
BEGIN

  DECLARE @drop_statement nvarchar(500)

  DECLARE drop_cursor CURSOR FOR
      SELECT 'alter table '+quotename(schema_name(ob.schema_id))+
      '.'+quotename(object_name(ob.object_id))+ ' drop constraint ' + quotename(fk.name) 
      FROM sys.objects ob INNER JOIN sys.foreign_keys fk ON fk.parent_object_id = ob.object_id
      WHERE fk.referenced_object_id = 
          (
             SELECT so.object_id 
             FROM sys.objects so JOIN sys.schemas sc
             ON so.schema_id = sc.schema_id
             WHERE so.name = N'location'  AND sc.name = N'festispec'  AND type in (N'U')
           )

  OPEN drop_cursor

  FETCH NEXT FROM drop_cursor
  INTO @drop_statement

  WHILE @@FETCH_STATUS = 0
  BEGIN
     EXEC (@drop_statement)

     FETCH NEXT FROM drop_cursor
     INTO @drop_statement
  END

  CLOSE drop_cursor
  DEALLOCATE drop_cursor

  DROP TABLE [festispec].[location]
END 
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE 
[festispec].[location]
(
   [id] int  NOT NULL Identity(1,1),
   [postalcode] nvarchar(6)  NULL,
   [number] nvarchar(45)  NULL,
   [city] nvarchar(45)  NOT NULL,
   [longtitude] float(53)  NULL,
   [latitude] float(53)  NULL,
   [created_at] datetime  NOT NULL,
   [updated_at] datetime  NULL,
   [deleted_at] datetime  NULL,
   CONSTRAINT unique_location UNIQUE(postalcode, number, city)

)
WITH (DATA_COMPRESSION = NONE)
GO
BEGIN TRY
    EXEC sp_addextendedproperty
        N'MS_SSMA_SOURCE', N'festispec.location',
        N'SCHEMA', N'festispec',
        N'TABLE', N'location'
END TRY
BEGIN CATCH
    IF (@@TRANCOUNT > 0) ROLLBACK
    PRINT ERROR_MESSAGE()
END CATCH
GO

USE festispec
GO
IF EXISTS (SELECT * FROM sys.objects so JOIN sys.schemas sc ON so.schema_id = sc.schema_id WHERE so.name = N'multplechoice'  AND sc.name = N'festispec'  AND type in (N'U'))
BEGIN

  DECLARE @drop_statement nvarchar(500)

  DECLARE drop_cursor CURSOR FOR
      SELECT 'alter table '+quotename(schema_name(ob.schema_id))+
      '.'+quotename(object_name(ob.object_id))+ ' drop constraint ' + quotename(fk.name) 
      FROM sys.objects ob INNER JOIN sys.foreign_keys fk ON fk.parent_object_id = ob.object_id
      WHERE fk.referenced_object_id = 
          (
             SELECT so.object_id 
             FROM sys.objects so JOIN sys.schemas sc
             ON so.schema_id = sc.schema_id
             WHERE so.name = N'multplechoice'  AND sc.name = N'festispec'  AND type in (N'U')
           )

  OPEN drop_cursor

  FETCH NEXT FROM drop_cursor
  INTO @drop_statement

  WHILE @@FETCH_STATUS = 0
  BEGIN
     EXEC (@drop_statement)

     FETCH NEXT FROM drop_cursor
     INTO @drop_statement
  END

  CLOSE drop_cursor
  DEALLOCATE drop_cursor

  DROP TABLE [festispec].[multplechoice]
END 
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE 
[festispec].[multplechoice]
(
   [id] int  NOT NULL IDENTITY(1,1),
   [option] nvarchar(45)  NOT NULL,
   [form_question_id] int  NOT NULL
)
WITH (DATA_COMPRESSION = NONE)
GO
BEGIN TRY
    EXEC sp_addextendedproperty
        N'MS_SSMA_SOURCE', N'festispec.multplechoice',
        N'SCHEMA', N'festispec',
        N'TABLE', N'multplechoice'
END TRY
BEGIN CATCH
    IF (@@TRANCOUNT > 0) ROLLBACK
    PRINT ERROR_MESSAGE()
END CATCH
GO

USE festispec
GO
IF EXISTS (SELECT * FROM sys.objects so JOIN sys.schemas sc ON so.schema_id = sc.schema_id WHERE so.name = N'payment_status'  AND sc.name = N'festispec'  AND type in (N'U'))
BEGIN

  DECLARE @drop_statement nvarchar(500)

  DECLARE drop_cursor CURSOR FOR
      SELECT 'alter table '+quotename(schema_name(ob.schema_id))+
      '.'+quotename(object_name(ob.object_id))+ ' drop constraint ' + quotename(fk.name) 
      FROM sys.objects ob INNER JOIN sys.foreign_keys fk ON fk.parent_object_id = ob.object_id
      WHERE fk.referenced_object_id = 
          (
             SELECT so.object_id 
             FROM sys.objects so JOIN sys.schemas sc
             ON so.schema_id = sc.schema_id
             WHERE so.name = N'payment_status'  AND sc.name = N'festispec'  AND type in (N'U')
           )

  OPEN drop_cursor

  FETCH NEXT FROM drop_cursor
  INTO @drop_statement

  WHILE @@FETCH_STATUS = 0
  BEGIN
     EXEC (@drop_statement)

     FETCH NEXT FROM drop_cursor
     INTO @drop_statement
  END

  CLOSE drop_cursor
  DEALLOCATE drop_cursor

  DROP TABLE [festispec].[payment_status]
END 
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE 
[festispec].[payment_status]
(
   [payment_status] nvarchar(15)  NOT NULL
)
WITH (DATA_COMPRESSION = NONE)
GO
BEGIN TRY
    EXEC sp_addextendedproperty
        N'MS_SSMA_SOURCE', N'festispec.payment_status',
        N'SCHEMA', N'festispec',
        N'TABLE', N'payment_status'
END TRY
BEGIN CATCH
    IF (@@TRANCOUNT > 0) ROLLBACK
    PRINT ERROR_MESSAGE()
END CATCH
GO

USE festispec
GO
IF EXISTS (SELECT * FROM sys.objects so JOIN sys.schemas sc ON so.schema_id = sc.schema_id WHERE so.name = N'quotation'  AND sc.name = N'festispec'  AND type in (N'U'))
BEGIN

  DECLARE @drop_statement nvarchar(500)

  DECLARE drop_cursor CURSOR FOR
      SELECT 'alter table '+quotename(schema_name(ob.schema_id))+
      '.'+quotename(object_name(ob.object_id))+ ' drop constraint ' + quotename(fk.name) 
      FROM sys.objects ob INNER JOIN sys.foreign_keys fk ON fk.parent_object_id = ob.object_id
      WHERE fk.referenced_object_id = 
          (
             SELECT so.object_id 
             FROM sys.objects so JOIN sys.schemas sc
             ON so.schema_id = sc.schema_id
             WHERE so.name = N'quotation'  AND sc.name = N'festispec'  AND type in (N'U')
           )

  OPEN drop_cursor

  FETCH NEXT FROM drop_cursor
  INTO @drop_statement

  WHILE @@FETCH_STATUS = 0
  BEGIN
     EXEC (@drop_statement)

     FETCH NEXT FROM drop_cursor
     INTO @drop_statement
  END

  CLOSE drop_cursor
  DEALLOCATE drop_cursor

  DROP TABLE [festispec].[quotation]
END 
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE 
[festispec].[quotation]
(
	[id]						int NOT NULL Identity(1,1),
	[event_id]					int NOT NULL,
	[price]						float(53) NOT NULL,
	[quotation status_status]	nvarchar(45) NOT NULL,
	[text]						nvarchar(max)  NULL,
	[created_at]				datetime NOT NULL DEFAULT getdate(), 
	[updated_at]				datetime NULL,
	[deleted_at]				datetime NULL,
	CONSTRAINT					PK_Quotation PRIMARY KEY(id),

)
WITH (DATA_COMPRESSION = NONE)
GO
BEGIN TRY
    EXEC sp_addextendedproperty
        N'MS_SSMA_SOURCE', N'festispec.quotation',
        N'SCHEMA', N'festispec',
        N'TABLE', N'quotation'
END TRY
BEGIN CATCH
    IF (@@TRANCOUNT > 0) ROLLBACK
    PRINT ERROR_MESSAGE()
END CATCH
GO

USE festispec
GO
IF EXISTS (SELECT * FROM sys.objects so JOIN sys.schemas sc ON so.schema_id = sc.schema_id WHERE so.name = N'quotation status'  AND sc.name = N'festispec'  AND type in (N'U'))
BEGIN

  DECLARE @drop_statement nvarchar(500)

  DECLARE drop_cursor CURSOR FOR
      SELECT 'alter table '+quotename(schema_name(ob.schema_id))+
      '.'+quotename(object_name(ob.object_id))+ ' drop constraint ' + quotename(fk.name) 
      FROM sys.objects ob INNER JOIN sys.foreign_keys fk ON fk.parent_object_id = ob.object_id
      WHERE fk.referenced_object_id = 
          (
             SELECT so.object_id 
             FROM sys.objects so JOIN sys.schemas sc
             ON so.schema_id = sc.schema_id
             WHERE so.name = N'quotation status'  AND sc.name = N'festispec'  AND type in (N'U')
           )

  OPEN drop_cursor

  FETCH NEXT FROM drop_cursor
  INTO @drop_statement

  WHILE @@FETCH_STATUS = 0
  BEGIN
     EXEC (@drop_statement)

     FETCH NEXT FROM drop_cursor
     INTO @drop_statement
  END

  CLOSE drop_cursor
  DEALLOCATE drop_cursor

  DROP TABLE [festispec].[quotation status]
END 
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE 
[festispec].[quotation status]
(
   [status] nvarchar(45)  NOT NULL
)
WITH (DATA_COMPRESSION = NONE)
GO
BEGIN TRY
    EXEC sp_addextendedproperty
        N'MS_SSMA_SOURCE', N'festispec.`quotation status`',
        N'SCHEMA', N'festispec',
        N'TABLE', N'quotation status'
END TRY
BEGIN CATCH
    IF (@@TRANCOUNT > 0) ROLLBACK
    PRINT ERROR_MESSAGE()
END CATCH
GO
/*
USE festispec
GO
IF EXISTS (SELECT * FROM sys.objects so JOIN sys.schemas sc ON so.schema_id = sc.schema_id WHERE so.name = N'region'  AND sc.name = N'festispec'  AND type in (N'U'))
BEGIN

  DECLARE @drop_statement nvarchar(500)

  DECLARE drop_cursor CURSOR FOR
      SELECT 'alter table '+quotename(schema_name(ob.schema_id))+
      '.'+quotename(object_name(ob.object_id))+ ' drop constraint ' + quotename(fk.name) 
      FROM sys.objects ob INNER JOIN sys.foreign_keys fk ON fk.parent_object_id = ob.object_id
      WHERE fk.referenced_object_id = 
          (
             SELECT so.object_id 
             FROM sys.objects so JOIN sys.schemas sc
             ON so.schema_id = sc.schema_id
             WHERE so.name = N'region'  AND sc.name = N'festispec'  AND type in (N'U')
           )

  OPEN drop_cursor

  FETCH NEXT FROM drop_cursor
  INTO @drop_statement

  WHILE @@FETCH_STATUS = 0
  BEGIN
     EXEC (@drop_statement)

     FETCH NEXT FROM drop_cursor
     INTO @drop_statement
  END

  CLOSE drop_cursor
  DEALLOCATE drop_cursor

  DROP TABLE [festispec].[region]
END 
GO
*/
/*
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE 
[festispec].[region]
(
   [region] int  NOT NULL,
   [subsides_in] int  NULL,
   [name] nvarchar(45)  NOT NULL,
   [location_id] int  NOT NULL
)
WITH (DATA_COMPRESSION = NONE)
GO
BEGIN TRY
    EXEC sp_addextendedproperty
        N'MS_SSMA_SOURCE', N'festispec.region',
        N'SCHEMA', N'festispec',
        N'TABLE', N'region'
END TRY
BEGIN CATCH
    IF (@@TRANCOUNT > 0) ROLLBACK
    PRINT ERROR_MESSAGE()
END CATCH
GO
*/
USE festispec
GO
IF EXISTS (SELECT * FROM sys.objects so JOIN sys.schemas sc ON so.schema_id = sc.schema_id WHERE so.name = N'type'  AND sc.name = N'festispec'  AND type in (N'U'))
BEGIN

  DECLARE @drop_statement nvarchar(500)

  DECLARE drop_cursor CURSOR FOR
      SELECT 'alter table '+quotename(schema_name(ob.schema_id))+
      '.'+quotename(object_name(ob.object_id))+ ' drop constraint ' + quotename(fk.name) 
      FROM sys.objects ob INNER JOIN sys.foreign_keys fk ON fk.parent_object_id = ob.object_id
      WHERE fk.referenced_object_id = 
          (
             SELECT so.object_id 
             FROM sys.objects so JOIN sys.schemas sc
             ON so.schema_id = sc.schema_id
             WHERE so.name = N'type'  AND sc.name = N'festispec'  AND type in (N'U')
           )

  OPEN drop_cursor

  FETCH NEXT FROM drop_cursor
  INTO @drop_statement

  WHILE @@FETCH_STATUS = 0
  BEGIN
     EXEC (@drop_statement)

     FETCH NEXT FROM drop_cursor
     INTO @drop_statement
  END

  CLOSE drop_cursor
  DEALLOCATE drop_cursor

  DROP TABLE [festispec].[question_type]
END 
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE 
[festispec].[question_type]
(
   [question_type] nvarchar(45) NOT NULL
)
WITH (DATA_COMPRESSION = NONE)
GO
BEGIN TRY
    EXEC sp_addextendedproperty
        N'MS_SSMA_SOURCE', N'festispec.question_type',
        N'SCHEMA', N'festispec',
        N'TABLE', N'question_type'
END TRY
BEGIN CATCH
    IF (@@TRANCOUNT > 0) ROLLBACK
    PRINT ERROR_MESSAGE()
END CATCH
GO

USE festispec
GO
IF EXISTS (SELECT * FROM sys.objects so JOIN sys.schemas sc ON so.schema_id = sc.schema_id WHERE so.name = N'user'  AND sc.name = N'festispec'  AND type in (N'U'))
BEGIN

  DECLARE @drop_statement nvarchar(500)

  DECLARE drop_cursor CURSOR FOR
      SELECT 'alter table '+quotename(schema_name(ob.schema_id))+
      '.'+quotename(object_name(ob.object_id))+ ' drop constraint ' + quotename(fk.name) 
      FROM sys.objects ob INNER JOIN sys.foreign_keys fk ON fk.parent_object_id = ob.object_id
      WHERE fk.referenced_object_id = 
          (
             SELECT so.object_id 
             FROM sys.objects so JOIN sys.schemas sc
             ON so.schema_id = sc.schema_id
             WHERE so.name = N'user'  AND sc.name = N'festispec'  AND type in (N'U')
           )

  OPEN drop_cursor

  FETCH NEXT FROM drop_cursor
  INTO @drop_statement

  WHILE @@FETCH_STATUS = 0
  BEGIN
     EXEC (@drop_statement)

     FETCH NEXT FROM drop_cursor
     INTO @drop_statement
  END

  CLOSE drop_cursor
  DEALLOCATE drop_cursor

  DROP TABLE [festispec].[user]
END 
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE 
[festispec].[user]
(
   [id] int  NOT NULL IDENTITY(1,1),
   [user_role] nvarchar(10)  NOT NULL,
   [location_id] int  NULL,
   [avatar] image NULL DEFAULT null,
   [email] nvarchar(255)  NOT NULL,
   [password] nvarchar(255)  NOT NULL,
   [first_name] nvarchar(45)  NOT NULL,
   [middle_name] nvarchar(45)  NULL,
   [last_name] nvarchar(45)  NOT NULL,
   [created_at] datetime  NOT NULL,
   [updated_at] datetime  NULL,
   [deleted_at] datetime  NULL
)
WITH (DATA_COMPRESSION = NONE)
GO
BEGIN TRY
    EXEC sp_addextendedproperty
        N'MS_SSMA_SOURCE', N'festispec.`user`',
        N'SCHEMA', N'festispec',
        N'TABLE', N'user'
END TRY
BEGIN CATCH
    IF (@@TRANCOUNT > 0) ROLLBACK
    PRINT ERROR_MESSAGE()
END CATCH
GO

USE festispec
GO
IF EXISTS (SELECT * FROM sys.objects so JOIN sys.schemas sc ON so.schema_id = sc.schema_id WHERE so.name = N'user_has_availability'  AND sc.name = N'festispec'  AND type in (N'U'))
BEGIN

  DECLARE @drop_statement nvarchar(500)

  DECLARE drop_cursor CURSOR FOR
      SELECT 'alter table '+quotename(schema_name(ob.schema_id))+
      '.'+quotename(object_name(ob.object_id))+ ' drop constraint ' + quotename(fk.name) 
      FROM sys.objects ob INNER JOIN sys.foreign_keys fk ON fk.parent_object_id = ob.object_id
      WHERE fk.referenced_object_id = 
          (
             SELECT so.object_id 
             FROM sys.objects so JOIN sys.schemas sc
             ON so.schema_id = sc.schema_id
             WHERE so.name = N'user_has_availability'  AND sc.name = N'festispec'  AND type in (N'U')
           )

  OPEN drop_cursor

  FETCH NEXT FROM drop_cursor
  INTO @drop_statement

  WHILE @@FETCH_STATUS = 0
  BEGIN
     EXEC (@drop_statement)

     FETCH NEXT FROM drop_cursor
     INTO @drop_statement
  END

  CLOSE drop_cursor
  DEALLOCATE drop_cursor

  DROP TABLE [festispec].[user_has_availability]
END 
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE 
[festispec].[user_has_availability]
(
   [user_id] int  NOT NULL,
   [day] date  NOT NULL
)
WITH (DATA_COMPRESSION = NONE)
GO
BEGIN TRY
    EXEC sp_addextendedproperty
        N'MS_SSMA_SOURCE', N'festispec.user_has_availability',
        N'SCHEMA', N'festispec',
        N'TABLE', N'user_has_availability'
END TRY
BEGIN CATCH
    IF (@@TRANCOUNT > 0) ROLLBACK
    PRINT ERROR_MESSAGE()
END CATCH
GO

USE festispec
GO
IF EXISTS (SELECT * FROM sys.objects so JOIN sys.schemas sc ON so.schema_id = sc.schema_id WHERE so.name = N'user_role'  AND sc.name = N'festispec'  AND type in (N'U'))
BEGIN

  DECLARE @drop_statement nvarchar(500)

  DECLARE drop_cursor CURSOR FOR
      SELECT 'alter table '+quotename(schema_name(ob.schema_id))+
      '.'+quotename(object_name(ob.object_id))+ ' drop constraint ' + quotename(fk.name) 
      FROM sys.objects ob INNER JOIN sys.foreign_keys fk ON fk.parent_object_id = ob.object_id
      WHERE fk.referenced_object_id = 
          (
             SELECT so.object_id 
             FROM sys.objects so JOIN sys.schemas sc
             ON so.schema_id = sc.schema_id
             WHERE so.name = N'user_role'  AND sc.name = N'festispec'  AND type in (N'U')
           )

  OPEN drop_cursor

  FETCH NEXT FROM drop_cursor
  INTO @drop_statement

  WHILE @@FETCH_STATUS = 0
  BEGIN
     EXEC (@drop_statement)

     FETCH NEXT FROM drop_cursor
     INTO @drop_statement
  END

  CLOSE drop_cursor
  DEALLOCATE drop_cursor

  DROP TABLE [festispec].[user_role]
END 
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE 
[festispec].[user_role]
(
   [role] nvarchar(10)  NOT NULL
)
WITH (DATA_COMPRESSION = NONE)
GO
BEGIN TRY
    EXEC sp_addextendedproperty
        N'MS_SSMA_SOURCE', N'festispec.user_role',
        N'SCHEMA', N'festispec',
        N'TABLE', N'user_role'
END TRY
BEGIN CATCH
    IF (@@TRANCOUNT > 0) ROLLBACK
    PRINT ERROR_MESSAGE()
END CATCH
GO

USE festispec
GO
IF EXISTS (SELECT * FROM sys.objects so JOIN sys.schemas sc ON so.schema_id = sc.schema_id WHERE so.name = N'PK_answer__id'  AND sc.name = N'festispec'  AND type in (N'PK'))
ALTER TABLE [festispec].[answer_] DROP CONSTRAINT [PK_answer__id]
 GO



ALTER TABLE [festispec].[answer_]
 ADD CONSTRAINT [PK_answer__id]
   PRIMARY KEY
   CLUSTERED ([id] ASC)

GO


USE festispec
GO
IF EXISTS (SELECT * FROM sys.objects so JOIN sys.schemas sc ON so.schema_id = sc.schema_id WHERE so.name = N'PK_assignees_user_id'  AND sc.name = N'festispec'  AND type in (N'PK'))
ALTER TABLE [festispec].[assignees] DROP CONSTRAINT [PK_assignees_user_id]
 GO



ALTER TABLE [festispec].[assignees]
 ADD CONSTRAINT [PK_assignees_user_id]
   PRIMARY KEY
   CLUSTERED ([user_id] ASC, [inspection_form_id] ASC)

GO


USE festispec
GO
IF EXISTS (SELECT * FROM sys.objects so JOIN sys.schemas sc ON so.schema_id = sc.schema_id WHERE so.name = N'PK_customer_id'  AND sc.name = N'festispec'  AND type in (N'PK'))
ALTER TABLE [festispec].[customer] DROP CONSTRAINT [PK_customer_id]
 GO



ALTER TABLE [festispec].[customer]
 ADD CONSTRAINT [PK_customer_id]
   PRIMARY KEY
   CLUSTERED ([id] ASC)

GO


USE festispec
GO
IF EXISTS (SELECT * FROM sys.objects so JOIN sys.schemas sc ON so.schema_id = sc.schema_id WHERE so.name = N'PK_event_id'  AND sc.name = N'festispec'  AND type in (N'PK'))
ALTER TABLE [festispec].[event] DROP CONSTRAINT [PK_event_id]
 GO



ALTER TABLE [festispec].[event]
 ADD CONSTRAINT [PK_event_id]
   PRIMARY KEY
   CLUSTERED ([id] ASC)

GO


USE festispec
GO
IF EXISTS (SELECT * FROM sys.objects so JOIN sys.schemas sc ON so.schema_id = sc.schema_id WHERE so.name = N'PK_event_inspection_id'  AND sc.name = N'festispec'  AND type in (N'PK'))
ALTER TABLE [festispec].[event_inspection] DROP CONSTRAINT [PK_event_inspection_id]
 GO



ALTER TABLE [festispec].[event_inspection]
 ADD CONSTRAINT [PK_event_inspection_id]
   PRIMARY KEY
   CLUSTERED ([id] ASC)

GO


USE festispec
GO
IF EXISTS (SELECT * FROM sys.objects so JOIN sys.schemas sc ON so.schema_id = sc.schema_id WHERE so.name = N'PK_event_status_status'  AND sc.name = N'festispec'  AND type in (N'PK'))
ALTER TABLE [festispec].[event_status] DROP CONSTRAINT [PK_event_status_status]
 GO



ALTER TABLE [festispec].[event_status]
 ADD CONSTRAINT [PK_event_status_status]
   PRIMARY KEY
   CLUSTERED ([status] ASC)

GO


USE festispec
GO
IF EXISTS (SELECT * FROM sys.objects so JOIN sys.schemas sc ON so.schema_id = sc.schema_id WHERE so.name = N'PK_file_id'  AND sc.name = N'festispec'  AND type in (N'PK'))
ALTER TABLE [festispec].[file_link] DROP CONSTRAINT [PK_file_id]
 GO



ALTER TABLE [festispec].[file_link]
 ADD CONSTRAINT [PK_file_id]
   PRIMARY KEY
   CLUSTERED ([id] ASC)

GO


USE festispec
GO
IF EXISTS (SELECT * FROM sys.objects so JOIN sys.schemas sc ON so.schema_id = sc.schema_id WHERE so.name = N'PK_form_question_id'  AND sc.name = N'festispec'  AND type in (N'PK'))
ALTER TABLE [festispec].[form_question] DROP CONSTRAINT [PK_form_question_id]
 GO



ALTER TABLE [festispec].[form_question]
 ADD CONSTRAINT [PK_form_question_id]
   PRIMARY KEY
   CLUSTERED ([id] ASC)

GO


USE festispec
GO
IF EXISTS (SELECT * FROM sys.objects so JOIN sys.schemas sc ON so.schema_id = sc.schema_id WHERE so.name = N'PK_inspection_form_id'  AND sc.name = N'festispec'  AND type in (N'PK'))
ALTER TABLE [festispec].[inspection_form] DROP CONSTRAINT [PK_inspection_form_id]
 GO



ALTER TABLE [festispec].[inspection_form]
 ADD CONSTRAINT [PK_inspection_form_id]
   PRIMARY KEY
   CLUSTERED ([id] ASC)

GO


USE festispec
GO
IF EXISTS (SELECT * FROM sys.objects so JOIN sys.schemas sc ON so.schema_id = sc.schema_id WHERE so.name = N'PK_law_id'  AND sc.name = N'festispec'  AND type in (N'PK'))
ALTER TABLE [festispec].[law] DROP CONSTRAINT [PK_law_id]
 GO



ALTER TABLE [festispec].[law]
 ADD CONSTRAINT [PK_law_id]
   PRIMARY KEY
   CLUSTERED ([id] ASC)

GO


USE festispec
GO
IF EXISTS (SELECT * FROM sys.objects so JOIN sys.schemas sc ON so.schema_id = sc.schema_id WHERE so.name = N'PK_location_id'  AND sc.name = N'festispec'  AND type in (N'PK'))
ALTER TABLE [festispec].[location] DROP CONSTRAINT [PK_location_id]
 GO



ALTER TABLE [festispec].[location]
 ADD CONSTRAINT [PK_location_id]
   PRIMARY KEY
   CLUSTERED ([id] ASC)

GO


USE festispec
GO
IF EXISTS (SELECT * FROM sys.objects so JOIN sys.schemas sc ON so.schema_id = sc.schema_id WHERE so.name = N'PK_multplechoice_id'  AND sc.name = N'festispec'  AND type in (N'PK'))
ALTER TABLE [festispec].[multplechoice] DROP CONSTRAINT [PK_multplechoice_id]
 GO



ALTER TABLE [festispec].[multplechoice]
 ADD CONSTRAINT [PK_multplechoice_id]
   PRIMARY KEY
   CLUSTERED ([id] ASC)

GO


USE festispec
GO
IF EXISTS (SELECT * FROM sys.objects so JOIN sys.schemas sc ON so.schema_id = sc.schema_id WHERE so.name = N'PK_payment_status_payment_status'  AND sc.name = N'festispec'  AND type in (N'PK'))
ALTER TABLE [festispec].[payment_status] DROP CONSTRAINT [PK_payment_status_payment_status]
 GO



ALTER TABLE [festispec].[payment_status]
 ADD CONSTRAINT [PK_payment_status_payment_status]
   PRIMARY KEY
   CLUSTERED ([payment_status] ASC)

GO

USE festispec
GO
IF EXISTS (SELECT * FROM sys.objects so JOIN sys.schemas sc ON so.schema_id = sc.schema_id WHERE so.name = N'PK_quotation status_status'  AND sc.name = N'festispec'  AND type in (N'PK'))
ALTER TABLE [festispec].[quotation status] DROP CONSTRAINT [PK_quotation status_status]
 GO



ALTER TABLE [festispec].[quotation status]
 ADD CONSTRAINT [PK_quotation status_status]
   PRIMARY KEY
   CLUSTERED ([status] ASC)

GO

/*
USE festispec
GO
IF EXISTS (SELECT * FROM sys.objects so JOIN sys.schemas sc ON so.schema_id = sc.schema_id WHERE so.name = N'PK_region_region'  AND sc.name = N'festispec'  AND type in (N'PK'))
ALTER TABLE [festispec].[region] DROP CONSTRAINT [PK_region_region]
 GO



ALTER TABLE [festispec].[region]
 ADD CONSTRAINT [PK_region_region]
   PRIMARY KEY
   CLUSTERED ([region] ASC)

GO
*/

USE festispec
GO
IF EXISTS (SELECT * FROM sys.objects so JOIN sys.schemas sc ON so.schema_id = sc.schema_id WHERE so.name = N'PK_type_question_type'  AND sc.name = N'festispec'  AND type in (N'PK'))
ALTER TABLE [festispec].[question_type] DROP CONSTRAINT [PK_type_question_type]
 GO



ALTER TABLE [festispec].[question_type]
 ADD CONSTRAINT [PK_type_question_type]
   PRIMARY KEY
   CLUSTERED ([question_type] ASC)

GO


USE festispec
GO
IF EXISTS (SELECT * FROM sys.objects so JOIN sys.schemas sc ON so.schema_id = sc.schema_id WHERE so.name = N'PK_user_id'  AND sc.name = N'festispec'  AND type in (N'PK'))
ALTER TABLE [festispec].[user] DROP CONSTRAINT [PK_user_id]
 GO



ALTER TABLE [festispec].[user]
 ADD CONSTRAINT [PK_user_id]
   PRIMARY KEY
   CLUSTERED ([id] ASC)

GO


USE festispec
GO
IF EXISTS (SELECT * FROM sys.objects so JOIN sys.schemas sc ON so.schema_id = sc.schema_id WHERE so.name = N'PK_user_has_availability_user_id'  AND sc.name = N'festispec'  AND type in (N'PK'))
ALTER TABLE [festispec].[user_has_availability] DROP CONSTRAINT [PK_user_has_availability_user_id]
 GO



ALTER TABLE [festispec].[user_has_availability]
 ADD CONSTRAINT [PK_user_has_availability_user_id]
   PRIMARY KEY
   CLUSTERED ([user_id] ASC, [day] ASC)

GO


USE festispec
GO
IF EXISTS (SELECT * FROM sys.objects so JOIN sys.schemas sc ON so.schema_id = sc.schema_id WHERE so.name = N'PK_user_role_role'  AND sc.name = N'festispec'  AND type in (N'PK'))
ALTER TABLE [festispec].[user_role] DROP CONSTRAINT [PK_user_role_role]
 GO



ALTER TABLE [festispec].[user_role]
 ADD CONSTRAINT [PK_user_role_role]
   PRIMARY KEY
   CLUSTERED ([role] ASC)

GO

/*
USE festispec
GO
IF EXISTS (SELECT * FROM sys.objects so JOIN sys.schemas sc ON so.schema_id = sc.schema_id WHERE so.name = N'region$region_UNIQUE'  AND sc.name = N'festispec'  AND type in (N'UQ'))
ALTER TABLE [festispec].[region] DROP CONSTRAINT [region$region_UNIQUE]
 GO



ALTER TABLE [festispec].[region]
 ADD CONSTRAINT [region$region_UNIQUE]
 UNIQUE 
   NONCLUSTERED ([region] ASC)

GO

IF EXISTS (SELECT * FROM sys.objects so JOIN sys.schemas sc ON so.schema_id = sc.schema_id WHERE so.name = N'region$name_UNIQUE'  AND sc.name = N'festispec'  AND type in (N'UQ'))
ALTER TABLE [festispec].[region] DROP CONSTRAINT [region$name_UNIQUE]
 GO



ALTER TABLE [festispec].[region]
 ADD CONSTRAINT [region$name_UNIQUE]
 UNIQUE 
   NONCLUSTERED ([name] ASC)

GO

IF EXISTS (SELECT * FROM sys.objects so JOIN sys.schemas sc ON so.schema_id = sc.schema_id WHERE so.name = N'region$location_id_UNIQUE'  AND sc.name = N'festispec'  AND type in (N'UQ'))
ALTER TABLE [festispec].[region] DROP CONSTRAINT [region$location_id_UNIQUE]
 GO



ALTER TABLE [festispec].[region]
 ADD CONSTRAINT [region$location_id_UNIQUE]
 UNIQUE 
   NONCLUSTERED ([location_id] ASC)

GO

IF EXISTS (SELECT * FROM sys.objects so JOIN sys.schemas sc ON so.schema_id = sc.schema_id WHERE so.name = N'region$subsides_in_UNIQUE'  AND sc.name = N'festispec'  AND type in (N'UQ'))
ALTER TABLE [festispec].[region] DROP CONSTRAINT [region$subsides_in_UNIQUE]
 GO



ALTER TABLE [festispec].[region]
 ADD CONSTRAINT [region$subsides_in_UNIQUE]
 UNIQUE 
   NONCLUSTERED ([subsides_in] ASC)

GO
*/

USE festispec
GO
IF EXISTS (
       SELECT * FROM sys.objects  so JOIN sys.indexes si
       ON so.object_id = si.object_id
       JOIN sys.schemas sc
       ON so.schema_id = sc.schema_id
       WHERE so.name = N'answer_'  AND sc.name = N'festispec'  AND si.name = N'fk_answer__user1_idx' AND so.type in (N'U'))
   DROP INDEX [fk_answer__user1_idx] ON [festispec].[answer_] 
GO
CREATE NONCLUSTERED INDEX [fk_answer__user1_idx] ON [festispec].[answer_]
(
   [user_id] ASC
)
WITH (SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF) ON [PRIMARY] 
GO
GO

USE festispec
GO
IF EXISTS (
       SELECT * FROM sys.objects  so JOIN sys.indexes si
       ON so.object_id = si.object_id
       JOIN sys.schemas sc
       ON so.schema_id = sc.schema_id
       WHERE so.name = N'assignees'  AND sc.name = N'festispec'  AND si.name = N'fk_assignees_inspection_form1_idx' AND so.type in (N'U'))
   DROP INDEX [fk_assignees_inspection_form1_idx] ON [festispec].[assignees] 
GO
CREATE NONCLUSTERED INDEX [fk_assignees_inspection_form1_idx] ON [festispec].[assignees]
(
   [inspection_form_id] ASC
)
WITH (SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF) ON [PRIMARY] 
GO
GO

USE festispec
GO
IF EXISTS (
       SELECT * FROM sys.objects  so JOIN sys.indexes si
       ON so.object_id = si.object_id
       JOIN sys.schemas sc
       ON so.schema_id = sc.schema_id
       WHERE so.name = N'event'  AND sc.name = N'festispec'  AND si.name = N'fk_assignment_assignment_status1_idx' AND so.type in (N'U'))
   DROP INDEX [fk_assignment_assignment_status1_idx] ON [festispec].[event] 
GO
CREATE NONCLUSTERED INDEX [fk_assignment_assignment_status1_idx] ON [festispec].[event]
(
   [event_status] ASC
)
WITH (SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF) ON [PRIMARY] 
GO
GO

USE festispec
GO
IF EXISTS (
       SELECT * FROM sys.objects  so JOIN sys.indexes si
       ON so.object_id = si.object_id
       JOIN sys.schemas sc
       ON so.schema_id = sc.schema_id
       WHERE so.name = N'event'  AND sc.name = N'festispec'  AND si.name = N'fk_assignment_customer1_idx' AND so.type in (N'U'))
   DROP INDEX [fk_assignment_customer1_idx] ON [festispec].[event] 
GO
CREATE NONCLUSTERED INDEX [fk_assignment_customer1_idx] ON [festispec].[event]
(
   [customer_id] ASC
)
WITH (SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF) ON [PRIMARY] 
GO
GO

USE festispec
GO
IF EXISTS (
       SELECT * FROM sys.objects  so JOIN sys.indexes si
       ON so.object_id = si.object_id
       JOIN sys.schemas sc
       ON so.schema_id = sc.schema_id
       WHERE so.name = N'event'  AND sc.name = N'festispec'  AND si.name = N'fk_assignment_location1_idx' AND so.type in (N'U'))
   DROP INDEX [fk_assignment_location1_idx] ON [festispec].[event] 
GO
CREATE NONCLUSTERED INDEX [fk_assignment_location1_idx] ON [festispec].[event]
(
   [location_id] ASC
)
WITH (SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF) ON [PRIMARY] 
GO
GO

USE festispec
GO
IF EXISTS (
       SELECT * FROM sys.objects  so JOIN sys.indexes si
       ON so.object_id = si.object_id
       JOIN sys.schemas sc
       ON so.schema_id = sc.schema_id
       WHERE so.name = N'event'  AND sc.name = N'festispec'  AND si.name = N'fk_assignment_payment_status1_idx' AND so.type in (N'U'))
   DROP INDEX [fk_assignment_payment_status1_idx] ON [festispec].[event] 
GO
CREATE NONCLUSTERED INDEX [fk_assignment_payment_status1_idx] ON [festispec].[event]
(
   [payment_status] ASC
)
WITH (SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF) ON [PRIMARY] 
GO
GO

USE festispec
GO
IF EXISTS (
       SELECT * FROM sys.objects  so JOIN sys.indexes si
       ON so.object_id = si.object_id
       JOIN sys.schemas sc
       ON so.schema_id = sc.schema_id
       WHERE so.name = N'customer'  AND sc.name = N'festispec'  AND si.name = N'fk_customer_location_idx' AND so.type in (N'U'))
   DROP INDEX [fk_customer_location_idx] ON [festispec].[customer] 
GO
CREATE NONCLUSTERED INDEX [fk_customer_location_idx] ON [festispec].[customer]
(
   [location_id] ASC
)
WITH (SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF) ON [PRIMARY] 
GO
GO

USE festispec
GO
IF EXISTS (
       SELECT * FROM sys.objects  so JOIN sys.indexes si
       ON so.object_id = si.object_id
       JOIN sys.schemas sc
       ON so.schema_id = sc.schema_id
       WHERE so.name = N'event_inspection'  AND sc.name = N'festispec'  AND si.name = N'fk_event_has_inspection_form_event1_idx' AND so.type in (N'U'))
   DROP INDEX [fk_event_has_inspection_form_event1_idx] ON [festispec].[event_inspection] 
GO
CREATE NONCLUSTERED INDEX [fk_event_has_inspection_form_event1_idx] ON [festispec].[event_inspection]
(
   [event_id] ASC
)
WITH (SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF) ON [PRIMARY] 
GO
GO

USE festispec
GO
IF EXISTS (
       SELECT * FROM sys.objects  so JOIN sys.indexes si
       ON so.object_id = si.object_id
       JOIN sys.schemas sc
       ON so.schema_id = sc.schema_id
       WHERE so.name = N'form_question'  AND sc.name = N'festispec'  AND si.name = N'fk_form_question_has_inspection_idx' AND so.type in (N'U'))
   DROP INDEX [fk_form_question_has_inspection_idx] ON [festispec].[form_question] 
GO
CREATE NONCLUSTERED INDEX [fk_form_question_has_inspection_idx] ON [festispec].[form_question]
(
   [inspection_id] ASC
)
WITH (SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF) ON [PRIMARY] 
GO
GO

USE festispec
GO
IF EXISTS (
       SELECT * FROM sys.objects  so JOIN sys.indexes si
       ON so.object_id = si.object_id
       JOIN sys.schemas sc
       ON so.schema_id = sc.schema_id
       WHERE so.name = N'form_question'  AND sc.name = N'festispec'  AND si.name = N'fk_form_question_type1_idx' AND so.type in (N'U'))
   DROP INDEX [fk_form_question_type1_idx] ON [festispec].[form_question] 
GO
CREATE NONCLUSTERED INDEX [fk_form_question_type1_idx] ON [festispec].[form_question]
(
   [type] ASC
)
WITH (SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF) ON [PRIMARY] 
GO
GO

USE festispec
GO
IF EXISTS (
       SELECT * FROM sys.objects  so JOIN sys.indexes si
       ON so.object_id = si.object_id
       JOIN sys.schemas sc
       ON so.schema_id = sc.schema_id
       WHERE so.name = N'inspection_form'  AND sc.name = N'festispec'  AND si.name = N'fk_inspection_form_event_inspection1_idx' AND so.type in (N'U'))
   DROP INDEX [fk_inspection_form_event_inspection1_idx] ON [festispec].[inspection_form] 
GO
CREATE NONCLUSTERED INDEX [fk_inspection_form_event_inspection1_idx] ON [festispec].[inspection_form]
(
   [event_inspection_id] ASC
)
WITH (SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF) ON [PRIMARY] 
GO
GO

USE festispec
GO
IF EXISTS (
       SELECT * FROM sys.objects  so JOIN sys.indexes si
       ON so.object_id = si.object_id
       JOIN sys.schemas sc
       ON so.schema_id = sc.schema_id
       WHERE so.name = N'answer_'  AND sc.name = N'festispec'  AND si.name = N'fk_inspection_has_form_question_form_question1_idx' AND so.type in (N'U'))
   DROP INDEX [fk_inspection_has_form_question_form_question1_idx] ON [festispec].[answer_] 
GO
CREATE NONCLUSTERED INDEX [fk_inspection_has_form_question_form_question1_idx] ON [festispec].[answer_]
(
   [form_question_id] ASC
)
WITH (SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF) ON [PRIMARY] 
GO
GO
/*
USE festispec
GO
IF EXISTS (
       SELECT * FROM sys.objects  so JOIN sys.indexes si
       ON so.object_id = si.object_id
       JOIN sys.schemas sc
       ON so.schema_id = sc.schema_id
       WHERE so.name = N'law'  AND sc.name = N'festispec'  AND si.name = N'fk_law_region1_idx' AND so.type in (N'U'))
   DROP INDEX [fk_law_region1_idx] ON [festispec].[law] 
GO
CREATE NONCLUSTERED INDEX [fk_law_region1_idx] ON [festispec].[law]
(
   [region_region] ASC
)
WITH (SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF) ON [PRIMARY] 
GO
GO
*/
USE festispec
GO
IF EXISTS (
       SELECT * FROM sys.objects  so JOIN sys.indexes si
       ON so.object_id = si.object_id
       JOIN sys.schemas sc
       ON so.schema_id = sc.schema_id
       WHERE so.name = N'multplechoice'  AND sc.name = N'festispec'  AND si.name = N'fk_multplechoice_form_question1_idx' AND so.type in (N'U'))
   DROP INDEX [fk_multplechoice_form_question1_idx] ON [festispec].[multplechoice] 
GO
CREATE NONCLUSTERED INDEX [fk_multplechoice_form_question1_idx] ON [festispec].[multplechoice]
(
   [form_question_id] ASC
)
WITH (SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF) ON [PRIMARY] 
GO
GO

USE festispec
GO
IF EXISTS (
       SELECT * FROM sys.objects  so JOIN sys.indexes si
       ON so.object_id = si.object_id
       JOIN sys.schemas sc
       ON so.schema_id = sc.schema_id
       WHERE so.name = N'quotation'  AND sc.name = N'festispec'  AND si.name = N'fk_offerte_assignment1_idx' AND so.type in (N'U'))
   DROP INDEX [fk_offerte_assignment1_idx] ON [festispec].[quotation] 
GO
CREATE NONCLUSTERED INDEX [fk_offerte_assignment1_idx] ON [festispec].[quotation]
(
   [event_id] ASC
)
WITH (SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF) ON [PRIMARY] 
GO
GO
/*
USE festispec
GO
IF EXISTS (
       SELECT * FROM sys.objects  so JOIN sys.indexes si
       ON so.object_id = si.object_id
       JOIN sys.schemas sc
       ON so.schema_id = sc.schema_id
       WHERE so.name = N'region'  AND sc.name = N'festispec'  AND si.name = N'fk_region_location1_idx' AND so.type in (N'U'))
   DROP INDEX [fk_region_location1_idx] ON [festispec].[region] 
GO
CREATE NONCLUSTERED INDEX [fk_region_location1_idx] ON [festispec].[region]
(
   [location_id] ASC
)
WITH (SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF) ON [PRIMARY] 
GO
GO

USE festispec
GO
IF EXISTS (
       SELECT * FROM sys.objects  so JOIN sys.indexes si
       ON so.object_id = si.object_id
       JOIN sys.schemas sc
       ON so.schema_id = sc.schema_id
       WHERE so.name = N'region'  AND sc.name = N'festispec'  AND si.name = N'fk_region_region1_idx' AND so.type in (N'U'))
   DROP INDEX [fk_region_region1_idx] ON [festispec].[region] 
GO
CREATE NONCLUSTERED INDEX [fk_region_region1_idx] ON [festispec].[region]
(
   [subsides_in] ASC
)
WITH (SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF) ON [PRIMARY] 
GO
GO
*/
USE festispec
GO
IF EXISTS (
       SELECT * FROM sys.objects  so JOIN sys.indexes si
       ON so.object_id = si.object_id
       JOIN sys.schemas sc
       ON so.schema_id = sc.schema_id
       WHERE so.name = N'assignees'  AND sc.name = N'festispec'  AND si.name = N'fk_user_has_assignment_user1_idx' AND so.type in (N'U'))
   DROP INDEX [fk_user_has_assignment_user1_idx] ON [festispec].[assignees] 
GO
CREATE NONCLUSTERED INDEX [fk_user_has_assignment_user1_idx] ON [festispec].[assignees]
(
   [user_id] ASC
)
WITH (SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF) ON [PRIMARY] 
GO
GO

USE festispec
GO
IF EXISTS (
       SELECT * FROM sys.objects  so JOIN sys.indexes si
       ON so.object_id = si.object_id
       JOIN sys.schemas sc
       ON so.schema_id = sc.schema_id
       WHERE so.name = N'user_has_availability'  AND sc.name = N'festispec'  AND si.name = N'fk_user_has_availability_user1_idx' AND so.type in (N'U'))
   DROP INDEX [fk_user_has_availability_user1_idx] ON [festispec].[user_has_availability] 
GO
CREATE NONCLUSTERED INDEX [fk_user_has_availability_user1_idx] ON [festispec].[user_has_availability]
(
   [user_id] ASC
)
WITH (SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF) ON [PRIMARY] 
GO
GO

USE festispec
GO
IF EXISTS (
       SELECT * FROM sys.objects  so JOIN sys.indexes si
       ON so.object_id = si.object_id
       JOIN sys.schemas sc
       ON so.schema_id = sc.schema_id
       WHERE so.name = N'user'  AND sc.name = N'festispec'  AND si.name = N'fk_user_location1_idx' AND so.type in (N'U'))
   DROP INDEX [fk_user_location1_idx] ON [festispec].[user] 
GO
CREATE NONCLUSTERED INDEX [fk_user_location1_idx] ON [festispec].[user]
(
   [location_id] ASC
)
WITH (SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF) ON [PRIMARY] 
GO
GO

USE festispec
GO
IF EXISTS (
       SELECT * FROM sys.objects  so JOIN sys.indexes si
       ON so.object_id = si.object_id
       JOIN sys.schemas sc
       ON so.schema_id = sc.schema_id
       WHERE so.name = N'user'  AND sc.name = N'festispec'  AND si.name = N'fk_user_user_role1_idx' AND so.type in (N'U'))
   DROP INDEX [fk_user_user_role1_idx] ON [festispec].[user] 
GO
CREATE NONCLUSTERED INDEX [fk_user_user_role1_idx] ON [festispec].[user]
(
   [user_role] ASC
)
WITH (SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF) ON [PRIMARY] 
GO
GO

USE festispec
GO
IF EXISTS (SELECT * FROM sys.objects so JOIN sys.schemas sc ON so.schema_id = sc.schema_id WHERE so.name = N'answer_$fk_answer__user1'  AND sc.name = N'festispec'  AND type in (N'F'))
ALTER TABLE [festispec].[answer_] DROP CONSTRAINT [answer_$fk_answer__user1]
 GO



ALTER TABLE [festispec].[answer_]
 ADD CONSTRAINT [answer_$fk_answer__user1]
 FOREIGN KEY 
   ([user_id])
 REFERENCES 
   [festispec].[festispec].[user]     ([id])
    ON DELETE NO ACTION
    ON UPDATE NO ACTION

GO

IF EXISTS (SELECT * FROM sys.objects so JOIN sys.schemas sc ON so.schema_id = sc.schema_id WHERE so.name = N'answer_$fk_inspection_has_form_question_form_question1'  AND sc.name = N'festispec'  AND type in (N'F'))
ALTER TABLE [festispec].[answer_] DROP CONSTRAINT [answer_$fk_inspection_has_form_question_form_question1]
 GO



ALTER TABLE [festispec].[answer_]
 ADD CONSTRAINT [answer_$fk_inspection_has_form_question_form_question1]
 FOREIGN KEY 
   ([form_question_id])
 REFERENCES 
   [festispec].[festispec].[form_question]     ([id])
    ON DELETE NO ACTION
    ON UPDATE NO ACTION

GO


USE festispec
GO
IF EXISTS (SELECT * FROM sys.objects so JOIN sys.schemas sc ON so.schema_id = sc.schema_id WHERE so.name = N'assignees$fk_assignees_inspection_form1'  AND sc.name = N'festispec'  AND type in (N'F'))
ALTER TABLE [festispec].[assignees] DROP CONSTRAINT [assignees$fk_assignees_inspection_form1]
 GO



ALTER TABLE [festispec].[assignees]
 ADD CONSTRAINT [assignees$fk_assignees_inspection_form1]
 FOREIGN KEY 
   ([inspection_form_id])
 REFERENCES 
   [festispec].[festispec].[inspection_form]     ([id])
    ON DELETE NO ACTION
    ON UPDATE NO ACTION

GO

IF EXISTS (SELECT * FROM sys.objects so JOIN sys.schemas sc ON so.schema_id = sc.schema_id WHERE so.name = N'assignees$fk_user_has_assignment_user1'  AND sc.name = N'festispec'  AND type in (N'F'))
ALTER TABLE [festispec].[assignees] DROP CONSTRAINT [assignees$fk_user_has_assignment_user1]
 GO



ALTER TABLE [festispec].[assignees]
 ADD CONSTRAINT [assignees$fk_user_has_assignment_user1]
 FOREIGN KEY 
   ([user_id])
 REFERENCES 
   [festispec].[festispec].[user]     ([id])
    ON DELETE NO ACTION
    ON UPDATE NO ACTION

GO


USE festispec
GO
IF EXISTS (SELECT * FROM sys.objects so JOIN sys.schemas sc ON so.schema_id = sc.schema_id WHERE so.name = N'customer$fk_customer_location'  AND sc.name = N'festispec'  AND type in (N'F'))
ALTER TABLE [festispec].[customer] DROP CONSTRAINT [customer$fk_customer_location]
 GO



ALTER TABLE [festispec].[customer]
 ADD CONSTRAINT [customer$fk_customer_location]
 FOREIGN KEY 
   ([location_id])
 REFERENCES 
   [festispec].[festispec].[location]     ([id])
    ON DELETE NO ACTION
    ON UPDATE NO ACTION

GO


USE festispec
GO
IF EXISTS (SELECT * FROM sys.objects so JOIN sys.schemas sc ON so.schema_id = sc.schema_id WHERE so.name = N'event$fk_assignment_assignment_status1'  AND sc.name = N'festispec'  AND type in (N'F'))
ALTER TABLE [festispec].[event] DROP CONSTRAINT [event$fk_assignment_assignment_status1]
 GO



ALTER TABLE [festispec].[event]
 ADD CONSTRAINT [event$fk_assignment_assignment_status1]
 FOREIGN KEY 
   ([event_status])
 REFERENCES 
   [festispec].[festispec].[event_status]     ([status])
    ON DELETE NO ACTION
    ON UPDATE NO ACTION

GO

IF EXISTS (SELECT * FROM sys.objects so JOIN sys.schemas sc ON so.schema_id = sc.schema_id WHERE so.name = N'event$fk_assignment_customer1'  AND sc.name = N'festispec'  AND type in (N'F'))
ALTER TABLE [festispec].[event] DROP CONSTRAINT [event$fk_assignment_customer1]
 GO



ALTER TABLE [festispec].[event]
 ADD CONSTRAINT [event$fk_assignment_customer1]
 FOREIGN KEY 
   ([customer_id])
 REFERENCES 
   [festispec].[festispec].[customer]     ([id])
    ON DELETE NO ACTION
    ON UPDATE NO ACTION

GO

IF EXISTS (SELECT * FROM sys.objects so JOIN sys.schemas sc ON so.schema_id = sc.schema_id WHERE so.name = N'event$fk_assignment_location1'  AND sc.name = N'festispec'  AND type in (N'F'))
ALTER TABLE [festispec].[event] DROP CONSTRAINT [event$fk_assignment_location1]
 GO



ALTER TABLE [festispec].[event]
 ADD CONSTRAINT [event$fk_assignment_location1]
 FOREIGN KEY 
   ([location_id])
 REFERENCES 
   [festispec].[festispec].[location]     ([id])
    ON DELETE NO ACTION
    ON UPDATE NO ACTION

GO

IF EXISTS (SELECT * FROM sys.objects so JOIN sys.schemas sc ON so.schema_id = sc.schema_id WHERE so.name = N'event$fk_assignment_payment_status1'  AND sc.name = N'festispec'  AND type in (N'F'))
ALTER TABLE [festispec].[event] DROP CONSTRAINT [event$fk_assignment_payment_status1]
 GO



ALTER TABLE [festispec].[event]
 ADD CONSTRAINT [event$fk_assignment_payment_status1]
 FOREIGN KEY 
   ([payment_status])
 REFERENCES 
   [festispec].[festispec].[payment_status]     ([payment_status])
    ON DELETE NO ACTION
    ON UPDATE NO ACTION

GO


USE festispec
GO
IF EXISTS (SELECT * FROM sys.objects so JOIN sys.schemas sc ON so.schema_id = sc.schema_id WHERE so.name = N'event_inspection$fk_event_has_inspection_form_event1'  AND sc.name = N'festispec'  AND type in (N'F'))
ALTER TABLE [festispec].[event_inspection] DROP CONSTRAINT [event_inspection$fk_event_has_inspection_form_event1]
 GO



ALTER TABLE [festispec].[event_inspection]
 ADD CONSTRAINT [event_inspection$fk_event_has_inspection_form_event1]
 FOREIGN KEY 
   ([event_id])
 REFERENCES 
   [festispec].[festispec].[event]     ([id])
    ON DELETE NO ACTION
    ON UPDATE NO ACTION

GO


USE festispec
GO
IF EXISTS (SELECT * FROM sys.objects so JOIN sys.schemas sc ON so.schema_id = sc.schema_id WHERE so.name = N'form_question$fk_form_question_has_inspection'  AND sc.name = N'festispec'  AND type in (N'F'))
ALTER TABLE [festispec].[form_question] DROP CONSTRAINT [form_question$fk_form_question_has_inspection]
 GO



ALTER TABLE [festispec].[form_question]
 ADD CONSTRAINT [form_question$fk_form_question_has_inspection]
 FOREIGN KEY 
   ([inspection_id])
 REFERENCES 
   [festispec].[festispec].[inspection_form]     ([id])
    ON DELETE NO ACTION
    ON UPDATE NO ACTION

GO

IF EXISTS (SELECT * FROM sys.objects so JOIN sys.schemas sc ON so.schema_id = sc.schema_id WHERE so.name = N'form_question$fk_type'  AND sc.name = N'festispec'  AND type in (N'F'))
ALTER TABLE [festispec].[form_question] DROP CONSTRAINT [form_question$fk_type]
 GO



ALTER TABLE [festispec].[form_question]
 ADD CONSTRAINT [form_question$fk_type]
 FOREIGN KEY 
   ([type])
 REFERENCES 
   [festispec].[festispec].[question_type]     ([question_type])
    ON DELETE NO ACTION
    ON UPDATE NO ACTION

GO


USE festispec
GO
IF EXISTS (SELECT * FROM sys.objects so JOIN sys.schemas sc ON so.schema_id = sc.schema_id WHERE so.name = N'inspection_form$fk_inspection_form_id'  AND sc.name = N'festispec'  AND type in (N'F'))
ALTER TABLE [festispec].[inspection_form] DROP CONSTRAINT [inspection_form$fk_inspection_form_event_id]
 GO



ALTER TABLE [festispec].[inspection_form]
 ADD CONSTRAINT [inspection_form$fk_inspection_form_id]
 FOREIGN KEY 
   ([event_inspection_id])
 REFERENCES 
   [festispec].[festispec].[event_inspection]     ([id])
    ON DELETE NO ACTION
    ON UPDATE NO ACTION

GO

/*
USE festispec
GO
IF EXISTS (SELECT * FROM sys.objects so JOIN sys.schemas sc ON so.schema_id = sc.schema_id WHERE so.name = N'law$fk_law_region1'  AND sc.name = N'festispec'  AND type in (N'F'))
ALTER TABLE [festispec].[law] DROP CONSTRAINT [law$fk_law_region1]
 GO
*/

/*
ALTER TABLE [festispec].[law]
 ADD CONSTRAINT [law$fk_law_region1]
 FOREIGN KEY 
   ([region_region])
 REFERENCES 
   [festispec].[festispec].[region]     ([region])
    ON DELETE NO ACTION
    ON UPDATE NO ACTION

GO
*/

USE festispec
GO
IF EXISTS (SELECT * FROM sys.objects so JOIN sys.schemas sc ON so.schema_id = sc.schema_id WHERE so.name = N'multplechoice$fk_multplechoice_form_question1'  AND sc.name = N'festispec'  AND type in (N'F'))
ALTER TABLE [festispec].[multplechoice] DROP CONSTRAINT [multplechoice$fk_multplechoice_form_question1]
 GO



ALTER TABLE [festispec].[multplechoice]
 ADD CONSTRAINT [multplechoice$fk_multplechoice_form_question1]
 FOREIGN KEY 
   ([form_question_id])
 REFERENCES 
   [festispec].[festispec].[form_question]     ([id])
    ON DELETE NO ACTION
    ON UPDATE NO ACTION

GO


USE festispec
GO
IF EXISTS (SELECT * FROM sys.objects so JOIN sys.schemas sc ON so.schema_id = sc.schema_id WHERE so.name = N'quotation$fk_offerte_assignment1'  AND sc.name = N'festispec'  AND type in (N'F'))
ALTER TABLE [festispec].[quotation] DROP CONSTRAINT [quotation$fk_offerte_assignment1]
 GO



ALTER TABLE [festispec].[quotation]
 ADD CONSTRAINT [quotation$fk_offerte_assignment1]
 FOREIGN KEY 
   ([event_id])
 REFERENCES 
   [festispec].[festispec].[event]     ([id])
    ON DELETE NO ACTION
    ON UPDATE NO ACTION

GO

IF EXISTS (SELECT * FROM sys.objects so JOIN sys.schemas sc ON so.schema_id = sc.schema_id WHERE so.name = N'quotation$fk_quotation_quotation status1'  AND sc.name = N'festispec'  AND type in (N'F'))
ALTER TABLE [festispec].[quotation] DROP CONSTRAINT [quotation$fk_quotation_quotation status1]
 GO



ALTER TABLE [festispec].[quotation]
 ADD CONSTRAINT [quotation$fk_quotation_quotation status1]
 FOREIGN KEY 
   ([quotation status_status])
 REFERENCES 
   [festispec].[festispec].[quotation status]     ([status])
    ON DELETE NO ACTION
    ON UPDATE NO ACTION

GO

/*
USE festispec
GO
IF EXISTS (SELECT * FROM sys.objects so JOIN sys.schemas sc ON so.schema_id = sc.schema_id WHERE so.name = N'region$fk_region_location1'  AND sc.name = N'festispec'  AND type in (N'F'))
ALTER TABLE [festispec].[region] DROP CONSTRAINT [region$fk_region_location1]
 GO



ALTER TABLE [festispec].[region]
 ADD CONSTRAINT [region$fk_region_location1]
 FOREIGN KEY 
   ([location_id])
 REFERENCES 
   [festispec].[festispec].[location]     ([id])
    ON DELETE NO ACTION
    ON UPDATE NO ACTION

GO

IF EXISTS (SELECT * FROM sys.objects so JOIN sys.schemas sc ON so.schema_id = sc.schema_id WHERE so.name = N'region$fk_region_region1'  AND sc.name = N'festispec'  AND type in (N'F'))
ALTER TABLE [festispec].[region] DROP CONSTRAINT [region$fk_region_region1]
 GO
 */

/*
ALTER TABLE [festispec].[region]
 ADD CONSTRAINT [region$fk_region_region1]
 FOREIGN KEY 
   ([subsides_in])
 REFERENCES 
   [festispec].[festispec].[region]     ([region])
    ON DELETE NO ACTION
    ON UPDATE NO ACTION

GO
*/

USE festispec
GO
IF EXISTS (SELECT * FROM sys.objects so JOIN sys.schemas sc ON so.schema_id = sc.schema_id WHERE so.name = N'user$fk_user_location1'  AND sc.name = N'festispec'  AND type in (N'F'))
ALTER TABLE [festispec].[user] DROP CONSTRAINT [user$fk_user_location1]
 GO



ALTER TABLE [festispec].[user]
 ADD CONSTRAINT [user$fk_user_location1]
 FOREIGN KEY 
   ([location_id])
 REFERENCES 
   [festispec].[festispec].[location]     ([id])
    ON DELETE NO ACTION
    ON UPDATE NO ACTION

GO

IF EXISTS (SELECT * FROM sys.objects so JOIN sys.schemas sc ON so.schema_id = sc.schema_id WHERE so.name = N'user$fk_user_user_role1'  AND sc.name = N'festispec'  AND type in (N'F'))
ALTER TABLE [festispec].[user] DROP CONSTRAINT [user$fk_user_user_role1]
 GO



ALTER TABLE [festispec].[user]
 ADD CONSTRAINT [user$fk_user_user_role1]
 FOREIGN KEY 
   ([user_role])
 REFERENCES 
   [festispec].[festispec].[user_role]     ([role])
    ON DELETE NO ACTION
    ON UPDATE NO ACTION

GO


USE festispec
GO
IF EXISTS (SELECT * FROM sys.objects so JOIN sys.schemas sc ON so.schema_id = sc.schema_id WHERE so.name = N'user_has_availability$fk_user_has_availability_user1'  AND sc.name = N'festispec'  AND type in (N'F'))
ALTER TABLE [festispec].[user_has_availability] DROP CONSTRAINT [user_has_availability$fk_user_has_availability_user1]
 GO



ALTER TABLE [festispec].[user_has_availability]
 ADD CONSTRAINT [user_has_availability$fk_user_has_availability_user1]
 FOREIGN KEY 
   ([user_id])
 REFERENCES 
   [festispec].[festispec].[user]     ([id])
    ON DELETE NO ACTION
    ON UPDATE NO ACTION

GO

ALTER TABLE [festispec].[law]
 ADD CONSTRAINT [law$fk_location_id]
 FOREIGN KEY 
   ([location_id])
 REFERENCES 
   [festispec].[festispec].[location]     ([id])
    ON DELETE NO ACTION
    ON UPDATE NO ACTION

GO


USE festispec
GO
ALTER TABLE  [festispec].[customer]
 ADD DEFAULT NULL FOR [telephone_number]
GO

ALTER TABLE  [festispec].[customer]
 ADD DEFAULT getdate() FOR [created_at]
GO

ALTER TABLE  [festispec].[customer]
 ADD DEFAULT NULL FOR [updated_at]
GO

ALTER TABLE  [festispec].[customer]
 ADD DEFAULT NULL FOR [deleted_at]
GO

USE festispec
GO
ALTER TABLE  [festispec].[event]
 ADD DEFAULT getdate() FOR [created_at]
GO

ALTER TABLE  [festispec].[event]
 ADD DEFAULT NULL FOR [updated_at]
GO

ALTER TABLE  [festispec].[event]
 ADD DEFAULT NULL FOR [deleted_at]
GO


USE festispec
GO
ALTER TABLE  [festispec].[event_inspection]
 ADD DEFAULT getdate() FOR [created_at]
GO

ALTER TABLE  [festispec].[event_inspection]
 ADD DEFAULT NULL FOR [updated_at]
GO

ALTER TABLE  [festispec].[event_inspection]
 ADD DEFAULT NULL FOR [deleted_at]
GO


USE festispec
GO
ALTER TABLE  [festispec].[file_link]
 ADD DEFAULT getdate() FOR [created_at]
GO

ALTER TABLE  [festispec].[file_link]
 ADD DEFAULT NULL FOR [updated_at]
GO

ALTER TABLE  [festispec].[file_link]
 ADD DEFAULT NULL FOR [deleted_at]
GO


USE festispec
GO
ALTER TABLE  [festispec].[form_question]
 ADD DEFAULT getdate() FOR [created_at]
GO

ALTER TABLE  [festispec].[form_question]
 ADD DEFAULT NULL FOR [updated_at]
GO

ALTER TABLE  [festispec].[form_question]
 ADD DEFAULT NULL FOR [deleted_at]
GO


USE festispec
GO
ALTER TABLE  [festispec].[inspection_form]
 ADD DEFAULT NULL FOR [event_inspection_id]
GO

ALTER TABLE  [festispec].[inspection_form]
 ADD DEFAULT NULL FOR [floor_plan]
GO

ALTER TABLE  [festispec].[event_inspection]
 ADD DEFAULT getdate() FOR [execution_date]
GO

ALTER TABLE  [festispec].[inspection_form]
 ADD DEFAULT getdate() FOR [created_at]
GO

ALTER TABLE  [festispec].[inspection_form]
 ADD DEFAULT NULL FOR [updated_at]
GO

ALTER TABLE  [festispec].[inspection_form]
 ADD DEFAULT NULL FOR [deleted_at]
GO


USE festispec
GO
ALTER TABLE  [festispec].[location]
 ADD DEFAULT NULL FOR [longtitude]
GO

ALTER TABLE  [festispec].[location]
 ADD DEFAULT NULL FOR [latitude]
GO

ALTER TABLE  [festispec].[location]
 ADD DEFAULT getdate() FOR [created_at]
GO

ALTER TABLE  [festispec].[location]
 ADD DEFAULT NULL FOR [updated_at]
GO

ALTER TABLE  [festispec].[location]
 ADD DEFAULT NULL FOR [deleted_at]
GO

/*
USE festispec
GO
ALTER TABLE  [festispec].[region]
 ADD DEFAULT NULL FOR [subsides_in]
GO
*/

USE festispec
GO
ALTER TABLE  [festispec].[user]
 ADD DEFAULT NULL FOR [location_id]
GO
/*
ALTER TABLE  [festispec].[user]
 ADD DEFAULT NULL FOR [avatar]
GO
*/
ALTER TABLE  [festispec].[user]
 ADD DEFAULT NULL FOR [middle_name]
GO

ALTER TABLE  [festispec].[user]
 ADD DEFAULT getdate() FOR [created_at]
GO

ALTER TABLE  [festispec].[user]
 ADD DEFAULT NULL FOR [updated_at]
GO

ALTER TABLE  [festispec].[user]
 ADD DEFAULT NULL FOR [deleted_at]
GO

IF (EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'reports'))
BEGIN
	DROP TABLE report
END
GO

CREATE TABLE [festispec].[report]
(
	id				int NOT NULL Identity(1,1),
	name			varchar(45) NOT NULL,
	file_link_id	int NOT NULL,
	created_at		datetime NOT NULL DEFAULT getdate(), 
	updated_at		datetime NULL DEFAULT NULL,
	deleted_at		datetime NULL DEFAULT NULL,
	CONSTRAINT		PK_report PRIMARY KEY(id),
	CONSTRAINT		FK_report_file FOREIGN KEY (file_link_id) REFERENCES [festispec].[file_link]([id])
)
WITH (DATA_COMPRESSION = NONE)
GO
BEGIN TRY
    EXEC sp_addextendedproperty
        N'MS_SSMA_SOURCE', N'festispec.report',
        N'SCHEMA', N'festispec',
        N'TABLE', N'report'
END TRY
BEGIN CATCH
    IF (@@TRANCOUNT > 0) ROLLBACK
    PRINT ERROR_MESSAGE()
END CATCH
GO

IF (EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'report_has_events'))
BEGIN
	DROP TABLE report_has_events
END
GO

CREATE TABLE [festispec].[report_has_events]
(
	report_id		int NOT NULL,
	event_id		int NOT NULL,
	CONSTRAINT		PK_report_has_events PRIMARY KEY(report_id,event_id),
	CONSTRAINT		FK_report_id FOREIGN KEY (report_id) REFERENCES [festispec].[report]([id]),
	CONSTRAINT		FK_event_id FOREIGN KEY (event_id) REFERENCES [festispec].[event]([id])
)
WITH (DATA_COMPRESSION = NONE)
GO
BEGIN TRY
    EXEC sp_addextendedproperty
        N'MS_SSMA_SOURCE', N'festispec.report_has_events',
        N'SCHEMA', N'festispec',
        N'TABLE', N'report_has_events'
END TRY
BEGIN CATCH
    IF (@@TRANCOUNT > 0) ROLLBACK
    PRINT ERROR_MESSAGE()
END CATCH
GO

