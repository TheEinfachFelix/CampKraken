# CampKraken
This is the IT Backbone of our Camp

# TODO

[x] DB Spawning
- fixe add Constraint
[ ] DB Schema Auto
[ ] DB Perms
[ ] DB Data
[ ] DB Mirror
Online Form
Webserver
API


grep -E "(DROP TABLE|DROP DATABASE)" schema.sql && echo "❌ Drop detected!" && exit 1

ALTER TABLE users ADD COLUMN IF NOT EXISTS nickname TEXT;