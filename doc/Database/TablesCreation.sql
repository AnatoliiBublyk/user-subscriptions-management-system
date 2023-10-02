-- Create the "admin_type" table
CREATE TABLE admin_types (
    id serial PRIMARY KEY,
    admin_type varchar(255) NOT NULL UNIQUE
);

-- Create the "subscriptions" table
CREATE TABLE subscriptions (
    id serial PRIMARY KEY,
    key varchar(255) NOT NULL UNIQUE,
    title varchar(255) NOT NULL,
    description text,
    duration integer NOT NULL DEFAULT 30,
    price integer NOT NULL
);

-- Create the "users" table
CREATE TABLE "users" (
    id serial PRIMARY KEY,
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
    is_enabled boolean  NOT NULL DEFAULT true
);	

-- Create the "user_subscriptions" table with foreign key references
CREATE TABLE user_subscriptions (
    user_id integer REFERENCES "users"(id),
    subscription_id integer REFERENCES subscriptions(id),
    start_date date NOT NULL,
	PRIMARY KEY (user_id, subscription_id)
);
