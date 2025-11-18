
CREATE OR REPLACE FUNCTION insert_participant(
    _data JSONB,
    _tags TEXT[]
)
RETURNS INT AS $$
DECLARE
    pid INT;            -- personId
    partid INT;         -- participantId
    contact JSONB;
    tagname TEXT;
    tagid INT;
    n TEXT;
    day RECORD;
BEGIN
    -- 1️⃣ Person anlegen
    INSERT INTO person (firstName, lastName, dateOfBirth, genderId)
    VALUES (
        _data->>'firstName',
        _data->>'lastName',
        (_data->>'dateOfBirth')::date,
        (_data->>'gender')::int
    )
    RETURNING personId INTO pid;

    -- 2️⃣ Adresse anlegen
    INSERT INTO addresses (personId, streetAndNumber, zipCode, city, coverName)
    VALUES (
        pid,
        _data->>'streetAndNumber',
        (_data->>'zipCode')::int,
        _data->>'city',
        _data->>'coverName'
    );

    -- 3️⃣ Contacts aus JSON (Telefonnummern)
    FOR contact IN SELECT * FROM jsonb_array_elements(_data->'contacts')
    LOOP
        INSERT INTO contactInfo (personId, contactInfoTypeId, info, details)
        VALUES (
            pid,
            0,                                      -- Telefon Type
            contact->>'number',
            contact->>'who'
        );
    END LOOP;

    -- 4️⃣ Email (falls vorhanden)
    IF (_data ? 'email') AND _data->>'email' <> '' THEN
        INSERT INTO contactInfo (personId, contactInfoTypeId, info)
        VALUES (
            pid,
            3,                                      -- E-Mail Type
            _data->>'email'
        );
    END IF;

    -- 5️⃣ Participant erzeugen
    INSERT INTO participants (
        personId,
        discountCodeId,
        userDiscountCode,
        shirtSizeId,
        selectedSlot
    )
    VALUES (
        pid,
        (SELECT "discountCodeId" FROM discountCodes WHERE name = 'NotChecked' LIMIT 1),
        _data->>'userDiscountCode',
        (_data->>'shirtSize')::int,
        _data->>'selectedSlot'
    )
    RETURNING participantId INTO partid;

    -- 6️⃣ ParticipantsPrivate anlegen
    INSERT INTO participantsPrivate (
        participantId,
        schoolTypeId,
        doctor,
        insuredBy,
        intolerances,
        healthInfo,
        specialInfos,
        healthInsuranceName 
    )
    VALUES (
        partid,
        (_data->>'schoolType')::int,
        _data->>'doctor',
        _data->>'insuredBy',
        _data->>'intolerances',
        _data->>'healthInfo',
        _data->>'specialInfos',
        _data->>'healthInsuranceName'
    );

    IF _data ? 'nutrition' THEN
        FOREACH n IN ARRAY (SELECT jsonb_array_elements_text(_data->'nutrition'))
        LOOP
            INSERT INTO nutritionsToPrivate (participantId, nutritionId)
            VALUES (partid, n::int)
            ON CONFLICT DO NOTHING;
        END LOOP;
    END IF;

    -- presences automatisch anlegen
    FOR day IN
        SELECT dayId
        FROM days
        WHERE date BETWEEN (_data->>'start-date')::date
                    AND (_data->>'end-date')::date
    LOOP
        INSERT INTO presences (personId, dayId)
        VALUES (pid, day.dayId)
        ON CONFLICT DO NOTHING;
    END LOOP;

    -- 7️⃣ Tags aus dem Array _tags
    IF _tags IS NOT NULL THEN
        FOREACH tagname IN ARRAY _tags
        LOOP
            SELECT tagId INTO tagid FROM tags WHERE name = tagname;
            IF tagid IS NOT NULL THEN
                INSERT INTO tagToParticipant (participantId, tagId)
                VALUES (partid, tagid)
                ON CONFLICT DO NOTHING;
            END IF;
        END LOOP;
    END IF;

    RETURN partid;
END;
$$ LANGUAGE plpgsql;