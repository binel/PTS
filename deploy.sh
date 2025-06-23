docker build -t localhost/pts-ui:latest ./pts-ui --build-arg REACT_APP_API_URL=http://homelab.io/ptsapi
docker build -t localhost/pts-api:latest -f PTS.Api/Dockerfile .
docker save localhost/pts-ui:latest -o pts-ui.tar
docker save localhost/pts-api:latest -o pts-api.tar
k3s ctr images import pts-ui.tar 
k3s ctr images import pts-api.tar 
kubectl rollout restart deployment pts-ui
kubectl rollout restart deployment pts-api 