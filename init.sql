CREATE TABLE address_type(
		type_id CHAR(1) PRIMARY KEY,
		type_description TEXT NOT NULL);

INSERT INTO address_type (
		type_id,
		type_description
	) VALUES
		('B', 'Business Address'),
		('H', 'Home Address');

CREATE TABLE customers(
		customer_id INTEGER PRIMARY KEY,
		first_name NVARCHAR(64) NOT NULL,
		last_name NVARCHAR(64) NOT NULL);

CREATE TABLE addresses(
		address_id INTEGER PRIMARY KEY,
		customer_id INTEGER NOT NULL,
		address_type_id CHAR(1) NOT NULL,
		line_1 VARCHAR(128) NOT NULL,
		line_2 VARCHAR(128),
		city VARCHAR(32) NOT NULL,
		state VARCHAR(32),
		zip CHAR(10),
		country VARCHAR(56) NOT NULL,
		CONSTRAINT customer_type_unique UNIQUE (customer_id, address_type_id),
		FOREIGN KEY (customer_id) REFERENCES customers(customer_id),
		FOREIGN KEY (address_type_id) REFERENCES address_type(type_id));
