
# HelloDB
Database build from scratch just for learning purpose. It has all the basic commands and it maintains a **B+ tree** index for records, So every operation is Log(n). It also saves data in csv format in file and we can resume database session.

## Usage
#### Meta Commands
these commands will always start with '.(dot)'
- list database: `.listdb` shows list of currenlty saved past database, that you we can resume consuming.
- open database: `.db <dbname>` open already existing database, else creates new one. If opened in middle of session, it will overwrite previous data.
- exit: `.exit` terminates current session, and prompts for saving progress so far in database.

So Table format is fixed, only supporting in  ID <unique>, Name, Email feilds

#### Basic Commands
- insert: `Insert <id> <name> <mail>`
- delete: `Delete <id>`
- update: `Update <id> <name> <mail>`
- select: `select <id1> <id2>...`
- select all: `select`

> Publish folder has ready compiled .exe for usage

## Project Structure

<img width="827" alt="lld" src="https://github.com/user-attachments/assets/70988039-4061-43c2-bc70-2aeae9810890">

## Data Storage Format using B+Tree

![image](https://github.com/user-attachments/assets/252aebd1-5662-402a-a0c4-79bd885ce9c1)


## Example Tutorial Here

[drive link](https://drive.google.com/file/d/1PzDCXHcyOyOZt3hMduNyJfTOtoMAS6Sl/view?usp=sharing)
