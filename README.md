# Proof of Concept 

Using Memcached as local cache for MongoDB with dotnet 6

## Requirements

* docker
* docker-compose

## Starting

Just enter the command below 

	docker compose up
	
Caution: Generating 1 million of entries took at least 2 minutes

## Notes

There isn't  indexes or shardind to slow down MongoDB capabilities.

Each time you starts the stack without downing it, 1 million new entries are created into the database. 

## Testing

Start your REST querying software and requests thoose URLS

* http://localhost:5000/hello : 
* http://localhost:5000/hello/guids : Get all UID from the database. The extraction is too big for memcached and cannot be stored in memory
* http://localhost:5000/hello/{{uuid}} : Get a particular entry

