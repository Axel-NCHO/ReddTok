ffmpeg -i %1 -i %2 -shortest -fflags shortest -max_interleave_delta 100M -c:v copy -c:a aac -strict experimental -map 0:v:0 -map 1:a:0 -y %3
echo file '%3' >> list.txt