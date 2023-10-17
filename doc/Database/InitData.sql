USE user_subscriptions_management;

-- Insert data into the "admin_types" table
INSERT INTO admin_types (admin_type)
VALUES
    ('none'),
    ('admin');

-- Insert data into the "subscriptions" table
INSERT INTO subscriptions ([key], title, [description], duration, price)
VALUES
    ('free', 'Free Subscription', 'Access to basic features', 30, 0),
    ('basic', 'Basic Subscription', 'Access to basic features', 30, 10),
    ('premium', 'Premium Subscription', 'Access to premium features', 30, 25),
    ('pro', 'Pro Subscription', 'Access to pro features', 30, 40);

-- Insert data into the "users" table
INSERT INTO "users" (username, password_hash, email, admin_type_id, first_name, last_name, middle_name, [address], phone, zip)
VALUES
    ('admin1', 'password123', 'admin1@example.com', 2, 'John', 'Doe', NULL, '123 Main St', '555-123-4567', '12345'),
    ('user1', 'userpass456', 'user1@example.com', 1, 'Alice', 'Smith', 'Mary', '456 Elm St', '555-987-6543', '67890'),
    ('user2', 'userpass789', 'user2@example.com', 1, 'Bob', 'Johnson', NULL, '789 Oak St', '555-567-8901', '34567');

