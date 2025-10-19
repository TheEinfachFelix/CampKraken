import sys
import json
import psycopg2
from psycopg2 import sql

#pip install psycopg2-binary

toFill = [
    {"Sname":"gender","Tname":"genders","limit":999},
    {"Sname":"nutrition","Tname":"nutritions","limit":4},
    {"Sname":"shirtSize","Tname":"shirtSizes","limit":999},
    {"Sname":"schoolType","Tname":"schoolTypes","limit":8}
]

def genChoices(name: str, conn) -> list[dict]:
    out = None
    cur = conn.cursor()
    for i in toFill:
        if name != i["Sname"]:
            continue
        
        ## queryDB
        query = sql.SQL("SELECT * FROM {} LIMIT %s").format(sql.Identifier(i["Tname"]))
        cur.execute(query, (i["limit"],))
        rows = cur.fetchall()
        out = []
        for row in rows:
            new = {}
            new["value"] = str(row[0])
            new["text"] = {}
            new["text"]["de"] = row [1]

            out.append(new)
    cur.close()
    return out

if __name__ == "__main__":
    path = "/home/ansible/myAnsible/submoduls/CampKraken/Moduls/Ausguck/frontend/src/assets/surveys/survey.json"
    conStr = f"host=192.168.178.143 port=5432 dbname=Rumpf user=postgres password=secure_password"

    ## init data
    file = open(path,"r")
    fileContent = file.read()
    file.close()
    fileJson = json.loads(fileContent)

    conn = psycopg2.connect(conStr)

    for page in fileJson["pages"]:
        #print (page)

        for element in page["elements"]:
            if (element["type"] in["radiogroup","dropdown"]):
                new = genChoices(element["name"], conn)
                if new:
                    element["choices"] = new

    # store Data
    outputJson = json.dumps(fileJson,indent=2, separators=(',', ': '),ensure_ascii = False)
    file = open(path, "w")
    file.write(outputJson)
    file.close()
    conn.close()