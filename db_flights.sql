--
-- PostgreSQL database dump
--

-- Dumped from database version 13.1
-- Dumped by pg_dump version 13.1

SET statement_timeout = 0;
SET lock_timeout = 0;
SET idle_in_transaction_session_timeout = 0;
SET client_encoding = 'UTF8';
SET standard_conforming_strings = on;
SELECT pg_catalog.set_config('search_path', '', false);
SET check_function_bodies = false;
SET xmloption = content;
SET client_min_messages = warning;
SET row_security = off;

--
-- Name: pgcrypto; Type: EXTENSION; Schema: -; Owner: -
--

CREATE EXTENSION IF NOT EXISTS pgcrypto WITH SCHEMA public;


--
-- Name: EXTENSION pgcrypto; Type: COMMENT; Schema: -; Owner: 
--

COMMENT ON EXTENSION pgcrypto IS 'cryptographic functions';


--
-- Name: sp_add_admin(text, text, integer, bigint); Type: FUNCTION; Schema: public; Owner: postgres
--

CREATE FUNCTION public.sp_add_admin(_admin_name text, _admin_surname text, _admin_level integer, _userid bigint) RETURNS TABLE(first text, last text, admin_level integer, userid bigint)
    LANGUAGE plpgsql
    AS $$
    BEGIN
            if not exists(select userId, count(id) from administrators
                  GROUP BY userId HAVING count(id) >1 Order By count(id)) then
                insert into administrators(first_name, last_name, level, user_id)
                values (_admin_name, _admin_surname, _admin_level, _userID);
            else
                raise exception 'duplicated user ID: %', _userID using hint = 'check the user id again';
            end if;
    end;
    $$;


ALTER FUNCTION public.sp_add_admin(_admin_name text, _admin_surname text, _admin_level integer, _userid bigint) OWNER TO postgres;

--
-- Name: sp_add_countries(text, text); Type: FUNCTION; Schema: public; Owner: postgres
--

CREATE FUNCTION public.sp_add_countries(_country_name text, _country_code text) RETURNS TABLE(country_name text, country_code text)
    LANGUAGE plpgsql
    AS $$
    BEGIN
        if not exists(select name, code_name, count(id) from countries
                  GROUP BY name,code_name HAVING count(id) >1 Order By count(id)) then
            insert into countries(name, code_name)
            values(initcap(_country_name), upper(_country_code));
        else
            raise exception 'duplicated country name or country code: %,%', _country_name, _country_code
                using hint = 'check the country name or country code again';
        end if;
    end;
    $$;


ALTER FUNCTION public.sp_add_countries(_country_name text, _country_code text) OWNER TO postgres;

--
-- Name: sp_get_admin_by_id(bigint); Type: FUNCTION; Schema: public; Owner: postgres
--

CREATE FUNCTION public.sp_get_admin_by_id(a_id bigint) RETURNS TABLE(first text, last text, admin_level integer, userid bigint)
    LANGUAGE plpgsql
    AS $$
    BEGIN
        return query
        SELECT first_name, last_name, level, user_id from administrators
        where id = a_id;
    end;
    $$;


ALTER FUNCTION public.sp_get_admin_by_id(a_id bigint) OWNER TO postgres;

--
-- Name: sp_get_all_administrator(); Type: FUNCTION; Schema: public; Owner: postgres
--

CREATE FUNCTION public.sp_get_all_administrator() RETURNS TABLE(first text, last text, admin_level integer, userid bigint)
    LANGUAGE plpgsql
    AS $$
    BEGIN
        return query
        SELECT first_name, last_name, level, user_id from administrators;
    end;
    $$;


ALTER FUNCTION public.sp_get_all_administrator() OWNER TO postgres;

--
-- Name: sp_get_all_countries(); Type: FUNCTION; Schema: public; Owner: postgres
--

CREATE FUNCTION public.sp_get_all_countries() RETURNS TABLE(country_id integer, country_name text, country_code text)
    LANGUAGE plpgsql
    AS $$
    BEGIN
        return query
        select id, name, code_name from countries;
    end;
    $$;


ALTER FUNCTION public.sp_get_all_countries() OWNER TO postgres;

--
-- Name: sp_get_country_by_id(bigint); Type: FUNCTION; Schema: public; Owner: postgres
--

CREATE FUNCTION public.sp_get_country_by_id(c_id bigint) RETURNS TABLE(country text, code text)
    LANGUAGE plpgsql
    AS $$
    BEGIN
        return query
        SELECT name, code_name from countries
        where id = c_id;
    end;
    $$;


ALTER FUNCTION public.sp_get_country_by_id(c_id bigint) OWNER TO postgres;

--
-- Name: sp_remove_admin(bigint); Type: FUNCTION; Schema: public; Owner: postgres
--

CREATE FUNCTION public.sp_remove_admin(a_id bigint) RETURNS TABLE(id integer, first text, last text, admin_level integer, userid bigint)
    LANGUAGE plpgsql
    AS $$
    BEGIN
        return query
        delete from administrators_id_seq where id = a_id;
    end;
    $$;


ALTER FUNCTION public.sp_remove_admin(a_id bigint) OWNER TO postgres;

--
-- Name: sp_remove_country(bigint); Type: FUNCTION; Schema: public; Owner: postgres
--

CREATE FUNCTION public.sp_remove_country(c_id bigint) RETURNS TABLE(country_id integer, country text, code text)
    LANGUAGE plpgsql
    AS $$
    BEGIN
        return query
        delete from countries where id = c_id;
    end;
    $$;


ALTER FUNCTION public.sp_remove_country(c_id bigint) OWNER TO postgres;

--
-- Name: sp_update_admin(text, text, integer, bigint, integer); Type: FUNCTION; Schema: public; Owner: postgres
--

CREATE FUNCTION public.sp_update_admin(_admin_name text, _admin_surname text, _admin_level integer, _userid bigint, new_id integer) RETURNS TABLE(first text, last text, admin_level integer, userid bigint)
    LANGUAGE plpgsql
    AS $$
    BEGIN
        return query
        update administrators
        set administrators.first_name = _admin_name,
            administrators.last_name = _admin_surname,
            administrators.level = _admin_level,
            administrators.user_id = _userID
            where administrators.id = new_id;

    end;
    $$;


ALTER FUNCTION public.sp_update_admin(_admin_name text, _admin_surname text, _admin_level integer, _userid bigint, new_id integer) OWNER TO postgres;

--
-- Name: sp_update_name_code_country(text, text, integer); Type: FUNCTION; Schema: public; Owner: postgres
--

CREATE FUNCTION public.sp_update_name_code_country(_country_code text, _country_name text, new_id integer) RETURNS TABLE(country_id integer, country text, code text)
    LANGUAGE plpgsql
    AS $$
    BEGIN
        return query
        update countries
        set countries.name = initcap(_country_name),
            countries.code_name = upper(_country_code)
            where countries.ID = new_id;

    end;
    $$;


ALTER FUNCTION public.sp_update_name_code_country(_country_code text, _country_name text, new_id integer) OWNER TO postgres;

SET default_tablespace = '';

SET default_table_access_method = heap;

--
-- Name: administrators; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.administrators (
    id integer NOT NULL,
    first_name text NOT NULL,
    last_name text NOT NULL,
    level integer NOT NULL,
    user_id bigint NOT NULL
);


ALTER TABLE public.administrators OWNER TO postgres;

--
-- Name: administrators_id_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

ALTER TABLE public.administrators ALTER COLUMN id ADD GENERATED ALWAYS AS IDENTITY (
    SEQUENCE NAME public.administrators_id_seq
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1
);


--
-- Name: airline_companies; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.airline_companies (
    id integer NOT NULL,
    name text NOT NULL,
    country_id integer NOT NULL,
    user_id bigint NOT NULL
);


ALTER TABLE public.airline_companies OWNER TO postgres;

--
-- Name: airline_companies_id_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public.airline_companies_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE public.airline_companies_id_seq OWNER TO postgres;

--
-- Name: airline_companies_id_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public.airline_companies_id_seq OWNED BY public.airline_companies.id;


--
-- Name: countries; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.countries (
    id integer NOT NULL,
    name text NOT NULL,
    code_name text NOT NULL
);


ALTER TABLE public.countries OWNER TO postgres;

--
-- Name: countries_id_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

ALTER TABLE public.countries ALTER COLUMN id ADD GENERATED ALWAYS AS IDENTITY (
    SEQUENCE NAME public.countries_id_seq
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1
);


--
-- Name: customers; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.customers (
    id integer NOT NULL,
    first_name text NOT NULL,
    last_name text NOT NULL,
    address text NOT NULL,
    phone text NOT NULL,
    credit_card_no text NOT NULL,
    user_id bigint NOT NULL
);


ALTER TABLE public.customers OWNER TO postgres;

--
-- Name: customers_id_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public.customers_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE public.customers_id_seq OWNER TO postgres;

--
-- Name: customers_id_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public.customers_id_seq OWNED BY public.customers.id;


--
-- Name: flights; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.flights (
    id integer NOT NULL,
    airline_company_id bigint NOT NULL,
    origin_country_id integer NOT NULL,
    destination_country_id integer NOT NULL,
    departure_time timestamp without time zone NOT NULL,
    landing_time timestamp without time zone NOT NULL,
    remaining_tickets integer
);


ALTER TABLE public.flights OWNER TO postgres;

--
-- Name: flights_id_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public.flights_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE public.flights_id_seq OWNER TO postgres;

--
-- Name: flights_id_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public.flights_id_seq OWNED BY public.flights.id;


--
-- Name: tickets; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.tickets (
    id integer NOT NULL,
    flight_id bigint NOT NULL,
    customer_id bigint NOT NULL
);


ALTER TABLE public.tickets OWNER TO postgres;

--
-- Name: tickets_id_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public.tickets_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE public.tickets_id_seq OWNER TO postgres;

--
-- Name: tickets_id_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public.tickets_id_seq OWNED BY public.tickets.id;


--
-- Name: user_roles; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.user_roles (
    id integer NOT NULL,
    role_name text NOT NULL
);


ALTER TABLE public.user_roles OWNER TO postgres;

--
-- Name: user_roles_id_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

ALTER TABLE public.user_roles ALTER COLUMN id ADD GENERATED ALWAYS AS IDENTITY (
    SEQUENCE NAME public.user_roles_id_seq
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1
);


--
-- Name: users; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.users (
    id integer NOT NULL,
    username text NOT NULL,
    password text NOT NULL,
    email text NOT NULL,
    user_role integer NOT NULL
);


ALTER TABLE public.users OWNER TO postgres;

--
-- Name: users_id_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public.users_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE public.users_id_seq OWNER TO postgres;

--
-- Name: users_id_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public.users_id_seq OWNED BY public.users.id;


--
-- Name: airline_companies id; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.airline_companies ALTER COLUMN id SET DEFAULT nextval('public.airline_companies_id_seq'::regclass);


--
-- Name: customers id; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.customers ALTER COLUMN id SET DEFAULT nextval('public.customers_id_seq'::regclass);


--
-- Name: flights id; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.flights ALTER COLUMN id SET DEFAULT nextval('public.flights_id_seq'::regclass);


--
-- Name: tickets id; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.tickets ALTER COLUMN id SET DEFAULT nextval('public.tickets_id_seq'::regclass);


--
-- Name: users id; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.users ALTER COLUMN id SET DEFAULT nextval('public.users_id_seq'::regclass);


--
-- Data for Name: administrators; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public.administrators (id, first_name, last_name, level, user_id) FROM stdin;
1	Natalie	Paola	1	4
2	Jack	Black	1	1
3	Zahra	Mowrad	2	11
\.


--
-- Data for Name: airline_companies; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public.airline_companies (id, name, country_id, user_id) FROM stdin;
1	Japan Airlines	2	1
4	Air Canada	5	4
5	Icelandair	8	4
6	Peach	9	1
7	United Airline	3	1
8	American Eagle	1	11
\.


--
-- Data for Name: countries; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public.countries (id, name, code_name) FROM stdin;
1	United Stats of America	USA
2	JAPAN	JPN
3	Algeria	DZA
4	Brazil	BRA
5	Canada	CAN
6	Chile	CHL
7	Ireland	IRL
8	Italy	ITA
9	Mexico	MEX
10	Spain	ESP
11	South Africa	ZAF
13	Tunisia	TUN
14	Kuwait	KWT
15	Costa Rica	CRI
16	Cuba	CUB
17	New Zealand	NZL
\.


--
-- Data for Name: customers; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public.customers (id, first_name, last_name, address, phone, credit_card_no, user_id) FROM stdin;
1	Jack	Black	Carmen-canada	05036241789	$2a$06$5J1iSMZAtbXs9rUfpphjLOlRqbVRiNflk/WjNJ7dXgQVpi0uCu6Ki	1
3	Ali	Farahat	Carmen-CAN	05333526890	$2a$06$k5p/SgRdsxynANaG6Nlcmu38bKD652xG8uQgUICdxidcK2NRcGDJC	3
4	Omar	Zaher	Los Angeles-USA	05478920136	$2a$06$P40U2nOEaL4kg9SHr5S/deED6qULmhxqLgnzgN7E3HorKfOPPQ/pa	4
\.


--
-- Data for Name: flights; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public.flights (id, airline_company_id, origin_country_id, destination_country_id, departure_time, landing_time, remaining_tickets) FROM stdin;
2	1	2	8	2021-11-01 16:58:00	2021-12-01 05:55:00	6
3	8	1	14	2021-01-15 11:00:00	2021-01-16 01:55:00	2
4	4	5	13	2021-01-31 20:05:00	2021-02-01 05:50:00	10
5	7	1	3	2021-01-31 20:05:00	2021-02-01 04:50:00	1
6	6	4	10	2021-02-02 16:58:00	2021-01-03 05:55:00	15
\.


--
-- Data for Name: tickets; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public.tickets (id, flight_id, customer_id) FROM stdin;
1	2	3
2	5	4
3	4	1
\.


--
-- Data for Name: user_roles; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public.user_roles (id, role_name) FROM stdin;
1	Administrator
2	Customer
3	Airline Company
\.


--
-- Data for Name: users; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public.users (id, username, password, email, user_role) FROM stdin;
1	Jack	$2a$06$kO2n/Wf1ghOjjTmYTV8dR.NQlfMxJf.lLg7Amg.gW4gB8rW1X3m66	jack@gmail.com	1
3	Sammy	$2a$06$xqicarczJFynLmAQR7Z1NOXZI.j8ORT9E9YOX5qGx/wkheOTuOt8K	sammi@gmail.com	3
4	natalie	$2a$06$MzLeF603i8fZDqNhxhkhRuQFdFvRtx/L8aImBcOhkmywriHpvvvEu	nat@gmail.com	1
5	Fatima	$2a$06$Sfi2t54CMabSHQhd5XPsKuijMACidB0M9avy8at2iGyhytA8FiWuS	fatima@gmail.com	2
6	Ali	$2a$06$zB1IdGU3nNh7b68J53nB3.yG5BMIcpy.qUccQ6eZ16jlHmHPmLM9q	ali@gmail.com	2
9	Omar	$2a$06$QTCCBtkR4f/G0GEtnfw7DO0MjYwvmpGd.laKTLEXncjqpA1ZvhoZq	omari@gmail.com	2
10	Amaliy	$2a$06$.VOxllWbgatRv9yKHXgZb.YgigVKD4607ck1ozkcw3AphJumiOqvi	amy@gmail.com	3
11	Zahra	$2a$06$1/DgaXhlIi6smXLjWoPmlOZtGAAgyMZC.Akgx1HUJfs25UFLN7Uju	zahra@gmail.com	1
\.


--
-- Name: administrators_id_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public.administrators_id_seq', 3, true);


--
-- Name: airline_companies_id_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public.airline_companies_id_seq', 8, true);


--
-- Name: countries_id_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public.countries_id_seq', 18, true);


--
-- Name: customers_id_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public.customers_id_seq', 4, true);


--
-- Name: flights_id_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public.flights_id_seq', 6, true);


--
-- Name: tickets_id_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public.tickets_id_seq', 3, true);


--
-- Name: user_roles_id_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public.user_roles_id_seq', 4, true);


--
-- Name: users_id_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public.users_id_seq', 11, true);


--
-- Name: administrators administrators_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.administrators
    ADD CONSTRAINT administrators_pkey PRIMARY KEY (id);


--
-- Name: administrators administrators_user_id_key; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.administrators
    ADD CONSTRAINT administrators_user_id_key UNIQUE (user_id);


--
-- Name: airline_companies airline_companies_name_user_id_key; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.airline_companies
    ADD CONSTRAINT airline_companies_name_user_id_key UNIQUE (name, user_id);


--
-- Name: airline_companies airline_companies_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.airline_companies
    ADD CONSTRAINT airline_companies_pkey PRIMARY KEY (id);


--
-- Name: countries countries_name_code_name_key; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.countries
    ADD CONSTRAINT countries_name_code_name_key UNIQUE (name, code_name);


--
-- Name: countries countries_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.countries
    ADD CONSTRAINT countries_pkey PRIMARY KEY (id);


--
-- Name: customers customers_phone_credit_card_no_user_id_key; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.customers
    ADD CONSTRAINT customers_phone_credit_card_no_user_id_key UNIQUE (phone, credit_card_no, user_id);


--
-- Name: customers customers_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.customers
    ADD CONSTRAINT customers_pkey PRIMARY KEY (id);


--
-- Name: flights flights_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.flights
    ADD CONSTRAINT flights_pkey PRIMARY KEY (id);


--
-- Name: tickets tickets_flight_id_customer_id_key; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.tickets
    ADD CONSTRAINT tickets_flight_id_customer_id_key UNIQUE (flight_id, customer_id);


--
-- Name: tickets tickets_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.tickets
    ADD CONSTRAINT tickets_pkey PRIMARY KEY (id);


--
-- Name: user_roles user_roles_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.user_roles
    ADD CONSTRAINT user_roles_pkey PRIMARY KEY (id);


--
-- Name: user_roles user_roles_role_name_key; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.user_roles
    ADD CONSTRAINT user_roles_role_name_key UNIQUE (role_name);


--
-- Name: users users_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.users
    ADD CONSTRAINT users_pkey PRIMARY KEY (id);


--
-- Name: users users_username_password_email_key; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.users
    ADD CONSTRAINT users_username_password_email_key UNIQUE (username, password, email);


--
-- Name: airline_companies airline_companies_country_id_fkey; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.airline_companies
    ADD CONSTRAINT airline_companies_country_id_fkey FOREIGN KEY (country_id) REFERENCES public.countries(id);


--
-- Name: airline_companies airline_companies_user_id_fkey; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.airline_companies
    ADD CONSTRAINT airline_companies_user_id_fkey FOREIGN KEY (user_id) REFERENCES public.users(id);


--
-- Name: airline_companies airline_companies_user_id_fkey1; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.airline_companies
    ADD CONSTRAINT airline_companies_user_id_fkey1 FOREIGN KEY (user_id) REFERENCES public.administrators(user_id);


--
-- Name: customers customers_user_id_fkey; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.customers
    ADD CONSTRAINT customers_user_id_fkey FOREIGN KEY (user_id) REFERENCES public.users(id);


--
-- Name: flights flights_airline_company_id_fkey; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.flights
    ADD CONSTRAINT flights_airline_company_id_fkey FOREIGN KEY (airline_company_id) REFERENCES public.airline_companies(id);


--
-- Name: flights flights_destination_country_id_fkey; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.flights
    ADD CONSTRAINT flights_destination_country_id_fkey FOREIGN KEY (destination_country_id) REFERENCES public.countries(id);


--
-- Name: flights flights_origin_country_id_fkey; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.flights
    ADD CONSTRAINT flights_origin_country_id_fkey FOREIGN KEY (origin_country_id) REFERENCES public.countries(id);


--
-- Name: tickets tickets_customer_id_fkey; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.tickets
    ADD CONSTRAINT tickets_customer_id_fkey FOREIGN KEY (customer_id) REFERENCES public.customers(id);


--
-- Name: tickets tickets_flight_id_fkey; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.tickets
    ADD CONSTRAINT tickets_flight_id_fkey FOREIGN KEY (flight_id) REFERENCES public.flights(id);


--
-- Name: users users_user_role_fkey; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.users
    ADD CONSTRAINT users_user_role_fkey FOREIGN KEY (user_role) REFERENCES public.user_roles(id);


--
-- PostgreSQL database dump complete
--

