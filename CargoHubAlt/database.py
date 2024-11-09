import sqlite3
import os
import sys

def connect():
    conn = sqlite3.connect(os.path.join(sys.path[0], 'CargoHubDatabase.db'))
    c = conn.cursor()
    return conn, c

def read_json(file):
    import json
    with open(os.path.join(sys.path[0], f"data/{file}")) as f:
        data = json.load(f)
    return data


def item_lines(conn, c, lister):
    for dict in lister:
        for i in dict.keys():
            if dict[i] == None:
                dict[i] = "N/A"
        c.execute("INSERT INTO Item_Lines (Id, Source_Id, Order_Date, Created_At, Updated_At) VALUES (?, ?, ?, ?, ?)", (dict['id'], dict['name'], dict['description'], dict['created_at'], dict['updated_at']))


def main():
    conn, c = connect()
    item_lines(conn, c, read_json('item_lines.json'))
    conn.commit()
    conn.close()


if __name__ == '__main__':
    main()