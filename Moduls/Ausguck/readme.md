# Ausguck

Die Online Anmeldung des Camps

## Todos

[x] Projektstruktur
[ ] Form Erstellen
[x] Form automatisch befüllen
    [ ] Playbook
[ ] Webserver
    [x] Displaying the Form
    [ ] Calling the API
    [ ] (opt) Reloading the data
    [ ] Playbooks
    [ ] integration test
    [x] Translation
[ ] API
    [X] DB User
    [ ] DB insert
    [ ] Trap
    [ ] Playbook
    [ ] Creds handeling

### minis

diferent other Contact info







🔑 Problem

PostgreSQL prüft Fremdschlüssel beim INSERT automatisch.

Beispiel: Du willst in participants einen Datensatz einfügen, der personId referenziert.

PostgreSQL will prüfen, dass personId existiert → dafür ist normalerweise SELECT auf person nötig.

Wenn der Benutzer kein SELECT hat → permission denied.

➡️ Mit minimalrechten einfaches INSERT über FK geht nicht direkt, außer du machst Tricks.

⚙️ Mögliche Lösungen
1️⃣ Separate Write-Only API / Stored Procedure

Erstelle eine Stored Procedure oder Function, die alle INSERTs übernimmt.

Der Benutzer bekommt nur EXECUTE-Recht auf die Function, keine direkten Table-Rechte.

Die Function läuft im Kontext eines privilegierten Users (SECURITY DEFINER) → kann alles schreiben.

Vorteil: User kann nur schreiben, nicht lesen, und nur über die API-Logik.

Beispiel: