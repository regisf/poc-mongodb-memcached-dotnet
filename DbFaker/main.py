import logging
import os
import random
import time
import uuid

import pymongo
from faker import Faker
from faker.providers import barcode, internet
from pymongo.errors import CollectionInvalid

count = int(os.getenv("DBCOUNT", 1_000_000))
conn_str = f"mongodb://{os.getenv('DBSERVER', '127.0.0.1')}/backend"


def generate_database_entries():
    fake = Faker()
    fake.add_provider(barcode)
    fake.add_provider(internet)

    logging.info(f"Generating {count} entries")
    fakes = []
    step = count // 100
    counter = 0
    for i in range(count):
        if not (i % step):
            counter += 1

        fakes.append({
            "guid": str(uuid.uuid4()),
            "name": fake.catch_phrase(),
            "category": "",
            "description": '\n'.join(fake.paragraphs(nb=5)),
            "ean": fake.ean(length=13)
        })

        print(f"\rGenerating {counter} %", end='')

    return fakes


def save_into_database(entries):
    client = pymongo.MongoClient(conn_str, serverSelectionTimeoutMS=500)
    db = client.get_database('backend')
    try:
        collection = db.create_collection('entries')
    except CollectionInvalid:
        collection = db.get_collection('entries')
    collection.insert_many(entries)


def main():
    start = time.time()
    entries = generate_database_entries()
    save_into_database(entries)
    end = time.time()
    print(f"\nDone in {end - start:.2f} sec")


if __name__ == '__main__':
    main()
