CREATE OR REPLACE FUNCTION insert_participant(
    _data JSONB,
    _tags TEXT[]
)
RETURNS INT AS $$
DECLARE
    pid INT;            -- personId
    partid INT;         -- participantId
    nut_raw TEXT;
    nut_id INT;
BEGIN
    -- -------------------------------------------------------------------
    -- 1️⃣ Person einfügen
    -- -------------------------------------------------------------------
    INSERT INTO "person" ("firstName", "lastName", "dateOfBirth", "genderId")
    VALUES (
        _data->>'firstName',
        _data->>'lastName',
        CASE WHEN (_data ? 'dateOfBirth') AND _data->>'dateOfBirth' <> '' THEN (_data->>'dateOfBirth')::date ELSE NULL END,
        (_data->>'gender')::int
    )
    RETURNING "personId" INTO pid;

    -- -------------------------------------------------------------------
    -- 2️⃣ Adresse einfügen
    -- -------------------------------------------------------------------
    INSERT INTO "addresses" ("personId", "streetAndNumber", "zipCode", "city", "coverName")
    VALUES (
        pid,
        _data->>'streetAndNumber',
        NULLIF((_data->>'zipCode')::bigint, 0),
        _data->>'city',
        _data->>'coverName'
    );

    -- -------------------------------------------------------------------
    -- 3️⃣ Participant einfügen
    -- -------------------------------------------------------------------
    INSERT INTO "participants" ("personId", "discountCodeId", "userDiscountCode", "shirtSizeId", "selectedSlot", "participantSrc")
    VALUES (
        pid,
        999, -- discount_fixed
        NULLIF(_data->>'userDiscountCode',''),
        COALESCE(NULLIF((_data->>'shirtSize')::int, 0), 1),
        CASE
            WHEN (_data->>'start-date' IS NOT NULL) AND (_data->>'end-date' IS NOT NULL)
            THEN CONCAT(COALESCE(_data->>'selectedSlot',''), '$', COALESCE(_data->>'start-date',''), '$', COALESCE(_data->>'end-date',''))
            ELSE COALESCE(_data->>'selectedSlot','')
        END,
        NULLIF(_data->>'participantSrc','')
    )
    RETURNING "participantId" INTO partid;

    -- -------------------------------------------------------------------
    -- 4️⃣ participantsPrivate einfügen
    -- -------------------------------------------------------------------
    INSERT INTO "participantsPrivate" (
        "participantId", "schoolTypeId", "doctor", "insuredBy",
        "intolerances", "healthInfo", "specialInfos", "healthInsuranceName"
    )
    VALUES (
        partid,
        COALESCE(NULLIF((_data->>'schoolType')::int, 0), 1),
        NULLIF(_data->>'doctor',''),
        NULLIF(_data->>'insuredBy',''),
        NULLIF(_data->>'intolerances',''),
        NULLIF(_data->>'healthInfo',''),
        NULLIF(_data->>'specialInfos',''),
        NULLIF(_data->>'healthInsuranceName','')
    );

    -- -------------------------------------------------------------------
    -- 5️⃣ Kontaktinformationen
    -- -------------------------------------------------------------------
    IF _data ? 'contacts' THEN
        INSERT INTO "contactInfo" ("personId", "contactInfoTypeId", "info", "details")
        SELECT
            pid,
            0,
            NULLIF(contact->>'number',''),
            NULLIF(contact->>'who','')
        FROM jsonb_array_elements(_data->'contacts') AS contact
        WHERE NULLIF(contact->>'number','') IS NOT NULL;
    END IF;

    IF (_data ? 'email') AND _data->>'email' <> '' THEN
        INSERT INTO "contactInfo" ("personId", "contactInfoTypeId", "info")
        VALUES (pid, 3, _data->>'email');
    END IF;

    -- 6️⃣ Nutrition
    IF _data ? 'nutrition' THEN
        FOR nut_raw IN
            SELECT value::text FROM jsonb_array_elements_text(_data->'nutrition') AS t(value)
        LOOP
            IF nut_raw IS NOT NULL AND nut_raw <> '' THEN
                -- Wenn ID gegeben ist, wiederverwenden, sonst neu erzeugen
                IF _data ? 'nutritionId' THEN
                    nut_id := (_data->>'nutritionId')::int;
                ELSE
                    INSERT INTO "nutritions" ("name")
                    VALUES (nut_raw)
                    RETURNING "nutritionId" INTO nut_id;
                END IF;

                -- Immer neu in nutritionsToPrivate
                INSERT INTO "nutritionsToPrivate" ("nutritionId", "participantId")
                VALUES (nut_id, partid);
            END IF;
        END LOOP;
    END IF;

    -- 7️⃣ Presences
    IF (_data ? 'start-date') AND (_data ? 'end-date') THEN
        INSERT INTO "presences" ("personId", "dayId")
        SELECT pid, "dayId"
        FROM "days"
        WHERE "date" BETWEEN (_data->>'start-date')::date AND (_data->>'end-date')::date;
    END IF;

    -- 8️⃣ Tags
    IF _tags IS NOT NULL AND array_length(_tags, 1) > 0 THEN
        INSERT INTO "tagToParticipant" ("participantId", "tagId")
        SELECT partid, t."tagId"
        FROM unnest(_tags) AS tagname
        JOIN "tags" t ON t."name" = tagname;
    END IF;
    RETURN pid;
END;
$$ LANGUAGE plpgsql
SECURITY DEFINER;
