CREATE TABLE VehicleMake (
  ID INT IDENTITY(1,1) PRIMARY KEY,
  Name VARCHAR(50) ,
  Abrv VARCHAR(10)
);


CREATE TABLE VehicleModel (
  ID INT IDENTITY(1,1) PRIMARY KEY,
  MakeId INT ,
  Name VARCHAR(50) ,
  Abrv VARCHAR(10),
  FOREIGN KEY (MakeId) REFERENCES VehicleMake(ID),
);