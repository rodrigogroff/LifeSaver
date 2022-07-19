
CREATE TABLE IF NOT EXISTS public."User" ( id bigserial NOT NULL, PRIMARY KEY (id)) WITH (OIDS = FALSE);
ALTER TABLE public."User" OWNER to postgres;
ALTER TABLE public."User" ADD COLUMN if not exists "stEmail" character varying(250);
ALTER TABLE public."User" ADD COLUMN if not exists "stMobile" character varying(250);
ALTER TABLE public."User" ADD COLUMN if not exists "stPassword" character varying(250);
ALTER TABLE public."User" ADD COLUMN if not exists "bActive" boolean;
ALTER TABLE public."User" ADD COLUMN if not exists "stName" character varying(250);
ALTER TABLE public."User" ADD COLUMN if not exists "dtJoin" timestamp without time zone;
ALTER TABLE public."User" ADD COLUMN if not exists "dtLastLogin" timestamp without time zone;

CREATE TABLE IF NOT EXISTS public."UserRegisterCode" ( id bigserial NOT NULL, PRIMARY KEY (id)) WITH (OIDS = FALSE);
ALTER TABLE public."UserRegisterCode" OWNER to postgres;
ALTER TABLE public."UserRegisterCode" ADD COLUMN if not exists "stCode" character varying(20);
ALTER TABLE public."UserRegisterCode" ADD COLUMN if not exists "stMobile" character varying(20);
ALTER TABLE public."UserRegisterCode" ADD COLUMN if not exists "fkUser" int;
ALTER TABLE public."UserRegisterCode" ADD COLUMN if not exists "dtRequest" timestamp without time zone;
ALTER TABLE public."UserRegisterCode" ADD COLUMN if not exists "dtExpires" timestamp without time zone;

CREATE TABLE IF NOT EXISTS public."UserPassRenewal" ( id bigserial NOT NULL, PRIMARY KEY (id)) WITH (OIDS = FALSE);
ALTER TABLE public."UserPassRenewal" OWNER to postgres;
ALTER TABLE public."UserPassRenewal" ADD COLUMN if not exists "stCode" character varying(20);
ALTER TABLE public."UserPassRenewal" ADD COLUMN if not exists "stMobile" character varying(20);
ALTER TABLE public."UserPassRenewal" ADD COLUMN if not exists "fkUser" int;
ALTER TABLE public."UserPassRenewal" ADD COLUMN if not exists "dtRequest" timestamp without time zone;
ALTER TABLE public."UserPassRenewal" ADD COLUMN if not exists "dtExpires" timestamp without time zone;

CREATE TABLE IF NOT EXISTS public."ItemFolder" ( id bigserial NOT NULL, PRIMARY KEY (id)) WITH (OIDS = FALSE);
ALTER TABLE public."ItemFolder" OWNER to postgres;
ALTER TABLE public."ItemFolder" ADD COLUMN if not exists "fkUser" int;
ALTER TABLE public."ItemFolder" ADD COLUMN if not exists "stName" character varying(200);
ALTER TABLE public."ItemFolder" ADD COLUMN if not exists "fkFolder" int;
ALTER TABLE public."ItemFolder" ADD COLUMN if not exists "dtRegister" timestamp without time zone;
ALTER TABLE public."ItemFolder" ADD COLUMN if not exists "bIncome" boolean;

CREATE TABLE IF NOT EXISTS public."Item" ( id bigserial NOT NULL, PRIMARY KEY (id)) WITH (OIDS = FALSE);
ALTER TABLE public."Item" OWNER to postgres;
ALTER TABLE public."Item" ADD COLUMN if not exists "fkUser" int;
ALTER TABLE public."Item" ADD COLUMN if not exists "fkFolder" int;
ALTER TABLE public."Item" ADD COLUMN if not exists "bIncome" boolean;
ALTER TABLE public."Item" ADD COLUMN if not exists "stName" character varying(20);
ALTER TABLE public."Item" ADD COLUMN if not exists "nuPeriod" int;
ALTER TABLE public."Item" ADD COLUMN if not exists "nuExpectedDay" int;
ALTER TABLE public."Item" ADD COLUMN if not exists "vlBaseCents" int;
ALTER TABLE public."Item" ADD COLUMN if not exists "dtRegister" timestamp without time zone;

CREATE TABLE IF NOT EXISTS public."ItemDrop" ( id bigserial NOT NULL, PRIMARY KEY (id)) WITH (OIDS = FALSE);
ALTER TABLE public."ItemDrop" OWNER to postgres;
ALTER TABLE public."ItemDrop" ADD COLUMN if not exists "fkUser" int;
ALTER TABLE public."ItemDrop" ADD COLUMN if not exists "fkItem" int;
ALTER TABLE public."ItemDrop" ADD COLUMN if not exists "fkFolder" int;
ALTER TABLE public."ItemDrop" ADD COLUMN if not exists "bActive" boolean;
ALTER TABLE public."ItemDrop" ADD COLUMN if not exists "vlCents" int;
ALTER TABLE public."ItemDrop" ADD COLUMN if not exists "dtRegister" timestamp without time zone;
ALTER TABLE public."ItemDrop" ADD COLUMN if not exists "nuDay" int;
ALTER TABLE public."ItemDrop" ADD COLUMN if not exists "nuMonth" int;
ALTER TABLE public."ItemDrop" ADD COLUMN if not exists "nuYear" int;
