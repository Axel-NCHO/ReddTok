ffmpeg -i %1 -i %2 -c:v copy -c:a aac -shortest -strict experimental -map 0:v:0 -map 1:a:0 -y %3
echo file '%3' >> list.txt