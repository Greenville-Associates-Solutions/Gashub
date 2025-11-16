CREATE TABLE GasTickerPrice (
    Id INTEGER PRIMARY KEY AUTOINCREMENT,
    GasTicker TEXT NOT NULL,
    RecordDate DATE NOT NULL,
    Price NUMERIC(10,4) NOT NULL,
    Description TEXT,
    UNIQUE (GasTicker, RecordDate)
);

