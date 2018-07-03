

/* For security reasons the login is created disabled and with a random password. */
/****** Object:  Login [radsqluser]    Script Date: 6/26/2018 8:45:42 PM ******/
CREATE LOGIN [radsqluser] WITH PASSWORD=N'aAETghmnssS9BNEj0DSR+8z/gN7+CtVQsYKadSzB9+M=', DEFAULT_DATABASE=[Radicitus], DEFAULT_LANGUAGE=[us_english], CHECK_EXPIRATION=ON, CHECK_POLICY=ON
GO

ALTER LOGIN [radsqluser] DISABLE
GO




/****** Object:  User [radsqluser]    Script Date: 6/26/2018 8:45:15 PM ******/
CREATE USER [radsqluser] FOR LOGIN [radsqluser] WITH DEFAULT_SCHEMA=[rad]
GO



--CREATE USER [radsqluser] FOR LOGIN [radsqluser]
  --  WITH DEFAULT_SCHEMA = [rad];

