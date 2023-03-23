CREATE TABLE "Users" (
  "UserId" UUID PRIMARY KEY,
  "Login" text,
  "Password" text
);

CREATE EXTENSION IF NOT EXISTS "uuid-ossp";

INSERT INTO "Users" ("UserId", "Login", "Password") 
VALUES (uuid_generate_v4(), 'admin', 'admin');