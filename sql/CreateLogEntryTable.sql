USE yaladb;

CREATE TABLE LogEntry (
    id VARCHAR(255) PRIMARY KEY,
    appName VARCHAR(255) NOT NULL,
    sourceName VARCHAR(255),
    message VARCHAR(255) NOT NULL,
    logType INT NOT NULL,
    timestamp DATETIME NOT NULL,
    CONSTRAINT FK_LogType FOREIGN KEY (logType) REFERENCES LogType(id)
);
