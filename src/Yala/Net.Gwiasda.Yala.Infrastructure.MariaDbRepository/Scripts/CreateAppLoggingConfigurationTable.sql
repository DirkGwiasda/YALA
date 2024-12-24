USE yaladb;

CREATE TABLE AppLoggingConfiguration (
    appName VARCHAR(255) PRIMARY KEY,
    logLevel INT NOT NULL
);
