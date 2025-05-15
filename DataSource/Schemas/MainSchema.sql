DROP TABLE IF EXISTS tbl_users;

CREATE TABLE tbl_users
(
    PK_user_id INT PRIMARY KEY IDENTITY(1,1),
    user_name VARCHAR(255) NOT NULL,
    user_password VARCHAR(255) NOT NULL
);