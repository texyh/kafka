echo " == stopping docker compose =="
docker-compose -f kafka.yml down

echo "== starting kafka =="
docker-compose -f kafka.yml up -d --force-recreate