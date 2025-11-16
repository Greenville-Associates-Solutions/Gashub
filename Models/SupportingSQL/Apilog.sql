CREATE TABLE Apilog (
    Id INTEGER PRIMARY KEY AUTOINCREMENT,
    Apiname TEXT NOT NULL,
    Eptype TEXT,
    Parameterlist TEXT,
    Apiresult TEXT,
    Hashid INT,
    Apinumber TEXT,
    Description TEXT
);
