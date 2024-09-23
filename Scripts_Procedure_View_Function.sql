CREATE OR REPLACE PROCEDURE INSERTNEWUSER (
    p_UserName IN VARCHAR2,
    p_Password IN VARCHAR2,
    p_FirstName IN VARCHAR2,
    p_LastName IN VARCHAR2,
    p_Email IN VARCHAR2,
    p_LocationID IN NUMBER,
    p_PhoneNumber IN VARCHAR2,
    p_FaxNumber IN VARCHAR2)
    
AS 
BEGIN
    INSERT INTO sa.Users (UserName, Password, FirstName, LastName, Email, LocationID, PhoneNumber, FaxNumber)
    VALUES (p_UserName, p_Password, p_FirstName, p_LastName, p_Email, p_LocationID, p_PhoneNumber, p_FaxNumber);
      
END INSERTNEWUSER;

CREATE OR REPLACE VIEW sa.UserView AS
SELECT 
    UserID,
    UserName,
    Password,
    FirstName,
    LastName,
    Email,
    LocationID,
    PhoneNumber,
    FaxNumber
FROM 
    sa.Users;
    

CREATE OR REPLACE PROCEDURE       InsertWeatherData (
    p_UserID IN NUMBER,
    p_LocationID IN NUMBER,
    p_Timestamp timestamp,
    p_Temperature IN NUMBER,
    p_Humidity IN NUMBER,
    p_WindSpeed IN VARCHAR2)

AS 
BEGIN
    INSERT INTO sa.Weather (UserID, LocationID, Timestamp, Temperature, Humidity, WindSpeed )
    VALUES (p_UserID, p_LocationID, p_Timestamp, p_Temperature, p_Humidity, p_WindSpeed);
  
END InsertWeatherData;