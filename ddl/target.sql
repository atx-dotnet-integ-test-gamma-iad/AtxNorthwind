--
-- PostgreSQL database dump (Northwind - northwind_dbo schema tables only)
--

CREATE SCHEMA northwind_dbo;

CREATE TABLE northwind_dbo.categories (
    categoryid integer NOT NULL,
    categoryname public.citext NOT NULL,
    description public.citext,
    picture bytea,
    CONSTRAINT ck_categories_len_categoryname CHECK ((length((categoryname)::text) <= 15))
);

CREATE TABLE northwind_dbo.customers (
    customerid public.citext NOT NULL,
    companyname public.citext NOT NULL,
    contactname public.citext,
    contacttitle public.citext,
    address public.citext,
    city public.citext,
    region public.citext,
    postalcode public.citext,
    country public.citext,
    phone public.citext,
    fax public.citext,
    CONSTRAINT ck_customers_len_address CHECK ((length((address)::text) <= 60)),
    CONSTRAINT ck_customers_len_city CHECK ((length((city)::text) <= 15)),
    CONSTRAINT ck_customers_len_companyname CHECK ((length((companyname)::text) <= 40)),
    CONSTRAINT ck_customers_len_contactname CHECK ((length((contactname)::text) <= 30)),
    CONSTRAINT ck_customers_len_contacttitle CHECK ((length((contacttitle)::text) <= 30)),
    CONSTRAINT ck_customers_len_country CHECK ((length((country)::text) <= 15)),
    CONSTRAINT ck_customers_len_customerid CHECK ((length((customerid)::text) <= 5)),
    CONSTRAINT ck_customers_len_fax CHECK ((length((fax)::text) <= 24)),
    CONSTRAINT ck_customers_len_phone CHECK ((length((phone)::text) <= 24)),
    CONSTRAINT ck_customers_len_postalcode CHECK ((length((postalcode)::text) <= 10)),
    CONSTRAINT ck_customers_len_region CHECK ((length((region)::text) <= 15))
);

CREATE TABLE northwind_dbo.orderdetails (
    orderid integer NOT NULL,
    productid integer NOT NULL,
    unitprice numeric(19,4) DEFAULT 0 NOT NULL,
    quantity smallint DEFAULT 1 NOT NULL,
    discount double precision DEFAULT 0 NOT NULL
);

CREATE TABLE northwind_dbo.orders (
    orderid integer NOT NULL,
    customerid public.citext,
    employeeid integer,
    orderdate timestamp without time zone,
    requireddate timestamp without time zone,
    shippeddate timestamp without time zone,
    shipvia integer,
    freight numeric(19,4) DEFAULT 0,
    shipname public.citext,
    shipaddress public.citext,
    shipcity public.citext,
    shipregion public.citext,
    shippostalcode public.citext,
    shipcountry public.citext,
    CONSTRAINT ck_orders_len_customerid CHECK ((length((customerid)::text) <= 5)),
    CONSTRAINT ck_orders_len_shipaddress CHECK ((length((shipaddress)::text) <= 60)),
    CONSTRAINT ck_orders_len_shipcity CHECK ((length((shipcity)::text) <= 15)),
    CONSTRAINT ck_orders_len_shipcountry CHECK ((length((shipcountry)::text) <= 15)),
    CONSTRAINT ck_orders_len_shipname CHECK ((length((shipname)::text) <= 40)),
    CONSTRAINT ck_orders_len_shippostalcode CHECK ((length((shippostalcode)::text) <= 10)),
    CONSTRAINT ck_orders_len_shipregion CHECK ((length((shipregion)::text) <= 15))
);

CREATE TABLE northwind_dbo.suppliers (
    supplierid integer NOT NULL,
    companyname public.citext NOT NULL,
    contactname public.citext,
    contacttitle public.citext,
    address public.citext,
    city public.citext,
    region public.citext,
    postalcode public.citext,
    country public.citext,
    phone public.citext,
    fax public.citext,
    homepage public.citext,
    CONSTRAINT ck_suppliers_len_address CHECK ((length((address)::text) <= 60)),
    CONSTRAINT ck_suppliers_len_city CHECK ((length((city)::text) <= 15)),
    CONSTRAINT ck_suppliers_len_companyname CHECK ((length((companyname)::text) <= 40)),
    CONSTRAINT ck_suppliers_len_contactname CHECK ((length((contactname)::text) <= 30)),
    CONSTRAINT ck_suppliers_len_contacttitle CHECK ((length((contacttitle)::text) <= 30)),
    CONSTRAINT ck_suppliers_len_country CHECK ((length((country)::text) <= 15)),
    CONSTRAINT ck_suppliers_len_fax CHECK ((length((fax)::text) <= 24)),
    CONSTRAINT ck_suppliers_len_phone CHECK ((length((phone)::text) <= 24)),
    CONSTRAINT ck_suppliers_len_postalcode CHECK ((length((postalcode)::text) <= 10)),
    CONSTRAINT ck_suppliers_len_region CHECK ((length((region)::text) <= 15))
);
