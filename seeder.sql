use festispec
go

/* User Role */
INSERT INTO [festispec].[user_role] VALUES ('admin')
INSERT INTO [festispec].[user_role] VALUES ('inspector')
INSERT INTO [festispec].[user_role] VALUES ('management')
INSERT INTO [festispec].[user_role] VALUES ('medewerker')
go

INSERT INTO [festispec].[event_status] VALUES ('geinitialiseert')
INSERT INTO [festispec].[event_status] VALUES ('gecancelt')
INSERT INTO [festispec].[event_status] VALUES ('voorbereid')
INSERT INTO [festispec].[event_status] VALUES ('uitgevoert')
INSERT INTO [festispec].[event_status] VALUES ('voltooid')
GO

/* Payment Status */
INSERT INTO [festispec].[payment_status] VALUES ('betaald')
INSERT INTO [festispec].[payment_status] VALUES ('niet betaald')
GO

/* Quotation Status */
INSERT INTO [festispec].[quotation status] VALUES ('geaccepteerd')
INSERT INTO [festispec].[quotation status] VALUES ('geweigert')
INSERT INTO [festispec].[quotation status] VALUES ('in afwachting')
GO

/* Location */
INSERT INTO [festispec].[location] (postalcode, number, city, longtitude, latitude) VALUES ('5223DE', '215', 'Den Bosch', 5.2866376, 51.6885178)
INSERT INTO [festispec].[location] (postalcode, number, city, longtitude, latitude) VALUES ('5348PR', '10', 'Oss', 5.530532, 51.772522)
INSERT INTO [festispec].[location] (postalcode, number, city, longtitude, latitude) VALUES ('5349TG', '51', 'Oss', 5.561913, 51.764657)
INSERT INTO [festispec].[location] (postalcode, number, city, longtitude, latitude) VALUES ('5211NL', '16', 'Den Bosch', 5.306538, 51.689414)
INSERT INTO [festispec].[location] (postalcode, number, city, longtitude, latitude) VALUES ('5211HH', '10', 'Den Bosch', 5.303095, 51.686826)
INSERT INTO [festispec].[location] (postalcode, number, city, longtitude, latitude) VALUES ('5223DF', '215', 'Den Bosch', 5.286992, 51.688298)
INSERT INTO [festispec].[location] (postalcode, number, city, longtitude, latitude) VALUES ('2850', '1', 'Boom', 4.363520, 51.085990)
INSERT INTO [festispec].[location] (postalcode, number, city, longtitude, latitude) VALUES ('2050', '1', 'Middenvijver', 4.379180, 51.228910)
INSERT INTO [festispec].[location] (postalcode, number, city, longtitude, latitude) VALUES ('3521AL', '1', 'Utrecht', 5.107220, 52.084560)
go

-- PASSWORD = 'test123'
INSERT INTO [festispec].[user] (user_role, location_id, email, password, first_name, last_name) 
VALUES ('admin', 1, 'john@avans.nl', 'ecd71870d1963316a97e3ac3408c9835ad8cf0f3c1bc703527c30265534f75ae', 'John', 'Doe')
INSERT INTO [festispec].[user] (user_role, location_id, email, password, first_name, last_name) 
VALUES ('inspector', 2, 'ben@avans2.nl', 'ecd71870d1963316a97e3ac3408c9835ad8cf0f3c1bc703527c30265534f75ae', 'Ben', 'Dover')
INSERT INTO [festispec].[user] (user_role, location_id, email, password, first_name, last_name) 
VALUES ('management', 3, 'stijn@avans3.nl', 'ecd71870d1963316a97e3ac3408c9835ad8cf0f3c1bc703527c30265534f75ae', 'Stijn', 'Smulders')
INSERT INTO [festispec].[user] (user_role, location_id, email, password, first_name, last_name) 
VALUES ('medewerker', 4, 'ger@avans4.nl', 'ecd71870d1963316a97e3ac3408c9835ad8cf0f3c1bc703527c30265534f75ae', 'Ger', 'Saris')
go

/* Customer */
INSERT INTO [festispec].[customer] ([location_id],[name],[email],[telephone_number],[kvk]) 
VALUES (4,'OTTEN SPEELGOED','otten@otten-speelgoed.nl','','16026257')
INSERT INTO [festispec].[customer] ([location_id],[name],[email],[telephone_number],[kvk]) 
VALUES (5,'Pamgene','info@pamgene.nl','','17126969')
INSERT INTO [festispec].[customer] ([location_id],[name],[email],[telephone_number],[kvk]) 
VALUES (6,'Avans','info@avans.nl','0412699000','41104408')
GO

/* Event */
INSERT INTO [festispec].[event] ([customer_id],[location_id],[event_status],[payment_status],[name],[start_date],[end_date],[created_at])
VALUES (3,7,'geinitialiseert','niet betaald','Tomorrowland','2019-12-11','2019-12-14','2019-11-17')
GO
INSERT INTO [festispec].[event] ([customer_id],[location_id],[event_status],[payment_status],[name],[start_date],[end_date],[created_at])
VALUES (3,8,'geinitialiseert','niet betaald','Elrow Town','2020-01-11','2020-01-14','2019-11-17')
INSERT INTO [festispec].[event] ([customer_id],[location_id],[event_status],[payment_status],[name],[start_date],[end_date],[created_at])
VALUES (3,9,'geinitialiseert','niet betaald','A State of Trance','2021-06-04','2021-06-8','2019-11-17')
GO

/* Quotation */
INSERT INTO [festispec].[quotation] ([event_id],[price],[quotation status_status],[text]) 
VALUES (1, 3599, 'geweigert','Een zeer slecht geprijsde offerte')
INSERT INTO [festispec].[quotation] ([event_id],[price],[quotation status_status],[text]) 
VALUES (1, 2999, 'in afwachting','Een zeer goed geprijsde offerte')
INSERT INTO [festispec].[quotation] ([event_id],[price],[quotation status_status],[text]) 
VALUES (2, 3499, 'in afwachting','Een zeer goed geprijsde offerte')
INSERT INTO [festispec].[quotation] ([event_id],[price],[quotation status_status],[text]) 
VALUES (3, 2000, 'geaccepteerd','Een zeer goed geprijsde offerte')
GO

/* Event Inspection */
INSERT INTO [festispec].[event_inspection] ([event_id], [name], [execution_date], [created_at])
VALUES (1, 'Tomorrowland', '2019-12-11', '2019-11-18')
INSERT INTO [festispec].[event_inspection] ([event_id], [name], [execution_date], [created_at])
VALUES (2, 'Elrow Town', '2020-01-11', '2019-11-18')
INSERT INTO [festispec].[event_inspection] ([event_id], [name], [execution_date], [created_at])
VALUES (3, 'A State of Trance', '2021-06-04', '2019-12-17')
GO

/* Event Inspection Form */
INSERT INTO [festispec].[inspection_form] ([event_inspection_id], [Name], [floor_plan],  [created_at])
VALUES (1, 'Toiletcontrole', '2A', '2019-11-24')
INSERT INTO [festispec].[inspection_form] ([event_inspection_id], [Name], [floor_plan], [created_at])
VALUES (1, 'Jassencontrole', '1B', '2019-11-24')
INSERT INTO [festispec].[inspection_form] ([event_inspection_id], [Name], [floor_plan], [created_at])
VALUES (2, 'Ticketcontrole', '1A', '2019-11-24')
INSERT INTO [festispec].[inspection_form] ([Name], [floor_plan], [created_at])
VALUES ('Template: Podium Inspectie', '', '2019-11-24')
INSERT INTO [festispec].[inspection_form] ([Name], [floor_plan], [created_at])
VALUES ('Template: Brand Route controle', '', '2019-11-24')
INSERT INTO [festispec].[inspection_form] ([Name], [floor_plan], [created_at])
VALUES ('Template: Ticketcontrole', '', '2019-11-24')
GO

/* Assignees */
INSERT INTO [festispec].[assignees] ([user_id], [inspection_form_id])
VALUES(2, 1)
INSERT INTO [festispec].[assignees] ([user_id], [inspection_form_id])
VALUES(2, 2)
GO

/* Question Types */
INSERT INTO [festispec].[question_type]
VALUES ('image')
INSERT INTO [festispec].[question_type]
VALUES ('multiple_choice')
INSERT INTO [festispec].[question_type]
VALUES ('open')
GO

/* Form Questions */
INSERT INTO [festispec].[form_question] ([inspection_id] ,[type] ,[order] ,[instructions] ,[question] ,[created_at])
VALUES (1, 'open', 1, 'Toiletten checken', 'Voldoen alle wcs aan de EU-standaard?', GETDATE())
INSERT INTO [festispec].[form_question] ([inspection_id] ,[type] ,[order] ,[instructions] ,[question] ,[created_at])
VALUES (1, 'open', 4, 'Toiletpapier kwaliteit controleren', 'Hoe zacht is het toiletpapier?', GETDATE())
INSERT INTO [festispec].[form_question] ([inspection_id] ,[type] ,[order] ,[instructions] ,[question] ,[created_at])
VALUES (1, 'multiple_choice', 2, 'Nooduitgangen op het terrein tellen', 'Zijn er genoeg nooduitgangen?', GETDATE())
INSERT INTO [festispec].[form_question] ([inspection_id] ,[type] ,[order] ,[instructions] ,[question] ,[created_at])
VALUES (1, 'image', 3, 'Controleer breedte van de hoofdingang', 'Upload een foto van de hoofdingang.', GETDATE())

INSERT INTO [festispec].[form_question] ([inspection_id] ,[type] ,[order] ,[instructions] ,[question] ,[created_at])
VALUES (2, 'multiple_choice', 1, 'Controleer of er genoeg plek is voor jassen', 'Is er genoeg ruimte voor maximaal 500 jassen?', GETDATE())
INSERT INTO [festispec].[form_question] ([inspection_id] ,[type] ,[order] ,[instructions] ,[question] ,[created_at])
VALUES (2, 'open', 2, 'Controle metaaldetectie jassen', 'Hoeveel metalen objecten zijn er gedecteerd?', GETDATE())
GO

/* Multiple choice question choices */
INSERT INTO [festispec].[multplechoice] ([option], [form_question_id])
VALUES ('Ja',3)
INSERT INTO [festispec].[multplechoice] ([option], [form_question_id])
VALUES ('Nee',3)
INSERT INTO [festispec].[multplechoice] ([option], [form_question_id])
VALUES ('Ja',5)
INSERT INTO [festispec].[multplechoice] ([option], [form_question_id])
VALUES ('Nee',5)
GO