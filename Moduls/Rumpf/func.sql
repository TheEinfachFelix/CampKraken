CREATE OR REPLACE FUNCTION insert_participant(
    _data JSONB,
    _tags TEXT[]
)
RETURNS INT AS $$
DECLARE
    pid INT;                -- personId
    partid INT;             -- participantId
    contact JSONB;
    tagname TEXT;
    tagid INT;
    nut TEXT;
    day RECORD;
    discount_fixed CONSTANT INT := 999; -- immer 999
    shirt_size_id INT;
    school_type_id INT;
BEGIN
    SET search_path = pg_catalog, public, pg_temp;
    -- Defaults für NOT NULL-Felder
    shirt_size_id := COALESCE(NULLIF((_data->>'shirtSize')::int, 0), 1);
    school_type_id := COALESCE(NULLIF((_data->>'schoolType')::int, 0), 1);

    -------------------------------------------------------------------
    -- 1️⃣ Person anlegen
    -------------------------------------------------------------------
    INSERT INTO "person" ("firstName", "lastName", "dateOfBirth", "genderId")
    VALUES (
        _data->>'firstName',
        _data->>'lastName',
        CASE WHEN (_data ? 'dateOfBirth') AND _data->>'dateOfBirth' <> '' THEN (_data->>'dateOfBirth')::date ELSE NULL END,
        CASE WHEN (_data ? 'gender') AND _data->>'gender' <> '' THEN (_data->>'gender')::int ELSE NULL END
    )
    RETURNING "personId" INTO pid;

    -------------------------------------------------------------------
    -- 2️⃣ Adresse anlegen
    -------------------------------------------------------------------
    INSERT INTO "addresses" ("personId", "streetAndNumber", "zipCode", "city", "coverName")
    VALUES (
        pid,
        _data->>'streetAndNumber',
        CASE WHEN (_data ? 'zipCode') AND _data->>'zipCode' <> '' THEN (_data->>'zipCode')::bigint ELSE NULL END,
        _data->>'city',
        _data->>'coverName'
    );

    -------------------------------------------------------------------
    -- 3️⃣ Kontakte / Telefon
    -------------------------------------------------------------------
    IF _data ? 'contacts' THEN
        FOR contact IN SELECT * FROM jsonb_array_elements(_data->'contacts')
        LOOP
            INSERT INTO "contactInfo" ("personId", "contactInfoTypeId", "info", "details")
            VALUES (
                pid,
                0, -- Telefon Type
                NULLIF(contact->>'number',''),
                NULLIF(contact->>'who','')
            );
        END LOOP;
    END IF;

    -------------------------------------------------------------------
    -- 4️⃣ E-Mail
    -------------------------------------------------------------------
    IF (_data ? 'email') AND _data->>'email' <> '' THEN
        INSERT INTO "contactInfo" ("personId", "contactInfoTypeId", "info")
        VALUES (pid, 3, _data->>'email');  -- 3 = E-Mail Type
    END IF;

    -------------------------------------------------------------------
    -- 5️⃣ Participant erzeugen
    -------------------------------------------------------------------
    INSERT INTO "participants" (
        "personId",
        "discountCodeId",
        "userDiscountCode",
        "shirtSizeId",
        "selectedSlot"
    ) VALUES (
        pid,
        discount_fixed,
        NULLIF(_data->>'userDiscountCode',''),
        shirt_size_id,
        CASE
            WHEN (_data->>'start-date' IS NOT NULL)
            AND (_data->>'end-date' IS NOT NULL)
            THEN CONCAT(
                COALESCE(_data->>'selectedSlot',''), '$', 
                COALESCE(_data->>'start-date',''), '$', 
                COALESCE(_data->>'end-date','')
            )
            ELSE COALESCE(_data->>'selectedSlot','')
        END
    )
    RETURNING "participantId" INTO partid;

    -------------------------------------------------------------------
    -- 6️⃣ participantsPrivate anlegen
    -------------------------------------------------------------------
    INSERT INTO "participantsPrivate" (
        "participantId",
        "schoolTypeId",
        "doctor",
        "insuredBy",
        "intolerances",
        "healthInfo",
        "specialInfos",
        "healthInsuranceName"
    )
    VALUES (
        partid,
        school_type_id,
        NULLIF(_data->>'doctor',''),
        NULLIF(_data->>'insuredBy',''),
        NULLIF(_data->>'intolerances',''),
        NULLIF(_data->>'healthInfo',''),
        NULLIF(_data->>'specialInfos',''),
        NULLIF(_data->>'healthInsuranceName','')
    );

    -------------------------------------------------------------------
    -- 7️⃣ Nutrition
    -------------------------------------------------------------------
    IF _data ? 'nutrition' THEN
        FOR nut IN SELECT jsonb_array_elements_text(_data->'nutrition')
        LOOP
            IF nut IS NOT NULL AND nut <> '' THEN
                INSERT INTO "nutritionsToPrivate" ("nutritionId", "participantId")
                VALUES (nut::int, partid)
                ON CONFLICT DO NOTHING;
            END IF;
        END LOOP;
    END IF;

    -------------------------------------------------------------------
    -- 8️⃣ Presences
    -------------------------------------------------------------------
    IF (_data ? 'start-date') AND (_data ? 'end-date') THEN
        FOR day IN
            SELECT "dayId"
            FROM "days"
            WHERE "date" BETWEEN (_data->>'start-date')::date
                             AND (_data->>'end-date')::date
        LOOP
            INSERT INTO "presences" ("personId", "dayId")
            VALUES (pid, day."dayId")
            ON CONFLICT DO NOTHING;
        END LOOP;
    END IF;

    -------------------------------------------------------------------
    -- 9️⃣ Tags
    -------------------------------------------------------------------
    IF _tags IS NOT NULL THEN
        FOREACH tagname IN ARRAY _tags
        LOOP
            SELECT "tagId" INTO tagid FROM "tags" WHERE "name" = tagname;
            IF tagid IS NOT NULL THEN
                INSERT INTO "tagToParticipant" ("participantId", "tagId")
                VALUES (partid, tagid)
                ON CONFLICT DO NOTHING;
            END IF;
        END LOOP;
    END IF;

    RETURN pid;
END;
$$ LANGUAGE plpgsql
SECURITY DEFINER;