create table flights(
    id serial primary key,
    airline_company_id bigint not null,
    origin_country_id int not null,
    destination_country_id int not null,
    departure_time timestamp not null,
    landing_time timestamp not null,
    remaining_tickets int,
    foreign key (airline_company_id) references airline_companies(id),
    foreign key (origin_country_id) references countries(id),
    foreign key (destination_country_id) references countries(id)
);

create table tickets(
    id serial primary key,
    flight_id bigint not null,
    customer_id bigint not null,
    foreign key (flight_id) references flights(id),
    foreign key (customer_id) references customers(id),
    unique (flight_id,customer_id)
);

create table countries(
    id int primary key generated always as identity,
    name text not null,
    code_name text not null,
    unique (name, code_name)
);

create table customers(
    id serial primary key,
    first_name text not null,
    last_name text not null,
    address text not null,
    phone text not null,
    credit_card_no text not null,
    user_id bigint not null,
    foreign key(user_id) references users(id),
    unique (phone,credit_card_no,user_id)
);

create table airline_companies(
    id serial primary key ,
    name text not null,
    country_id int not null,
    user_id bigint not null,
    foreign key (country_id) references countries(id),
    foreign key (user_id) references users(id),
    foreign key (user_id) references administrators(user_id),
    unique (name, user_id)
);

create table users(
    id serial primary key,
    username text not null,
    password text not null,
    email text not null,
    user_role int not null,
    foreign key (user_role) references user_roles(id),
    unique (username,password,email)
);

create table user_roles(
    id int primary key generated always as identity,
    role_name text not null,
    unique (role_name)
);

create table administrators(
    id int primary key generated always as identity,
    first_name text not null,
    last_name text not null,
    level int not null,
    user_id bigint not null,
    unique (user_id)
);

--inserting data to countries table
insert into countries(name, code_name)
values('United Stats of America', 'USA');
insert into countries(name, code_name)
values('JAPAN', 'JPN');
insert into countries(name, code_name)
values('Algeria', 'DZA');
insert into countries(name, code_name)
values('Brazil', 'BRA');
insert into countries(name, code_name)
values('Canada', 'CAN');
insert into countries(name, code_name)
values('Chile', 'CHL');
insert into countries(name, code_name)
values('Ireland', 'IRL');
insert into countries(name, code_name)
values('Italy', 'ITA');
insert into countries(name, code_name)
values('Mexico', 'MEX');
insert into countries(name, code_name)
values('Spain', 'ESP');
insert into countries(name, code_name)
values('South Africa', 'ZAF');
insert into countries(name, code_name)
values('Tunisia', 'TUN');
insert into countries(name, code_name)
values('Kuwait', 'KWT');
insert into countries(name, code_name)
values('Costa Rica', 'CRI');
insert into countries(name, code_name)
values('Cuba', 'CUB');

--inserting into user_roles
insert into user_roles(role_name)
values('Administrator');
insert into user_roles(role_name)
values('Customer');
insert into user_roles(role_name)
values('Airline Company');

--function to generate password
create extension pgcrypto;

--inserting into users
insert into users(username, password, email, user_role)
values('Jack', crypt('jack1993', gen_salt('bf')),'jack@gmail.com', 1);
insert into users(username, password, email, user_role)
values('Sammy', crypt('sammi32', gen_salt('bf')),'sammi@gmail.com', 3);
insert into users(username, password, email, user_role)
values('natalie', crypt('nataly523', gen_salt('bf')),'nat@gmail.com', 1);
insert into users(username, password, email, user_role)
values('Fatima', crypt('fatima78', gen_salt('bf')),'fatima@gmail.com', 2);
insert into users(username, password, email, user_role)
values('Ali', crypt('Ali28', gen_salt('bf')),'ali@gmail.com', 2);
insert into users(username, password, email, user_role)
values('Omar', crypt('omar45', gen_salt('bf')),'omari@gmail.com', 2);
insert into users(username, password, email, user_role)
values('Amaliy', crypt('amaliy206', gen_salt('bf')),'amy@gmail.com', 3);
insert into users(username, password, email, user_role)
values('Zahra', crypt('zahra542', gen_salt('bf')),'zahra@gmail.com', 1);

-- inserting into customers also here i used the crypt extension to credit card
insert into customers(first_name, last_name, address, phone, credit_card_no, user_id)
values('Jack','Black','Carmen-CAN', '05036241789',crypt('15243698700',gen_salt('bf')), 1);
insert into customers(first_name, last_name, address, phone, credit_card_no, user_id)
values('Ali','Farahat','Carmen-CAN', '05333526890',crypt('52148003647',gen_salt('bf')), 3);
insert into customers(first_name, last_name, address, phone, credit_card_no, user_id)
values('Omar','Zaher','Los Angeles-USA', '05478920136',crypt('52014563987',gen_salt('bf')), 4);

--insert into Administrator
insert into administrators(first_name, last_name, level, user_id)
values('Zahra', 'Mowrad', 2, 11);
insert into administrators(first_name, last_name, level, user_id)
values('Natalie', 'Paola', 1, 4);
insert into administrators(first_name, last_name, level, user_id)
values('Jack', 'Black', 1, 1);

--inserting into Airlines companies
insert into airline_companies(name, country_id, user_id)
values('Japan Airlines',2,1);
insert into airline_companies(name, country_id, user_id)
values('Air Canada',5,4);
insert into airline_companies(name, country_id, user_id)
values('United Airline',3,1);
insert into airline_companies(name, country_id, user_id)
values('Icelandair',8,4);
insert into airline_companies(name, country_id, user_id)
values('Peach',9,1);
insert into airline_companies(name, country_id, user_id)
values('American Eagle',1,11);

--inserting into flights
insert into flights(airline_company_id, origin_country_id, destination_country_id, departure_time, landing_time, remaining_tickets)
values(1,2,8,'11-01-2021 16:58', '12-01-2021 05:55',6);
insert into flights(airline_company_id, origin_country_id, destination_country_id, departure_time, landing_time, remaining_tickets)
values(8,1,14,'01-15-2021 11:00', '01-16-2021 01:55',2);
insert into flights(airline_company_id, origin_country_id, destination_country_id, departure_time, landing_time, remaining_tickets)
values(4,5,13,'01-31-2021 20:05', '02-01-2021 05:50',10);
insert into flights(airline_company_id, origin_country_id, destination_country_id, departure_time, landing_time, remaining_tickets)
values(7,1,3,'01-31-2021 20:05', '02-01-2021 04:50',1);
insert into flights(airline_company_id, origin_country_id, destination_country_id, departure_time, landing_time, remaining_tickets)
values(6,4,10,'02-02-2021 16:58', '01-03-2021 05:55',15);

--inserting into tickets
insert into tickets(flight_id, customer_id)
values(2,3);
insert into tickets(flight_id, customer_id)
values(5,4);
insert into tickets(flight_id, customer_id)
values(4,1);
