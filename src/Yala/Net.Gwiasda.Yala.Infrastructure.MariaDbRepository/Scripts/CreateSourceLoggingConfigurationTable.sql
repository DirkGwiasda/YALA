USE yaladb;

CREATE TABLE SourceLoggingConfiguration (
    appName VARCHAR(255),
    sourceName VARCHAR(255),
    logLevel INT NOT NULL,
    PRIMARY KEY (appName, sourceName)
);
