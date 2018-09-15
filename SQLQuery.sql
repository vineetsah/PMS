USE [PMS]
GO

/****** Object:  Table [dbo].[Vehicle_Parking]    Script Date: 4/23/2018 10:36:01 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Vehicle_Parking](
	[SlotId] [int] NOT NULL,
	[ArrivalTime] [datetime] NULL,
	[DepartTime] [date] NULL,
	[TotalFare] [decimal](10, 2) NULL,
	[RegistrationId] [nvarchar](10) NOT NULL,
	[VehicleType] [nvarchar](15) NOT NULL,
	[Floor] [nvarchar](10) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[RegistrationId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO


/**************************Object: Table[dbo].[Vehicle_Parking] Insert Query***********************************************/
INSERT INTO [dbo].[Vehicle_Parking]
           ([SlotId]
           ,[ArrivalTime]
           ,[DepartTime]
           ,[TotalFare]
           ,[RegistrationId]
           ,[VehicleType]
           ,[Floor])
     VALUES
           (<SlotId, int,>
           ,<ArrivalTime, datetime,>
           ,<DepartTime, date,>
           ,<TotalFare, decimal(10,2),>
           ,<RegistrationId, nvarchar(10),>
           ,<VehicleType, nvarchar(15),>
           ,<Floor, nvarchar(10),>)
GO

/**************************Object: Table[dbo].[Vehicle_Parking] Delete Query***********************************************/
USE [PMS]
GO

DELETE FROM [dbo].[Vehicle_Parking]
      WHERE <Search Conditions,,>
GO

/**************************Object: Table[dbo].[Vehicle_Parking] Update Query***********************************************/

USE [PMS]
GO
UPDATE [dbo].[Vehicle_Parking]
   SET [SlotId] = <SlotId, int,>
      ,[ArrivalTime] = <ArrivalTime, datetime,>
      ,[DepartTime] = <DepartTime, date,>
      ,[TotalFare] = <TotalFare, decimal(10,2),>
      ,[RegistrationId] = <RegistrationId, nvarchar(10),>
      ,[VehicleType] = <VehicleType, nvarchar(15),>
      ,[Floor] = <Floor, nvarchar(10),>
 WHERE <Search Conditions,,>
GO
/**************************Object: Table[dbo].[Vehicle_Parking] Fetch Query***********************************************/

/****** Script for SelectTopNRows command from SSMS  ******/
SELECT TOP (10) [SlotId]
      ,[ArrivalTime]
      ,[DepartTime]
      ,[TotalFare]
      ,[RegistrationId]
      ,[VehicleType]
      ,[Floor]
  FROM [PMS].[dbo].[Vehicle_Parking]

  /**************************Object: Table[dbo].[Vehicle_Parking] GetAvailable SlotID Query***********************************************/
  USE [PMS]
GO
 select  Top 1 T1.SlotId + 1 as NextId,T1.Floor
from [dbo].[Vehicle_Parking] as T1
  left outer join [dbo].[Vehicle_Parking]  as T2 on T1.SlotId + 1 = T2.SlotId AND T1.Floor=T2.Floor
 where T2.SlotId is null AND T1.Floor='F2'

order by T1.SlotId asc;
GO