# Database Documentation: user_subscriptions_management

## Description
This database is designed to facilitate the management of user subscriptions and associated information within an API. It allows for the creation and tracking of user accounts, various types of subscriptions, and the relationships between users and their subscriptions.

## Tables

1. **admin_type**
   - `id`: An auto-incremented unique identifier for admin types.
   - `admin_type`: Describes the type of administrator (e.g., super admin, moderator).

2. **subscriptions**
   - `id`: An auto-incremented unique identifier for subscription plans.
   - `key`: A unique key to identify subscription plans.
   - `title`: The title or name of the subscription plan.
   - `description`: A text field for a detailed description of the subscription plan.
   - `duration`: The duration of the subscription in days (default: 30 days).
   - `price`: The price of the subscription plan.

3. **user**
   - `id`: An auto-incremented unique identifier for users.
   - `username`: A unique username for user identification.
   - `password_hash`: A hashed password for user authentication.
   - `email`: The email address of the user.
   - `admin_type_id`: A reference to the admin_type table, indicating the type of admin user (if applicable).
   - `first_name`: First name of the user.
   - `last_name`: Last name of the user.
   - `middle_name`: Middle name of the user (nullable).
   - `address`: User's address information.
   - `phone`: User's phone number.
   - `zip`: User's ZIP or postal code.
   - `is_enabled`: A flag (default: true) to indicate whether the user account is enabled.

4. **user_subscriptions**
   - `user_id`: A reference to the user table, linking users to their subscriptions.
   - `subscription_id`: A reference to the subscriptions table, indicating the subscribed plan.
   - `start_date`: The date when the subscription begins.

## Tools and Technologies

- **Database Management System**: SQL Server 2022
  - SQL Server is used as the backend database system for this API.
  - Ensure that you have SQL Server 2022 or a compatible version installed for proper functionality.

## Purpose

This database serves as the backbone for a user subscription management system. It enables the creation and maintenance of user accounts with various levels of administration and allows users to subscribe to different plans, each with its title, description, duration, and price. The user_subscriptions table tracks user-subscription relationships, including the start date of each subscription.

## Usage

- User account creation and management, including admin roles.
- Subscription plan creation and modification.
- User subscription tracking and management.
- API endpoints can be built on top of this database to provide functionality for user registration, subscription management, and more.

**Important Note**: Ensure that proper security measures are implemented, such as hashing passwords and validating user inputs, to protect user data and maintain data integrity.