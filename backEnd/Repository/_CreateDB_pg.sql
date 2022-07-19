
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
