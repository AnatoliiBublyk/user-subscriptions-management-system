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

-- Insert data into the "users_profile" table
INSERT INTO users_profile (email, first_name, last_name, middle_name, [address], phone, zip)
VALUES
    ('admin1@example.com', 'John', 'Doe', NULL, '123 Main St', '555-123-4567', '12345'),
    ('user1@example.com', 'Alice', 'Smith', 'Mary', '456 Elm St', '555-987-6543', '67890'),
    ('user2@example.com', 'Bob', 'Johnson', NULL, '789 Oak St', '555-567-8901', '34567');

-- Insert data into the "users" table
INSERT INTO "users" (username, password_hash, admin_type_id)
VALUES
    ('admin1', 'c184719e296d9f812f353ff74779ae3a2f340bc43078ae449131207edeb3605a',  2),
    ('user1', 'b1f0dbf36d514e57144d7d3041de910082106eda07193004083157f703b2d5cd',  1),
    ('user2', '3d559a6af685f76e5411df259797644c9f34ac69e401dbb35f0b2fabdb4b363c',  1);

