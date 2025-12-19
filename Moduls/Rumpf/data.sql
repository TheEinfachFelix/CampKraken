-- Gender
INSERT INTO "genders" ("genderId", "name") VALUES (0, 'männlich') ON CONFLICT ON CONSTRAINT genders_pkey DO UPDATE SET "name" = EXCLUDED."name";
INSERT INTO "genders" ("genderId", "name") VALUES (1, 'weiblich') ON CONFLICT ON CONSTRAINT genders_pkey DO UPDATE SET "name" = EXCLUDED."name";
INSERT INTO "genders" ("genderId", "name") VALUES (2, 'divers') ON CONFLICT ON CONSTRAINT genders_pkey DO UPDATE SET "name" = EXCLUDED."name";

-- Contact Info Type
INSERT INTO "contactInfoTypes" ("contactInfoTypeId", "name") VALUES (0, 'Telefon') ON CONFLICT ON CONSTRAINT "contactInfoTypes_pkey" DO UPDATE SET "name" = EXCLUDED."name";
INSERT INTO "contactInfoTypes" ("contactInfoTypeId", "name") VALUES (1, 'Fax') ON CONFLICT ON CONSTRAINT "contactInfoTypes_pkey" DO UPDATE SET "name" = EXCLUDED."name";
INSERT INTO "contactInfoTypes" ("contactInfoTypeId", "name") VALUES (2, 'Mobil') ON CONFLICT ON CONSTRAINT "contactInfoTypes_pkey" DO UPDATE SET "name" = EXCLUDED."name";
INSERT INTO "contactInfoTypes" ("contactInfoTypeId", "name") VALUES (3, 'E-Mail') ON CONFLICT ON CONSTRAINT "contactInfoTypes_pkey" DO UPDATE SET "name" = EXCLUDED."name";

-- Tags
INSERT INTO "tags" ("tagId", "name") VALUES (0, 'swimmer') ON CONFLICT ON CONSTRAINT "tags_pkey" DO UPDATE SET "name" = EXCLUDED."name";
INSERT INTO "tags" ("tagId", "name") VALUES (1, 'alone') ON CONFLICT ON CONSTRAINT "tags_pkey" DO UPDATE SET "name" = EXCLUDED."name";
INSERT INTO "tags" ("tagId", "name") VALUES (2, 'Small group') ON CONFLICT ON CONSTRAINT "tags_pkey" DO UPDATE SET "name" = EXCLUDED."name";
INSERT INTO "tags" ("tagId", "name") VALUES (3, 'picturesAllowed') ON CONFLICT ON CONSTRAINT "tags_pkey" DO UPDATE SET "name" = EXCLUDED."name";
INSERT INTO "tags" ("tagId", "name") VALUES (4, 'isHealthy') ON CONFLICT ON CONSTRAINT "tags_pkey" DO UPDATE SET "name" = EXCLUDED."name";
INSERT INTO "tags" ("tagId", "name") VALUES (5, 'hasLiabilityInsurance') ON CONFLICT ON CONSTRAINT "tags_pkey" DO UPDATE SET "name" = EXCLUDED."name";
INSERT INTO "tags" ("tagId", "name") VALUES (6, 'needsMeds') ON CONFLICT ON CONSTRAINT "tags_pkey" DO UPDATE SET "name" = EXCLUDED."name";
INSERT INTO "tags" ("tagId", "name") VALUES (7, 'supervised') ON CONFLICT ON CONSTRAINT "tags_pkey" DO UPDATE SET "name" = EXCLUDED."name";

-- Discount Codes
INSERT INTO "discountCodes" ("discountCodeId", "name") VALUES (0, 'none')              ON CONFLICT ON CONSTRAINT "discountCodes_pkey" DO UPDATE SET "name" = EXCLUDED."name";
INSERT INTO "discountCodes" ("discountCodeId", "name") VALUES (1, 'H2410')             ON CONFLICT ON CONSTRAINT "discountCodes_pkey" DO UPDATE SET "name" = EXCLUDED."name";
INSERT INTO "discountCodes" ("discountCodeId", "name") VALUES (2, 'Family10%')         ON CONFLICT ON CONSTRAINT "discountCodes_pkey" DO UPDATE SET "name" = EXCLUDED."name";
INSERT INTO "discountCodes" ("discountCodeId", "name") VALUES (3, 'Family3%')          ON CONFLICT ON CONSTRAINT "discountCodes_pkey" DO UPDATE SET "name" = EXCLUDED."name";
INSERT INTO "discountCodes" ("discountCodeId", "name") VALUES (4, 'Family5%')          ON CONFLICT ON CONSTRAINT "discountCodes_pkey" DO UPDATE SET "name" = EXCLUDED."name";
INSERT INTO "discountCodes" ("discountCodeId", "name") VALUES (5, 'Fr10%+Fam10%')      ON CONFLICT ON CONSTRAINT "discountCodes_pkey" DO UPDATE SET "name" = EXCLUDED."name";
INSERT INTO "discountCodes" ("discountCodeId", "name") VALUES (6, 'Fr10%+Fam3%')       ON CONFLICT ON CONSTRAINT "discountCodes_pkey" DO UPDATE SET "name" = EXCLUDED."name";
INSERT INTO "discountCodes" ("discountCodeId", "name") VALUES (7, 'Fr10%+Fam5%')       ON CONFLICT ON CONSTRAINT "discountCodes_pkey" DO UPDATE SET "name" = EXCLUDED."name";
INSERT INTO "discountCodes" ("discountCodeId", "name") VALUES (8, 'Früh10%')           ON CONFLICT ON CONSTRAINT "discountCodes_pkey" DO UPDATE SET "name" = EXCLUDED."name";
INSERT INTO "discountCodes" ("discountCodeId", "name") VALUES (9, 'H2410+Fr10%')       ON CONFLICT ON CONSTRAINT "discountCodes_pkey" DO UPDATE SET "name" = EXCLUDED."name";
INSERT INTO "discountCodes" ("discountCodeId", "name") VALUES (10, 'Member')           ON CONFLICT ON CONSTRAINT "discountCodes_pkey" DO UPDATE SET "name" = EXCLUDED."name";
INSERT INTO "discountCodes" ("discountCodeId", "name") VALUES (11, 'Member early')     ON CONFLICT ON CONSTRAINT "discountCodes_pkey" DO UPDATE SET "name" = EXCLUDED."name";
INSERT INTO "discountCodes" ("discountCodeId", "name") VALUES (12, 'Special Discount') ON CONFLICT ON CONSTRAINT "discountCodes_pkey" DO UPDATE SET "name" = EXCLUDED."name";
INSERT INTO "discountCodes" ("discountCodeId", "name") VALUES (13, 'refugee')          ON CONFLICT ON CONSTRAINT "discountCodes_pkey" DO UPDATE SET "name" = EXCLUDED."name";
INSERT INTO "discountCodes" ("discountCodeId", "name") VALUES (999, 'NotChecked')          ON CONFLICT ON CONSTRAINT "discountCodes_pkey" DO UPDATE SET "name" = EXCLUDED."name";


-- Shirt Sizes
INSERT INTO "shirtSizes" ("shirtSizeId", "name") VALUES (0, '118-128') ON CONFLICT ON CONSTRAINT "shirtSizes_pkey" DO UPDATE SET "name" = EXCLUDED."name";
INSERT INTO "shirtSizes" ("shirtSizeId", "name") VALUES (1, '130-140') ON CONFLICT ON CONSTRAINT "shirtSizes_pkey" DO UPDATE SET "name" = EXCLUDED."name";
INSERT INTO "shirtSizes" ("shirtSizeId", "name") VALUES (2, '142-152') ON CONFLICT ON CONSTRAINT "shirtSizes_pkey" DO UPDATE SET "name" = EXCLUDED."name";
INSERT INTO "shirtSizes" ("shirtSizeId", "name") VALUES (3, 'XS') ON CONFLICT ON CONSTRAINT "shirtSizes_pkey" DO UPDATE SET "name" = EXCLUDED."name";
INSERT INTO "shirtSizes" ("shirtSizeId", "name") VALUES (4, 'S') ON CONFLICT ON CONSTRAINT "shirtSizes_pkey" DO UPDATE SET "name" = EXCLUDED."name";
INSERT INTO "shirtSizes" ("shirtSizeId", "name") VALUES (5, 'M') ON CONFLICT ON CONSTRAINT "shirtSizes_pkey" DO UPDATE SET "name" = EXCLUDED."name";
INSERT INTO "shirtSizes" ("shirtSizeId", "name") VALUES (6, 'L') ON CONFLICT ON CONSTRAINT "shirtSizes_pkey" DO UPDATE SET "name" = EXCLUDED."name";
INSERT INTO "shirtSizes" ("shirtSizeId", "name") VALUES (7, 'XL') ON CONFLICT ON CONSTRAINT "shirtSizes_pkey" DO UPDATE SET "name" = EXCLUDED."name";
INSERT INTO "shirtSizes" ("shirtSizeId", "name") VALUES (8, 'XXL') ON CONFLICT ON CONSTRAINT "shirtSizes_pkey" DO UPDATE SET "name" = EXCLUDED."name";
INSERT INTO "shirtSizes" ("shirtSizeId", "name") VALUES (9, 'XXXL') ON CONFLICT ON CONSTRAINT "shirtSizes_pkey" DO UPDATE SET "name" = EXCLUDED."name";

-- Nutrition
INSERT INTO "nutritions" ("nutritionId", "name") VALUES (0, 'normal') ON CONFLICT ON CONSTRAINT "nutritions_pkey" DO UPDATE SET "name" = EXCLUDED."name";
INSERT INTO "nutritions" ("nutritionId", "name") VALUES (1, 'vegitarisch') ON CONFLICT ON CONSTRAINT "nutritions_pkey" DO UPDATE SET "name" = EXCLUDED."name";
INSERT INTO "nutritions" ("nutritionId", "name") VALUES (2, 'vegan') ON CONFLICT ON CONSTRAINT "nutritions_pkey" DO UPDATE SET "name" = EXCLUDED."name";
INSERT INTO "nutritions" ("nutritionId", "name") VALUES (3, 'halal') ON CONFLICT ON CONSTRAINT "nutritions_pkey" DO UPDATE SET "name" = EXCLUDED."name";
SELECT setval(pg_get_serial_sequence('"nutritions"', 'nutritionId'), COALESCE(MAX("nutritionId"), 0) + 1, false) FROM "nutritions";

-- School Types
INSERT INTO "schoolTypes" ("schoolTypeId", "name") VALUES (0, 'Grundschule') ON CONFLICT ON CONSTRAINT "schoolTypes_pkey" DO UPDATE SET "name" = EXCLUDED."name";
INSERT INTO "schoolTypes" ("schoolTypeId", "name") VALUES (1, 'Sekundarschule') ON CONFLICT ON CONSTRAINT "schoolTypes_pkey" DO UPDATE SET "name" = EXCLUDED."name";
INSERT INTO "schoolTypes" ("schoolTypeId", "name") VALUES (2, 'Förderschule') ON CONFLICT ON CONSTRAINT "schoolTypes_pkey" DO UPDATE SET "name" = EXCLUDED."name";
INSERT INTO "schoolTypes" ("schoolTypeId", "name") VALUES (3, 'Realschule') ON CONFLICT ON CONSTRAINT "schoolTypes_pkey" DO UPDATE SET "name" = EXCLUDED."name";
INSERT INTO "schoolTypes" ("schoolTypeId", "name") VALUES (4, 'Gymnasium') ON CONFLICT ON CONSTRAINT "schoolTypes_pkey" DO UPDATE SET "name" = EXCLUDED."name";
INSERT INTO "schoolTypes" ("schoolTypeId", "name") VALUES (5, 'Gesamtschule') ON CONFLICT ON CONSTRAINT "schoolTypes_pkey" DO UPDATE SET "name" = EXCLUDED."name";
INSERT INTO "schoolTypes" ("schoolTypeId", "name") VALUES (6, 'Waldorfschule') ON CONFLICT ON CONSTRAINT "schoolTypes_pkey" DO UPDATE SET "name" = EXCLUDED."name";
INSERT INTO "schoolTypes" ("schoolTypeId", "name") VALUES (7, 'Andere Schulen') ON CONFLICT ON CONSTRAINT "schoolTypes_pkey" DO UPDATE SET "name" = EXCLUDED."name";