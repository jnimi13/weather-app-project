CREATE TABLE weather.Country(
	CountryID NUMBER GENERATED ALWAYS AS IDENTITY (START WITH 1 INCREMENT BY 1 CACHE 10) NOT NULL,
	CountryName varchar2(40) NOT NULL,

CONSTRAINT PK_CountryID PRIMARY KEY (CountryID)
);

CREATE TABLE weather.Location(
	LocationID NUMBER GENERATED ALWAYS AS IDENTITY (START WITH 1 INCREMENT BY 1 CACHE 10) NOT NULL,
	CountryID NUMBER NOT NULL,
	CityName varchar2(15) NOT NULL,
	Latitude float NULL,
	Longitude float NULL,
CONSTRAINT PK_Location PRIMARY KEY (LocationID),
CONSTRAINT FK_CountryLoc FOREIGN KEY (CountryID) REFERENCES weather.Country (CountryID)
);

CREATE TABLE weather.Station(
	StationID NUMBER GENERATED ALWAYS AS IDENTITY (START WITH 1 INCREMENT BY 1 CACHE 10) NOT NULL,
	StaionName nvarchar2(25) NOT NULL,
	LocationID NUMBER NOT NULL,
CONSTRAINT PK_Station PRIMARY KEY (StationID),
CONSTRAINT FK_LocationStation FOREIGN KEY (LocationID) REFERENCES weather.Location (LocationID)
);

CREATE TABLE weather.User_Table (
    UserID NUMBER GENERATED ALWAYS AS IDENTITY (START WITH 1 INCREMENT BY 1 CACHE 10) NOT NULL PRIMARY KEY,
    UserName VARCHAR2(20) NOT NULL,
    Password VARCHAR2(20) NOT NULL,
    FirstName VARCHAR2(25) NOT NULL,
    LastName VARCHAR2(25) NOT NULL,
    Email VARCHAR2(100) NOT NULL,
    LocationID NUMBER NULL,
    PhoneNumber VARCHAR2(15) NULL,
    FaxNumber VARCHAR2(50) NULL,
CONSTRAINT PK_Users PRIMARY KEY (UserID),
CONSTRAINT FK_LocationUser FOREIGN KEY (LocationID) REFERENCES sa.Location (LocationID)
);

CREATE TABLE weather.AlertType(
	AlertTypeID NUMBER GENERATED ALWAYS AS IDENTITY (START WITH 1 INCREMENT BY 1 CACHE 10) NOT NULL,
	AlertTypeDesc varchar2(15) NOT NULL,
CONSTRAINT PK_AlertType PRIMARY KEY (AlertTypeID)
);

CREATE TABLE weather.Alert(
	AlertID NUMBER GENERATED ALWAYS AS IDENTITY (START WITH 1 INCREMENT BY 1 CACHE 10) NOT NULL,
	StationID NUMBER NOT NULL,
	Timestamp timestamp NOT NULL,
	AlertTypeID NUMBER NOT NULL,
	Description varchar2(255) NULL,
CONSTRAINT PK_Alert PRIMARY KEY (AlertID),
CONSTRAINT FK_StationAlert FOREIGN KEY (StationID) REFERENCES weather.Station (StationID),
CONSTRAINT FK_AlertTypeAlert FOREIGN KEY (AlertTypeID) REFERENCES weather.AlertType (AlertTypeID)
);


CREATE TABLE weather.Weather(
	WeatherID NUMBER GENERATED ALWAYS AS IDENTITY (START WITH 1 INCREMENT BY 1 CACHE 10) NOT NULL,
    UserID	NUMBER NOT NULL,
    LocationID	NUMBER	NOT NULL,
    Timestamp	timestamp,
    Temperature	float,
    Humidity	float,
    WindSpeed	float,
CONSTRAINT PK_Weather PRIMARY KEY (WeatherID),
CONSTRAINT FK_WeatherUser FOREIGN KEY (UserID) REFERENCES weather.Users (UserID),
CONSTRAINT FK_WeatherLocation FOREIGN KEY (LocationID) REFERENCES weather.Location (LocationID)
);
       
     
CREATE TABLE weather.SubscriptionType(
	SubscriptionTypeID NUMBER GENERATED ALWAYS AS IDENTITY (START WITH 1 INCREMENT BY 1 CACHE 10) NOT NULL,
	SubscriptionDescription varchar2(20) NOT NULL,
CONSTRAINT PK_SubscriptionType PRIMARY KEY (SubscriptionTypeID)
);

CREATE TABLE weather.Subscription(
	SubscriptionID NUMBER GENERATED ALWAYS AS IDENTITY (START WITH 1 INCREMENT BY 1 CACHE 10) NOT NULL,
	UserID NUMBER NOT NULL,
	SubscriptionTypeID NUMBER NOT NULL,
	Price float NULL,
	StartDate Date NULL,
	EndDate Date NULL,
	Status smallint NULL,
	NotifyBySMS char(1) NULL,
	NotifyByEmail char(1) NULL,
	NotifyByPushing char(1) NULL,
	Frequency smallint NULL,
	AlertTypeID smallint NULL,
CONSTRAINT PK_Subscription PRIMARY KEY (SubscriptionID),
CONSTRAINT FK_SubscriptionSubscriptionType FOREIGN KEY (SubscriptionTypeID) REFERENCES weather.SubscriptionType (SubscriptionTypeID),
CONSTRAINT FK_SubscriptionAlertType FOREIGN KEY (AlertTypeID) REFERENCES weather.AlertType (AlertTypeID)
);

CREATE TABLE weather.Support(
	SupportID NUMBER GENERATED ALWAYS AS IDENTITY (START WITH 1 INCREMENT BY 1 CACHE 10) NOT NULL,
	UserID NUMBER NOT NULL,
	SubmissionDate date NOT NULL,
	IssueType smallint NOT NULL,
	Description varchar2(255) NULL,
	Status smallint NULL,
	PriorityType smallint NULL,
	AssignedTo NUMBER NULL,
	ResolutionDate date NULL,
	ResolutionDetails varchar2(255) NULL,
	FollowUpRequired char(1) NULL,

CONSTRAINT PK_Support PRIMARY KEY (SupportID),
CONSTRAINT FK_SupportUser FOREIGN KEY (UserID) REFERENCES weather.Users (UserID)
);
