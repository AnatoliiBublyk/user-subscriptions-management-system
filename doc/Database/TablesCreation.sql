USE user_subscriptions_management

-- Create the "admin_type" table
CREATE TABLE admin_types (
    id INT PRIMARY KEY IDENTITY(1, 1),
    admin_type varchar(255) NOT NULL UNIQUE
);

-- Create the "subscriptions" table
CREATE TABLE subscriptions (
    id INT PRIMARY KEY IDENTITY(1, 1),
    [key] varchar(255) NOT NULL UNIQUE,
    title varchar(255) NOT NULL,
    description text,
    duration integer NOT NULL DEFAULT 30,
    price integer NOT NULL
);

-- Create the "users" table
CREATE TABLE "users" (
    id INT PRIMARY KEY IDENTITY(1, 1),
    username varchar(255) NOT NULL UNIQUE,
    password_hash varchar(255) NOT NULL,
    email varchar(255) NOT NULL,
    admin_type_id integer REFERENCES admin_types(id),
    first_name varchar(255) NOT NULL,
    last_name varchar(255) NOT NULL,
    middle_name varchar(255),
    address varchar(255) NOT NULL,
    phone varchar(255) NOT NULL,
    zip varchar(255) NOT NULL,
	balance FLOAT NOT NULL DEFAULT 0,
    is_enabled BIT  NOT NULL DEFAULT 1
);	

-- Create the "user_subscriptions" table with foreign key references
CREATE TABLE user_subscriptions (
    [user_id] INT REFERENCES "users"(id),
    subscription_id INT REFERENCES subscriptions(id),
    [start_date] DATE NOT NULL,
	PRIMARY KEY ([user_id], subscription_id)
);
